using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  public static class HostsUtil
  {
    /// <summary>
    /// Reads all lines of the hosts file and returns them in a string array
    /// </summary>
    /// <returns>The lines of the hosts file</returns>
    public static string[] GetHosts()
    {
      return File.ReadAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts");
    }

    /// <summary>
    /// Overwrites all lines of the hosts file.
    /// Will retry 2 times if failed
    /// </summary>
    /// <param name="hosts">The lines that will be written in the hosts file</param>
    /// <param name="retry">(Internal use only) the amount of retries that are already done</param>
    public static void SetHosts(string[] hosts, int retry = 0)
    {
      // Try to change the hosts file (cannot be successful due to anti virus, file lock, ...)
      try
      {
        File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
      }
      catch (Exception ex)
      {
        // Retry it 2 more times
        if (retry < 3)
        {
          SetHosts(hosts, retry++);
        }
        else
        {
          // Show the corresponding error message
          string error = "";

          if (ex is DirectoryNotFoundException)
          {
            error = "The hosts file could not be found. (DirectoryNotFoundException)\r\nPlease visit our discord server so we can provide help to you.";
          }
          else if(ex is IOException)
          {
            error = "(Retry 3/3) Cannot open the hosts file. (IOException)\r\nPlease try to deactivate your antivurs.\r\n\r\nIf this doesn't fix your issue, please visit our discord server so we can provide help to you.";
          }
          else if(ex is UnauthorizedAccessException)
          {
            error = "(Retry 3/3) Cannot access the hosts file. (UnauthorizedAccessException)\r\nPlease try to deactivate your antivurs.\r\n\r\nIf this doesn't fix your issue, please visit our discord server so we can provide help to you.";
          }
          else
          {
            error = "(Retry 3/3) " + ex.Message;
          }

          MessageBox.Show(error, "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
  }
}