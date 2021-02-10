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
    /// Will retry 2 times if failed
    /// </summary>
    /// <returns>The lines of the hosts file or null if an exception occured</returns>
    public static string[] GetHosts(int retry = 0)
    {
      try
      {
        return File.ReadAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts");
      }
      catch (Exception ex)
      {
        // Retry it 2 more times
        if (retry < 3)
        {
          retry++;
          return GetHosts(retry);
        }
        else
        {
          // Show the corresponding error message
          string error = "(Retry 3/3) ";

          if (ex is DirectoryNotFoundException)
          {
            error += "The hosts file could not be found. (DirectoryNotFoundException)\r\nPlease visit our discord server so we can provide help to you.";
          }
          else if (ex is IOException)
          {
            error += "Cannot open the hosts file. (IOException)\r\nPlease try to deactivate your antivurs.\r\n\r\nIf this doesn't fix your issue, please visit our discord server so we can provide help to you.";
          }
          else if (ex is UnauthorizedAccessException)
          {
            error += "Cannot access the hosts file. (UnauthorizedAccessException)\r\nPlease try to deactivate your antivirus.\r\n\r\nIf this doesn't fix your issue, please visit our discord server so we can provide help to you.";
          }
          else
          {
            error += ex.Message;
          }

          MessageBox.Show(error, "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);

          return null;
        }
      }
    }

    /// <summary>
    /// Overwrites all lines of the hosts file.
    /// Will retry 2 times if failed
    /// </summary>
    /// <param name="hosts">The lines that will be written in the hosts file</param>
    /// <returns>Bool if writing to the hosts file was successful</returns>
    public static bool SetHosts(string[] hosts) => setHosts(hosts);

    // private method to hide the retry parameter
    private static bool setHosts(string[] hosts, int retry = 0)
    {
      // Try to change the hosts file (cannot be successful due to anti virus, file lock, ...)
      try
      {
        File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
        return true;
      }
      catch (Exception ex)
      {
        // Retry it 2 more times
        if (retry < 3)
        {
          retry++;
          setHosts(hosts);
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
            error = "(Retry 3/3) Cannot access the hosts file. (UnauthorizedAccessException)\r\nPlease try to deactivate your antivirus.\r\n\r\nIf this doesn't fix your issue, please visit our discord server so we can provide help to you.";
          }
          else
          {
            error = "(Retry 3/3) " + ex.Message;
          }

          MessageBox.Show(error, "Ultimate Osu Server Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return false;
      }
    }
  }
}