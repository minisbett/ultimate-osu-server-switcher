
namespace UltimateOsuServerSwitcher.Forms
{
  partial class History
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnExit = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.btnClearHistory = new System.Windows.Forms.Button();
      this.lblSize = new System.Windows.Forms.Label();
      this.SuspendLayout();
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
      this.btnExit.Location = new System.Drawing.Point(280, 12);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(35, 35);
      this.btnExit.TabIndex = 3;
      this.btnExit.Text = "X";
      this.btnExit.UseVisualStyleBackColor = false;
      this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 30);
      this.label1.TabIndex = 6;
      this.label1.Text = "History";
      this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      // 
      // flowLayoutPanel
      // 
      this.flowLayoutPanel.AutoScroll = true;
      this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanel.Location = new System.Drawing.Point(12, 53);
      this.flowLayoutPanel.Name = "flowLayoutPanel";
      this.flowLayoutPanel.Size = new System.Drawing.Size(303, 444);
      this.flowLayoutPanel.TabIndex = 7;
      this.flowLayoutPanel.WrapContents = false;
      this.flowLayoutPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      // 
      // btnClearHistory
      // 
      this.btnClearHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
      this.btnClearHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnClearHistory.FlatAppearance.BorderSize = 2;
      this.btnClearHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
      this.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnClearHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnClearHistory.ForeColor = System.Drawing.Color.White;
      this.btnClearHistory.Location = new System.Drawing.Point(12, 505);
      this.btnClearHistory.Name = "btnClearHistory";
      this.btnClearHistory.Size = new System.Drawing.Size(121, 35);
      this.btnClearHistory.TabIndex = 8;
      this.btnClearHistory.Text = "Clear History";
      this.btnClearHistory.UseVisualStyleBackColor = false;
      this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
      // 
      // lblSize
      // 
      this.lblSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSize.ForeColor = System.Drawing.Color.White;
      this.lblSize.Location = new System.Drawing.Point(131, 511);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new System.Drawing.Size(184, 23);
      this.lblSize.TabIndex = 9;
      this.lblSize.Text = "Disk usage: 50.1 MB";
      this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // History
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.ClientSize = new System.Drawing.Size(327, 549);
      this.Controls.Add(this.btnClearHistory);
      this.Controls.Add(this.lblSize);
      this.Controls.Add(this.flowLayoutPanel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnExit);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "History";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    private System.Windows.Forms.Button btnClearHistory;
    private System.Windows.Forms.Label lblSize;
  }
}