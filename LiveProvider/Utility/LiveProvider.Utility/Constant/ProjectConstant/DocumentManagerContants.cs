using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LiveProvider.Utility.Constant.ProjectConstant
{
    /// <summary>
    /// Author:Sahibzada Sami ud din
    /// Date:05/07/2011
    /// </summary>
    public class DocumentManagerContants
    {
        public static DateTime DATE_MINIMUM = new DateTime(1901, 01, 01, 12, 0, 0);
        public const string RECORD_NOT_INSERTED_ERROR_MSG = "Record Not Inserted.";
        public const string PARAMETER_POPULATOR_ERROR_MSG = "Parameters exception";
        public static string DOCUMENT_MANAGEMENT_DIRECTORY = @"D:\ftp\";
        public static int DOCUMENT_MANAGEMENT_DOC_SIZE = 0x10000;
        public static int DOCUMENT_PAGE_SIZE = 200;
        public static string DOCUMENT_DIRECTOY_SAPERATOR = @"\";

        public static string NAME_SPACE = " ";
        public static string PATIENT_DOCUMENT_NAME_FE = "Patient Name";

        public static int DATE_SEARCH_VALUE = 3;

        public static string DOCUMENT_FILE_EXT_SAPERATOR = ".";

        public static string DOCUMENT_HOST_NAME_ = "http://192.168.0.63:82/Documents/";

        public static string DOCUMENT_HOST_SAPERATOR = "/";

        public static object TEMP_VALUE = "";

        public static string VENDOR_NAME = "Quivus";

        public static string DOCUMENT_UPLAOD_EMAIL_TEMPLATE = "DocumentUpload";

        public static string DOCUMENT_UPLAOD_EMAIL_FAILURE = "BulkUploadFailure";
    }
}