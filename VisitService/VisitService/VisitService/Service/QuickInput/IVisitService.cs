using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Utility.Fault;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;
using Quivus.Model.TypeDTOs;
using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.MainQuickInput;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.User.Patient;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.Prescription;

namespace VisitService.Service
{

    public partial interface IVisitService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int cbID, int patId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int CreateVisit(VisitDTO visitDTO, int managedByUserId);

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool SaveVisit(VisitDTO visitDTO, int managedByUserId);



        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// List all the encounter types
        /// </summary>
        /// <returns></returns>        
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<EncounterTypeDTO> LoadEncounterTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Medical History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<MedicalHistoryTypeDTO> LoadMedicalHistoryTypes();

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject,int cbID, int patId);


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
        [FaultContract(typeof(QuivusFault))]
        //[TransactionFlow (TransactionFlowOption.Allowed)]
        bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId);

         /// <summary>
        /// Author Ismail
        /// Date 12 july 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// This is overloaded method it save PMH,Physical & Assessment One By One In Last call front end make IsFinal True to
        /// Indicate that this is last call & on last call we will update visit status & calculate visit score
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
         [OperationContract (Name="ManageQuickInput")]
        [FaultContract(typeof(QuivusFault))]
        bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId, bool IsFinal);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Family History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<FamilyHistoryTypeDTO> LoadFamilyHistoryTypes();


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, int UserTypeId, int ClinicBranchId, int patID);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient Family History
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient medical History
        /// PatientMedicalHistory_Delete
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject);

        
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientVitals(VitalDTO VitalObject);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientLabs(LabDTO LabObject);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientROS(List<ROSDTO> ROSList);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/updateSocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction,int cbID, int patId);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// Delete SocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/updateSocial History 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientSocialHistory(SocialHistoryDTO SocialHistoryObject, int cbID, int patId);

        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// Delete SocialHistory 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool DeleteSocialHistory(SocialHistoryDTO DeletedObject);

        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="PhysicalList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList);

        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient Recommendation
        /// </summary>
        /// <param name="RecommendationList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList);


        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientAllergy(AllergyDTO AllergyObj);

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies List
        /// </summary>
        /// <param name="Allergylist"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientAllergies(List<AllergyDTO> Allergylist);

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient chrnoc medication
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj);

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
        bool SignVisit(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate);

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
        bool CoSignVisit(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate);


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientProcedure(List<ProcedureDTO> InputList);

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientInHouseLabs(List<InHouseLabDTO> InputList);

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs results
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientInHouseLabResults(int PatientInHouseLabTestID, String TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt);

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        int ManagePatientMedication(MedicationDTO InputObj);

         /// <summary>
        /// Author Ismail
        /// Date 21 July 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientMedicationList(List<MedicationDTO> MedicationList);
       
        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// Delete patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt);




        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientHPI(List<HPI_CCDTO> hpiList);

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int cbId, int patID);

        /// <summary>
        /// Author Mansoor 
        /// Date 20 July 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList);
        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp);

        /// <summary>
        /// Author Baber Waqas at 1/24/2012
        /// </summary>
        /// <param name="customICDCodeDTO"></param>
        /// <param name="managedByUserIDForVisit"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof (QuivusFault))]
        bool ManageCustomICDCode(ICDCodeDTO customICDCodeDTO, int managedByUserIDForVisit, int clinicBranchID);
        /// <summary>
        /// Author Ismail 
        /// Date 22 May 2011
        /// This Method Will be used to Insert/Update/Delete Patient follow up instructions
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList);

        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist);
    }
}
