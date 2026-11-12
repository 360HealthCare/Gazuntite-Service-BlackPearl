using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput.MainQuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.TypeDTOs;
using Quivus.Model.User.Patient;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Diagnosis;

namespace Visit.Business.BL.QuickInput
{
    public interface IQuickInputManager
    {
        int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, int cbID, int patId);

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
        bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId);

        bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId, bool IsFinal);

        int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject,int UserTypeId,int ClinicBranchId, int patId);

        int ManagePatientVitals(VitalDTO VitalObject);

        int ManagePatientLabs(Quivus.Model.QuickInput.Labs.LabDTO LabObject);

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int cbId, int patientId);



        bool ManagePatientROS(List<ROSDTO> ROSList);

        int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, int cbID, int patID);

        bool DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject);

        int ManagePatientSocialHistory(SocialHistoryDTO SocialHistoryObject, int cbID, int patId);

        bool DeleteSocialHistory(SocialHistoryDTO DeletedObject);

        bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList);

        bool ManagePatientRecommendation(List<Quivus.Model.QuickInput.Recommendation.PatientRecommendationDTO> RecommendationList);



        bool ManagePatientAllergy(AllergyDTO AllergyObj);

        bool ManagePatientAllergies(List<AllergyDTO> Allergylist);

        bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist);

        bool ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj);

        bool ManagePatientProcedure(List<Quivus.Model.QuickInput.Procedure.ProcedureDTO> InputList);

        bool ManagePatientInHouseLabs(List<Quivus.Model.QuickInput.Labs.InHouseLabDTO> InputList);

        bool ManagePatientInHouseLabResults(int PatientInHouseLabTestID, string TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt);

        int ManagePatientMedication(MedicationDTO InputObj);

        bool DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt);

        bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchID, DateTime ActionDate);

        bool ManagePatientHPI(List<HPI_CCDTO> hpiList);

        bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int cbID, int patID);

        bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList);

        bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp);

        bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList);

        bool ManagePatientMedication(List<MedicationDTO> MedicationList);

        bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject);

        bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject);
        bool ManageCustomICDCode(ICDCodeDTO icdCodeDTO, int managedByUserIDForVisit, int clinicBranchID);// added new interface methode
    }
}
