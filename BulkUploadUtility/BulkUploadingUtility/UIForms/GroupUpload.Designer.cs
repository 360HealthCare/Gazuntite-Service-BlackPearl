namespace BulkUploadingUtility.UIForms
{
    partial class GroupUpload
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
            this.tipLogin = new System.Windows.Forms.ToolTip(this.components);
            this.deselectAll = new System.Windows.Forms.LinkLabel();
            this.ChkAll = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlClose = new System.Windows.Forms.Panel();
            this.lnkCloseSearch = new System.Windows.Forms.LinkLabel();
            this.lstSelectPatient = new System.Windows.Forms.CheckedListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblSelected = new System.Windows.Forms.Label();
            this.searchList = new System.Windows.Forms.ListBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.pnlClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // deselectAll
            // 
            this.deselectAll.ActiveLinkColor = System.Drawing.Color.Yellow;
            this.deselectAll.AutoSize = true;
            this.deselectAll.DisabledLinkColor = System.Drawing.Color.Yellow;
            this.deselectAll.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deselectAll.ForeColor = System.Drawing.Color.Yellow;
            this.deselectAll.LinkColor = System.Drawing.Color.Yellow;
            this.deselectAll.Location = new System.Drawing.Point(539, 256);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(83, 17);
            this.deselectAll.TabIndex = 25;
            this.deselectAll.TabStop = true;
            this.deselectAll.Text = "DESELECT";
            this.deselectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deselectAll_LinkClicked);
            // 
            // ChkAll
            // 
            this.ChkAll.ActiveLinkColor = System.Drawing.Color.Yellow;
            this.ChkAll.AutoSize = true;
            this.ChkAll.DisabledLinkColor = System.Drawing.Color.Yellow;
            this.ChkAll.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAll.ForeColor = System.Drawing.Color.Yellow;
            this.ChkAll.LinkColor = System.Drawing.Color.Yellow;
            this.ChkAll.Location = new System.Drawing.Point(476, 256);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Size = new System.Drawing.Size(63, 17);
            this.ChkAll.TabIndex = 24;
            this.ChkAll.TabStop = true;
            this.ChkAll.Text = "SELECT";
            this.ChkAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChkAll_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(39, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "SEARCH PATIENT";
            // 
            // pnlClose
            // 
            this.pnlClose.BackColor = System.Drawing.Color.White;
            this.pnlClose.Controls.Add(this.lnkCloseSearch);
            this.pnlClose.Location = new System.Drawing.Point(202, 127);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(418, 21);
            this.pnlClose.TabIndex = 22;
            this.pnlClose.Visible = false;
            // 
            // lnkCloseSearch
            // 
            this.lnkCloseSearch.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkCloseSearch.AutoSize = true;
            this.lnkCloseSearch.LinkColor = System.Drawing.Color.Black;
            this.lnkCloseSearch.Location = new System.Drawing.Point(187, 5);
            this.lnkCloseSearch.Name = "lnkCloseSearch";
            this.lnkCloseSearch.Size = new System.Drawing.Size(42, 13);
            this.lnkCloseSearch.TabIndex = 0;
            this.lnkCloseSearch.TabStop = true;
            this.lnkCloseSearch.Text = "CLOSE";
            this.lnkCloseSearch.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkCloseSearch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCloseSearch_LinkClicked);
            // 
            // lstSelectPatient
            // 
            this.lstSelectPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.lstSelectPatient.CheckOnClick = true;
            this.lstSelectPatient.Enabled = false;
            this.lstSelectPatient.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSelectPatient.ForeColor = System.Drawing.Color.White;
            this.lstSelectPatient.FormattingEnabled = true;
            this.lstSelectPatient.Location = new System.Drawing.Point(202, 164);
            this.lstSelectPatient.Name = "lstSelectPatient";
            this.lstSelectPatient.Size = new System.Drawing.Size(418, 89);
            this.lstSelectPatient.TabIndex = 21;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(426, 278);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(283, 278);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(112, 23);
            this.btnCreate.TabIndex = 19;
            this.btnCreate.Text = "UPLOAD";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.White;
            this.lblSelected.Location = new System.Drawing.Point(39, 191);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(144, 18);
            this.lblSelected.TabIndex = 18;
            this.lblSelected.Text = "SELECT PATIENT";
            // 
            // searchList
            // 
            this.searchList.FormattingEnabled = true;
            this.searchList.Location = new System.Drawing.Point(202, 60);
            this.searchList.Name = "searchList";
            this.searchList.Size = new System.Drawing.Size(418, 69);
            this.searchList.TabIndex = 17;
            this.searchList.Visible = false;
            this.searchList.SelectedIndexChanged += new System.EventHandler(this.searchList_SelectedIndexChanged);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(202, 43);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(418, 20);
            this.searchBox.TabIndex = 16;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // GroupUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.ClientSize = new System.Drawing.Size(660, 342);
            this.Controls.Add(this.deselectAll);
            this.Controls.Add(this.ChkAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlClose);
            this.Controls.Add(this.lstSelectPatient);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.searchList);
            this.Controls.Add(this.searchBox);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximizeBox = false;
            this.Name = "GroupUpload";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GROUP UPLOAD";
            this.pnlClose.ResumeLayout(false);
            this.pnlClose.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip tipLogin;
        private System.Windows.Forms.LinkLabel deselectAll;
        private System.Windows.Forms.LinkLabel ChkAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlClose;
        private System.Windows.Forms.LinkLabel lnkCloseSearch;
        private System.Windows.Forms.CheckedListBox lstSelectPatient;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.ListBox searchList;
        private System.Windows.Forms.TextBox searchBox;
    }
}