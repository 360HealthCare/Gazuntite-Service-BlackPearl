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
using BulkUploadingUtility.Model;

namespace BulkUploadingUtility.UIForms
{
    public partial class GroupUpload : Form
    {
        static List<Document> docSearchList = null;

        public GroupUpload()
        {
            InitializeComponent();
        }

        private void ChkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lstSelectPatient_SelectORDeselectAll(true);
        }

        private void lstSelectPatient_SelectORDeselectAll(bool chk)
        {
            for (int index = 0; index < lstSelectPatient.Items.Count; index++)
            {
                lstSelectPatient.SetItemChecked(index, chk);
            }
            if (chk)
            {
                docSearchList = null;
                for (int index = 0; index < lstSelectPatient.Items.Count; index++)
                {
                    if (docSearchList == null)
                    {
                        docSearchList = new List<Document>();
                    }
                    Document docSearch = (Document)lstSelectPatient.Items[index];
                    docSearchList.Add(docSearch);
                }
            }
            else
                docSearchList = null;
        }

        private void deselectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lstSelectPatient_SelectORDeselectAll(false);
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FolderCreationController folderController = new FolderCreationController();
            List<Document> patSearchList = null;
            try
            {
                if (searchBox.Text.Length > 2)
                {
                    
                    char[] searchString = searchBox.Text.ToCharArray();
                    if (searchString[2] == '-' || searchString[2] == '/')
                    {
                        if (searchBox.Text.Length >= 8)
                        {
                            patSearchList = folderController.SearchPatientFolder("*" + searchBox.Text + "*");
                            searchList.Items.Clear();
                            foreach (Document doc in patSearchList)
                            {
                                searchList.Items.Add(doc);
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
                        patSearchList = folderController.SearchPatientFolder("*" + searchBox.Text + "*");
                        searchList.Items.Clear();
                        foreach (Document doc in patSearchList)
                        {
                            searchList.Items.Add(doc);
                        }
                        searchList.Visible = true;
                        pnlClose.Visible = true;
                    }
                    else if (!searchBox.Text.Contains(',') && !searchBox.Text.Contains(' '))
                    {
                        if (searchBox.Text.Length == 3 || searchBox.Text.Length == 5 || searchBox.Text.Length == 7 || searchBox.Text.Length > 9)
                        {
                            patSearchList = folderController.SearchPatientFolder("*" + searchBox.Text + "*");
                            searchList.Items.Clear();
                            foreach (Document doc in patSearchList)
                            {
                                searchList.Items.Add(doc);
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
                            patSearchList = folderController.SearchPatientFolder("*" + searchBox.Text + "*");
                            if (patSearchList != null)
                            {
                                searchList.Items.Clear();
                                foreach (Document doc in patSearchList)
                                {
                                    searchList.Items.Add(doc);
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
                            patSearchList = folderController.SearchPatientFolder("*" + searchBox.Text + "*");
                            if (patSearchList != null)
                            {
                                searchList.Items.Clear();
                                foreach (Document doc in patSearchList)
                                {
                                    searchList.Items.Add(doc);
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
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
            this.Cursor = Cursors.Default; 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            docSearchList = null;
            searchList.Items.Clear();
            lstSelectPatient.Items.Clear();
            searchBox.Text = "";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Session.DocSearchList = docSearchList;
            this.Close();
        }

        private void searchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Document patSearch = (Document)searchList.SelectedItem;
                lstSelectPatient.Items.Add(patSearch);
                lstSelectPatient.SetItemChecked(lstSelectPatient.Items.Count - 1, true);
                if (docSearchList == null)
                {
                    docSearchList = new List<Document>();
                }
                docSearchList.Add(patSearch);
                lstSelectPatient.SelectedIndex = -1;
            }
            catch (Exception exp)
            {
                ServiceOperator.ManageLog(exp.Message, exp.StackTrace, true);
            }
        }

        private void lnkCloseSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            searchList.Visible = false;
            pnlClose.Visible = false;
        }
    }
}
