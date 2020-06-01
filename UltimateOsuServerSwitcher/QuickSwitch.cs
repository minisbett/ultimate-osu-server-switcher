using IWshRuntimeLibrary;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  public static class QuickSwitch
  {
    public static void CreateShortcut(Server server, string path)
    {
      WshShell shell = new WshShell();
      IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(path, server.ServerName + ".lnk"));
      shortcut.Description = $"Switch to {server.ServerName} and start osu!";
      shortcut.TargetPath = $"{Application.ExecutablePath} \"{server.ServerName}\"";
    }

    public static async void Switch(string servername)
    {
      var data = await FetchOnlineDataAsync();
      Server server = data.Servers.FirstOrDefault(x => x.ServerName == servername);
      if (server == null)
      {
        MessageBox.Show("The referenced server could not be found.\r\n\r\nPlease try to recreate this shortcut.", "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }
    }

    private static WebClient m_client = new WebClient();

    private static async Task<string> DownloadAsync(string url)
    {
      var result = await m_client.DownloadStringTaskAsync(new Uri(url));
      return result;
    }

    private static async Task<Data> FetchOnlineDataAsync()
    {
      string raw = await DownloadAsync("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/data/data.json");
      return JsonConvert.DeserializeObject<Data>(raw);
    }
  }
}
