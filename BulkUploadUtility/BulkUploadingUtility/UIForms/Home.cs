using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using BulkUploadingUtility.BL;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.PopUp;
using TaskScheduler;
using BulkUploadingUtility.UserManagerService;
using BulkUploadingUtility.Model;
using BulkUploadingUtility.Model.Response;
using System.IO;
using System.Threading;

namespace BulkUploadingUtility.UIForms
{
    public partial class Home : Form
    {
        static bool isReady = false;
        public Home()
        {
            InitializeComponent();
            loadTask();
            start();
            CmbDrive.DataSource = DriveInfo.GetDrives();
            CmbDrive.SelectedIndex = -1;
            this.CmbDrive.SelectedIndexChanged += new System.EventHandler(this.CmbDrive_SelectedIndexChanged);
            selectDrive();
            BulkDocumentUploader.FileUpLoaded += new EventHandler(upLoaderController_FileUpLoaded);
            BulkDocumentUploader.LogControl += new EventHandler(Record_Log);
                
        }

        private void selectDrive()
        {
            try
            {
                for (int index = 0; index < CmbDrive.Items.Count; index++)
                {
                    string selectedText = CmbDrive.Items[index].ToString();
                    if (selectedText == InfoStore.ReadDriveInfo())
                    {
                        CmbDrive.SelectedIndex = index;
                        isReady = true;
                        break;
                    }
                }
            }
            catch
            {
                
            }
        }

