using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  // https://stackoverflow.com/questions/19147/what-is-the-correct-way-to-create-a-single-instance-wpf-application
  public class NativeMethods
  {
    /// <summary>
    /// Broadcast hwnd to send a windows message to all processes
    /// </summary>
    public const int HWND_BROADCAST = 0xffff;

    /// <summary>
    /// Message to "wake up" the running switcher instance
    /// </summary>
    public static readonly int WM_WAKEUP = RegisterWindowMessage("WM_WAKEUP");

    /// <summary>
    /// Send a message to another process
    /// </summary>
    /// <returns></returns>
    [DllImport("user32")]
    public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    /// <summary>
    /// Get a message id by registering it using win api calls
    /// </summary>
    /// <returns></returns>
    [DllImport("user32")]
    public static extern int RegisterWindowMessage(string message);
  }
}
