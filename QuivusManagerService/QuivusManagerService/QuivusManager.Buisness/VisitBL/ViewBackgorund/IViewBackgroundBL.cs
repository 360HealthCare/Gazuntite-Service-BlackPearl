using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.IntegratedServies.VisitService;


namespace QuivusManager.Buisness.VisitBL.ViewBackgorund
{
    public interface IViewBackgroundBL
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>     
        SoapTemplateResponse LoadSoapTemplateByClinicBranchId(int clinicBranchId);



        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>     
        PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, int UserId, int UserTypeId);





        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>    
        ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>     
        ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>    
        VisitNotesResponse LoadModuleNotesByVisitId(int visitId);



        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId, int clinicBranchIDList, List<int> stationIDs, bool isAllSelected);

        PatientMedicalHistoryResponse LoadPatientMedicalHistoryByVisit(int VisitId);

        PatientFamilyHistoryResponse LoadPatientFamilyHistoryByVisit(int VisitId);
        PatientVitalResponse LoadPatientVitalByVisit(int VisitId);

        PatientLabResponse LoadPatientLabsByVisit(int VisitId);

        PatientVitalResponse LoadPatientPreviousVitals(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by Visit 
        /// </summary>
        /// <returns></returns>
        PatientHPIResponse LoadPatientHPIByVisit(int visitId);


        SocialHistoryAddictionResponse LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId);

        SocialHistoryResponse LoadSocialHistoryByVisitId(int VisitId);


        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>  
        PatientRecommendationResponse LoadPatientRecommendationByVisit(int visitId);

        PatientPhysicalResponse LoadPatientPhysicalsByVisit(int VisitId);
        
        /// <summary>
        /// Author Mansoor
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>           
        PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int visitId);

       PatientROSResponse LoadPatientROSByVisit(int VisitId);

       QuickInputResponse LoadQuickInputByVisit(int VisitId);


       /// <summary>
       /// Author Mansoor
       /// Date 25 March 2011
       /// This Method Will be used to load Patient Follow UP by visit Id
       /// </summary>
       /// <returns></returns>
       /// <param name="VisitId"></param>
       /// <returns></returns>
       PatientFollowUpResponse LoadPatientFollowUpByVisitId(int visitId);

       VisistStakeHolderResponse LoadVisitStakeHolderByVisitId(int VisitId, int UserTypeId);



       ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication.MedicationPrescribedResponse LoadPatientMedicationPrescribedByPatient(int PatientId);

       ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy.AllergyResponse LoadPatientAllergyByPatientId(int PatientId,int ClinicbranchId);

       PatientVisitResponse LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId);

       PatientVitalResponse LoadPatientVitals(int PatientId, int ClinicBranchId);

       ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription.PatientPrescriptionResponse LoadPateintPrescriptionByVisitId(int VisitId);

       ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription.PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId);

       ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication.PatientMedicationReponse LoadPatientMedicationByPatientId(int PatientId);

       PatientDiagnosisResponse LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);
       PatientFollowupInstructionResponse LoadPatientFollowUpInstructionByVisitId(int visitId);

       ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription.PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicalBranchId, List<int> stationIDs, bool isAllSelected);

       FollowupLabsResponse LoadPatientLabTestsForFollowupByPatient(int PatientId);

       PatientInHouseLabResponse LoadPateintInHouseLabsByVisitId(int VisitId);

       PatientInHouseLabResponse LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientVitalResponse LoadVitalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientHPIResponse LoadHPIByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientROSResponse LoadROSByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientPhysicalResponse LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientDiagnosisResponse LoadPatientResolvedDiagnosisByPatientId(int PatientId);

       PatientDiagnosisResponse LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientDiagnosisResponse LoadPatientAcuteDiagnosisByPatientId(int PatientId);

       ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure.PatientProcedureResponse LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       QuickInputResponse ImportVisitByTemplate(int TemplateId, int ClinicBranchId);

       PatientMedicalHistoryResponse LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientFamilyHistoryResponse LoadFamilyHistoryByPatientId(int PatientId, int clinicBranchId, List<int> stationIDs, bool isAllSelected);

       SocialHistoryResponse LoadPatientSocialHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientRecommendationResponse LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientFollowUpResponse LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientLabResponse LoadPatientLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       ServiceResponse.ServiceCustomResponse.QuickInput.ModuleDictation.ModuleDictationResponse LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

       ServiceResponse.ServiceCustomResponse.QuickInput.ModuleNotes.ModuleNotesResponse LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

       PatientFollowupInstructionResponse LoadFollowupInstructionByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       SocialHistoryVBResponse LoadSocialHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

       PatientDiagnosisResponse LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId);





       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient allergy prescribed by PatientId by Id
       /// </summary>
       /// <returns></returns>               
       List<AllergyDTO> LoadPatientAllergyAllByPatientId(int PatientId);


       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient medical history by patient Id
       /// </summary>
       /// <returns></returns>       
       List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient family history by patient Id
       /// </summary>
       /// <returns></returns>       
       List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// </summary>
       /// <param name="InputList"></param>
       /// <returns></returns>       
       List<SocialhistoryVBDTO> LoadSocialHistoryAllByPatientId(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient medication by patient prescription Id
       /// </summary>
       /// <returns></returns>       
       List<MedicationDTO> LoadPatientMedicationAllByPatientId(int PatientId);


       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// get immunization list of patient        
       /// </summary>
       /// <param name="patientId"></param>
       /// <param name="providerUserId"></param>
       /// <param name="userTypeId"></param>
       /// <returns></returns>       
       List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int patientId);


       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load Patient visit by clinic branch Id
       /// </summary>
       /// <returns></returns>
       List<VitalDTO> LoadPatientVitalAll(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient hpi by patient Id
       /// </summary>
       /// <returns></returns>
       List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient ROS by patient Id
       /// </summary>
       /// <returns></returns>
       List<ROSDTO> LoadROSAllByPatientId(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load patient physicals by patient Id
       /// </summary>
       /// <returns></returns>
       List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId);

       /// <summary>
       /// Author Mansoor
       /// Date 14 Sep 2011
       /// This Method Will be used to load Patient module dictation by patrint Id
       /// </summary>
       /// <returns></returns>
       List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId);

       /// <summary>
       /// Author Muhammad Ismail 
       /// Date 20 May 2011
       /// This Method Will be used to load Patient module notes by patrint Id
       /// </summary>
       /// <returns></returns>
       List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId);


       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient in house labs by patient Id
       /// </summary>
       /// <returns></returns>       
       List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId);//  

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load Patient Labs by patrint Id
       /// </summary>
       /// <returns></returns>       
       List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId);//

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
       /// </summary>
       /// <returns></returns>       
       List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisAllByPatientId(int PatientId);//

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
       /// </summary>
       /// <returns></returns>       
       List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId);//

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
       /// </summary>
       /// <returns></returns>       
       List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisAllByPatientId(int PatientId);//

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient procedure by patient Id
       /// </summary>
       /// <returns></returns>       
       List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId);//

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient medication by patient diagnosis Id
       /// </summary>
       /// <returns></returns>       
       List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId);//

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient recommendation by patient Id
       /// </summary>
       /// <returns></returns>       
       List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId);//


       /// <summary>
       /// Author Mansoor
       /// Date 26 Sep 2011
       /// This Method Will be used to load patient FollowUp by patient Id
       /// </summary>
       /// <returns></returns>       
       List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId);

       List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId);


    }
}
