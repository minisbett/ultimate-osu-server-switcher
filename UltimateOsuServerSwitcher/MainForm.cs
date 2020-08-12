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
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    #endregion

    #region Variable declaration
    // List with all servers fetched from the online data.
    private List<Server> m_servers = new List<Server>();

    private Server m_currentSelectedServer = null;

    private int m_currentSelectedServerIndex => m_servers.IndexOf(m_currentSelectedServer);

    // Path to the icon cache
    string m_iconCacheFolder = Environment.GetEnvironmentVariable("localappdata") + @"\UltimateOsuServerSwitcher\IconCache";
    #endregion

    #region Winforms

    #region Program initialize

    public MainForm()
    {
      InitializeComponent();

      if (!Directory.Exists(m_iconCacheFolder))
        Directory.CreateDirectory(m_iconCacheFolder);
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
      btnConnect.Visible = false;
      pctrLoading.Visible = true;
      Application.DoEvents();

      await Task.Delay(1);


      VersionState vs = await VersionChecker.GetCurrentState();
      if (vs == VersionState.BLACKLISTED)
      {
        MessageBox.Show($"Your current version ({VersionChecker.CurrentVersion}) is blacklisted.\r\n\r\nThis can happen when the version contains security flaws or other things that could interrupt a good user experience.\r\n\r\nPlease download the newest version of the switcher from its website. (github.com/minisbett/ultimate-osu-server-switcher/releases).", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
        return;
      }
      else if (vs == VersionState.MAINTENANCE)
      {
        MessageBox.Show("The switcher is currently hold in maintenance mode which means that the switcher is currently not available.\r\n\r\nJoin our discord server for more informations.\r\nThe discord server and be found on our github page.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
        return;
      }
      else if (vs == VersionState.OUTDATED)
      {
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
          s.Certificate = Encoding.UTF8.GetBytes(await DownloadAsync(s.CertificateUrl));
          s.CertificateThumbprint = new X509Certificate2(s.Certificate).Thumbprint;
        }
        catch // Cerfiticate url not valid or certificate is not cer
        {
          continue;
        }

        // Check if icon is valid and exists
        if (!File.Exists(m_iconCacheFolder + $@"\{s.ServerName}.png"))
        {
          try
          {
            Image icon = await DownloadImageAsync(s.IconUrl);
            if (icon.Width != 256 || icon.Height != 256)
              continue;
            icon.Save(m_iconCacheFolder + $@"\{s.ServerName}.png");
            icon.Dispose();
          }
          catch // Image could not be downloaded or loaded
          {
            continue;
          }
        }

        m_servers.Add(s);
      }

      // Load bancho and localhost
      try
      {
        Image icon = await DownloadImageAsync(Server.BanchoServer.IconUrl);
        if (icon.Width == 256 && icon.Height == 256)
        {
          icon.Save(m_iconCacheFolder + $@"\{Server.BanchoServer.ServerName}.png");
          icon.Dispose();

          m_servers.Add(Server.BanchoServer);
        }
      }
      catch // Image could not be downloaded or loaded
      {

      }

      try
      {
        Image icon = await DownloadImageAsync(Server.LocalhostServer.IconUrl);
        if (icon.Width == 256 && icon.Height == 256)
        {
          icon.Save(m_iconCacheFolder + $@"\{Server.LocalhostServer.ServerName}.png");
          icon.Dispose();

          m_servers.Add(Server.LocalhostServer);
        }
      }
      catch // Image could not be downloaded or loaded
      {

      }

      // Sort the servers by priority (first bancho, then featured, then normal, then localhost)
      m_servers = m_servers.OrderByDescending(x => x.Priority).ToList();


      // Check if servers which icon is in cache no longer exist
      foreach (string icon in Directory.GetFiles(m_iconCacheFolder, "*.png", SearchOption.TopDirectoryOnly))
      {
        string name = Path.GetFileNameWithoutExtension(icon);
        if (!m_servers.Any(x => x.ServerName == name))
          File.Delete(icon);
      }

      // Load icons from cache
      foreach (Server server in m_servers)
        if (File.Exists(m_iconCacheFolder + $@"\{server.ServerName}.png"))
          using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(m_iconCacheFolder + $@"\{server.ServerName}.png")))
            server.Icon = Image.FromStream(ms);

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
      if (!string.IsNullOrEmpty(m_currentSelectedServer.DiscordUrl))
        Process.Start(m_currentSelectedServer.DiscordUrl);
    }

    private async void BtnConnect_Click(object sender, EventArgs e)
    {
      btnConnect.Visible = false;
      pctrConnecting.Visible = true;
      Application.DoEvents();
      CertificateManager.UninstallAllCertificates(m_servers);
      List<string> hosts = HostsUtil.GetHosts().ToList();
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));
      HostsUtil.SetHosts(hosts.ToArray());

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

        hosts = HostsUtil.GetHosts().ToList();
        foreach (string domain in osu_domains)
          hosts.Add(m_currentSelectedServer.ServerIP + " " + domain);
        HostsUtil.SetHosts(hosts.ToArray());

        if (!m_currentSelectedServer.IsLocalhost)
          CertificateManager.InstallCertificate(m_currentSelectedServer);

        bool available = await CheckServerAvailability();
        if (!available)
        {
          MessageBox.Show("The connection test failed. Please restart the switcher and try again.\r\n\r\nIf it's still not working the server either didn't update their mirror yet or their server is currently not running (for example due to maintenance).", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }

      pctrConnecting.Visible = false;

      UpdateUI();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void btnLeft_Click(object sender, EventArgs e)
    {
      m_currentSelectedServer = m_servers[m_currentSelectedServerIndex - 1];
      UpdateUI();
    }

    private void btnRight_Click(object sender, EventArgs e)
    {
      m_currentSelectedServer = m_servers[m_currentSelectedServerIndex + 1];
      UpdateUI();
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
      if (!m_currentSelectedServer.IsUnidentified)
        pctrCurrentSelectedServer.Image = m_currentSelectedServer.Icon;
      Server s = GetCurrentServer();
      btnConnect.Visible = m_currentSelectedServer != s;
      pctrAlreadyConnected.Visible = m_currentSelectedServer == s;
      if (!s.IsUnidentified)
        pctrCurrentServer.Image = s.Icon;

      btnRight.Visible = m_currentSelectedServerIndex + 1 != m_servers.Count;
      btnLeft.Visible = m_currentSelectedServerIndex != 0;

      lblInfo.Text = m_currentSelectedServer.ServerName;
      lblInfo.ForeColor = m_currentSelectedServer.IsFeatured ? Color.Orange : Color.White;
    }

    #endregion

    #endregion
  }
}