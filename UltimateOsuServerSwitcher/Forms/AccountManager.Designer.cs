namespace UltimateOsuServerSwitcher.Forms
{
  partial class AccountManager
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
      this.flwpnlAccounts = new System.Windows.Forms.FlowLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.btnRemoveAccount = new System.Windows.Forms.Button();
      this.btnAddAccount = new System.Windows.Forms.Button();
      this.lnklblHowToAdd = new System.Windows.Forms.LinkLabel();
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
      this.btnExit.Location = new System.Drawing.Point(376, 14);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(35, 35);
      this.btnExit.TabIndex = 3;
      this.btnExit.Text = "X";
      this.btnExit.UseVisualStyleBackColor = false;
      this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
      // 
      // flwpnlAccounts
      // 
      this.flwpnlAccounts.Location = new System.Drawing.Point(12, 61);
      this.flwpnlAccounts.Name = "flwpnlAccounts";
      this.flwpnlAccounts.Size = new System.Drawing.Size(398, 263);
      this.flwpnlAccounts.TabIndex = 4;
      this.flwpnlAccounts.WrapContents = false;
      this.flwpnlAccounts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(12, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(179, 30);
      this.label1.TabIndex = 5;
      this.label1.Text = "Account Manager";
      this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      // 
      // btnRemoveAccount
      // 
      this.btnRemoveAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
      this.btnRemoveAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnRemoveAccount.FlatAppearance.BorderSize = 2;
      this.btnRemoveAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnRemoveAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnRemoveAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnRemoveAccount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnRemoveAccount.ForeColor = System.Drawing.Color.White;
      this.btnRemoveAccount.Location = new System.Drawing.Point(12, 330);
      this.btnRemoveAccount.Name = "btnRemoveAccount";
      this.btnRemoveAccount.Size = new System.Drawing.Size(139, 34);
      this.btnRemoveAccount.TabIndex = 13;
      this.btnRemoveAccount.Text = "Remove Account";
      this.btnRemoveAccount.UseVisualStyleBackColor = false;
      this.btnRemoveAccount.Click += new System.EventHandler(this.btnRemoveAccount_Click);
      // 
      // btnAddAccount
      // 
      this.btnAddAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
      this.btnAddAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
      this.btnAddAccount.FlatAppearance.BorderSize = 2;
      this.btnAddAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnAddAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
      this.btnAddAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnAddAccount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnAddAccount.ForeColor = System.Drawing.Color.White;
      this.btnAddAccount.Location = new System.Drawing.Point(290, 330);
      this.btnAddAccount.Name = "btnAddAccount";
      this.btnAddAccount.Size = new System.Drawing.Size(120, 34);
      this.btnAddAccount.TabIndex = 15;
      this.btnAddAccount.Text = "Add Account";
      this.btnAddAccount.UseVisualStyleBackColor = false;
      this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
      // 
      // lnklblHowToAdd
      // 
      this.lnklblHowToAdd.AutoSize = true;
      this.lnklblHowToAdd.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.lnklblHowToAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
      this.lnklblHowToAdd.Location = new System.Drawing.Point(194, 337);
      this.lnklblHowToAdd.Name = "lnklblHowToAdd";
      this.lnklblHowToAdd.Size = new System.Drawing.Size(90, 21);
      this.lnklblHowToAdd.TabIndex = 16;
      this.lnklblHowToAdd.TabStop = true;
      this.lnklblHowToAdd.Text = "How to add";
      this.lnklblHowToAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblHowToAdd_LinkClicked);
      // 
      // AccountManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
      this.ClientSize = new System.Drawing.Size(422, 375);
      this.Controls.Add(this.lnklblHowToAdd);
      this.Controls.Add(this.btnAddAccount);
      this.Controls.Add(this.btnRemoveAccount);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.flwpnlAccounts);
      this.Controls.Add(this.btnExit);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.Name = "AccountManager";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Account Manager";
      this.Load += new System.EventHandler(this.AccountManager_Load);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BorderlessDragMouseDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnExit;
    private System.Windows.Forms.FlowLayoutPanel flwpnlAccounts;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnRemoveAccount;
    private System.Windows.Forms.Button btnAddAccount;
    private System.Windows.Forms.LinkLabel lnklblHowToAdd;
  }
}