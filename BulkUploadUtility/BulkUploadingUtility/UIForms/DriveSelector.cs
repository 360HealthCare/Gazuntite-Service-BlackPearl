using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BulkUploadingUtility.Model;
using BulkUploadingUtility.Model.Response;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.BL;

namespace BulkUploadingUtility.UIForms
{
    public partial class DriveSelector : Form
    {
        static List<PatSearch> patSearchList = null;
        bool isAllSelected = false;
        public DriveSelector(List<PatSearch> patientSearchList, bool isAllPat)
        {
            InitializeComponent();
            combDrive.DataSource = DriveInfo.GetDrives();
            patSearchList = patientSearchList;
            combDrive.SelectedIndex = -1;
            combDrive.SelectedIndexChanged += combDrive_SelectedIndexChanged;
            isAllSelected = isAllPat;
        }


        private void combDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FolderCreationController folderController = new FolderCreationController();
            CreateFolderReponse response = null;
            if (combDrive.SelectedIndex > -1)
            {
                try
                {
                    if (patSearchList != null && !isAllSelected)
                    {
                        response = folderController.CreateFolder(patSearchList, combDrive.SelectedItem.ToString());
                        MessageBox.Show("Total folders created : " + response.TotalFolderCreated + "\nFolder already exists : " + response.TotalExistingFolder);
                        InfoStore.SaveDriveInfo(combDrive.SelectedItem.ToString());
                        this.Close();
                    }
                    else
                    {
                        response = folderController.CreateAllFolder(combDrive.SelectedItem.ToString());
                        MessageBox.Show("Total folders created : " + response.TotalFolderCreated + "\nFolder already exists : " + response.TotalExistingFolder);
                        InfoStore.SaveDriveInfo(combDrive.SelectedItem.ToString());
                        this.Close();
                    }
                    
                }
                catch (Exception exp)
                {
                    ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
                    MessageBox.Show(exp.Message);
                }
                this.Cursor = Cursors.Default;
            }
           
        }

        private void combDrive_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
