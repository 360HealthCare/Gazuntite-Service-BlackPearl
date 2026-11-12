using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomResponse.RecentPatient;

namespace QuivusManager.Buisness.PatientBL
{
    public interface IPatientBL
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        PatientEmergencyResponse ManagePatientEmergencyContact(int PatientId, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList);

        PatientSaveResponse SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList);

        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO);

        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO);

        ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList);

        PatientSearchResponse SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo);

        bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId);

        RecentPatientRespose LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId);

        PatientProfileSettingResponse LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId);

        PatientProfileSettingResponse LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId);

        PatientProfileSettingResponse ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj);

        ServiceResponse.ServiceCustomResponse.ServiceResponse SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy);

        PatientProfileResponse LoadPatientCompleteProfileForClinic(int patientId, int clinicBranchId, int providerUserId);

        ServiceResponse.ServiceCustomResponse.PatientProfileCustomization.PatientProfileInfoResposne LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId);

        ServiceResponse.ServiceCustomResponse.PatientProfileCustomization.PatientProfileAllergyResponse LoadPatientAllergiesForProvider(int patientId);

        ServiceResponse.ServiceCustomResponse.PatientProfileCustomization.PatientProfileDiagnosisResponse LoadPatientDiagnosisForProvider(int patientId);

        ServiceResponse.ServiceCustomResponse.PatientProfileCustomization.PatientProfileImmunizationResponse LoadPatientImmunizationForProvider(int patientId);

        ServiceResponse.ServiceCustomResponse.PatientProfileCustomization.PatientProfileChronicMedicationResponse LoadPatientMedicationForProvider(int patientId);

        ServiceResponse.ServiceCustomResponse.PatientProfileCustomization.PatientProfileInsuranceResponse LoadPatientInsuranceForProvider(int patientId);

        string LoadQuivusIdByPatientId(int PatientId);

        string LoadRcopiaIdByPatientId(int PatientId);
    }
}
