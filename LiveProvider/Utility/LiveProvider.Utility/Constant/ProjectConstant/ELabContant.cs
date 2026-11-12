using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Constant.ProjectConstant
{
    public class ELabContant
    {
        public static string GENERIC_DTO_CONVERSION_ERROR_MSG = "GenericDataReaderToDTOConverter";

        public static DateTime DATE_MINIMUM = new DateTime(1901, 01, 01, 12, 0, 0);

        public static string INVALID_ARGUMENT_EXCEPTION_MSG = "Invalid Arguments.";

        public static string RECORD_NOT_INSERTED_ERROR_MSG = "Record Not Inserted";

        public static string SELECTION_NULL = "null";
        public static string SELECTION_TRUE = "true";
        public static string SELECTION_False = "false";

        public static string PARAMETER_POPULATOR_ERROR_MSG { get; set; }

        public static string STATION_IDS_TVP = "StationIdsTVP";

        public static string PROPERTY_VISIT_ID = "VisitID";

        public static string PROPERTY_DICTATION_ID = "DictationID";

        public static string PROPERTY_CREATED_BY = "CreatedBy";

        public static string PROPERTY_CREATED_AT = "CreatedAt";



        public static string PROPERTY_MODIFIED_BY = "ModifiedBy";

        public static string PROPERTY_MODIFIED_AT = "ModifiedAt";

        public static string PATIENT_LABS_DTO_CONVERSION_ERROR_MSG = "Error converting patient labs information";

        public static string PATIENT_ID_INVALID_ERROR_MSG = "Invalid Patient ID.";


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

        public static string CLINIC_ID_INVALID_ERROR_MSG = "Invalid clinic branch ID.";

        public static string PAGE_NO_INVALID_ERROR_MSG = "Invalid page number.";

        public static string VISIT_ID_INVALID_ERROR_MSG = " Visit Id is invalid";

        public static string FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG = "Error converting patient FollowupLabs DTO";

        public static int PAGE_SIZE = 200;

        public static string ERROR_MSG = "Error";

        public static string SUCESS_MSG = "Success";

        public static string PATIENT_ORDERNUMBER_TVP = "[dbo].[OrderNumberTVP]";

        public static string LABORDER_PROCESS_SUCESS_MSG = "Order(s) are processed sucessfully";

        public static string FIELD_ID_INVALID_ERROR_MSG = "Invalid field id.";

        public static int PATIENT_SEARCH_FIELD_ID = 3;

        public static int PROVIDER_SEARCH_FIELD_ID = 5;

        public static int DATE_SEARCH_FIELD_ID = 2;

        public static int ALL_TEST_TYPE_ID = 8;
    }
}
