using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher.Controls
{
  class ImageLoadingBar : Control
  {
    public int Maximum { get; set; } = 100;

    private int m_value = 0;
    public int Value
    {
      get => m_value;
      set
      {
        m_value = value;
        Refresh();
      }
    }

    public Image UnloadedImage { get; set; }

    public Image LoadedImage { get; set; }

    public ImageLoadingBar()
    {
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      DoubleBuffered = true;
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (UnloadedImage == null || LoadedImage == null)
        return;

      Image loadedImage = new Bitmap(Width, Height);
      Image unloadedImage = new Bitmap(Width, Height);

      using (Graphics _g = Graphics.FromImage(loadedImage))
        _g.DrawImage(LoadedImage, 0, 0, Width, Height);

      using (Graphics _g = Graphics.FromImage(unloadedImage))
        _g.DrawImage(UnloadedImage, 0, 0, Width, Height);

      Graphics g = e.Graphics;
      g.DrawImage(unloadedImage, 0, 0, Width, Height);

      double factor = (double)Value / Maximum;
      int height = (int)(Height * factor);
      Rectangle rect = new Rectangle(0, Height - height, Width, height);

      if (height == 0)
        return;

      Image snippet = new Bitmap(rect.Width, rect.Height);
      using (Graphics _g = Graphics.FromImage(snippet))
        _g.DrawImage(loadedImage, 0, 0, rect, GraphicsUnit.Pixel);

      g.DrawImage(snippet, rect.X, rect.Y, rect.Width, rect.Height);
    }
  }
}
