namespace UltimateOsuServerSwitcher.Controls
{
  partial class AccountItem
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

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.pctrServerIcon = new System.Windows.Forms.PictureBox();
      this.lblServername = new System.Windows.Forms.Label();
      this.lblUsername = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pctrServerIcon)).BeginInit();
      this.SuspendLayout();
      // 
      // pctrServerIcon
      // 
      this.pctrServerIcon.Location = new System.Drawing.Point(32, 16);
      this.pctrServerIcon.Name = "pctrServerIcon";
      this.pctrServerIcon.Size = new System.Drawing.Size(64, 64);
      this.pctrServerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrServerIcon.TabIndex = 0;
      this.pctrServerIcon.TabStop = false;
      this.pctrServerIcon.Click += new System.EventHandler(this.Global_Click);
      this.pctrServerIcon.MouseEnter += new System.EventHandler(this.Global_MouseEnter);
      this.pctrServerIcon.MouseLeave += new System.EventHandler(this.Global_MouseLeave);
      // 
      // lblServername
      // 
      this.lblServername.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblServername.ForeColor = System.Drawing.Color.White;
      this.lblServername.Location = new System.Drawing.Point(0, 80);
      this.lblServername.Name = "lblServername";
      this.lblServername.Size = new System.Drawing.Size(128, 23);
      this.lblServername.TabIndex = 1;
      this.lblServername.Text = "Server";
      this.lblServername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblServername.Click += new System.EventHandler(this.Global_Click);
      this.lblServername.MouseEnter += new System.EventHandler(this.Global_MouseEnter);
      this.lblServername.MouseLeave += new System.EventHandler(this.Global_MouseLeave);
      // 
      // lblUsername
      // 
      this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblUsername.ForeColor = System.Drawing.Color.White;
      this.lblUsername.Location = new System.Drawing.Point(0, 97);
      this.lblUsername.Name = "lblUsername";
      this.lblUsername.Size = new System.Drawing.Size(128, 23);
      this.lblUsername.TabIndex = 2;
      this.lblUsername.Text = "Username";
      this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblUsername.Click += new System.EventHandler(this.Global_Click);
      this.lblUsername.MouseEnter += new System.EventHandler(this.Global_MouseEnter);
      this.lblUsername.MouseLeave += new System.EventHandler(this.Global_MouseLeave);
      // 
      // AccountItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.Controls.Add(this.lblServername);
      this.Controls.Add(this.lblUsername);
      this.Controls.Add(this.pctrServerIcon);
      this.Name = "AccountItem";
      this.Size = new System.Drawing.Size(128, 128);
      this.Click += new System.EventHandler(this.Global_Click);
      this.MouseEnter += new System.EventHandler(this.Global_MouseEnter);
      this.MouseLeave += new System.EventHandler(this.Global_MouseLeave);
      ((System.ComponentModel.ISupportInitialize)(this.pctrServerIcon)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pctrServerIcon;
    private System.Windows.Forms.Label lblServername;
    private System.Windows.Forms.Label lblUsername;
  }
}
