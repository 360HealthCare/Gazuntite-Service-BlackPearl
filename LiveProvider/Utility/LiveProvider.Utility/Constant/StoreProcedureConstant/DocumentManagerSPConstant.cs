using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Constant.StoreProcedureConstant
{
    /// <summary>
    /// Author Muhammad Ismail,Sahibzada Sami ud Din
    /// Date 04 July 2011
    /// This Class will conatin the store procedure Names used in document Manager Class
    /// </summary>
    public class DocumentManagerSPConstant
    {
        public static string DOCUMENT_TYPE_LOAD_SP = "[type.PatientDocumentType_LoadAll]";
        public static string DOCUMENT_SECTION_TYPE_LOAD_SP = "[type.DocumentSection_LoadAll]";
        public static string DOCUMENT_PATIENT_INSERT_SP = "[PatientDocument_Insert]";
        public static string DOCUMENT_PATIENT_UPDATE_SP = "[PatientDocument_Update]";
        public static string DOCUMENT_PATIENT_DELETE_SP = "[PatientDocument_Delete]";
        public static string DOCUMENT_PATIENT_LOAD_BY_CLINIC_ID = "[PatientDocument_LoadByClinicBranchID]";
        public static string DOCUMENT_SERVERFILE_UPDATE_SP = "[PatientDocumentServerPath_Update]";
        public static string DOCUMENT_PATIENT_SEARCH = "[PatientDocument_SEARCH]";

        public static string DOCUMENT_PATIENT_LOAD_SEARCH_FIELD = "[PatientDocument_LoadSearchField]";
        public static string DOCUMENT_PATIENT_LOAD_BY_CLINICID_AND_PATIENTID_SP = "[PatientDocument_LoadByClinicBranchIDAndPatientID]";

        public static string DOCUMENT_PATIENT_FILTER = "[PatientDocument_Filter]";

        public static string DOCUMENT_PATIENT_DATE_FILTER = "[PatientDocument_DateFilter]";

        
        public static string DOCUMENT_PATIENT_SECTION_FILTER = "[PatientDocumentSection_Filter]";

        public static string DOCUMENT_PATIENT_Date_SEARCH = "[PatientDocument_DateSearch]";
    }
}
