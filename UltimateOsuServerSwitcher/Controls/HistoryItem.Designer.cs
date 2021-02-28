
namespace UltimateOsuServerSwitcher.Controls
{
  partial class HistoryItem
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.pctrServerIconFrom = new System.Windows.Forms.PictureBox();
      this.pctrServerIconTo = new System.Windows.Forms.PictureBox();
      this.lblFrom = new System.Windows.Forms.Label();
      this.lblTo = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lblDateTime = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pctrServerIconFrom)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrServerIconTo)).BeginInit();
      this.SuspendLayout();
      // 
      // pctrServerIconFrom
      // 
      this.pctrServerIconFrom.Location = new System.Drawing.Point(33, 28);
      this.pctrServerIconFrom.Name = "pctrServerIconFrom";
      this.pctrServerIconFrom.Size = new System.Drawing.Size(64, 64);
      this.pctrServerIconFrom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrServerIconFrom.TabIndex = 1;
      this.pctrServerIconFrom.TabStop = false;
      // 
      // pctrServerIconTo
      // 
      this.pctrServerIconTo.Location = new System.Drawing.Point(177, 28);
      this.pctrServerIconTo.Name = "pctrServerIconTo";
      this.pctrServerIconTo.Size = new System.Drawing.Size(64, 64);
      this.pctrServerIconTo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pctrServerIconTo.TabIndex = 2;
      this.pctrServerIconTo.TabStop = false;
      // 
      // lblFrom
      // 
      this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFrom.ForeColor = System.Drawing.Color.White;
      this.lblFrom.Location = new System.Drawing.Point(10, 90);
      this.lblFrom.Name = "lblFrom";
      this.lblFrom.Size = new System.Drawing.Size(107, 28);
      this.lblFrom.TabIndex = 3;
      this.lblFrom.Text = "label1";
      this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblTo
      // 
      this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTo.ForeColor = System.Drawing.Color.White;
      this.lblTo.Location = new System.Drawing.Point(154, 90);
      this.lblTo.Name = "lblTo";
      this.lblTo.Size = new System.Drawing.Size(107, 28);
      this.lblTo.TabIndex = 4;
      this.lblTo.Text = "label2";
      this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(115, 39);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(44, 37);
      this.label3.TabIndex = 5;
      this.label3.Text = "➜";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(219, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(50, 20);
      this.label1.TabIndex = 6;
      this.label1.Text = "label3";
      // 
      // lblDateTime
      // 
      this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
      this.lblDateTime.Location = new System.Drawing.Point(95, 5);
      this.lblDateTime.Name = "lblDateTime";
      this.lblDateTime.Size = new System.Drawing.Size(176, 20);
      this.lblDateTime.TabIndex = 6;
      this.lblDateTime.Text = "label3";
      this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // HistoryItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.Controls.Add(this.lblDateTime);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.pctrServerIconTo);
      this.Controls.Add(this.lblTo);
      this.Controls.Add(this.pctrServerIconFrom);
      this.Controls.Add(this.lblFrom);
      this.Name = "HistoryItem";
      this.Size = new System.Drawing.Size(274, 122);
      ((System.ComponentModel.ISupportInitialize)(this.pctrServerIconFrom)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pctrServerIconTo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pctrServerIconFrom;
    private System.Windows.Forms.PictureBox pctrServerIconTo;
    private System.Windows.Forms.Label lblFrom;
    private System.Windows.Forms.Label lblTo;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblDateTime;
  }
}
