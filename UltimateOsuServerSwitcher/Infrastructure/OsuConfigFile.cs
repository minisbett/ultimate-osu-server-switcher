using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Model;

namespace UltimateOsuServerSwitcher.Infrastructure
{
  public static class OsuConfigFile
  {
    /// <summary>
    /// Changes the username and password in the osu config file to the specified account
    /// </summary>
    /// <param name="account">The account with the user credentials that are being put into the file</param>
    public static void SetAccount(Account account)
    {
      // try to get the osu config file path
      string configFile = Paths.OsuConfigFile;
      if (configFile == null)
      {
        MessageBox.Show("There was a problem trying to switch your account details.\n\nPlease try to re-add your account in the Account Manager.\n\nIf you still need help, visit our Discord server.", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // get the config file content
      string[] osuConfigFile = System.IO.File.ReadAllLines(configFile);
      // replace the username and password
      for (int i = 0; i < osuConfigFile.Length; i++)
        if (osuConfigFile[i].StartsWith("Username"))
          osuConfigFile[i] = "Username = " + account.Username;
        else if (osuConfigFile[i].StartsWith("Password"))
          osuConfigFile[i] = "Password = " + account.Password;

      // Write the modified config file
      System.IO.File.WriteAllLines(configFile, osuConfigFile);
    }

    /// <summary>
    /// Tries to read the current account details from the osu config file
    /// </summary>
    /// <returns>Tuple with username or password or null if there were problems reading the config file</returns>
    public static (string username, string password) GetAccount()
    {
      // Try to get the osu config file, otherwise show error message
      string configFile = Paths.OsuConfigFile;
      if (configFile == null)
      {
        MessageBox.Show("The path to your osu config file could not be identified.\nYour osu is either not installed properly or has not been started on your Windows account yet.\n\nPlease make sure you started osu at least once and you installed it with admin privilages.\nIf not, try to start osu as an admin.\n\nIf you still need help, visit our Discord server.", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return (null, null);
      }

      // Read the config file
      string[] configLines = File.ReadAllLines(configFile);
      string username = "";
      string password = "";

      // Try to get the username and password by finding the lines that start with the keys "Username" and "Password", then split the line
      foreach (string line in configLines)
      {
        if (line.StartsWith("Username"))
          username = line.Split(" ".ToCharArray(), 3)[2];
        else if (line.StartsWith("Password"))
          password = line.Split(" ".ToCharArray(), 3)[2];
      }

      return (username, password);
    }
  }
}
