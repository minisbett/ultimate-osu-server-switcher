using IWshRuntimeLibrary;
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
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Model;
using UltimateOsuServerSwitcher.Utils;

namespace UltimateOsuServerSwitcher
{
  public static class Switcher
  {
    // The settings for the switcher
    private static Settings m_settings => new Settings(Paths.SettingsFile);

    // The settings instance for the saved osu accounts
    private static Settings m_accounts => new Settings(Paths.AccountsFile);

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

      // Get the hosts file to edit it
      List<string> hosts = HostsUtil.GetHosts().ToList();

      // Check if reading the hosts file was successful
      if (hosts == null)
        return;
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));

      // Edit the hosts file if the server has a custom ip
      if (server.HasIP)
      {
        // Change the hosts file by adding the server's ip and the osu domain
        foreach (string domain in Variables.OsuDomains)
          hosts.Add(server.IP + " " + domain);
      }

      // Apply the changes to the hosts file
      bool successful = HostsUtil.SetHosts(hosts.ToArray());
      // check if writing to the hosts file was successful
      if (!successful)
        return;

      try
      {
        // Get rid of all uneccessary certificates
        CertificateManager.UninstallAllCertificates(Servers);
      }
      catch (Exception ex)
      {
        // Show error
        MessageBox.Show($"Error whilst trying to uninstall certificates.\n\n{ex.Message}\n\nPlease try again.\nIf the issue persists, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      // Install the certificate if needed (not needed for bancho and localhost)
      if (server.HasCertificate)
        try
        {
          CertificateManager.InstallCertificate(server);
        }
        catch (Exception ex)
        {
          // Show error
          MessageBox.Show($"Error whilst trying to install certificates.\n\n{ex.Message}\n\nPlease switch back to bancho to try again. Otherwise you will experience desynchronizations between your hosts file and your installed certificates.\n\nIf the issue persists, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

      // switch the account in the osu config file if that option is enabled
      if (m_settings["switchAccount"] == "true")
      {
        // Only switch when an account is saved for that server
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(m_accounts["accounts"]);
        if(accounts.Any(x => x.ServerUID == server.UID))
        {
          // get the account that belongs to the server the user switched to
          Account account = accounts.First(x => x.ServerUID == server.UID);

          // Try to change the account details in the osu config file
          OsuConfigFileUtils.SetAccount(account);
        }
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

      TelemetryService.SendTelemetry(from.ServerName ?? "Unknown", to.ServerName ?? "Unknown", TelemetryUtils.MillisecondsToString(span), CheckServerAvailability());
    }

    #region QuickSwitch

    /// <summary>
    /// Creates a QuickSwitch shortcut at the given location for the given server
    /// </summary>
    /// <param name="file">The path to the .lnk file</param>
    /// <param name="server">The server the shortcuts lets you switch to</param>
    public static void CreateShortcut(string file, Server server)
    {
      WshShell shell = new WshShell();
      IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(file);
      shortcut.Description = $"Switch to {server.ServerName}";
      if (server.Icon != null)
        shortcut.IconLocation = Paths.IconCacheFolder + $@"\{server.UID}.ico";
      shortcut.TargetPath = "cmd";
      shortcut.Arguments = $"/c call \"{Application.ExecutablePath}\" \"{server.UID}\"";
      shortcut.Save();
    }

    #endregion

    /// <summary>
    /// Checks if the currently redirected server is available (c.ppy.sh:80 ping)
    /// </summary>
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
