using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace UltimateOsuServerSwitcher
{
  public static class QuickSwitch
  {
    // Path to the icon cache
    static string m_iconCacheFolder = Environment.GetEnvironmentVariable("localappdata") + @"\UltimateOsuServerSwitcher\IconCache";

    public static void CreateShortcut(Server server, string path)
    {
      WshShell shell = new WshShell();
      IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(path);
      shortcut.Description = $"Switch to {server.ServerName} and start osu!";
      shortcut.IconLocation = m_iconCacheFolder + $@"\{server.ServerName}.ico";
      shortcut.TargetPath = Application.ExecutablePath;
      shortcut.Arguments = server.ServerName;
      shortcut.Save();
    }

    private static WebClient m_client = new WebClient();

    public static void Switch(string servername)
    {
      var data = JsonConvert.DeserializeObject<Data>(m_client.DownloadString("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/data/data.json"));
      List<Server> servers = data.Servers.ToList();
      servers.Add(Server.BanchoServer);
      Server server = servers.FirstOrDefault(x => x.ServerName == servername);
      if (server == null)
      {
        MessageBox.Show("The referenced server could not be found.\r\n\r\nPlease try to recreate this shortcut.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      CertificateManager.UninstallAllCertificates(data.Servers.ToList());
      List<string> hosts = HostsUtil.GetHosts().ToList();
      hosts.RemoveAll(x => x.Contains(".ppy.sh"));
      HostsUtil.SetHosts(hosts.ToArray());

      if (!server.IsBancho)
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
          hosts.Add(server.ServerIP + " " + domain);

        HostsUtil.SetHosts(hosts.ToArray());
        CertificateManager.InstallCertificate(server);
      }

      Process.Start(GetOsuExecutablePath());
    }

    public static string GetOsuExecutablePath()
    {
      string regKey = "HKEY_CLASSES_ROOT\\osu\\shell\\open\\command";
      string altRegKey = "HKEY_CLASSES_ROOT\\osu!\\shell\\open\\command";
      try
      {
        string dir = Registry.GetValue(regKey, "", "").ToString();
        if (dir == "")
          dir = Registry.GetValue(altRegKey, "", "").ToString();
        if (dir != "")
          return dir.Remove(0, 1).Split('"')[0];
      }
      catch
      {
        return "";
      }
      return "";
    }
  }
}
