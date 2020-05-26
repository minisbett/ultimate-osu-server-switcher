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
      this.tbCntrl = new MetroFramework.Controls.MetroTabControl();
      this.tbpgSwitcher = new MetroFramework.Controls.MetroTabPage();
      this.pctrbxServerIcon = new System.Windows.Forms.PictureBox();
      this.cmbbxServer = new MetroFramework.Controls.MetroComboBox();
      this.lblOsuRunning = new MetroFramework.Controls.MetroLabel();
      this.lblCurrentServer = new MetroFramework.Controls.MetroLabel();
      this.btnConnect = new MetroFramework.Controls.MetroButton();
      this.tbpgAbout = new MetroFramework.Controls.MetroTabPage();
      this.lblGithub = new MetroFramework.Controls.MetroLabel();
      this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
      this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
      this.tgglDarkTheme = new MetroFramework.Controls.MetroToggle();
      this.lblAbout = new MetroFramework.Controls.MetroLabel();
      this.lblTitle = new MetroFramework.Controls.MetroLabel();
      this.styleManager = new MetroFramework.Components.MetroStyleManager(this.components);
      this.osuWatcher = new System.Windows.Forms.Timer(this.components);
      this.tbCntrl.SuspendLayout();
      this.tbpgSwitcher.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrbxServerIcon)).BeginInit();
      this.tbpgAbout.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.styleManager)).BeginInit();
      this.SuspendLayout();
      // 
      // tbCntrl
      // 
      this.tbCntrl.Controls.Add(this.tbpgSwitcher);
      this.tbCntrl.Controls.Add(this.tbpgAbout);
      this.tbCntrl.Location = new System.Drawing.Point(0, 26);
      this.tbCntrl.Name = "tbCntrl";
      this.tbCntrl.SelectedIndex = 0;
      this.tbCntrl.Size = new System.Drawing.Size(424, 185);
      this.tbCntrl.TabIndex = 0;
      // 
      // tbpgSwitcher
      // 
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
      this.lblOsuRunning.Text = "Please close osu before switching your server!";
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
      this.tbpgAbout.Controls.Add(this.lblGithub);
      this.tbpgAbout.Controls.Add(this.metroLabel1);
      this.tbpgAbout.Controls.Add(this.metroPanel1);
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
      // lblGithub
      // 
      this.lblGithub.AutoSize = true;
      this.lblGithub.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblGithub.Location = new System.Drawing.Point(351, 84);
      this.lblGithub.Name = "lblGithub";
      this.lblGithub.Size = new System.Drawing.Size(49, 19);
      this.lblGithub.TabIndex = 6;
      this.lblGithub.Text = "GitHub";
      this.lblGithub.Click += new System.EventHandler(this.LblGithub_Click);
      // 
      // metroLabel1
      // 
      this.metroLabel1.AutoSize = true;
      this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
      this.metroLabel1.Location = new System.Drawing.Point(281, 110);
      this.metroLabel1.Name = "metroLabel1";
      this.metroLabel1.Size = new System.Drawing.Size(63, 15);
      this.metroLabel1.TabIndex = 5;
      this.metroLabel1.Text = "Dark Mode";
      // 
      // metroPanel1
      // 
      this.metroPanel1.HorizontalScrollbarBarColor = true;
      this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
      this.metroPanel1.HorizontalScrollbarSize = 10;
      this.metroPanel1.Location = new System.Drawing.Point(304, 106);
      this.metroPanel1.Name = "metroPanel1";
      this.metroPanel1.Size = new System.Drawing.Size(41, 25);
      this.metroPanel1.TabIndex = 4;
      this.metroPanel1.VerticalScrollbarBarColor = true;
      this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
      this.metroPanel1.VerticalScrollbarSize = 10;
      // 
      // tgglDarkTheme
      // 
      this.tgglDarkTheme.AutoSize = true;
      this.tgglDarkTheme.Location = new System.Drawing.Point(318, 110);
      this.tgglDarkTheme.Name = "tgglDarkTheme";
      this.tgglDarkTheme.Size = new System.Drawing.Size(80, 17);
      this.tgglDarkTheme.TabIndex = 2;
      this.tgglDarkTheme.Text = "Aus";
      this.tgglDarkTheme.UseVisualStyleBackColor = true;
      this.tgglDarkTheme.CheckedChanged += new System.EventHandler(this.TgglDarkTheme_CheckedChanged);
      // 
      // lblAbout
      // 
      this.lblAbout.Location = new System.Drawing.Point(19, 10);
      this.lblAbout.Name = "lblAbout";
      this.lblAbout.Size = new System.Drawing.Size(379, 121);
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
      this.osuWatcher.Interval = 500;
      this.osuWatcher.Tick += new System.EventHandler(this.osuWatcher_Tick);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425, 211);
      this.Controls.Add(this.lblTitle);
      this.Controls.Add(this.tbCntrl);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Resizable = false;
      this.ShowIcon = false;
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.tbCntrl.ResumeLayout(false);
      this.tbpgSwitcher.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pctrbxServerIcon)).EndInit();
      this.tbpgAbout.ResumeLayout(false);
      this.tbpgAbout.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.styleManager)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private MetroFramework.Controls.MetroTabControl tbCntrl;
    private MetroFramework.Controls.MetroTabPage tbpgSwitcher;
    private MetroFramework.Controls.MetroTabPage tbpgAbout;
    private MetroFramework.Controls.MetroLabel lblTitle;
    private MetroFramework.Controls.MetroButton btnConnect;
    private MetroFramework.Controls.MetroComboBox cmbbxServer;
    private MetroFramework.Controls.MetroLabel lblCurrentServer;
    private MetroFramework.Controls.MetroToggle tgglDarkTheme;
    private MetroFramework.Controls.MetroLabel lblAbout;
    private MetroFramework.Components.MetroStyleManager styleManager;
    private MetroFramework.Controls.MetroPanel metroPanel1;
    private MetroFramework.Controls.MetroLabel metroLabel1;
    private MetroFramework.Controls.MetroLabel lblGithub;
    private MetroFramework.Controls.MetroLabel lblOsuRunning;
    private System.Windows.Forms.Timer osuWatcher;
    private System.Windows.Forms.PictureBox pctrbxServerIcon;
  }
}

