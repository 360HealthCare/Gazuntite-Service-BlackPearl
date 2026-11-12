using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.Model;
using System.IO;
using BulkUploadingUtility.BL;

namespace BulkUploadingUtility.Utility
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 07/20/2011
    /// This class will create uri on different level
    /// </summary>

    public class DirectoryOperation
    {

        public static string makeDirectoryUrl(Document doc)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments + Contants.SEPRATOR + doc.Pratice /*+ Contants.SEPRATOR + doc.Station*/ + Contants.SEPRATOR +
                doc.PatientID;
            if(!string.IsNullOrEmpty(doc.SubSection))
                directoryUrl = directoryUrl + Contants.SEPRATOR + doc.SubSection;
            else
                directoryUrl = directoryUrl + Contants.SEPRATOR + doc.Section;
            return directoryUrl;
        }

        public static string makeDirectoryUrlForUpload(Document doc)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments + Contants.SEPRATOR + doc.Pratice /*+ Contants.SEPRATOR + doc.Station*/ + Contants.SEPRATOR +
                doc.PatientID + Contants.SEPRATOR + doc.Section + Contants.SEPRATOR;
            return directoryUrl;
        }
        public static string makeDirectoryUrlFstLvl(Document doc)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments;
            return directoryUrl;
        }

        public static string makeDirectoryUrlSecLvl(Document doc)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments+ Contants.SEPRATOR + doc.Pratice;
            return directoryUrl;
        }

        public static string makeDirectoryUrlThrdLvl(Document doc)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments + Contants.SEPRATOR + doc.Pratice/*+ Contants.SEPRATOR + doc.Station*/;
            return directoryUrl;
        }

        public static string makeDirectoryUrlFourthLvl(Document doc)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments + Contants.SEPRATOR + doc.Pratice /*+ Contants.SEPRATOR + doc.Station*/ +
                Contants.SEPRATOR + doc.PatientID;
            return directoryUrl;
        }


        public static string MakeFullUrl(Document doc, UploadedFileInfo file)
        {
            string directoryUrl = UConfigurationManager.GetHostName + doc.VendorDocuments + Contants.SEPRATOR + doc.Pratice /*+ Contants.SEPRATOR + doc.Station*/ + Contants.SEPRATOR +
                doc.PatientID;
            if (!string.IsNullOrEmpty(doc.SubSection))
                directoryUrl = directoryUrl + Contants.SEPRATOR + doc.SubSection;
            else
                directoryUrl = directoryUrl + Contants.SEPRATOR + doc.Section;
            directoryUrl=directoryUrl + Contants.SEPRATOR + MakeFileName(file);
            return directoryUrl;
        }

        public static string MakeArchive(Document doc, UploadedFileInfo file)
        {
            string directoryUrl = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + doc.VendorDocuments + Contants.DIR_SEPRATOR + doc.Pratice + Contants.DIR_SEPRATOR + doc.Patient + Contants.DIR_SEPRATOR + "Archive";
            return directoryUrl;
        }

        public static string MakeArchiveWithFile(Document doc, UploadedFileInfo file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + System.DateTime.Now.ToFileTimeUtc().ToString() + Path.GetExtension(file.FileName);
            string directoryUrl = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + doc.VendorDocuments + Contants.DIR_SEPRATOR + doc.Pratice
                + Contants.DIR_SEPRATOR + doc.Patient + Contants.DIR_SEPRATOR + "Archive" + Contants.DIR_SEPRATOR + fileName;
            return directoryUrl;
        }

        public static string MakeDirectoryAddress(Document doc, UploadedFileInfo file)
        {
            string root = string.Empty;
            
            root = InfoStore.ReadDriveInfo() + UConfigurationManager.Root;

            string directoryAddress = root + doc.VendorDocuments + Contants.DIR_SEPRATOR + doc.Pratice /*+ Contants.DIR_SEPRATOR + doc.Station*/ + Contants.DIR_SEPRATOR +
                doc.Patient + Contants.DIR_SEPRATOR + doc.Section;
            if (!string.IsNullOrEmpty(directoryAddress))
                directoryAddress = directoryAddress + Contants.DIR_SEPRATOR + doc.SubSection;
            directoryAddress = directoryAddress + Contants.DIR_SEPRATOR + file.FileName;
            return directoryAddress;
        }

        public static string MakeDirectoryUrl(Document doc, UploadedFileInfo file)
        {
            string directoryAddress =  doc.VendorDocuments + Contants.DIR_SEPRATOR + doc.Pratice /*+ Contants.DIR_SEPRATOR + doc.Station*/ + Contants.DIR_SEPRATOR +
                doc.Patient + Contants.DIR_SEPRATOR + doc.Section + Contants.DIR_SEPRATOR + MakeFileName(file);
            return directoryAddress;
        }

        private static string MakeFileName(UploadedFileInfo file)
        {
            Guid docVirtualName = file.VirtualFileName;
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = docVirtualName + fileExtension;
            return fileName;
        }

    }
}
