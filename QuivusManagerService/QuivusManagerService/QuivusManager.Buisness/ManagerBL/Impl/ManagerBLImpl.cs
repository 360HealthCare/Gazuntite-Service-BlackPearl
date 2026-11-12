using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using System.ServiceModel;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.Buisness.ApplicationSession;



namespace QuivusManager.Buisness.ManagerBL.Impl
{
    public class ManagerBLImpl : IManagerBL
    {
        UserManagerServiceClient UserServiceClient = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load the user login history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        public void LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            try
            {
                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));

                }

                QuivusLogger.Error("QuivusManagerService ManagerBLImpl :: Calling LogLoginHistory On User-Service");

                UserServiceClient.LogLoginHistory(userId, userType, clinicBranch, isSucessFull);
            }
            catch
            {
                throw;
            }
        }

        /// Author Muhammad Ismail
        /// Created Date 08 Sep 2011
        /// This Method Is Used To Update the user session
        /// In Case User Set KickOutPreviousSession to true we will discard previous active session
        /// In Case User Set KickOutPreviousSession to false we will discard new crested session
        /// </summary>
        /// <param name="SessionObj"></param>
        public UserSessionDTO UpdateUserSession(UserSessionDTO SessionObj)
        {
            UserSessionDTO ReturnSessionObj = null;
            try
            {
                QuivusLogger.Error("QuivusManagerService UpdateUserSession :: Calling UpdateUserSession");

                if (SessionObj != null)
                {
                    UserSessionDTO obj = QApplicationSession.GetUserSession(SessionObj.SessionID);

                    if (SessionObj.KickOutPreviousSession)
                    {
                        // It mean Kick Previous Active Session
                        // Now We will not destroy that session we will update it IsAlreadyLoggedIn status To true 
                        // As in system only one session exist with IsAlreadyLoggedIn status To false per user
                        // Also we update current session IsAlreadyLoggedIn status To false 

                        if (obj != null)
                        {
                            // 1. Remove Or Update Previous Session status to true
                            QApplicationSession.updateUserActiveSession(obj.UserID, obj.UserTypeID);

                            // 2. Update current session IsAlreadyLoggedIn status To false to make it active session
                            obj.IsAlreadyLoggedIn = false;

                             UserSessionDTO objIpdated = QApplicationSession.GetUserSession(SessionObj.SessionID);
                             QuivusLogger.Info("New Session Is Already Logged In Set TO False" + objIpdated.IsAlreadyLoggedIn + "GUID::" + objIpdated.SessionID);

                             SessionObj = obj;

                             QApplicationSession.UpdateUserSession(SessionObj);
                        }
                    } //End If PArt
                    else
                    {
                        QuivusLogger.Info("Old Session Deleted");
                        // User Ask To Destroy It's Own Session Just Remove it 
                        if (obj != null)
                        {
                            ReturnSessionObj = QApplicationSession.GetUserActiveSession(obj.UserID, obj.UserTypeID);                            
                        }
                        QApplicationSession.RemoveUserSession(SessionObj.SessionID);
                    } // End Else
                }
            }
            catch
            {
                throw;
            }
            QuivusLogger.Info("Return  Session is" + SessionObj);
            return SessionObj;
        }
        /* Commented By Ismail Moved TO Streaming Service
         * public EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
         {
             EmailResponse response = null;
             String Result = String.Empty;
             try
             {
                 if (UserServiceClient == null)
                 {
                     UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                 }
                 if (response == null)
                 {
                     response = new EmailResponse();
                 }
                 Result = UserServiceClient.SendEmail(requestedUserId, clinicBranchID, subject, receiptEmail, message);

                 if (!string.IsNullOrEmpty(Result))
                 {
                     response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                 }
                 else
                 {
                     response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                 }
             }
             catch
             {
                 throw;
             }
             return response;
         }
         */

        public void SignOutUserSession(Guid sessionId)
        {
            try
            { 
                //QuivusLogger.Error("QuivusManagerService ManagerBLImpl :: Calling LogLoginHistory On User-Service");

                QApplicationSession.RemoveUserSession(sessionId); 
            }
            catch
            {
                throw;
            }
        }

    }
}
