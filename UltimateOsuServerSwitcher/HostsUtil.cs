using System;
using System.Collections.Generic;
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

    public static void SetHosts(string[] hosts)
    {
      try
      {
        File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
      }
      catch (Exception ex)
      {
        DialogResult dr = MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        if (dr == DialogResult.Retry)
        {
          try
          {
            File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
          }
          catch
          {
            MessageBox.Show("Something went really wrong.\r\n\r\nPlease restart the switcher and try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }
  }
}
