using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusUtility;
using Quivus.Utility.Factory;
using QuivusModel.Communication.MailDTO;
using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.MailTemplates;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Hosting;
using QuivusUtility.Utilities;


namespace Communication.Business.EmailManager.EmailManagerImp
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 1 December 2011
    /// Description: This class contains business logic of Email
    /// </summary>
    public class EmailManagerImp:IEmailManager
    {
        CommunicationDataAccess.Email.EmailDAO emailDAO = null;
        Quivus.Utility.MailTemplates.TemplateHandler oEmailTempHandler = null;

        ///// Methods from Stream Service //////////////////////////////


        /// <summary>
        /// Author Mansoor
        /// Date 17 Jan 2011
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string MailSending(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
        {
            string response = null;
           // int Result;
            try
            {
                QuivusLogger.Info("SendEmail PARAM requestedUserId:: " + requestedUserId
                    + "requestedUserId:: " + clinicBranchID + " subjectsubject:: " + subject + "receiptEmail::" + receiptEmail
                    + "message:: " + message);

                if (requestedUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                else if (String.IsNullOrEmpty(receiptEmail))
                {
                    throw new InvalidArgumentException(ProjectConstant.EMAIL_ID_INVALID_ERROR_MSG);
                }

                Send(message, subject, receiptEmail, null, null);

            }
            catch
            {
                throw;
            }
            return response;
        }

        public void Send(string message,string Subject,string to, string[] cc, string[] bcc)
        {
            #region QuivusHost
            
            byte[] buffer = null;
           
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("support@quivus.com");
            msg.Body = message;
            msg.Subject = Subject;
            //msg.BodyFormat = group.MailFormat;

            //msg.To = CreateRecipientList(to);
            msg.To.Add(to);
            //if (cc != null && cc.Length > 0)
            //{
            //    msg.CC.Add(CreateRecipientList(cc));
            //}
            //if (bcc != null && bcc.Length > 0)
            //{
            //    msg.Bcc.Add(CreateRecipientList(bcc));
            //}

            //set SMTP server, if defined
            string smtp = "host.quivus.com";

            //Replaced Code to Send Email

            SmtpClient client = new SmtpClient(smtp);
            client.EnableSsl = false;//true;
            client.Port = 25;//587;
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis; 
           // client.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential loginInfo = new NetworkCredential("support@quivus.com", "123456");
            client.Credentials = loginInfo;


            client.Send(msg);
            
            #endregion QuivusHost




            #region Gmail

            /*
            //string gMailAccount = "zahid.mehmood@zigron.com";
           string password = "";
            //string to = "zahid.mehmood@zigron.com";
            //string subject = "Test";



            NetworkCredential loginInfo = new NetworkCredential("zahid.mehmood@zigron.com", password);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("zahid.mehmood@zigron.com");
            msg.To.Add(to);//new MailAddress(to));

            

            msg.Subject = Subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            //Attachment attachment;
            //attachment = new Attachment("c:\\result.txt");
            //msg.Attachments.Add(attachment);

            // AttachmentCollection 

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            client.Send(msg);
            */

            #endregion Gmail
        }

        /////End  Methods from Stream Service///////////////////////

       /// <summary>
       ///Author: Zahid Mehmood
       /// Date: 7 December 2011
       /// Description: Email will be sent with attachment
       /// </summary>
       /// <param name="requestedUserId"></param>
       /// <param name="clinicBranchID"></param>
       /// <param name="subject"></param>
       /// <param name="receiptEmail"></param>
       /// <param name="message"></param>
       /// <param name="Attachments"></param>
       /// <returns></returns>
        public string MailSendingWithAttachments(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message,string Attachments)
        {
            try
            {
               // SendAttachment(subject, message, subject, receiptEmail, "", Attachments);
                SendAttachment(subject, message, subject, receiptEmail, requestedUserId.ToString(),"" ,Attachments);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return "";
        }

        public void SendAttachment(string message, string body, string Subject, string to, string from, string filepath, string fileName)
        {
            #region QuivusHost

            byte[] buffer = null;

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("support@quivus.com");
            msg.Body = message;
            msg.Subject = Subject;
            //msg.BodyFormat = group.MailFormat;

            //msg.To = CreateRecipientList(to);
            msg.To.Add(to);


            Attachment objAttachment = new Attachment(fileName);

            msg.Attachments.Add(objAttachment);

            //if (cc != null && cc.Length > 0)
            //{
            //    msg.CC.Add(CreateRecipientList(cc));
            //}
            //if (bcc != null && bcc.Length > 0)
            //{
            //    msg.Bcc.Add(CreateRecipientList(bcc));
            //}

            //set SMTP server, if defined
            string smtp = "host.quivus.com";

            //Replaced Code to Send Email

            SmtpClient client = new SmtpClient(smtp);
            client.EnableSsl = false;//true;
            client.Port = 25;//587;
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis; 
           // client.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential loginInfo = new NetworkCredential("support@quivus.com", "123456");
            client.Credentials = loginInfo;


            client.Send(msg);

            #endregion QuivusHost


            #region gmail
            /*
            //string gMailAccount = "zahid.mehmood@zigron.com";
            string password = "";
            //string to = "zahid.mehmood@zigron.com";
            //string subject = "Test";
            NetworkCredential loginInfo = new NetworkCredential("zahid.mehmood@zigron.com", password);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("zahid.mehmood@zigron.com");
            msg.To.Add(new MailAddress(to));
            msg.Subject = Subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            Attachment attachment;
            attachment = new Attachment(fileName);
            msg.Attachments.Add(attachment);

            // AttachmentCollection 

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            client.Send(msg);
            #endregion gmail


            byte[] buffer = null;

            MailMessage msgMail = new MailMessage();

            if (!string.IsNullOrEmpty(filepath))
            {

                buffer = File.ReadAllBytes(filepath);
                MemoryStream stream = new MemoryStream(buffer);
                Attachment attachmentMail = new Attachment(stream, fileName);
                msg.Attachments.Add(attachmentMail);

            }
            */
            #endregion gmail
        }
          //  msgMail.From = new  MailAddress(   // MailAddress(group.SenderAddress);

 
        /*
            msgMail.Body = body;//template.Body;
            msgMail.Subject =Subject ;
            //msg.BodyFormat = group.MailFormat;

            //msg.To = CreateRecipientList(to);
            msg.To.Add(CreateRecipientList(to));
          
        
            //set SMTP server, if defined
            string smtp = "host.quivus.com";

            //Replaced Code to Send Email

            SmtpClient client = new SmtpClient(smtp);
            client.EnableSsl = false;//true;
            client.Port = 25;//587;
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis; //network 
            NetworkCredential loginInfo = new NetworkCredential("support@quivus.com", "123456");
            client.Credentials = loginInfo;


            client.Send(msg);
         * */

        

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 1 December 2011
        /// </summary>
        /// <param name="oEmailDTO"></param>
        /// <returns></returns>
        public string SendEmail(EmailDTO oEmailDTO)
        {
            try
            {
                if (oEmailDTO.EmailTemplateID > 0 && oEmailTempHandler == null)
                {
                    #region commit
                    //Quivus.Utility.MailTemplates.MailTemplate oMailTemplate=new Quivus.Utility.MailTemplates.MailTemplate();
                    //Quivus.Utility.MailTemplates.Xml.TemplateGroupElement oTempGroupElement=new Quivus.Utility.MailTemplates.Xml.TemplateGroupElement();
                    //oMailTemplate.Locale = System.Globalization.CultureInfo.CurrentCulture;
                    ////oEmailTempHandler.Send()
                    //oEmailTempHandler.Send(oTempGroupElement.GetTemplate(System.Globalization.CultureInfo.CurrentCulture),"zahid.mehmood@zigron.com");
                    #endregion commit

                }

                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }


               
                string sEmailUserID= emailDAO.SaveEmailInformation(oEmailDTO);
              //  oEmailDTO.Attachment = System.IO.File.ReadAllBytes("C:\\result.txt");
                         //   oEmailDTO.AttachedFileName = "result.txt";
                string[] sRecipientsList;
                int[] recipients=null; 
                if (oEmailDTO.RecipientEmail != null)
                {
                    sRecipientsList = oEmailDTO.RecipientEmail.Split(';');
                    recipients=new int[sRecipientsList.Length];
                    int counter=0;
                    foreach (var item in sRecipientsList)
                    {
                        if (item.ToString().Trim() != string.Empty)
                        {
                            emailDAO.SaveReceieverList(sEmailUserID, item);
                            recipients[counter] = int.Parse(item);
                        }
                    }

                    emailDAO.SaveSender(sEmailUserID, oEmailDTO.SENDERID.ToString());
                }

                #region GetUserInfo

             
                UserManagerService.UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();
                string email = null;

                for (int recipientsCount = 0; recipientsCount < recipients.Length; recipientsCount++)
                {
                    if(recipients[recipientsCount] != 0)
                    email += objUserManagerService.GetUserEmailByUserId(recipients[recipientsCount]) + ",";
                }
                
                
               // string mailSendTo=null;
             /*
                var userInfo = objUserManagerService.GetUserBasicInfoById(recipients);
                if (userInfo.Length > 0)
                {
                    for (int UserData = 0; UserData < recipients.Length; UserData++)
                    {
                        for (int counter = 0; counter < userInfo.Length; counter++)
                        {
                            if (recipients[UserData].ToString().Trim() == userInfo[counter].UserId.ToString().Trim())
                            {
                               mailSendTo =userInfo[counter].UserEmail+";";

                               // userInfo[].UserEmail;
                            }
                        }

                    }

                }
              */
                #endregion GetUserInfo

                if (email.Trim().Length > 0)
                {
                    email=email.Remove(email.Length - 1, 1);
                    if (oEmailDTO.HasAttachment)
                    {
                        // string sFileData = System.Text.Encoding.ASCII.GetString(oEmailDTO.Attachment, 0, oEmailDTO.Attachment.Length);
                        /// Change path to 
                        /// if folder not exists create 
                        //if (!Directory.Exists("c:\\" + sEmailUserID))
                        //    Directory.CreateDirectory("c:\\" + sEmailUserID);

                        //StreamWriter sWriter = new StreamWriter("c:\\" + sEmailUserID + "\\" + oEmailDTO.AttachedFileName, false);
                        //  sWriter.Write(sFileData);
                        //  sWriter.Close();

                        ///////////// Need to change attachemtn type from byte[] to string

                        // string filepath = "";//Path.Combine(oEmailDTO.Attachment,oEmailDTO.AttachedFileName);
                        //  string filepath = @"C:\Users\786\Documents\visual studio 2010\Projects\DownloadFiles\DownloadFiles\FileServer\test\"+oEmailDTO.AttachedFileName;
                        string filepath = emailDAO.GetFilePath(oEmailDTO.UserId.ToString());
                        MailSendingWithAttachments(oEmailDTO.SENDERID, oEmailDTO.ClinicBranchID, oEmailDTO.Subject,email, oEmailDTO.Message, filepath+"\\"+oEmailDTO.AttachedFileName);
                    }

                    else
                        MailSending(oEmailDTO.SENDERID, oEmailDTO.ClinicBranchID, oEmailDTO.Subject, email, oEmailDTO.Message);
                }
                return "Mail Sent Successfully.";
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return "";
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="iUserEmailID"></param>
        /// <returns></returns>
        public EmailDTO GetEmail(int userEmailID, int userID, int clinicBrachID, int stationID)
        {
           EmailDTO emailDTO = null;
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }
                emailDTO = emailDAO.GetEmailDetails(userEmailID, userID, clinicBrachID, stationID);

                UserManagerService.UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();

                int[] userIDs = new int[1];
                userIDs[0]=emailDTO.SENDERID;
                var userInfo= objUserManagerService.GetUserBasicInfoById(userIDs);
                emailDTO.From = userInfo[0].FirstName + " " + userInfo[0].LastName;

                userIDs[0] = emailDTO.RECIEVERID;
                userInfo = objUserManagerService.GetUserBasicInfoById(userIDs);
                emailDTO.Sendto = userInfo[0].FirstName + " " + userInfo[0].LastName;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return emailDTO;
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="iUserEmailID"></param>
        /// <returns></returns>
        public string DeleteEmail(string userEmailID, string userID)
        {
           // EmailDTO emailDTO = null;
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }

                var userEmailIDList=userEmailID.Split('^');
                userEmailID = "<Email>";
                for (int emailID = 0; emailID < userEmailIDList.Length; emailID++)
                {
                    userEmailID += "<id>" + userEmailIDList[emailID] + "</id>";
                }
                userEmailID += "</Email>";
                return emailDAO.DeleteEmail(userEmailID,int.Parse(userID));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return "";
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="iUserEmailID"></param>
        /// <returns></returns>
        public string DeleteSentEmail(string userEmailID, string userID)
        {
            // EmailDTO emailDTO = null;
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }

                var userEmailIDList = userEmailID.Split('^');
                userEmailID = "<Email>";
                for (int emailID = 0; emailID < userEmailIDList.Length; emailID++)
                {
                    userEmailID += "<id>" + userEmailIDList[emailID] + "</id>";
                }
                userEmailID += "</Email>";
                return emailDAO.DeleteEmail(userEmailID, int.Parse(userID));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return "";
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iClinicID"></param>
        /// <param name="iStationID"></param>
        /// <returns></returns>
        public MailDTO GetAllEmails(int userID, int clinicID, int stationID,int pageSize,int currentPage)
        {
            MailDTO mailDTO = null;
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }
                mailDTO = emailDAO.GetAllEmails(userID, clinicID, stationID, pageSize, currentPage);


                #region GetUserInfo

                int[] userEmailID = new int[mailDTO.Emails.Count];
              for (int emailCount = 0; emailCount < mailDTO.Emails.Count; emailCount++)
              {
                  userEmailID[emailCount] = mailDTO.Emails[emailCount].SENDERID;
              }
              //     userEmailID.Remove(userEmailID.Length - 1, 1);
              UserManagerService.UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();

              //UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();
              // int[] userIdList={470,573,580,597};
              //  String[] userIDList = _emailManager.GetEmailSenderList(userID);//_emailManager.GetEmailSenderList(userIdList);
              // var userInfo = objUserManagerService.GetUserBasicInfoById(_emailManager.GetEmailSenderList(userEmailID.Remove(userEmailID.Length - 1, 1)));

              var userInfo = objUserManagerService.GetUserBasicInfoById(userEmailID);
              if (userInfo.Length > 0)
              {
                  for (int UserData = 0; UserData < mailDTO.Emails.Count; UserData++)
                  {
                      //   string userIDFromUserService=string.Empty;
                      //    int counter=0;
                      for (int counter = 0; counter < userInfo.Length; counter++)
                      {
                          if (mailDTO.Emails[UserData].SENDERID.ToString().Trim() == userInfo[counter].UserId.ToString().Trim())
                          {
                              mailDTO.Emails[UserData].From = userInfo[counter].FirstName + " " + userInfo[counter].LastName;
                          }
                      }

                  }

              }
                #endregion GetUserInfo

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return mailDTO;
        }

        public int[] GetEmailSenderList(String userID)
        {
            try
            {
                int[] emailSenderList;
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }
                return emailSenderList = emailDAO.GetEmailSenderList(userID);

               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        ///// <summary>
        ///// Author: Zahid Mehmood
        ///// Date: 7 December 2011
        ///// </summary>
        ///// <param name="sFileStream"></param>
        ///// <param name="sFileName"></param>
        //public void UploadFile(string userID, string sFileName, Stream sFileStream)
        //{
        //    try
        //    {
        //         QuivusLogger.Info("UploadFile file name " + sFileName);
        //         string subDir = Guid.NewGuid().ToString("N");

        //         // get full path to and create the directory to save file in
        //         string uploadDir = Path.Combine(Path.GetDirectoryName(typeof(Communication.Business.EmailManager.EmailManagerImp.EmailManagerImp).Assembly.Location), subDir);
        //         Directory.CreateDirectory(uploadDir);

        //         // 64 KiB buffer
        //         byte[] buff = new byte[0x10000];

        //         // save the file in chunks
        //         using (FileStream fs = new FileStream(Path.Combine(uploadDir,sFileName), FileMode.Create))
        //         {
        //             int bytesRead = sFileStream.Read(buff, 0, buff.Length);
        //             while (bytesRead > 0)
        //             {
        //                 fs.Write(buff, 0, bytesRead);
        //                 bytesRead = sFileStream.Read(buff, 0, buff.Length);
        //             }
        //         }

        //        QuivusLogger.Info("UploadFile call ended with file name " + sFileName);
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 8 December 2011
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iClinicID"></param>
        /// <param name="iStationID"></param>
        /// <returns></returns>
        public MailDTO GetAllSentMails(int userID, int clinicID, int stationID, int pageSize, int currentPage)
        {
            MailDTO mailDTO = null;
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }
                mailDTO = emailDAO.GetAllSentEmails(userID, clinicID, stationID, pageSize, currentPage);

                #region GetUserInfo

                int[] userEmailID = new int[mailDTO.Emails.Count];
                for (int emailCount = 0; emailCount < mailDTO.Emails.Count; emailCount++)
                {
                    userEmailID[emailCount] = mailDTO.Emails[emailCount].RECIEVERID;
                }
                //     userEmailID.Remove(userEmailID.Length - 1, 1);
                UserManagerService.UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();

                //UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();
                // int[] userIdList={470,573,580,597};
                //  String[] userIDList = _emailManager.GetEmailSenderList(userID);//_emailManager.GetEmailSenderList(userIdList);
                // var userInfo = objUserManagerService.GetUserBasicInfoById(_emailManager.GetEmailSenderList(userEmailID.Remove(userEmailID.Length - 1, 1)));

                var userInfo = objUserManagerService.GetUserBasicInfoById(userEmailID);
                if (userInfo.Length > 0)
                {
                    for (int UserData = 0; UserData < mailDTO.Emails.Count; UserData++)
                    {
                        //   string userIDFromUserService=string.Empty;
                        //    int counter=0;
                        for (int counter = 0; counter < userInfo.Length; counter++)
                        {
                            if (mailDTO.Emails[UserData].RECIEVERID.ToString().Trim() == userInfo[counter].UserId.ToString().Trim())
                            {
                                mailDTO.Emails[UserData].From = userInfo[counter].FirstName + " " + userInfo[counter].LastName;
                            }
                        }

                    }

                }
                #endregion GetUserInfo
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return mailDTO;
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 9 December 2011
        /// </summary>
        /// <param name="sUserEmailID"></param>
        /// <param name="sFileName"></param>
        /// <param name="sfileExtension"></param>
        /// <returns></returns>
        public Stream DownloadFile(string sUserEmailID, string sFileName, string sfileExtension)
        {
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }
                string sFilePath = emailDAO.GetFilePath(sUserEmailID);
                string downloadFilePath = Path.Combine(sFilePath, "test\\" + sFileName + "." + sfileExtension);
             //   WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                return File.OpenRead(downloadFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 9 December 2011
        /// </summary>
        /// <param name="sUserEmailID"></param>
        /// <param name="sFileName"></param>
        /// <param name="sFileExtension"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UploadFile(string userID, string fileName, Stream data)
        {
            try
            {
                if (emailDAO == null)
                {
                    emailDAO = (CommunicationDataAccess.Email.EmailDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.Email.EmailDAO));
                }

                UploadFileParser fileParser = new UploadFileParser(data);

                if (fileParser.Success)
                {
                    string subDir = Guid.NewGuid().ToString("N");

                    string sFilePath = emailDAO.GetFilePath(userID);
                   // string uploadDir = Path.Combine(sFilePath, fileName);
                    Directory.CreateDirectory(sFilePath);
                    byte[] buff = new byte[0x10000000];
                    using (FileStream fs = new FileStream(Path.Combine(sFilePath, fileParser.Filename), FileMode.Create))
                    {
                        fs.Write(fileParser.FileContents, 0, fileParser.FileContents.Length);
                        //int bytesRead = data.Read(buff, 0, buff.Length);
                        //while (bytesRead > 0)
                        //{
                        //    fs.Write(buff, 0, bytesRead);
                        //    bytesRead = data.Read(buff, 0, buff.Length);
                        //}
                    }

                }

                #region commit

                //// 64 KiB buffer
                //byte[] buff = new byte[0x10000000];

                //// save the file in chunks
                //using (FileStream fs = new FileStream(Path.Combine(uploadDir, fileName + fileExtension), FileMode.Create))
                //{
                //    int bytesRead = data.Read(buff, 0, buff.Length);
                //    while (bytesRead > 0)
                //    {
                //        fs.Write(buff, 0, bytesRead);
                //        bytesRead = data.Read(buff, 0, buff.Length);
                //    }
                //}

                #endregion commit

                return "File Uploaded";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
