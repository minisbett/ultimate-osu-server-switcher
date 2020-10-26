using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  static class Program
  {
    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // Check if the program was started from a QuickSwitch Shortcut
      if (args.Length == 1)
      {
        MessageBox.Show("QuickSwitch to " + args[0]);
        return;
      }

      // Fix https://github.com/MinisBett/ultimate-osu-server-switcher/issues/9
      // (Windows 7 only)
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


      // https://stackoverflow.com/questions/19147/what-is-the-correct-way-to-create-a-single-instance-wpf-application
      // Create mutex for single instance checks
      Mutex mutex = new Mutex(true, "UltimateOsuServerSwitcher");

      // Check if the mutex is owned by another process
      if (mutex.WaitOne(TimeSpan.Zero, true))
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());

        // Release the mutex for clean up
        mutex.ReleaseMutex();
      }
      else
      {
        // If the mutex is owned by another process (a switcher instance is already running)
        // sent the WM_WAKEUP message to all processes to put the current instance in the foreground
        WinApi.SendMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_WAKEUP, 0, 0);
      }
    }
  }
}
