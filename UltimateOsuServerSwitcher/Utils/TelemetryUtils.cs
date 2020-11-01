using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class TelemetryUtils
  {
    /// <summary>
    /// Converts a millisecond integer into a readable string e.g.
    /// </summary>
    /// <param name="millis">The milliseconds to parse to a readable string</param>
    /// <returns></returns>
    public static string MillisecondsToString(int millis)
    {
      // If the milliseconds are 0 or negative, it makes no fucking sense
      // I mean, -4 days?! I will switch the server in 4 days!
      if (millis < 1)
        return "undefined";
      // Get how many days/hours/minutes/... would be these milliseconds
      int seconds = (int)Math.Floor((double)millis / 1000);
      int minutes = (int)Math.Floor((double)seconds / 60);
      int hours = (int)Math.Floor((double)minutes / 60);
      int days = (int)Math.Floor((double)hours / 24);

      // Check if it is at least a day, an hour, a minute, ...
      if (days > 0)
      {
        return $"{days} day{(days > 1 ? "s" : "")}";
      }
      else if (hours > 0)
      {
        return $"{hours} hour{(hours > 1 ? "s" : "")}";
      }
      else if (minutes > 0)
      {
        return $"{minutes} minute{(minutes > 1 ? "s" : "")}";
      }
      else if (seconds > 0)
      {
        return $"{seconds} second{(seconds > 1 ? "s" : "")}";
      }

      return "undefined";
    }
  }
}
