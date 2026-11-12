using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;


namespace QuivusManager.Buisness.VisitBL.QuickInput
{
    public interface IQuickInputBL
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>     
        CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId, int managedByUserTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>     
        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId, int managedByUserTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SignVisit(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int clinicBranchId, int patientId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>      
        QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes);

        PatientMedicalHistoryResponse ManagePatientMedicalHistory(List<MedicalHistoryDTO> PateintMedicalHistoryList, int cbID, int patId);

        /// <summary>
        /// Author Mansoor
        /// Date 11 Mar 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId,int UserTypeId);

        PatientFamilyHistoryResponse ManagePatientFamilyHistory(List<FamilyHistoryDTO> PateintFamilyHistoryList,int UserTypeId,int ClinicBranchId, int patId);

        PatientVitalResponse ManagePatientVitals(List<VitalDTO> VitalList);

        PatientLabResponse ManagePatientLabs(List<LabDTO> LabList);


        /// <summary>
        /// Author Mansoor
        /// Date 21 Mar 2011
        /// get hpi details for auto complete suggestion box, by type id and clinic branch
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        HPIDetailAutoCompleteResponse LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId, int clinicBranchId, string searchCriteria);


        SocialHistoryAddictionResponse ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, int cbId, int PatId);

        SocialHistoryAddictionResponse DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject);

        SocialHistoryResponse ManagePatientSocialHistory(List<SocialHistoryDTO> SocialHistoryList, int cbId, int patId);

        SocialHistoryResponse DeleteSocialHistory(SocialHistoryDTO DeletedObject);

        PatientPhysicalResponse ManagePatientPhysical(List<PhysicalsDTO> PhysicalList);

        PatientROSResponse ManagePatientROS(List<ROSDTO> ROSList);


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// get Recommendation auto complete suggestion box, by type id and clinic branch
        /// </summary>        
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        LinksAutoCompleteResponse LoadRecommendationLinkAutoCompleteByClinicId(int clinicBranchId, string searchCriteria);


        PatientRecommendationResponse ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList);

        ServiceResponse.ServiceCustomResponse.ServiceResponse CoSignVisit(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate);

        AllergyResponse ManagePatientAllergy(AllergyDTO AllergyObj);

        ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication.MedicationPrescribedResponse ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj);

        PatientProcedureResponse ManagePatientProcedure(List<ProcedureDTO> InputList);

        PatientInHouseLabResponse ManagePatientInHouseLabs(List<InHouseLabDTO> InputList);

        PatientInHouseLabResponse ManagePatientInHouseLabResults(int PatientInHouseLabTestID, string TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt);

        PatientMedicationReponse ManagePatientMedication(MedicationDTO InputObj);

        ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication.PatientMedicationReponse DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt);

        PatientHPIResponse ManagePatientHPI(List<HPI_CCDTO> hpiList);

        ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp.PatientFollowUpResponse ManagePatientFollowUp(List<PatientFollowUpDTO> patientFollowUpList);

        PatientDiagnosisResponse ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int cbId, int patID);

        ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp.PatientFollowupInstructionResponse ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList);

        ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId, int UserTypeId, bool IsFinal);

        AllergyResponse ManagePatientAllergyList(List<AllergyDTO> AllergyList);

        bool SaveTempQI(QuickInputDTO quickInputDTO, string fileName);

        bool DeleteTempQI(string fileName);

        QuickInputResponse LoadTempQI(string fileName);

        bool IsTempQIExists(string fileName);

        PatientMedicationReponse ManagePatientMedicationList(List<MedicationDTO> InputList);

        bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist);

        bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList);

        bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject);

        bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject);

        void UpdateAllergiesToEprescription(IEnumerable<AllergyDTO> qResults, int patientID);
        bool ManageCustomICDCode(ICDCodeDTO customICDCodeDTO, int managedByUserIDForVisit, int clinicBranchID);
    }
}
