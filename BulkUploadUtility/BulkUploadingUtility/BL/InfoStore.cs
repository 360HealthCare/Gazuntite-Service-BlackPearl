using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.Model.Response;

namespace BulkUploadingUtility.BL
{
    public class InfoStore
    {
        static string fileName = "driveInfo.txt";

        public static void SaveDriveInfo(string driveName)
        {
            try
            {
                StreamWriter SW;
                SW = File.CreateText(fileName);
                SW.WriteLine(driveName);
                SW.Close();
            }
            catch
            {
                throw;
            }
        }

        public static string ReadDriveInfo()
        {
            string driveName = string.Empty;
            try
            {
                StreamReader SR = null;
                if (File.Exists(fileName))
                {
                    SR = File.OpenText(fileName);
                    driveName = SR.ReadLine();
                    SR.Close();
                }
                
            }
            catch
            {
                throw;
            }
            return driveName;
        }

        public static string SaveLog(string content)
        {
            string filePath = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\Bulk Upload Log\\";
            string fileName = DateTime.Now.ToShortDateString().Replace('/','-')+ " " + DateTime.Now.ToShortTimeString().Replace(':',' ') + ".txt";
            try
            {
                StreamWriter SW;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                SW = File.CreateText(filePath + fileName);
                SW.Write(content);
                SW.Close();
            }
            catch
            {
                throw;
            }
            return filePath + fileName;
        }

        public static string SaveLog(DocumentsUploadResponse uploadedRespose, string content)
        {
            DocumentsUploadResponse preResponse = ReadSummary();
            uploadedRespose = _prepareSummary(preResponse, uploadedRespose);
            _saveSingleUploadSummary(uploadedRespose);
            string filePath = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\Bulk Upload Log\\Single Patient Uploaded Log\\";
            string fileName = DateTime.Now.ToShortDateString().Replace('/', '-') + ".txt";
            try
            {
                StreamWriter SW;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (!File.Exists(filePath + fileName))
                {
                    SW = File.CreateText(filePath + fileName);
                    SW.Write(content);
                    SW.Close();
                }
                else
                {
                    SW = File.AppendText(filePath + fileName);
                    SW.Write(content);
                    SW.Close();
                }
                 
            }
            catch
            {
                throw;
            }
            return filePath + fileName;
        }

        private static DocumentsUploadResponse _prepareSummary(DocumentsUploadResponse preResponse, DocumentsUploadResponse uploadedRespose)
        {
            try
            {
                uploadedRespose.TotalPatientScaned += preResponse.TotalPatientScaned;
                uploadedRespose.TotalPatientUploaded += preResponse.TotalPatientUploaded;
                uploadedRespose.TotalFilesUploaded += preResponse.TotalFilesUploaded;
            }
            catch
            {
                throw;
            }
            return uploadedRespose;
        }

        private static void _saveSingleUploadSummary(DocumentsUploadResponse uploadedRespose)
        {
            string filePath = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\Bulk Upload Log\\Single Patient Uploaded Log\\Summary\\";
            string fileName = DateTime.Now.ToShortDateString().Replace('/', '-') + ".txt";
            try
            {
                StreamWriter SW;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                SW = File.CreateText(filePath + fileName);
                SW.WriteLine("Total Patients Scanned :" + uploadedRespose.TotalPatientScaned);
                SW.WriteLine("Total Patients Uploaded :" + uploadedRespose.TotalPatientUploaded);
                SW.WriteLine("Total Documents Uploaded :" + uploadedRespose.TotalFilesUploaded);
                SW.Close();
            }
            catch
            {
                throw;
            }
            return;
        }

        public static DocumentsUploadResponse ReadSummary()
        {
            DocumentsUploadResponse response = null;
            try
            {
                response = new DocumentsUploadResponse();
                StreamReader reader = null;
                string filePath = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\Bulk Upload Log\\Single Patient Uploaded Log\\Summary\\";
                string file = DateTime.Now.ToShortDateString().Replace('/', '-') + ".txt";
                if (File.Exists(filePath + file))
                {
                    reader = File.OpenText(filePath + file);
                    string[] values = reader.ReadLine().Split(':');
                    response.TotalPatientScaned = int.Parse(values[1]);
                    values = reader.ReadLine().Split(':');
                    response.TotalPatientUploaded = int.Parse(values[1]);
                    values = reader.ReadLine().Split(':');
                    response.TotalFilesUploaded = int.Parse(values[1]);
                    reader.Close();
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public static DocumentsUploadResponse ReadSummary(string fileAddress)
        {
            DocumentsUploadResponse response = null;
            try
            {
                response = new DocumentsUploadResponse();
                StreamReader reader = null;
                string filePath = InfoStore.ReadDriveInfo() + UConfigurationManager.Root + "\\Bulk Upload Log\\Single Patient Uploaded Log\\Summary\\";
                if (File.Exists(filePath + fileAddress))
                {
                    reader = File.OpenText(filePath + fileAddress);
                    string[] values = reader.ReadLine().Split(':');
                    response.TotalPatientScaned = int.Parse(values[1]);
                    values = reader.ReadLine().Split(':');
                    response.TotalPatientUploaded = int.Parse(values[1]);
                    values = reader.ReadLine().Split(':');
                    response.TotalFilesUploaded = int.Parse(values[1]);
                    reader.Close();
                }
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
