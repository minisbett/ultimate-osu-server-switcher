using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateOsuServerSwitcher
{
  public class Settings : Dictionary<string, string>
  {
    /// <summary>
    /// The settings file of this class
    /// </summary>
    public string File { get; }

    public Settings(string file)
    {
      File = file;

      // Create the file if it doesn't exist to prevent bugs when reading from a non-existing file
      if (!System.IO.File.Exists(file))
        System.IO.File.WriteAllText(file, "");
      else
        Load();
    }

    // Handle interaction with the indexer
    public new string this[string key]
    {
      get
      {
        // Refresh dictionary
        Load();

        return base[key];
      }
      set
      { 
        // Set the value and immediately save
        base[key] = value;
        Save();
      }
    }

    /// <summary>
    ///  Bind a default value to a key to prevent uninitialized properties
    /// </summary>
    /// <param name="key">The key to initialize</param>
    /// <param name="value">The value</param>
    public void SetDefaultValue(string key, string value)
    {
      // Refresh dictionary
      Load();

      // If the variable does not exist yet, initialize and save
      if (!Keys.Contains(key))
      {
        Add(key, value);
        Save();
      }
    }

    private void Save()
    {
      // Parse the dictionary in the key=value format
      string str = "";
      foreach (var kvp in this)
        str += kvp.Key + "=" + kvp.Value + "\r\n";

      // Remove the \r\n at the end and write everything to the settings file
      str = str.Substring(0, str.Length - 2);
      System.IO.File.WriteAllText(File, str);
    }

    private void Load()
    {
      // Clear the dictionary
      Clear();

      // Read the file and get every line, remove all \r to only get the lines without the \r at the end
      string str = System.IO.File.ReadAllText(File);
      List<string> lines = str.Split('\n').Select(x => x.Replace("\r", "")).ToList();

      // Go through every line that contains a = (valid lines) and split it by that,
      // then add the parsed key and value to the dictionary
      foreach (string line in lines.Where(x => x.Contains('=')))
      {
        // Split and limit the elements to 2 to ignore all = after the first one
        string key = line.Split("=".ToCharArray(), 2)[0];
        string value = line.Split("=".ToCharArray(), 2)[1];
        // If the key already exists, throw an exception
        // Otherwise add key and value to the dictionary
        if (ContainsKey(key))
          throw new InvalidOperationException("Tried to set the key '" + key + "' but it was already set.\r\n(Double occurence of the key in the settings file)");
        else
          Add(key, value);
      }
    }
  }
}
