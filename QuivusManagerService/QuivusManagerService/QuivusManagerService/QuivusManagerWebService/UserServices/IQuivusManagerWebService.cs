using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManagerWebService
{
    public partial interface IQuivusManagerWebService
    {
        /// <summary>
        /// Author : Mansoor
        /// Created : 18 Aug 2011
        /// This Method is Used TO Authenicate the User 
        /// In Case Of Successfull verfication system return User DTO Object
        /// Update by Mansoor     
        /// Incase of unsucessfull scenario system sends the error message and code
        /// to Clinet side
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        LoginResponse AuthenticateUser(string UserId, string UserPassword);

        /// <summary>
        /// Author : Mansoor
        /// Created : 18 Aug 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        [OperationContract]
        BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 23 aug 2011
        /// verify email existance 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>            
        [OperationContract]
        bool VerifyUserEmail(string email);


        /// <summary>
        /// Author Mansoor
        /// Date 24 aug 2011
        /// save the patient info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns> 
        [OperationContract]
        PatientSaveResponse SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList);


        /// <summary>
        /// Author Mansoor
        /// Date 24 aug 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientProfileResponse LoadPatientCompleteProfile(int patientId, int clinicBranchId, int providerUserId);

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Will be used to save/update patient Info.
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        //[OperationContract]
        //PatientResponse ManagePatient(PatientDTO patientDTO, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        //[OperationContract]
        //PatientResponse LoadPatientByPatientId(int PatientId, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 8 Sep 2011        
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 8 Sep 2011                
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO);

        /// <summary>
        /// Author Mansoor
        /// Date 8 Sep 2011         
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList);

        /// <summary>
        /// Author Mansoor
        /// Date 8 Sep 2011 
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        [OperationContract]
        PatientEmergencyResponse ManagePatientEmergencyContact(int PatientID, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList);

        /// <summary>
        /// Author Mansoor
        /// Date 12 Sep 2011         
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns
        [OperationContract]
        ServiceResponse SaveAddressBook(int roleProfileID, int managedByUserID, QuivusManager.IntegratedServies.AddressBookService.AddressBookDTO addressBookDTO);


        /// <summary>
        /// Author Mansoor
        /// Date 14 sep 2011        
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        [OperationContract]
        int GetClinicBranchIdByClinicBranchCode(string branchCode);


        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011       
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [OperationContract]
        int ManageClinicUserProfile(ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011    
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId);



        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011
        /// Method Used To load Speciality Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]        
        List<SpecialityTypeDTO> LoadSpecialityTypes();


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        [OperationContract]       
        List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId);


        [OperationContract]        
        int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId);


        [OperationContract]        
        int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO);


        [OperationContract]
        List<ProviderSearchDTO> SearchProviderForAppointment(SearchProviderCriteriaDTO criteria);
        
    }
}
