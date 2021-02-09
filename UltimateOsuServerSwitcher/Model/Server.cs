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
    /// <summary>
    /// A unique id to identify the server
    /// </summary>
    public string UID { get; set; } = null;

    /// <summary>
    /// The name of the server
    /// </summary>
    [JsonProperty("name")]
    public string ServerName { get; private set; } = null;

    /// <summary>
    /// Determines whether the server is featured or not
    /// </summary>
    public bool IsFeatured { get; set; } = false;

    /// <summary>
    /// The IP-Address of the server
    /// </summary>
    [JsonProperty("ip")]
    public string IP { get; private set; } = null;

    /// <summary>
    /// The url to the certificate
    /// </summary>
    [JsonProperty("certificate_url")]
    public string CertificateUrl { get; private set; } = null;

    /// <summary>
    /// The certificate
    /// </summary>
    public byte[] Certificate { get; set; } = null;

    /// <summary>
    /// The thumbprint of the certificate
    /// </summary>
    public string CertificateThumbprint { get; set; } = null;

    /// <summary>
    /// The url to the discord server
    /// </summary>
    [JsonProperty("discord_url")]
    public string DiscordUrl { get; private set; } = null;

    /// <summary>
    /// The url of the server's icon
    /// </summary>
    [JsonProperty("icon_url")]
    public string IconUrl { get; private set; } = null;

    /// <summary>
    /// The icon
    /// </summary>
    public Image Icon { get; set; } = null;

    /// <summary>
    /// Determines whether the server was identified (is in the database) or not
    /// </summary>
    public bool IsUnidentified => UID == null;

    /// <summary>
    /// Determines whether the server is localhost or not
    /// </summary>
    public bool IsLocalhost => UID == "localhost";

    /// <summary>
    /// Determines whether the server is the official osu!bancho server or not
    /// </summary>
    public bool IsBancho => UID == "bancho";

    /// <summary>
    /// Determines whether the server should a have certificate in theory or not
    /// </summary>
    public bool HasCertificate => !IsLocalhost && !IsUnidentified && !IsBancho;

    /// <summary>
    /// The priority of the server
    /// </summary>
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

    /// <summary>
    /// An unidentified server object
    /// </summary>
    public static Server UnidentifiedServer => new Server();

    /// <summary>
    /// A server object that represents the bancho server
    /// </summary>
    public static Server BanchoServer => new Server() { UID = "bancho", ServerName = "osu!bancho", DiscordUrl = "", CertificateUrl = "", IconUrl = "https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/osu_256.png" };

    /// <summary>
    /// A server object that represents a localhost server
    /// </summary>
    public static Server LocalhostServer => new Server() { UID = "localhost", ServerName = "localhost (for development purposes)", IP = "127.0.0.1", IconUrl = "https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/osu_256.png" };

    /// <summary>
    /// Returns an array with new instances of all static/hardcoded servers
    /// </summary>
    public static Server[] StaticServers => new Server[] { BanchoServer, LocalhostServer };
  }
}
