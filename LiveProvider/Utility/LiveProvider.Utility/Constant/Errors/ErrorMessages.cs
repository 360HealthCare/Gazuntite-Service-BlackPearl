using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Constant.Errors
{
    public class ErrorMessages
    {
        public static string INVALID_DOCUMENTID_ERROR_MSG = "DocumentID id is invalid";
        public static string INVALID_PATIENTID_ERROR_MSG = "PatientID id is invalid";
        public static string INVALID_DOCUMENTNAME_ERROR_MSG = "DocumentName id is invalid";
        public static string INVALID_SERVERFILEPATH_ERROR_MSG = "ServerFilePath id is invalid";
        public static string INVALID_CLIENTFILEPATH_ERROR_MSG = "ClientFilePath id is invalid";
        public static string INVALID_DOCUMENRTYPEID_ERROR_MSG = "DocumentTypeId id is invalid";
        public static string INVALID_SESSIONID_ERROR_MSG = "SessionId is invalid";
        public static string INVALID_UPLOADEDBYUSERID_ERROR_MSG = "UploadedByUserId id is invalid";
        public static string INVALID_UPLOADEDBYUSERTYPEID_ERROR_MSG = "UploadedByUserTypeId id is invalid";
        public static string INVALID_ISDOCUMENTFILEUPLOADED_ERROR_MSG = "IsDocumentFileUploaded id is invalid";
        public static string INVALID_CREATEDBY_ERROR_MSG = "CreatedBy id is invalid";
        public static string INVALID_CREATEDAT_ERROR_MSG = "CreatedAt id is invalid";
        public const string  VISIT_DATE_INVALID_ERROR_MSG = "*date is Null.";
        public static string PATIENTDOCUMENT_OBJECT_NULL_MSG = "PatientDocumentDTO object is null";
        public static string INVALID_PAGENO_ERROR_MSG = "PageNo paramater is invalid";
        public static string INVALID_PAGESIZE_ERROR_MSG = "Page size is invalid";
        public static string INVALID_FILECONTENT_ERROR_MSG = "File content is null";
        public static string INVALID_DOCUMENT_VIRTUAL_PATH_ERROR_MSG = "documentVirtualName is invalid";
        public const string CLINIC_BRANCH_ID_INVALID_ERROR_MSG = "Invalid Clinic Branch ID.";
        public const string PROFILE_ID_INVALID_ERROR_MSG = "Invalid Profile ID.";
        public const string USER_TYPE_INVALID_ERROR_MSG = "Invalid User Type ID.";
        public const string DEMAND_PAGE_NO_INVALID_ERROR_MSG = "Invalid Demand Page No.";
        public const string SEARCH_STRING_INVALID_ERROR_MSG = "Invalid Search Criteria.";
        public const string INVALID_DOCUMENT_VIRTUAL_NAME = "Invalid DocumentVirtualName.";

        public static string INVALID_PATIENT_FIRST_NAME_MSG = "Invalid Patient first name";


        public static string INVALID_PATIENT_UPLOADED_BY_USER_FIRST_NAME_MSG = "Invalid Uploaded by user first name";

        public static string INVALID_FIELDID_ERROR_MSG = "Invalid Field id value";

        public static string INVALID_SEARCHTEXT_ERROR_MSG = "Invalid search text value";

        //M
        public const string GENERIC_DTO_CONVERSION_ERROR_MSG = "GenericDataReaderToDTOConverter";

        public const string DATAREADER_DTO_CONVERSION_ERROR_MSG = "DataReaderToDTOConverter";

        public enum ERROR_CODE
        {
            InvalidArguments,            
            GenericDTOConversion,
            DataReaderToDTOConversion
        }
        public static string LABS_INVALID_VISIT_ID_ERROR_MSG = "Invalid visit id in Labs";

        public static string LABS_INVALID_MODIFIEDBY_ERROR_MSG = "Invalid modified by id in Labs";

        public static string LABS_INVALID_CREATEDBY_ERROR_MSG = "Invalid created by id in Labs";

        

        public static string INVALID_LAB_ONSET_ERROR_MSG = "Lab OnSet date is invalid";

        public static string INVALID_LAB_TEST_ID_ERROR_MSG = "Invalid lab test type id";

        public static string INVALID_LAB_LIONIC_TEST_TYPE_ID_ERROR_MSG = "Invalid lab lionic test type id";

        public static string INVALID_LAB_TEST_ERROR_MSG = "Lab test type is invalid";

        public static string INVALID_LAB_LIONIC_TEST_TYPE_ERROR_MSG = "Invalid lab test type id";

        public static string LABS_DTO_INVALID_ERROR_MSG = "Lab DTO is invalid";
    }
}
