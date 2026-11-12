using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Constant.StoreProcedureConstant
{
    public class ELabSPConstant
    {
        public static string LAB_NATURE_TYPE_LOAD_SP = "[type.LabTestNatureType_LoadAll]";

        public static string LAB_TEST_PRIORITY_TYPE_LOAD_SP = "[type.LabTestPriorityType_LoadAll]";

        public static string LAB_TEST_SPECIMENT_TYPE_LOAD_SP = "[type.LabTestSpecimenType_LoadAll]";

        public static string LAB_TEST_TYPE_LOAD_SP = "[type.LabTestType_LoadAll]";

        public static string LAB_TYPE_LOAD_SP = "[type.LabType_LoadAll]";

        public static string LAB_TEST_ORDER_METHOD_TYPE_LOAD_SP = "[type.LabType_LoadAll]";

        public static string ICD_CODE_BY_NAME_LOAD_SP = "[type.ICDCode_LoadByICDCodeNameAutoComplete]";

        public static string LIONICLAB_TEST_TYPE_LOAD_SP = "[type.LIONICLabTestType_LoadBySearchString]";

        public static string LABS_INSERT_SP = "PatientLab_Insert";

        public static string LABS_UPDATE_SP = "PatientLab_Update";

        public static string PATIENT_LABS_LOAD_BY_PATIENT_ID_SP = "PatientLab_LoadByPatientANDClinicBranch";

        public static string ELAB_CONVERSION_ERROR_MSG = "Error converting station ids list";

        public static string PATIENT_LABS_LOAD_BY_VISIT_ID_SP = "PatientLab_LoadByVisit";

        public static string PATIENT_LOAD_ALL_LABS = "PatientLab_LoadAllByPatientId";

        public static string FOLLOWUP_LABS_LOAD_BY_PATIENT_SP = "[PatientLabForFollowup_LoadByPatient]";

        public static string LOAD_ALL_LAB_ORDER_BY_CINIC_BRANCH = "[dbo].[LabOrder_LoadAllByClinicBranchID]";

        public static string LOAD_ALL_SEARCH_FIELDS = "[dbo].[type.SearchFields_LoadAll]";

        public static string LOAD_ALL_LAB_RESULT_BY_CINIC_BRANCH = "[dbo].[LabResult_LoadAllByClinicBranchID]";

        public static string LABORDER_PROCESS_BYDATE = "[dbo].[LabOrder_ProcessByDate]";

        public static string LABORDER_PROCESS_BY_ORDERNUMBERS = "[dbo].[LabOrder_ProcessByOrderNumbers]";

        public static string SEARCH_ORDER_BY_SEARCHSTRING_IDS_SP = "LabOrder_SearchByClinicBranchIDAndSearchText";

        public static string SEARCH_RESULT_BY_SEARCHSTRING_IDS_SP = "LabResult_SearchByClinicBranchIDAndSearchText";

        public static string SEARCH_ORDER_BY_PATIENT_OR_PROVIDER_IDS_SP = "LabOrder_SearchByClinicBranchIDAndPatientOrProviderIDs";

        public static string SEARCH_RESULT_BY_PATIENT_OR_PROVIDER_IDS_SP = "LabResult_SearchByClinicBranchIDAndPatientOrProviderIDs";

        public static string FILTER_ORDER_BY_CLINIC_BRANCH_SP = "[dbo].[LabOrder_FilterByClinicBranchIDAndCriteria]";

        public static string FILTER_RESULT_BY_CLINIC_BRANCH_SP = "[dbo].[LabResult_FilterByClinicBranchIDAndCriteria]";

        public static string PROCESS_ORDER_BY_FILTER_SP = "[dbo].[LabOrder_ProcessByFilter]";

        public static string UPDATE_LAB_ORDER_SP = "[dbo].[LabOrder_Update]";

        public static string DELETE_LAB_ORDER_SP = "[dbo].[PatientLab_Delete]";

        public static string SIGN_LABS_BY_VISIT = "[dbo].[PatientLab_Signed_BY_VisitID]";
    }
}
