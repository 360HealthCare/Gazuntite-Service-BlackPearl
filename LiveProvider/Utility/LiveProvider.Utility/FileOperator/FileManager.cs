using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LiveProvider.Utility.Constant.ProjectConstant;
//using System.Drawing.Imaging;
//using System.Drawing;
using LiveProvider.Utility.Logger;

namespace LiveProvider.Utility.FileOperator
{
    /// <summary>
    /// Author: Sahibzada Sami ud Din
    /// Date:08/07/2011
    /// This class will do file management Operation
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// This method will handle file operation
        /// </summary>
        /// <param name="FileContents"></param>
        /// <param name="FileName"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public static bool FileHandler(Stream FileContents, string patientId, string sectionName, string documentVirtualName, string fileName, string practice, string station, string documentRootDirectory)
        {
            LiveProvider.Utility.Logger.LiveProviderLogger.Info("Calling FileHandler");
            bool Response = false;
            string FileDirectory = string.Empty;
            try
            {
                FileDirectory = DirectoryCreator(patientId, sectionName, documentVirtualName, fileName, practice, station, documentRootDirectory);
                if (!string.IsNullOrEmpty(FileDirectory))
                {
                   Response = ConvertAndSave(FileContents, FileDirectory);
                }
            }
            catch
            {
                throw;
            }
            return Response;
        }
        /// <summary>
        /// This method will do directory operation
        /// </summary>
        /// <returns></returns>
        public static string DirectoryCreator(string patientId, string sectionName, string documentVirtualName, string fileName, string practice, string station, string documentRootDirectory)
        {
            LiveProvider.Utility.Logger.LiveProviderLogger.Info("Calling DirectoryCreator");
            string fileExtension = string.Empty;
            string FileDirectory = string.Empty;
            try
            {
                fileExtension = Path.GetExtension(fileName);
                if (!Directory.Exists(documentRootDirectory))
                {
                    Directory.CreateDirectory(documentRootDirectory);
                }
                if (Directory.Exists(documentRootDirectory))
                {
                    FileDirectory = documentRootDirectory + DocumentManagerContants.VENDOR_NAME + DocumentManagerContants.DOCUMENT_DIRECTOY_SAPERATOR + practice + DocumentManagerContants.DOCUMENT_DIRECTOY_SAPERATOR + station + DocumentManagerContants.DOCUMENT_DIRECTOY_SAPERATOR + patientId + DocumentManagerContants.DOCUMENT_DIRECTOY_SAPERATOR
                        + sectionName;
                    if (!Directory.Exists(FileDirectory))
                    {
                        Directory.CreateDirectory(FileDirectory);
                    }
                    FileDirectory = FileDirectory + DocumentManagerContants.DOCUMENT_DIRECTOY_SAPERATOR + documentVirtualName + fileExtension;
                }
            }
            catch
            {
                throw;
            }
            return FileDirectory;
        }
        /// <summary>
        /// This method convert and save file
        /// </summary>
        /// <param name="FileContents"></param>
        /// <param name="FileDirectory"></param>
        /// <returns></returns>
        public static bool ConvertAndSave(Stream FileContents, string FileDirectory)
        {
            LiveProvider.Utility.Logger.LiveProviderLogger.Info("Calling ConvertAndSave");
            bool Response = false;
            FileStream fs = new FileStream(FileDirectory, FileMode.Create);
            try
            {
                byte[] buffer = new byte[DocumentManagerContants.DOCUMENT_MANAGEMENT_DOC_SIZE];
                int bytesRead;
                BinaryWriter writer = null;
                writer = new BinaryWriter(fs);
                long totalBytesRead = 0;
                do
                {
                    bytesRead = FileContents.Read(buffer, 0, buffer.Length);
                    writer.Write(buffer);
                    totalBytesRead += bytesRead;
                } 
                while (bytesRead > 0);
                if (totalBytesRead > 0)
                {
                    Response = true;
                }
                fs.Close();
                FileContents.Close();
            }
            catch
            {
                throw;
            }
            return Response;
        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 23/8/2011
        /// This Method will save server path
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="sectionId"></param>
        /// <param name="documentVirtualName"></param>
        /// <param name="fileName"></param>
        /// <param name="practice"></param>
        /// <param name="station"></param>
        /// <returns></returns>

        public static string ServerFileCreator(string patientId, string sectionName, string documentVirtualName, string fileName, string practice, string station, string documentHostName)
        {
            LiveProvider.Utility.Logger.LiveProviderLogger.Info("Calling DirectoryCreator");
            string fileExtension = string.Empty;
            string FileDirectory = string.Empty;
            try
            {
                fileExtension = Path.GetExtension(fileName);
                FileDirectory = documentHostName  + DocumentManagerContants.VENDOR_NAME + DocumentManagerContants.DOCUMENT_HOST_SAPERATOR + practice + DocumentManagerContants.DOCUMENT_HOST_SAPERATOR + station + DocumentManagerContants.DOCUMENT_HOST_SAPERATOR + patientId + DocumentManagerContants.DOCUMENT_HOST_SAPERATOR
                        + sectionName + DocumentManagerContants.DOCUMENT_HOST_SAPERATOR;
                FileDirectory = FileDirectory  + documentVirtualName + fileExtension;
            }
            catch
            {
                throw;
            }
            return FileDirectory;
        }

        public static bool CheckAndSaveImage(string fileName, Stream FileContents,string path)
        {
            bool response = false;
            //ImageFormat format;
            try
            {
            //    string FileExtension = Path.GetExtension(fileName).ToLower();
            //    LiveProviderLogger.Info("FileExtension :" + FileExtension);
            //    switch (FileExtension)
            //    { 
            //        case ".bmp":
            //            response=true;
            //            format=ImageFormat.Bmp;
            //            break;
            //        case ".jpg":
            //            response=true;
            //            format=ImageFormat.Jpeg;
            //            break;
            //        case ".png":
            //            response = true;
            //            format=ImageFormat.Png;
            //        break;
            //        case ".gif":
            //            response = true;
            //            format=ImageFormat.Gif;
            //        break;
            //        case ".tiff":
            //            response = true;
            //            format=ImageFormat.Tiff;
            //        break;  
                //}
                //if (response)
                //{
                //    Bitmap img = new Bitmap(FileContents);
                //    img.Save(path);
                //}
               
            }
            catch(Exception exp)
            {
                LiveProviderLogger.Info(exp.Message+exp.InnerException+exp.ToString());
                throw;
            }

            return response;
        }
    }
}
