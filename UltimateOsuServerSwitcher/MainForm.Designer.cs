namespace UltimateOsuServerSwitcher
{
  partial class MainForm
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.tbcntrlMain = new MetroFramework.Controls.MetroTabControl();
      this.tbpgSwitcher = new MetroFramework.Controls.MetroTabPage();
      this.lblCreateShortcut = new MetroFramework.Controls.MetroLabel();
      this.pctrbxServerIcon = new System.Windows.Forms.PictureBox();
      this.cmbbxServer = new MetroFramework.Controls.MetroComboBox();
      this.lblOsuRunning = new MetroFramework.Controls.MetroLabel();
      this.lblCurrentServer = new MetroFramework.Controls.MetroLabel();
      this.btnConnect = new MetroFramework.Controls.MetroButton();
      this.tbpgAbout = new MetroFramework.Controls.MetroTabPage();
      this.lblClearIconCache = new MetroFramework.Controls.MetroLabel();
      this.lblDiscord = new MetroFramework.Controls.MetroLabel();
      this.lblGithub = new MetroFramework.Controls.MetroLabel();
      this.lblDarkMode = new MetroFramework.Controls.MetroLabel();
      this.tgglDarkTheme = new MetroFramework.Controls.MetroToggle();
      this.lblAbout = new MetroFramework.Controls.MetroLabel();
      this.lblTitle = new MetroFramework.Controls.MetroLabel();
      this.styleManager = new MetroFramework.Components.MetroStyleManager(this.components);
      this.osuWatcher = new System.Windows.Forms.Timer(this.components);
      this.btnUpdateAvailable = new MetroFramework.Controls.MetroButton();
      this.tbcntrlMain.SuspendLayout();
      this.tbpgSwitcher.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrbxServerIcon)).BeginInit();
      this.tbpgAbout.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.styleManager)).BeginInit();
      this.SuspendLayout();
      // 
      // tbcntrlMain
      // 
      this.tbcntrlMain.Controls.Add(this.tbpgSwitcher);
      this.tbcntrlMain.Controls.Add(this.tbpgAbout);
      this.tbcntrlMain.Location = new System.Drawing.Point(0, 26);
      this.tbcntrlMain.Name = "tbcntrlMain";
      this.tbcntrlMain.SelectedIndex = 0;
      this.tbcntrlMain.Size = new System.Drawing.Size(424, 185);
      this.tbcntrlMain.TabIndex = 0;
      // 
      // tbpgSwitcher
      // 
      this.tbpgSwitcher.Controls.Add(this.lblCreateShortcut);
      this.tbpgSwitcher.Controls.Add(this.pctrbxServerIcon);
      this.tbpgSwitcher.Controls.Add(this.cmbbxServer);
      this.tbpgSwitcher.Controls.Add(this.lblOsuRunning);
      this.tbpgSwitcher.Controls.Add(this.lblCurrentServer);
      this.tbpgSwitcher.Controls.Add(this.btnConnect);
      this.tbpgSwitcher.HorizontalScrollbarBarColor = true;
      this.tbpgSwitcher.Location = new System.Drawing.Point(4, 35);
      this.tbpgSwitcher.Name = "tbpgSwitcher";
      this.tbpgSwitcher.Size = new System.Drawing.Size(416, 146);
      this.tbpgSwitcher.TabIndex = 0;
      this.tbpgSwitcher.Text = "Server Switcher";
      this.tbpgSwitcher.VerticalScrollbarBarColor = true;
      // 
      // lblCreateShortcut
      // 
      this.lblCreateShortcut.AutoSize = true;
      this.lblCreateShortcut.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblCreateShortcut.Location = new System.Drawing.Point(3, 127);
      this.lblCreateShortcut.Name = "lblCreateShortcut";
      this.lblCreateShortcut.Size = new System.Drawing.Size(101, 19);
      this.lblCreateShortcut.TabIndex = 7;
      this.lblCreateShortcut.Text = "Create Shortcut";
      this.lblCreateShortcut.Visible = false;
      this.lblCreateShortcut.Click += new System.EventHandler(this.lblCreateShortcut_Click);
      // 
      // pctrbxServerIcon
      // 
      this.pctrbxServerIcon.BackColor = System.Drawing.Color.Transparent;
      this.pctrbxServerIcon.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pctrbxServerIcon.Location = new System.Drawing.Point(365, 95);
      this.pctrbxServerIcon.Name = "pctrbxServerIcon";
      this.pctrbxServerIcon.Size = new System.Drawing.Size(48, 48);
      this.pctrbxServerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrbxServerIcon.TabIndex = 6;
      this.pctrbxServerIcon.TabStop = false;
      this.pctrbxServerIcon.Click += new System.EventHandler(this.pctrbxServerIcon_Click);
      // 
      // cmbbxServer
      // 
      this.cmbbxServer.FormattingEnabled = true;
      this.cmbbxServer.ItemHeight = 23;
      this.cmbbxServer.Location = new System.Drawing.Point(106, 49);
      this.cmbbxServer.Name = "cmbbxServer";
      this.cmbbxServer.Size = new System.Drawing.Size(212, 29);
      this.cmbbxServer.TabIndex = 2;
      this.cmbbxServer.SelectedIndexChanged += new System.EventHandler(this.CmbbxServer_SelectedIndexChanged);
      // 
      // lblOsuRunning
      // 
      this.lblOsuRunning.ForeColor = System.Drawing.Color.Red;
      this.lblOsuRunning.Location = new System.Drawing.Point(19, 80);
      this.lblOsuRunning.Name = "lblOsuRunning";
      this.lblOsuRunning.Size = new System.Drawing.Size(379, 20);
      this.lblOsuRunning.TabIndex = 5;
      this.lblOsuRunning.Text = "osu! is currently running and will be restarted.";
      this.lblOsuRunning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblOsuRunning.Visible = false;
      // 
      // lblCurrentServer
      // 
      this.lblCurrentServer.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblCurrentServer.FontSize = MetroFramework.MetroLabelSize.Tall;
      this.lblCurrentServer.Location = new System.Drawing.Point(0, 0);
      this.lblCurrentServer.Name = "lblCurrentServer";
      this.lblCurrentServer.Size = new System.Drawing.Size(416, 37);
      this.lblCurrentServer.TabIndex = 4;
      this.lblCurrentServer.Text = "Fetching server data...";
      this.lblCurrentServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // btnConnect
      // 
      this.btnConnect.Enabled = false;
      this.btnConnect.Location = new System.Drawing.Point(147, 105);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(122, 29);
      this.btnConnect.TabIndex = 3;
      this.btnConnect.Text = "Connect";
      this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
      // 
      // tbpgAbout
      // 
      this.tbpgAbout.Controls.Add(this.lblClearIconCache);
      this.tbpgAbout.Controls.Add(this.lblDiscord);
      this.tbpgAbout.Controls.Add(this.lblGithub);
      this.tbpgAbout.Controls.Add(this.lblDarkMode);
      this.tbpgAbout.Controls.Add(this.tgglDarkTheme);
      this.tbpgAbout.Controls.Add(this.lblAbout);
      this.tbpgAbout.HorizontalScrollbarBarColor = true;
      this.tbpgAbout.Location = new System.Drawing.Point(4, 35);
      this.tbpgAbout.Name = "tbpgAbout";
      this.tbpgAbout.Size = new System.Drawing.Size(416, 146);
      this.tbpgAbout.TabIndex = 1;
      this.tbpgAbout.Text = "About";
      this.tbpgAbout.VerticalScrollbarBarColor = true;
      // 
      // lblClearIconCache
      // 
      this.lblClearIconCache.AutoSize = true;
      this.lblClearIconCache.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblClearIconCache.Enabled = false;
      this.lblClearIconCache.Location = new System.Drawing.Point(0, 127);
      this.lblClearIconCache.Name = "lblClearIconCache";
      this.lblClearIconCache.Size = new System.Drawing.Size(105, 19);
      this.lblClearIconCache.TabIndex = 8;
      this.lblClearIconCache.Text = "Clear icon cache";
      this.lblClearIconCache.Click += new System.EventHandler(this.lblClearIconCache_Click);
      // 
      // lblDiscord
      // 
      this.lblDiscord.AutoSize = true;
      this.lblDiscord.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblDiscord.CustomForeColor = true;
      this.lblDiscord.ForeColor = System.Drawing.Color.Blue;
      this.lblDiscord.Location = new System.Drawing.Point(146, 128);
      this.lblDiscord.Name = "lblDiscord";
      this.lblDiscord.Size = new System.Drawing.Size(53, 19);
      this.lblDiscord.TabIndex = 7;
      this.lblDiscord.Text = "Discord";
      this.lblDiscord.Click += new System.EventHandler(this.lblDiscord_Click);
      // 
      // lblGithub
      // 
      this.lblGithub.AutoSize = true;
      this.lblGithub.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblGithub.CustomForeColor = true;
      this.lblGithub.ForeColor = System.Drawing.Color.ForestGreen;
      this.lblGithub.Location = new System.Drawing.Point(205, 128);
      this.lblGithub.Name = "lblGithub";
      this.lblGithub.Size = new System.Drawing.Size(49, 19);
      this.lblGithub.TabIndex = 6;
      this.lblGithub.Text = "GitHub";
      this.lblGithub.Click += new System.EventHandler(this.LblGithub_Click);
      // 
      // lblDarkMode
      // 
      this.lblDarkMode.AutoSize = true;
      this.lblDarkMode.FontSize = MetroFramework.MetroLabelSize.Small;
      this.lblDarkMode.Location = new System.Drawing.Point(294, 128);
      this.lblDarkMode.Name = "lblDarkMode";
      this.lblDarkMode.Size = new System.Drawing.Size(63, 15);
      this.lblDarkMode.TabIndex = 5;
      this.lblDarkMode.Text = "Dark Mode";
      // 
      // tgglDarkTheme
      // 
      this.tgglDarkTheme.AutoSize = true;
      this.tgglDarkTheme.Location = new System.Drawing.Point(330, 127);
      this.tgglDarkTheme.Name = "tgglDarkTheme";
      this.tgglDarkTheme.Size = new System.Drawing.Size(80, 17);
      this.tgglDarkTheme.TabIndex = 2;
      this.tgglDarkTheme.Text = "Aus";
      this.tgglDarkTheme.UseVisualStyleBackColor = true;
      this.tgglDarkTheme.CheckedChanged += new System.EventHandler(this.TgglDarkTheme_CheckedChanged);
      // 
      // lblAbout
      // 
      this.lblAbout.Location = new System.Drawing.Point(3, 6);
      this.lblAbout.Name = "lblAbout";
      this.lblAbout.Size = new System.Drawing.Size(402, 119);
      this.lblAbout.TabIndex = 3;
      this.lblAbout.Text = "loading...";
      // 
      // lblTitle
      // 
      this.lblTitle.AutoSize = true;
      this.lblTitle.Location = new System.Drawing.Point(4, 12);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(179, 19);
      this.lblTitle.TabIndex = 2;
      this.lblTitle.Text = "Ultimate Osu Server Switcher";
      // 
      // styleManager
      // 
      this.styleManager.Owner = this;
      this.styleManager.Style = MetroFramework.MetroColorStyle.Pink;
      this.styleManager.Theme = MetroFramework.MetroThemeStyle.Light;
      // 
      // osuWatcher
      // 
      this.osuWatcher.Interval = 1000;
      this.osuWatcher.Tick += new System.EventHandler(this.osuWatcher_Tick);
      // 
      // btnUpdateAvailable
      // 
      this.btnUpdateAvailable.Location = new System.Drawing.Point(256, 29);
      this.btnUpdateAvailable.Name = "btnUpdateAvailable";
      this.btnUpdateAvailable.Size = new System.Drawing.Size(153, 23);
      this.btnUpdateAvailable.TabIndex = 7;
      this.btnUpdateAvailable.Text = "A new update is available!";
      this.btnUpdateAvailable.Visible = false;
      this.btnUpdateAvailable.Click += new System.EventHandler(this.btnUpdateAvailable_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425, 211);
      this.Controls.Add(this.btnUpdateAvailable);
      this.Controls.Add(this.lblTitle);
      this.Controls.Add(this.tbcntrlMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Resizable = false;
      this.ShowIcon = false;
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.tbcntrlMain.ResumeLayout(false);
      this.tbpgSwitcher.ResumeLayout(false);
      this.tbpgSwitcher.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrbxServerIcon)).EndInit();
      this.tbpgAbout.ResumeLayout(false);
      this.tbpgAbout.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.styleManager)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private MetroFramework.Controls.MetroTabControl tbcntrlMain;
    private MetroFramework.Controls.MetroTabPage tbpgSwitcher;
    private MetroFramework.Controls.MetroTabPage tbpgAbout;
    private MetroFramework.Controls.MetroLabel lblTitle;
    private MetroFramework.Controls.MetroButton btnConnect;
    private MetroFramework.Controls.MetroComboBox cmbbxServer;
    private MetroFramework.Controls.MetroLabel lblCurrentServer;
    private MetroFramework.Controls.MetroToggle tgglDarkTheme;
    private MetroFramework.Controls.MetroLabel lblAbout;
    private MetroFramework.Components.MetroStyleManager styleManager;
    private MetroFramework.Controls.MetroLabel lblDarkMode;
    private MetroFramework.Controls.MetroLabel lblGithub;
    private MetroFramework.Controls.MetroLabel lblOsuRunning;
    private System.Windows.Forms.Timer osuWatcher;
    private System.Windows.Forms.PictureBox pctrbxServerIcon;
    private MetroFramework.Controls.MetroButton btnUpdateAvailable;
    private MetroFramework.Controls.MetroLabel lblDiscord;
    private MetroFramework.Controls.MetroLabel lblClearIconCache;
    private MetroFramework.Controls.MetroLabel lblCreateShortcut;
  }
}

