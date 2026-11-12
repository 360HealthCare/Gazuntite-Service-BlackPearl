using System.Windows.Forms;
namespace BulkUploadingUtility.UIForms
{
    partial class Home
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
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tabOndemand = new System.Windows.Forms.TabPage();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.dtpByDate = new System.Windows.Forms.DateTimePicker();
            this.rdoByDate = new System.Windows.Forms.RadioButton();
            this.rdoCurrentLog = new System.Windows.Forms.RadioButton();
            this.rdoSelectGroup = new System.Windows.Forms.RadioButton();
            this.CmbDrive = new System.Windows.Forms.ComboBox();
            this.LblSelectDrv = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnBro = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.rdoSelect = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.LblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblStatus = new System.Windows.Forms.Label();
            this.FileUpdngProgBr = new System.Windows.Forms.ProgressBar();
            this.tabScheduled = new System.Windows.Forms.TabPage();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.Daily = new System.Windows.Forms.TabControl();
            this.Trigger = new System.Windows.Forms.TabPage();
            this.ActiveChk = new System.Windows.Forms.CheckBox();
            this.DailyTimeTxt = new System.Windows.Forms.DateTimePicker();
            this.WeeklyTab = new System.Windows.Forms.TabPage();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.WeeklyListBox = new System.Windows.Forms.ListBox();
            this.WeeklyTime = new System.Windows.Forms.DateTimePicker();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.PwdLbl = new System.Windows.Forms.Label();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.UserNameLbl = new System.Windows.Forms.Label();
            this.tabPatientFolder = new System.Windows.Forms.TabPage();
            this.rdoSelected = new System.Windows.Forms.RadioButton();
            this.rdoAllPat = new System.Windows.Forms.RadioButton();
            this.deselectAll = new System.Windows.Forms.LinkLabel();
            this.ChkAll = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlClose = new System.Windows.Forms.Panel();
            this.lnkCloseSearch = new System.Windows.Forms.LinkLabel();
            this.lstSelectPatient = new System.Windows.Forms.CheckedListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lnkLogout = new System.Windows.Forms.LinkLabel();
            this.searchList = new System.Windows.Forms.ListBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.TabLog = new System.Windows.Forms.TabPage();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.patientFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.tipCreateDirectory = new System.Windows.Forms.ToolTip(this.components);
            this.tabContainer.SuspendLayout();
            this.tabOndemand.SuspendLayout();
            this.tabScheduled.SuspendLayout();
            this.Daily.SuspendLayout();
            this.Trigger.SuspendLayout();
            this.WeeklyTab.SuspendLayout();
            this.tabPatientFolder.SuspendLayout();
            this.pnlClose.SuspendLayout();
            this.TabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tabOndemand);
            this.tabContainer.Controls.Add(this.tabScheduled);
            this.tabContainer.Controls.Add(this.tabPatientFolder);
            this.tabContainer.Controls.Add(this.TabLog);
            this.tabContainer.Location = new System.Drawing.Point(1, 0);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(695, 367);
            this.tabContainer.TabIndex = 0;
            this.tabContainer.SelectedIndexChanged += new System.EventHandler(this.Ondemand_SelectedIndexChanged);
            // 
            // tabOndemand
            // 
            this.tabOndemand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.tabOndemand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabOndemand.Controls.Add(this.btnSendMail);
            this.tabOndemand.Controls.Add(this.dtpByDate);
            this.tabOndemand.Controls.Add(this.rdoByDate);
            this.tabOndemand.Controls.Add(this.rdoCurrentLog);
            this.tabOndemand.Controls.Add(this.rdoSelectGroup);
            this.tabOndemand.Controls.Add(this.CmbDrive);
            this.tabOndemand.Controls.Add(this.LblSelectDrv);
            this.tabOndemand.Controls.Add(this.btnUpload);
            this.tabOndemand.Controls.Add(this.btnBro);
            this.tabOndemand.Controls.Add(this.txtAddress);
            this.tabOndemand.Controls.Add(this.rdoSelect);
            this.tabOndemand.Controls.Add(this.rdoAll);
            this.tabOndemand.Controls.Add(this.LblMsg);
            this.tabOndemand.Controls.Add(this.label1);
            this.tabOndemand.Controls.Add(this.LblStatus);
            this.tabOndemand.Controls.Add(this.FileUpdngProgBr);
            this.tabOndemand.Location = new System.Drawing.Point(4, 22);
            this.tabOndemand.Name = "tabOndemand";
            this.tabOndemand.Padding = new System.Windows.Forms.Padding(3);
            this.tabOndemand.Size = new System.Drawing.Size(687, 341);
            this.tabOndemand.TabIndex = 0;
            this.tabOndemand.Text = "ON DEMAND";
            this.tabOndemand.Click += new System.EventHandler(this.tabOndemand_Click);
            // 
            // btnSendMail
            // 
            this.btnSendMail.Enabled = false;
            this.btnSendMail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSendMail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMail.ForeColor = System.Drawing.Color.White;
            this.btnSendMail.Location = new System.Drawing.Point(516, 96);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(75, 23);
            this.btnSendMail.TabIndex = 15;
            this.btnSendMail.Text = "Send";
            this.tipCreateDirectory.SetToolTip(this.btnSendMail, "Upload documents");
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // dtpByDate
            // 
            this.dtpByDate.CalendarFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpByDate.CalendarForeColor = System.Drawing.SystemColors.Control;
            this.dtpByDate.CalendarMonthBackground = System.Drawing.SystemColors.WindowFrame;
            this.dtpByDate.Enabled = false;
            this.dtpByDate.Location = new System.Drawing.Point(452, 56);
            this.dtpByDate.Name = "dtpByDate";
            this.dtpByDate.Size = new System.Drawing.Size(229, 20);
            this.dtpByDate.TabIndex = 14;
            // 
            // rdoByDate
            // 
            this.rdoByDate.AutoSize = true;
            this.rdoByDate.Enabled = false;
            this.rdoByDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoByDate.ForeColor = System.Drawing.SystemColors.Control;
            this.rdoByDate.Location = new System.Drawing.Point(560, 30);
            this.rdoByDate.Name = "rdoByDate";
            this.rdoByDate.Size = new System.Drawing.Size(83, 20);
            this.rdoByDate.TabIndex = 13;
            this.rdoByDate.TabStop = true;
            this.rdoByDate.Text = "BY DATE";
            this.rdoByDate.UseVisualStyleBackColor = true;
            this.rdoByDate.CheckedChanged += new System.EventHandler(this.rdoByDate_CheckedChanged);
            // 
            // rdoCurrentLog
            // 
            this.rdoCurrentLog.AutoSize = true;
            this.rdoCurrentLog.Enabled = false;
            this.rdoCurrentLog.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCurrentLog.ForeColor = System.Drawing.SystemColors.Control;
            this.rdoCurrentLog.Location = new System.Drawing.Point(482, 30);
            this.rdoCurrentLog.Name = "rdoCurrentLog";
            this.rdoCurrentLog.Size = new System.Drawing.Size(72, 20);
            this.rdoCurrentLog.TabIndex = 12;
            this.rdoCurrentLog.TabStop = true;
            this.rdoCurrentLog.Text = "TODAY";
            this.rdoCurrentLog.UseVisualStyleBackColor = true;
            // 
            // rdoSelectGroup
            // 
            this.rdoSelectGroup.AutoSize = true;
            this.rdoSelectGroup.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSelectGroup.ForeColor = System.Drawing.Color.White;
            this.rdoSelectGroup.Location = new System.Drawing.Point(249, 11);
            this.rdoSelectGroup.Name = "rdoSelectGroup";
            this.rdoSelectGroup.Size = new System.Drawing.Size(136, 21);
            this.rdoSelectGroup.TabIndex = 11;
            this.rdoSelectGroup.TabStop = true;
            this.rdoSelectGroup.Text = "SELECT GROUP";
            this.rdoSelectGroup.UseVisualStyleBackColor = true;
            this.rdoSelectGroup.CheckedChanged += new System.EventHandler(this.rdoSelectGroup_CheckedChanged);
            // 
            // CmbDrive
            // 
            this.CmbDrive.FormattingEnabled = true;
            this.CmbDrive.Location = new System.Drawing.Point(560, 3);
            this.CmbDrive.Name = "CmbDrive";
            this.CmbDrive.Size = new System.Drawing.Size(121, 21);
            this.CmbDrive.TabIndex = 10;
            // 
            // LblSelectDrv
            // 
            this.LblSelectDrv.AutoSize = true;
            this.LblSelectDrv.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSelectDrv.ForeColor = System.Drawing.Color.White;
            this.LblSelectDrv.Location = new System.Drawing.Point(449, 5);
            this.LblSelectDrv.Name = "LblSelectDrv";
            this.LblSelectDrv.Size = new System.Drawing.Size(105, 16);
            this.LblSelectDrv.TabIndex = 9;
            this.LblSelectDrv.Text = "CHANGE DRIVE";
            // 
            // btnUpload
            // 
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpload.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(189, 96);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "Upload";
            this.tipCreateDirectory.SetToolTip(this.btnUpload, "Upload documents");
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnBro
            // 
            this.btnBro.Enabled = false;
            this.btnBro.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBro.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBro.ForeColor = System.Drawing.Color.White;
            this.btnBro.Location = new System.Drawing.Point(298, 52);
            this.btnBro.Name = "btnBro";
            this.btnBro.Size = new System.Drawing.Size(87, 23);
            this.btnBro.TabIndex = 7;
            this.btnBro.Text = "Browse";
            this.btnBro.UseVisualStyleBackColor = true;
            this.btnBro.Click += new System.EventHandler(this.btnBro_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(111, 55);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(176, 20);
            this.txtAddress.TabIndex = 6;
            // 
            // rdoSelect
            // 
            this.rdoSelect.AutoSize = true;
            this.rdoSelect.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSelect.ForeColor = System.Drawing.Color.White;
            this.rdoSelect.Location = new System.Drawing.Point(141, 11);
            this.rdoSelect.Name = "rdoSelect";
            this.rdoSelect.Size = new System.Drawing.Size(81, 21);
            this.rdoSelect.TabIndex = 5;
            this.rdoSelect.TabStop = true;
            this.rdoSelect.Text = "SELECT";
            this.rdoSelect.UseVisualStyleBackColor = true;
            this.rdoSelect.CheckedChanged += new System.EventHandler(this.rdoSelect_CheckedChanged);
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAll.ForeColor = System.Drawing.Color.White;
            this.rdoAll.Location = new System.Drawing.Point(46, 11);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(52, 21);
            this.rdoAll.TabIndex = 4;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "ALL";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // LblMsg
            // 
            this.LblMsg.AutoSize = true;
            this.LblMsg.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMsg.ForeColor = System.Drawing.SystemColors.Control;
            this.LblMsg.Location = new System.Drawing.Point(46, 260);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(79, 18);
            this.LblMsg.TabIndex = 3;
            this.LblMsg.Text = "Message";
            this.LblMsg.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.LblStatus.Location = new System.Drawing.Point(43, 142);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(71, 18);
            this.LblStatus.TabIndex = 1;
            this.LblStatus.Text = "STATUS";
            // 
            // FileUpdngProgBr
            // 
            this.FileUpdngProgBr.Location = new System.Drawing.Point(43, 177);
            this.FileUpdngProgBr.Name = "FileUpdngProgBr";
            this.FileUpdngProgBr.Size = new System.Drawing.Size(574, 54);
            this.FileUpdngProgBr.TabIndex = 0;
            // 
            // tabScheduled
            // 
            this.tabScheduled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.tabScheduled.Controls.Add(this.CancelBtn);
            this.tabScheduled.Controls.Add(this.SaveBtn);
            this.tabScheduled.Controls.Add(this.Daily);
            this.tabScheduled.Controls.Add(this.txtPwd);
            this.tabScheduled.Controls.Add(this.PwdLbl);
            this.tabScheduled.Controls.Add(this.TxtUserName);
            this.tabScheduled.Controls.Add(this.UserNameLbl);
            this.tabScheduled.Location = new System.Drawing.Point(4, 22);
            this.tabScheduled.Name = "tabScheduled";
            this.tabScheduled.Padding = new System.Windows.Forms.Padding(3);
            this.tabScheduled.Size = new System.Drawing.Size(687, 341);
            this.tabScheduled.TabIndex = 1;
            this.tabScheduled.Text = "SCHEDULED";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(363, 295);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "CANCEL";
            this.tipCreateDirectory.SetToolTip(this.CancelBtn, "Clear all feilds");
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(226, 295);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 5;
            this.SaveBtn.Text = "SAVE";
            this.tipCreateDirectory.SetToolTip(this.SaveBtn, "Create Schedule");
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // Daily
            // 
            this.Daily.Controls.Add(this.Trigger);
            this.Daily.Controls.Add(this.WeeklyTab);
            this.Daily.Location = new System.Drawing.Point(0, 108);
            this.Daily.Name = "Daily";
            this.Daily.SelectedIndex = 0;
            this.Daily.Size = new System.Drawing.Size(691, 181);
            this.Daily.TabIndex = 4;
            // 
            // Trigger
            // 
            this.Trigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.Trigger.Controls.Add(this.ActiveChk);
            this.Trigger.Controls.Add(this.DailyTimeTxt);
            this.Trigger.Location = new System.Drawing.Point(4, 22);
            this.Trigger.Name = "Trigger";
            this.Trigger.Padding = new System.Windows.Forms.Padding(3);
            this.Trigger.Size = new System.Drawing.Size(683, 155);
            this.Trigger.TabIndex = 0;
            this.Trigger.Text = "DAILY";
            // 
            // ActiveChk
            // 
            this.ActiveChk.AutoSize = true;
            this.ActiveChk.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveChk.ForeColor = System.Drawing.SystemColors.Control;
            this.ActiveChk.Location = new System.Drawing.Point(10, 32);
            this.ActiveChk.Name = "ActiveChk";
            this.ActiveChk.Size = new System.Drawing.Size(80, 21);
            this.ActiveChk.TabIndex = 5;
            this.ActiveChk.Text = "ACTIVE";
            this.ActiveChk.UseVisualStyleBackColor = true;
            // 
            // DailyTimeTxt
            // 
            this.DailyTimeTxt.CustomFormat = "7";
            this.DailyTimeTxt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DailyTimeTxt.Location = new System.Drawing.Point(6, 66);
            this.DailyTimeTxt.Name = "DailyTimeTxt";
            this.DailyTimeTxt.ShowUpDown = true;
            this.DailyTimeTxt.Size = new System.Drawing.Size(200, 20);
            this.DailyTimeTxt.TabIndex = 0;
            // 
            // WeeklyTab
            // 
            this.WeeklyTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.WeeklyTab.Controls.Add(this.ChkActive);
            this.WeeklyTab.Controls.Add(this.WeeklyListBox);
            this.WeeklyTab.Controls.Add(this.WeeklyTime);
            this.WeeklyTab.Location = new System.Drawing.Point(4, 22);
            this.WeeklyTab.Name = "WeeklyTab";
            this.WeeklyTab.Padding = new System.Windows.Forms.Padding(3);
            this.WeeklyTab.Size = new System.Drawing.Size(683, 155);
            this.WeeklyTab.TabIndex = 2;
            this.WeeklyTab.Text = "WEEKLY";
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.ForeColor = System.Drawing.SystemColors.Control;
            this.ChkActive.Location = new System.Drawing.Point(7, 28);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.Size = new System.Drawing.Size(64, 17);
            this.ChkActive.TabIndex = 4;
            this.ChkActive.Text = "ACTIVE";
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // WeeklyListBox
            // 
            this.WeeklyListBox.FormattingEnabled = true;
            this.WeeklyListBox.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.WeeklyListBox.Location = new System.Drawing.Point(296, 19);
            this.WeeklyListBox.Name = "WeeklyListBox";
            this.WeeklyListBox.Size = new System.Drawing.Size(120, 95);
            this.WeeklyListBox.TabIndex = 3;
            // 
            // WeeklyTime
            // 
            this.WeeklyTime.CustomFormat = "7";
            this.WeeklyTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.WeeklyTime.Location = new System.Drawing.Point(6, 62);
            this.WeeklyTime.Name = "WeeklyTime";
            this.WeeklyTime.ShowUpDown = true;
            this.WeeklyTime.Size = new System.Drawing.Size(200, 20);
            this.WeeklyTime.TabIndex = 2;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(171, 66);
            this.txtPwd.MaxLength = 35;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(240, 20);
            this.txtPwd.TabIndex = 3;
            // 
            // PwdLbl
            // 
            this.PwdLbl.AutoSize = true;
            this.PwdLbl.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PwdLbl.ForeColor = System.Drawing.SystemColors.Control;
            this.PwdLbl.Location = new System.Drawing.Point(44, 66);
            this.PwdLbl.Name = "PwdLbl";
            this.PwdLbl.Size = new System.Drawing.Size(102, 18);
            this.PwdLbl.TabIndex = 2;
            this.PwdLbl.Text = "PASSWORD";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Location = new System.Drawing.Point(171, 22);
            this.TxtUserName.MaxLength = 35;
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(240, 20);
            this.TxtUserName.TabIndex = 1;
            // 
            // UserNameLbl
            // 
            this.UserNameLbl.AutoSize = true;
            this.UserNameLbl.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLbl.ForeColor = System.Drawing.SystemColors.Control;
            this.UserNameLbl.Location = new System.Drawing.Point(44, 24);
            this.UserNameLbl.Name = "UserNameLbl";
            this.UserNameLbl.Size = new System.Drawing.Size(103, 18);
            this.UserNameLbl.TabIndex = 0;
            this.UserNameLbl.Text = "USER NAME";
            // 
            // tabPatientFolder
            // 
            this.tabPatientFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.tabPatientFolder.Controls.Add(this.rdoSelected);
            this.tabPatientFolder.Controls.Add(this.rdoAllPat);
            this.tabPatientFolder.Controls.Add(this.deselectAll);
            this.tabPatientFolder.Controls.Add(this.ChkAll);
            this.tabPatientFolder.Controls.Add(this.label2);
            this.tabPatientFolder.Controls.Add(this.pnlClose);
            this.tabPatientFolder.Controls.Add(this.lstSelectPatient);
            this.tabPatientFolder.Controls.Add(this.btnClear);
            this.tabPatientFolder.Controls.Add(this.btnCreate);
            this.tabPatientFolder.Controls.Add(this.lblSelected);
            this.tabPatientFolder.Controls.Add(this.lnkLogout);
            this.tabPatientFolder.Controls.Add(this.searchList);
            this.tabPatientFolder.Controls.Add(this.searchBox);
            this.tabPatientFolder.Location = new System.Drawing.Point(4, 22);
            this.tabPatientFolder.Name = "tabPatientFolder";
            this.tabPatientFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatientFolder.Size = new System.Drawing.Size(687, 341);
            this.tabPatientFolder.TabIndex = 2;
            this.tabPatientFolder.Text = "PATIENT FOLDER";
            this.tabPatientFolder.Click += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // rdoSelected
            // 
            this.rdoSelected.AutoSize = true;
            this.rdoSelected.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSelected.ForeColor = System.Drawing.Color.White;
            this.rdoSelected.Location = new System.Drawing.Point(424, 6);
            this.rdoSelected.Name = "rdoSelected";
            this.rdoSelected.Size = new System.Drawing.Size(138, 20);
            this.rdoSelected.TabIndex = 17;
            this.rdoSelected.Text = "SELECT PATIENT";
            this.rdoSelected.UseVisualStyleBackColor = true;
            this.rdoSelected.CheckedChanged += new System.EventHandler(this.rdoSelected_CheckedChanged);
            // 
            // rdoAllPat
            // 
            this.rdoAllPat.AutoSize = true;
            this.rdoAllPat.Checked = true;
            this.rdoAllPat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAllPat.ForeColor = System.Drawing.Color.White;
            this.rdoAllPat.Location = new System.Drawing.Point(281, 6);
            this.rdoAllPat.Name = "rdoAllPat";
            this.rdoAllPat.Size = new System.Drawing.Size(111, 20);
            this.rdoAllPat.TabIndex = 16;
            this.rdoAllPat.TabStop = true;
            this.rdoAllPat.Text = "ALL PATIENT";
            this.rdoAllPat.UseVisualStyleBackColor = true;
            this.rdoAllPat.CheckedChanged += new System.EventHandler(this.rdoAllPat_CheckedChanged);
            // 
            // deselectAll
            // 
            this.deselectAll.ActiveLinkColor = System.Drawing.Color.Yellow;
            this.deselectAll.AutoSize = true;
            this.deselectAll.DisabledLinkColor = System.Drawing.Color.Yellow;
            this.deselectAll.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deselectAll.ForeColor = System.Drawing.Color.Yellow;
            this.deselectAll.LinkColor = System.Drawing.Color.Yellow;
            this.deselectAll.Location = new System.Drawing.Point(537, 252);
            this.deselectAll.Name = "deselectAll";
            this.deselectAll.Size = new System.Drawing.Size(83, 17);
            this.deselectAll.TabIndex = 15;
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
            this.ChkAll.Location = new System.Drawing.Point(474, 252);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Size = new System.Drawing.Size(63, 17);
            this.ChkAll.TabIndex = 14;
            this.ChkAll.TabStop = true;
            this.ChkAll.Text = "SELECT";
            this.ChkAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChkAll_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(37, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "SEARCH PATIENT";
            // 
            // pnlClose
            // 
            this.pnlClose.BackColor = System.Drawing.Color.White;
            this.pnlClose.Controls.Add(this.lnkCloseSearch);
            this.pnlClose.Enabled = false;
            this.pnlClose.Location = new System.Drawing.Point(200, 123);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(418, 21);
            this.pnlClose.TabIndex = 12;
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
            this.lstSelectPatient.Location = new System.Drawing.Point(200, 160);
            this.lstSelectPatient.Name = "lstSelectPatient";
            this.lstSelectPatient.Size = new System.Drawing.Size(418, 89);
            this.lstSelectPatient.TabIndex = 11;
            this.lstSelectPatient.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstSelectPatient_ItemCheck);
            this.lstSelectPatient.SelectedIndexChanged += new System.EventHandler(this.lstSelectPatient_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(424, 274);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "CLEAR";
            this.tipCreateDirectory.SetToolTip(this.btnClear, "Clear all feilds");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(281, 274);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(112, 23);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "CREATE FOLDERS";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.White;
            this.lblSelected.Location = new System.Drawing.Point(37, 187);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(144, 18);
            this.lblSelected.TabIndex = 5;
            this.lblSelected.Text = "SELECT PATIENT";
            // 
            // lnkLogout
            // 
            this.lnkLogout.ActiveLinkColor = System.Drawing.Color.Yellow;
            this.lnkLogout.AutoSize = true;
            this.lnkLogout.DisabledLinkColor = System.Drawing.Color.Yellow;
            this.lnkLogout.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogout.ForeColor = System.Drawing.Color.Yellow;
            this.lnkLogout.LinkColor = System.Drawing.Color.Yellow;
            this.lnkLogout.Location = new System.Drawing.Point(594, 4);
            this.lnkLogout.Name = "lnkLogout";
            this.lnkLogout.Size = new System.Drawing.Size(79, 17);
            this.lnkLogout.TabIndex = 2;
            this.lnkLogout.TabStop = true;
            this.lnkLogout.Text = "SIGN OUT";
            this.lnkLogout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLogout_LinkClicked);
            // 
            // searchList
            // 
            this.searchList.Enabled = false;
            this.searchList.FormattingEnabled = true;
            this.searchList.Location = new System.Drawing.Point(200, 56);
            this.searchList.Name = "searchList";
            this.searchList.Size = new System.Drawing.Size(418, 69);
            this.searchList.TabIndex = 1;
            this.searchList.Visible = false;
            this.searchList.SelectedIndexChanged += new System.EventHandler(this.searchList_SelectedIndexChanged);
            // 
            // searchBox
            // 
            this.searchBox.Enabled = false;
            this.searchBox.Location = new System.Drawing.Point(200, 39);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(418, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // TabLog
            // 
            this.TabLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.TabLog.Controls.Add(this.lblLog);
            this.TabLog.Controls.Add(this.txtLog);
            this.TabLog.Location = new System.Drawing.Point(4, 22);
            this.TabLog.Name = "TabLog";
            this.TabLog.Padding = new System.Windows.Forms.Padding(3);
            this.TabLog.Size = new System.Drawing.Size(687, 341);
            this.TabLog.TabIndex = 3;
            this.TabLog.Text = "LOG";
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.ForeColor = System.Drawing.Color.White;
            this.lblLog.Location = new System.Drawing.Point(18, 24);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(124, 18);
            this.lblLog.TabIndex = 1;
            this.lblLog.Text = "PROGESS LOG";
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(59, 53);
            this.txtLog.MaxLength = 100000000;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(584, 248);
            this.txtLog.TabIndex = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(78)))));
            this.ClientSize = new System.Drawing.Size(698, 352);
            this.Controls.Add(this.tabContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUIVUS BULK DOCUMENT UPLOAD UTILITY";
            this.TransparencyKey = System.Drawing.Color.Pink;
            this.tabContainer.ResumeLayout(false);
            this.tabOndemand.ResumeLayout(false);
            this.tabOndemand.PerformLayout();
            this.tabScheduled.ResumeLayout(false);
            this.tabScheduled.PerformLayout();
            this.Daily.ResumeLayout(false);
            this.Trigger.ResumeLayout(false);
            this.Trigger.PerformLayout();
            this.WeeklyTab.ResumeLayout(false);
            this.WeeklyTab.PerformLayout();
            this.tabPatientFolder.ResumeLayout(false);
            this.tabPatientFolder.PerformLayout();
            this.pnlClose.ResumeLayout(false);
            this.pnlClose.PerformLayout();
            this.TabLog.ResumeLayout(false);
            this.TabLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage tabOndemand;
        private System.Windows.Forms.TabPage tabScheduled;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label PwdLbl;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.Label UserNameLbl;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TabControl Daily;
        private System.Windows.Forms.TabPage Trigger;
        private System.Windows.Forms.DateTimePicker DailyTimeTxt;
        private System.Windows.Forms.TabPage WeeklyTab;
        private CheckBox ChkActive;
        private ListBox WeeklyListBox;
        private DateTimePicker WeeklyTime;
        private CheckBox ActiveChk;
        private ProgressBar FileUpdngProgBr;
        private Label LblMsg;
        private Label label1;
        private Label LblStatus;
        private TabPage tabPatientFolder;
        private RadioButton rdoSelect;
        private RadioButton rdoAll;
        private Button btnBro;
        private TextBox txtAddress;
        private Button btnUpload;
        private FolderBrowserDialog patientFolderBrowser;
        private ListBox searchList;
        private TextBox searchBox;
        private LinkLabel lnkLogout;
        private Label lblSelected;
        private Button btnClear;
        private Button btnCreate;
        private CheckedListBox lstSelectPatient;
        private Panel pnlClose;
        private LinkLabel lnkCloseSearch;
        private Label label2;
        private ToolTip tipCreateDirectory;
        private LinkLabel deselectAll;
        private LinkLabel ChkAll;
        private ComboBox CmbDrive;
        private Label LblSelectDrv;
        private RadioButton rdoSelected;
        private RadioButton rdoAllPat;
        private TabPage TabLog;
        private Label lblLog;
        private TextBox txtLog;
        private RadioButton rdoSelectGroup;
        private DateTimePicker dtpByDate;
        private RadioButton rdoByDate;
        private RadioButton rdoCurrentLog;
        private Button btnSendMail;
    }
}