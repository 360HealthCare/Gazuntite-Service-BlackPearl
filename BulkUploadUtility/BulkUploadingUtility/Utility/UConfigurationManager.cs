using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BulkUploadingUtility.Model;

namespace BulkUploadingUtility.Utility
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 07/20/2011
    /// This class will manage comfigurations 
    /// </summary>
   
    public class UConfigurationManager
    {

        public static string GetHostName
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.FTP_HOST].ToString();
            }
        }

        public static string FTPUserName
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.FTP_USER_NAME].ToString();
            }
        }

        public static string FTPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.FTP_PASSWORD].ToString();
            }
        }

        public static string Root
        {
            get
            {
                return @"DocumentManagement\";
            }
        }

        public static string ApplicationExeNameAndPathUrl
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            }
        }

        public static string TaskName
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.TASK_NAME].ToString();
            }
        }

        public static string VendorName
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.VENDOR_NAME].ToString();
            }
        }

        public static string OptionalRoot
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.OPTIONAL_ROOT].ToString();
            }
        }
        public static string[] Recipients
        {
            get
            {
                return ConfigurationManager.AppSettings[Contants.RECIPIENTS].ToString().Split(',');
            }
        }
    }
}
