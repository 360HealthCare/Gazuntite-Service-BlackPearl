using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using QuivusModel.Communication.MailDTO;
namespace Communication.Business.EmailManager
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 1 December 2011
    /// Description: This interface contains functions related to Email 
    /// </summary>
   public interface IEmailManager
    {

       string SendEmail(EmailDTO oEmailDTO);

      EmailDTO GetEmail(int userEmailID, int userID, int clinicBrachID, int stationID);

      string DeleteEmail(string iUserEmailID, string userID);

      string DeleteSentEmail(string iUserEmailID, string userID);

       MailDTO GetAllEmails(int userID, int clinicID, int stationID,int pageSize,int currentPage);

       MailDTO GetAllSentMails(int userID, int clinicID, int stationID, int pageSize, int currentPage);

       Stream DownloadFile(string sUserEmailID, string sFileName, string sfileExtension);

       string UploadFile(string userID, string sFileName, Stream sFileStream);

       int[] GetEmailSenderList(string userID);
    }
}
