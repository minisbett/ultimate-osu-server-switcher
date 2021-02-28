using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Controls;
using UltimateOsuServerSwitcher.Infrastructure;
using UltimateOsuServerSwitcher.Model;
using UltimateOsuServerSwitcher.Utils;

namespace UltimateOsuServerSwitcher.Forms
{
  public partial class AccountManager : CustomForm
  {
    // The settings instance for the saved osu accounts
    private Settings m_accounts = new Settings(Paths.AccountsFile);

    // The settings instance for the temp variables
    private Settings m_temp = new Settings(Paths.TempFile);
    
    // Currently selected item
    private AccountItem m_selected = null;

    public AccountManager()
    {
      InitializeComponent();

      // Initialize UI variable to show the how to add message once when the user started the account manager for the first time
      m_temp.SetDefaultValue("howtoadd", "false");

      // Load the saved accounts
      List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(m_accounts["accounts"]);
      foreach (Account account in accounts)
        AddAccountItem(account);
    }

    private void AddAccountItem(Account account)
    {
      // Create an account item based on the specified account
      AccountItem item = new AccountItem(account);

      // Register the custom click event to handle selection
      item.ItemClicked += (sender, e) =>
      {
        // Either unselect the item if its already selected or select it
        if (m_selected == sender)
        {
          // Unselect the item
          m_selected.Unselect();

          // set the selected item to null
          m_selected = null;

        }
        else
        {
          // Unselect the currently selected item if not null
          m_selected?.Unselect();

          // Change the selected item and select it
          m_selected = sender as AccountItem;
          m_selected.Select();
        }

        // Update the UI to apply selection change
        btnRemoveAccount.Enabled = m_selected != null;
      };

      // Add the item control to the flow layout panel
      flwpnlAccounts.Controls.Add(item);
    }

    private void BorderlessDragMouseDown(object sender, MouseEventArgs e)
    {
      // Make borderless window moveable
      if (e.Button == MouseButtons.Left)
      {
        WinApi.ReleaseCapture();
        WinApi.SendMessage(Handle, 0xA1, 0x2, 0);
      }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      // Close account manager when clicking on the X
      Close();
    }

    private void btnRemoveAccount_Click(object sender, EventArgs e)
    {
      // Get all saved accounts
      List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(m_accounts["accounts"]);

      // Remove the selected account from the list of saved accounts
      // uid comparison is needed because account list above does not contain same class instances as the account items
      // and as there is only 1 account per server uid this pretty much does the job
      accounts.RemoveAll(x => x.ServerUID == m_selected.Account.ServerUID);

      // Save the edited list of saved accounts
      m_accounts["accounts"] = JsonConvert.SerializeObject(accounts);

      // Remove the account item from the flow layout panel
      flwpnlAccounts.Controls.Remove(m_selected);

      // Set the current selected item to null as the selected item got removed
      m_selected = null;

      // Update UI to apply selection change
      btnRemoveAccount.Enabled = false;
    }

    private void btnAddAccount_Click(object sender, EventArgs e)
    {
      // Get the current server
      Server server = Switcher.GetCurrentServer();

      // Check if the current server can be identified; If not, you cannot save account data for that
      if (server.IsUnidentified)
      {
        MessageBox.Show("You are currently connected to an unknown server.\n\nYou can only save account data for servers that are featured in the switcher.\n\nTo add an account to the switcher, please follow these steps:\n\n1. Connect to the server you would like to save your account data from.\n2. In osu, login with that account on the server.\n3. Close osu to force it writing the account data in the config file\n4. Click on 'Add Account in the switcher.", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // Check if you already have an account saved for the current connected server
      List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(m_accounts["accounts"]);
      if(accounts.Any(x => x.ServerUID == server.UID))
      {
        MessageBox.Show($"You already have account data saved for the server you are currently connected to. ({server.ServerName})\n\nIf you want to update your data, remove the old one by selecting it and clicking on 'Remove Account", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // get the account details from the osu config file
      (string username, string password) = OsuConfigFile.GetAccount();
      // GetAccount() returns null if there was an error. If so, just return, the GetAccount() method already shows an error message
      if (username == null || password == null)
        return;

      // Check if the username and password could both be extracted from the config file. Otherwise show error message
      if(username == "" || password == "")
      {
        MessageBox.Show("Could not find the username and password in your osu config file.\nPlease make sure you connected to the server which account data you want to save and you logged in successfully.", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      // Create an account instance and add it to the list, then save the json
      Account account = new Account(server.UID, username, password);
      accounts.Add(account);
      m_accounts["accounts"] = JsonConvert.SerializeObject(accounts);

      // Add an account item to the UI
      AddAccountItem(account);

      // Display informations about the added account
      MessageBox.Show($"Your account was successfully added.\n\nServer: {server.ServerName}\nUsername: {username}", "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void lnklblHowToAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Show a help how to add a server
      MessageBox.Show("Follow these instructions to add your account.\n\n1. Connect to the server you would like to save your account data from.\n2. In osu, login with that account on the server.\n3. Close osu to force it writing the account data in the config file\n4. Click on 'Add Account' in the switcher.\nThe switcher will automatically get your data from the config file.\n\nNote: Your saved accounts are NOT stored on a server. They are securely saved on your computer.", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void AccountManager_Load(object sender, EventArgs e)
    {
      // Wait till UI is loaded
      await Task.Delay(1);

      // Show the how to add message if user haven't seen it yet
      if(m_temp["howtoadd"] == "false")
      {
        // Set to true to not show again and trigger the linkclicked event of the "how to add" linklabel
        m_temp["howtoadd"] = "true";
        lnklblHowToAdd_LinkClicked(lnklblHowToAdd, null);
      }
    }
  }
}
