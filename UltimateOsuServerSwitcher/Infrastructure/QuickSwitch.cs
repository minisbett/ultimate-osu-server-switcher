using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Utils;

namespace UltimateOsuServerSwitcher.Infrastructure
{
  public static class QuickSwitch
  {
    // The settings for the switcher
    private static Settings m_settings => new Settings(Paths.SettingsFile);

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
      shortcut.IconLocation = Paths.IconCacheFolder + $@"\{server.UID}.ico";
      shortcut.TargetPath = Application.ExecutablePath;
      shortcut.Arguments = server.UID;
      shortcut.Save();
    }

    public static void LoadServers()
    {
      // Try to load online data and verify servers
      List<Mirror> mirrors = null;
      try
      {
        // Download the mirror data from github and deserialize it into a mirror list
        mirrors = JsonConvert.DeserializeObject<List<Mirror>>(WebHelper.DownloadStringAsync(Urls.Mirrors).Result);
      }
      catch
      {
        // If it was not successful, github may cannot currently be reached or I made a mistake in the json data.
        MessageBox.Show("Error whilst parsing the server mirrors from GitHub!\r\nPlease make sure you can connect to www.github.com in your browser.\r\n\r\nIf this issue persists, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }

      // Parse every server from their mirror
      foreach (Mirror mirror in mirrors)
      {
        Server server = null;
        // Try to serialize the mirror into a server
        try
        {
          // Download the data from the mirror and try to parse it into a server object.
          server = JsonConvert.DeserializeObject<Server>(WebHelper.DownloadStringAsync(mirror.Url).Result);
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
        Switcher.AddServer(server).GetAwaiter().GetResult();
      }

      // Load the static servers
      foreach (Server server in Server.StaticServers)
      {
        // add the server directly because hardcoded servers dont need the validation checks (or would even fail there)
        Switcher.AddServerNoCheck(server).GetAwaiter().GetResult();
      }
    }

    public static void Switch(Server server)
    {
      // Only close osu if the feature is enabled
      if (m_settings["closeOsuBeforeSwitching"] == "true")
      {
        // Find the osu process and, if found, kill it
        Process osu = Process.GetProcessesByName("osu!").FirstOrDefault();
        if (osu != null)
        {
          osu.Kill();
          // Wait till osu is completely closed
          osu.WaitForExit();
        }
      }

      // Switch only if the user is not already connected
      Server current = Switcher.GetCurrentServer();
      if (current.UID != server.UID)
      {
        Switcher.SwitchServer(server);

        // Check if the server is available (server that has been switched to is reachable)
        if (!Switcher.CheckServerAvailability())
        {
          // If not, show a warning
          MessageBox.Show("The connection test failed. Please try again.\r\n\r\nIf it's still not working the server either didn't update their mirror yet or their server is currently not running (for example due to maintenance).\nIn this case, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }

      // Check if osu ise not running (it may be when close when switching feature is disabled)
      if (Process.GetProcessesByName("osu!").FirstOrDefault() == null)
      {
        // Check if the osu path could be found
        string osuDir = Paths.OsuFolder;
        if (osuDir == null)
        {
          MessageBox.Show("The path to the osu!.exe file could not be found.\n(server was still switched)\n\nPlease make sure you installed osu correctly by starting it as an admin.\n\nIf this issue persists, please visit our Discord server.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        // build the osu path
        string osuExecutablePath = Path.Combine(osuDir, "osu!.exe");

        // run osu
        WinUtils.StartProcessUnelevated(osuExecutablePath);
      }
    }
  }
}
