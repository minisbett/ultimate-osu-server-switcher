using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public static class Urls
  {
    /// <summary>
    /// Url to the json file with all mirrors
    /// </summary>
    public static string Mirrors => "https://raw.githubusercontent.com/MinisBett/ultimate-osu-server-switcher/master/datav2/mirrors.json";

    /// <summary>
    /// Url to the github repository of the switcher
    /// </summary>
    public static string Repository => "https://github.com/minisbett/ultimate-osu-server-switcher";

    /// <summary>
    /// Url to the discord page on the github page, which redirects to the discord invite url
    /// </summary>
    public static string Discord => "https://minisbett.github.io/ultimate-osu-server-switcher/discord.html";

    /// <summary>
    /// Url to a video from discord about discord rich presence for explaination
    /// </summary>
    public static string RichPresenceExplanation => "https://www.youtube.com/watch?v=Ss-IvNjl7JQ";
  }
}