        #region Uploading Method
        public void start()
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    BulkUploadController upLoaderController = new BulkUploadController();
                    upLoaderController.FileUpLoaded += new EventHandler(upLoaderController_FileUpLoaded);
                }
                else
                {
                    LblMsg.Text = Contants.NETWORK_UNAVILABLE;
                }
            }
            catch (Exception exp)
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    LblMsg.Text = Contants.NETWORK_UNAVILABLE;
                }
                else
                {
                    LblMsg.Text = Contants.ERROR_MSG;
                }
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
        }

        void upLoaderController_FileUpLoaded(object sender, EventArgs e)
        {
            LblStatus.Visible = true;
            if (FileUpdngProgBr.Maximum != int.Parse(sender.ToString()))
            {
                FileUpdngProgBr.Maximum = int.Parse(sender.ToString());
            }
            FileUpdngProgBr.Value = FileUpdngProgBr.Minimum + 1;
            //LblStatus.Text = FileUpdngProgBr.Maximum + Contants.STATUS_MSG + currentCount;
            FileUpdngProgBr.PerformStep();
        }
        void Record_Log(object sender, EventArgs e)
        {
            this.txtLog.Text = this.txtLog.Text + sender.ToString() + "\r\n";

        }
        static void ShowPopup(string msg)
        {
            Application.EnableVisualStyles();
            Application.Run(new MessagePopup(msg));
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string logPath = string.Empty;
            txtLog.Text = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(InfoStore.ReadDriveInfo()))
                {
                    MessageBox.Show("Please Select Required Drive/Folder for Patient Documents");
                    this.Cursor = Cursors.Default;
                    btnUpload.Enabled = true;
                    return;
                }
                tabContainer.SelectedTab = TabLog;
                FileUpdngProgBr.Maximum = 0;
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    BulkUploadController upLoaderController = new BulkUploadController();
                    BulkDocumentUploader uploader = new BulkDocumentUploader();
                    upLoaderController.FileUpLoaded += new EventHandler(upLoaderController_FileUpLoaded);
                    upLoaderController.LogControl += new EventHandler(Record_Log);
                    if (rdoAll.Checked)
                    {
                        DocumentsUploadResponse response  = upLoaderController.startUtility(true, string.Empty, null);
                        LblMsg.Visible = true;
                        LblMsg.Text = Contants.SUCESS_MSG_PREFIX + response.TotalFilesUploaded + Contants.SUCESS_MSG_SUFFIX;
                        logPath = InfoStore.SaveLog(txtLog.Text);
                        EmailController emailController = new EmailController();
                        emailController.sendEmail(response, logPath);
                    }
                    else if (rdoSelect.Checked)
                    {
                        try
                        {
                            string path = txtAddress.Text;
                            path = path + Contants.DIR_SEPRATOR;
                            string shortPath = path.Substring(3);
                            Document doc = new Document();
                            string[] documentData = shortPath.Split('\\');
                            doc.VendorDocuments = documentData[1];
                            doc.Pratice = documentData[2];
                            doc.Patient = documentData[3];
                            DocumentsUploadResponse response = upLoaderController.startUtility(false, path, doc);
                            LblMsg.Visible = true;
                            LblMsg.Text = Contants.SUCESS_MSG_PREFIX + response.TotalFilesUploaded + Contants.SUCESS_MSG_SUFFIX;
                            logPath = InfoStore.SaveLog(response, txtLog.Text);
                            if (response.IsError)
                            {
                                MessageBox.Show(response.ErrorMessage);
                            }
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            MessageBox.Show(ex.Message+"\n"+ "Selected path is invalid, please select valid path/drive to upload the patient documents.");
                        }
                    }
                }
                else
                {
                    LblMsg.Visible = true;
                    LblMsg.Text = Contants.NETWORK_UNAVILABLE;
                }
            }
            catch (Exception exp)
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                {
                    MessageBox.Show(Contants.NETWORK_UNAVILABLE);
                }
                else
                {
                    MessageBox.Show(exp.Message);
                }
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
            btnUpload.Enabled = true;
            this.Cursor = Cursors.Default;
        }


        #endregion

        #region schedular Methods
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            bool response = false;
            try
            {
                SchedularController schedularController = new SchedularController();
                if (!string.IsNullOrEmpty(TxtUserName.Text) && !string.IsNullOrEmpty(txtPwd.Text))
                {
                    Model.UtilityTask task = new Model.UtilityTask();
                    task.UserName = TxtUserName.Text;
                    task.Password = txtPwd.Text;
                    //if(schedularController.IsTaskCreated())
                        schedularController.AddTask(task);
                }
                else
                {
                    MessageBox.Show(Contants.INVALID_USER_NAME_AND_PASSWORD);
                    return;
                }
                if (!ChkActive.Checked && !ActiveChk.Checked)
                {
                    MessageBox.Show(Contants.INVALID_ACTIVE_ACTION);
                    return;
                }
                else
                {
                    if (ActiveChk.Checked || ChkActive.Checked)
                    {
                        Model.UtilityTrigger trigger = new Model.UtilityTrigger();
                        trigger.Type = 1;
                        DateTime time = DateTime.Parse(DailyTimeTxt.Text);
                        trigger.Hour = short.Parse(time.TimeOfDay.Hours.ToString());
                        trigger.Min = short.Parse(time.TimeOfDay.Minutes.ToString());
                        if (ChkActive.Checked)
                        {
                            trigger.Type = 2;
                            time = DateTime.Parse(WeeklyTime.Text);
                            trigger.Hour = short.Parse(time.TimeOfDay.Hours.ToString());
                            trigger.Min = short.Parse(time.TimeOfDay.Minutes.ToString());
                            if (WeeklyListBox.SelectedIndex == 0)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Sunday;
                            }
                            else if (WeeklyListBox.SelectedIndex == 1)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Monday;
                            }
                            else if (WeeklyListBox.SelectedIndex == 2)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Tuesday;
                            }
                            else if (WeeklyListBox.SelectedIndex == 3)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Wednesday;
                            }
                            else if (WeeklyListBox.SelectedIndex == 4)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Thursday;
                            }
                            else if (WeeklyListBox.SelectedIndex == 5)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Friday;
                            }
                            else if (WeeklyListBox.SelectedIndex == 6)
                            {
                                trigger.WeekDay = DaysOfTheWeek.Saturday;
                            }
                        }
                        trigger.UserName = TxtUserName.Text;
                        trigger.Password = txtPwd.Text;
                        response = schedularController.AddTrigger(trigger);
                        if (response)
                            MessageBox.Show("Schedule is created");
                    }
                }
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
                MessageBox.Show(Contants.ERROR_MSG);
            }
            
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.TxtUserName.Text = string.Empty;
            this.txtPwd.Text = string.Empty;
            ChkActive.Checked = false;
            ActiveChk.Checked = false;
        }

        private void loadTask()
        {
            try
            {
                SchedularController schedularController = new SchedularController();
                if (schedularController.IsTaskCreated())
                {
                    
                }
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
        }

        #endregion

        #region form methods

        private void Ondemand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabContainer.SelectedIndex == 2)
            {
                FolderCreationController folderController = new FolderCreationController();
                try
                {
                    if (!folderController.IsUserLogin())
                    {
                        Login loginForm = new Login();
                        loginForm.ShowDialog();
                        if (!folderController.IsUserLogin())
                        {
                            tabContainer.SelectedTab = tabOndemand;
                        }
                    }
                }
                catch (Exception exp)
                {
                    ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
                }
            }
        }

        private void btnBro_Click(object sender, EventArgs e)
        {
            patientFolderBrowser.ShowDialog();
            txtAddress.Text = patientFolderBrowser.SelectedPath;
        }

        private void rdoSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSelect.Checked)
            {
                this.btnBro.Enabled = true;
                rdoCurrentLog.Enabled = true;
                rdoByDate.Enabled = true;
                btnSendMail.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            patSearchList = null;
            searchList.Items.Clear();
            lstSelectPatient.Items.Clear();
            searchBox.Text = "";
        }

        #endregion

        #region Folder creation methods

        static List<PatSearch> patSearchList = null;

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FolderCreationController folderController = new FolderCreationController();
            List<PatSearch> patSearchList = null;
            try
            {
                char[] searchString = searchBox.Text.ToCharArray();
                if (searchString[2] == '-' || searchString[2] == '/')
                {
                    if (searchBox.Text.Length >= 8)
                    {
                        patSearchList = folderController.SearchPatient(searchBox.Text);
                        searchList.Items.Clear();
                        foreach (PatSearch pat in patSearchList)
                        {
                            searchList.Items.Add(pat);
                        }
                        searchList.Visible = true;
                        pnlClose.Visible = true;
                    }
                    else
                    {
                        searchList.Visible = false;
                        pnlClose.Visible = false;
                    }
                }
                else if (searchBox.Text.Length >= 6 && searchBox.Text.Length <= 10)
                {
                    patSearchList = folderController.SearchPatient(searchBox.Text);
                    searchList.Items.Clear();
                    foreach (PatSearch pat in patSearchList)
                    {
                        searchList.Items.Add(pat);
                    }
                    searchList.Visible = true;
                    pnlClose.Visible = true;
                }
                else if (!searchBox.Text.Contains(',') && !searchBox.Text.Contains(' '))
                {
                    if (searchBox.Text.Length == 3 || searchBox.Text.Length == 5 || searchBox.Text.Length == 7 || searchBox.Text.Length > 9)
                    {
                        patSearchList = folderController.SearchPatient(searchBox.Text);
                        searchList.Items.Clear();
                        foreach (PatSearch pat in patSearchList)
                        {
                            searchList.Items.Add(pat);
                        }
                        searchList.Visible = true;
                        pnlClose.Visible = true;
                    }
                    else
                    {
                        searchList.Visible = false;
                        pnlClose.Visible = false;
                    }
                }
                else if (searchBox.Text.Contains(' '))  //its a Name format Last, First
                {
                    string[] tok = searchBox.Text.Split(' ');
                    if (tok.Length == 2 || tok.Length == 3)
                    {
                        patSearchList = folderController.SearchPatient(searchBox.Text);
                        if (patSearchList != null)
                        {
                            searchList.Items.Clear();
                            foreach (PatSearch pat in patSearchList)
                            {
                                searchList.Items.Add(pat);
                            }
                            searchList.Visible = true;
                            pnlClose.Visible = true;
                        }
                        else
                        {
                            searchList.Visible = false;
                            pnlClose.Visible = false;
                        }
                    }
                    else
                    {
                        searchList.Visible = false;
                        pnlClose.Visible = false;
                    }
                }
                else if (searchBox.Text.Contains(','))
                {
                    string[] tok = searchBox.Text.Split(',');
                    if (tok.Length == 2 && (tok[0].Length >= 2 || tok[1].Length >= 2))
                    {
                        patSearchList = folderController.SearchPatient(searchBox.Text);
                        if (patSearchList != null)
                        {
                            searchList.Items.Clear();
                            foreach (PatSearch pat in patSearchList)
                            {
                                searchList.Items.Add(pat);
                            }
                            searchList.Visible = true;
                            pnlClose.Visible = true;
                        }
                        else
                        {
                            searchList.Visible = false;
                            pnlClose.Visible = false;
                        }
                    }

                }
                else
                {
                    searchList.Visible = false;
                    pnlClose.Visible = false;
                }
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
            this.Cursor = Cursors.Default; 
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderCreationController folderController = new FolderCreationController();
            try
            {
                folderController.Logout();
                tabContainer.SelectedTab = tabOndemand;
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            tabContainer.SelectedTab = TabLog;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DriveSelector sel = new DriveSelector(patSearchList, rdoAllPat.Checked);
                FolderCreationController.LogControl += new EventHandler(Record_Log);
                sel.ShowDialog();
               
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
                MessageBox.Show(exp.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void searchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PatSearch patSearch = (PatSearch)searchList.SelectedItem;
                lstSelectPatient.Items.Add(patSearch);
                lstSelectPatient.SetItemChecked(lstSelectPatient.Items.Count - 1, true);
                if (patSearchList == null)
                {
                    patSearchList = new List<PatSearch>();
                }
                patSearchList.Add(patSearch);
                lstSelectPatient.SelectedIndex = -1;
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }

        }

        private void lstSelectPatient_SelectORDeselectAll(bool chk)
        {
            for (int index = 0; index < lstSelectPatient.Items.Count; index++)
            {
                lstSelectPatient.SetItemChecked(index, chk);
            }
            if (chk)
            {
                patSearchList = null;
                for (int index = 0; index < lstSelectPatient.Items.Count; index++)
                {
                    if (patSearchList == null)
                    {
                        patSearchList = new List<PatSearch>();
                    }
                    PatSearch patSearch = (PatSearch)lstSelectPatient.Items[index];
                    patSearchList.Add(patSearch);
                }
            }
            else
                patSearchList = null;
        }

        private void lstSelectPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (lstSelectPatient.SelectedIndex != -1)
                {
                    if (patSearchList == null)
                    {
                        patSearchList = new List<PatSearch>();
                    }
                    PatSearch patSearch = (PatSearch)lstSelectPatient.SelectedItem;
                    bool selected = lstSelectPatient.GetItemChecked(lstSelectPatient.SelectedIndex);
                    if (selected)
                        patSearchList.Add(patSearch);
                    else
                        deletePatientFrmLst(patSearch);
                    lstSelectPatient.SelectedIndexChanged -= lstSelectPatient_SelectedIndexChanged;
                    lstSelectPatient.SelectedIndex = -1;
                    lstSelectPatient.SelectedIndexChanged += lstSelectPatient_SelectedIndexChanged;
                    
                }
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }

        }

        private void deletePatientFrmLst(PatSearch patSearch)
        {
            if (patSearchList != null)
            {
                patSearchList.Remove(patSearch);
            }
        }

        private void lstSelectPatient_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void lnkCloseSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            searchList.Visible = false;
            pnlClose.Visible = false;
        }

        private void ChkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lstSelectPatient_SelectORDeselectAll(true);
        }

        private void deselectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lstSelectPatient_SelectORDeselectAll(false);
        }

        #endregion

        private void CmbDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbDrive.SelectedIndex != -1)
            {
                InfoStore.SaveDriveInfo(CmbDrive.SelectedItem.ToString());
            }
        }

        private void rdoAllPat_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAll.Checked)
            {
                this.searchBox.Enabled = false;
                this.searchList.Enabled = false;
                this.lstSelectPatient.Enabled = false;
            }
        }

        private void rdoSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSelected.Checked)
            {
                this.searchBox.Enabled = true;
                this.searchList.Enabled = true;
                this.lstSelectPatient.Enabled = true;
                this.pnlClose.Enabled = true;
            }
        }

        private void rdoSelectGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string logPath = string.Empty;
                List<DocumentsUploadResponse> responseList = new List<DocumentsUploadResponse>();
                if (rdoSelectGroup.Checked)
                {
                    GroupUpload groupView = new GroupUpload();
                    groupView.ShowDialog();
                    if (Session.DocSearchList != null && Session.DocSearchList.Count > 0)
                    {
                        if (string.IsNullOrEmpty(InfoStore.ReadDriveInfo()))
                        {
                            MessageBox.Show("Please Select Required Drive/Folder for Patient Documents");
                            this.Cursor = Cursors.Default;
                            btnUpload.Enabled = true;
                            return;
                        }
                        tabContainer.SelectedTab = TabLog;
                        FileUpdngProgBr.Maximum = 0;
                        int fileCount = 0;
                        if (NetworkInterface.GetIsNetworkAvailable())
                        {
                            BulkUploadController upLoaderController = new BulkUploadController();
                            BulkDocumentUploader uploader = new BulkDocumentUploader();
                            upLoaderController.FileUpLoaded += new EventHandler(upLoaderController_FileUpLoaded);
                            upLoaderController.LogControl += new EventHandler(Record_Log);
                            txtLog.Text = string.Empty;
                            foreach (Document doc in Session.DocSearchList)
                            {
                                string path = createSelectedpath(doc);
                                path = path + Contants.DIR_SEPRATOR;
                                responseList.Add(upLoaderController.startUtility(false, path, doc));
                                if (responseList[responseList.Count - 1].IsError)
                                {
                                    MessageBox.Show(responseList[responseList.Count - 1].ErrorMessage);
                                }
                            }
                            LblMsg.Visible = true;
                            LblMsg.Text = Contants.SUCESS_MSG_PREFIX + InfoStore.ReadSummary().TotalFilesUploaded + Contants.SUCESS_MSG_SUFFIX;
                            logPath = InfoStore.SaveLog(txtLog.Text);
                            EmailController emailController = new EmailController();
                            emailController.sendEmail(responseList, logPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string createSelectedpath(Document doc)
        {
            string path = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\" + doc.VendorDocuments + "\\" +doc.Pratice + "\\" + doc.Patient;
            return path;
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            string logPath = string.Empty;
            try
            {
                DateTime mailDate = DateTime.Now;
                if(rdoByDate.Checked)
                {
                    mailDate = DateTime.Parse(dtpByDate.Text);
                }
                logPath = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\Bulk Upload Log\\Single Patient Uploaded Log\\Summary\\" + mailDate.ToShortDateString().Replace('/', '-') + ".txt";
                DocumentsUploadResponse response = InfoStore.ReadSummary(mailDate.ToShortDateString().Replace('/','-') + ".txt");
                EmailController emailController = new EmailController();
                emailController.sendEmail(response, logPath);
            }
            catch
            {
            }
        }

        private void rdoByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoByDate.Checked)
            {
                dtpByDate.Enabled = true;
            }
        }

        private void tabOndemand_Click(object sender, EventArgs e)
        {

        }

    }
}
