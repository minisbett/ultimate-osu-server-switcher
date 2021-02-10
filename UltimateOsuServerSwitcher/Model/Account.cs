using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher.Model
{
  public class Account
  {
    /// <summary>
    /// UID of the server the account is binded to
    /// </summary>
    [JsonProperty("serveruid")]
    public string ServerUID { get; private set; } = null;
    /// <summary>
    /// Username of the saved user
    /// </summary>
    [JsonProperty("username")]
    public string Username { get; private set; } = null;

    /// <summary>
    /// plaintext password of the saved user
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; private set; } = null;

    public Account(string serveruid, string username, string password)
    {
      ServerUID = serveruid;
      Username = username;
      Password = password;
    }
  }
}
