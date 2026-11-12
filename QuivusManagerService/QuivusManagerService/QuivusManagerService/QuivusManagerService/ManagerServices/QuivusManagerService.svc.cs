using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.Buisness.ManagerBL;
using QuivusManager.Buisness.ManagerBL.Impl;
using QuivusManager.IntegratedServies.UserManagerService;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.Buisness.CommonBL;
using QuivusManager.Buisness.CommonBL.Impl;
using System.Xml;
using Quivus.Utility.CustomException;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using QuivusManagerService.CustomeControls.General;
using System.Web.UI;
using QuivusManagerService.CustomeControls;




namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QuivusManagerService" in code, svc and config file together.

   [ServiceBehavior(Namespace = "http://quivus.com/QuivusManagerService")]
    public partial class QuivusManagerService : QuivusBaseService, IQuivusManagerService
    {

        IManagerBL ManagerBL = null;

        ApplicationSessionHandler SessionHandler = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load the user login history
        /// Update Date 07 Mar-2011
        /// Method Return Type Change From Void TO User-Session DTO
        /// This method will also be used to maintain user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>

        public UserSessionDTO LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            UserSessionDTO userSession = null;
            try
            {
                QuivusLogger.Info("QuivusManagerService Calling LogLoginHistory userId" + userId + "userType" + userType + "clinicBranch" + clinicBranch);

                if (ManagerBL == null)
                {
                    ManagerBL = new ManagerBLImpl();
                }
                ManagerBL.LogLoginHistory(userId, userType, clinicBranch, isSucessFull);

                if (SessionHandler == null)
                {
                    SessionHandler = new ApplicationSessionHandler();
                }
                QuivusLogger.Info("QuivusManagerService After Calling LogLoginHistory Calling CreateUserSession");

                /* New Logic Updated on Sep 07 2011 By Ismail requested By Hassan Raza Naqvi :)
                 
                 * Now We keep track of Ip adress of client machine & when user with same credentials try to login
                 * we prompht new logged-in user that you want to kick out previous logged in user on other location whose IP adress is BLAH BLAH BLAH :)
                 * 
                */
                userSession = SessionHandler.CreateUserSession(userId, userType, clinicBranch, isSucessFull); // Added By Ismail TO Create USer Session

            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }

            return userSession;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 08 Sep 2011
        /// This Method Is Used To Update the user session
        /// In Case User Set KickOutPreviousSession to true we will discard previous active session
        /// In Case User Set KickOutPreviousSession to false we will discard new crested session
        /// </summary>
        /// <param name="SessionObj"></param>

        public UserSessionDTO UpdateUserSession(UserSessionDTO SessionObj)
        {
            UserSessionDTO userSession = null;
            try
            {
                QuivusLogger.Info("QuivusManagerService Calling UpdateUserSession SessionObj" + SessionObj);

                if (ManagerBL == null)
                {
                    ManagerBL = new ManagerBLImpl();
                }
                userSession = ManagerBL.UpdateUserSession(SessionObj);

            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }

            return userSession;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 8 Sep 2011
        /// </summary>
        /// <param name="sessionId"></param>      
        public void SignOutUserSession(Guid sessionId)
        {
            //UserSessionDTO userSession = null;
            try
            {
                QuivusLogger.Info("QuivusManagerService Calling SignOutUserSession sessionId" + sessionId);

                if (ManagerBL == null)
                {
                    ManagerBL = new ManagerBLImpl();
                }
                ManagerBL.SignOutUserSession(sessionId);

            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
        }


        /// <summary>
        /// Author Mansoor
        /// Created Date 17 Jan 2011
        /// This method is used to implement send email functionality
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// // Commented By Ismail
        //public QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
        //{
        //    QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse response = null;
        //    String Result = String.Empty;
        //    try
        //    {
        //        if (ManagerBL == null)
        //        {
        //            ManagerBL = new ManagerBLImpl();
        //        }
        //       response = ManagerBL.SendEmail(requestedUserId, clinicBranchID, subject, receiptEmail, message);
        //    }
        //    catch (FaultException<QuivusFault> exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse();
        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Ali Noor
        /// Created Date 1st Feb 2011
        /// This method is used to implement send email functionality including attachement
        /// </summary>
        /// <param name=""></param>
        //public QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(EmailDTO data)
        //{
        //    QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse response = null;
        // //   IEmailManager emailMangerImpl = null;
        //    try
        //    {

        //    }
        //    catch (Exception exp)
        //    {

        //    }
        //    return response;
        //}

        //public string GetServiceVersion()
        //{
        //    return "Development";
        //}


        public string GetResponseAsXML()
        {
            string xmlOutput = "";

            XmlDocument doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement("Library");
            doc.AppendChild(root);

            XmlElement book = doc.CreateElement("Book");
            book.SetAttribute("BookType", "Hardcover");
            XmlElement title = doc.CreateElement("Title");
            title.InnerText = "Door Number Three";
            XmlElement author = doc.CreateElement("Author");
            author.InnerText = "O'Leary, Patrick";
            book.AppendChild(title);
            book.AppendChild(author);
            root.AppendChild(book);

            xmlOutput = doc.OuterXml;

            return xmlOutput;
        }

        public string PingAllClientServices()
        {
            string result = "";
            //  Logger.Write("New Error ", "Error", 1, 1, System.Diagnostics.TraceEventType.Error);
            QuivusLogger.Debug("Test Email Manager Working");

            ICommonBL common = new CommonBLImpl();
            result = common.PingAllServices();
            return result;
        }


        public DateTime DateTestServices(DateTime date)
        {
            QuivusLogger.Debug("Test Date Recieved::" + date);
            return date;
        }

        

    }
}
