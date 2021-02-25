using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher.Model
{
  public class HistoryElement
  {

    [JsonProperty("datetime")]
    /// <summary>
    /// Datetime when the switch happened
    /// </summary>
    public DateTime DateTime { get; private set; }

    /// <summary>
    /// UID of the server from which the switch was done
    /// </summary>
    [JsonProperty("from")]
    public string FromUID { get; private set; }

    /// <summary>
    /// UID of the server to which was switched
    /// </summary>
    [JsonProperty("to")]
    public string ToUID { get; private set; }

    public HistoryElement(DateTime datetime, string fromuid, string touid)
    {
      DateTime = datetime;
      FromUID = fromuid;
      ToUID = touid;
    }
  }
}
