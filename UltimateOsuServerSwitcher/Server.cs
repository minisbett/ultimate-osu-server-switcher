using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public class Server
  {

    private WebClient m_client = new WebClient();

    [JsonProperty("name")]
    public string ServerName { get; private set; } = null;

    [JsonProperty("featured")]
    public bool IsFeatured { get; private set; } = false;

    [JsonProperty("ip")]
    public string ServerIP { get; private set; } = null;

    [JsonProperty("certificate_url")]
    public string CertificateUrl { get; private set; } = null;

    public byte[] ServerCertificate => Encoding.UTF8.GetBytes(m_client.DownloadString(CertificateUrl));

    [JsonProperty("certificate_thumbprint")]
    public string CertificateThumbprint { get; private set; } = null;

    [JsonProperty("website_url")]
    public string WebsiteUrl { get; private set; } = null;

    [JsonProperty("icon_url")]
    public string IconUrl { get; private set; } = null;

    public Image Icon { get; set; } = null;

    public bool IsUnidentified => ServerName == null;

    public bool IsBancho => ServerName == "osu!bancho";

    public static Server UnidentifiedServer => new Server();

    public static Server BanchoServer => new Server() { ServerName = "osu!bancho", WebsiteUrl = "https://osu.ppy.sh", IconUrl = "https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/bancho.png?raw=true" };
  }
}
