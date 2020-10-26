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
    // Dll import to send a message to the current running instance if a second is started
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // https://stackoverflow.com/questions/19147/what-is-the-correct-way-to-create-a-single-instance-wpf-application
      
      // Create mutex for single instance checks
      Mutex mutex = new Mutex(true, "UltimateOsuServerSwitcher");

      // Check if the mutex is owned by another process
      if (mutex.WaitOne(TimeSpan.Zero, true))
      {

        // Fix https://github.com/MinisBett/ultimate-osu-server-switcher/issues/9
        // (Windows 7 only)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
        NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_WAKEUP, IntPtr.Zero, IntPtr.Zero);
      }
    }
  }
}
