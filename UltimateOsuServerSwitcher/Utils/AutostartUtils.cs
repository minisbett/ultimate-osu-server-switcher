using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher.Utils
{
  public static class AutostartUtils
  {

    // The autostart link file
    private static string m_autostartFile = Environment.GetEnvironmentVariable("appdata") + @"\Microsoft\Windows\Start Menu\Programs\Startup\UltimateOsuServerSwitcher.lnk";

    /// <summary>
    /// Creates or updates the link file in the autostart folder of windows
    /// </summary>
    public static void UpdateAutostartFile()
    {
      WshShell shell = new WshShell();
      IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(m_autostartFile);
      shortcut.TargetPath = Application.ExecutablePath;
      shortcut.Arguments = "-silent";
      shortcut.Save();
    }

    /// <summary>
    /// Removes the link file from the autostart folder
    /// </summary>
    public static void RemoveAutostartFile()
    {
      if (System.IO.File.Exists(m_autostartFile))
        System.IO.File.Delete(m_autostartFile);
    }
  }
}
