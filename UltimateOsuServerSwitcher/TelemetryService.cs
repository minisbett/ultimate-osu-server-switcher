using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class TelemetryService
  {

    // The telemetry cache file
    private static string telemetryCacheFile = Environment.GetEnvironmentVariable("localappdata") + @"\UltimateOsuServerSwitcher\telemetry_cache.txt";

    /// <summary>
    /// Send telemetry to the server
    /// </summary>
    /// <param name="from">The server the user is coming from</param>
    /// <param name="to">The server the user is switching to</param>
    /// <param name="span">The span between this and the last switch</param>
    /// <param name="successful">Connectivity status of the switched to server</param>
    public static void SendTelemetry(string from, string to, int span, bool successful)
    {
      string url = "https://uosuss.000webhostapp.com/index.php";
      string get = $"from={from}&to={to}&span={(span != -1 ? millisecondsToReadableString(span) : "undefined")}&successful={(successful ? 1 : 0)}";
      new WebClient().DownloadData(url + "?" + get);
    }

    /// <summary>
    /// Gets the telemetry cache
    /// </summary>
    /// <returns>The telemetry cache</returns>
    public static string GetTelemetryCache()
    {
      if (!File.Exists(telemetryCacheFile))
        return "";
      return File.ReadAllText(telemetryCacheFile);
    }

    /// <summary>
    /// Sets the telemetry cache
    /// </summary>
    /// <param name="cache">The telemetry cache</param>
    public static void SetTelemetryCache(string cache)
    {
      File.WriteAllText(telemetryCacheFile, cache);
    }

    // Converts a millisecond integer into a readable string e.g.
    // 5000 = 5 seconds
    // 62000 = 1 minute
    // 345,600,000 = 4 days
    private static string millisecondsToReadableString(int millis)
    {
      // If the milliseconds are 0 or negative, it makes no fucking sense
      // I mean, -4 days?! I will switch the server in 4 days!
      if (millis < 1)
        return "Undefined";
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

      return "Undefined";
    }
  }
}
