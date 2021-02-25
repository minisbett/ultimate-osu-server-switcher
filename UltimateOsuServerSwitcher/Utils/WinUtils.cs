using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher.Utils
{
  public static class WinUtils
  {
    /// <summary>
    /// Starts a file in an unelevated environment
    /// </summary>
    /// <param name="file">The file to start</param>
    public static void StartProcessUnelevated(string file)
    {
      Process.Start("explorer.exe", $"\"{file}\"");
    }
  }
}
