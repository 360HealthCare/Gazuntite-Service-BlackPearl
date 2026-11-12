using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Utility.Logger;
using System.Net;

namespace DocumentManager.Business.PathCreater
{
    public class ServerFileCreater
    {
        /// <summary>
        /// Author Sahibzada Sami ud din
        /// 7/20/2011
        /// This methos will create directory at FTP server.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        /// 
        public bool MakeDirectory(string root, string path, string ftpAddress, string ftpUser, string ftpPassword)
        {
            bool response = false;
            string practiceName = string.Empty;
            string stationName = string.Empty;
            string patientid = string.Empty;
            string sectionName = string.Empty;
            try
            {
                string[] pathInfo = path.Split('|');
                practiceName = pathInfo[0];
                stationName = pathInfo[1];
                patientid = pathInfo[2];
                sectionName = pathInfo[3]; ;
                string directory = ftpAddress + "/" + root;
                LiveProviderLogger.Info("Calling Make Directory");
                CreateDirectory(directory, ftpUser, ftpPassword);
                directory = directory + "/" + practiceName;
                CreateDirectory(directory, ftpUser, ftpPassword);
                directory = directory + "/" + stationName;
                CreateDirectory(directory, ftpUser, ftpPassword);
                directory = directory + "/" + patientid;
                CreateDirectory(directory, ftpUser, ftpPassword);
                directory = directory + "/" + sectionName;
                CreateDirectory(directory, ftpUser, ftpPassword);
                response = true;
            }
            catch
            {
                throw;
            }
            return response;
        }


        private bool CreateDirectory(string directory, string ftpUser, string ftpPassword)
        {
            LiveProviderLogger.Info("Calling CreateDirectory");
            bool response = false;
            FtpWebResponse ftpResponse = null;
            FtpWebRequest request = null;
            try
            {
                request = CreateRequestObj(directory, ftpUser, ftpPassword);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpResponse = (FtpWebResponse)request.GetResponse();
                response = true;
                ftpResponse.Close();
            }
            catch (WebException)
            {
                
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 07/20/2011
        /// This methos will create request object.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>

        public FtpWebRequest CreateRequestObj(string directory, string ftpUser, string ftpPassword)
        {
            LiveProviderLogger.Info("Calling Create Request Obj");
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directory);
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                return request;
            }
            catch
            {
                throw;
            }
        }
    }
}
