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
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial interface IQuivusManagerService
    {
      
        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ServiceResponse SignVisit(int VisitId, UserSessionDTO userSession);

      
        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, UserSessionDTO userSession, int clinicBranchId, int patientId);

       

       

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes, UserSessionDTO userSession);

      


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        ///  
        [OperationContract]
        PatientMedicalHistoryResponse ManagePatientMedicalHistory(List<MedicalHistoryDTO> PateintMedicalHistoryList, UserSessionDTO userSession, int cbID, int patId);

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
        [OperationContract]
        ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 12 july 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        [OperationContract(Name = "ManageQuickInput")]
        ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId, bool IsFinal, UserSessionDTO userSession);
       

       

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientFamilyHistoryResponse ManagePatientFamilyHistory(List<FamilyHistoryDTO> PateintFamilyHistoryList, UserSessionDTO userSession, int cbId, int patID);

       


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PatientVitalResponse ManagePatientVitals(List<VitalDTO> VitalList, UserSessionDTO userSession);

       


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Labs
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientLabResponse ManagePatientLabs(List<LabDTO> LabList, UserSessionDTO userSession);


        /// <summary>
        /// Author Mansoor
        /// Date 21 Mar 2011
        /// get hpi details for auto complete suggestion box, by type id and clinic branch
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        HPIDetailAutoCompleteResponse LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId, int clinicBranchId, string searchCriteria, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/updateSocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        SocialHistoryAddictionResponse ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, UserSessionDTO userSession,int cbID, int patId);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// Delete SocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        SocialHistoryAddictionResponse DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/updateSocial History 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        SocialHistoryResponse ManagePatientSocialHistory(List<SocialHistoryDTO> SocialHistoryList, UserSessionDTO userSession, int cbId, int patID);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// Delete SocialHistory 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        SocialHistoryResponse DeleteSocialHistory(SocialHistoryDTO DeletedObject, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
          [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientPhysicalResponse ManagePatientPhysical(List<PhysicalsDTO> PhysicalList, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 21 Feb 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientROSResponse ManagePatientROS(List<ROSDTO> ROSList, UserSessionDTO userSession);

          /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// get Recommendation auto complete suggestion box, by type id and clinic branch
        /// </summary>        
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        [OperationContract]
        LinksAutoCompleteResponse LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient Recommendation
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        PatientRecommendationResponse ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        ServiceResponse CoSignVisit(int VisitId, int ProviderId, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        //  [OperationContract] For E-Prescription Use As Requested By Babar
        AllergyResponse ManagePatientAllergy(AllergyDTO AllergyObj, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient chrnoc medication
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        //   [OperationContract] For E-Prescription Use As Requested By Babar
    ///    [FaultContract(typeof(QuivusFault))]
        MedicationPrescribedResponse ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientProcedureResponse ManagePatientProcedure(List<ProcedureDTO> InputList, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientInHouseLabResponse ManagePatientInHouseLabs(List<InHouseLabDTO> InputList, UserSessionDTO userSession);


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs results
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientInHouseLabResponse ManagePatientInHouseLabResults(int PatientInHouseLabTestID, String TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientMedicationReponse ManagePatientMedication(MedicationDTO InputObj, UserSessionDTO userSession);

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// Delete patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientMedicationReponse DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt, UserSessionDTO userSession);

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
         [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        PatientHPIResponse ManagePatientHPI(List<HPI_CCDTO> hpiList, UserSessionDTO userSession);

         /// <summary>
         /// Author Mansoor 
         /// Date 22 Mar 2011
         /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         PatientFollowUpResponse ManagePatientFollowUp(List<PatientFollowUpDTO> patientFollowUpList, UserSessionDTO userSession);

         /// <summary>
         /// Author Mansoor 
         /// Date 22 Mar 2011
         /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         PatientDiagnosisResponse ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, UserSessionDTO userSession, int cbID, int patId);

         /// <summary>
         /// Author Ismail 
         /// Date 22 May 2011
         /// This Method Will be used to Insert/Update/Delete Patient follow up instructions
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         PatientFollowupInstructionResponse ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList, UserSessionDTO userSession);
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         bool SaveTempQI(QuickInputDTO quickInputDTO, string fileName, UserSessionDTO userSession);

         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         bool DeleteTempQI(string fileName, UserSessionDTO userSession);

         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         QuickInputResponse LoadTempQI(string fileName, UserSessionDTO userSession);

         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         bool IsTempQIExists(string fileName, UserSessionDTO userSession);

         /// <summary>
         /// Author Muhammad Ismail 
         /// Date 07 October 2011
         /// This Method Will be used to Delete Patient Family History
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject, UserSessionDTO userSession);

         /// <summary>
         /// Author Muhammad Ismail 
         /// Date 07 October 2011
         /// This Method Will be used to Delete Patient medical History
         /// PatientMedicalHistory_Delete
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject, UserSessionDTO userSession);

         /// <summary>
         /// Author Tamjeed-ul-Hassan 
         /// Date 08 December 2011
         /// This Method Will be used to generate Soap-Note
         /// PatientMedicalHistory_Delete
         /// </summary>
         /// <returns></returns>
         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         string GenerateSoapNoteByVisitID(int VisitID, UserSessionDTO userSession, int clinicBranchId, List<int> stationIds, bool isAllSelected);

         [OperationContract]
         [FaultContract(typeof(QuivusFault))]
         string GenerateTempSoapNote(string fileName);
    }
}