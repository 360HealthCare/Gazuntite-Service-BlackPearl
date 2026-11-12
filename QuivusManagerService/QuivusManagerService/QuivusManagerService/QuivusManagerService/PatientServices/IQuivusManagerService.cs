using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomResponse.RecentPatient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization;
using QuivusManager.ServiceResponse.ServiceCustomSession;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{

    public partial interface IQuivusManagerService
    {
        [OperationContract]

        PatientSaveResponse SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO, UserSessionDTO userSession);


        [OperationContract]
        ServiceResponse SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        [OperationContract]
        PatientEmergencyResponse ManagePatientEmergencyContact(int PatientID, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 27 Jan 2011
        /// Get portion of patient search data, based on query string and demand page no
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchString"></param>
        /// <param name="demandPageNo"></param>
        /// <returns></returns>
        [OperationContract]
        PatientSearchResponse SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient in the most patient list for that user on this clinic
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Load the list of most recent loaded patients for the provider for location
        /// </summary>
        /// <param name="providerUserId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        RecentPatientRespose LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId, UserSessionDTO userSession);

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
        [OperationContract]
        PatientProfileSettingResponse LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 08 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing For specific Section
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        ///  /// <param name="SectionId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientProfileSettingResponse LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Manage Patient Profile Settings 
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientProfileSettingResponse ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingsObj, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient allergy info provided
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="patientAllergy"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientProfileResponse LoadPatientCompleteProfileForClinic(int patientId, int clinicBranchId, int providerUserId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientProfileInfoResposne LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient profile allergies for the provider, to show in patient exam container
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientProfileAllergyResponse LoadPatientAllergiesForProvider(int patientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        [OperationContract]
        PatientProfileDiagnosisResponse LoadPatientDiagnosisForProvider(int patientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        [OperationContract]
        PatientProfileImmunizationResponse LoadPatientImmunizationForProvider(int patientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>    
        [OperationContract]
        PatientProfileChronicMedicationResponse LoadPatientMedicationForProvider(int patientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 4 Feb 2011
        /// load the patient profile Insurance for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>      
        [OperationContract]
        PatientProfileInsuranceResponse LoadPatientInsuranceForProvider(int patientId, UserSessionDTO userSession);
    }
}
