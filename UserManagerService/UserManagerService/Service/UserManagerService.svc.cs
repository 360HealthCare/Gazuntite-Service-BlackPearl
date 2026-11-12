using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

using UserManagerService;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;

using UserManagerService.Business.Authentication;
using UserManagerService.Business.Authentication.Impl;
using Quivus.Model.Common;

using UserManagerService.Business.SearchEngine;
using UserManagerService.Business.SearchEngine.Impl;

using UserManagerService.Business.QuickLink;

using UserManagerService.Business.Search;
using UserManagerService.Business.Search.Impl;

using Quivus.Model.User;
using UserManager.Business.Registration;
using UserManager.Business.Registration.Impl;

using UserManagerService.Business.Common;

using UserManager.Business.Allergy;
using UserManager.Business.Allergy.Impl;

using UserManagerService.Business.ICDCode;
using UserManagerService.Business.ICDCode.Impl;
using Quivus.Model.User.Patient;
using UserManager.Business.User.Patient.PatientRegistration;
using UserManager.Business.User.Patient.PatientRegistration.Impl;

using UserManager.Business.User.Patient;
using UserManager.Business.User.Patient.Impl;

using Quivus.Model.AddressBook;
using UserManager.Business.User;
using UserManager.Business.User.Impl;
using UserManager.Business.Utility;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
//using Quivus.Utility.Fault;
using QuivusUtility.Utilities;
using Quivus.Model.QuickLink;
using Quivus.Model.SearchEngine;
using Quivus.Model.Search;

using Quivus.Model.RecentPatient;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.TypeDTOs;

using Quivus.Model.Visit;
using Quivus.Model.User;
using QuivusModel.User.Patient;
using QuivusModel.User;
using QuivusModel.User.ClinicUser;
using UserManager.Business.User.ClinicUser;
using UserManager.Business.User.ClinicUser.Impl;
using QuivusModel.Common;
using QuivusModel.Association;
using QuivusModel.Clinic;
using UserManager.Business.Clinic;
using Quivus.Model.Clinic;
using UserManager.Business.Clinic.Impl;
using Quivus.Utility;
using QuivusModel.Search;
using QuivusModel.Billing.User;
using UserManager.Business.Billing;
using QuivusModel.Billing.PendingBills;
using UserManager.Business.Billing.Impl;

//using Quivus.Model.ServiceCustomResponse.Visit;

namespace UserManagerService.Service
{

    /// <summary>
    /// Anotation for Service Behavior Updated By Ismail
    /// </summary>
    /// 
    //[ExceptionShielding("UserManagerServicePolicy")]
    // [ServiceBehavior (InstanceContextMode=InstanceContextMode.PerCall) ] Commented By Ismail
    public class UserManagerService : QuivusBaseService, IUserManagerService
    {
        // to cache the static data
        //  private static AddressBookStaticDataResponse objCacheAddressBookStaticDataResponse = null;
        //  private static RegistrationStaticDataResponse objCacheRegistrationStaticDataResponse = null;

        IAuthenticationManager userAuthenticationImpl = null;
        ISearchEngineManager searchEngineImpl = null;


        IQuivusRegistartionManager quivusRegistartionImpl = null;
        IPatientRegistrationManager patientRegistartionImpl = null;

        IQuickLinkManager quicklinkImpl = null;
        IContactSearchManager contactSearchImpl = null;

        StaticDataManager sdManager = null;

        IUserManager userManagerImpl = null;

        IPatientRegistrationManager patientRegisterationImpl = null;

        IPatientManager patientManagerImpl = null;

        IAllergyManager allergyManager = null;

        IClinicUserManager clinicUserManager = null;

        IClinicManager clinicManager = null;

        IBillingManager billingManagerImpl = null;

        //M IVisitService visitService = null;

