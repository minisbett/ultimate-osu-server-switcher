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
    [JsonProperty("url")]
    public string Url { get; private set; } = null;

    [JsonProperty("featured")]
    public bool Featured { get; private set; } = false;
  }
}
