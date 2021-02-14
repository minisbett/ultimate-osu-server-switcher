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
using UltimateOsuServerSwitcher.Infrastructure;
using UltimateOsuServerSwitcher.Model;
using UltimateOsuServerSwitcher.Utils;

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

    // The settings instance for the saved osu accounts
    private Settings m_accounts => new Settings(Paths.AccountsFile);

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
      m_settings.SetDefaultValue("useDiscordRichPresence", "false");
      m_settings.SetDefaultValue("switchAccount", "false");

      // Initialize the list of saved accounts
      m_accounts.SetDefaultValue("accounts", JsonConvert.SerializeObject(new List<Account>()));

      // Set the settings controls to their state from the settings file
      chckbxMinimize.Checked = m_settings["minimizeToTray"] == "true";
      chckbxSendTelemetry.Checked = m_settings["sendTelemetry"] == "true";
      chckbxCloseBeforeSwitching.Checked = m_settings["closeOsuBeforeSwitching"] == "true";
      chckbxReopenAfterSwitching.Checked = m_settings["reopenOsuAfterSwitching"] == "true";
      chckbxUseDiscordRichPresence.Checked = m_settings["useDiscordRichPresence"] == "true";
      chckbxSwitchAccount.Checked = m_settings["switchAccount"] == "true";

      //
      // Telemetry disabled because not finished yet
      //
      m_settings["sendTelemetry"] = "false";
      chckbxSendTelemetry.Checked = false;
      chckbxSendTelemetry.Enabled = false;
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
        MessageBox.Show($"Your current version ({VersionChecker.CurrentVersion}) is blacklisted.\r\n\r\nThis can happen when the version contains security flaws or other things that could interrupt a good user experience.\r\n\r\nPlease download the newest version of the switcher from our GitHub page.\r\n(github.com/minisbett/ultimate-osu-server-switcher/releases)", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
        return;
      }
      else if (vs == VersionState.MAINTENANCE)
      {
        // If the switcher is in maintenance, also prevent the user from using it.
        MessageBox.Show("The switcher is currently hold in maintenance mode which means that the switcher is currently not available.\r\n\r\nJoin our discord server for more informations.\r\nThe Discord server and be found on our GitHub page.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
        return;
      }
      else if (vs == VersionState.OUTDATED)
      {
        // Show the user a message that a new version is available if the current switcher is outdated.
        MessageBox.Show($"Your switcher version ({VersionChecker.CurrentVersion}) is outdated.\r\nA newer version ({await VersionChecker.GetNewestVersion()}) is available.\r\n\r\nWe recommend to download the newest version of the switcher from our GitHub page.\r\n(github.com/minisbett/ultimate-osu-server-switcher/releases)", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        MessageBox.Show("Error whilst parsing the server mirrors from GitHub!\r\nPlease make sure you can connect to www.github.com in your browser.\r\n\r\nIf this issue persists, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }

      // set the maximum to the amount of mirrors + amount of static servers that need to be loaded
      imgLoadingBar.Maximum = mirrors.Count + Server.StaticServers.Length;

      // Parse every server from their mirror
      foreach (Mirror mirror in mirrors)
      {
        lblInfo.Text = $"Loading mirror {mirror.Url}";
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

        // add the server
        await Switcher.AddServer(server);

        imgLoadingBar.Value++;
        Application.DoEvents();
      }

      // Load the static servers
      foreach (Server server in Server.StaticServers)
      {
        lblInfo.Text = $"Loading {server.ServerName}";
        Application.DoEvents();

        // add the server directly because hardcoded servers dont need the validation checks (or would even fail there)
        await Switcher.AddServerNoCheck(server);

        imgLoadingBar.Value++;
        Application.DoEvents();
      }

      // Remove all saved accounts from servers that may no longer exist
      List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(m_accounts["accounts"]);
      accounts.RemoveAll(a => !Switcher.Servers.Any(x => x.UID == a.ServerUID));
      m_accounts["accounts"] = JsonConvert.SerializeObject(accounts);

      // Enable/Disable the timer depending on if useDiscordRichPresence is true or false
      // Set here because the timer needs to have the servers loaded
      richPresenceUpdateTimer.Enabled = m_settings["useDiscordRichPresence"] == "true";

      // Initialize the current selected server variable
      m_currentSelectedServer = Switcher.GetCurrentServer();
      // If current connected server is unidentified, show/select bancho
      if (m_currentSelectedServer.IsUnidentified)
        m_currentSelectedServer = Switcher.Servers.First(x => x.UID == "bancho");

      // Hide loading button and loading bar after all mirrors are loaded
      pctrLoading.Visible = false;
      imgLoadingBar.Visible = false;

      // Show the account manager button and the Create A Shortcut linklabel because all servers are loaded now
      btnAccountManager.Visible = true;
      lnklblCreateShortcut.Visible = true;

      // Update the UI
      UpdateUI();
    }

    #endregion

    #region Click & CheckChanged Events

    private void pctrCurrentSelectedServer_DoubleClick(object sender, EventArgs e)
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

      // Check if the user is holding ctrl to quick start osu
      bool pressingCtrl = System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) || System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl);

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
        MessageBox.Show("The connection test failed. Please restart the switcher and try again.\r\n\r\nIf it's still not working the server either didn't update their mirror yet or their server is currently not running (for example due to maintenance).\nIn this case, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }

      // Start osu if the reopen feature is enabled and an osu instance was found before switching
      // osuExecutablePath can only be != "" if close before switching feature is enabled
      // so a check for the closeOsuBeforeSwitching setting is not necessary
      if (m_settings["reopenOsuAfterSwitching"] == "true" && osuExecutablePath != "")
      {
        WinUtils.StartProcessUnelevated(osuExecutablePath);
      }
      else if (pressingCtrl) // Start osu if ctrl was pressed when clicking on connect and it has not started already ue to the reopen feature
      {
        // If we dont have the exe path yet because osu was not killed before get it from the registry
        if (osuExecutablePath == "")
        {
          // Check if the osu path could be found
          string osuDir = Paths.OsuFolder;
          if (osuDir == null)
          {
            MessageBox.Show("The path to the osu!.exe file could not be found.\n\nPlease make sure you installed osu correctly by starting it as an admin.\n\nIf this issue persists, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
          else // build the path
            osuExecutablePath = Path.Combine(osuDir, "osu!.exe");
        }

        // Run osu if a path has been found
        if (osuExecutablePath != "")
          WinUtils.StartProcessUnelevated(osuExecutablePath);
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

    private void lnklblCreateShortcut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // let the user decide where to save the shortcut
      SaveFileDialog sfd = new SaveFileDialog();
      sfd.Filter = "Shortcut|*.lnk";
      sfd.FileName = $"Play on {m_currentSelectedServer.ServerName}.lnk";
      if(sfd.ShowDialog() == DialogResult.OK)
      {
        // Save the shortcut
        QuickSwitch.CreateShortcut(sfd.FileName, m_currentSelectedServer);
      }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      // force close instead of hiding to the system tray when pressing ctrl
      if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) || System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl))
        m_forceclose = true;

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

    private void chckbxUseDiscordRichPresence_CheckedChanged(object sender, EventArgs e)
    {
      // Save the useDiscordRichPresence setting
      m_settings["useDiscordRichPresence"] = chckbxUseDiscordRichPresence.Checked ? "true" : "false";

      // If the presence feature gets deactivated, remove the precense if needed
      if (!chckbxUseDiscordRichPresence.Checked && Discord.IsPrecenseSet)
        Discord.RemovePresence();
      else if (chckbxUseDiscordRichPresence.Checked) // If the feature is getting enabled show informations
        MessageBox.Show("In order to make this feature run properly, please disable the Discord Rich Presense in your osu! settings.\r\n\r\nIf it still doesn't show up, try to switch the server or restart the switcher in order to reload the Discord Rich Presence.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Information);

      // En/Disable the timer that constantly checks if osu is running
      richPresenceUpdateTimer.Enabled = chckbxUseDiscordRichPresence.Checked;
    }

    private void chckbxSwitchAccount_CheckedChanged(object sender, EventArgs e)
    {
      // Save the switchAccount setting
      m_settings["switchAccount"] = chckbxSwitchAccount.Checked ? "true" : "false";
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

    private void btnAccountManager_Click(object sender, EventArgs e)
    {
      // Open the account manager
      new AccountManager().ShowDialog();
    }

    private void pctrAlreadyConnected_Click(object sender, EventArgs e)
    {
      // Open osu if ctrl is pressed
      if(System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) || System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl))
      {
        // Get osu dir and check if successful
        string osuDir = Paths.OsuFolder;
        if (osuDir != null)
          WinUtils.StartProcessUnelevated(Path.Combine(osuDir, "osu!.exe"));
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

    // 5 second timer
    private void richPresenceUpdateTimer_Tick(object sender, EventArgs e)
    {
      // Get all osu instances to check if osu is running
      Process[] osuInstances = Process.GetProcessesByName("osu!");
      // If no osu is running but a presence is set, remove that presence
      if (osuInstances.Length == 0 && Discord.IsPrecenseSet)
        Discord.RemovePresence();
      else if (osuInstances.Length > 0) // If osu is running
      {
        // Get the current server
        Server currentServer = Switcher.GetCurrentServer();

        // Get the osu window title to determine what the user is currently doing
        string title = osuInstances[0].MainWindowTitle;

        // get the username to display in the rpc
        string username = OsuConfigFileUtils.GetAccount().username;

        // If title is just "osu!" the user is in idle state
        if (title == "osu!")
          Discord.SetPresence("Idle", $"{username ?? "?"} - {currentServer.ServerName}");
        else if (title.StartsWith("osu!  -")) // If title starts with "osu!  -" there is a map name behind it that the user is playing/watching/editing
          if (title.EndsWith(".osu")) // If the title ends with .osu the user is in the editor
            Discord.SetPresence($"Editing: {title.Substring(8)}", $"{username ?? "?"} - {currentServer.ServerName}"); // substring 8 to remove the "osu!  - "
          else // Otherwise hes playing or watching
            Discord.SetPresence($"Playing: {title.Substring(8)}", $"{username ?? "?"} - {currentServer.ServerName}"); // substring 8 to remove the "osu!  - "
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
      else if(m.Msg == NativeMethods.WM_UPDATE)
      {
        // If the QuickSwitch feature was used update the button if you are currently connected
        // e.g. if you have bancho selected and use a shortcut to switch to bancho to update from "connect" to "already connected"
        Server currentServer = Switcher.GetCurrentServer();
        btnConnect.Visible = m_currentSelectedServer.UID != currentServer.UID;
        pctrAlreadyConnected.Visible = m_currentSelectedServer.UID == currentServer.UID;
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
      // Show the featured badge depending on if the server is featured or not
      pctrFeatured.Visible = m_currentSelectedServer.IsFeatured;

      // Get a graphics object to measure the size to the info text
      // in order to position the featured badge next to the text
      Graphics g = CreateGraphics();
      // Measure the size of the text
      SizeF textSize = g.MeasureString(m_currentSelectedServer.ServerName, lblInfo.Font);
      // Set the location, x is the middle of the panel + half the text - 5
      // to position the featured badge next to the next (-5 to make the distance less)
      pctrFeatured.Location = new Point(pnlSwitcher.Width / 2 + (int)textSize.Width / 2 - 5, pctrFeatured.Location.Y);
    }
  }
}