        #region Default Implementation of GetServiceVersion
        public override string GetServiceVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }
        #endregion



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
        public LoginDTO AuthenticateUser(string UserId, string UserPassword)
        {
            LoginDTO response = null;

            try
            {
                if (userAuthenticationImpl == null)
                {
                    userAuthenticationImpl = new AuthenticationManagerImpl();
                }
                //throw new Exception("Ismail");
                response = userAuthenticationImpl.AutheticateUser(UserId, UserPassword);
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

            return response;
        }



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
                if (userAuthenticationImpl == null)
                {
                    userAuthenticationImpl = new AuthenticationManagerImpl();
                }

                userAuthenticationImpl.LogLoginHistory(userId, userType, clinicBranch, isSucessFull);
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
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<SearchEngineDTO> LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId)
        {
            List<SearchEngineDTO> response = null;
            try
            {
                if (searchEngineImpl == null)
                {
                    searchEngineImpl = new SearchEngineManagerImpl();
                }

                response = searchEngineImpl.LoadUserSearchEnginebyLocation(UserId, ClinicBranchId);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        public List<SearchEngineDTO> LoadAllSearchEngines()
        {
            List<SearchEngineDTO> response = null;
            try
            {
                if (searchEngineImpl == null)
                {
                    searchEngineImpl = new SearchEngineManagerImpl();
                }

                response = searchEngineImpl.LoadAllSearchEngines();
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
        public int SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID)
        {
            int response = 0;
            try
            {
                if (searchEngineImpl == null)
                {
                    searchEngineImpl = new SearchEngineManagerImpl();
                }

                response = searchEngineImpl.SetSearchEngineAsDefault(ProviderUserID, ClinicBranchID, UserSearchEngineID);
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
        public List<QuickLinkDTO> LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId)
        {
            List<QuickLinkDTO> response = null;
            try
            {
                if (quicklinkImpl == null)
                {
                    quicklinkImpl = new QuickLinkManagerImpl();
                }
                response = quicklinkImpl.LoadUserDefaultQuickLinksByLocation(UserId, ClinicBranchId);
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
            return response;
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
        /* Commented By Ismail
        public String SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message)
        {
            String response = null;

            try
            {
                if (emailMangerImpl == null)
                {
                    emailMangerImpl = new EmailManagerImpl();
                }
                response = emailMangerImpl.SendEmail(requestedUserId, clinicBranchID, subject, receiptEmail, message);
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
            return response;
        }
        */
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 jan 2011
        /// Method Used To Load Clinic Users
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>

        public List<ContactSearchDTO> LoadUsersByClinicBranchID(int ClinicBranchId)
        {
            List<ContactSearchDTO> response = null;
            try
            {
                if (contactSearchImpl == null)
                {
                    contactSearchImpl = new ContactSearchManagerImpl();
                }

                response = contactSearchImpl.LoadUsersByClinicBranchID(ClinicBranchId);
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
        public int ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<Int32> SearchEngineTypeIDs, int CreatedBy)
        {
            int response = 0;
            try
            {
                if (searchEngineImpl == null)
                {
                    searchEngineImpl = new SearchEngineManagerImpl();
                }

                response = searchEngineImpl.ManageUserSearchEngine(ProviderUserID, ClinicBranchID, SearchEngineTypeIDs, CreatedBy);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy)
        {
            int response = 0;
            try
            {
                if (searchEngineImpl == null)
                {
                    searchEngineImpl = new SearchEngineManagerImpl();
                }
                response = searchEngineImpl.ClearUserAddedSearchEngines(ProviderUserID, ClinicBranchID, ModifiedBy);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User/ Update User Information
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
       [Obsolete]
        public UserDTO RegisterNewUser(RegistrationDTO registrationDTO)
        {
            UserDTO response = null;
            try
            {
                if (quivusRegistartionImpl == null)
                {
                    quivusRegistartionImpl = new QuivusRegistartionManagerImpl();
                }
                response = quivusRegistartionImpl.RegisterNewUser(registrationDTO);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return all static data required for registeration
        /// </summary>
        /// <returns></returns>
        public RegisterationStaticDataDTO GetRegisterationStaticData()
        {
            RegisterationStaticDataDTO response = null;
            try
            {
                if (sdManager == null)
                {
                    sdManager = new StaticDataManager();
                }
                response = sdManager.GetAllRegisterationStaticData();
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011 Move This Method TO Main Service
        /// </summary>
        /// <returns></returns>
        //public AddressBookStaticDataResponse GetAddressBookStaticData()
        //{
        //    AddressBookStaticDataResponse response = null;
        //    try
        //    {
        //        //if (objCacheAddressBookStaticDataResponse == null)
        //        //{
        //        //    IAddressBookService abManager = new AddressBookServiceImpl();
        //        //    response = abManager.GetAllAddressBookStaticData();

        //        //    if (response.ResponseStatus)
        //        //    {
        //        //        objCacheAddressBookStaticDataResponse = response;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    response = objCacheAddressBookStaticDataResponse;
        //        //}
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new AddressBookStaticDataResponse();

        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Mansoor
        /// Date 19 JAn 2011
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns> // Move This Method To Main Service
        //public CityResponse GetCityListByState(int stateId)
        //{
        //    CityResponse response = null;
        //    try
        //    {
        //        IAddressBookService abManager = new AddressBookServiceImpl();
        //        response = abManager.GetAllCityListByStateId(stateId);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new CityResponse();

        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Mansoor
        /// Date 20 Jan 2011
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        //public ZipCodeResponse GetZipCodesByStateIdAndCityName(int stateId, string cityName)
        //{
        //    ZipCodeResponse response = null;
        //    try
        //    {
        //        IAddressBookService abManager = new AddressBookServiceImpl();
        //        response = abManager.GetZipCodesByStateIdAndCityName(stateId, cityName);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new ZipCodeResponse();

        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}



        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        public int RegisterBaseUser(BaseUserDTO baseUserDTO)
        {
            int response = 0;
            try
            {
                if (quivusRegistartionImpl == null)
                {
                    quivusRegistartionImpl = new QuivusRegistartionManagerImpl();
                }
                response = quivusRegistartionImpl.RegisterBaseUser(baseUserDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 JAn 2011
        /// To fetch the list of 
        /// </summary>
        /// <param name="allergyName"></param>
        /// <returns></returns>
        public List<AllergySearchDTO> SearchAllergyTypeByName(string allergyName)
        {
            List<AllergySearchDTO> response = null;
            try
            {
                if (allergyManager == null)
                {
                    allergyManager = new AllergyManagerImpl();
                }
                response = allergyManager.SearchAllergyTypeByName(allergyName);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Jan 2011
        /// search the matching icd codes and gets the description, on the bases of icd code provided
        /// </summary>
        /// <param name="icdCode"></param>
        /// <returns></returns>
        public List<ICDCodeSearchDTO> SearchICDCodeByCode(string icdCode)
        {
            List<ICDCodeSearchDTO> response = null;
            try
            {
                IICDCodeManager icdcodeManager = new ICDCodeManagerImpl();
                response = icdcodeManager.searchICDCODEByCode(icdCode);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save the patient profile and language w.r.t the userId provided
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList)
        {
            int response = 0;
            try
            {
                if (patientRegistartionImpl == null)
                {
                    patientRegistartionImpl = new PatientRegistrationManagerImpl();
                }
                response = patientRegistartionImpl.SavePatientBasicProfileAndLangauge(userId, managedByUserID, patientDTO, PatientLanguageDTOList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        //public List<string> TestSize(List<string> testList)
        //{
        //    List<string> response = new List<string>();

        //    if (testList == null || testList.Count <= 0)
        //    {
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //        response.Add("mansoor");
        //        response.Add("hassan");
        //    }

        //    foreach (string sd in testList)
        //    {
        //        response.Add(sd);
        //        response.Add(sd);
        //    }

        //    return response;
        //}


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        public int ManagePatientEmergencyContact(int PatientId, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList)
        {
            int response = 0;
            try
            {
                if (patientRegistartionImpl == null)
                {
                    patientRegistartionImpl = new PatientRegistrationManagerImpl();
                }
                response = patientRegistartionImpl.ManagePatientEmergencyContact(EmergencyContactList, PatientId, CreatedBy);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        public int SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO)
        {
            int response = 0;
            try
            {
                if (patientRegistartionImpl == null)
                {
                    patientRegistartionImpl = new PatientRegistrationManagerImpl();
                }
                response = patientRegistartionImpl.ManagePatientGurantor(patientID, managedByUserID, patientGurantorDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the user address book, specified in the Party Type
        /// </summary>
        /// <param name="addressBookDTO"></param>
        /// <returns></returns>

        //public ServiceResponse SaveAddressBook(int roleProfileID, int managedByUserID, AddressBookDTO addressBookDTO)
        //{
        //    ServiceResponse response = null;
        //    try
        //    {
        //        IAddressBookService abManager = new AddressBookServiceImpl();
        //        response = abManager.SaveAddressBook(roleProfileID, managedByUserID, addressBookDTO);
        //    }
        //    catch (Exception exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new ServiceResponse();

        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        public int SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList)
        {
            int response = 0;
            try
            {
                QuivusLogger.Info("Call Recieved To Save Patient Insurance");

                if (patientRegistartionImpl == null)
                {
                    patientRegistartionImpl = new PatientRegistrationManagerImpl();
                }
                response = patientRegistartionImpl.ManagePatientInsuranceInfo(patientID, managedByUserID, patientInsuranceDTOList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient health Info like allergies, medical history, family history
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientHealthDTO"></param>
        /// <returns></returns>
        public int SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO)
        {
            int response = 0;
            try
            {
                if (patientRegistartionImpl == null)
                {
                    patientRegistartionImpl = new PatientRegistrationManagerImpl();
                }
                response = patientRegistartionImpl.ManagePatientHealthInfo(patientID, managedByUserID, patientHealthDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 19 Jan 2011
        /// Return static data required for registeration, based on type provided
        /// </summary>
        /// <returns></returns>
        public RegisterationStaticDataDTO GetRegisterationStaticDataByType(List<string> typeList)
        {
            RegisterationStaticDataDTO response = null;
            try
            {
                if (sdManager == null)
                {
                    sdManager = new StaticDataManager();
                }
                response = sdManager.GetRegisterationStaticDataByType(typeList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 27 Jan 2011
        /// Get portion of patient search data, based on query string and demand page no
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchString"></param>
        /// <param name="demandPageNo"></param>
        /// <returns></returns>
        public PatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo)
        {
            PatientSearchResultDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient in the most patient list for that user on this clinic
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>       
        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool response = false;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.SaveRecentPatient(clinicBranchId, providerUserId, patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Load the list of most recent loaded patients for the provider for location
        /// </summary>
        /// <param name="providerUserId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadRecentPatientsByProviderIdAndBranchId(providerUserId, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public PatientProfileCompleteSettingDTO LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId)
        {
            PatientProfileCompleteSettingDTO response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadPatientProfileSettings(clinicBranchId, UserId, PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public PatientProfileCompleteSettingDTO LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId)
        {
            PatientProfileCompleteSettingDTO response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadPatientProfileSettingsBySection(clinicBranchId, UserId, PatientId, SectionId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj)
        {
            int response = 0;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.ManagePatientProfileSettings(ProfileSettingObj);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient allergy info provided
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="patientAllergy"></param>
        /// <returns></returns>     
        public bool SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy)
        {
            bool response = false;
            try
            {
                if (allergyManager == null)
                {
                    allergyManager = new AllergyManagerImpl();
                }
                response = allergyManager.SavePatientAllergy(patientId, createdBy, patientAllergy);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>       
        public PatientProfileDTO LoadPatientCompleteProfileForClinic(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileDTO response = null;
            try
            {
                if (patientRegistartionImpl == null)
                {
                    patientRegistartionImpl = new PatientRegistrationManagerImpl();
                }
                response = patientRegistartionImpl.LoadPatientCompleteProfilebyPatientId(patientId, clinicBranchId, providerUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public List<ProfileStaticDataDTO> LoadProfileStaticData()
        {
            List<ProfileStaticDataDTO> response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadProfileStaticData();
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient profile allergies for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>       
        public PatientProfileAllergyDTO LoadPatientAllergiesForProvider(int patientId)
        {
            PatientProfileAllergyDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientAllergiesForProvider(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 4 Feb 2011
        /// load the patient profile Insurance for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>       
        public PatientProfileInsuranceDTO LoadPatientInsuranceForProvider(int patientId)
        {
            PatientProfileInsuranceDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientInsuranceForProvider(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>        
        public String GetUserEmailByUserId(int userId)
        {
            String response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.GetUserEmailByUserId(userId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>        
        public PatientProfileTaggedDTO LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileTaggedDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientProfileInfoForProvider(patientId, clinicBranchId, providerUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>       

        public PatientProfileDiagnosisDTO LoadPatientDiagnosisForProvider(int patientId)
        {
            PatientProfileDiagnosisDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientDiagnosisForProvider(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>           
        public PatientProfileImmuniztionDTO LoadPatientImmunizationForProvider(int patientId)
        {
            PatientProfileImmuniztionDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientImmunizationForProvider(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>            
        public PatientProfileChronicMedicationDTO LoadPatientMedicationForProvider(int patientId)
        {
            PatientProfileChronicMedicationDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientMedicationForProvider(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public List<ImageTypeDTO> LoadImageTypes()
        {
            List<ImageTypeDTO> response = null;
            try
            {
                if (quivusRegistartionImpl == null)
                {
                    quivusRegistartionImpl = new QuivusRegistartionManagerImpl();
                }
                response = quivusRegistartionImpl.LoadImageTypes();
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>        
        public Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID)
        {
            Guid response = new Guid();
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.GetPartyGuidByProfileIdAndTypeId(roleProfileID, addressPartyTypeID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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

        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.GetUserBasicInfoById(ProviderIdList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the module types for notes etc
        /// </summary>              
        /// <returns></returns>        
        public List<ModuleTypeDTO> LoadModuleTypes()
        {
            List<ModuleTypeDTO> response = null;
            try
            {
                if (sdManager == null)
                {
                    sdManager = new StaticDataManager();
                }
                response = sdManager.ListAllModuleTypeDTOs();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>    
        public bool IsProviderAtClinic(int userId, int clinicBranchId)
        {
            bool isProvider = false;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                isProvider = userManagerImpl.IsProviderAtClinic(userId, clinicBranchId);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return isProvider;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// get the RecGuid of respective table, used to load the patient profile customized setting (AB)
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partyTypeID"></param>
        /// <returns></returns>        
        public Guid GetPartyGuidByPatientIdAndPartyTypeId(int patientId, int partyTypeID)
        {
            Guid response = new Guid();
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.GetPartyGuidByPatientIdAndPartyTypeId(patientId, partyTypeID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public int ManagePatient(PatientDTO patientDTO)
        {
            int Result = 0;
            try
            {
                if (patientRegisterationImpl == null)
                {
                    patientRegisterationImpl = new PatientRegistrationManagerImpl();
                }

                Result = patientRegisterationImpl.ManagePatient(patientDTO);
            }
            catch (InvalidArgumentException exp)
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
            return Result;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>

        public PatientDTO LoadPatientByPatientId(int PatientId)
        {
            PatientDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 26 august 2011
        /// Method Used To load patient basic info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientBasicInfoDTO LoadPatientBasicInfoByPatientId(int PatientId)
        {
            PatientBasicInfoDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientBasicInfoByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 27 august 2011
        /// Method Used To load clinic brach Id
        /// </summary>
        /// <param name="ClicniBranchName"></param>
        /// <returns></returns>
        public int LoadClinicBranchIdByClinicBranchName(String ClicniBranchName)
        {
            int response = 0;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadClinicBranchIdByClinicBranchName(ClicniBranchName);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 27 august 2011
        /// Method Used To load patient basic info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public PatientUserInfo LoadPatientUserInfoByQuivusId(String QuivusId)
        {
            PatientUserInfo response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadPatientUserInfoByQuivusId(QuivusId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 29 march 2011
        /// Method WIll be use to verify USer password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public UserVerificationDTO VerifyUser(int UserId, string password)
        {
            UserVerificationDTO response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.VerifyUser(UserId, password);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public BaseUserDTO LoadBaseUserByUserId(int UserId)
        {
            BaseUserDTO response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadBaseUserByUserId(UserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 26 2011
        /// Method Will BE used to load quivus id Info By patient Id :)
        /// Patient_QuivudIDLoadByPatientID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>

        public string LoadQuivusIdByPatientId(int PatientId)
        {
            string response = String.Empty;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadQuivusIdByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 26 2011
        /// Method Will BE used to load rcopia id Info By patient Id :)
        /// Patient_QuivudIDLoadByPatientID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>

        public string LoadRcopiaIdByPatientId(int PatientId)
        {
            string response = String.Empty;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadRcopiaIdByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 10 2011
        /// Method Will BE used to update e-prescription details :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="RCopiaID"></param>
        /// <returns></returns>
        public bool UpdatePatientEPrescriptionDetails(String RCopiaID, int PatientId)
        {
            bool response = false;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.UpdatePatientEPrescriptionDetails(RCopiaID, PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        public ExternalPatientDTO LoadExternalPatients(int FromRecord, int ToRecord)
        {
            ExternalPatientDTO response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.LoadExternalPatients(FromRecord, ToRecord);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 05 july 2011
        /// Method Will BE used to update external patients :)
        /// </summary>
        /// <returns></returns>
        public bool UpdatePatientRCopiaId(List<PatientRcopiaDTO> patientList)
        {
            bool response = false;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.UpdatePatientRCopiaId(patientList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 18/7/2011
        /// Method will return provider List
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID)
        {
            List<ProviderDTO> providerList = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                providerList = userManagerImpl.LoadAllProviderByClinicBranchId(clinicBranchID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return providerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, basic user info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public BaseUserDTO LoadPatientBaseUserInfoByPatientIdForCompleteProfile(int patientId)
        {
            BaseUserDTO usr = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                usr = userManagerImpl.LoadPatientBaseUserInfoByPatientIdForCompleteProfile(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return usr;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, allergy info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientAllergyDTO> LoadPatientAllergyInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientAllergyDTO> providerList = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                providerList = userManagerImpl.LoadPatientAllergyInfoByPatientIdForCompleteProfile(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return providerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 aug 2011
        /// verify email existance
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public bool VerifyUserEmail(string email)
        {
            bool flag = false;

            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                flag = userManagerImpl.VerifyUserEmail(email);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return flag;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 sep 2011        
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>      
        public int GetClinicBranchIdByClinicBranchCode(string branchCode)
        {
            int brID = 0;

            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                brID = userManagerImpl.GetClinicBranchIdByClinicBranchCode(branchCode);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return brID;
        }

        public DateTime DateTestService(DateTime date)
        {
            QuivusLogger.Debug("Test Date Recieved::" + date);
            return DateTime.Now;
        }




        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>      
        public int ManageClinicUserProfile(ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId)
        {
            int profileId = 0;

            try
            {
                if (clinicUserManager == null)
                {
                    clinicUserManager = new ClinicUserManagerImpl();
                }
                profileId = clinicUserManager.ManageClinicUserProfile(clinicUser, userTypeId, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return profileId;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId)
        {
            ClinicUserDTO profile = null;

            try
            {
                if (clinicUserManager == null)
                {
                    clinicUserManager = new ClinicUserManagerImpl();
                }
                profile = clinicUserManager.LoadClinicUserProfile(userId, profileId, userTypeId, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return profile;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011
        /// Method Used To load Speciality Types
        /// </summary>
        /// <returns></returns>       
        public List<SpecialityTypeDTO> LoadSpecialityTypes()
        {
            List<SpecialityTypeDTO> list = null;

            try
            {
                if (sdManager == null)
                {
                    sdManager = new StaticDataManager();
                }
                list = sdManager.LoadSpecialityTypes();
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<ProviderDTO> LoadAllProviderByClinicBranchIdAndStationId(int clinicBranchID, List<int> stationIds, bool isLoadAll)
        {
            List<ProviderDTO> providerList = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                providerList = userManagerImpl.LoadAllProviderByClinicBranchId(clinicBranchID, stationIds, isLoadAll);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return providerList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<StationDTO> LoadAllStationsByClinicId(int clinicBranchID)
        {
            List<StationDTO> providerList = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                providerList = userManagerImpl.LoadAllStationsByClinicId(clinicBranchID);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return providerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public WorkingHourDTO LoadProviderWorkingHours(int providerUserId)
        {
            WorkingHourDTO workingHour = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                workingHour = userManagerImpl.LoadProviderWorkingHours(providerUserId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return workingHour;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId)
        {
            List<PatientProviderAssociationDTO> list = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                list = userManagerImpl.ListAllPatientProvidersWithLocations(patientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>       
        public int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId)
        {
            int flag = 0;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                flag = userManagerImpl.CheckProviderAuthorityForPatientInfoAccess(providerUserId, patientId, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return flag;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>      
        public int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO)
        {
            int flag = 0;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                flag = userManagerImpl.ManageProviderAuthorization(patientProviderAssociationDTO);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return flag;
        }


        public List<ProviderSearchDTO> SearchProviderForAppointment(SearchProviderCriteriaDTO criteria)
        {
            List<ProviderSearchDTO> list = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                list = userManagerImpl.SearchProviderForAppointment(criteria);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return list;
        }

        public PatientSearchResultDTO LoadUsersByPracticeId(int clinicBranchID, int pageNo, int pageSize)
        {


            IPatientManager patientManagerImpl = null;
            PatientSearchResultDTO response = null;
            try
            {
                if (patientManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                response = patientManagerImpl.LoadUsersByPracticeId(clinicBranchID, pageNo, pageSize);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 25/11/2011
        /// This method will Get Station Names By StationID
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<StationDTO> GetStationNamesByStationID(List<int> stationIDList)
        {
            List<StationDTO> list = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                list = userManagerImpl.GetStationNamesByStationID(stationIDList);

            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return list;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Create Date 8 Dec 2011
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public List<ClinicRoomDTO> LoadClinicRoomByStationId(int stationId)
        {
            List<ClinicRoomDTO> clinicRoomList = null;
            try
            {
                if (userManagerImpl == null)
                {
                    patientManagerImpl = new PatientManagerImpl();
                }
                clinicRoomList = patientManagerImpl.LoadClinicRoomByStationId(stationId);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return clinicRoomList;
        }

        /// <summary>
        /// Author Sami
        /// Date 15 Dec 2011      
        /// This method will return clinic, clinicbranch and station info for soap note
        /// </summary>        
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>  
        public ClinicDTO LoadClinicBranchAndStationInfo(int stationId)
        {
            ClinicDTO response = null;
            try
            {
                if (clinicManager == null)
                {
                    clinicManager = new ClinicManager();
                }

                response = clinicManager.LoadClinicBranchAndStationInfo(stationId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }


        /// <summary>
        /// Author Hassan
        /// Date 10 JAN 2012
        /// This method will return user types
        /// </summary>
        /// <returns></returns>
        public List<UserTypeDTO> LoadAllUserType()
        {
            List<UserTypeDTO> list = null;
            try
            {
                if (sdManager == null)
                {
                    sdManager = new StaticDataManager();
                }

                List<string> typeList = new List<string>();
                typeList.Add(ProjectConstant.USER_TYPE_REGISTERATION_STATIC_DATA);

                RegisterationStaticDataDTO regSD = sdManager.GetRegisterationStaticDataByType(typeList);
                list = regSD.UserTypes;
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return list;
        }

        public List<int> LoadAllClinicBranch()
        {
            List<int> ids = null;
            try
            {
                if (clinicManager == null)
                {
                    clinicManager = new ClinicManager();
                }

                ids = clinicManager.LoadAllClinicBranch();

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            return ids;
        }

        public List<int> LoadClinicBranchUsers(int userTypeId, int clinicBranchId)
        {
            List<int> ids = null;
            try
            {
                if (clinicManager == null)
                {
                    clinicManager = new ClinicManager();
                }

                ids = clinicManager.LoadClinicBranchUsers(userTypeId, clinicBranchId);

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            return ids;

        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 17 Jan 2012
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public ClinicProviderSearchDTO LoadClinicUserBasicInfo(int userId, int clinicBranchId)
        {
            ClinicProviderSearchDTO response = null;
            try
            {
                if (clinicUserManager == null)
                {
                    clinicUserManager = new ClinicUserManagerImpl();
                }
                response = clinicUserManager.LoadClinicUserBascInfo(userId, clinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        public int UpdateUserStatus(int userId, string update, bool status)
        {
            int result = 0;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }

                result = userManagerImpl.UpdateUserStatus(userId, update, status);

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }

            return result;

        }

        #region Billing Service related Mehtods

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/26/2012
        /// to get Patient info for super bill
        /// </summary>
        /// <param name="PatientIdList"></param>       
        /// <returns></returns>
        public List<SuperBillPatientInfoDTO> GetPatientBillingInfoByPatientId(List<int> PatientIdList)
        {
            List<SuperBillPatientInfoDTO> PatientInfoList = null;
            try
            {
                if (billingManagerImpl == null)
                {
                    billingManagerImpl = new BillingManagerImpl();
                }
                PatientInfoList = billingManagerImpl.GetPatientBillingInfoByPatientId(PatientIdList);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return PatientInfoList;
        }

        
        #endregion

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This call will load patient user basic info like name email using thier patient Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>

        public List<PatientBasicUserDTO> GetUserBasicInfoByPatientIds(List<int> PatientIds)
        {
            List<PatientBasicUserDTO> response = null;
            try
            {
                if (userManagerImpl == null)
                {
                    userManagerImpl = new UserManagerImpl();
                }
                response = userManagerImpl.GetUserBasicInfoByPatientIds(PatientIds);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
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
            return response;
        }
    }
}
