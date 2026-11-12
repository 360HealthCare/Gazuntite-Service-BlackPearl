using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleNotes;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleDictation;



namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]

        PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>    
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>      
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>             
        [OperationContract]
        //[FaultContract(typeof(QuivusFault)
        ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        VisitNotesResponse LoadModuleNotesByVisitId(int visitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [OperationContract]
        ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Patient Medical History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientMedicalHistoryResponse LoadPatientMedicalHistoryByVisit(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Family History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientFamilyHistoryResponse LoadPatientFamilyHistoryByVisit(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social history Addiction By social History Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SocialHistoryAddictionResponse LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social history by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SocialHistoryResponse LoadSocialHistoryByVisitId(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Vital History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientVitalResponse LoadPatientVitalByVisit(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientLabResponse LoadPatientLabsByVisit(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load Patient Previous 10 Vitals History 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientVitalResponse LoadPatientPreviousVitals(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by Visit 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientHPIResponse LoadPatientHPIByVisit(int visitId, UserSessionDTO userSession);




        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientROSResponse LoadPatientROSByVisit(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Patient physical History by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientPhysicalResponse LoadPatientPhysicalsByVisit(int VisitId, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>  
        [OperationContract]
        PatientRecommendationResponse LoadPatientRecommendationByVisit(int visitId, UserSessionDTO userSession);



        /// Author Ismail
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        //[OperationContract]
        //PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int visitId, UserSessionDTO userSession);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Quick Input by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        QuickInputResponse LoadQuickInputByVisit(int VisitId, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>   
        [OperationContract]
        PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int visitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow UP by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>   
        [OperationContract]
        PatientFollowUpResponse LoadPatientFollowUpByVisitId(int visitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 28 March 2011
        /// This Method Will be used to load visit stake holder by visit by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        VisistStakeHolderResponse LoadVisitStakeHolderByVisitId(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient medication prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MedicationPrescribedResponse LoadPatientMedicationPrescribedByPatient(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        AllergyResponse LoadPatientAllergyByPatientId(int PatientId, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientVisitResponse LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientVitalResponse LoadPatientVitals(int PatientId, int ClinicBranchId, UserSessionDTO userSession);



        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient presription by visit by Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientPrescriptionResponse LoadPateintPrescriptionByVisitId(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientMedicationReponse LoadPatientMedicationByPatientId(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientDiagnosisResponse LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientFollowupInstructionResponse LoadPatientFollowUpInstructionByVisitId(int visitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load presription medication by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicalBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load presription medication by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        FollowupLabsResponse LoadPatientLabTestsForFollowupByPatient(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by visit Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientInHouseLabResponse LoadPateintInHouseLabsByVisitId(int VisitId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientInHouseLabResponse LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);


        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient vital by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientVitalResponse LoadVitalsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientHPIResponse LoadHPIByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientROSResponse LoadROSByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientPhysicalResponse LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientDiagnosisResponse LoadPatientResolvedDiagnosisByPatientId(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Baber waqas
        /// Data 26 may 2011
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientAllDiagnosisResponse LoadPatientAllDiagnosisByPatientId(int PatientId, int ClinicalBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);



        /// <summary>
        /// Author Hassan Naqvi
        /// Data 1 Feb 2012
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="ClinicalBranchId"></param>
        /// <param name="userSession"></param>
        /// <param name="stationIDs"></param>
        /// <param name="isAllSelected"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientAllDiagnosisResponse LoadPatientAllChronicDiagnosisByPatientId(int PatientId, int ClinicalBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);



        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientDiagnosisResponse LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientDiagnosisResponse LoadPatientAcuteDiagnosisByPatientId(int PatientId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 5 may 2011
        /// This Method Will be used to load patient chronic diagnosis by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProcedureResponse LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 06 May 2011
        /// This Method Will be used to import visit By template
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        QuickInputResponse ImportVisitByTemplate(int TemplateId, int ClinicBranchId, UserSessionDTO userSession);

        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientMedicalHistoryResponse LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientFamilyHistoryResponse LoadFamilyHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient social history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        SocialHistoryResponse LoadPatientSocialHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientRecommendationResponse LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientFollowUpResponse LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 10 May 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientLabResponse LoadPatientLabsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        ModuleDictationResponse LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        ModuleNotesResponse LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientFollowupInstructionResponse LoadFollowupInstructionByPatientId(int PatientId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);


        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        SocialHistoryVBResponse LoadSocialHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientDiagnosisResponse LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId, UserSessionDTO userSession);
    }
}