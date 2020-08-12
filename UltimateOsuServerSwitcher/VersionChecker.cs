using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class VersionChecker
  {
    public static string CurrentVersion = "2.0";

    private static WebClient m_client = new WebClient();

    public async static Task<VersionState> GetCurrentState()
    {
      string blacklistedVersionsStr = await m_client.DownloadStringTaskAsync("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/BLACKLISTED_VERSIONS");
      string[] blacklistedVersions = blacklistedVersionsStr.Replace("\r", "").Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      if (blacklistedVersions.Length >= 1 && blacklistedVersions[0] == "*")
        return VersionState.MAINTENANCE;
      if (blacklistedVersions.Contains(CurrentVersion))
        return VersionState.BLACKLISTED;
      if (await GetNewestVersion() != CurrentVersion)
        return VersionState.OUTDATED;

      return VersionState.LATEST;
    }

    public async static Task<string> GetNewestVersion()
    {
      string newestVersion = await m_client.DownloadStringTaskAsync("https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/VERSION");
      newestVersion = newestVersion.Replace("\n", "");
      return newestVersion;
    }
  }

  public enum VersionState
  {
    LATEST,
    OUTDATED,
    BLACKLISTED,
    MAINTENANCE
  }
}