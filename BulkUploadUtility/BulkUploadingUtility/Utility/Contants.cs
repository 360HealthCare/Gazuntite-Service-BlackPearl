using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkUploadingUtility.Utility
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 7/20/2011
    /// This class will save contants
    /// </summary>

    public static class Contants
    {
        public static string FTP_USER_NAME = "FtpUserName";

        public static string FTP_HOST = "FtpHost";

        public static string FTP_PASSWORD = "FtpPassword";

        public static string SEPRATOR = "/";

        public static string ROOT = "root";

        public static int DOCUMENT_MANAGEMENT_DOC_SIZE = 4096;

        public static string DIR_SEPRATOR = @"\";

        public static string NETWORK_UNAVILABLE = "You are not connected to network please check your network conncetion";

        public static string SUCESS_MSG_PREFIX = "Files uploaded sucessfully\n";

        public static string SUCESS_MSG_SUFFIX = " files is uploaded to server";

        public static string ERROR_MSG = "Utility encounter problem please contact support desk";

        public static string ACTIVE_INVAILD_MSG = "Please active schedule time";

        public static string TASK_NAME = "TaskName";

        public static string TASK_CREATED_MSG = "Task is created";

        public static string INVALID_USER_NAME_AND_PASSWORD = "Please enter user name and password";

        public static string INVALID_ACTIVE_ACTION = "Please active Schedular";

        public static string STATUS_MSG = " of ";


        public static string INVALID_USER_OR_PASSWORD = "Invalid user name or password.";


        public static string INVALID_SELECTED_MSG = "Please select one or more patients";

        public static string VENDOR_NAME = "VendorName";

        public static string OPTIONAL_ROOT = "OptionalRoot";

        public static string RECIPIENTS = "Recipients";
    }
}
