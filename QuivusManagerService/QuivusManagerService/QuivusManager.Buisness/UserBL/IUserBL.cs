using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
//using QuivusModel.User.ClinicUser;
//using QuivusModel.Association;
//using QuivusModel.Common;

namespace QuivusManager.Buisness.UserBL
{
    public interface IUserBL
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
        
        LoginResponse AuthenticateUser(string UserId, string UserPassword);

        QuickLinkResponse LoadUserDefaultQuickLinksbyLocation(int UserId, int ClinicBranchId);

        SearchEngineResponse LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId);

        SearchEngineResponse SetDefaultSearchEngine(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID);

        SearchEngineResponse ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy);

        SearchEngineResponse ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy);

        ContactSearchResponse LoadUsersByClinicBranchID(int ClinicBranchID);

        UserRegistrationResponse RegisterNewUser(RegistrationDTO registrationDTO);

        BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO);

        EmailResponse GetUserEmailByUserId(int userId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param>
        /// <returns></returns>       
        Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID);

        List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        bool IsProviderAtClinic(int userId, int clinicBranchId);

        ServiceResponse.ServiceCustomResponse.ServiceResponse VerifyUser(int UserId, string password);

        PatientResponse ManagePatient(PatientDTO patientDTO);

        PatientResponse LoadPatientByPatientId(int PatientId);

        BaseUserResponse LoadBaseUserByUserId(int UserId);




        bool SyncExternalPatients(int FromRecord, int ToRecord);

        ExternalPatientDTO LoadExternalPatients(int FromRecord, int ToRecord);

        /// <summary>
        /// Author Mansoor
        /// Date 23 aug 2011
        /// verify email existance 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        bool VerifyUserEmail(string email);

        /// <summary>
        /// Author Mansoor
        /// Date 14 sep 2011        
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>        
        int GetClinicBranchIdByClinicBranchCode(string branchCode);

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011       
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>        
        int ManageClinicUserProfile(QuivusManager.IntegratedServies.UserManagerService.ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011    
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        QuivusManager.IntegratedServies.UserManagerService.ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId);



        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011
        /// Method Used To load Speciality Types
        /// </summary>
        /// <returns></returns>        
        List<SpecialityTypeDTO> LoadSpecialityTypes();


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        List<QuivusManager.IntegratedServies.UserManagerService.PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId);
        
        int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId);
        
        int ManageProviderAuthorization(QuivusManager.IntegratedServies.UserManagerService.PatientProviderAssociationDTO patientProviderAssociationDTO);


        List<QuivusManager.IntegratedServies.UserManagerService.ProviderSearchDTO> SearchProviderForAppointment(QuivusManager.IntegratedServies.UserManagerService.SearchProviderCriteriaDTO criteria);

        List<StationDTO> GetStationNamesByStationID(List<int> stationIDList);


        /// <summary>
        /// Added by Zahid on 13 Jan 2012
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        List<ContactSearchDTO> LoadProviderByClinicBranchID(int ClinicBranchID);

        /// <summary>
        /// Added by Zahid Mehmood on 17 Jan 2012
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        ClinicProviderSearchDTO LoadClinicUserBasicInfo(int userId, int clinicBranchId);
    }
}
