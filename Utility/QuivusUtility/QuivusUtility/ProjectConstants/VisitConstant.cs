using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility
{
    /// <summary> 
    /// Author Mansoor
    /// Date 17 FEb 2011
    /// This constant class is intended for only Visit specific contants
    /// </summary>
    public class VisitConstant
    {
        #region Stored Procs

        public const string ENCOUNTER_TYPE_LOAD_ALL_SP = "[type.VisitEncounterType_LoadAll]";
        public const string SOAP_TEMPALTE_TYPE_LOAD_BY_CLINIC_ID_SP = "SoapTemplate_LoadByClinicBranch";
        public const string VISIT_INSERT_SP = "Visit_Insert";
        public const string ZERO_VISIT_INSERT_SP = "ZeroVisit_Insert";
        public const string CHECK_ZERO_VISIT_INSERT_SP = "Check_ZeroVisit_Exist";
        
        
        public const string VISIT_MARK_SIGNED_SP = "Visit_MarkAsSigned";
        public const string VISIT_LOAD_BY_PATIENT_AND_CLINIC_SP = "Visit_LoadForViewBackgroundListByClinicBranch";
        public const string VISIT_UPDATE_SP = "Visit_Update";

        public const string IMMUNIZATION_INSERT_SP = "PatientImmunization_Insert";
        public const string IMMUNIZATION_UPDATE_SP = "PatientImmunization_Update";
        public const string IMMUNIZATION_LOAD_ALL_SP = "[type.ImmunizationType_LoadAll]";

        public const string LOTNUMBER_LOAD_BY_CLINIC_ID_SP = "[Immunization.LotNumber_LoadAutoCompleteByClinicBranch]";
        public const string DOSE_LOAD_BY_CLINIC_ID_SP = "[Immunization.Dose_LoadAutoCompleteByClinicBranch]";
        public const string REFUSAL_REASON_LOAD_BY_CLINIC_ID_SP = "[Immunization.RefusalReason_LoadAutoCompleteByClinicBranch]";
        public const string SITE_LOAD_BY_CLINIC_ID_SP = "[Immunization.Site_LoadAutoCompleteByClinicBranch]";
        public const string SERIES_LOAD_BY_CLINIC_ID_SP = "[Immunization.Series_LoadAutoCompleteByClinicBranch]";
        public const string MANUFACTURER_LOAD_BY_CLINIC_ID_SP = "[Immunization.Manufacturer_LoadAutoCompleteByClinicBranch]";
        public const string ROUT_LOAD_BY_CLINIC_ID_SP = "[Immunization.Route_LoadAutoCompleteByClinicBranch]";


        public const string IMMUNIZATION_LOAD_BY_VISIT_ID_SP = "PatientImmunization_LoadByVisit";
        public const string IMMUNIZATION_LOAD_BY_PATIENT_ID_SP = "PatientImmunization_LoadByPatientANDClinicBranch";

        public const string MODULE_NOTES_LOAD_BY_VISIT_ID_SP = "ModuleNote_LoadByVisit";
        public const string MODULE_NOTES_MANAGE_SP = "ModuleNotes_TVPAddUpdateDelete";

        public const string MODULE_DICTATION_MANAGE_SP = "ModuleDictation_TVPAddUpdateDelete";

        public const string IMMUNIZATION_MANAGE_SP = "PatientImmunization_TVPAddUpdateDelete";


        #endregion

        public static string MEDICAL_HISTORY_TYPE_LOAD_SP = "[type.MedicalHistoryType_LoadAll]";
       
        public static string PATIENT_MEDICAL_HISTORY_LOAD_BY_PATIENT_ID_SP = "PatientMedicalHistory_LoadByVisit";
        public static string FAMILY_HISTORY_TYPE_LOAD_SP = "[type.FamilyHistoryType_LoadAll]";
        public static string DISEASE_TYPE_LOAD_SP = "[type.DiseaseType_LoadAll]";

        


        public static string VITAL_TYPE_LOAD_SP = "[type.VitalType_LoadAll]";
        public static string PATIENT_FAMILY_HISTORY_LOAD_BY_PATIENT_ID_SP = "[PatientFamilyHistory_LoadByVisit]";

        public static string PATIENT_MEDICAL_INSERT_SP = "PatientMedicalHistory_Insert";
        public static string PATIENT_MEDICAL_UPDATE_SP = "PatientMedicalHistory_Update";
        public static string PATIENT_FAMILY_INSERT_SP = "PatientFamilyHistory_Insert";
        public static string PATIENT_FAMILY_UPDATE_SP = "PatientFamilyHistory_Update";

        public static string VITAL_INSERT_SP = "PatientVital_Insert";
        public static string VITAL_UPDATE_SP = "PatientVital_Update";
        
    

        public static string LABS_INSERT_SP = "PatientLab_Insert";
        public static string LABS_UPDATE_SP = "PatientLab_Update";
       

        public static string PATIENT_LABS_LOAD_BY_VISIT_ID_SP = "PatientLab_LoadByVisit";

        public static string LAB_NATURE_TYPE_LOAD_SP = "[type.LabTestNatureType_LoadAll]";
        public static string LAB_TEST_ORDER_METHOD_TYPE_LOAD_SP = ".[type.LabTestOrderMethodType_LoadAll]";
        public static string LAB_TYPE_LOAD_SP="[type.LabType_LoadAll]";
        public static string LAB_TEST_TYPE_LOAD_SP="[type.LabTestType_LoadAll]";
        public static string LAB_TEST_SPECIMENT_TYPE_LOAD_SP="[type.LabTestSpecimenType_LoadAll]";
        public static string LAB_TEST_PRIORITY_TYPE_LOAD_SP = "[type.LabTestPriorityType_LoadAll]";
        public static string ICD_CODE_BY_NAME_LOAD_SP = "[type.ICDCode_LoadByICDCodeNameAutoComplete]";
        public static string ICDCodeCustomSpLoadByName= "[type.ICDCode_LoadByICDCodeNameAutoCompleteCustom]";
        
        public static string PATIENT_VITAL_LOAD_BY_VISIT_ID_SP = "PatientVital_LoadByVisit";
        public static string PATIENT_VITAL_PREVIOUS_LOAD_BY_VISIT_ID_SP = "[PatientVital_LoadRecent10]";
        public static string ROS_MAIN_TYPE_LOAD_SP = "[type.ROSMainCategoryType_LoadAll]";
        public static string ROS_SUB_TYPE_LOAD_SP = "[type.ROSSubCategoryType_LoadAll]";
        public static string PATIENT_ROS_MANAGE_SP = "[PatientROS_InsertUpdateDelete]";
        public static string HPI_DATA_TYPE_LOAD_SP = "[type.HPIDataControlType_LoadAll]";


        public const string HPI_TYPE_LOAD_SP = "[type.HPIDataControlType_LoadAll]";
        public const string HPI_DETAILS_LOAD_BY_TYPE_ID_AND_CLINIC_ID_SP = "PatientHPIAutoCompleteData_LoadByClinicBranch";
        public const string FOLLOW_UP_AUTO_COMPLETE_DATA_LOAD_CLINIC_ID_SP = "PatientFollowupAutoCompleteData_LoadByClinicBranch";
        public const string FOLLOWUP_DATACONTROL_TYPE = "[type.FollowupDataControlType_LoadAll]";

        public static string SOCIAL_ADDICTION_TYPE_LOAD_SP = "[type.SocialHistoryAddictionType_LoadAll]";
        public static string SOCIAL_MARITAL_TYPE_LOAD_SP = "[type.MaritalStatusType_LoadAll]";

        public const string HPI_MANAGE_SP = "PatientHPI_InsertUpdateDelete";
        public const string PATIENT_HPI_LOAD_BY_VISIT_ID_SP = "PatientHPI_LoadByVisit";

        public static string PATIENT_SOCIAL_HISTORY_ADDICTION_LOAD_SP = "[SocialHistoryAddiction_LoadBySocialHistoryID]";
        public static string SOCIAL_ADICTION_UPDATE_SP = "[SocialHistoryAddiction_Update]";
        public static string SOCIAL_ADICTION_INSERT_SP = "[SocialHistoryAddiction_Insert]";
        public static string SOCIAL_ADICTION_DELETED_SP = "[SocialHistoryAddiction_MarkAsDeleted]";
        public static string PATIENT_SOCIAL_INSERT_SP = "[PatientSocialHistory_Insert]";
        public static string PATIENT_SOCIAL_UPDATE_SP="[PatientSocialHistory_Update]";
        public static string PATIENT_SOCIAL_DELETED_SP = "[PatientSocialHistory_MarkAsDeleted]";
        public static string PATIENT_SOCIAL_HISTORY_LOAD_SP = "[SocialHistory_LoadByVisit]";
        public static string SOCIAL_USAGE_AMOUNT_TYPE_LOAD_SP = "[type.SocialHistoryUsageAmount_LoadAll]";
        public static string SOCIAL_MEDIUM_TYPE_LOAD_SP = "[type.SocialHistoryMedium_LoadAll]";
        public static string PATIENT_PHYSICAL_MANAGE_SP = "[PatientPhysical_InsertUpdateDelete]";


        public const string PLAN_TYPES_LOAD_SP = "[type.RecommendationPlanType_LoadAll]"; // M not created yet
        public const string METHOD_TYPES_LOAD_SP = "[type.RecommendationMethodType_LoadAll]"; // M not created yet
        public const string DURATION_TYPES_LOAD_SP = "[type.RecommendationDurationType_LoadAll]"; // M not created yet
        public const string LINKS_LOAD_SP = "[type.RecommendationLinksType_LoadAll]"; // M not created yet

        public const string PATIENT_RECOMMENDATION_LOAD_BY_VISIT_SP = "PatientRecommendation_LoadByVisit"; // M not created yet

        public static string PHYSICAL_SUB_TYPE_LOAD_SP = "[type.PhysicalSubCategoryType_LoadAll]";
        public static string PATIENT_ROS_PREVIOUS_LOAD_BY_VISIT_ID_SP = "[PatientROS_LoadByVisit]";
        public static string PATIENT_PHYSICAL_PREVIOUS_LOAD_BY_VISIT_ID_SP = "[PatientPhysical_LoadByVisit]";
        public static string LIONICLAB_TEST_TYPE_LOAD_SP = "[type.LIONICLabTestType_LoadBySearchString]";

        public const string PATIENT_DIAGNOSIS_LOAD_BY_VISIT_SP = "PatientDiagnosis_LoadByVisit";
        public const string PATIENT_DIAGNOSIS_MANAGE_SP = "PatientDiagnosis_InsertUpdateDelete";
        public static string PHYSICAL_MAIN_TYPE_LOAD_SP = "[type.PhysicalMainCategoryType_LoadAll]";

        public const string FOLLOWUP_METHOD_TYPE_LOAD_SP = "[type.FollowUpMethodType_LoadAll]";
        public const string PATIENT_FOLLOWUP_INSERT_SP = "PatientFollowUp_Insert";
        public const string PATIENT_FOLLOWUP_UPDATE_SP = "PatientFollowUp_Update";
        public const string PATIENT_FOLLOWUP_LOAD_BY_VISIT_SP = "PatientFollowUp_LoadByVisit";
        public static string PATIENT_VISIT_LOAD_BY_CLINICBRANCH_SP = "[bck.Visit_LoadByPatient]";
        public static string PATIENT_ALLERGY_INSERT_SP = "[PatientAllergy_Insert]";
        public static string PATIENT_ALLERGY_UPDATE_SP;
        public static string PATIENT_CHRONIC_MEDICATION_INSERT_SP = "[PatientMedicationPrescribed_Insert]";
        public static string PATIENT_CHRONIC_MEDICATION_UPDATE_SP;
        public static string VISIT_MARK_COSIGNED_SP = "[VisitCoSign_Insert]";
        public static string PATIENT_RECOMMENDATION_MANAGE_SP = "PatientRecommendation_InsertUpdateDelete";
        public static string VISIT_STAKE_HOLDER_LOAD_BY_VISIT_SP = "[VisitStakeHolders_LoadByVisit]";
        //public static string MEDICATION_PRESCRIBED_LOAD_BY_VISIT_SP ="PrescriptionMedication_LoadByPatientPrescriptionID";// TODO 
        public static string MEDICATION_PRESCRIBED_LOAD_BY_PATIENT_SP  = "[PatientMedicationPrescribed_LoadByPatient]";
        public static string ALLERGY_LOAD_BY_PATIENT_ID_SP = "[PatientAllergy_LoadByPatient]";
        public static string LOAD_PATIENT_VITALS_SP = "[bck.PatientVital_LoadByPatient]";
        public static string VISIT_MARK_DATA_EXIST_SP = "[Visit_SetAsValidDataExists]";
        public static string VISIT_DATA_LOAD_BY_VISIT_SP = "Visit_LoadByID";
        public static string LOAD_ZEROVISIT_BY_PATIENT_SP = "Load_ZeroVisit_ByPatientID";
        
        public static string IN_HOUSE_LAB_TYPE_LOAD_SP = "[InHouseLabTestType_LoadByClinicBranch]";
        public static string CPT_MEDICAL_TYPE_LOAD_SP = "[type.CPTMedicalType_LoadByForAutoComplete]";
        public static string PATIENT_PRESCRIPTION_LOAD_BY_VISIT_SP = "[PatientPrescription_LoadByVisit]";
        public static string PATIENT_PROCEDURE_MANAGE_SP = "[PatientProcedure_TVPAddUpdateDelete]";
        public static string PATIENT_IN_HOUSE_LABS_MANAGE_SP = "[PatientInHouseLabTest_TVPAddUpdateDelete]";
        public static string PATIENT_INHOUSELAB_RESULT_SP = "[PatientInHouseLabTest_AddUpdateResult]";
        public static string MEDICATION_INSERT_SP = "[PatientMedication_Insert]";
        public static string MEDICATION_UPDATE_SP = "[PatientMedication_Update]";
        public static string PATIENT_MEDICATION_LOAD_BY_PRESCRIPTION_ID_SP = "[PatientMedication_LoadByPatientID]";
        public static string DELETE_PATIENT_MEDICTION_SP = "[PatientMedication_MarkAsDeleted]";
        public static string PATIENT_DIAGNOSIS_LOAD_BY_PATIENT_ID_SP = "[PatientDiagnosis_LoadByPatientIDANDClinicBranchID]";
        public static string FOLLOWUP_SERVICE_TYPE_LOAD_SP = "[type.FollowupServiceType_LoadAll]";

        public static string FOLLOWUP_INSTRUCTION_AGE_TYPE_LOAD_SP = "[type.FollowupInstructionAgeType_LoadAll]";
        public static string FOLLOWUP_INSTRUCTION_DURATION_TYPE_LOAD_SP = "[type.FollowupInstructionDurationType_LoadAll]";
        public static string PATIENT_FOLLOWUP_INSTRUCTION_LOAD_BY_VISIT_ID_SP = "[PatientFollowupInstruction_LoadByVisit]";
        public static string PATIENT_FOLLOWUP_INSTRUCTION_MANAGE_SP = "[PatientFollowupInstruction_TVPInsertUpdateDelete]";

        public static string PRESCRIPTION_MEDICATION_LOADBY_PATIENTPRESCRIPTIONID_SP = "[PrescriptionMedication_LoadByPatientPrescriptionID]";
        public static string PRESCRIPTION_MEDICATION_LOADBY_PATIENTID_SP = "[dbo].[PrescriptionMedication_LoadByPatientIDANDClinicBranchID]";
        public static string FOLLOWUP_INSTRUCTION_AGE_VALUE_TYPE_LOAD_SP = "[type.FollowupInstructionAgeValueType_LoadAll]";

        public static string PATIENT_PROCEDURE_LOAD_SP = "[PatientProcedure_LoadByVisit]";
        public static string FOLLOWUP_LABS_LOAD_BY_PATIENT_SP = "[PatientLabForFollowup_LoadByPatient]";
        public static string LOAD_INHOUSE_LABS_BY_VISIT_ID_SP = "PatientInHouseLabTest_LoadByVisitID";
        public static string LOAD_INHOUSE_LABS_BY_PATIENT_ID_SP = "[dbo].[PatientInHouseLabTest_LoadByPatientIDANDClinicBranchID]";
        public static string LOAD_MEDICAL_HISTORY_BY_PATIENT_ID_SP = "[dbo].[bck.PatientMedicalHistory_LoadByPatientIDANDClinicBranchID]";
        public static string LOAD_FAMILY_HISTORY_BY_PATIENT_ID_SP = "[dbo].[bck.PatientFamilyHistory_LoadByPatientIDANDClinicBranchID_new]";
        public static string LOAD_VITAL_BY_PATIENT_ID_SP = "[dbo].[PatientVital_LoadByPatientIDANDClinicBranchID]";
        public static string LOAD_HPI_BY_PATIENT_ID_SP = "[dbo].[PatientHPI_LoadByPatientIDANDClinicBranchID]";
        public static string LOAD_ROS_BY_PATIENT_ID_SP = "PatientROS_LoadByPatientIDANDClinicBranchID";
        public static string LOAD_PHYSICAL_BY_PATIENT_ID_SP = "[dbo].[PatientPhysical_LoadByPatientIDANDClinicBranchID]";
        public static string PATIENT_DIAGNOSIS_RESOLVED_LOAD_BY_PATIENT_ID_SP = "PatientDiagnosisResolved_LoadByPatientID";
        public static string PATIENT_DIAGNOSIS_CHRONIC_LOAD_BY_PATIENT_ID_SP = "[dbo].[PatientDiagnosisChronic_LoadByPatientIDANDClinicBranchID]";
        public static string PATIENT_DIAGNOSIS_ACUTE_LOAD_BY_PATIENT_ID_SP = "PatientDiagnosisAcute_LoadByPatientID";
        public static string MODULE_DICTATION_LOAD_BY_VISIT_ID_SP = "ModuleDictation_LoadByVisit";
        public static string MODULE_TYPE_LOAD_SP = "[type.ModuleType_LoadAll]";

        public static string PATIENT_PROCEDURE_LOAD_BY_PATIENT_ID_SP = "[dbo].[PatientProcedure_LoadByPatientIDANDClinicBranchID]";
        public static string VISIT_CALCULATE_SCORE_SP = "Visit_CalculateScoresByID";

        public static string ACCUTE_VISIT_LOAD_BY_CLINIC_BRANCH_ID_SP = "GetVisitID_AccuteVisit";
        public static string CHRONIC_DISEASE_VISIT_LOAD_BY_CLINIC_BRANCH_ID_SP = "GetVisitID_ChronicDiseaseVisit";
        public static string COMPREHENSIVE_VISIT_LOAD_BY_CLINIC_BRANCH_ID_SP = "GetVisitID_ComprehensiveVisit";
        public static string PATIENT_SOCIAL_HISTORY_LOAD_BY_PATIENT_ID_SP = "[dbo].[SocialHistory_LoadByPatientIDANDClinicBranchID]";
        public static string PATIENT_LABS_LOAD_BY_PATIENT_ID_SP = "PatientLab_LoadByPatientANDClinicBranch";
        public static string PATIENT_RECOMMENDATION_LOAD_BY_PATIENT_ID_SP = "[dbo].[PatientRecommendation_LoadByPatientIDANDClinicBranchID]";
        public static string PATIENT_FOLLOW_UP_LOAD_BY_PATIENT_ID_SP = "PatientFollowUp_LoadByPatientIDANDClinicBranchID";
        public static string PATIENT_MODULE_DICTATION_LOAD_BY_PATIENT_ID_SP = "ModuleDictation_LoadByPatientANDClinicBranch";
        public static string PATIENT_MODULE_NOTES_LOAD_BY_PATIENT_ID_SP = "ModuleNote_LoadByPatientANDClinicBranch";
        public static string PATIENT_FOLLOW_UP_INSTRUCTION_LOAD_BY_PATIENT_ID_SP = "PatientFollowupInstruction_LoadByPatientANDClinicBranchID";
        public static string SOCIAL_HISTORY_LOAD_BY_PATIENT_ID_SP = "[dbo].[PatientSocialHistoryID_LoadByPatientIDANDClinicBranchID]";
        public static string ROS_SUB_LEVEL_CATEGORY_LOAD_SP = "[type.ROSSubLevel3CategoryType_LoadAll]";
        public static string PHYSICAL_CATEGORY_LOAD_SP = "[type.PhysicalCategoryType_LoadAll]";
        public static string PHYSICAL_CATEGORY_LOAD_BY_GENDER_SP = "[type.PhysicalCategoryType_LoadAllByGender]";
        public static string ROS_SUB_LEVEL_CATEGORY_LOAD_BY_GENDER_SP = "[type.ROSSubLevel3CategoryType_LoadAllByGender]";
        public static string PATIENT_ALLERGIES_MANAGE_SP = "[PatientAllergy_TVPAddUpdateDelete]";
        public static string PATIENT_PRESCRIPTION_MEDICATION_MANAGE_SP = "[PrescriptionMedication_TVPAddUpdateDelete]";
        public static string PHYSICAL_LEVEL_THREE_CATEGORY_LOAD_BY_GENDER_SP = "[type.PhysicalSubLevel3CategoryType_LoadAll]";
        public static string PATIENT_DIAGNOSIS_BY_RCOPIA_MANAGE_SP = "[PatientDiagnosis_TVPInsertUpdateDelete_ByRCOPIAID]";

        public static string VISIT_QI_TEMP_FILE_DIRECTORY = @"C:\TempFiles";
        public static string DIRECTORY_SAPERATOR = @"\";

        public static string PATIENT_MEDICATION_MANAGE_SP = "[PatientMedication_TVPAddUpdateDelete]";



        public static string PATIENT_DIAGNOSIS_LOAD_BY_PATIENT_ID_CLINIC_BRANCH_ID_FOR_RECOMMENDATION_SP = "[PatientDiagnosis_LoadByPatientIDANDClinicBranchIDForRecommendation]";
        public static string PATIENT_LAST_SEEN_BY_PATIENT_ID = "PatientLastSeenBy_LoadByPatientID";



        public static string PATIENT_LOAD_ALL_MEDICAL_HISTORY = "PatientMedicalHistory_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_FAMILY_HISTORY = "PatientFamilyHistory_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_SOCIAL_HISTORY = "PatientSocialHistory_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_IMMUNIZATION = "PatientImmunization_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_VITAL = "PatientVital_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_HPI = "PatientHPI_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_ROS = "PatientROS_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_PHYSICALS = "PatientPhysical_LoadAllByPatientID";

        public static string PATIENT_LOAD_ALL_INHOUSE_LABS = "PatientInHouseLabTest_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_LABS = "PatientLab_LoadAllByPatientId";
        public static string PATIENT_LOAD_ALL_PROCEDURE = "PatientProcedure_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_PRESCRIPTION_MEDICATION = "PrescriptionMedication_LoadByPatientID";
        public static string PATIENT_LOAD_ALL_RECOMMENDATION = "PatientRecommendation_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_CHRONIC_DIAGNOSIS = "PatientDiagnosisChronic_LoadAllByPatientID";

        public static string PATIENT_FOLLOW_UP_ALL_LOAD_BY_PATIENT_ID_SP = "PatientFollowUp_LoadAllByPatientID";
        public static string PATIENT_FOLLOW_UP_INSTRUCTION_ALL_LOAD_BY_PATIENT_ID_SP = "PatientFollowupInstruction_LoadAllByPatientID";

        public static string PATIENT_LOAD_ALL_DICTATION = "ModuleDictation_LoadAllByPatientID";
        public static string PATIENT_LOAD_ALL_NOTES = "ModuleNote_LoadAllByPatientID";
        public static string PATIENT_FAMILY_DELETED_SP = "PatientFamilyHistory_Delete";
        public static string PATIENT_MEDICAL_DELETED_SP="PatientMedicalHistory_Delete";
        public static string ICD_CODE_MANAGE_CUSTOM = "CustomICDCodeDescription_InsertUpdateDeleteTVP";




    }
}
