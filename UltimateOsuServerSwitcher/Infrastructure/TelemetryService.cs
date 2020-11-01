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
    /// <summary>
    /// Send telemetry to the server
    /// </summary>
    /// <param name="from">The server the user is coming from</param>
    /// <param name="to">The server the user is switching to</param>
    /// <param name="span">The span between this and the last switch</param>
    /// <param name="successful">Connectivity status of the switched to server</param>
    public static void SendTelemetry(string from, string to, string span, bool successful)
    {
      // Send a get request with the arguments as parameters
      string url = "https://uosuss.000webhostapp.com/index.php";
      string get = $"from={from}&to={to}&span={span}&successful={(successful ? 1 : 0)}";
      new WebClient().DownloadData(url + "?" + get);
    }

    /// <summary>
    /// Gets the telemetry cache
    /// </summary>
    /// <returns>The telemetry cache</returns>
    public static string GetTelemetryCache()
    {
      // If the file does not exist yet, return nothing
      if (!File.Exists(Paths.TelemetryCacheFile))
        return "";
      return File.ReadAllText(Paths.TelemetryCacheFile);
    }

    /// <summary>
    /// Sets the telemetry cache
    /// </summary>
    /// <param name="cache">The telemetry cache</param>
    public static void SetTelemetryCache(string cache)
    {
      // Write to the telemetry cache file
      File.WriteAllText(Paths.TelemetryCacheFile, cache);
    }
  }
}
