using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Search;
using Quivus.Model.RecentPatient;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.User.Patient;
using QuivusModel.Clinic;

namespace UserManager.Business.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Date 27 Jan 2011    
    /// </summary>

    public interface IPatientManager
    {
        bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId);

        List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId);

        PatientSearchResultDTO LoadPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>     
        PatientProfileTaggedDTO LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel, also log in the most recent patients
        /// <param name="patientId"></param>
        /// <returns></returns>
        PatientProfileAllergyDTO LoadPatientAllergiesForProvider(int patientId);


        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>            
        PatientProfileDiagnosisDTO LoadPatientDiagnosisForProvider(int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>            
        PatientProfileImmuniztionDTO LoadPatientImmunizationForProvider(int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>           
        PatientProfileChronicMedicationDTO LoadPatientMedicationForProvider(int patientId);

        PatientProfileInsuranceDTO LoadPatientInsuranceForProvider(int patientId);

        /// <summary>
        /// Author Muhammad ISmail
        /// Date 28 March 2011
        /// Method Used To load patient info by Id
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        PatientDTO LoadPatientByPatientId(int PatientId);

        QuivusModel.User.Patient.PatientBasicInfoDTO LoadPatientBasicInfoByPatientId(int PatientId);

        QuivusModel.User.Patient.PatientUserInfo LoadPatientUserInfoByQuivusId(string QuivusId);

        int LoadClinicBranchIdByClinicBranchName(string ClicniBranchName);

        PatientSearchResultDTO LoadUsersByPracticeId(int clinicBranchID, int pageNo, int pageSize);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Create Date 8 Dec 2011
        /// This mthod will return list of clinic room by stationId
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        List<ClinicRoomDTO> LoadClinicRoomByStationId(int stationId);
    }
}
