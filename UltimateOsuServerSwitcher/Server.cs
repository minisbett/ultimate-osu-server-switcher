using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    [JsonProperty("ip")]
    public string ServerIP { get; private set; } = null;

    [JsonProperty("certificate_url")]
    public string CertificateUrl { get; private set; } = null;

    public byte[] ServerCertificate => Encoding.UTF8.GetBytes(m_client.DownloadString(CertificateUrl));

    [JsonProperty("certificate_thumbprint")]
    public string CertificateThumbprint { get; private set; } = null;

    public bool IsUnidentified => ServerName == null;

    public bool IsBancho => ServerName == "osu!bancho";

    public static Server UnidentifiedServer => new Server();

    public static Server BanchoServer => new Server() { ServerName = "osu!bancho" };
  }
}
