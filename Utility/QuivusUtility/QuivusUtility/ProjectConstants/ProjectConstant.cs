using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quivus.Utility
{
    /// <summary>
    /// Author Muhammad Ismail
    /// created Date 14 jan 2011
    ///  This Constant class Is intended for project general contants
    /// </summary>
    public class ProjectConstant
    {
        public const int THUMBNAIL_WIDTH = 18;
        public const int THUMBNAIL_HEIGHT = 19;


        public const string LOCKED_STATUS = "locked";
        public const string ACTIVE_STATUS = "active";



        public const string ALL_REGISTERATION_STATIC_DATA_ALL = "All";
        public const string STATUS_TYPE_REGISTERATION_STATIC_DATA = "StatusType";
        public const string USER_TYPE_REGISTERATION_STATIC_DATA = "UserType";
        public const string PREFIX_TYPE_REGISTERATION_STATIC_DATA = "PrefixType";
        public const string GENDER_TYPE_REGISTERATION_STATIC_DATA = "GenderType";
        public const string MARITAL_STATUS_TYPE_REGISTERATION_STATIC_DATA = "MaritalStatusType";
        public const string RACE_TYPE_REGISTERATION_STATIC_DATA = "RaceType";
        public const string ETHINICITY_TYPE_REGISTERATION_STATIC_DATA = "EthinicityType";
        public const string OCCUPATION_TYPE_REGISTERATION_STATIC_DATA = "OccupationType";
        public const string LANGUAGE_TYPE_REGISTERATION_STATIC_DATA = "LanguageType";
        public const string LANGUAGE_ABILITY_MODE_TYPE_REGISTERATION_STATIC_DATA = "LanguageAbilityModeType";
        public const string RELATIONSHIP_TYPE_REGISTERATION_STATIC_DATA = "RelationShipType";
        public const string SMOKING_TYPE_REGISTERATION_STATIC_DATA = "SmokingType";
        public const string INSURANCE_TYPE_REGISTERATION_STATIC_DATA = "InsuranceType";
        public const string INSURANCE_NATURE_TYPE_REGISTERATION_STATIC_DATA = "InsuranceNature";
        public const string INSURANCE_ORGANIZATION_TYPE_REGISTERATION_STATIC_DATA = "InsuranceOrganization";
       
        
        
        public static string ICDCustomCodeTVP = "ICDCustomCodeTVP";
        public const string PREFERENCE_DB = "PreferencesDB";
        public const string COMMUNICATION_DB = "CommunicationDB";
        public const string ADDRESS_BOOK_DB = "AddressBookDB";
        public const string INSURANCE_DB = "InsuranceDB";
        public const string ENCOUNTER_DB = "EncounterDB";

        public const string STATION_IDS_TVP = "StationIdsTVP";
        public const string PHONE_TVP = "PhoneTVP";
        public const string ADDRESS_TVP = "AddressTVP";
        public const string PATIENT_LANGUAGE_TVP = "PatientLanguageTVP";
        public const string PATIENT_INSURANCE_TVP = "PatientInsuranceTVP";
        public const string MODULE_NOTE_TVP = "ModuleNoteTVP";
        public const string IMMUNIZATION_TVP = "PatientImmunizationTVP";

        public const string MODULE_DICTATION_TVP = "ModuleDictationTVP";

        public const string PATIENT_ALLERGY_TVP = "PatientHistoryAllergyTVP";
        public const string PATIENT_MEDICAL_HISTORY_TVP = "PatientHistoryMedicalTVP";
        public const string PATIENT_FAMILY_HISTORY_TVP = "PatientHistoryFamilyTVP";

        public const string PATIENT_DIAGNOSIS_TVP = "PatientDiagnosisTVP";


        public const string REGISTRATION_NKDA = "NKDA(No Known Drug Allergy)";
        public const string VISIT_NKDA = "nkda";

        public const bool RESPONSE_SUCCESS_STATUS = true;
        public const bool RESPONSE_FAILURE_STATUS = false;

        public const string STRING_NULL = "NULL";
        public const string STRING_TRUE = "True";
        public const string STRING_FALSE = "False";

        public const string SESSION_TIME_OUT = "SessionTimeOut";

        // front end can not send null value in date so this value is added to compare it before insert inot DB
        public static DateTime DATE_MINIMUM = new DateTime(1901, 01, 01, 12, 0, 0); //= DateTime.MinValue;

        public static DateTime DATE_LIMIT = new DateTime(1901, 1, 1);
        #region Error Codes

        public enum ERROR_CODE
        {
            InvalidArguments,
            InvalidCredentails,
            AccountLocked,
            GenericDTOConversion,
            UserDTOConversion,
            ServiceUnavaliableError,
            AddressBookTableConversion,
            UserManagerTableConversion,
            EmergencyTableConversion,
            RecordNotInserted,
            AddressBookDTOConversion,
            VisitDTOConversion,
            ModuleNoteTableConversion,
            MedicalHistoryConversion,
            InvalidSession = 15,
            VITALDTOConversion,
            FollowupLabsConversion,
            ModuleDictationConversion,
            AllergyTableConversion,
            PrescriptionMedicationTableConversion,

        }

        #endregion

        public enum ADDRESS_PARTY_TYPE
        {
            Patient = 1,
            EmergencyContact = 9,
            PatientGuarantor = 10
        }

        public enum ADDRESS_OWNER_TYPE
        {
            Self = 1,
            Mother = 2,
            Father = 3
        }

        public enum USER_PROFILE_CATEGORY_ID
        {
            Address = 11,
            State = 12,
            City = 13,
            Zip = 14,

            Phone = 6,
            MotherPhoneExt = 19,
            FatherPhoneExt = 21,
            EmergencyContactPhoneExt = 26,
            GurantorPhoneExt = 29

        }

        public enum VISIT_TEMPLATE_TYPE
        {
            AccuteVisit = 1,
            ChronicDiseaseVisit = 2,
            ComprehensiveVisit = 3
        }


        public enum USER_TYPE
        { Doctor = 3, Nurse = 2, Patient = 1, LabTechnician = 4, Admin = 5, NursePractitioner = 6, Receptionist = 7, BillingAssistant = 8, PracticeManager = 9, JnrSecurityAdmin = 19, Student=21, BillingManager=22 }


        public const string HASH_ALGO_SHA1 = "SHA1";

        public const int PATIENT_SEARCH_PAGE_SIZE = 200;

        #region Error Messages

        public const string INVALID_LOGIN_ERROR_MSG = "Invalid user name or password.";
        public const string ACCOUNT_lOCKED_ERROR_MSG = "Account Locked.";
        public const string INVALID_ARGUMENT_EXCEPTION_MSG = "Invalid Arguments.";

        public const string GENERIC_EXCEPTION_MSG = "Service error. Please contact your administrator.";

        public const string GENERIC_DTO_CONVERSION_ERROR_MSG = "GenericDataReaderToDTOConverter";
        public const string USER_MANAGER_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";
        public const string ADDRESS_BOOK_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";
        public const string ADDRESS_BOOK_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "AddressBookTableConversionException";
        public const string MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "ModuleNoteTableConversionException";

        public const string VISIT_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";

        public const string EMERGENCY_CONTACT_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Emergency Contact TableConversionException";

        public const string USER_MANAGER_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "UserManagerTableConversionException";
        public const string SERVICE_UNAVALIABLE_ERROR_MSG = "Service Unavailable";

        public const string STATE_ID_INVALID_ERROR_MSG = "Invalid State ID.";
        public const string CITY_NAME_INVALID_ERROR_MSG = "Invalid City Name.";
        public const string ZIP_CODE_INVALID_ERROR_MSG = "Invalid Zip Code.";
        public const string USER_ID_INVALID_ERROR_MSG = "Invalid User ID.";
        public const string USER_TYPE_ID_INVALID_ERROR_MSG = "Invalid User type ID.";
        public const string PATIENT_ID_INVALID_ERROR_MSG = "Invalid Patient ID.";

        public const string INVALID_OPERATION = "Invalid Operation received.";

        public const string NULL_CLINIC_USER_DTO_RECEIVED = "ClinicUserDTO is null.";

        public const string PROFILE_ID_INVALID_ERROR_MSG = "Invalid Profile ID.";

        public const string PASSWORD_INVALID_ERROR_MSG = "Invalid User Password.";
        public const string USER_TYPE_INVALID_ERROR_MSG = "Invalid User Type ID.";
        public const string CLINIC_ID_INVALID_ERROR_MSG = "Invalid clinic branch ID.";
        public const string CLINIC_BRANCH_ID_INVALID_ERROR_MSG = "Invalid Clinic Branch ID.";
        public const string EMAIL_ID_INVALID_ERROR_MSG = "Invalid Email ID.";
        public const string SEARCH_ENGINE_ID_INVALID_ERROR_MSG = "Invalid Search Engine ID.";
        public const string ALLERGY_INVALID_ERROR_MSG = "Allergy is Empty.";
        public const string ALLERGY_NAME_INVALID_ERROR_MSG = "Invalid Allergy Name.";
        public const string ICDCODE_CODE_INVALID_ERROR_MSG = "Invalid ICD Code.";
        public const string IcdcodeCodeConversionMsg = "Convertion Failed For ICD CODE.";
        public const string DEMAND_PAGE_NO_INVALID_ERROR_MSG = "Invalid Demand Page No.";
        public const string SEARCH_STRING_INVALID_ERROR_MSG = "Invalid Search Criteria.";

        public static string EMERGENCY_CONTACT_TVP = "PatientEmergencyContactTVP";

        public const string RECORD_NOT_INSERTED_ERROR_MSG = "Record Not Inserted.";

        public const string RECORD_NOT_UPDATED_ERROR_MSG = "Record Not Updated.";

        public const string PARTY_GUID_EMPTY_ERROR_MSG = "Invalid Guid.";

        public const string VISTI_ID_INVALID_ERROR_MSG = "Invalid Visit ID.";

        public const string HPI_TYPE_ID_INVALID_ERROR_MSG = "Invalid HPI Type ID.";

        public const string DATACONTROL_TYPE_ID_INVALID_ERROR_MSG = "Invalid DATA CONTROL Type ID.";

        #endregion


        public static string INSERTION_FAILED_EXCEPTION = "Insertion Failed";
        public static string PROFILE_SETTING_ERROR_MSG = "Error Converting patient profile information";

        public static int SECTION_FIRST = 1;
        public static int SECTION_SECOND = 2;
        public static int SECTION_THIRD = 3;

        public static string ALLERGY_COLLECTION = "Allergies";
        public static string DIAGNOSIS_COLLECTION = "Diagnosis";
        public static string CHRONICMEDICATION_COLLECTION = "ChronicMedication";
        public static string IMMUNIZATION_COLLECTION = "Immunization";
        public static string INSURANCE_COLLECTION = "Insurance";
        public static string SECTION_ID_INVALID_ERROR_MSG = "Invalid Section Id";
        public static string SEARCH_ENGINE_TYPE_TVP = "Rec_Ids";
        public static string DEFUALT_PASSWORD = "quivus";
        public static string GURANTOR_LIST_EMPTY = "Guarantor list is empty";
        public static string EMPTY_LIST = "List is empty";
        public static string USER_ID_DTO_CONVERSION_ERROR_MSG = "Error converting user id information ";
        public static string INVALID_USER_SESSION = "User session is null";
        public static string INVALID_USER_SESSION_ID = " User session Id is invalid";
        public static string EXPIRED_USER_SESSION_ID = " User session Id is expired";





        public static string MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG = "Error converting medical history type information ";

        public const string VIEW_BACKGROUND_DTO_CONVERSION_ERROR_MSG = "Error converting View Background information ";

        public static string VISIT_ID_INVALID_ERROR_MSG = " Visit Id is invalid";
        public static string PATIENT_MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG = "Error converting patient medical history information";
        public static string FAMILY_HISTORY_DTO_CONVERSION_ERROR_MSG = "Error converting family history type information ";
        public static string PATIENT_FAMILY_HISTORY_DTO_CONVERSION_ERROR_MSG = "Error converting patient family history information";
        public static string FAMILY_HISTORY_ID_INVALID_ERROR_MSG = "Family history id is invalid";
        public static string PATIENT_LABS_DTO_CONVERSION_ERROR_MSG = "Error converting patient labs information";

        public const string PATIENT_DIAGNOSIS_DTO_CONVERSION_ERROR_MSG = "Error converting patient diagnosis information";
        public const string PATIENT_HPI_DTO_CONVERSION_ERROR_MSG = "Error converting patient HPI information";

        public const string DIAGNOSIS_CONVERSION_ERROR_MSG = "Error converting patient Diagnosis information";

        //   public static string SELECTION_YES="yes";
        //   public static string SELECTION_NO="no";
        public static string SELECTION_NULL = "null";
        public static string SELECTION_TRUE = "true";
        public static string SELECTION_False = "false";

        public static string PATIENT_EXTERNAL_TVP = "PatientRcopiaIdsTVP";
        public static string PATIENT_ROS_TVP = "PatientROSTVP";
        public const string PATIENT_HPI_TVP = "PatientHPITVP";
        public static string PATIENT_PHYSICAL_TVP = "PatientPhysicalTVP";
        public static string PATIENT_RECOMMENDATION_TVP = "PatientRecommendationTVP";

        public static string PATIENT_EXTERNAL_ADDRESSBOOK_TVP = "AddressPartyRecGuidTVP";
        public static string PATIENT_ALLERGIES_TVP = "PatientAllergyTVP";
        public static string PATIENT_PRESCRIPTION_MEDICATION_TVP = "PrescriptionMedicationTVP";
        public static string PATIENT_MEDICATION_TVP = "PatientMedicationTVP";

        public static string PHYSICAL_SUB_CATEGORY_DTO_CONVERSION_ERROR_MSG = "Error converting physical sub category information";
        public static string VISIT_STAKE_HOLDER_DTO_CONVERSION_ERROR_MSG = "Visit Stake holder dto conversion error";
        public static string USER_PASSWORD_INVALID_ERROR_MSG = "Provide user password";
        public static string MEDICATION_PRESCRIBED_DTO_CONVERSION_ERROR_MSG = "Error converting paient medication prescribed information";
        public static string ALLERGY_DTO_CONVERSION_ERROR_MSG = "Error converting paient allergies information";

        public static string ADDICTION_ALCOHOL = "alcohol";
        public static string ADDICTION_DRUGS = "drugs";
        public static string ADDICTION_TOBACCO = "tobacco";
        public static string VITAL_DTO_CONVERSION_ERROR_MSG = "Error converting Vtial DTO";
        public static string INVALID_ENCOUNTER_TYPE_MSG = "Invalid encounter type id";
        public static string VISIT_DATA_DTO_CONVERSION_ERROR_MSG = "Error converting visit DTO";
        public static string PATIENT_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG = "Error converting patient prescription DTO";
        public static string PATIENT_PRESCRIPTION_ID_INVALID_ERROR_MSG;
        public static string PATIENT_PROCEDURE_TVP = "PatientProcedureTVP";
        public static string PATIENT_INHOUSE_LABS_TVP = "PatientInHouseLabTestTVP";
        public static string PATIENT_FOLLOWUP_INSTRUCTION_TVP = "PatientFollowupInstructionTVP";
        public static string INHOUSE_LABS_CONVERSION_ERROR_MSG = "Error converting patient in house labs DTO";
        public static string PATIENT_MEDICATION_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG = "Error converting patient medication DTO";
        public static string PATIENT_FOLLOWUP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG = "Error converting PatientFollowupInstructionDTOListConverter";
        public static Guid EMPTY_GUID = new Guid("00000000-0000-0000-0000-000000000000");
        public static string FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG = "Error converting patient FollowupLabs DTO";
        public static string PATIENT_IN_HOUSE_LABS_DTO_CONVERSION_ERROR_MSG = "Error converting patient in house labs DTO";
        public static string PATIENT_DIAGNOSIS_RESOLVED_DTO_CONVERSION_ERROR_MSG = "Error converting patient diagnosis DTO";
        public static string RECENT_PATIENT_DTO_CONVERSION_ERROR_MSG = "Error converting recent patient DTO";
        public static string ModuleTypeConverter = "Error converting module type DTO";
        public static string PATIENT_PROCEDURE_DTO_CONVERSION_ERROR_MSG = "Error converting procedure DTO";
        public static string TEMPLATE_ID_INVALID_ERROR_MSG = "Invalid template Id ";
        public static string PROPERTY_CREATED_BY = "CreatedBy";
        public static string PROPERTY_MODIFIED_BY = "ModifiedBy";
        public static string PROPERTY_GUID = "GUID";
        public static string PROPERTY_DICTATION_ID = "DictationID";
        public static string PROPERTY_CREATED_AT = "CreatedAt";
        public static string PROPERTY_MODIFIED_AT = "ModifiedAt";
        public static string PROPERTY_VISIT_ID = "VisitID";
        public static string NO_VISIT_TEMPLATE_FOUND = "No visit found for specified template";
        public static string PATIENT_MODULE_DICATION_DTO_CONVERSION_ERROR_MSG = "Error converting module dictation DTO";
        public static string PATIENT_MODULE_NOTES_DTO_CONVERSION_ERROR_MSG = "Error converting module notes DTO";
        public static string SOCIAL_HISTORY_DTO_CONVERSION_ERROR_MSG = "Error converting social history DTO";
        public static string PATIENT_REGISTRATION_EMAIL_TEMPLATE = "PatientRegistration";
        public static string INVALID_UPDATE_WITHOUT_IS_DIRTY = "You are trying to update record without setting IsDirty";
        public static string EMAIL_NOT_FOUND = "No email found for specified user";
        public static string DTO_CONVERSION_ERROR_MSG = "Data reader to DTO conversion error";
        public static string PARAMETER_POPULATOR_ERROR_MSG = "Parameter population error";
        public static string IMMUNIZATION_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting immunization to data table";
        public static string HPI_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting HPI to data table";
        public static string IN_HOUSE_LABS_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting in house labs to data table";
        public static string ROS_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting ros to data table";
        public static string PHYSICALS_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting physicals to data table";
        public static string RECOMMENDATION_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting recommendation to data table";
        public static string PROCEDURE_TABLE_CONVERSION_CONVERSION_ERROR_MSG = "Error converting procedure to data table";
        public static string PATIENT_FROM_RECORD_INVALID_ERROR_MSG = "Invalid range for from-record";
        public static string PATIENT_TO_RECORD_INVALID_ERROR_MSG = "Invalid range for to-record";

        public static string PATIENT_ROS_SUB_LEVEL_CATEGORY_CONVERSION_ERROR_MSG = "Error converting ros sub level categories";
        public static string ROS_SUB_CATEGORY_TYPE_INVALID_ERROR_MSG = "Invalid Ros sub category type Id";
        public static string ALLERGIES_CONVERSION_ERROR_MSG = "Error converting allergies list";
        public static string PRESCRIPTION_MEDICATION_CONVERSION_ERROR_MSG = "Error converting medication prescription list";

        public static string PATIENT_CLINIC_ID_INVALID_ERROR_MSG = "Invalid clinic id";

        public static string LOAD_AUTOSAVE_MSG = "Error loadig and converting autosave object";
        public static string SELECTION_Y = "y";

        public static string INVALID_CREATED_STATION_ID = "Invalid CreatedStationID";
        public static string PATIENT_BASIC_INFO_DTO_CONVERSION_ERROR_MSG = "Error converting patient basic info DTO";


        public const string NULL_DOCTOR_DTO_RECIEVED_WHILE_REGISTRATION = " Doctor DTO can not be null while registering a user";
        public const string NULL_NURSEPRACTIOER_DTO_RECIEVED_WHILE_REGISTRATION = " Nurse PRactioner DTO can not be null while registering a user";

        public const string NULL_NURSE_DTO_RECIEVED_WHILE_REGISTRATION = " Nurse DTO can not be null while registering a user";

        public const string NULL_RECEPTIONIST_DTO_RECIEVED_WHILE_REGISTRATION = " Receptionist DTO can not be null while registering a user";
        public const string NULL_PRACTICE_MANAGER_DTO_RECIEVED_WHILE_REGISTRATION = " Practice Manager DTO can not be null while registering a user";

        public const string NULL_BillingASS_DTO_RECIEVED_WHILE_REGISTRATION = " Billing Assistent DTO can not be null while registering a user";

        public const string NULL_SECURITY_ADMIN_DTO_RECIEVED_WHILE_REGISTRATION = " Security Admin Assistent DTO can not be null while registering a user";

        public const string NULL_JrSECURITY_ADMIN_DTO_RECIEVED_WHILE_REGISTRATION = " Jr Security Admin Assistent DTO can not be null while registering a user";
        public const string NULL_STUDENT_DTO_RECIEVED_WHILE_REGISTRATION = " Student DTO can not be null while registering a user";
        public const string NULL_BILLING_MANAGER_DTO_RECIEVED_WHILE_REGISTRATION = " Billing Manager DTO can not be null while registering a user";

        public const string NULL_LAB_TECH_DTO_RECIEVED_WHILE_REGISTRATION = " Lab Tech DTO can not be null while registering a user";

        public static string IP_CONFLICT_EXPIRED_USER_SESSION_ID = "You have logged-in at some other location with Ip-address = ";
        public static string IP_CONFLICT_EXPIRED_USER_SESSION_TERMINATED = " your session will be terminated";

          public static string PATIENT_FOLLOW_UP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG = "Error converting patient follow up instruction";
         // public static string PATIENT_FOLLOW_UP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG = "Error converting patient follow up instruction";

        public static string EMPTY_STATION_LIST = "station is empty";

        public static string STATION_ID_INVALID_ERROR_MSG = "station id is invaild";
       
    }
}