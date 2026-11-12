using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Quivus.Utility.Fault;
using Quivus.Model.QuickInput;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
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
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Dictation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using QuivusModel.QuickInput.ComprehensiveHistory.Social;


namespace VisitService.Service
{

    public partial interface IVisitService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, int UserId, int UserTypeId);


        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Patient Medical History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MedicalHistoryDTO> LoadPatientMedicalHistoryByVisit(int VisitId);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Family History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<FamilyHistoryDTO> LoadPatientFamilyHistoryByVisit(int VisitId);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Vital History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<VitalDTO> LoadPatientVitalByVisit(int VisitId);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabDTO> LoadPatientLabsByVisit(int VisitId);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load Patient Previous 10 Vitals History 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<RecentVitalDTO> LoadPatientPreviousVitals(int PatientId);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSDTO> LoadPatientROSByVisit(int VisitId);


        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social history Addiction By social History Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialHistoryAddictionDTO> LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId);

        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<HPI_CCDTO> LoadPatientHPIByVisitId(int VisitId);



         /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social history by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialHistoryDTO> LoadSocialHistoryByVisitId(int VisitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientRecommendationDTO> LoadPatientRecommendationByVisitId(int VisitId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientDiagnosisByVisitId(int visitId);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient physical History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalsDTO> LoadPatientPhysicalsByVisit(int VisitId);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Recommendation History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientRecommendationDTO> LoadPatientRecommendationByVisit(int VisitId);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Recommendation History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        QuickInputDTO LoadQuickInputByVisit(int VisitId);


        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow up by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientFollowUpDTO LoadFollowUpByVisitId(int visitId);

        /// <summary>
        /// Author Ismail
        /// Date 25 March 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<VisitDTO> LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId);

         /// <summary>
        /// Author Ismail
        /// Date 28 March 2011
        /// This Method Will be used to load visit stake holder by visit by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<VisistStakeHolderDTO> LoadVisitStakeHolderByVisitId(int VisitId, int UserTypeId);

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient medication prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MedicationPrescribedDTO> LoadPatientMedicationPrescribedByPatient(int PatientId);

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId);

        /// <summary>
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<VitalDTO> LoadPatientVitals(int PatientId, int ClinicBranchId);

        /// <summary>
        /// Author Ismail
        /// Date 05 April 2011
        /// This Method Will be used to load visit by visit by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        VisitDTO LoadVisitDataByVisitId(int VisitId);


          /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient presription by visit by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PrescriptionDTO> LoadPateintPrescriptionByVisitId(int VisitId);

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId);


        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MedicationDTO> LoadPatientMedicationByPatientId(int PatientId);

         /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

         /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

         /// <summary>
        /// Author Atta
        /// Date 20 April 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InHouseLabDTO> LoadPateintInHouseLabsByVisitId(int VisitId);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InHouseLabDTO> LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MedicalHistoryDTO> LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<FamilyHistoryDTO> LoadFamilyHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient vital by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<VitalDTO> LoadVitalsByPatientId(int PatientId, int ClinicbranchId, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<HPI_CCDTO> LoadHPIByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSDTO> LoadROSByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalsDTO> LoadPhysicalsByPatientId(int PatientId, int ClinicbranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisByPatientId(int PatientId);

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisByPatientId(int PatientId);

        /// <summary>
        /// Author Ismail
        /// Date 5 may 2011
        /// This Method Will be used to load patient procedure by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ProcedureDTO> LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient social history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialHistoryDTO> LoadPatientSocialHistoryByPatientId(int PatientId, int ClinicbranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientRecommendationDTO> LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowUpDTO> LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 10 May 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ModuleDictationDTO> LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ModuleNoteDTO> LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InstructionAgeTypeDTO> LoadFollowUpInstructionAgeTypes();

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InstructionDurationTypeDTO> LoadFollowUpInstructionDurationTypes();

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// Mansoor - 04-08-2011 - oslete now
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        //M [OperationContract]
        //M [FaultContract(typeof(QuivusFault))]
        List<ServiceTypeDTO> LoadFollowUpServiceTypes(int ClinicBranchID);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowupInstructionDTO> LoadFollowupInstructionDTOByVisitId(int VisitId);

        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowupInstructionDTO> LoadFollowupInstructionByPatientId(int PatientId, int cliniBranchID, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InstructionAgeValueTypeDTO> LoadFollowUpInstructionAgeValueTypes();

        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SocialhistoryVBDTO> LoadSocialHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected);



        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId);



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
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
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
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int patientId);


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<VitalDTO> LoadPatientVitalAll(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ROSDTO> LoadROSAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId);//  

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId);//

        //Alerady added
        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId);

        //Alerady added
        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient procedure by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId);//



        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module dictation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module notes by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId);

        #endregion

    }
}
