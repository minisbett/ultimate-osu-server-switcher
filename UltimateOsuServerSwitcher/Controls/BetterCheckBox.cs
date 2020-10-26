using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  public class BetterCheckBox : CheckBox
  {
    protected override void OnPaint(PaintEventArgs pevent)
    {
      Color color = Color.DarkRed;
      if (Checked)
        color = Color.LimeGreen;
      if (!Enabled)
        color = Color.Gray;

      Graphics g = pevent.Graphics;
      g.FillRectangle(new SolidBrush(BackColor), pevent.ClipRectangle);

      RectangleF area = new RectangleF(Height * 0.07f, Height * 0.17f, Height * 0.54f, Height * 0.54f);

      g.FillRectangle(new SolidBrush(color), area);
      SizeF size = g.MeasureString(Text, Font);
      g.DrawString(Text, Font, new SolidBrush(ForeColor), Height * 0.70f, Height / 2 - size.Height / 2 + 1, StringFormat.GenericDefault);
    }
  }
}
