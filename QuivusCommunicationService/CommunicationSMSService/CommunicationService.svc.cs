using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using QuivusUtility.Utilities;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using Communication.Business;
using Quivus.Utility.Factory;
using System.Web.Routing;
using CommunicationDataAccess;
using Communication.Business.AlertManager.AlertManagerImp;
using Communication.Business.EmailManager.EmailManagerImp;
using QuivusModel.Communication.MailDTO;
using System.Net;
using System.IO;
using System.Web.Hosting;
//using QuivusCommunicationService.UserManagerService;
using QuivusModel.Communication;
using Communication.Business.EmailManager;
using Communication.Business.AlertManager;
using Communication.Business.SMSManager;
using QuivusModel.Communication.SMSDTO;
using System.Xml;
using System.Xml.Serialization;
using QuivusModel.Communication.AlertsDTO;
namespace QuivusCommunicationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    /// <summary>
    ///Author: Zahid Mehmood
    /// Date: 1 December 2011 
    /// </summary>

   public partial class CommunicationService : ICommunicationService
    {

        private IAlertManager _alertManager;
        private IEmailManager _emailManager;
        private ISMSManager _smsManager;

        #region SMS 

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 1 December 2011
        /// </summary>
        /// <param name="oSMSLogDTO"></param>
        /// <returns></returns>
        public string SendSMS(XmlDocument xmlSmsDTO)
        {
            SMSLogDTO smsLogDTO=null;

            int iResult = 0;
            try
            {

                #region Deserialize SMS
                XmlDocument doc = new XmlDocument();
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "SMS";
                xRoot.IsNullable = true;
                XmlSerializer serializer = new XmlSerializer(typeof(SMSLogDTO), xRoot);
                string xml = xmlSmsDTO.InnerXml;


                smsLogDTO = (SMSLogDTO)serializer.Deserialize(new StringReader(xml));
                #endregion Deserialize SMS

                if (_smsManager == null)
                {
                    _smsManager = (SMSManagerImp)DAOFactory.GetDataAccessObject(typeof(SMSManagerImp));
                }
                //smsLogDTO.ClinicBranchID = 1;
                //smsLogDTO.CreatedBy = 597;
                //smsLogDTO.IsDeleted = false;
                //smsLogDTO.PhoneNumber = "123465789;1234567890";
                //smsLogDTO.SMSTemplateID = 0;
                //smsLogDTO.Text = "Test";
                //smsLogDTO.UserId = 470;

            return _smsManager.SendSMS(smsLogDTO);
            // return iResult.ToString();
            }
           
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return iResult.ToString();
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 January 2012
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clinicBranchID"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public SMSResponseDTO GetSMSLog(string userID, string clinicBranchID, string currentPage)
        {
            SMSResponseDTO smsLogDTO = null;

            int iResult = 0;
            try
            {
                if (_smsManager == null)
                {
                    _smsManager = (SMSManagerImp)DAOFactory.GetDataAccessObject(typeof(SMSManagerImp));
                }
                //smsLogDTO.ClinicBranchID = 1;
                //smsLogDTO.CreatedBy = 597;
                //smsLogDTO.IsDeleted = false;
                //smsLogDTO.PhoneNumber = "123465789;1234567890";
                //smsLogDTO.SMSTemplateID = 0;
                //smsLogDTO.Text = "Test";
                //smsLogDTO.UserId = 470;

                return _smsManager.GetSentSMSLog(int.Parse(userID), int.Parse(clinicBranchID), int.Parse(currentPage));
                // return iResult.ToString();
            }

            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return smsLogDTO;
        }


        public string DeleteSMS(string smsID, string userID)
        {
            SMSResponseDTO smsLogDTO = null;

            int iResult = 0;
            try
            {
                if (_smsManager == null)
                {
                    _smsManager = (SMSManagerImp)DAOFactory.GetDataAccessObject(typeof(SMSManagerImp));
                }
                return _smsManager.DeleteSMS(smsID, userID);
                // return iResult.ToString();
            }

            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return "";
        }

        #endregion SMS

        #region Email

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 1 December 2011
        /// </summary>
        /// <param name="oAlertDTO"></param>
        /// <returns></returns>
        public string SaveAlert(AlertDTO oAlertDTO)
        {
            try
            {
                if (_alertManager == null)
                {
                    _alertManager = (Communication.Business.AlertManager.AlertManagerImp.AlertManagerImp)DAOFactory.GetDataAccessObject(typeof(Communication.Business.AlertManager.AlertManagerImp.AlertManagerImp));
                }
                return _alertManager.SaveAlert(oAlertDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return "";
        }

        /// <summary>
        /// test Function
        /// </summary>
        /// <param name="iDigit"></param>
        /// <returns></returns>
        public int Multiply(string iDigit)
        {
            //int num = int.Parse(iDigit);
            return int.Parse(iDigit) * 2;
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 1 December 2011
        /// </summary>
        /// <param name="oEmailDTO"></param>
        /// <returns></returns>
        public string SendEmail(XmlDocument xmlemaillDTO)
        {
            try
            {
                EmailDTO emailDTO = new EmailDTO();

                #region Test
                XmlDocument doc = new XmlDocument();
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Email";
                xRoot.IsNullable = true;
                XmlSerializer serializer = new XmlSerializer(typeof(EmailDTO), xRoot);
                string xml = xmlemaillDTO.InnerXml;




                emailDTO = (EmailDTO)serializer.Deserialize(new StringReader(xml));
                #endregion Test
              

                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                if (emailDTO.EmailTemplateID <= 0)
                    emailDTO.EmailTemplateID = 2;
                //emailDTO.ClinicBranchID = 1;
                //emailDTO.HasAttachment = false;
                //emailDTO.Message = "This is a test email.";
                //emailDTO.Subject = "Testing";
                emailDTO.RecipientEmail = emailDTO.RECIEVERID+";";
                //emailDTO.SENDERID = 470;
               
                ///
                /// Email attachement path will be get from Attachements path table and linked with this file  ( path\emailid\filename)  {Email Forward case exception}
                ///
                //AttachementDTO oAttachemntDTO = new AttachementDTO();

                /// Need to convert type
                ///emailDTO.Attachment = @"C:\Users\786\Documents\visual studio 2010\Projects\DownloadFiles\DownloadFiles\FileServer\test";
                //oEmailDTO.Attachemnts = oEmailDTO.Attachemnts.Add();
                return _emailManager.SendEmail(emailDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return "";
        }


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="iUserEmailID"></param>
        /// <param name="iUserID"></param>
        /// <param name="iClinicBrachID"></param>
        /// <param name="iStationID"></param>
        /// <returns></returns>
        public EmailDTO GetEmailDetails(string userEmailID, string userID, string clinicBrachID, string stationID)
        {
             EmailDTO emailDTO = null;
            try
            {

                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                emailDTO = _emailManager.GetEmail(int.Parse(userEmailID), int.Parse(userID), int.Parse(clinicBrachID), int.Parse(stationID));
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return emailDTO;
        }

#region Commited

        //public byte DownloadAttachmentFile(string sUserEmailID, string sAttachementFileID)
        //{
        //    try
        //    {

        //    }
        //    catch (InvalidArgumentException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    catch (ConfigurationException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    catch (TargetInvocationException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }

        //    catch (SqlException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //}

        //public byte DownloadAllAttachments(string sUserEmailID)
        //{
        //    try
        //    {

        //    }
        //    catch (InvalidArgumentException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    catch (ConfigurationException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    catch (TargetInvocationException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }

        //    catch (SqlException exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp.Message);
        //        QuivusLogger.Error(exp.StackTrace);
        //        HandleFault.HandleFautException(exp);
        //    }
        //}

#endregion Commited

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <returns></returns>
        public MailDTO GetAllEmails(String userID, String clinicBrachID, String stationID, String pageSize, String currentPage)
        {
            MailDTO mailDTO = null;
            try
            {
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }

                mailDTO = _emailManager.GetAllEmails(int.Parse(userID), int.Parse(clinicBrachID), int.Parse(stationID), int.Parse(pageSize), int.Parse(currentPage));

           //     int[] userEmailID = new int[emailDTOList.Count];
           //     for (int emailCount = 0; emailCount < emailDTOList.Count; emailCount++)
           //     {
           //         userEmailID[emailCount]=emailDTOList[emailCount].SENDERID;
           //     }
           ////     userEmailID.Remove(userEmailID.Length - 1, 1);

           //     UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();
           //    // int[] userIdList={470,573,580,597};
           //   //  String[] userIDList = _emailManager.GetEmailSenderList(userID);//_emailManager.GetEmailSenderList(userIdList);
           //    // var userInfo = objUserManagerService.GetUserBasicInfoById(_emailManager.GetEmailSenderList(userEmailID.Remove(userEmailID.Length - 1, 1)));

           //     var userInfo = objUserManagerService.GetUserBasicInfoById(userEmailID);
           //     if(userInfo.Length > 0)
           //     {
           //         for (int UserData = 0; UserData < emailDTOList.Count; UserData++)
           //         {
           //          //   string userIDFromUserService=string.Empty;
           //         //    int counter=0;
           //             for (int counter = 0; counter < userInfo.Length; counter++)
           //             {
           //                if(emailDTOList[UserData].SENDERID.ToString().Trim() == userInfo[counter].UserId.ToString().Trim())
           //                 {
           //                     emailDTOList[UserData].From = userInfo[counter].FirstName + " " + userInfo[counter].LastName;
           //                 }
           //             }
                      
           //         }
               
           //     }

                
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return mailDTO;
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="sUserEmailID"></param>
        /// <returns></returns>
        public string DeleteEmail(string userEmailID, string userID)
        {
           // EmailDTO oEmailDTO = null;
            try
            {
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                return _emailManager.DeleteEmail(userEmailID, userID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return "";
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 29 December 2011
        /// </summary>
        /// <param name="sUserEmailID"></param>
        /// <returns></returns>
        public string DeleteSentEmail(string userEmailID, string userID)
        {
            // EmailDTO oEmailDTO = null;
            try
            {
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                return _emailManager.DeleteEmail(userEmailID, userID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return "";
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date:7 December 2011
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="sFileStream"></param>
        public void uploadfile(string sFileName,System.IO.Stream sFileStream)
        {
            try
            {
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
               // _emailManager.UploadFile(sFileStream, sFileName);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            //return false;     
           
        }


        //public void Upload1(Stream data)
        //{
        //    string subDir = Guid.NewGuid().ToString("N");

        //    // get full path to and create the directory to save file in
        //    string uploadDir = Path.Combine(Path.GetDirectoryName(typeof(Communication.Business.EmailManager.EmailManagerImp.EmailManagerImp).Assembly.Location), subDir);
        //    Directory.CreateDirectory(uploadDir);

        //    // 64 KiB buffer
        //    byte[] buff = new byte[0x10000000];

        //    // save the file in chunks
        //    using (FileStream fs = new FileStream(Path.Combine(uploadDir,"update.dat"), FileMode.Create))
        //    {
        //        int bytesRead = data.Read(buff, 0, buff.Length);
        //        while (bytesRead > 0)
        //        {
        //            fs.Write(buff, 0, bytesRead);
        //            bytesRead = data.Read(buff, 0, buff.Length);
        //        }
        //    }
        //}


        //public void Upload(string sUserEmailID, string sFileName, string sfileExtension, Stream data)
        //{
        //    // string subDir = sUserEmailID;//Guid.NewGuid().ToString("N");

        //    // get full path to and create the directory to save file in
        //    string uploadDir = Path.Combine(Path.GetDirectoryName(typeof(Communication.Business.EmailManager.EmailManagerImp.EmailManagerImp).Assembly.Location), sUserEmailID);
        //    Directory.CreateDirectory(uploadDir);

        //    // 64 KiB buffer
        //    byte[] buff = new byte[0x10000000];

        //    // save the file in chunks
        //    using (FileStream fs = new FileStream(Path.Combine(uploadDir, sFileName + "." + sfileExtension), FileMode.Create))
        //    {
        //        int bytesRead = data.Read(buff, 0, buff.Length);
        //        while (bytesRead > 0)
        //        {
        //            fs.Write(buff, 0, bytesRead);
        //            bytesRead = data.Read(buff, 0, buff.Length);
        //        }
        //    }
        //}


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 8 December 2011
        /// Description: Get list of all Sent mails of a specific user
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iClinicBrachID"></param>
        /// <param name="iStationID"></param>
        /// <returns></returns>
        public MailDTO GetAllSentMails(string userID, string clinicBrachID, string stationID, string pageSize, string currentPage)
        {
            MailDTO oEmailDTO = null;
            try
            {
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                return oEmailDTO = _emailManager.GetAllSentMails(int.Parse(userID), int.Parse(clinicBrachID), int.Parse(stationID), int.Parse(pageSize), int.Parse(currentPage));
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return oEmailDTO;
        }

       /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 9 December 2011
        /// Description: Download Mail Attachemt
       /// </summary>
       /// <param name="sUserEmailID"></param>
       /// <param name="sFileName"></param>
       /// <param name="sfileExtension"></param>
       /// <returns></returns>
        public Stream DownloadFile(string userEmailID, string fileName, string fileExtension)
        {
            try
            {
               // string sUserEmailID = string.Empty;
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                return _emailManager.DownloadFile(userEmailID, fileName, fileExtension);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return Stream.Null;
        }


        //public Stream DF(string fileName, string fileExtension)
        //{
        //    string downloadFilePath = Path.Combine(@"C:\Users\786\Documents\visual studio 2010\Projects\DownloadFiles\DownloadFiles\FileServer", fileName + "." + fileExtension);
        //    WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
        //    return File.OpenRead(downloadFilePath);
        //}


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 9 December 2011
        /// Description:Upload Mail Attachemt
        /// </summary>
        /// <param name="sUserEmailID"></param>
        /// <param name="sFileName"></param>
        /// <param name="sFileExtension"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Upload(string userID, string fileName, Stream data)
        {
            try
            {
                // string sUserEmailID = string.Empty;
                if (_emailManager == null)
                {
                    _emailManager = (EmailManagerImp)DAOFactory.GetDataAccessObject(typeof(EmailManagerImp));
                }
                 _emailManager.UploadFile(userID, fileName, data);
               // return "";
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (ConfigurationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (TargetInvocationException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            catch (SqlException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
          //  return "";
        }

        private static void _streamToFile(string fileName, string fileExtension, Stream data)
        {
            // create name of subdirectory
            string subDir = Guid.NewGuid().ToString("N");

            // get full path to and create the directory to save file in
            string uploadDir = Path.Combine(Path.GetDirectoryName(typeof(CommunicationService).Assembly.Location), subDir);
            Directory.CreateDirectory(uploadDir);

            // 64 KiB buffer
            byte[] buff = new byte[0x10000000];

            // save the file in chunks
            using (FileStream fs = new FileStream(Path.Combine(uploadDir, fileName + "." + fileExtension), FileMode.Create))
            {
                int bytesRead = data.Read(buff, 0, buff.Length);
                while (bytesRead > 0)
                {
                    fs.Write(buff, 0, bytesRead);
                    bytesRead = data.Read(buff, 0, buff.Length);
                }
            }
        }


        public string Uploaded(string Testing, Stream data)
        {
            string[] sValues = Testing.Split('|');
            _streamToFile(sValues[0],sValues[1],data);

            return "";
        }


        ////////////////// Test Method //////////////////////

        public void TestUpload(string fileName, Stream data)
        {
            // create name of subdirectory


            Parser objParser = new Parser(data);
            if (objParser.Success)
            {
                string subDir = Guid.NewGuid().ToString("N");
                string filename = objParser.Filename;
                //if (objParser.Success)
                //    filename = objParser.Filename;
                //else
                //    filename = "test";
                string fileExtension = "xlsx";
                // get full path to and create the directory to save file in
                string uploadDir = Path.Combine(Path.GetDirectoryName(typeof(CommunicationService).Assembly.Location), subDir);
                Directory.CreateDirectory(uploadDir);

                // 64 KiB buffer
                byte[] buff = new byte[0x10000000];

                // save the file in chunks
                if (!string.IsNullOrEmpty(objParser.Filename.ToString().Trim()))
                {
                    using (FileStream fs = new FileStream(Path.Combine(uploadDir, filename), FileMode.Create))
                    {
                        //fs.Write(buff, 0, objParser.FileContents.Length);
                        //int bytesRead = data.Read(buff, 0, buff.Length);
                       // while (bytesRead > 0)
                       // {
                            fs.Write(objParser.FileContents, 0, objParser.FileContents.Length);
                         //   bytesRead = data.Read(buff, 0, buff.Length);
                        

                    }

                }
            }
        }

        /////////////////

        #endregion Email




       
    }
}
