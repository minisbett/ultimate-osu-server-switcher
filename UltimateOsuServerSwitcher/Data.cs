using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public class Data
  {  
    [JsonProperty("about")]
    public string AboutText { get; private set; }

    [JsonProperty("servers")]
    public Server[] Servers { get; private set; }
  }
}
