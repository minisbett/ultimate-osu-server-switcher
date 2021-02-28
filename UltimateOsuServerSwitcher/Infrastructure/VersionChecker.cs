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
    /// <summary>
    /// The current version of this switcher
    /// </summary>
    public static string CurrentVersion = "2.0.0";

    // The web client used for all web connections in this class
    private static WebClient m_client = new WebClient();

    /// <summary>
    /// Gets the current version state of this switcher
    /// </summary>
    /// <returns>The version state of this switcher</returns>
    public async static Task<VersionState> GetCurrentState()
    {
      // Downloads the blacklisted version file and parses them into an array
      string blacklistedVersionsStr = await m_client.DownloadStringTaskAsync(Urls.Blacklist);
      string[] blacklistedVersions = blacklistedVersionsStr.Replace("\r", "").Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      // If the blacklist starts with "*", return MAINTENANCE
      if (blacklistedVersions.Length >= 1 && blacklistedVersions[0] == "*")
        return VersionState.MAINTENANCE;
      // If the blacklist contains the version of this switcher, return BLACKLISTED
      if (blacklistedVersions.Contains(CurrentVersion))
        return VersionState.BLACKLISTED;
      // If the newest version is not the one of this switcher, return OUTDATED
      if (await GetNewestVersion() != CurrentVersion)
        return VersionState.OUTDATED;

      // Otherwise, return LATEST
      return VersionState.LATEST;
    }

    /// <summary>
    /// Gets the newest version available of the switcher
    /// </summary>
    /// <returns>The newest available version</returns>
    public async static Task<string> GetNewestVersion()
    {
      // Download the VERSION file, remove the white space and return
      string newestVersion = await m_client.DownloadStringTaskAsync(Urls.VersionFile);
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