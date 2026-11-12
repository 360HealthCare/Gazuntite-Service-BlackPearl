using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility.ProjectConstants
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Created Date 19 Jan 2011
    /// Class Used To Store Registration Specific Constants
    /// </summary>
    public class UserRegistrationConstant
    {
        public const string REGISTRATION_NAME_EMPTY = "* Name should not be empty.";
        public const string REGISTRATION_FIRST_NAME_EMPTY = "* First name should not be empty.";
        public const string REGISTRATION_LAST_NAME_EMPTY = "* Last name should not be empty.";
        public const string REGISTRATION_GUARANTOR_FIRST_NAME_EMPTY = "* Guarantor first name should not be empty.";
        public const string REGISTRATION_GUARANTOR_FIRST_NAME_INVALID = "* Guarantor first name should not include special characters.";
        public const string REGISTRATION_GUARANTOR_LAST_NAME_EMPTY = "* Guarantor last name should not be empty.";
        public const string REGISTRATION_GUARANTOR_LAST_NAME_INVALID = "* Guarantor last name should not include special characters.";

        public const string REGISTRATION_GUARANTOR_ADRESS_EMPTY = "* Guarantor address should not be empty.";

        public const string REGISTRATION_GUARANTOR_PHONE_INVALID = "* Guarantor phone should not be empty..";

        public const string REGISTRATION_GUARANTOR_PHONE_TYPE = "* Select guarantor phone type.";

        public const string REGISTRATION_GUARANTOR_RELATION_EMPTY = "* Select relationship for guarantor.";
        public const string REGISTRATION_GUARANTOR_STATE_EMPTY = "* Select state for guarantor.";
        public const string REGISTRATION_GUARANTOR_CITY_EMPTY = "* Select city for guarantor.";
        public const string REGISTRATION_GUARANTOR_ZIPCODE_EMPTY = "* Select zipcode for guarantor.";

        public const string REGISTRATION_RELATION_EMPTY = "* Relation should not be empty.";

        public const string REGISTRATION_DOB_EMPTY = "* Date of birth should not be empty.";

        public const string REGISTRATION_GENEDER_EMPTY = "* Select Gender.";
        public const string REGISTRATION_STATUS_EMPTY = "* Select Status.";

        public const string REGISTRATION_PHONE_EMPTY = "* Phone number should not be empty.";
        //public static string REGISTRATION_PHONE_INVALID = "* Enter valid Phone number.";
        public const string REGISTRATION_PHONE_OWNER_TYPE_EMPTY = "* Phone Owner type invalid.";
        public const string REGISTRATION_PHONE_TYPE_EMPTY = "* Select Phone type.";
        public const string REGISTRATION_PHONE_EXT_EMPTY = "* Phone extension should not be empty.";
        public const string REGISTRATION_PHONE_INVALID = "* Phone number  should be of ten numeric digits.";
        public const string REGISTRATION_PHONE_EXTENSION_INVALID = "* Phone number extension should be of four numeric digits.";
        public const string REGISTRATION_PHONE_PERSONAL_INVALID = "* Personal phone number should be of ten numeric digits and extension must be of three numeric digits.";
        public const string REGISTRATION_PHONE_MOTHER_INVALID = "* Mother phone number should be of ten numeric digits .";
        public const string REGISTRATION_PHONE_EXT_MOTHER_INVALID = "* Mother phone number extension should be of four numeric digits.";
        public const string REGISTRATION_MOTHER_PHONE_TYPE_EMPTY = "* Select mother's maiden phone type.";
        public const string REGISTRATION_FATHER_PHONE_TYPE_EMPTY = "* Select father's maiden phone type.";

        public const string REGISTRATION_PHONE_FATHER_INVALID = "* Father phone number should be of ten numeric digits .";
        public const string REGISTRATION_PHONE_EXT_FATHER_INVALID = "* Father phone number extension should be of four numeric digits.";
        public const string REGISTRATION_INVALID_IMAGEPATH = "Invalid image file path provided";
        public const string REGISTRATION_NOT_UPLOADED_IMAGE = "Image not uploaded yet";
        public const string REGISTRATION_SOCIALSECURITYNUMBER_ERROR = "* Enter valid social security number.";

        public const string REGISTRATION_STATE_EMPTY = "* Select State.";
        public const string REGISTRATION_CITY_EMPTY = "* Select City.";
        public const string REGISTRATION_ZIP_EMPTY = "* Select Zip Code.";

        public const string REGISTRATION_DOCTOR_EMPTY = "* Select Doctor.";
        public const string REGISTRATION_SPECIAL_EMPTY = "* Select Doctor.";

        public const string REGISTRATION_CLINIC_EMPTY = "* Select Clinic.";

        public const string REGISTRATION_EMAIL_EMPTY = "* Email should not be empty.";
        public const string REGISTRATION_EMAIL_INVALID = "* Enter valid Email address.";

        public const string REGISTRATION_EMAIL_ALREADY_EXIST = "* Email address already exists.";

        public const string REGISTRATION_OK = "Patient Register Successfully.";

        public const string REGISTRATION_FIRST_NAME_INVALID = "* First name should not include special characters.";
        public const string REGISTRATION_LAST_NAME_INVALID = "* Last name should not include special characters.";
        public const string REGISTRATION_MIDDLE_NAME_INVALID = "* Middle name should not include special character.";

        public const string REGISTRATION_MEDICARE_INVALID = "* Medicare number should not include special characters.";
        public const string REGISTRATION_MOTHER_MAIDENNAME_INVALID = "* Mother's maiden name should not include special characters.";

        public const string REGISTRATION_FATHER_NAME_INVALID = "* Mother's maiden name should not include special characters.";

        public const string REGISTRATION_Employer_INVALID = "* Employer name should not include special characters.";

        public const string REGISTRATION_DRUGS_EMPTY = "* Drugs should not be empty.";
        public const string REGISTRATION_ALCOHOL_EMPTY = "* Alcohol should not be empty.";

        public const string REGISTRATION_INSURANCE_EMPTY = "* Insurance object is Null.";
        public const string REGISTRATION_INSURANCE_TYPE = "* Select insurance type.";
        public const string REGISTRATION_INSURANCE_NATURE = "* Select insurance nature.";
        public const string REGISTRATION_INSURANCE_ORGANIZATION = "* Select insurance organization.";
        public const string REGISTRATION_INSURANCE_POLICY_EMPTY = "* Insurance policy number should not be empty.";
        public const string REGISTRATION_INSURANCE_PERSON_NAME_EMPTY = "* Insurance person name should not be empty.";
        public const string REGISTRATION_INSURANCE_START_DATE_EMPTY = "* Select start date for Insurance.";
        public const string REGISTRATION_INSURANCE_INVALID_DATE = "* Insurance start date must be less than end date.";        
        public const string REGISTRATION_INSURANCE_END_DATE_EMPTY = "* Select end date for Insurance.";
        public const string REGISTRATION_INSURANCE_STATUS_INVALID = "* Invalid Insurance Status.";
        public const string REGISTRATION_INSURANCE_PERSON_DOB_INVALID = "* Invalid Insured Person DOB.";
        public const string REGISTRATION_INSURANCE_PERSON_GENDER_INVALID = "* Invalid Insured Person Gender.";

        public const string REGISTRATION_INSURANCE_COUNT_INVALID = "* Invalid insurance data.";

        public const string REGISTRATION_SECOND_INSURANCE_NATURE = "* Select secondary insurance nature.";
        public const string REGISTRATION_SECOND_INSURANCE_POLICY_EMPTY = "* Secondary insurance policy number should not be empty.";
        public const string REGISTRATION_SECOND_INSURANCE_PERSON_NAME_EMPTY = "* Secondary insurance person name should not be empty.";
        public const string REGISTRATION_SECOND_INSURANCE_START_DATE_EMPTY = "* Select start date for secondary insurance.";
        public const string REGISTRATION_SECOND_INSURANCE_END_DATE_EMPTY = "* Select end date for secondary insurance.";

        public const string REGISTRATION_ADDRESS_INVALID = "* Address should not empty.";
        public const string REGISTRATION_PATIENT_DEFAULT_PASSWORD = "quivus";

        public const string REGISTRATION_DTO_INVALID = "* Registration object is Null.";

        public const string PROFILE_SETTING_DTO_INVALID = "* Profile Setting object is Null.";

        public const string REGISTRATION_PREFIX_EMPTY = "* Please provide prefix.";
        public const string REGISTRATION_ACCOUNT_STATUS_EMPTY = "* Please provide account status.";

        public const string REGISTRATION_USER_TYPE_EMPTY = "* Please provide user type.";

        public const string REGISTRATION_CLINIC_BRANCH_EMPTY = "* Please provide valid clinic branch Id.";

        public const string REGISTRATION_PATIENTID_MISSING = "* Please provide valid patient Id.";
        public const string REGISTRATION_CREATED_BY_MISSING = "* Please provide valid created by Id.";

        public const string REGISTRATION_PATIENT_RELATIONSHIP_EMPTY = "* Please provide relationship id.";

        public const string PATIENT_MARITAL_STATUS_EMPTY = "* Please provide marital status id.";

        public const string PATIENT_RACE_TYPE_EMPTY = "* Please provide race type id.";
        public const string PATIENT_ETHINICITY_TYPE_EMPTY = "* Please provide ethnicity id.";
        public const string PATIENT_OCCUPATION_TYPE_EMPTY = "* Please provide occupation id.";

        public const string PATIENT_LANGUAGE_TYPE_EMPTY = "* Please provide language id.";
        public const string PATIENT_LANGUAGE_ABILITY_MODE_EMPTY = "* Please provide language ability mode id.";
        public const string PATIENT_LANGUAGE_COUNTRY_EMPTY = "* Please provide language country id.";

        public const string REGISTRATION_GURANTOR_EMPTY = "* Patient Guarantor object is Null.";
        public const string REGISTRATION_GURANTOR_FIRST_NAME_EMPTY = "* Guarantor first name should not be empty.";
        public const string REGISTRATION_GURANTOR_FIRST_NAME_INVALID = "* Guarantor first name is invalid.";
        public const string REGISTRATION_GURANTOR_LAST_NAME_EMPTY = "* Guarantor last name should not be empty.";
        public const string REGISTRATION_GURANTOR_LAST_NAME_INVALID = "* Guarantor last name is invalid.";

        public const string REGISTRATION_EMERGENCY_FIRST_NAME_INVALID = "* Emergency contact first name is invalid.";

        public const string ADDRESS_BOOK_DTO_INVALID = "* AddressBook object is Null.";
        public const string ADDRESS_BOOK_PARTY_ID_INVALID = "* AddressBook PartyTypeId is invalid.";

        public const string PATIENT_ALLERGY_EMPTY = "* Patient Allergy object is Null.";
        public const string PATIENT_ALLERGY_TYPE_INVALID = "* Patient Allergy Id is Empty.";
        public const string PATIENT_ALLERGY_REACTION_INVALID = "* Patient Allergy Reaction is Empty.";

        public const string PATIENT_MEDICAL_HISTORY_EMPTY = "* Patient Medical History object is Null.";
        public const string PATIENT_MEDICAL_HISTORY_ICD_CODE_INVALID = "* Patient Medical History ICD CODE is Empty.";
        public const string PATIENT_MEDICAL_HISTORY_ICD_CODE_PROBLEM_INVALID = "* Patient Medical History Problem is Empty.";

        public const string PATIENT_FAMILY_HISTORY_EMPTY = "* Patient Family History object is Null.";
        public const string PATIENT_FAMILY_HISTORY_ICD_CODE_INVALID = "* Patient Family History ICD CODE is Empty.";
        public const string PATIENT_FAMILY_HISTORY_ICD_CODE_PROBLEM_INVALID = "* Patient Family History Problem is Empty.";
        public const string PATIENT_FAMILY_HISTORY_RELATION_PROBLEM_INVALID = "* Patient Family History Relation is Empty.";
        
        public static string REGISTRATION_PROVIDER_ID_EMPTY="Please provide provider Id";
        public static string REGISTRATION_SECTION_ID_EMPTY="Please provide section Id";
        public static string REGISTRATION_USER_PROFILE_CATEGORY_ID_EMPTY="Please provide category Id";
        
        public static string IMAGE_ID_EMPTY = "Image id is invalid";
        public static string IMAGE_TYPE_ID_EMPTY =  "Image type id is invalid";
        public static string IMAGE_DATA_EMPTY =  "Image data is invalid";
        public static string IMAGE_USER_ID_EMPTY =  "Image user id is invalid";

        public static string USER_ID_EMPTY="user id is invalid";
        public static string REGISTRATION_CREATED_BY_ERROR = "CreatedBy id is invalid";
        public static string REGISTRATION_MODIFIED_BY_ERROR = "ModifiedBy id is invalid";      

    }
}
