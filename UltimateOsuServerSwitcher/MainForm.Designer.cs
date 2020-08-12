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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.btnSwitcher = new System.Windows.Forms.Button();
      this.btnExit = new System.Windows.Forms.Button();
      this.pnlSwitcher = new System.Windows.Forms.Panel();
      this.pctrCurrentServer = new System.Windows.Forms.PictureBox();
      this.pctrConnecting = new System.Windows.Forms.PictureBox();
      this.lblInfo = new System.Windows.Forms.Label();
      this.btnLeft = new System.Windows.Forms.PictureBox();
      this.btnRight = new System.Windows.Forms.PictureBox();
      this.pctrCurrentSelectedServer = new System.Windows.Forms.PictureBox();
      this.pctrAlreadyConnected = new System.Windows.Forms.PictureBox();
      this.btnConnect = new System.Windows.Forms.PictureBox();
      this.pctrLoading = new System.Windows.Forms.PictureBox();
      this.btnSettings = new System.Windows.Forms.Button();
      this.pnlSettings = new System.Windows.Forms.Panel();
      this.btnHelp = new System.Windows.Forms.Button();
      this.pnlHelp = new System.Windows.Forms.Panel();
      this.pnlSwitcher.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentServer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrConnecting)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnLeft)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnRight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentSelectedServer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrAlreadyConnected)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnConnect)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrLoading)).BeginInit();
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
      this.btnSwitcher.Location = new System.Drawing.Point(12, 12);
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
      this.pnlSwitcher.Controls.Add(this.pctrCurrentServer);
      this.pnlSwitcher.Controls.Add(this.pctrConnecting);
      this.pnlSwitcher.Controls.Add(this.lblInfo);
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
      // lblInfo
      // 
      this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblInfo.ForeColor = System.Drawing.Color.White;
      this.lblInfo.Location = new System.Drawing.Point(3, 287);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(554, 34);
      this.lblInfo.TabIndex = 6;
      this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
      // 
      // pctrCurrentSelectedServer
      // 
      this.pctrCurrentSelectedServer.Location = new System.Drawing.Point(152, 28);
      this.pctrCurrentSelectedServer.Name = "pctrCurrentSelectedServer";
      this.pctrCurrentSelectedServer.Size = new System.Drawing.Size(256, 256);
      this.pctrCurrentSelectedServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
      this.pnlSettings.Location = new System.Drawing.Point(12, 52);
      this.pnlSettings.Name = "pnlSettings";
      this.pnlSettings.Size = new System.Drawing.Size(560, 405);
      this.pnlSettings.TabIndex = 8;
      this.pnlSettings.Visible = false;
      this.pnlSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
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
      this.pnlHelp.Location = new System.Drawing.Point(12, 52);
      this.pnlHelp.Name = "pnlHelp";
      this.pnlHelp.Size = new System.Drawing.Size(560, 405);
      this.pnlHelp.TabIndex = 8;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.ClientSize = new System.Drawing.Size(584, 469);
      this.Controls.Add(this.btnHelp);
      this.Controls.Add(this.btnSettings);
      this.Controls.Add(this.pnlSwitcher);
      this.Controls.Add(this.btnExit);
      this.Controls.Add(this.btnSwitcher);
      this.Controls.Add(this.pnlSettings);
      this.Controls.Add(this.pnlHelp);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ultimate Osu Server Switcher";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      this.pnlSwitcher.ResumeLayout(false);
      this.pnlSwitcher.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentServer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrConnecting)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnLeft)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnRight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrCurrentSelectedServer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrAlreadyConnected)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btnConnect)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrLoading)).EndInit();
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
  }
}

