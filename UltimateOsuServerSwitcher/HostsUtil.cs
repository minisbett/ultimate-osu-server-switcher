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
    public static string[] GetHosts()
    {
      return File.ReadAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts");
    }

    public static void SetHosts(string[] hosts, int retry = 0)
    {
      try
      {
        File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
      }
      catch (Exception ex)
      {
        if (retry < 3) // Reply twice
        {
          SetHosts(hosts, retry++);
        }
        else
        {
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