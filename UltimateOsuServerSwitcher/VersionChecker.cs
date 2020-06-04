using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class VersionChecker
  {
    private static string m_currentVersion = "1.3";

    private static WebClient m_client = new WebClient();

    public async static Task<bool> NewVersionAvailable()
    {
      string newestVersion = await m_client.DownloadStringTaskAsync("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/data/version.txt");
      newestVersion = newestVersion.Replace("\n", "");
      
      return m_currentVersion != newestVersion;
    }
  }
}