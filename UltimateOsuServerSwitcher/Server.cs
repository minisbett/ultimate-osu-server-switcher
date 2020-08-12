using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public class Server
  {

    private WebClient m_client = new WebClient();

    [JsonProperty("name")]
    public string ServerName { get; private set; } = null;

    public bool IsFeatured { get; set; } = false;

    [JsonProperty("ip")]
    public string ServerIP { get; private set; } = null;

    [JsonProperty("certificate_url")]
    public string CertificateUrl { get; private set; } = null;

    public byte[] Certificate { get; set; } = null;

    public string CertificateThumbprint { get; set; } = null;

    [JsonProperty("discord_url")]
    public string DiscordUrl { get; private set; } = null;

    [JsonProperty("icon_url")]
    public string IconUrl { get; private set; } = null;

    public Image Icon { get; set; } = null;

    public bool IsUnidentified => ServerName == null;

    public bool IsLocalhost => ServerName == "localhost" && ServerIP == "127.0.0.1";

    public bool IsBancho => ServerName == "osu!bancho";

    public bool HasCertificate => !IsLocalhost && !IsUnidentified && !IsBancho;

    public int Priority
    {
      get
      {
        if (IsBancho)
          return 4;
        else if (IsFeatured)
          return 3;
        else if (IsLocalhost)
          return 1;
        return 2;
      }
    }

    public static Server UnidentifiedServer => new Server();

    public static Server BanchoServer => new Server() { ServerName = "osu!bancho", DiscordUrl = "", CertificateUrl = "", IconUrl = "https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/osu_256.png" };

    public static Server LocalhostServer => new Server() { ServerName = "localhost", ServerIP = "127.0.0.1", IconUrl = "https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/osu_256.png" };
    }
}
