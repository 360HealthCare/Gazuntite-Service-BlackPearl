using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.BL;
using BulkUploadingUtility.UserManagerService;

namespace BulkUploadingUtility.UIForms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            cmbPractice.DisplayMember = "ClinicBranchName";
            cmbPractice.ValueMember = "ClinicBranchIdSeq";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FolderCreationController folderController = new FolderCreationController();
            List<ClinicBranchDTO> response = null;
            try
            {
                response = folderController.Login(txtProviderID.Text, txtPassword.Text);
                if (response.Count > 1)
                {
                    cmbPractice.DataSource = response;
                    cmbPractice.Visible = true;
                    lblPractice.Visible = true;
                }
                else if (response.Count == 1)
                {
                    ClinicBranchDTO clinicBranch = response[0];
                    if (clinicBranch != null)
                    {
                        folderController.SetCurrentPractice(clinicBranch.ClinicBranchIdSeq);
                    }
                    this.Close();
                }
                else if (response.Count == 0)
                {
                    MessageBox.Show(Contants.INVALID_USER_OR_PASSWORD);
                }
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
                MessageBox.Show(Contants.INVALID_USER_OR_PASSWORD);
            }
            this.Cursor = Cursors.Default;
        }

        private void cmbPractice_SelectedIndexChanged(object sender, EventArgs e)
        {
            FolderCreationController folderController = new FolderCreationController();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (cmbPractice.SelectedIndex > 0)
                {
                    folderController.SetCurrentPractice(int.Parse(cmbPractice.SelectedValue.ToString()));
                    this.Close();
                }
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
            this.Cursor = Cursors.Default;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(sender, e);
            }
        }

       

        
    }
}
