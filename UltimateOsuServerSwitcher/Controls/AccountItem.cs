using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateOsuServerSwitcher.Model;

namespace UltimateOsuServerSwitcher.Controls
{
  public partial class AccountItem : UserControl
  {

    public delegate void ClickHandler(object sender, EventArgs e);
    public event ClickHandler ItemClicked;

    private bool m_selected = false;
    private bool m_hovering = false;

    public Account Account { get; }

    public AccountItem(Account account)
    {
      InitializeComponent();

      // Apply the account informations to the UI
      pctrServerIcon.Image = Switcher.Servers.First(x => x.UID == account.ServerUID).Icon;
      lblServername.Text = Switcher.Servers.First(x => x.UID == account.ServerUID).ServerName;
      lblUsername.Text = account.Username;

      // Make the account this item is binded to available to the account manager for later use
      Account = account;
    }

    private void Global_MouseEnter(object sender, EventArgs e)
    {
      // Temporarily change the background of the item when hovering on it with the mouse
      // but only do this when the item is not selected as the selection background color is brighter than the hovering one
      if (!m_selected)
        BackColor = Color.FromArgb(31, 31, 31);

      // Change the hovering state
      m_hovering = true;
    }

    private void Global_MouseLeave(object sender, EventArgs e)
    {
      // Change the color back to the default when leaving the item with the mouse
      // but only do this when the item is not selected; If it is selected, the background should always be highlighted
      if (!m_selected)
        BackColor = Color.FromArgb(10, 10, 10);

      // Change the hovering state
      m_hovering = false;
    }

    private void Global_Click(object sender, EventArgs e)
    {
      // Trigger the custom item click event to pass the click to the account manager form
      ItemClicked?.Invoke(this, EventArgs.Empty);
    }

    public new void Select()
    {
      // Visually show the selection by highlighting the background
      BackColor = Color.FromArgb(45, 45, 45);

      // Change the selected state
      m_selected = true;
    }

    public void Unselect()
    {
      // Set the background back to the default color
      // but only if the mouse is not hovering on the item; Otherwise the highlighting would stop even though the mouse is still hovering on it
      if (!m_hovering)
        BackColor = Color.FromArgb(10, 10, 10);

      // Change the selected state
      m_selected = false;
    }
  }
}
