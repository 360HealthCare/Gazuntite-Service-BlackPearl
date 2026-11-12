using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.Model.Response;

namespace BulkUploadingUtility.BL
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 16 Jan 2012
    /// This method will use for sending emails
    /// </summary>
    public class EmailController
    {
        public bool sendEmail(List<string> feilds, bool isSucess)
        {
            DmsEmailService.DocumentManagerServiceClient client = new DmsEmailService.DocumentManagerServiceClient();
            bool response = false;
            try
            {
                response = client.SendEmails(UConfigurationManager.Recipients.ToList<string>(), feilds, isSucess);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool sendEmail(DocumentsUploadResponse uploadResponse, string path)
        {
            bool response = false;
            bool isSucess = false;
            try
            {
                List<string> feilds = new List<string>();
                if (uploadResponse.IsError)
                {
                    feilds.Add(uploadResponse.ErrorMessage);
                    feilds.Add(DateTime.Now.ToShortDateString());
                    feilds.Add(DateTime.Now.ToShortTimeString());
                    feilds.Add(uploadResponse.TotalPatientScaned.ToString());
                    feilds.Add(uploadResponse.TotalPatientUploaded.ToString());
                    feilds.Add(uploadResponse.TotalFilesUploaded.ToString());
                    feilds.Add(path);
                }
                else
                {
                    feilds.Add(DateTime.Now.ToShortDateString());
                    feilds.Add(DateTime.Now.ToShortTimeString());
                    feilds.Add(uploadResponse.TotalPatientScaned.ToString());
                    feilds.Add(uploadResponse.TotalPatientUploaded.ToString());
                    feilds.Add(uploadResponse.TotalFilesUploaded.ToString());
                    feilds.Add(path);
                    isSucess = true;
                }
                sendEmail(feilds, isSucess);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool sendEmail(List<DocumentsUploadResponse> uploadResponseList, string path)
        {
            bool response = false;
            try
            {
                DocumentsUploadResponse uploadResponse = new DocumentsUploadResponse();
                foreach (DocumentsUploadResponse obj in uploadResponseList)
                {
                    uploadResponse.TotalFilesUploaded += obj.TotalFilesUploaded;
                    uploadResponse.TotalPatientScaned += obj.TotalPatientScaned;
                    uploadResponse.TotalPatientUploaded += obj.TotalPatientUploaded;
                    if (obj.IsError)
                    {
                        uploadResponse.IsError = obj.IsError;
                        uploadResponse.ErrorMessage = obj.ErrorMessage;
                    }
                }
                response = sendEmail(uploadResponse, path);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
