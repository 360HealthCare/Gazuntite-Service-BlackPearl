namespace BulkUploadingUtility.UIForms
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.lblProviderID = new System.Windows.Forms.Label();
            this.txtProviderID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cmbPractice = new System.Windows.Forms.ComboBox();
            this.lblPractice = new System.Windows.Forms.Label();
            this.tipLogin = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblProviderID
            // 
            this.lblProviderID.AutoSize = true;
            this.lblProviderID.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviderID.ForeColor = System.Drawing.Color.White;
            this.lblProviderID.Location = new System.Drawing.Point(142, 6);
            this.lblProviderID.Name = "lblProviderID";
            this.lblProviderID.Size = new System.Drawing.Size(99, 18);
            this.lblProviderID.TabIndex = 0;
            this.lblProviderID.Text = "Provider ID";
            // 
            // txtProviderID
            // 
            this.txtProviderID.Location = new System.Drawing.Point(75, 35);
            this.txtProviderID.Name = "txtProviderID";
            this.txtProviderID.Size = new System.Drawing.Size(235, 20);
            this.txtProviderID.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(75, 109);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(235, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(144, 80);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(85, 18);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLogin.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(147, 154);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(79, 35);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "SIGN IN";
            this.tipLogin.SetToolTip(this.btnLogin, "sign in");
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cmbPractice
            // 
            this.cmbPractice.FormattingEnabled = true;
            this.cmbPractice.Items.AddRange(new object[] {
            "Select Practice"});
            this.cmbPractice.Location = new System.Drawing.Point(75, 240);
            this.cmbPractice.Name = "cmbPractice";
            this.cmbPractice.Size = new System.Drawing.Size(235, 21);
            this.cmbPractice.TabIndex = 5;
            this.cmbPractice.Visible = false;
            this.cmbPractice.SelectionChangeCommitted += new System.EventHandler(this.cmbPractice_SelectedIndexChanged);
            // 
            // lblPractice
            // 
            this.lblPractice.AutoSize = true;
            this.lblPractice.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPractice.ForeColor = System.Drawing.Color.White;
            this.lblPractice.Location = new System.Drawing.Point(147, 210);
            this.lblPractice.Name = "lblPractice";
            this.lblPractice.Size = new System.Drawing.Size(72, 18);
            this.lblPractice.TabIndex = 6;
            this.lblPractice.Text = "Practice";
            this.lblPractice.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.ClientSize = new System.Drawing.Size(395, 285);
            this.Controls.Add(this.lblPractice);
            this.Controls.Add(this.cmbPractice);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtProviderID);
            this.Controls.Add(this.lblProviderID);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProviderID;
        private System.Windows.Forms.TextBox txtProviderID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cmbPractice;
        private System.Windows.Forms.Label lblPractice;
        private System.Windows.Forms.ToolTip tipLogin;
    }
}