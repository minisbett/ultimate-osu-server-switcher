using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class Switcher
  {
    // The settings for the switcher
    private static Settings m_settings => new Settings(Paths.SettingsFile);

    /// <summary>
    /// The servers that were parsed from the web
    /// </summary>
    public static List<Server> Servers { get; set; }

    /// <summary>
    /// Switch the server
    /// </summary>
    /// <param name="server">The server to switch to</param>
    public static void SwitchServer(Server server)
    {
      // Remember the old server for telemetry
      Server from = GetCurrentServer();

      // Get rid of all uneccessary certificates
      CertificateManager.UninstallAllCertificates(Servers);

      // Clean up the hosts file
      List<string> hosts = HostsUtil.GetHosts().ToList();
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));
      HostsUtil.SetHosts(hosts.ToArray());

      // Edit the hosts file if the server is not bancho
      if (!server.IsBancho)
      {
        // Change the hosts file by adding the server's ip and the osu domain
        hosts = HostsUtil.GetHosts().ToList();
        foreach (string domain in Variables.OsuDomains)
          hosts.Add(server.IP + " " + domain);
        HostsUtil.SetHosts(hosts.ToArray());

        // Install the certificate if needed (not needed for bancho and localhost)
        if (server.HasCertificate)
          CertificateManager.InstallCertificate(server);
      }

      // Send telemetry if enabled
      if (m_settings["sendTelemetry"] == "true")
      {
        SendTelemetry(from, server);
      }
    }

    /// <summary>
    /// Returns the server the user is currently connected to
    /// </summary>
    public static Server GetCurrentServer()
    {
      // Get every line of the hosts file
      string[] hosts = HostsUtil.GetHosts();

      // Check for the first line that contains the osu domain
      for (int i = 0; i < hosts.Length; i++)
        if (hosts[i].Contains(".ppy.sh"))
        {
          // Read the ip that .ppy.sh redirects to
          string ip = hosts[i].Replace("\t", " ").Split(' ')[0];

          // Try to identify and return the server
          return Servers.FirstOrDefault(x => x.IP == ip) ?? Server.UnidentifiedServer;
        }

      // Because after downloading icon etc the bancho server object is not equal to Server.BanchoServer
      // because the Icon property is set then
      return Servers.First(x => x.IsBancho);
    }

    private static void SendTelemetry(Server from, Server to)
    {
      // Get the span between this and the last switching in unix milliseconds
      int span = -1;
      long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
      // If the unix time in the telemetry cache somehow cannot be converted
      // For example if the file was edited or the timestamp not set in the first place
      // Send an undefined timespan (-1)
      if (long.TryParse(TelemetryService.GetTelemetryCache(), out long lastUnixTime))
      {
        span = (int)(currentUnixTime - lastUnixTime);
      }

      // Set the timestamp in the file to the current one for the next switching
      TelemetryService.SetTelemetryCache(currentUnixTime.ToString());

      TelemetryService.SendTelemetry(from.ServerName ?? "Unknown", GetCurrentServer().ServerName, span, CheckServerAvailability());
    }

    public static bool CheckServerAvailability()
    {
      // Try to build up a connection to the c interface of the osu server
      // to check if the bancho server is running
      try
      {
        using (TcpClient client = new TcpClient("c.ppy.sh", 80))
          return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
