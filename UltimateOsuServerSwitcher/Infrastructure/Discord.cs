using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class Discord
  {

    // The discord RPC client used for the rich presence
    // given the app id (see developer portal) and -1 as the pipe for automatic pipe scan
    private static DiscordRpcClient m_client = new DiscordRpcClient("770355757622755379", -1);


    // Current RichPresence object
    private static RichPresence m_richPresence = null;

    // Determines if a presence is currently being shown
    public static bool IsPrecenseSet { get; private set; } = false;

    public static Server ShownServer { get; private set; } = null;

    public static void SetPresenceServer(Server server)
    {
      if (!m_client.IsInitialized)
        m_client.Initialize();

      if (!IsPrecenseSet)
      {
        m_richPresence = new RichPresence();
        m_richPresence.Timestamps = new Timestamps(DateTime.UtcNow);
        m_richPresence.State = $"Playing on {server.ServerName}";
        m_richPresence.Details = "Ultimate Osu Server Switcher";
        m_richPresence.Assets = new Assets() { LargeImageKey = "osu", LargeImageText = "Download on GitHub!\r\nminisbett/ultimate-osu-server-switcher" };
        m_client.SetPresence(m_richPresence);

        IsPrecenseSet = true;
      }
      else
      {
        m_richPresence.State = "Currently playing on: " + server.ServerName;
        m_client.SetPresence(m_richPresence);
      }

      ShownServer = server;
    }

    public static void RemovePresence()
    {
      if (IsPrecenseSet)
      {
        m_client.ClearPresence();
        IsPrecenseSet = false;
        ShownServer = null;
      }
    }
  }
}
