using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusUtility.Utilities;
using Quivus.Utility.Factory;
using QuivusManager.DAO.ApplicationSession;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System.Configuration;
using QuivusManager.Buisness.Utility;
using Quivus.Utility.Logger;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace QuivusManager.Buisness.ApplicationSession
{
    public class ApplicationSessionHandler
    {
        ApplictionSessionDAO appSessionDAO = null;
        /// <summary>
        /// Author Muhammad Ismail :)
        /// Date 07-Mar-2011
        /// Check If Session For Same User Exist Then Delete that & Create New 
        // Other Wise Simply Create New One :)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        /// <returns></returns>
        public UserSessionDTO CreateUserSession(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            UserSessionDTO userSession = null;

            try
            {               
                QuivusLogger.Info("Checking If User Session Already Exist WIth userId::" + userId);

               // QApplicationSession.RemoveUserSessionIfExist(userId, userType, clinicBranch);
             //   QApplicationSession.UpdateUserSessionIfExist(userId, userType, clinicBranch);

               // QApplicationSession.RemoveUserSessionIfExist(userId, userType, clinicBranch);
                bool IsSessionExist = QApplicationSession.IsUserSessionExist(userId, userType, clinicBranch);

                String IpAddress = GetClientIPAddress();

                userSession = new UserSessionDTO
                {
                    UserID = userId,
                    UserTypeID = userType,
                    ClinicBranchID = clinicBranch,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    LastActivityPerformedAt = DateTime.Now,
                    TimeZoneId = 0,
                    SessionID = Guid.NewGuid(),
                    ClientIPAdress = IpAddress,
                    IsAlreadyLoggedIn=IsSessionExist
                };

                QApplicationSession.AddUserSession(userSession.SessionID, userSession);

                
                // Create New Session & Save In DB That's It :)           
                //if (appSessionDAO == null)
                //{
                //    appSessionDAO = (ApplictionSessionDAO)DAOFactory.GetDataAccessObject(typeof(ApplictionSessionDAO));
                //}

                //bool IsProvider = appSessionDAO.AddUserSession(userSession);

                userSession.IsProvider = true;

            }
            catch
            {
               
                throw;
            }
            return userSession;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 Sep 2011
        /// Utility Method to Get Client Ip Address
        /// </summary>
        /// <returns></returns>
        private String GetClientIPAddress()
        {
            String IpAddress = string.Empty;

            try
            {

                OperationContext context = OperationContext.Current;

                if (context != null)
                {
                    MessageProperties messageProperties = context.IncomingMessageProperties;

                    if (messageProperties != null)
                    {
                        RemoteEndpointMessageProperty endpointProperty =
                          messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

                        if (endpointProperty != null)
                        IpAddress = endpointProperty.Address;
                    }
                    
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message); // Just Log that Exception :)
            }
            return IpAddress;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 Mar 2011
        /// The Method Will Be used to Verfiy User Session & Session Id Valid Also Check Time Frame
        /// change by : Mansoor
        /// Date : 08 March 2011
        /// session time checked
        /// </summary>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public static bool IsValidUserSession(UserSessionDTO userSession)
        {
            bool Result = false;

            QuivusLogger.Info("Start Validating User Session ::" + userSession);
            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1);
                if (stackFrame != null)
                {
                    QuivusLogger.Info(" Info:: Validating User Session...... Call Is For Method:: " + stackFrame.GetMethod().Name);
                }
            }
            catch
            {
                QuivusLogger.Error("Error In Reflecting Method Name");
                // Ignore this Exception
            }

            //  QuivusLogger.Info("Validating User Session ::" + userSession);

            //TEMP
            //return true;

            if (userSession != null)
            {
                // Find User Session Object In Application Session Using GUID i.e SessionId :)

                QuivusLogger.Info("In QuivusManagerService IsValidUserSession Called PARAM:: userSession.SessionID " + userSession.SessionID);

                UserSessionDTO obj = QApplicationSession.GetActiveSession(userSession.SessionID);

               
                if (obj != null)
                {

                    // If Obj  IsAlreadyLoggedIn set to true It mean some other user has aquired his session. Code Update Sep 08 2011 By ismail :)
                    if (obj.IsAlreadyLoggedIn.Equals(true))
                    {
                        QuivusLogger.Info("User Session Already Logged In " + userSession.SessionID);
                        String LoggendInIpAddress = obj.ClientIPAdress;

                        // This User is  Logged In other Location Just Throw Exception From Here Including his IP Adress
                        UserSessionDTO objCurrent = QApplicationSession.GetUserActiveSession(obj.UserID,obj.UserTypeID);
                        if (objCurrent != null)
                        {
                            LoggendInIpAddress = objCurrent.ClientIPAdress;
                        }

                        QApplicationSession.RemoveUserSession(userSession.SessionID);

                        // Now Remove this session fromm Application Session 

                        throw new InvalidSessionException(ProjectConstant.IP_CONFLICT_EXPIRED_USER_SESSION_ID + LoggendInIpAddress
                            + ProjectConstant.IP_CONFLICT_EXPIRED_USER_SESSION_TERMINATED
                            , (int)ProjectConstant.ERROR_CODE.InvalidSession);
                    }

                    // Put Time-Period Using LastActivityPerformedAt  Check Either Session Is TimedOut Or Not :)
                    QuivusLogger.Info("Verfiy User Session Time :: UserID::" + obj.UserID);

                    QuivusLogger.Info("User Session Time LastActivityPerformedAt:: " + obj.LastActivityPerformedAt);

                    QuivusLogger.Info("Current Time:: " + DateTime.Now);

                    if (obj.LastActivityPerformedAt <= DateTime.Now.AddMinutes(-1 * double.Parse(QConfigurationManager.SessionTimeOut)))
                    {
                        QuivusLogger.Info("User Session Time Out Duration IS :: " + QConfigurationManager.SessionTimeOut);

                        // remove from session :(
                        QApplicationSession.RemoveUserSession(userSession.SessionID);
                        QuivusLogger.Info("User Session is Expired:: UserID::" + userSession.UserID);
                        // send session time out 
                        throw new InvalidSessionException(ProjectConstant.EXPIRED_USER_SESSION_ID, (int)ProjectConstant.ERROR_CODE.InvalidSession);
                    }
                    else
                    {
                        obj.LastActivityPerformedAt = DateTime.Now;// Update User LastActivity Performed Time TO current As last activity is within time :)
                        Result = true;
                    }
                }
                else
                {
                    // Flex provide valid session GUID but 
                    QuivusLogger.Info("Unable To Load User Session UserID::" + userSession.UserID);
                    throw new InvalidSessionException(ProjectConstant.INVALID_USER_SESSION_ID, (int)ProjectConstant.ERROR_CODE.InvalidSession);
                }
            }
            else
            {
                QuivusLogger.Info("Invalid User Session ");
                throw new InvalidSessionException(ProjectConstant.INVALID_USER_SESSION, (int)ProjectConstant.ERROR_CODE.InvalidSession);
            }

            return Result;
        }
    }
}
