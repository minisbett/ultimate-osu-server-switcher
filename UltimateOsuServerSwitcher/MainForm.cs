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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace UltimateOsuServerSwitcher
{
  public partial class MainForm : MetroForm
  {
    #region Variable declaration
    // List with all servers fetched from the online data.
    private List<Server> m_servers = new List<Server>();

    // Determines if osu is currently running
    bool m_osuRunning = false;

    // Path to the icon cache
    string m_iconCacheFolder = Environment.GetEnvironmentVariable("localappdata") + @"\UltimateOsuServerSwitcher\IconCache";
    #endregion

    #region Winforms

    #region Program initialize

    public MainForm()
    {
      InitializeComponent();

      //Initialize Theming
      styleManager.Theme = Properties.Settings.Default.darkMode ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
      tgglDarkTheme.Checked = Properties.Settings.Default.darkMode;
      tbcntrlMain.SelectedIndex = 0;

      if (!Directory.Exists(m_iconCacheFolder))
        Directory.CreateDirectory(m_iconCacheFolder);
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
      await Task.Delay(10);

      // Load online data
      var data = await FetchOnlineDataAsync();
      lblAbout.Text = data.AboutText;

      // Order the list to get the featured servers to the top, putting bancho back at index 0
      m_servers.Add(Server.BanchoServer);
      m_servers.AddRange(data.Servers.Where(x => x.IsFeatured));
      m_servers.AddRange(data.Servers.Where(x => !x.IsFeatured));
      m_servers.Add(Server.LocalhostServer);

      lblCurrentServer.Text = "Updating icon cache...";
      Application.DoEvents();

      // Check if servers which icon is in cache no longer exist
      foreach (string icon in Directory.GetFiles(m_iconCacheFolder, "*.png", SearchOption.TopDirectoryOnly))
      {
        string name = Path.GetFileNameWithoutExtension(icon);
        if (!m_servers.Any(x => x.ServerName == name))
          File.Delete(icon);
      }

      // Check for servers which icons arent in cache yet
      foreach (Server server in m_servers)
      {
        if (!File.Exists(m_iconCacheFolder + $@"\{server.ServerName}.png") && !string.IsNullOrEmpty(server.IconUrl))
        {
          lblCurrentServer.Text = $"Downloading icon of {server.ServerName}...";
          Application.DoEvents();
          Image icon = await DownloadImageAsync(server.IconUrl);
          icon.Save(m_iconCacheFolder + $@"\{server.ServerName}.png");
          icon.Dispose();
        }
      }

      // Load icons from cache
      foreach (Server server in m_servers)
        if (File.Exists(m_iconCacheFolder + $@"\{server.ServerName}.png"))
          using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(m_iconCacheFolder + $@"\{server.ServerName}.png")))
            server.Icon = Image.FromStream(ms);

      // Create .ico files for shortcuts
      foreach (Server server in m_servers)
      {
        if (server.Icon != null)
          using (FileStream fs = File.OpenWrite(m_iconCacheFolder + $@"\{server.ServerName}.ico"))
          using (MemoryStream ms = new MemoryStream((byte[])new ImageConverter().ConvertTo(server.Icon, typeof(byte[]))))
            ImagingHelper.ConvertToIcon(ms, fs, 48, true);
      }

      //Adds all servers to combo box
      foreach (Server server in m_servers)
      {
        string name = server.ServerName;
        if (server.IsFeatured)
          name = "⭐" + name + "⭐";
        cmbbxServer.Items.Add(name);
      }

      bool newVersionAvailable = await VersionChecker.NewVersionAvailable();
      btnUpdateAvailable.Visible = newVersionAvailable;

      lblClearIconCache.Enabled = true;
      lblCreateShortcut.Show();

      UpdateServerUI();

      osuWatcher.Start();
    }

    #endregion

    #region Click events

    private void LblGithub_Click(object sender, EventArgs e) =>
      Process.Start("https://www.github.com/minisbett/ultimate-osu-server-switcher");

    private void lblDiscord_Click(object sender, EventArgs e) =>
      Process.Start("https://minisbett.github.io/ultimate-osu-server-switcher/discord.html");

    private void btnUpdateAvailable_Click(object sender, EventArgs e) =>
      Process.Start("https://www.github.com/MinisBett/ultimate-osu-server-switcher/releases/latest");

    private void lblCreateShortcut_Click(object sender, EventArgs e)
    {
      SaveFileDialog sfd = new SaveFileDialog()
      {
        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
        Filter = "Shortcut|*.lnk",
        FileName = GetSelectedServer().ServerName
      };

      if (sfd.ShowDialog() == DialogResult.OK)
        QuickSwitch.CreateShortcut(GetSelectedServer(), sfd.FileName);
    }

    private async void lblClearIconCache_Click(object sender, EventArgs e)
    {
      cmbbxServer.Enabled = false;
      btnConnect.Enabled = false;
      lblClearIconCache.Enabled = false;
      lblCreateShortcut.Hide();
      tbcntrlMain.SelectedIndex = 0;
      lblCurrentServer.Text = "Clearing cache...";
      Application.DoEvents();
      Directory.Delete(m_iconCacheFolder, true);
      Directory.CreateDirectory(m_iconCacheFolder);
      m_servers.ForEach(x => x.Icon = null);

      // Check for servers which icons arent in cache yet
      foreach (Server server in m_servers)
      {
        lblCurrentServer.Text = $"Downloading icon of {server.ServerName}...";
        Application.DoEvents();
        Image icon = await DownloadImageAsync(server.IconUrl);
        icon.Save(m_iconCacheFolder + $@"\{server.ServerName}.png");
        icon.Dispose();
      }

      // Load icons from cache
      foreach (Server server in m_servers)
        if (File.Exists(m_iconCacheFolder + $@"\{server.ServerName}.png"))
        {
          using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(m_iconCacheFolder + $@"\{server.ServerName}.png")))
            server.Icon = Image.FromStream(ms);
        }

      // Create .ico files for shortcuts
      foreach (Server server in m_servers)
      {
        using (FileStream fs = File.OpenWrite(m_iconCacheFolder + $@"\{server.ServerName}.ico"))
        using (MemoryStream ms = new MemoryStream((byte[])new ImageConverter().ConvertTo(server.Icon, typeof(byte[]))))
          ImagingHelper.ConvertToIcon(ms, fs, 48, true);
      }

      cmbbxServer.Enabled = true;
      btnConnect.Enabled = true;
      lblClearIconCache.Enabled = true;
      lblCreateShortcut.Show();
      UpdateServerUI();
    }

    private void pctrbxServerIcon_Click(object sender, EventArgs e)
    {
      Server selectedServer = GetSelectedServer();
      Process.Start(selectedServer.WebsiteUrl);
    }

    private void BtnConnect_Click(object sender, EventArgs e)
    {
      bool osuWasRunning = m_osuRunning;
      if (m_osuRunning)
      {
        Process p = Process.Start(new ProcessStartInfo()
        {
          FileName = "taskkill",
          Arguments = "/IM osu!.exe",
          CreateNoWindow = true,
          UseShellExecute = false
        });

        p.WaitForExit();
      }
      lblCurrentServer.Text = "Connecting...";
      Application.DoEvents();
      Server selectedServer = GetSelectedServer();

      CertificateManager.UninstallAllCertificates(m_servers);
      List<string> hosts = HostsUtil.GetHosts().ToList();
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));
      HostsUtil.SetHosts(hosts.ToArray());

      if (!selectedServer.IsBancho)
      {
        string[] osu_domains = new string[]
        {
          //"delta.ppy.sh",
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
          //"s.ppy.sh",
          "i.ppy.sh",
          //"bm6.ppy.sh",
        };

        hosts = HostsUtil.GetHosts().ToList();
        foreach (string domain in osu_domains)
          hosts.Add(selectedServer.ServerIP + " " + domain);
        HostsUtil.SetHosts(hosts.ToArray());

        if (!selectedServer.IsLocalhost)
          CertificateManager.InstallCertificate(selectedServer);
      }

      if (osuWasRunning)
        Process.Start(QuickSwitch.GetOsuExecutablePath());

      UpdateServerUI();
    }

    #endregion

    #region Other events

    private void TgglDarkTheme_CheckedChanged(object sender, EventArgs e)
    {
      styleManager.Theme = tgglDarkTheme.Checked ? MetroThemeStyle.Dark : MetroThemeStyle.Light;

      Properties.Settings.Default.darkMode = tgglDarkTheme.Checked;
      Properties.Settings.Default.Save();
    }

    private void osuWatcher_Tick(object sender, EventArgs e)
    {
      Server selected = GetSelectedServer();
      if (selected != null)
      {
        m_osuRunning = Process.GetProcessesByName("osu!").Any();
        lblOsuRunning.Visible = m_osuRunning;
      }
    }

    private void CmbbxServer_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnConnect.Enabled = GetCurrentServer().ServerName != GetSelectedServer().ServerName;
      SetServerIcon();
    }
    #endregion

    #endregion

    #region Download and Server Util

    #region Download from web

    private WebClient m_client = new WebClient();

