using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class Variables
  {
    /// <summary>
    /// All osu domains that need to be forwarded in the hosts file
    /// </summary>
    public static string[] OsuDomains => new string[]
    {
          "osu.ppy.sh", "c.ppy.sh", "c1.ppy.sh", "c2.ppy.sh", "c3.ppy.sh",
          "c4.ppy.sh", "c5.ppy.sh", "c6.ppy.sh", "ce.ppy.sh", "a.ppy.sh", "i.ppy.sh"
    };

    /// <summary>
    /// Domain part to identify the lines in the hosts file that redirects osu stuff
    /// </summary>
    public static string HostsIdentifier => ".ppy.sh";
  }
}
