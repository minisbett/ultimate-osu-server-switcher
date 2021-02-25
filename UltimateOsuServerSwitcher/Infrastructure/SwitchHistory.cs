using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateOsuServerSwitcher.Model;

namespace UltimateOsuServerSwitcher.Infrastructure
{

  public static class SwitchHistory
  {

    private static Settings m_history = new Settings(Paths.HistoryFile);

    private static int m_maximum = 100;

    public static HistoryElement[] GetHistory() => JsonConvert.DeserializeObject<HistoryElement[]>(m_history["history"]);

    public static void AddToHistory(Server from, Server to)
    {
      List<HistoryElement> history = GetHistory().ToList();

      while (history.Count >= m_maximum)
        history.RemoveAt(0);

      history.Add(new HistoryElement(DateTime.Now, from.UID, to.UID));

      string json = JsonConvert.SerializeObject(history.ToArray());
      m_history["history"] = json;
    }
  }
}