#pragma warning disable CS1998
    private async Task<Image> DownloadImageAsync(string url)
    {
      try
      {
        using (Stream stream = m_client.OpenRead(url))
          return Image.FromStream(stream);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error ocurred.\r\n\r\n" + ex.Message + "\r\n\r\nPlease try again or create an issue on github.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
      }

      return null;
    }

    private async Task<string> DownloadAsync(string url)
    {
      var result = await m_client.DownloadStringTaskAsync(new Uri(url));
      return result;
    }

    private async Task<Data> FetchOnlineDataAsync()
    {
      string raw = await DownloadAsync("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/data/data.json");
      return JsonConvert.DeserializeObject<Data>(raw);
    }

    #endregion

    #region Server stuff

    private void SetServerIcon()
    {
      Server selectedServer = GetSelectedServer();
      pctrbxServerIcon.Visible = selectedServer.Icon != null;
      if (selectedServer.Icon != null)
        pctrbxServerIcon.Image = selectedServer.Icon;
    }

    private Server GetCurrentServer()
    {
      string[] hosts = HostsUtil.GetHosts();

      for (int i = 0; i < hosts.Length; i++)
        if (hosts[i].Contains(".ppy.sh"))
        {
          string ip = hosts[i].Replace("\t", " ").Split(' ')[0];
          return m_servers.FirstOrDefault(x => x.ServerIP == ip) ?? Server.UnidentifiedServer;
        }

      return Server.BanchoServer;
    }

    private Server GetSelectedServer()
    {
      if (cmbbxServer.SelectedItem == null)
        return null;

      return m_servers.First(x => x.ServerName == cmbbxServer.SelectedItem.ToString().Replace("⭐", ""));
    }

    private void UpdateServerUI()
    {
      Server current = GetCurrentServer();

      lblCreateShortcut.Visible = !current.IsUnidentified;

      if (current.IsUnidentified)
        lblCurrentServer.Text = $"You are connected to a yet unknown server!";
      else if (current.IsLocalhost)
      {
        cmbbxServer.SelectedIndex = cmbbxServer.Items.Count - 1;
        lblCurrentServer.Text = "You are connected to localhost";
        CmbbxServer_SelectedIndexChanged(cmbbxServer, EventArgs.Empty);
      }
      else
      {
        cmbbxServer.SelectedIndex = m_servers.IndexOf(m_servers.First(x => x.ServerName == current.ServerName));
        CmbbxServer_SelectedIndexChanged(cmbbxServer, EventArgs.Empty);
        if (current.IsFeatured)
          lblCurrentServer.Text = $"You are connected to ⭐{current.ServerName}⭐";
        else
          lblCurrentServer.Text = $"You are connected to {current.ServerName}";
        if (!current.IsBancho)
        {
          using (var client = new WebClient())
          {
            try
            {
              Stopwatch sw = Stopwatch.StartNew();
              client.DownloadString("http://" + GetSelectedServer().ServerIP);
              sw.Stop();
              serverPingText.Text = "Online - " + sw.Elapsed.Milliseconds + "ms";
            }
            catch
            {
              serverPingText.Text = "Offline";
            }
          }
        }
        else
          serverPingText.Text = "Online";
      }
    }

    #endregion

    #endregion
  }
}