using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Utility.MailTemplates;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;


using Quivus.Utility;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;

using QuivusModel.Communication;
using UserManager.Business.Utility;
using QuivusModel.Communication.MailDTO;

namespace UserManagerService.Business.EmailManager.Impl
{
    public class EmailManagerImpl : IEmailManager
    {
        UserDAO userDAO = null;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Jan 2011
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public String SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
        {
            String response = null;
            int Result;
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

                TemplateHandler handler = TemplateHandler.Instance;
                MailTemplate template = handler["SimpleMailByProvider"];
                template.Subject = subject;
                template.Body = String.Format(template.Body, message);


                try
                {
                    handler.Send(template, receiptEmail);

                }
                catch (Exception exp)
                {
                    QuivusLogger.Error(exp);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                Result = userDAO.LogEmailSend(requestedUserId, clinicBranchID, subject, receiptEmail, message, DateTime.Now);

                response = Result.ToString();
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Ali Noor
        /// </summary>
        /// <param name="emailDTO"></param>
        /// <returns></returns>
        public String SendEmailWithAttachment(EmailDTO emailDTO)
        {
            String response = null;

            try
            {
                QuivusLogger.Info("SendEmail PARAM UserId:: " + emailDTO.UserId
                    + "requestedUserId:: " + emailDTO.ClinicBranchID + " subjectsubject:: " + emailDTO.Subject + "receiptEmail::" + emailDTO.RecipientEmail
                    + "message:: " + emailDTO.Message);

                if (emailDTO.UserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (emailDTO.ClinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                else if (String.IsNullOrEmpty(emailDTO.RecipientEmail))
                {
                    throw new InvalidArgumentException(ProjectConstant.EMAIL_ID_INVALID_ERROR_MSG);
                }

                TemplateHandler handler = TemplateHandler.Instance;
                MailTemplate template = handler["SimpleMailByProvider"];
                template.Subject = emailDTO.Subject;
                template.Body = String.Format(template.Body, emailDTO.Message);
                template.Attachment = emailDTO.Attachment;
                template.AttachedFileName = emailDTO.AttachedFileName;


                try
                {
                    handler.Send(template, emailDTO.RecipientEmail);

                }
                catch (Exception exp)
                {
                    QuivusLogger.Error(exp);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                int result = userDAO.LogEmailWithAttachment(emailDTO, DateTime.Now);
                response = result.ToString();

            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
