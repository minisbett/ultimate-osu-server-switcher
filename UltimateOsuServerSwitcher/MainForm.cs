using IWshRuntimeLibrary;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace UltimateOsuServerSwitcher
{
  public partial class MainForm : Form
  {
    #region Dll Imports
    // Import DLL files to make a borderless window moveable
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    #endregion

    #region Variable declaration

    // List with all servers fetched from the online data.
    private List<Server> m_servers = new List<Server>();

    // The server that is currently selected (not the one the user is connected to)
    private Server m_currentSelectedServer = null;

    // The index of the server above
    private int m_currentSelectedServerIndex => m_servers.IndexOf(m_currentSelectedServer);

    // Path to the icon cache
    string m_iconCacheFolder = Environment.GetEnvironmentVariable("localappdata") + @"\UltimateOsuServerSwitcher\IconCache";
   
    #endregion

    #region Winforms

    #region Program initialize

    public MainForm()
    {
      InitializeComponent();

      // Check if the icon cache folder exists
      if (!Directory.Exists(m_iconCacheFolder))
        Directory.CreateDirectory(m_iconCacheFolder);

      // Set the version label
      lblVersion.Text = "Version: " + VersionChecker.CurrentVersion;
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
      // Hide the connect button, show the loading button to make the user clear that the servers are being fetched
      btnConnect.Visible = false;
      pctrLoading.Visible = true;
      Application.DoEvents();

      await Task.Delay(1);

      // Check the state of the current version
      VersionState vs = await VersionChecker.GetCurrentState();
      if (vs == VersionState.BLACKLISTED)
      {
        // If the current version is blacklisted, prevent the user from using it.
        MessageBox.Show($"Your current version ({VersionChecker.CurrentVersion}) is blacklisted.\r\n\r\nThis can happen when the version contains security flaws or other things that could interrupt a good user experience.\r\n\r\nPlease download the newest version of the switcher from its website. (github.com/minisbett/ultimate-osu-server-switcher/releases).", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
        return;
      }
      else if (vs == VersionState.MAINTENANCE)
      {
        // If the switcher is in maintenance, also prevent the user from using it.
        MessageBox.Show("The switcher is currently hold in maintenance mode which means that the switcher is currently not available.\r\n\r\nJoin our discord server for more informations.\r\nThe discord server and be found on our github page.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
        return;
      }
      else if (vs == VersionState.OUTDATED)
      {
        // Show the user a message that a new version is available if the current switcher is outdated.
        MessageBox.Show($"Your switcher version ({VersionChecker.CurrentVersion}) is outdated.\r\nA newer version ({await VersionChecker.GetNewestVersion()}) is available.\r\n\r\nYou can download it from our github page.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }


      lblInfo.Text = "Fetching mirrors...";
      Application.DoEvents();

      // Load online data and verify servers
      List<Mirror> mirrors = await FetchMirrorsAsync();
      foreach (Mirror m in mirrors)
      {
        lblInfo.Text = $"Parsing mirror {m.Url}";
        Application.DoEvents();
        Server s = JsonConvert.DeserializeObject<Server>(await DownloadAsync(m.Url));
        s.IsFeatured = m.Featured;
        lblInfo.Text = $"Parsing mirror {m.Url} ({s.ServerName})";
        Application.DoEvents();

        // Check if everything is set
        if (s.ServerName == null || s.ServerIP == null || s.IconUrl == null || s.CertificateUrl == null || s.DiscordUrl == null)
          continue;

        // Check if server name is valid
        if (s.ServerName.Replace(" ", "").Length < 3 || s.ServerName.Length > 24)
          continue;
        if (s.ServerName.StartsWith(" "))
          continue;
        if (s.ServerName.EndsWith(" "))
          continue;
        if (!Regex.Match(s.ServerName.Replace("!", "").Replace(" ", ""), "^\\w+$").Success) // Only a-zA-Z0-9 !
          continue;
        if (s.ServerName.Replace("  ", "") != s.ServerName) // Double space is invalid
          continue;
        if (s.ServerName == Server.BanchoServer.ServerName || s.ServerName == Server.LocalhostServer.ServerName)
          continue;


        // Check if that server name already exists (if so, remove second occurence)
        if (m_servers.Any(x => x.ServerName.ToLower().Replace(" ", "") == s.ServerName.ToLower().Replace(" ", "")))
          continue;

        // Check discord url
        if (!s.DiscordUrl.Replace("https", "").Replace("http", "").Replace("://", "").StartsWith("discord.gg"))
          continue;

        try
        {
          // Try to parse the certificate from the given url
          s.Certificate = Encoding.UTF8.GetBytes(await DownloadAsync(s.CertificateUrl));
          s.CertificateThumbprint = new X509Certificate2(s.Certificate).Thumbprint;
        }
        catch // Cerfiticate url not valid or certificate is not cer
        {
          continue;
        }

        // Check if icon is valid
        try
        {
          // Download the icon and check if its 256x256
          Image icon = await DownloadImageAsync(s.IconUrl);
          if (icon.Width != 256 || icon.Height != 256)
            continue;
          s.Icon = icon;

          m_servers.Add(s);
        }
        catch // Image could not be downloaded or loaded
        {
          continue;
        }
      }

      // Load bancho and localhost
      try
      {
        // Download the icon and check if its 256x256
        Image icon = await DownloadImageAsync(Server.BanchoServer.IconUrl);
        if (icon.Width == 256 && icon.Height == 256)
        {
          Server s = Server.BanchoServer;
          s.Icon = icon;
          m_servers.Add(s);
        }
      }
      catch // Image could not be downloaded or loaded
      {

      }

      try
      {
        // Download the icon and check if its 256x256
        Image icon = await DownloadImageAsync(Server.LocalhostServer.IconUrl);
        if (icon.Width == 256 && icon.Height == 256)
        {
          Server s = Server.LocalhostServer;
          s.Icon = icon;
          m_servers.Add(s);
        }
      }
      catch // Image could not be downloaded or loaded
      {

      }

      // Sort the servers by priority (first bancho, then featured, then normal, then localhost)
      m_servers = m_servers.OrderByDescending(x => x.Priority).ToList();

      // Create .ico files for shortcuts
      foreach (Server server in m_servers)
      {
        using (FileStream fs = File.OpenWrite(m_iconCacheFolder + $@"\{server.ServerName}.ico"))
        using (MemoryStream ms = new MemoryStream((byte[])new ImageConverter().ConvertTo(server.Icon, typeof(byte[]))))
          ImagingHelper.ConvertToIcon(ms, fs, server.Icon.Width, true);
      }

      m_currentSelectedServer = GetCurrentServer();

      btnLeft.Visible = true;
      btnRight.Visible = true;

      UpdateUI();
    }

    #endregion

    #region Click events

    private void pctrServerIcon_Click(object sender, EventArgs e)
    {
      // If the server has a discord, open the link
      if (!string.IsNullOrEmpty(m_currentSelectedServer.DiscordUrl))
        Process.Start(m_currentSelectedServer.DiscordUrl);
    }

    private async void BtnConnect_Click(object sender, EventArgs e)
    {
      // Log the current server (before switching) for telemetry
      Server from = GetCurrentServer();

      // Show the connecting button and hide the connect button
      btnConnect.Visible = false;
      pctrConnecting.Visible = true;
      Application.DoEvents();

      // Get rid of all uneccessary certificates
      CertificateManager.UninstallAllCertificates(m_servers);

      // Clean up the hosts file
      List<string> hosts = HostsUtil.GetHosts().ToList();
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));
      HostsUtil.SetHosts(hosts.ToArray());

      // Edit the hosts file if the server is not bancho
      if (!m_currentSelectedServer.IsBancho)
      {

        string[] osu_domains = new string[]
        {
          "osu.ppy.sh",
          "c.ppy.sh",
          "c1.ppy.sh",
          "c2.ppy.sh",
          "c3.ppy.sh",
          "c4.ppy.sh",
          "c5.ppy.sh",
          "c6.ppy.sh",
          "ce.ppy.sh",
          "a.ppy.sh",
          "i.ppy.sh"
        };

        // Change the hosts file
        hosts = HostsUtil.GetHosts().ToList();
        foreach (string domain in osu_domains)
          hosts.Add(m_currentSelectedServer.ServerIP + " " + domain);
        HostsUtil.SetHosts(hosts.ToArray());

        // Install the certificate if needed
        if (m_currentSelectedServer.HasCertificate)
          CertificateManager.InstallCertificate(m_currentSelectedServer);
      }

      // Check if the server is available
      bool available = await CheckServerAvailability();
      if (!available)
      {
        MessageBox.Show("The connection test failed. Please restart the switcher and try again.\r\n\r\nIf it's still not working the server either didn't update their mirror yet or their server is currently not running (for example due to maintenance).", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

      if (chckbxSendTelemetry.Checked)
      {
        // Get the span between this and the last switching in unix milliseconds
        int span = -1;
        long currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        // If the unix time in the telemetry cache somehow cannot be converted
        // For example if the file was edited or the timestamp not set in the first place
        // Send an undefined timespan (-1)
        if(long.TryParse(TelemetryService.GetTelemetryCache(), out long lastUnixTime))
        {
          span = (int)(currentUnixTime - lastUnixTime);
        }

        // Set the timestamp in the file to the current one for the next switching
        TelemetryService.SetTelemetryCache(currentUnixTime.ToString());

        TelemetryService.SendTelemetry(from.ServerName ?? "Unknown", GetCurrentServer().ServerName, span, available);
      }

      pctrConnecting.Visible = false;

      UpdateUI();
    }

    private void btnLeft_Click(object sender, EventArgs e)
    {
      // Move the selection 1 to the left
      m_currentSelectedServer = m_servers[m_currentSelectedServerIndex - 1];
      UpdateUI();
    }

    private void btnRight_Click(object sender, EventArgs e)
    {
      // Move the selection 1 to the right
      m_currentSelectedServer = m_servers[m_currentSelectedServerIndex + 1];
      UpdateUI();
    }
    private void lnklblTelemetryLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      MessageBox.Show("We would appreciate to log some data to improve the user experience for everyone.\r\n\r\nThe following data will be transmitted to our server:\r\n\r\n- Server you are coming from and switching to\r\n- The time span between switching the server\r\n- Connectivity status of servers\r\n\r\n\r\nNote: No informations that would identify you will be transmitted. All informations are completely anonymous.\r\n\r\nYou can stop sending telemtry data by disabling that option at any time.", "UOSS Telemetry Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void pctrGithub_Click(object sender, EventArgs e)
    {
      Process.Start("https://github.com/minisbett/ultimate-osu-server-switcher");
    }

    private void pctrDiscord_Click(object sender, EventArgs e)
    {
      Process.Start("https://minisbett.github.io/ultimate-osu-server-switcher/discord.html");
    }

    #region Tab pages

    private void btnSettings_Click(object sender, EventArgs e)
    {
      btnSwitcher.BackColor = Color.FromArgb(10, 10, 10);
      btnSwitcher.FlatAppearance.BorderSize = 0;
      btnHelp.BackColor = Color.FromArgb(10, 10, 10);
      btnHelp.FlatAppearance.BorderSize = 0;

      btnSettings.BackColor = Color.FromArgb(31, 31, 31);
      btnSettings.FlatAppearance.BorderSize = 2;

      pnlSwitcher.Visible = false;
      pnlHelp.Visible = false;
      pnlSettings.Visible = true;
    }

    private void btnSwitcher_Click(object sender, EventArgs e)
    {
      btnSettings.BackColor = Color.FromArgb(10, 10, 10);
      btnSettings.FlatAppearance.BorderSize = 0;
      btnHelp.BackColor = Color.FromArgb(10, 10, 10);
      btnHelp.FlatAppearance.BorderSize = 0;

      btnSwitcher.BackColor = Color.FromArgb(31, 31, 31);
      btnSwitcher.FlatAppearance.BorderSize = 2;

      pnlSettings.Visible = false;
      pnlHelp.Visible = false;
      pnlSwitcher.Visible = true;
    }
    private void btnHelp_Click(object sender, EventArgs e)
    {
      btnSettings.BackColor = Color.FromArgb(10, 10, 10);
      btnSettings.FlatAppearance.BorderSize = 0;
      btnSwitcher.BackColor = Color.FromArgb(10, 10, 10);
      btnSwitcher.FlatAppearance.BorderSize = 0;

      btnHelp.BackColor = Color.FromArgb(31, 31, 31);
      btnHelp.FlatAppearance.BorderSize = 2;

      pnlSettings.Visible = false;
      pnlSwitcher.Visible = false;
      pnlHelp.Visible = true;
    }

    #endregion

    #endregion

    #region Other events

    private void BorderlessDragMouseDown(object sender, MouseEventArgs e)
    {
      // Make borderless window moveable
      if (e.Button == MouseButtons.Left)
      {
        ReleaseCapture();
        SendMessage(Handle, 0xA1, 0x2, 0);
      }
    }

    #endregion

    #endregion

    #region Download and Server Util

    #region Web stuff

    private WebClient m_client = new WebClient();

#pragma warning disable CS1998
    private async Task<Image> DownloadImageAsync(string url)
    {
      using (Stream stream = m_client.OpenRead(url))
        return Image.FromStream(stream);
    }

    private async Task<string> DownloadAsync(string url)
    {
      var result = await m_client.DownloadStringTaskAsync(new Uri(url));
      return result;
    }

    private async Task<List<Mirror>> FetchMirrorsAsync()
    {
      string raw = await DownloadAsync("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/mirrors.json");
      return JsonConvert.DeserializeObject<List<Mirror>>(raw);
    }

    private async Task<bool> CheckServerAvailability()
    {
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

    #endregion

    #region Server stuff

    private Server GetCurrentServer()
    {
      string[] hosts = HostsUtil.GetHosts();

      for (int i = 0; i < hosts.Length; i++)
        if (hosts[i].Contains(".ppy.sh"))
        {
          string ip = hosts[i].Replace("\t", " ").Split(' ')[0];
          return m_servers.FirstOrDefault(x => x.ServerIP == ip) ?? Server.UnidentifiedServer;
        }

      // Because after downloading icon etc the m_servers bancho is not equal to Server.BanchoServer
      return m_servers.First(x => x.IsBancho);
    }

    private void UpdateUI()
    {
      // Show the image of the selected server if the server was identified
      if (!m_currentSelectedServer.IsUnidentified)
        pctrCurrentSelectedServer.Image = m_currentSelectedServer.Icon;

      // Show/Hide the connect/already connected button depending on if you are currently connected to the selected server
      Server s = GetCurrentServer();
      btnConnect.Visible = m_currentSelectedServer != s;
      pctrAlreadyConnected.Visible = m_currentSelectedServer == s;
      // Show the image of the connected server if the server was identified
      if (!s.IsUnidentified)
        pctrCurrentServer.Image = s.Icon;

      btnRight.Visible = m_currentSelectedServerIndex + 1 != m_servers.Count;
      btnLeft.Visible = m_currentSelectedServerIndex != 0;

      lblInfo.Text = m_currentSelectedServer.ServerName;
      lblInfo.ForeColor = m_currentSelectedServer.IsFeatured ? Color.Orange : Color.White;
      pctrVerified.Visible = m_currentSelectedServer.IsFeatured;

      Graphics g = CreateGraphics();
      pctrVerified.Location = new Point(pnlSwitcher.Width / 2 + (int)g.MeasureString(m_currentSelectedServer.ServerName, lblInfo.Font).Width / 2, pctrVerified.Location.Y);
    }

    #endregion

    #endregion
  }
}