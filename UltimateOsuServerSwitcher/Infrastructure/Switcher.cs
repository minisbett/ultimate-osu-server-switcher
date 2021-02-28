using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Infrastructure;
using UltimateOsuServerSwitcher.Model;
using UltimateOsuServerSwitcher.Utils;

namespace UltimateOsuServerSwitcher
{
  public static class Switcher
  {
    // The settings for the switcher
    private static Settings m_settings = new Settings(Paths.SettingsFile);

    // The settings for the saved osu accounts
    private static Settings m_accounts = new Settings(Paths.AccountsFile);

    // The settings for the switch history
    private static Settings m_history = new Settings(Paths.HistoryFile);

    /// <summary>
    /// The servers that were parsed from the web
    /// </summary>
    public static List<Server> Servers { get; set; } = new List<Server>();

    /// <summary>
    /// Switch the server
    /// </summary>
    /// <param name="server">The server to switch to</param>
    public static void SwitchServer(Server server)
    {
      // Remember the old server for telemetry
      Server from = GetCurrentServer();

      // Get the hosts file to edit it
      List<string> hosts = HostsUtils.GetHosts().ToList();

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
      bool successful = HostsUtils.SetHosts(hosts.ToArray());
      // check if writing to the hosts file was successful
      if (!successful)
        return;

      try
      {
        // Get rid of all uneccessary certificates
        CertificateUtils.UninstallAllCertificates(Servers);
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
          CertificateUtils.InstallCertificate(server);
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
        if (accounts.Any(x => x.ServerUID == server.UID))
        {
          // get the account that belongs to the server the user switched to
          Account account = accounts.First(x => x.ServerUID == server.UID);

          // Try to change the account details in the osu config file
          OsuConfigFile.SetAccount(account);
        }
      }

      // Add the switch to the history by getting the history, adding the element and re-saving it
      List<HistoryElement> history = JsonConvert.DeserializeObject<List<HistoryElement>>(m_history["history"]);
      history.Add(new HistoryElement(DateTime.Now, from.UID, server.UID));
      string json = JsonConvert.SerializeObject(history.ToArray());
      m_history["history"] = json;

      // Send telemetry if enabled
      if (m_settings["sendTelemetry"] == "true")
      {
        SendTelemetry(from, server);
      }
    }

    /// <summary>
    /// Validates a server and adds it to the switcher
    /// </summary>
    /// <param name="server">The server</param>
    /// <returns>true is successful, false if invalid</returns>
    public static async Task<bool> AddServer(Server server)
    {
      // Check if UID is 6 letters long (If not I made a mistake)
      if (server.UID.Length != 6)
        return false;

      // Check if the UID is really unique (I may accidentally put the same uid for two servers)
      if (Servers.Any(x => x.UID == server.UID))
        return false;

      // Check if everything is set
      if (server.ServerName == null ||
          server.IP == null ||
          server.IconUrl == null ||
          server.CertificateUrl == null ||
          server.DiscordUrl == null)
        return false;

      // Check if server name length is valid (between 3 and 24)
      if (server.ServerName.Replace(" ", "").Length < 3 || server.ServerName.Length > 24)
        return false;
      // Check if it neither start with a space, nor end
      if (server.ServerName.StartsWith(" "))
        return false;
      if (server.ServerName.EndsWith(" "))
        return false;
      // // Only a-zA-Z0-9 ! is allowed
      if (!Regex.Match(server.ServerName.Replace("!", "").Replace(" ", ""), "^\\w+$").Success)
        return false;
      // Double spaces are invalid because its hard to tell how many spaces there are
      // (One server could be named test 123 and the other test  123)
      if (server.ServerName.Replace("  ", "") != server.ServerName)
        return false;

      // Check if the server fakes a hardcoded server
      if (Server.StaticServers.Any(x => x.ServerName == server.ServerName))
        return false;

      // Check if the server ip is formatted correctly
      if (!Regex.IsMatch(server.IP, @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$"))
        return false;

      // Check if that server name already exists (if so, prioritize the first one)
      if (Servers.Any(x => x.ServerName.ToLower().Replace(" ", "") == server.ServerName.ToLower().Replace(" ", "")))
        return false;

      // Check if its a real discord invite url
      if (server.DiscordUrl != "" && !server.DiscordUrl.Replace("https", "").Replace("http", "").Replace("://", "").StartsWith("discord.gg"))
        return false;

      // Initialize variables like Certificate and Icon that are downloaded from their urls when
      // all checks are done (IconUrl, CertificateUrl)

      try
      {
        // Try to parse the certificate from the given url
        server.Certificate = await WebUtils.DownloadBytesAsync(server.CertificateUrl);
        server.CertificateThumbprint = new X509Certificate2(server.Certificate).Thumbprint;
      }
      catch // Cerfiticate url not valid or certificate type is not cer (base64 encoded)
      {
        return false;
      }

      // Check if icon is valid
      try
      {
        // Download the icon and check if its at least 256x256
        Image icon = await WebUtils.DownloadImageAsync(server.IconUrl);
        if (icon.Width < 256 || icon.Height < 256)
          return false;

        // Scale the image to 256x256
        server.Icon = new Bitmap(icon, new Size(256, 256));
      }
      catch // Image could not be downloaded or loaded
      {
        return false;
      }

      // Try to create .ico file for shortcut
      try
      {
        using (FileStream fs = System.IO.File.OpenWrite(Paths.IconCacheFolder + $@"\{server.UID}.ico"))
        using (MemoryStream ms = new MemoryStream((byte[])new ImageConverter().ConvertTo(server.Icon, typeof(byte[]))))
          IconUtils.ConvertToIcon(ms, fs, server.Icon.Width, true);
      }
      catch
      {
        return false;
      }

      // Add the server to the servers that were successfully parsed and checked
      Servers.Add(server);

      // Sort the servers by priority (first bancho, then featured, then normal, then localhost)
      Servers = Servers.OrderByDescending(x => x.Priority).ToList();

      return true;
    }

    /// <summary>
    /// Adds a server without doing various validation checks
    /// </summary>
    public static async Task<bool> AddServerNoCheck(Server server)
    {
      // Check if icon is valid
      try
      {
        // Download the icon and check if its at least 256x256
        Image icon = await WebUtils.DownloadImageAsync(server.IconUrl);
        if (icon.Width < 256 || icon.Height < 256)
          return false;

        // Scale the image to 256x256
        server.Icon = new Bitmap(icon, new Size(256, 256));
      }
      catch // Image could not be downloaded or loaded
      {
        return false;
      }

      // Try to create .ico file for shortcut
      try
      {
        using (FileStream fs = System.IO.File.OpenWrite(Paths.IconCacheFolder + $@"\{server.UID}.ico"))
        using (MemoryStream ms = new MemoryStream((byte[])new ImageConverter().ConvertTo(server.Icon, typeof(byte[]))))
          IconUtils.ConvertToIcon(ms, fs, server.Icon.Width, true);
      }
      catch
      {
        return false;
      }

      // Add the server to the servers that were successfully parsed and checked
      Servers.Add(server);

      // Sort the servers by priority (first bancho, then featured, then normal, then localhost)
      Servers = Servers.OrderByDescending(x => x.Priority).ToList();

      return true;
    }

    /// <summary>
    /// Returns the server the user is currently connected to
    /// </summary>
    public static Server GetCurrentServer()
    {
      // Get every line of the hosts file
      string[] hosts = HostsUtils.GetHosts();

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
