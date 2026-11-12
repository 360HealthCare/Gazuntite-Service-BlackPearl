using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QuivusManager.Buisness.UserBL;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.Buisness.UserBL.Impl;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using System.ServiceModel;


using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.PatientBL;

using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {

        IUserBL UserImpl = null;

        /// <summary>
        /// Author M.Ismail
        /// Created Date Jan 11 2010
        /// This Method is Used TO Authenicate the User 
        /// In Case Of Successfull verfication system return User DTO Object
        /// Update by Mansoor
        /// Date 13 Jan 2010
        /// Response type changed to custom to catter the unavaliblity of Exception handling on
        /// Clinet side
        /// Update by Mansoor
        /// Date 14 Jan 2010
        /// Incase of unsucessfull scenario system sends the error message and code
        /// Clinet side
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        /// 
        public LoginResponse AuthenticateUser(string UserId, string UserPassword)
        {
            LoginResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService AuthenticateUser Called");
                QuivusLogger.Info("AuthenticateUser PARAM UserId::" + UserId + "UserPassword::" + UserPassword);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                //{
                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                response = UserImpl.AuthenticateUser(UserId, UserPassword);
            }
            //}
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new LoginResponse();

            //    ResponseErrorHandler.HandleException(response, exp);
            //}
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LoginResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public QuickLinkResponse LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId, UserSessionDTO userSession)
        {
            QuickLinkResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadUserDefaultQuickLinksbyLocation Called");
                QuivusLogger.Info("LoadUserDefaultQuickLinksbyLocation PARAM UserId::" + UserId + "ClinicBranchId::" + ClinicBranchId);
             
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.LoadUserDefaultQuickLinksbyLocation(UserId, ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new QuickLinkResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new QuickLinkResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public SearchEngineResponse LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId, UserSessionDTO userSession)
        {
            SearchEngineResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadUserSearchEnginebyLocation Called");
                QuivusLogger.Info("LoadUserSearchEnginebyLocation PARAM UserId::" + UserId + "ClinicBranchId::" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.LoadUserSearchEnginebyLocation(UserId, ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SearchEngineResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchEngineResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To set specified Search engine As default
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        public SearchEngineResponse SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID, UserSessionDTO userSession)
        {
            SearchEngineResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SetDefaultSearchEngine Called");
                QuivusLogger.Info("SetDefaultSearchEngine PARAM ProviderUserID::" + ProviderUserID + "ClinicBranchId::" + ClinicBranchID + "UserSearchEngineID::" + UserSearchEngineID);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.SetDefaultSearchEngine(ProviderUserID, ClinicBranchID, UserSearchEngineID);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SearchEngineResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchEngineResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// Changed 07 Feb 2011
        /// Changed by Mansoor
        /// Params removed [int UserSearchEngineID, bool IsDefault] //M
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>
        /// <param name="UserSearchEngineID"></param>
        /// <param name="IsDefault"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public SearchEngineResponse ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<Int32> SearchEngineTypeIDs, int CreatedBy, UserSessionDTO userSession)
        {
            SearchEngineResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageUserSearchEngine Called");
                QuivusLogger.Info("ManageUserSearchEngine PARAM ProviderUserID::" + ProviderUserID + "ClinicBranchId::" + ClinicBranchID +
                    "SearchEngineTypeIDs::" + SearchEngineTypeIDs + "CreatedBy::" + CreatedBy);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.ManageUserSearchEngine(ProviderUserID, ClinicBranchID, SearchEngineTypeIDs, CreatedBy);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SearchEngineResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchEngineResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ismail ou
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public SearchEngineResponse ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy, UserSessionDTO userSession)
        {
            SearchEngineResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ClearUserAddedSearchEngines Called");
                QuivusLogger.Info("ClearUserAddedSearchEngines PARAM ProviderUserID::" + ProviderUserID + "ClinicBranchId::" + ClinicBranchID + "ModifiedBy::" + ModifiedBy);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.ClearUserAddedSearchEngines(ProviderUserID, ClinicBranchID, ModifiedBy);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SearchEngineResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SearchEngineResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 jan 2011
        /// Method Used To Load Clinic Users
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>

        public ContactSearchResponse LoadUsersByClinicBranchID(int ClinicBranchId, UserSessionDTO userSession)
        {
            ContactSearchResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadUsersByClinicBranchID Called");
                QuivusLogger.Info("LoadUsersByClinicBranchID PARAM ClinicBranchId::" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.LoadUsersByClinicBranchID(ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ContactSearchResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ContactSearchResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User/ Update User Information
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        public UserRegistrationResponse RegisterNewUser(RegistrationDTO registrationDTO, UserSessionDTO userSession)
        {
            UserRegistrationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService RegisterNewUser Called");
                QuivusLogger.Info("RegisterNewUser PARAM registrationDTO::" + registrationDTO);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.RegisterNewUser(registrationDTO);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new UserRegistrationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new UserRegistrationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        public BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO, UserSessionDTO userSession)
        {
            BaseUserRegistrationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService RegisterBaseUser Called");
                QuivusLogger.Info("RegisterBaseUser PARAM BaseUserDTO::" + baseUserDTO);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.RegisterBaseUser(baseUserDTO);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new BaseUserRegistrationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new BaseUserRegistrationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);

                if (response == null)
                    response = new BaseUserRegistrationResponse();
                ResponseErrorHandler.HandleException(response, ex);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>        
        public EmailResponse GetUserEmailByUserId(int userId, UserSessionDTO userSession)
        {
            EmailResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetUserEmailByUserId Called");
                QuivusLogger.Info("GetUserEmailByUserId PARAM userId::" + userId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.GetUserEmailByUserId(userId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new EmailResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new EmailResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad ISmail
        /// Date 22 Feb 2011
        /// This call will load user basic infor like name email using thier Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList, UserSessionDTO userSession)
        {
            List<UserBasicInfoDTO> response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetUserBasicInfoById Called");
                QuivusLogger.Info("GetUserBasicInfoById PARAM ProviderIdList::" + ProviderIdList);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.GetUserBasicInfoById(ProviderIdList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }

        // <summary>
        /// Author Muhammad Ismail
        /// Date 29 march 2011
        /// Method WIll be use to verify USer password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ServiceResponse VerifyUser(int UserId, string password, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService VerifyUser Called");
                QuivusLogger.Info("VerifyUser PARAM UserId::" + UserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.VerifyUser(UserId, password);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Will be used to save/update patient Info.
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        public PatientResponse ManagePatient(PatientDTO patientDTO, UserSessionDTO userSession)
        {
            PatientResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatient Called");
                QuivusLogger.Info("ManagePatient PARAM patientDTO::" + patientDTO);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.ManagePatient(patientDTO);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientResponse LoadPatientByPatientId(int PatientId, UserSessionDTO userSession)
        {
            PatientResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientByPatientId Called");
                QuivusLogger.Info("LoadPatientByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.LoadPatientByPatientId(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 2 2011
        /// Method Will BE used to load Base User Info By User Id :)
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public BaseUserResponse LoadBaseUserByUserId(int UserId, UserSessionDTO userSession)
        {
            BaseUserResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadBaseUserByUserId Called");
                QuivusLogger.Info("LoadBaseUserByUserId PARAM UserId::" + UserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.LoadBaseUserByUserId(UserId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new BaseUserResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new BaseUserResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        public bool SyncExternalPatients(int FromRecord, int ToRecord)
        {
            bool response = false;
            int TotalNumberOfRecords = 0;
            int StartRecord = 1;
          
            int ThresHold = 100;
          
            try
            {
                QuivusLogger.Info("In QuivusManagerService SyncExternalPatients Called");
                QuivusLogger.Info("SyncExternalPatients PARAM FromRecord::" + FromRecord + "ToRecord :: " + ToRecord);


                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }


             //   response = UserImpl.SyncExternalPatients(FromRecord, ToRecord);
                ExternalPatientDTO externalpatient = UserImpl.LoadExternalPatients(FromRecord, ToRecord);

                if (externalpatient != null)
                {
                    TotalNumberOfRecords = externalpatient.TotalRecord;

                    if (TotalNumberOfRecords <= ThresHold)
                    {
                        response = UserImpl.SyncExternalPatients(StartRecord, ThresHold);
                    }
                    else
                    { 
                    // Total Number of Records are More than thershold break the call
                        int TotalIteration = TotalNumberOfRecords / ThresHold;
                        for (StartRecord = 1; StartRecord <= TotalIteration; StartRecord++)
                        {                       
                            // There is On Exception If Number of Records Are Less than Threshold TO be Handled
                            response = UserImpl.SyncExternalPatients(StartRecord, ThresHold);
                        }
                    }

                    /*while (TotalNumberOfRecords > 0)
                    {
                        if (LastRecord > TotalNumberOfRecords)
                        {
                            ThresHold  = TotalNumberOfRecords - LastRecord ;
                            if (ThresHold < 0)
                                ThresHold *= -1;
                        }

                        response = UserImpl.SyncExternalPatients(StartRecord, ThresHold);

                        StartRecord += 1;

                        if (LastRecord >= TotalNumberOfRecords || LoopBreaker)
                        {
                            break; // Exit the Loop
                        }
                        else
                        {
                            LastRecord = LastRecord + ThresHold;

                            if (LastRecord > TotalNumberOfRecords)
                            {
                                ThresHold = TotalNumberOfRecords - LastRecord;
                                if (ThresHold < 0)
                                    ThresHold *= -1;

                                LoopBreaker = true;
                            }
                        }

                    } */
                }

            }

            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }
    }
}