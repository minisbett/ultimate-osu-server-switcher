using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  static class Program
  {

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    
    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Process current = Process.GetCurrentProcess();
      Process[] instances = Process.GetProcessesByName(current.ProcessName).Where(x => x.Id != current.Id).ToArray();
      if(instances.Any())
        return;

      if(args.Length == 1)
      {
        QuickSwitch.Switch(args[0]);
        return;
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
