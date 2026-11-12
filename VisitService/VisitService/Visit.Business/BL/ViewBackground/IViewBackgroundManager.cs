using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.ROS;

using Quivus.Model.QuickInput.ComprehensiveHistory.Social;

using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.MainQuickInput;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.Visit;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Procedure;
using QuivusModel.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.Dictation;


namespace Visit.Business.BL.ViewBackground
{
    public interface IViewBackgroundManager
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Date 11 Mrch 2011
        /// Method WIll be used to load patient Medical History By Visit Id
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        List<MedicalHistoryDTO> LoadPatientMedicalHistoryByVisit(int VisitId, bool IsTemplateVisit);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId, bool IsTemplateVisit);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId, bool IsTemplateVisit);

        List<FamilyHistoryDTO> LoadPatientFamilyHistoryByVisit(int VisitId, bool IsTemplateVisit);


        List<VitalDTO> LoadPatientVitalByVisit(int VisitId, bool IsTemplateVisit);

        List<LabDTO> LoadPatientLabsByVisit(int VisitId, bool IsTemplateVisit);

        List<RecentVitalDTO> LoadPatientPreviousVitals(int PatientId);

        List<ROSDTO> LoadPatientROSByVisit(int VisitId, bool IsTemplateVisit);


        List<SocialHistoryAddictionDTO> LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId, bool IsTemplateVisit);


        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by visit Id
        /// </summary>
        /// <returns></returns>  
        List<HPI_CCDTO> LoadPatientHPIByVisitId(int VisitId, bool IsTemplateVisit);



        List<SocialHistoryDTO> LoadPatientSocialHistoryByVisitId(int VisitId, bool IsTemplateVisit);

        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>  
        List<PatientRecommendationDTO> LoadPatientRecommendationByVisitId(int VisitId);


        List<PhysicalsDTO> LoadPatientPhysicalsByVisit(int VisitId, bool IsTemplateVisit);


        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>   
        List<PatientDiagnosisDTO> LoadPatientDiagnosisByVisitId(int visitId, bool IsTemplateVisit);

        List<PatientRecommendationDTO> LoadPatientRecommendationByVisit(int VisitId, bool IsTemplateVisit);

        QuickInputDTO LoadQuickInputByVisit(int VisitId,bool IsTemplateVisit);

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow up by visit Id
        /// </summary>
        /// <returns></returns> 
        PatientFollowUpDTO LoadFollowUpByVisitId(int visitId, bool IsTemplateVisit);

        List<VisitDTO> LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId);

        List<VisistStakeHolderDTO> LoadVisitStakeHolderByVisitId(int VisitId, int UserTypeId);

        List<MedicationPrescribedDTO> LoadPatientMedicationPrescribedByPatient(int PatientId);

        List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId);

        List<VitalDTO> LoadPatientVitals(int PatientId, int ClinicBranchId);

        VisitDTO LoadVisitDataByVisitId(int VisitId, bool IsTemplateVisit);

        List<Quivus.Model.QuickInput.Prescription.PrescriptionDTO> LoadPateintPrescriptionByVisitId(int VisitId);

        List<Quivus.Model.QuickInput.Prescription.PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId);

        List<Quivus.Model.QuickInput.Medication.MedicationDTO> LoadPatientMedicationByPatientId(int PatientPrescriptionId);

        List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId);


        List<PatientFollowupInstructionDTO> LoadFollowupInstructionDTOByVisitId(int VisitId, bool IsTemplateVisit);

        List<ProcedureDTO> LoadProcedureDTOListByVisitId(int VisitId, bool IsTemplateVisit);

        List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientId(int PatienId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId);

        List<InHouseLabDTO> LoadPateintInHouseLabsByVisitId(int VisitId, bool IsTemplateVisit);

        List<InHouseLabDTO> LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<MedicalHistoryDTO> LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<FamilyHistoryDTO> LoadFamilyHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<VitalDTO> LoadVitalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<HPI_CCDTO> LoadHPIByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<ROSDTO> LoadROSByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<PhysicalsDTO> LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisByPatientId(int PatientId);

        List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisByPatientId(int PatientId);

        List<ProcedureDTO> LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        QuickInputDTO ImportVisitByTemplate(int TemplateId, int ClinicBranchId);

        List<SocialHistoryDTO> LoadPatientSocialHistoryByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<PatientRecommendationDTO> LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        List<Quivus.Model.QuickInput.FollowUp.PatientFollowUpDTO> LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<Quivus.Model.QuickInput.Dictation.ModuleDictationDTO> LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<ModuleNoteDTO> LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<Quivus.Model.QuickInput.FollowUp.PatientFollowupInstructionDTO> LoadFollowupInstructionByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        List<SocialhistoryVBDTO> LoadSocialHistoryByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);



        #region WEB PORTAL

        //Alerady added
        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId);


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

        //Alerady added
        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<MedicationDTO> LoadPatientMedicationByPatientId(int PatientId);


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
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>        
        List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>        
        List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>        
        List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>        
        List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId);

        List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId);

        #endregion
    }
}
