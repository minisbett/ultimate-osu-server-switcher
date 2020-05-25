using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UltimateOsuServerSwitcher
{
  public class ProcessWatcher
  {

    public Process Process { get; set; }

    public Predicate<Process> Condition { get; }

    private Thread thread;

    public delegate void ProcessStateChangedHandler(object sender, ProcessStateChangedEventArgs e);
    public event ProcessStateChangedHandler ProcessStateChanged;

    public ProcessWatcher(Predicate<Process> condition)
    {
      Condition = condition;

      thread = new Thread(watcher);
    }

    public void Start()
    {
      thread.Start();
    }

    public void Stop()
    {
      thread.Abort();
    }

    private void watcher()
    {
      while (true)
      {
        Process[] processes = Process.GetProcesses().Where(x => Condition.Invoke(x)).ToArray();
        Process before = Process;
        Process = processes.FirstOrDefault();

        if ((before == null ^ Process == null) || (before != null && Process != null && before.MainWindowTitle != Process.MainWindowTitle))
          ProcessStateChanged?.Invoke(this, new ProcessStateChangedEventArgs(Process));
      }
    }
  }

  public class ProcessStateChangedEventArgs : EventArgs
  {

    public Process Process { get; }

    public bool Running { get; }

    public ProcessStateChangedEventArgs(Process process)
    {
      Process = process;
      Running = Process != null;
    }
  }
}