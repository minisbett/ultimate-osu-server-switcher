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
  public static class WebHelper
  {
    // The web client used for every connected in this class
    private static WebClient m_client = new WebClient();

    /// <summary>
    /// Download an image from a given url
    /// </summary>
    /// <param name="url">The given url to the image</param>
    /// <returns>The downloaded image</returns>
    public static async Task<Image> DownloadImageAsync(string url)
    {
      //using (Stream stream = m_client.OpenRead(url))
      //  return Image.FromStream(stream);
      // Download the bytes to the image async and convert it to an image using a memory stream
      using (MemoryStream stream = new MemoryStream(await DownloadBytesAsync(url)))
        return Image.FromStream(stream);
    }

    /// <summary>
    /// Downloads the bytes of a given url
    /// </summary>
    /// <param name="url">The given url</param>
    /// <returns>The byte array</returns>
    public static async Task<byte[]> DownloadBytesAsync(string url)
    {
      // Download the bytes async and return them
      var bytes = await m_client.DownloadDataTaskAsync(url);
      return bytes;
    }

    /// <summary>
    /// Downloads the string of a given url
    /// </summary>
    /// <param name="url">The given url</param>
    /// <returns>The string</returns>
    public static async Task<string> DownloadStringAsync(string url)
    {
      // Download the string and return it
      var str = await m_client.DownloadStringTaskAsync(url);
      return str;
    }
  }
}
