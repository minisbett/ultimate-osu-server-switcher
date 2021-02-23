using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher.Forms
{
  public class CustomForm : Form
  {
    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams cp = base.CreateParams;
        cp.ClassStyle |= 0x20000;
        return cp;
      }
    }
  }
}
