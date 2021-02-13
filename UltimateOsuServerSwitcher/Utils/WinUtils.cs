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
    public static void StartProcessUnelevated(string file)
    {
      Process.Start("explorer.exe", file);
    }
  }
}
