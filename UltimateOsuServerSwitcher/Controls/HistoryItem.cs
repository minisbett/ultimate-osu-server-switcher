using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Model;

namespace UltimateOsuServerSwitcher.Controls
{
  public partial class HistoryItem : UserControl
  {
    public HistoryItem(HistoryElement element)
    {
      InitializeComponent();

      Server from = Switcher.Servers.First(x => x.UID == element.FromUID);
      Server to = Switcher.Servers.First(x => x.UID == element.ToUID);

      lblFrom.Text = from.ServerName;
      lblTo.Text = to.ServerName;
      lblDateTime.Text = $"{element.DateTime.ToShortDateString()} {element.DateTime.ToShortTimeString()}";
      pctrServerIconFrom.Image = from.Icon;
      pctrServerIconTo.Image = to.Icon;
    }
  }
}
