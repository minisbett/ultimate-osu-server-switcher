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
      this.btnSwitcher = new System.Windows.Forms.Button();
      this.btnExit = new System.Windows.Forms.Button();
      this.pnlSwitcher = new System.Windows.Forms.Panel();
      this.pctrVerified = new System.Windows.Forms.PictureBox();
      this.lblInfo = new System.Windows.Forms.Label();
      this.pctrCurrentServer = new System.Windows.Forms.PictureBox();
      this.pctrConnecting = new System.Windows.Forms.PictureBox();
      this.btnLeft = new System.Windows.Forms.PictureBox();
      this.btnRight = new System.Windows.Forms.PictureBox();
      this.pctrCurrentSelectedServer = new System.Windows.Forms.PictureBox();
      this.pctrAlreadyConnected = new System.Windows.Forms.PictureBox();
      this.btnConnect = new System.Windows.Forms.PictureBox();
      this.pctrLoading = new System.Windows.Forms.PictureBox();
      this.btnSettings = new System.Windows.Forms.Button();
      this.pnlSettings = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lnklblWhyMinimize = new System.Windows.Forms.LinkLabel();
      this.chckbxMinimize = new UltimateOsuServerSwitcher.BetterCheckBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.chckbxOpenAfterQuickSwitching = new UltimateOsuServerSwitcher.BetterCheckBox();
      this.chckbxReopenAfterSwitching = new UltimateOsuServerSwitcher.BetterCheckBox();
      this.chckbxCloseBeforeSwitching = new UltimateOsuServerSwitcher.BetterCheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.redirectUrlLabel = new System.Windows.Forms.Label();
      this.checkRedirectButton = new System.Windows.Forms.Button();
      this.chckbxSendTelemetry = new UltimateOsuServerSwitcher.BetterCheckBox();
      this.lnklblTelemetryLearnMore = new System.Windows.Forms.LinkLabel();
      this.btnHelp = new System.Windows.Forms.Button();
      this.pnlHelp = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.lblVersion = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.pctrDiscord = new System.Windows.Forms.PictureBox();
      this.pctrGithub = new System.Windows.Forms.PictureBox();
      this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
      this.cntxtmnNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlSwitcher.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrVerified)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentServer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrConnecting)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnLeft)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnRight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentSelectedServer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrAlreadyConnected)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnConnect)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrLoading)).BeginInit();
      this.pnlSettings.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.pnlHelp.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrDiscord)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrGithub)).BeginInit();
      this.cntxtmnNotifyIcon.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnSwitcher
      // 
      this.btnSwitcher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
      this.btnSwitcher.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnSwitcher.FlatAppearance.BorderSize = 2;
      this.btnSwitcher.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnSwitcher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnSwitcher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSwitcher.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSwitcher.ForeColor = System.Drawing.Color.White;
      this.btnSwitcher.Location = new System.Drawing.Point(12, 10);
      this.btnSwitcher.Name = "btnSwitcher";
      this.btnSwitcher.Size = new System.Drawing.Size(88, 34);
      this.btnSwitcher.TabIndex = 1;
      this.btnSwitcher.Text = "Switcher";
      this.btnSwitcher.UseVisualStyleBackColor = false;
      this.btnSwitcher.Click += new System.EventHandler(this.btnSwitcher_Click);
      // 
      // btnExit
      // 
      this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
      this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnExit.FlatAppearance.BorderSize = 2;
      this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
      this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnExit.ForeColor = System.Drawing.Color.White;
      this.btnExit.Location = new System.Drawing.Point(537, 12);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(35, 35);
      this.btnExit.TabIndex = 2;
      this.btnExit.Text = "X";
      this.btnExit.UseVisualStyleBackColor = false;
      this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
      // 
      // pnlSwitcher
      // 
      this.pnlSwitcher.Controls.Add(this.pctrVerified);
      this.pnlSwitcher.Controls.Add(this.lblInfo);
      this.pnlSwitcher.Controls.Add(this.pctrCurrentServer);
      this.pnlSwitcher.Controls.Add(this.pctrConnecting);
      this.pnlSwitcher.Controls.Add(this.btnLeft);
      this.pnlSwitcher.Controls.Add(this.btnRight);
      this.pnlSwitcher.Controls.Add(this.pctrCurrentSelectedServer);
      this.pnlSwitcher.Controls.Add(this.pctrAlreadyConnected);
      this.pnlSwitcher.Controls.Add(this.btnConnect);
      this.pnlSwitcher.Controls.Add(this.pctrLoading);
      this.pnlSwitcher.Location = new System.Drawing.Point(12, 52);
      this.pnlSwitcher.Name = "pnlSwitcher";
      this.pnlSwitcher.Size = new System.Drawing.Size(560, 405);
      this.pnlSwitcher.TabIndex = 3;
      this.pnlSwitcher.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      // 
      // pctrVerified
      // 
      this.pctrVerified.Image = ((System.Drawing.Image)(resources.GetObject("pctrVerified.Image")));
      this.pctrVerified.Location = new System.Drawing.Point(330, 294);
      this.pctrVerified.Name = "pctrVerified";
      this.pctrVerified.Size = new System.Drawing.Size(16, 16);
      this.pctrVerified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrVerified.TabIndex = 9;
      this.pctrVerified.TabStop = false;
      this.pctrVerified.Visible = false;
      // 
      // lblInfo
      // 
      this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblInfo.ForeColor = System.Drawing.Color.White;
      this.lblInfo.Location = new System.Drawing.Point(3, 293);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(554, 34);
      this.lblInfo.TabIndex = 6;
      this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // pctrCurrentServer
      // 
      this.pctrCurrentServer.Location = new System.Drawing.Point(391, 333);
      this.pctrCurrentServer.Name = "pctrCurrentServer";
      this.pctrCurrentServer.Size = new System.Drawing.Size(48, 48);
      this.pctrCurrentServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrCurrentServer.TabIndex = 8;
      this.pctrCurrentServer.TabStop = false;
      // 
      // pctrConnecting
      // 
      this.pctrConnecting.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.pctrConnecting.Image = ((System.Drawing.Image)(resources.GetObject("pctrConnecting.Image")));
      this.pctrConnecting.Location = new System.Drawing.Point(150, 311);
      this.pctrConnecting.Name = "pctrConnecting";
      this.pctrConnecting.Size = new System.Drawing.Size(260, 95);
      this.pctrConnecting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pctrConnecting.TabIndex = 7;
      this.pctrConnecting.TabStop = false;
      this.pctrConnecting.Visible = false;
      // 
      // btnLeft
      // 
      this.btnLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnLeft.Image")));
      this.btnLeft.Location = new System.Drawing.Point(80, 124);
      this.btnLeft.Name = "btnLeft";
      this.btnLeft.Size = new System.Drawing.Size(32, 64);
      this.btnLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.btnLeft.TabIndex = 5;
      this.btnLeft.TabStop = false;
      this.btnLeft.Visible = false;
      this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
      this.btnLeft.DoubleClick += new System.EventHandler(this.btnLeft_Click);
      // 
      // btnRight
      // 
      this.btnRight.Image = ((System.Drawing.Image)(resources.GetObject("btnRight.Image")));
      this.btnRight.Location = new System.Drawing.Point(448, 124);
      this.btnRight.Name = "btnRight";
      this.btnRight.Size = new System.Drawing.Size(32, 64);
      this.btnRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.btnRight.TabIndex = 4;
      this.btnRight.TabStop = false;
      this.btnRight.Visible = false;
      this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
      this.btnRight.DoubleClick += new System.EventHandler(this.btnRight_Click);
      // 
      // pctrCurrentSelectedServer
      // 
      this.pctrCurrentSelectedServer.Location = new System.Drawing.Point(152, 28);
      this.pctrCurrentSelectedServer.Name = "pctrCurrentSelectedServer";
      this.pctrCurrentSelectedServer.Size = new System.Drawing.Size(256, 256);
      this.pctrCurrentSelectedServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pctrCurrentSelectedServer.TabIndex = 0;
      this.pctrCurrentSelectedServer.TabStop = false;
      this.pctrCurrentSelectedServer.Click += new System.EventHandler(this.pctrServerIcon_Click);
      // 
      // pctrAlreadyConnected
      // 
      this.pctrAlreadyConnected.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.pctrAlreadyConnected.Image = ((System.Drawing.Image)(resources.GetObject("pctrAlreadyConnected.Image")));
      this.pctrAlreadyConnected.Location = new System.Drawing.Point(150, 311);
      this.pctrAlreadyConnected.Name = "pctrAlreadyConnected";
      this.pctrAlreadyConnected.Size = new System.Drawing.Size(260, 95);
      this.pctrAlreadyConnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pctrAlreadyConnected.TabIndex = 2;
      this.pctrAlreadyConnected.TabStop = false;
      this.pctrAlreadyConnected.Visible = false;
      // 
      // btnConnect
      // 
      this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
      this.btnConnect.Location = new System.Drawing.Point(150, 311);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(260, 95);
      this.btnConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.btnConnect.TabIndex = 1;
      this.btnConnect.TabStop = false;
      this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
      // 
      // pctrLoading
      // 
      this.pctrLoading.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.pctrLoading.Image = ((System.Drawing.Image)(resources.GetObject("pctrLoading.Image")));
      this.pctrLoading.Location = new System.Drawing.Point(150, 311);
      this.pctrLoading.Name = "pctrLoading";
      this.pctrLoading.Size = new System.Drawing.Size(260, 95);
      this.pctrLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pctrLoading.TabIndex = 3;
      this.pctrLoading.TabStop = false;
      // 
      // btnSettings
      // 
      this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnSettings.FlatAppearance.BorderSize = 0;
      this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSettings.ForeColor = System.Drawing.Color.White;
      this.btnSettings.Location = new System.Drawing.Point(106, 12);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Size = new System.Drawing.Size(88, 34);
      this.btnSettings.TabIndex = 4;
      this.btnSettings.Text = "Settings";
      this.btnSettings.UseVisualStyleBackColor = false;
      this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
      // 
      // pnlSettings
      // 
      this.pnlSettings.Controls.Add(this.groupBox1);
      this.pnlSettings.Location = new System.Drawing.Point(12, 52);
      this.pnlSettings.Name = "pnlSettings";
      this.pnlSettings.Size = new System.Drawing.Size(560, 405);
      this.pnlSettings.TabIndex = 8;
      this.pnlSettings.Visible = false;
      this.pnlSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lnklblWhyMinimize);
      this.groupBox1.Controls.Add(this.chckbxMinimize);
      this.groupBox1.Controls.Add(this.groupBox3);
      this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.ForeColor = System.Drawing.Color.White;
      this.groupBox1.Location = new System.Drawing.Point(-1, 8);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(579, 402);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "General";
      // 
      // lnklblWhyMinimize
      // 
      this.lnklblWhyMinimize.AutoSize = true;
      this.lnklblWhyMinimize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lnklblWhyMinimize.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
      this.lnklblWhyMinimize.Location = new System.Drawing.Point(193, 28);
      this.lnklblWhyMinimize.Name = "lnklblWhyMinimize";
      this.lnklblWhyMinimize.Size = new System.Drawing.Size(44, 20);
      this.lnklblWhyMinimize.TabIndex = 3;
      this.lnklblWhyMinimize.TabStop = true;
      this.lnklblWhyMinimize.Text = "Why?";
      this.lnklblWhyMinimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblWhyMinimize_LinkClicked);
      // 
      // chckbxMinimize
      // 
      this.chckbxMinimize.AutoSize = true;
      this.chckbxMinimize.Checked = true;
      this.chckbxMinimize.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chckbxMinimize.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.chckbxMinimize.ForeColor = System.Drawing.Color.White;
      this.chckbxMinimize.Location = new System.Drawing.Point(1, 27);
      this.chckbxMinimize.Name = "chckbxMinimize";
      this.chckbxMinimize.Size = new System.Drawing.Size(193, 24);
      this.chckbxMinimize.TabIndex = 3;
      this.chckbxMinimize.Text = "Minimize to system tray";
      this.chckbxMinimize.UseVisualStyleBackColor = true;
      this.chckbxMinimize.CheckedChanged += new System.EventHandler(this.chckbxMinimize_CheckedChanged);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.chckbxOpenAfterQuickSwitching);
      this.groupBox3.Controls.Add(this.chckbxReopenAfterSwitching);
      this.groupBox3.Controls.Add(this.chckbxCloseBeforeSwitching);
      this.groupBox3.Controls.Add(this.groupBox2);
      this.groupBox3.ForeColor = System.Drawing.Color.White;
      this.groupBox3.Location = new System.Drawing.Point(0, 101);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(579, 328);
      this.groupBox3.TabIndex = 5;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Switching Behaviour";
      // 
      // chckbxOpenAfterQuickSwitching
      // 
      this.chckbxOpenAfterQuickSwitching.AutoSize = true;
      this.chckbxOpenAfterQuickSwitching.Checked = true;
      this.chckbxOpenAfterQuickSwitching.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chckbxOpenAfterQuickSwitching.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.chckbxOpenAfterQuickSwitching.ForeColor = System.Drawing.Color.White;
      this.chckbxOpenAfterQuickSwitching.Location = new System.Drawing.Point(1, 93);
      this.chckbxOpenAfterQuickSwitching.Name = "chckbxOpenAfterQuickSwitching";
      this.chckbxOpenAfterQuickSwitching.Size = new System.Drawing.Size(403, 24);
      this.chckbxOpenAfterQuickSwitching.TabIndex = 9;
      this.chckbxOpenAfterQuickSwitching.Text = "Open osu after quick-switching the server (Shortcut)";
      this.chckbxOpenAfterQuickSwitching.UseVisualStyleBackColor = true;
      this.chckbxOpenAfterQuickSwitching.CheckedChanged += new System.EventHandler(this.chckbxOpenAfterQuickSwitching_CheckedChanged);
      // 
      // chckbxReopenAfterSwitching
      // 
      this.chckbxReopenAfterSwitching.AutoSize = true;
      this.chckbxReopenAfterSwitching.Checked = true;
      this.chckbxReopenAfterSwitching.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chckbxReopenAfterSwitching.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.chckbxReopenAfterSwitching.ForeColor = System.Drawing.Color.White;
      this.chckbxReopenAfterSwitching.Location = new System.Drawing.Point(1, 63);
      this.chckbxReopenAfterSwitching.Name = "chckbxReopenAfterSwitching";
      this.chckbxReopenAfterSwitching.Size = new System.Drawing.Size(306, 24);
      this.chckbxReopenAfterSwitching.TabIndex = 7;
      this.chckbxReopenAfterSwitching.Text = "Re-open osu after switching the server";
      this.chckbxReopenAfterSwitching.UseVisualStyleBackColor = true;
      this.chckbxReopenAfterSwitching.CheckedChanged += new System.EventHandler(this.chckbxReopenAfterSwitching_CheckedChanged);
      // 
      // chckbxCloseBeforeSwitching
      // 
      this.chckbxCloseBeforeSwitching.AutoSize = true;
      this.chckbxCloseBeforeSwitching.Checked = true;
      this.chckbxCloseBeforeSwitching.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chckbxCloseBeforeSwitching.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.chckbxCloseBeforeSwitching.ForeColor = System.Drawing.Color.White;
      this.chckbxCloseBeforeSwitching.Location = new System.Drawing.Point(1, 32);
      this.chckbxCloseBeforeSwitching.Name = "chckbxCloseBeforeSwitching";
      this.chckbxCloseBeforeSwitching.Size = new System.Drawing.Size(299, 24);
      this.chckbxCloseBeforeSwitching.TabIndex = 6;
      this.chckbxCloseBeforeSwitching.Text = "Close osu before switching the server";
      this.chckbxCloseBeforeSwitching.UseVisualStyleBackColor = true;
      this.chckbxCloseBeforeSwitching.CheckedChanged += new System.EventHandler(this.chckbxCloseBeforeSwitching_CheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.checkRedirectButton);
      this.groupBox2.Controls.Add(this.redirectUrlLabel);
      this.groupBox2.Controls.Add(this.lnklblTelemetryLearnMore);
      this.groupBox2.Controls.Add(this.chckbxSendTelemetry);
      this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox2.ForeColor = System.Drawing.Color.White;
      this.groupBox2.Location = new System.Drawing.Point(0, 207);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(601, 250);
      this.groupBox2.TabIndex = 4;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Other";
      // 
      // redirectUrlLabel
      // 
      this.redirectUrlLabel.AutoSize = true;
      this.redirectUrlLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.redirectUrlLabel.Location = new System.Drawing.Point(63, 66);
      this.redirectUrlLabel.Name = "redirectUrlLabel";
      this.redirectUrlLabel.Size = new System.Drawing.Size(384, 20);
      this.redirectUrlLabel.TabIndex = 10;
      this.redirectUrlLabel.Text = "Click to check where https://osu.ppy.sh/ redirects to";
      // 
      // checkRedirectButton
      // 
      this.checkRedirectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
      this.checkRedirectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.checkRedirectButton.FlatAppearance.BorderSize = 2;
      this.checkRedirectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.checkRedirectButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.checkRedirectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.checkRedirectButton.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.checkRedirectButton.ForeColor = System.Drawing.Color.White;
      this.checkRedirectButton.Location = new System.Drawing.Point(0, 62);
      this.checkRedirectButton.Name = "checkRedirectButton";
      this.checkRedirectButton.Size = new System.Drawing.Size(63, 27);
      this.checkRedirectButton.TabIndex = 10;
      this.checkRedirectButton.Text = "Check";
      this.checkRedirectButton.UseVisualStyleBackColor = false;
      this.checkRedirectButton.Click += new System.EventHandler(this.checkRedirectButton_Click);
      // 
      // chckbxSendTelemetry
      // 
      this.chckbxSendTelemetry.AutoSize = true;
      this.chckbxSendTelemetry.Checked = true;
      this.chckbxSendTelemetry.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chckbxSendTelemetry.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.chckbxSendTelemetry.ForeColor = System.Drawing.Color.White;
      this.chckbxSendTelemetry.Location = new System.Drawing.Point(0, 32);
      this.chckbxSendTelemetry.Name = "chckbxSendTelemetry";
      this.chckbxSendTelemetry.Size = new System.Drawing.Size(451, 24);
      this.chckbxSendTelemetry.TabIndex = 2;
      this.chckbxSendTelemetry.Text = "Send anonymous telemetry data for better user experience";
      this.chckbxSendTelemetry.UseVisualStyleBackColor = true;
      this.chckbxSendTelemetry.CheckedChanged += new System.EventHandler(this.chckbxSendTelemetry_CheckedChanged);
      // 
      // lnklblTelemetryLearnMore
      // 
      this.lnklblTelemetryLearnMore.AutoSize = true;
      this.lnklblTelemetryLearnMore.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lnklblTelemetryLearnMore.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
      this.lnklblTelemetryLearnMore.Location = new System.Drawing.Point(435, 33);
      this.lnklblTelemetryLearnMore.Name = "lnklblTelemetryLearnMore";
      this.lnklblTelemetryLearnMore.Size = new System.Drawing.Size(91, 20);
      this.lnklblTelemetryLearnMore.TabIndex = 1;
      this.lnklblTelemetryLearnMore.TabStop = true;
      this.lnklblTelemetryLearnMore.Text = "Learn more";
      this.lnklblTelemetryLearnMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblTelemetryLearnMore_LinkClicked);
      // 
      // btnHelp
      // 
      this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnHelp.FlatAppearance.BorderSize = 0;
      this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnHelp.ForeColor = System.Drawing.Color.White;
      this.btnHelp.Location = new System.Drawing.Point(200, 12);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(64, 34);
      this.btnHelp.TabIndex = 9;
      this.btnHelp.Text = "Help";
      this.btnHelp.UseVisualStyleBackColor = false;
      this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
      // 
      // pnlHelp
      // 
      this.pnlHelp.Controls.Add(this.label2);
      this.pnlHelp.Controls.Add(this.lblVersion);
      this.pnlHelp.Controls.Add(this.pictureBox2);
      this.pnlHelp.Controls.Add(this.pictureBox1);
      this.pnlHelp.Controls.Add(this.label1);
      this.pnlHelp.Controls.Add(this.pctrDiscord);
      this.pnlHelp.Controls.Add(this.pctrGithub);
      this.pnlHelp.Location = new System.Drawing.Point(12, 52);
      this.pnlHelp.Name = "pnlHelp";
      this.pnlHelp.Size = new System.Drawing.Size(560, 405);
      this.pnlHelp.TabIndex = 8;
      this.pnlHelp.Visible = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Gray;
      this.label2.Location = new System.Drawing.Point(5, 365);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(188, 16);
      this.label2.TabIndex = 6;
      this.label2.Text = "Copyright (c) Niklas Fehde, 2020";
      // 
      // lblVersion
      // 
      this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVersion.ForeColor = System.Drawing.Color.Gray;
      this.lblVersion.Location = new System.Drawing.Point(423, 3);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(134, 21);
      this.lblVersion.TabIndex = 5;
      this.lblVersion.Text = "Version X.X.X";
      this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(247, 54);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(292, 49);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox2.TabIndex = 4;
      this.pictureBox2.TabStop = false;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(22, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(301, 59);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(5, 384);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(415, 16);
      this.label1.TabIndex = 2;
      this.label1.Text = "Ultimate Osu Server Switcher is not affiliated in any way with ppy. Pty Ltd";
      // 
      // pctrDiscord
      // 
      this.pctrDiscord.Image = ((System.Drawing.Image)(resources.GetObject("pctrDiscord.Image")));
      this.pctrDiscord.Location = new System.Drawing.Point(423, 338);
      this.pctrDiscord.Name = "pctrDiscord";
      this.pctrDiscord.Size = new System.Drawing.Size(64, 64);
      this.pctrDiscord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrDiscord.TabIndex = 1;
      this.pctrDiscord.TabStop = false;
      this.pctrDiscord.Click += new System.EventHandler(this.pctrDiscord_Click);
      // 
      // pctrGithub
      // 
      this.pctrGithub.Image = ((System.Drawing.Image)(resources.GetObject("pctrGithub.Image")));
      this.pctrGithub.Location = new System.Drawing.Point(493, 338);
      this.pctrGithub.Name = "pctrGithub";
      this.pctrGithub.Size = new System.Drawing.Size(64, 64);
      this.pctrGithub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrGithub.TabIndex = 0;
      this.pctrGithub.TabStop = false;
      this.pctrGithub.Click += new System.EventHandler(this.pctrGithub_Click);
      // 
      // notifyIcon
      // 
      this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
      this.notifyIcon.Text = "Ultimate Osu Server Switcher";
      this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
      // 
      // cntxtmnNotifyIcon
      // 
      this.cntxtmnNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
      this.cntxtmnNotifyIcon.Name = "cntxtmnNotifyIcon";
      this.cntxtmnNotifyIcon.Size = new System.Drawing.Size(109, 54);
      // 
      // showToolStripMenuItem
      // 
      this.showToolStripMenuItem.Name = "showToolStripMenuItem";
      this.showToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
      this.showToolStripMenuItem.Text = "Show";
      this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(105, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.ClientSize = new System.Drawing.Size(584, 469);
      this.Controls.Add(this.btnHelp);
      this.Controls.Add(this.btnSettings);
      this.Controls.Add(this.btnExit);
      this.Controls.Add(this.btnSwitcher);
      this.Controls.Add(this.pnlSettings);
      this.Controls.Add(this.pnlHelp);
      this.Controls.Add(this.pnlSwitcher);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ultimate Osu Server Switcher";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      this.pnlSwitcher.ResumeLayout(false);
      this.pnlSwitcher.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrVerified)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentServer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrConnecting)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnLeft)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnRight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentSelectedServer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrAlreadyConnected)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnConnect)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrLoading)).EndInit();
      this.pnlSettings.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.pnlHelp.ResumeLayout(false);
      this.pnlHelp.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrDiscord)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrGithub)).EndInit();
      this.cntxtmnNotifyIcon.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button btnSwitcher;
    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Panel pnlSwitcher;
    private System.Windows.Forms.PictureBox pctrCurrentSelectedServer;
    private System.Windows.Forms.PictureBox btnConnect;
    private System.Windows.Forms.PictureBox pctrAlreadyConnected;
    private System.Windows.Forms.PictureBox pctrLoading;
    private System.Windows.Forms.PictureBox btnRight;
    private System.Windows.Forms.PictureBox btnLeft;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.PictureBox pctrConnecting;
    private System.Windows.Forms.Button btnSettings;
    private System.Windows.Forms.Panel pnlSettings;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.Panel pnlHelp;
    private System.Windows.Forms.PictureBox pctrCurrentServer;
    private System.Windows.Forms.PictureBox pctrGithub;
    private System.Windows.Forms.PictureBox pctrDiscord;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label lblVersion;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pctrVerified;
    private System.Windows.Forms.LinkLabel lnklblTelemetryLearnMore;
        private BetterCheckBox chckbxSendTelemetry;
        private System.Windows.Forms.GroupBox groupBox1;
        private BetterCheckBox chckbxMinimize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnklblWhyMinimize;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip cntxtmnNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private BetterCheckBox chckbxCloseBeforeSwitching;
        private BetterCheckBox chckbxReopenAfterSwitching;
        private BetterCheckBox chckbxOpenAfterQuickSwitching;
    private System.Windows.Forms.Button checkRedirectButton;
    private System.Windows.Forms.Label redirectUrlLabel;
  }
}

