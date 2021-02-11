using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

    /// <summary>
    /// Accounts file where all accounts are saved
    /// </summary>
    public static string AccountsFile => AppFolder + @"\accounts";

    /// <summary>
    /// The path to the osu directory where osu is installed
    /// </summary>
    public static string OsuFolder => getOsuFolder();

    private static string getOsuFolder()
    {
      try
      {
        // Get the osu executeable path from the registry if osu was installed correctly
        // path will look like this: "F:\\osu!\\osu.exe",1
        string osuexe = Registry.GetValue("HKEY_CLASSES_ROOT\\osu\\DefaultIcon", "", "").ToString();
        // Remove the "" and the ,1 at the end
        osuexe = osuexe.Substring(1, osuexe.Length - 4);

        // Get the directory of the exe file
        string osudir = new FileInfo(osuexe).DirectoryName;

        // return the directory
        return osudir;
      }
      catch
      {
        // If something went wrong (e.g. osu is not installed so the registry key could not be found) return null
        return null;
      }
    }

    /// <summary>
    /// The path to the config file of the osu installation.
    /// Returns null if the path could not be built. (either through missing registry key or not existing config file)
    /// </summary>
    public static string OsuConfigFile => getOsuConfigFile();

    private static string getOsuConfigFile()
    {
      try
      {
        // Build the config file path
        string configFileName = $"osu!.{Environment.UserName}.cfg";
        string configFile = Path.Combine(getOsuFolder(), configFileName);

        // Check if the config file exists
        if (!File.Exists(configFile))
          return null;

        // Return the config file
        return configFile;
      }
      catch
      {
        // If something went wrong (e.g. osu is not installed or hasnt started on this windows account yet) return null
        return null;
      }
    }
  }
}
