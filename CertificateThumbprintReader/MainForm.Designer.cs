namespace CertificateThumbprintReader
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
      this.txtCertificate = new System.Windows.Forms.TextBox();
      this.txtThumbprint = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnGetThumbprint = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtCertificate
      // 
      this.txtCertificate.Location = new System.Drawing.Point(12, 25);
      this.txtCertificate.Multiline = true;
      this.txtCertificate.Name = "txtCertificate";
      this.txtCertificate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtCertificate.Size = new System.Drawing.Size(385, 168);
      this.txtCertificate.TabIndex = 0;
      // 
      // txtThumbprint
      // 
      this.txtThumbprint.Location = new System.Drawing.Point(12, 199);
      this.txtThumbprint.Name = "txtThumbprint";
      this.txtThumbprint.ReadOnly = true;
      this.txtThumbprint.Size = new System.Drawing.Size(272, 20);
      this.txtThumbprint.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Certificate";
      // 
      // btnGetThumbprint
      // 
      this.btnGetThumbprint.Location = new System.Drawing.Point(290, 199);
      this.btnGetThumbprint.Name = "btnGetThumbprint";
      this.btnGetThumbprint.Size = new System.Drawing.Size(107, 22);
      this.btnGetThumbprint.TabIndex = 3;
      this.btnGetThumbprint.Text = "Get Thumbprint";
      this.btnGetThumbprint.UseVisualStyleBackColor = true;
      this.btnGetThumbprint.Click += new System.EventHandler(this.btnGetThumbprint_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(410, 233);
      this.Controls.Add(this.btnGetThumbprint);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtThumbprint);
      this.Controls.Add(this.txtCertificate);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "MainForm";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Certificate Thumbprint Reader";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtCertificate;
    private System.Windows.Forms.TextBox txtThumbprint;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnGetThumbprint;
  }
}

