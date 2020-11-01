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
    /// <summary>
    /// The maximum value of the progress
    /// </summary>
    public int Maximum { get; set; } = 100;

    private int m_value = 0;
    /// <summary>
    /// The current value of the progress
    /// </summary>
    public int Value
    {
      get => m_value;
      set
      {
        m_value = value;
        Refresh();
      }
    }

    /// <summary>
    /// The image used for the unloaded area
    /// </summary>
    public Image UnloadedImage { get; set; }

    /// <summary>
    /// The image used for the loaded area
    /// </summary>
    public Image LoadedImage { get; set; }

    public ImageLoadingBar()
    {
      // Prevent flimmering by using double buffer and changing some style settings
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      DoubleBuffered = true;
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      // Check if the image is set (prevent exception when placing control in editor because no image is set then)
      if (UnloadedImage == null || LoadedImage == null)
        return;

      // Resize both images to the size to the control
      Image loadedImage = new Bitmap(Width, Height);
      Image unloadedImage = new Bitmap(Width, Height);

      using (Graphics _g = Graphics.FromImage(loadedImage))
        _g.DrawImage(LoadedImage, 0, 0, Width, Height);

      using (Graphics _g = Graphics.FromImage(unloadedImage))
        _g.DrawImage(UnloadedImage, 0, 0, Width, Height);

      // Draw the full unloaded image on the control (will be overdrawn later)
      Graphics g = e.Graphics;
      g.DrawImage(unloadedImage, 0, 0, Width, Height);

      // Calculate how high the other image must be drawn  (control will fill from bottom to top)
      double factor = (double)Value / Maximum;
      int height = (int)(Height * factor);
      // Rectangle that specifies where the loaded image goes on the control
      Rectangle rect = new Rectangle(0, Height - height, Width, height);

      // If nothing is loaded yet (factor = 0 => height = 0) dont draw
      if (height == 0)
        return;

      // Get the snippet from the bottom of the loaded image (control will fill from bottom to top)
      Image snippet = new Bitmap(rect.Width, rect.Height);
      using (Graphics _g = Graphics.FromImage(snippet))
        _g.DrawImage(loadedImage, 0, 0, rect, GraphicsUnit.Pixel);

      // Draw the snippet of the loaded image
      g.DrawImage(snippet, rect.X, rect.Y, rect.Width, rect.Height);
    }
  }
}
