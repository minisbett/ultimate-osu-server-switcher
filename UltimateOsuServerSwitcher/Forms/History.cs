using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Controls;
using UltimateOsuServerSwitcher.Infrastructure;
using UltimateOsuServerSwitcher.Model;

namespace UltimateOsuServerSwitcher.Forms
{
  public partial class History : CustomForm
  {

    private static Settings m_history = new Settings(Paths.HistoryFile);
    
    public History()
    {
      InitializeComponent();

      HistoryElement[] history = JsonConvert.DeserializeObject<HistoryElement[]>(m_history["history"]);
      foreach (HistoryElement element in history.Reverse())
      {
        flowLayoutPanel.Controls.Add(new HistoryItem(element));
      }

      long size = new FileInfo(Paths.HistoryFile).Length;

      if (size < 1024)
        lblSize.Text = $"Disk usage: {size} B";
      else if (size < 1024 * 1024)
        lblSize.Text = $"Disk usage: {Math.Round(size / 1024d, 1).ToString(new CultureInfo("en-US"))} KB";
      else if (size < 1024 * 1024 * 1024)
        lblSize.Text = $"Disk usage: {Math.Round(size / 1024 / 1024d, 1).ToString(new CultureInfo("en-US"))} MB";
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      Close();
    }
    private void BorderlessDragMouseDown(object sender, MouseEventArgs e)
    {
      // Make borderless window moveable
      if (e.Button == MouseButtons.Left)
      {
        WinApi.ReleaseCapture();
        WinApi.SendMessage(Handle, 0xA1, 0x2, 0);
      }
    }

    private void btnClearHistory_Click(object sender, EventArgs e)
    {
      m_history["history"] = JsonConvert.SerializeObject(new HistoryElement[] { });
      flowLayoutPanel.Controls.Clear();

      long size = new FileInfo(Paths.HistoryFile).Length;
      lblSize.Text = $"Disk usage: {size} B";
    }
  }
}
