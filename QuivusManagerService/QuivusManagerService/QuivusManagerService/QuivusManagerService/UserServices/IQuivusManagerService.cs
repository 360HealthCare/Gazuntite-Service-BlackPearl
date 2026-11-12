using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    
    public partial interface IQuivusManagerService
    {
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
        [OperationContract]
        LoginResponse AuthenticateUser(string UserId, string UserPassword);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Quick link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        [OperationContract]

        QuickLinkResponse LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId, UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        [OperationContract]

        SearchEngineResponse LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId, UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// This Method Is Used To set specified Search engine As default
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        [OperationContract]
        SearchEngineResponse SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Jan 17 2011
        /// Method Used to MAnage the Search engine for user against Location
        /// /// Changed 07 Feb 2011
        /// Changed by Mansoor
        /// Params removed [int UserSearchEngineID, bool IsDefault] //M
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>        
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        [OperationContract]

        SearchEngineResponse ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<Int32> SearchEngineTypeIDs, int CreatedBy, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        [OperationContract]

        SearchEngineResponse ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 jan 2011
        /// Method Used To Load Clinic Users
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]

        ContactSearchResponse LoadUsersByClinicBranchID(int ClinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        [OperationContract]

        UserRegistrationResponse RegisterNewUser(RegistrationDTO registrationDTO, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        [OperationContract]

        BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]

        EmailResponse GetUserEmailByUserId(int userId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 22 Feb 2011
        /// This call will load user basic infor like name email using thier Ids :)
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 29 march 2011
        /// Method WIll be use to verify USer password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse VerifyUser(int UserId, string password, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Will be used to save/update patient Info.
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        [OperationContract]
        PatientResponse ManagePatient(PatientDTO patientDTO, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientResponse LoadPatientByPatientId(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date May 2 2011
        /// Method Will BE used to load Base User Info By User Id :)
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        [OperationContract]
        BaseUserResponse LoadBaseUserByUserId(int UserId, UserSessionDTO userSession);



        // <summary>
        // Author Ismail
        // Date 05 july 2011
        // Method Will BE used to load external patients :)
        // </summary>
        // <returns></returns>
        [OperationContract]
        bool SyncExternalPatients(int FromRecord, int ToRecord);
    }
}
