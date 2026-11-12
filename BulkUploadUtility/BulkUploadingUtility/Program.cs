using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BulkUploadingUtility.PopUp;
using BulkUploadingUtility.Model;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.BL;
using BulkUploadingUtility.UIForms;
using System.Net.NetworkInformation;


namespace BulkUploadingUtility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
       
    }
}
