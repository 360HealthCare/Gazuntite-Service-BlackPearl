using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusUtility.ProjectConstants
{
    /// <summary>
    /// Author Muhammad Ismail 
    /// Date 18 March 
    /// Contain Error Messages
    /// </summary>
    public class ErrorMessages
    {
        public static string INVALID_MODIFIEDBY_ERROR_MSG = "ModifiedBy id is invalid";
        public static string INVALID_RELATIONSHIP_ERROR_MSG = " Invalid relationship id";
        public static string INVALID_FAMILY_TYPE_ID_ERROR_MSG = " Invalid family type id";
        public static string INVALID_DISEASE_TYPE_ID_ERROR_MSG = " Invalid disease type id";

        public const string DIAGNOSIS_DTO_INVALID_ERROR_MSG = "Diagnosis object is Null.";
        public const string HPI_DTO_INVALID_ERROR_MSG = "HPI object is Null.";

        public static string VITAL_DTO_INVALID_ERROR_MSG = "Vital object is Null.";
        public static string INVALID_LAB_TEST_ID_ERROR_MSG = "Invalid lab test type id";
        public static string INVALID_LAB_ONSET_ERROR_MSG = "Lab OnSet date is invalid";
        public static string INVALID_LAB_TEST_ERROR_MSG = "Lab test type is invalid";
        public static string INVALID_CREATEDAT_ERROR_MSG = "Created at date is invalid";
        public static string PATIENT_FAMILY_DTO_INVALID_ERROR_MSG = "Patient medical history object is Null.";
        public static string PATIENT_MEDICAL_DTO_INVALID_ERROR_MSG = "Patient family history object is Null.";
        public static string INVALID_PATIENT_MEDICALHISTORY_DESCP_ERROR_MSG = "Patient Medical History description is invalid";

        public const string VISIT_DTO_INVALID_ERROR_MSG = "* Visit object is Null.";
        public const string USER_ID_INVALID_ERROR_MSG = "* User id is Invalid.";
        public const string PATIENT_ID_INVALID_ERROR_MSG = "* Patient id is Invalid.";
        public const string CLINIC_DTO_INVALID_ERROR_MSG = "* Clinic object is Null.";
        public const string VISIT_DATE_INVALID_ERROR_MSG = "* Visit date is Null.";
        public const string VISIT_RECORD_STATUS_INVALID_ERROR_MSG = "* Visit record status is Invalid.";
        public const string USER_TYPE_INVALID_ERROR_MSG = "* User type is Null.";
        public const string VISIT_STATUS_INVALID_ERROR_MSG = "* Visit status is Invalid.";
        public const string VISIT_OWNER_ID_INVALID_ERROR_MSG = "* Visit owner id is Invalid.";
        public const string VISIT_TEMPLATE_ID_INVALID_ERROR_MSG = "Visit template id is Invalid";
        public const string VISIT_TYPE_ID_INVALID_ERROR_MSG = "Visit type id is Invalid";

        public const string IMMUNIZATION_DTO_INVALID_ERROR_MSG = "* Immunization object is Null.";
        public const string IMMUNIZATION_TYPE_INVALID_ERROR_MSG = "* Immunization Type is Invalid.";


        public const string MODULE_NOTE_DTO_INVALID_ERROR_MSG = "* Module Note object is Null.";
        public const string MODULE_TYPE_ID_INVALID_ERROR_MSG = "Module type id is Invalid";
        public const string NOTE_INVALID_ERROR_MSG = "Note is Invalid";

        public const string VISIT_ID_INVALID_ERROR_MSG = "Visit id is Invalid";

        public static string INVALID_MEDICAL_HISTORY_ERROR_MSG = "Medical History type id is invalid";

        public static string INVALID_VISIT_ID_MEDICAL_HISTORY_ERROR_MSG = "Invalid Visit Id in medical history";

        public static string INVALID_VISIT_ID_ERROR_MSG ="Visit Id is invalid";
        public static string INVALID_CREATEDBY_ERROR_MSG = "Created By is invalid";
        public static string INVALID_PATIENT_ID_ERROR_MSG ="Patient Id is invalid";
        public static string INVALID_ROS_SUB_ID_ERROR_MSG = "Ros sub category id is invalid";
        public static string INVALID_ROS_MAIN_ID_ERROR_MSG = "Ros main category id is invalid";

        public static string PATIENT_QUICKINPUT_DTO_INVALID_ERROR_MSG="Patient quick input object is invalid";
        public static string PATIENT_QUICKINPUT_DTO_EMPTY_ERROR_MSG = "Patient quick input object is empty";
        public static string INVALID_MODIFIEDAT_ERROR_MSG="Modified At is invalid";
        public static string INVALID_ENDDATE_ERROR_MSG ="Socail addiction end date is invalid";
        public static string INVALID_ONSET_ERROR_MSG = "Socail addiction onset date is invalid";
        public static string SOCIAL_ADDICTION_DTO_INVALID_ERROR_MSG = "Socail addiction dto is invalid";
        public static string SOCIAL_ADDICTION_TYPE_ID_ERROR_MSG = "Socail addiction type id is invalid";
        public static string INVALID_PATIENT_SOCIAL_HISTORY_ID_ERROR_MSG="Patient Social history is invalid";
        public static string INVALID_PHYSICAL_SUB_ID_ERROR_MSG="Physical Sub category Id is invalid";
        public static string INVALID_PHYSICAL_MAIN_ID_ERROR_MSG="Physical Main category Id is invalid";
        public static string PHYSICAL_LIST_INVALID_ERROR_MSG = "Physical List is invalid";
        public static string ROS_LIST_INVALID_ERROR_MSG = "ROS List is invalid";
        public static string PATIENT_SOCIAL_HISTORY_ID_ERROR_MSG="Patient Social history id is invalid";
        public static string INVALID_ALLERGY_REACTION_ERROR_MSG ="Please enter aleergy reaction";
        public static string PATIENT_Date_OF_BIRTH_ERROR_MSG = "Patient date of birth is invalid";
        public static string PATIENT_QUIVUS_ID_ERROR_MSG ="Patient quivus id is invalid";
        public static string INVALID_RECGUID_ERROR_MSG = "Patient recguid id is invalid";
        public static string PATIENT_DTO_INVALID_ERROR_MSG="Patient DTO is invalid";

        public static string SOCIAL_ADDICTION_INVALID_CREATEDBY_ERROR_MSG ="Invalid created by id in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_ONSET_ERROR_MSG  ="Invalid onset date in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_ENDDATE_ERROR_MSG = "Invalid end date in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_MODIFIEDAT_ERROR_MSG= "Invalid modified at in social addiction";

        public static string SOCIAL_ADDICTION_INVALID_MEDIUM_TYPE_ID_ERROR_MSG = "Invalid medium type id in social addiction";
        public static string SOCIAL_ADDICTION_INVALID_USAGEAMOUNT_ID_ERROR_MSG = "Invalid usgae amount id in social addiction";

        public static string DIANOSIS_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in diagnosis";
        public static string DIANOSIS_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in  diagnosis";
        public static string DIANOSIS_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in  diagnosis";
        public static string DIANOSIS_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in diagnosis";
        public static string DIANOSIS_INVALID_CREATEDBY_ERROR_MSG= "Invalid created by id in diagnosis";

        public static string FOLLOWUP_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in followup";
        public static string FOLLOWUP_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in followup";
        public static string FOLLOWUP_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in followup";
        public static string FOLLOWUP_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in followup";
        public static string FOLLOWUP_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in followup";

        public static string RECOMMENDATION_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in Recommendation";
        public static string RECOMMENDATION_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in Recommendation";
        public static string RECOMMENDATION_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in Recommendation";
        public static string RECOMMENDATION_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in Recommendation";
        public static string RECOMMENDATION_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in Recommendation";
        public static string RECOMMENDATION_LIST_INVALID_ERROR_MSG = "Recommendation list is empty";

        public static string HPI_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in HPI";
        public static string HPI_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in HPI";
        public static string HPI_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in HPI";
        public static string HPI_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in HPI";
        public static string HPI_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in HPI";

        public static string LABS_DTO_INVALID_ERROR_MSG="Lab DTO is invalid";
        public static string LABS_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in Labs";
        public static string LABS_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in Labs";
        public static string LABS_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in Labs";
        public static string LABS_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in Labs";


        public static string PHYSICALS_INVALID_PATIENT_ID_ERROR_MSG= "Invalid patient id in physical list";
        public static string PHYSICALS_INVALID_VISIT_ID_ERROR_MSG= "Invalid visit id in physical list";
        public static string PHYSICALS_INVALID_MODIFIEDBY_ERROR_MSG= "Invalid modified by id in physical list";
        public static string PHYSICALS_INVALID_CREATEDAT_ERROR_MSG= "Invalid created at in physical list";
        public static string PHYSICALS_INVALID_CREATEDBY_ERROR_MSG= "Invalid created by in physical list";


        public static string ROS_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in ros list";
        public static string ROS_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in ros list";
        public static string ROS_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in ros list";
        public static string ROS_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in ros list";
        public static string ROS_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in ros list";

        public static string VITAL_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in vital";
        public static string VITAL_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in vital";
        public static string VITAL_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in vital";
        public static string VITAL_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in vital";


        public static string ALLERGY_INVALID_ONSET_ERROR_MSG = "Invalid onset date in allergy";
        public static string ALLERGY_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in allergy";
        public static string ALLERGY_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in allergy";
        public static string ALLERGY_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at date in allergy";
        public static string ALLERGY_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in allergy";

        public static string CHRONIC_MEDICATION_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in chronic medication";
        public static string CHRONIC_MEDICATION_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in chronic medication";
        public static string CHRONIC_MEDICATION_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in chronic medication";
        public static string CHRONIC_MEDICATION_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at date in chronic medication";

        public static string MEDICAL_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in medical history";
        public static string MEDICAL_HISTORY_INVALID_MODIFIEDAT_ERROR_MSG = "Invalid modified at in medical history";
        public static string MEDICAL_HISTORY_INVALID_CREATEDAT_ERROR_MSG= "Invalid created at in medical history";
        public static string MEDICAL_HISTORY_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in medical history";

        public static string FAMILY_HISTORY_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in family history";
        public static string FAMILY_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in family history";
        public static string FAMILY_HISTORY_INVALID_MODIFIEDAT_ERROR_MSG = "Invalid modified at in family history";
        public static string FAMILY_HISTORY_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in family history";
        public static string FAMILY_HISTORY_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in family history";

        public static string SOCIAL_HISTORY_PATIENT_ID_INVALID_ERROR_MSG = "Invalid patient id in social history";
        public static string SOCIAL_HISTORY_VISIT_ID_INVALID_ERROR_MSG = "Invalid visit id in social history";
        public static string SOCIAL_HISTORY_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in social history";
        public static string SOCIAL_HISTORY_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in social history";
        public static string SOCIAL_HISTORY_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in social history";
        public static string INVALID_ACTION_DATE_ERROR_MSG ="Invalid action date";
        public static string INVALID_PROVIDER_ID_ERROR_MSG="Invalid provider id";
        public static string INVALID_USER_ID_ERROR_MSG = "Invalid user id";
        public static string INVALID_USER_TYPE_ID_ERROR_MSG= "Invalid user type id";
        public static string INVALID_COSIGN_DATE_ERROR_MSG="Invalid Co-Sign date";

        public static string CLINIC_BRANCH_ID_INVALID_ERROR_MSG = "Invalid Clinic branch Id";
        public static string CPT_MEDICAL_TYPE_ID_ERROR_MSG = "Invalid CPT Medical type Id";
        public static string PROCEDURE_DTO_INVALID_ERROR_MSG="Procedure object is invalid";
        public static string CPT_MEDICAL_TYPE_ERROR_MSG="Invalid CPT Medical Type";
        public static string INVALID_INHOUSELAB_TEST_TYPE_ID_ERROR_MSG="Invalid in house lab test type id";
        public static string INVALID_IN_HOUSELAB_TEST_TYPE_ERROR_MSG = "Invalid in house lab test type";
        public static string INVALID_LAB_LIONIC_TEST_TYPE_ERROR_MSG = "Invalid lab lionic test type";
        public static string INVALID_LAB_LIONIC_TEST_TYPE_ID_ERROR_MSG = "Invalid lab lionic test type id";

        public static string INVALID_MODULE_DICTATION_GUID_ERROR_MSG = "Module Dictatid is invalid";
        public static string INVALID_ICD_CODE_OBJECT_ERROR_MSG = "Invalid ICD Code Object in diagnosis";
        public static string INVALID_ICD_CODE_ID_ERROR_MSG = "Invalid ICD Code id in diagnosis";
        public static string FOLLOWUP_DATE_ERROR_MSG = "Invalid follow up date in followup";
        public static string FOLLOWUP_INVALID_DRUG_ID_ERROR_MSG = "Invalid drug id in followup";
        public static string FOLLOWUP_INVALID_LAB_TEST_ID_ERROR_MSG = "Invalid lab test id in followup";
        public static string FOLLOWUP_METHOD_TYPE_ERROR_MSG = "Invalid follow up type object in followup";
        public static string FOLLOWUP_METHOD_TYPE_ID_ERROR_MSG = "Invalid follow up type id in followup";
        public static string USER_TYPE_ID_INVALID_ERROR_MSG="Invalid user type Id";
      
        public static string IMMUNIZATION_MODIFIED_BY_INVALID_ERROR_MSG="Invalid modified by in immunization";
        public static string PROCEDURE_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in procedure";
        public static string PROCEDURE_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by in procedure";
        public static string PROCEDURE_INVALID_CREATEDAT_ERROR_MSG = "Invalid create at in procedure";
        public static string PROCEDURE_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by in procedure";

        public static string INVALID_FOLLOW_UP_SERVICE_TYPE_ID_ERROR_MSG = "Invalid service type id in followup instructions";
        public static string INVALID_FOLLOW_UP_SERVICE_TYPE_ERROR_MSG = "Invalid service type object in followup instructions";
        public static string FOLLOW_UP_INSTRUCTION_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by in followup instructions";
        public static string FOLLOW_UP_INSTRUCTION_INVALID_CREATEDAT_ERROR_MSG = "Invalid created at in followup instructions";
        public static string FOLLOW_UP_INSTRUCTION_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by in followup instructions";
        public static string FOLLOW_UP_INSTRUCTION_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id  in followup instructions";
        public static string FOLLOW_UP_INSTRUCTION_INVALID_PATIENT_ID_ERROR_MSG = "Invalid patient id in followup instructions";
        public static string FOLLOW_UP_INSTRUCTION__LIST_INVALID_ERROR_MSG = "Invalid followup instructions list";
        public static string MODULE_DICATION_USER_TYPEID_INVALID_ERROR_MSG = "Invalid user type id in module dictation";
        public static string VISIT_OWNER_TYPE_DTO_INVALID_ERROR_MSG = "Invalid owner type object";
        public static string VISIT_OWNER_TYPE_ID_INVALID_ERROR_MSG = "Invalid owner type id";
        public static string INVALID_ROS_SUB_LEVEL_CATEGORY_ID_ERROR_MSG = "Ros sub level category id is invalid";
        public static string MEDICATION_LIST_INVALID_ERROR_MSG="Invalid medication list";
        public static string INVALID_PATIENT_FAMILY_TYPE_ID_ERROR_MSG = "Invalid patient family histroy id";
        public static string INVALID_PATIENT_MEDICAL_ID_ERROR_MSG = "Invalid patient medical histroy id";

      

    }
}
