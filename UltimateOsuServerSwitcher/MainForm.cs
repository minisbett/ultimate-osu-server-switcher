using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.IO.Ports;

namespace UltimateOsuServerSwitcher
{
  public partial class MainForm : MetroForm
  {

    #region Variable declaration
    // List with all servers fetched from the online data, having bancho as the initial value.
    private List<Server> m_servers = new List<Server> { Server.BanchoServer };

    // Determines if osu is currently running
    bool m_osuRunning = false;
    #endregion

    #region Winforms

    #region Program initialize

    public MainForm()
    {
      InitializeComponent();

      //Initialize Theming
      styleManager.Theme = Properties.Settings.Default.darkMode ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
      tgglDarkTheme.Checked = Properties.Settings.Default.darkMode;
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
      await Task.Delay(10);

      // Load online data
      var data = await FetchOnlineDataAsync();
      m_servers.AddRange(data.Servers);
      lblAbout.Text = data.AboutText;

      // Retrieve images
      WebClient client = new WebClient();
      foreach (Server server in m_servers.Where(x => !string.IsNullOrEmpty(x.IconUrl)))
      {
        try
        {
          using (Stream stream = client.OpenRead(server.IconUrl))
            server.Icon = Image.FromStream(stream);
        }
        catch { }
      }

      //Adds all servers to combo box
      cmbbxServer.Items.AddRange(m_servers.Select(x => x.ServerName).ToArray());

      // Get current server and set ui text
      Server current = GetCurrentServer();
      if (current.IsUnidentified)
      {
        lblCurrentServer.Text = $"You are connected to a yet unknown server!";
        cmbbxServer.SelectedIndex = 0;
        btnConnect.Enabled = true;
      }
      else
      {
        cmbbxServer.SelectedIndex = m_servers.IndexOf(m_servers.First(x => x.ServerName == current.ServerName));
        lblCurrentServer.Text = $"You are connected to {current.ServerName}";
      }

      osuWatcher.Start();
    }

    #endregion

    #region Click events

    private void LblGithub_Click(object sender, EventArgs e) =>
      Process.Start("http://www.github.com/minisbett/ultimate-osu-server-switcher");

    private void pctrbxServerIcon_Click(object sender, EventArgs e)
    {
      Server selectedServer = m_servers.First(x => x.ServerName == cmbbxServer.SelectedItem.ToString());
      Process.Start(selectedServer.WebsiteUrl);
    }

    private void BtnConnect_Click(object sender, EventArgs e)
    {
      lblCurrentServer.Text = "Connecting...";
      Application.DoEvents();
      Server selectedServer = m_servers.First(x => x.ServerName == cmbbxServer.SelectedItem.ToString());

      CertificateManager.UninstallAllCertificates(m_servers);
      List<string> hosts = HostsUtil.GetHosts().ToList();
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));
      HostsUtil.SetHosts(hosts.ToArray());

      if (selectedServer.IsBancho)
      {
        string[] _hosts = HostsUtil.GetHosts();
        _hosts = _hosts.Where(x => !x.Contains(".ppy.sh")).ToArray();
        HostsUtil.SetHosts(_hosts);
      }
      else
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

        CertificateManager.InstallCertificate(selectedServer);
      }

      Server current = GetCurrentServer();
      if (current.IsUnidentified)
        lblCurrentServer.Text = $"You are connected to a yet unknown server!";
      else
      {
        cmbbxServer.SelectedIndex = m_servers.IndexOf(m_servers.First(x => x.ServerName == current.ServerName));
        CmbbxServer_SelectedIndexChanged(cmbbxServer, EventArgs.Empty);
        lblCurrentServer.Text = $"You are connected to {current.ServerName}";
      }
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
      m_osuRunning = Process.GetProcessesByName("osu!").Any();
      lblOsuRunning.Visible = m_osuRunning;
      btnConnect.Enabled = !m_osuRunning && GetCurrentServer().ServerName != cmbbxServer.SelectedItem.ToString();
    }

    private void CmbbxServer_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnConnect.Enabled = !m_osuRunning && GetCurrentServer().ServerName != cmbbxServer.SelectedItem.ToString();
      SetServerIcon();
    }
    #endregion

    #endregion

    #region Download and Server Util

    #region Download from web

    private WebClient m_client = new WebClient();

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
      Server selectedServer = m_servers.First(x => x.ServerName == cmbbxServer.SelectedItem.ToString());
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

    #endregion

    #endregion

  }
}