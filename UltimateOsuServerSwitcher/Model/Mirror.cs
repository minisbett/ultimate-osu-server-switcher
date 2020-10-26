using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public class Mirror
  {
    /// <summary>
    /// A unique id to identify the mirror/server
    /// </summary>
    [JsonProperty("uid")]
    public string UID { get; private set; } = null;

    /// <summary>
    /// The url of the mirror
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; private set; } = null;

    /// <summary>
    /// Determines whether the server is featured or not
    /// </summary>
    [JsonProperty("featured")]
    public bool Featured { get; private set; } = false;
  }
}
