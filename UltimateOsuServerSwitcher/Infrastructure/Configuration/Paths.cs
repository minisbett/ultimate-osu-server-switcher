using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class Paths
  {
    /// <summary>
    /// Folder where all files for the switcher are saved
    /// /// </summary>
    public static string AppFolder => Environment.GetEnvironmentVariable("localappdata") + @"\UltimateOsuServerSwitcher";

    /// <summary>
    /// Cache file for the telemetry
    /// </summary>
    public static string TelemetryCacheFile => AppFolder + @"\telemetry_cache";

    /// <summary>
    /// Folder where the .ico icons for the shortcuts are saved
    /// </summary>
    public static string IconCacheFolder => AppFolder + @"\IconCache";

    /// <summary>
    /// Settings file where all switcher settings are saved
    /// </summary>
    public static string SettingsFile => AppFolder + @"\settings";
  }
}
