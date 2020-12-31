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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  public partial class MainForm : Form
  {
    // The server that is currently selected (not the one the user is connected to)
    private Server m_currentSelectedServer = null;

    // The index of the server above
    private int m_currentSelectedServerIndex => Switcher.Servers.IndexOf(m_currentSelectedServer);

    // Temporary variable to force-close the switcher if minimize to system tray is enabled
    private bool m_forceclose = false;

    // The settings for the switcher
    private Settings m_settings => new Settings(Paths.SettingsFile);

    #region Program initialize

    public MainForm()
    {
      InitializeComponent();

      // Check if the icon cache folder exists
      if (!Directory.Exists(Paths.IconCacheFolder))
        Directory.CreateDirectory(Paths.IconCacheFolder);

      // Set the version label to the current version provided by the version checker
      lblVersion.Text = "Version: " + VersionChecker.CurrentVersion;

      // Initialize all settings with their default values
      m_settings.SetDefaultValue("minimizeToTray", "true");
      m_settings.SetDefaultValue("sendTelemetry", "false");
      m_settings.SetDefaultValue("closeOsuBeforeSwitching", "true");
      m_settings.SetDefaultValue("reopenOsuAfterSwitching", "true");
      m_settings.SetDefaultValue("openOsuAfterQuickSwitching", "true");
      m_settings.SetDefaultValue("useDiscordRichPresence", "true");

      // Set the settings controls to their state from the settings file
      chckbxMinimize.Checked = m_settings["minimizeToTray"] == "true";
      chckbxSendTelemetry.Checked = m_settings["sendTelemetry"] == "true";
      chckbxCloseBeforeSwitching.Checked = m_settings["closeOsuBeforeSwitching"] == "true";
      chckbxReopenAfterSwitching.Checked = m_settings["reopenOsuAfterSwitching"] == "true";
      chckbxOpenAfterQuickSwitching.Checked = m_settings["openOsuAfterQuickSwitching"] == "true";
      chckbxUseDiscordRichPresence.Checked = m_settings["useDiscordRichPresence"] == "true";

      //
      // Telemetry disabled because not finished yet
      //
      m_settings["sendTelemetry"] = "false";
      chckbxSendTelemetry.Checked = false;
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

      // Try to load online data and verify servers
      List<Mirror> mirrors = null;
      try
      {
        // Download the mirror data from github and deserialize it into a mirror list
        mirrors = JsonConvert.DeserializeObject<List<Mirror>>(await WebHelper.DownloadStringAsync(Urls.Mirrors));
      }
      catch
      {
        // If it was not successful, github may cannot currently be reached or I made a mistake in the json data.
        MessageBox.Show("Error whilst parsing the server mirrors from GitHub!\r\nPlease make sure you can connect to www.github.com in your browser.\r\n\r\nIf this issue persists, please visit our discord. You can find the invite link on our GitHub Page (minisbett/ultimate-osu-server-switcher)", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }

      imgLoadingBar.Maximum = mirrors.Count;
      // Try to load all servers
      List<Server> servers = new List<Server>();
      foreach (Mirror mirror in mirrors)
      {
        lblInfo.Text = $"Parsing mirror {mirror.Url}";
        Application.DoEvents();
        Server server = null;
        // Try to serialize the mirror into a server
        try
        {
          // Download the data from the mirror and try to parse it into a server object.
          server = JsonConvert.DeserializeObject<Server>(await WebHelper.DownloadStringAsync(mirror.Url));
        }
        catch
        {
          // If the cast was not successful (invalid json) or the mirror could not be reached, skip the mirror
          continue;
        }
        // Forward mirror variables to the server
        server.IsFeatured = mirror.Featured;
        server.UID = mirror.UID;
        lblInfo.Text = $"Parsing mirror {mirror.Url} ({server.ServerName})";
        Application.DoEvents();

        // Check if UID is 6 letters long (If not I made a mistake)
        if (server.UID.Length != 6)
          continue;

        // Check if the UID is really unique (I may accidentally put the same uid for two servers)
        if (servers.Any(x => x.UID == server.UID))
          continue;

        // Check if everything is set
        if (server.ServerName == null ||
            server.IP == null ||
            server.IconUrl == null ||
            server.CertificateUrl == null ||
            server.DiscordUrl == null)
          continue;

        // Check if server name length is valid (between 3 and 24)
        if (server.ServerName.Replace(" ", "").Length < 3 || server.ServerName.Length > 24)
          continue;
        // Check if it neither start with a space, nor end
        if (server.ServerName.StartsWith(" "))
          continue;
        if (server.ServerName.EndsWith(" "))
          continue;
        // // Only a-zA-Z0-9 ! is allowed
        if (!Regex.Match(server.ServerName.Replace("!", "").Replace(" ", ""), "^\\w+$").Success)
          continue;
        // Double spaces are invalid because its hard to tell how many spaces there are
        // (One server could be named test 123 and the other test  123)
        if (server.ServerName.Replace("  ", "") != server.ServerName)
          continue;
        // Check if the server fakes a hardcoded server
        if (Server.StaticServers.Any(x => x.ServerName == server.ServerName))
          continue;

        // Check if the server ip is formatted correctly
        if (!Regex.IsMatch(server.IP, @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$"))
          continue;

        // Check if that server name already exists (if so, prioritize the first one)
        if (servers.Any(x => x.ServerName.ToLower().Replace(" ", "") == server.ServerName.ToLower().Replace(" ", "")))
          continue;

        // Check if its a real discord invite url
        if (!server.DiscordUrl.Replace("https", "").Replace("http", "").Replace("://", "").StartsWith("discord.gg"))
          continue;

        // Initialize variables like Certificate and Icon that are downloaded from their urls when
        // all checks are done (IconUrl, CertificateUrl)

        try
        {
          // Try to parse the certificate from the given url
          server.Certificate = await WebHelper.DownloadBytesAsync(server.CertificateUrl);
          server.CertificateThumbprint = new X509Certificate2(server.Certificate).Thumbprint;
        }
        catch // Cerfiticate url not valid or certificate type is not cer (base64 encoded)
        {
          continue;
        }

        // Check if icon is valid
        try
        {
          // Download the icon and check if its at least 256x256
          Image icon = await WebHelper.DownloadImageAsync(server.IconUrl);
          if (icon.Width < 256 || icon.Height < 256)
            continue;

          // Scale the image to 256x256
          server.Icon = new Bitmap(icon, new Size(256, 256));

          // Add the server to the servers that were successfully parsed and checked
          servers.Add(server);
        }
        catch // Image could not be downloaded or loaded
        {
          continue;
        }

        imgLoadingBar.Value++;
      }

      // Load bancho and localhost
      try
      {
        // Download the icon and check if its at least 256x256
        Image icon = await WebHelper.DownloadImageAsync(Server.BanchoServer.IconUrl);
        if (icon.Width >= 256 && icon.Height >= 256)
        {
          // Add the bancho server
          Server s = Server.BanchoServer;
          // Scale the image to 256x256
          s.Icon = new Bitmap(icon, new Size(256, 256));
          servers.Add(s);
        }
      }
      catch // Image could not be downloaded or loaded
      {

      }

      try
      {
        // Download the icon and check if its at least 256x256
        Image icon = await WebHelper.DownloadImageAsync(Server.LocalhostServer.IconUrl);
        if (icon.Width >= 256 && icon.Height >= 256)
        {
          // Add the localhost server
          Server s = Server.LocalhostServer;
          // Scale the image to 256x256
          s.Icon = new Bitmap(icon, new Size(256, 256));
          servers.Add(s);
        }
      }
      catch // Image could not be downloaded or loaded
      {

      }

      // Sort the servers by priority (first bancho, then featured, then normal, then localhost)
      servers = servers.OrderByDescending(x => x.Priority).ToList();

      // Create .ico files for shortcuts
      foreach (Server server in servers)
      {
        using (FileStream fs = File.OpenWrite(Paths.IconCacheFolder + $@"\{server.UID}.ico"))
        using (MemoryStream ms = new MemoryStream((byte[])new ImageConverter().ConvertTo(server.Icon, typeof(byte[]))))
          ImagingHelper.ConvertToIcon(ms, fs, server.Icon.Width, true);
      }

      Switcher.Servers = servers;

      // Enable/Disable the timer depending on if useDiscordRichPresence is true or false
      // Set here because the timer needs to have the servers loaded
      richPresenceUpdateTimer.Enabled = m_settings["useDiscordRichPresence"] == "true";

      // Initialize the current selected server variable
      m_currentSelectedServer = Switcher.GetCurrentServer();
      if (m_currentSelectedServer.IsUnidentified)
        m_currentSelectedServer = Switcher.Servers.First(x => x.UID == "bancho");

      // Hide loading button and loading bar after all mirrors are loaded
      pctrLoading.Visible = false;
      imgLoadingBar.Visible = false;

      // Show the account manager button because all servers are loaded now
      btnAccountManager.Visible = true;

      // Update the UI
      UpdateUI();
    }

    #endregion

    #region Click & CheckChanged Events

    private void pctrServerIcon_Click(object sender, EventArgs e)
    {
      // If the server is set has a discord, open the link
      if (m_currentSelectedServer != null && !string.IsNullOrEmpty(m_currentSelectedServer.DiscordUrl))
        Process.Start(m_currentSelectedServer.DiscordUrl);
    }

    private void BtnConnect_Click(object sender, EventArgs e)
    {
      // Change the shown button to the "connecting" one
      btnConnect.Visible = false;
      pctrConnecting.Visible = true;
      Application.DoEvents();

      // Save the osu executable path for the reopen feature later
      string osuExecutablePath = "";
      // Only close osu if the feature is enabled
      if (m_settings["closeOsuBeforeSwitching"] == "true")
      {
        // Find the osu process and, if found, save the executable path and kill it
        Process osu = Process.GetProcessesByName("osu!").FirstOrDefault();
        if (osu != null)
        {
          osuExecutablePath = osu.MainModule.FileName;
          osu.Kill();
          // Wait till osu is completely closed
          osu.WaitForExit();
        }
      }

      // Switch the server to the currently selected one
      Switcher.SwitchServer(m_currentSelectedServer);

      // Check if the server is available (server that has been switched to is reachable)
      if (!Switcher.CheckServerAvailability())
      {
        // If not, show a warning
        MessageBox.Show("The connection test failed. Please restart the switcher and try again.\r\n\r\nIf it's still not working the server either didn't update their mirror yet or their server is currently not running (for example due to maintenance).", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

      // Start osu if the reopen feature is enabled and an osu instance was found before switching
      // osuExecutablePath can only be != "" if close before switching feature is enabled
      // so a check for the closeOsuBeforeSwitching setting is not necessary
      if (m_settings["reopenOsuAfterSwitching"] == "true" && osuExecutablePath != "")
      {
        Process.Start(osuExecutablePath);
      }

      // Hide the "connecting" button and update the UI (update UI will show the already connected button then)
      pctrConnecting.Visible = false;
      UpdateUI();
    }

    private void btnLeft_Click(object sender, EventArgs e)
    {
      // Move the selection 1 to the left and update the UI
      m_currentSelectedServer = Switcher.Servers[m_currentSelectedServerIndex - 1];
      UpdateUI();
    }

    private void btnRight_Click(object sender, EventArgs e)
    {
      // Move the selection 1 to the right and update the UI
      m_currentSelectedServer = Switcher.Servers[m_currentSelectedServerIndex + 1];
      UpdateUI();
    }
    private void lnklblTelemetryLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Show informations about telemetry logging
      MessageBox.Show("We would appreciate to log some data to improve the user experience for everyone.\r\n\r\nThe following data will be transmitted to our server:\r\n\r\n- Server you are coming from and switching to\r\n- The time span between switching the server\r\n- Connectivity status of servers\r\n\r\n\r\nNote: No informations that would identify you will be transmitted. All informations are completely anonymous.\r\n\r\nYou can stop sending telemtry data at any time by disabling that option.", "UOSS Telemetry Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void lnklblWhyMinimize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Show informations about the minimize to tray option
      MessageBox.Show("Minimize to system tray means that if you close the program it just gets minimized to the system tray\r\n(icons next to the clock in the taskbar).\r\n\r\nThis way, the switcher will run in background and, when you open it again, doesn't need to load all servers again.\r\n\r\nThis feature is useful when you switch the server quite often.\r\n\r\nYou can still close the switcher by either disabling this option or right clicking the icon in the system tray, and then 'Exit'.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void lnklblWhatRichPresence_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Open a discord video about discord rich presence for explaination
      Process.Start(Urls.RichPresenceExplanation);
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      // Close the application
      Application.Exit();
    }

    private void pctrGithub_Click(object sender, EventArgs e)
    {
      // Open the github page
      Process.Start(Urls.Repository);
    }

    private void pctrDiscord_Click(object sender, EventArgs e)
    {
      // Open the page that redirects to the discord invite url
      Process.Start(Urls.Discord);
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // If the show tool strip of the context menu from the notify icon is clicked, show the switcher
      // by simulating a click on the notify icon
      notifyIcon_MouseClick(notifyIcon, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // If the exit tool strip of the context menu from the notify icon is clicked, force-close the program
      // (force close needed because otherwise the FormClosing event would be cancelled again and the program
      // just minimized to the taskbar again)
      m_forceclose = true;
      Application.Exit();
    }

    private void chckbxMinimize_CheckedChanged(object sender, EventArgs e)
    {
      // Save the minimizeToTray setting
      m_settings["minimizeToTray"] = chckbxMinimize.Checked ? "true" : "false";
    }

    private void chckbxSendTelemetry_CheckedChanged(object sender, EventArgs e)
    {
      // Save the sendTelemetry setting
      m_settings["sendTelemetry"] = chckbxSendTelemetry.Checked ? "true" : "false";
    }

    private void chckbxCloseBeforeSwitching_CheckedChanged(object sender, EventArgs e)
    {
      // Only enable the reopen setting control when close before switching is enabled
      chckbxReopenAfterSwitching.Enabled = chckbxCloseBeforeSwitching.Checked;
      // Save the closeOsuBeforeSwitching setting
      m_settings["closeOsuBeforeSwitching"] = chckbxCloseBeforeSwitching.Checked ? "true" : "false";
    }

    private void chckbxReopenAfterSwitching_CheckedChanged(object sender, EventArgs e)
    {
      // Save the reopenOsuAfterSwitching setting
      m_settings["reopenOsuAfterSwitching"] = chckbxReopenAfterSwitching.Checked ? "true" : "false";
    }

    private void chckbxOpenAfterQuickSwitching_CheckedChanged(object sender, EventArgs e)
    {
      m_settings["openOsuAfterQuickSwitching"] = chckbxOpenAfterQuickSwitching.Checked ? "true" : "false";
    }

    private void chckbxUseDiscordRichPresence_CheckedChanged(object sender, EventArgs e)
    {
      m_settings["useDiscordRichPresence"] = chckbxUseDiscordRichPresence.Checked ? "true" : "false";

      // If the presence feature gets deactivated, remove the precense if needed
      if (!chckbxUseDiscordRichPresence.Checked && Discord.IsPrecenseSet)
        Discord.RemovePresence();
      else
        MessageBox.Show("In order to make this feature run properly, please disable the Discord Rich Presense in your osu! settings.\r\n\r\nIf it still doesn't show up, try to switch the server or restart the switcher in order to reload the Discord Rich Presence.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Information);

      // En/Disable the timer that constantly checks if osu is running
      richPresenceUpdateTimer.Enabled = chckbxUseDiscordRichPresence.Checked;
    }

    private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
    {
      // Only show when left mouse button is used because the right should show the context menu
      if (e.Button == MouseButtons.Left)
      {
        // Hide the notify icon and show the switcher when clicked on the notify icon
        notifyIcon.Visible = false;
        Show();
      }
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

    private void richPresenceUpdateTimer_Tick(object sender, EventArgs e)
    {
      // Get all osu instances to check if osu is running
      Process[] osuInstances = Process.GetProcessesByName("osu!");
      // If no osu is running but a presence is set, remove that presence
      if (osuInstances.Length == 0 && Discord.IsPrecenseSet)
        Discord.RemovePresence();
      else if (osuInstances.Length > 0)
      {
        // Get the current server
        Server currentServer = Switcher.GetCurrentServer();
        // Check if either no presence is set or the current presence is a different server
        // If the presence is a different server, update the presence (server was switched)
        if (!Discord.IsPrecenseSet || Discord.ShownServer.UID != currentServer.UID)
          Discord.SetPresenceServer(currentServer);
      }
    }

    private void BorderlessDragMouseDown(object sender, MouseEventArgs e)
    {
      // Make borderless window moveable
      if (e.Button == MouseButtons.Left)
      {
        WinApi.ReleaseCapture();
        WinApi.SendMessage(Handle, 0xA1, 0x2, 0);
      }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      // If the minimize to tray option is enabled, hide the switcher, 
      // show the tray symbol and cancel the exit
      if (chckbxMinimize.Checked && !m_forceclose)
      {
        e.Cancel = true;
        notifyIcon.Visible = true;
        Hide();
      }
    }

    protected override void WndProc(ref Message m)
    {
      // Override the wndproc event to receive messages from other switcher instances (quick switch, multi instance, ...)

      if (m.Msg == NativeMethods.WM_WAKEUP) // Called by a second switcher instance to tell this switcher to get to the foreground
      {
        // If the switcher is not minimized to the system tray, just focus it; otherwise simulate a click on the notify icon
        if (Visible)
          Focus();
        else
          notifyIcon_MouseClick(notifyIcon, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
      }
      else
        base.WndProc(ref m);
    }

    #endregion

    private void UpdateUI()
    {
      // Get the current server to update the UI
      Server currentServer = Switcher.GetCurrentServer();

      // If the server the user is currently connected to unidentified the icon of the server cannot be shown
      // because its unknown. Otherwise show the icon of the current connected server
      pctrCurrentServer.Image = currentServer.IsUnidentified ? null : currentServer.Icon;

      // Show the image of the server that is currently selected
      pctrCurrentSelectedServer.Image = m_currentSelectedServer.Icon;

      // Show the connect / already connected button whether the currently selected server
      // is the one the user is connected to or not
      btnConnect.Visible = m_currentSelectedServer.UID != currentServer.UID;
      pctrAlreadyConnected.Visible = m_currentSelectedServer.UID == currentServer.UID;

      // Show the left and right arrow whether the index allows to go to the left or right
      btnRight.Visible = m_currentSelectedServerIndex < Switcher.Servers.Count - 1;
      btnLeft.Visible = m_currentSelectedServerIndex > 0;

      // Set the info text to the name of the current selected server
      lblInfo.Text = m_currentSelectedServer.ServerName;
      // Change the color of the server name depending on if the server is featured or not
      lblInfo.ForeColor = m_currentSelectedServer.IsFeatured ? Color.Orange : Color.White;
      // Show the verified badge depending on if the server is featured or not
      pctrVerified.Visible = m_currentSelectedServer.IsFeatured;

      // Get a graphics object to measure the size to the info text
      // in order to position the verified badge next to the text
      Graphics g = CreateGraphics();
      // Measure the size of the text
      SizeF textSize = g.MeasureString(m_currentSelectedServer.ServerName, lblInfo.Font);
      // Set the location, x is the middle of the panel + half the text - 5
      // to position the verified badge next to the next (-5 to make the distance less)
      pctrVerified.Location = new Point(pnlSwitcher.Width / 2 + (int)textSize.Width / 2 - 5, pctrVerified.Location.Y);
    }
  }
}