using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusUtility.ProjectConstants
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:  12/27/2011
    /// </summary>
    public class BillingConstant
    {

        #region Super Bill E/M Service Charges

        public const string VISIT_COUNT_CHRONIC_DIAGNOSIS = "Visit_countVisitChronicDiagnosis";
        public const string VISIT_COUNT_HPI = "Visit_countVisitHPI";
        public const string VISIT_HISTORY_EXAMINATION_DECISION_LEVEL_COUNTS_SP = "Visit_HEDLevelCounts";

        #region Enumerators

        public enum SuperBillHistoryLevelScales
        { NA = 1, Focused, Expanded, Detailed, Comprehensive }

        public enum SuperBillExaminationLevelScales
        { NA = 1, Focused, Expanded, Detailed, Comprehensive }

        public enum SuperBillDecisionLevelScales
        { NA = 1, StraightForward, Low, Moderate, High }

        public enum EncounterType
        { New = 1, Established }

        #endregion

        #endregion

        #region Visit Service related constant

        public const string PLACE_OF_SERVICE_TYPE_LOAD_ALL_SP = "[type.PlaceOfService_LoadAll]";
        public const string PLACE_OF_SERVICE_TYPE_LOAD_BY_NAME_SP = "[type.PlaceOfService_LoadByNameAutoComplete]";

        public const string PROCEDURE_MODIFIER_TYPE_LOAD_SP = "[type.CPTModifier_LoadByCodeAutoComplete]";

        public const string PROCEDURE_MODIFIER_LOAD_BY_PATIENT_PROCEDURE_ID_SP = "PatientProcedure_ModifierLoadByPatientProcedureId";

        public const string PATIENT_PROCEDURE_LOAD_SP = "QBPatientProcedure_LoadByVisit";
        public const string VISIT_LOAD_ALL_SIGNED_PENDING_BILLS_SP = "Visit_LoadAllSignedPendingBills";

        public const string VISIT_MARK_CLAIM_CREATED_SP = "Visit_MarkAsClaimCreated";
        public const string VISIT_MARK_LIST_CLAIM_CREATED_SP = "Visit_MarkListAsClaimCreated";
        public const string PATIENT_DIAGNOSIS_MARK_CREATED_FROM_BILLING_SP = "PatientDiagnosis_CreatedFromBilling";
        public const string PATIENT_DIAGNOSIS_PROCEDURE_MARK_INCLUDE_IN_BILLING_SP = "PatientDiagnosisProcedure_IncludeInBilling";
        public const string GENERAL_ID_TVP = "GeneralIdsTVP";

        public static string VISIT_DIAGNOSIS_MANAGE_SP = "VisitDiagnosis_InsertUpdateDelete";
        public static string VISIT_PROCEDURE_MANAGE_SP = "VisitProcedure_TVPAddUpdateDelete";
        public static string VISIT_PROCEDURE_TVP = "VisitProcedureTVP";

        public static string PATIENT_PROCEDURE_MODIFIER_MANAGE_SP = "PatientProcedureModifier_TVPAddUpdateDelete";
        public static string PATIENT_PROCEDURE_MODIFIER_TVP = "PatientProcedureModifierTVP";

        #endregion
                
        #region User Service related constant

        public const string PATIENT_QUIVUS_IDS_LOAD_BY_PATIENT_IDS_SP = "Patient_QuivusIDsLoadByPatientIDs";
        public const string PATIENT_Billing_INFO_LOAD_BY_PATIENT_ID_SP = "Patient_BillingInfoLoadByPatientID";       

        #endregion


        public static string CLAIM_ENM_TVP = "ClaimENMServiceTVP";
        public static string CLAIM_CMS_QUESTION_INSERT_SP = "ClaimCMSQuestions_Insert";
        public static string CLAIM_ENM_CODE_INSERT_SP = "ClaimENM_Insert";
        public static string CLAIM_WRS_TVP = "ClaimWRSTVP";
        public static string CLAIM_WRS_INSERT_SP = "ClaimWRS_Insert";
        public const string CLAIM_WRS_LOAD_BY_CLAIMID_SP = "ClaimWorkServices_LoadByClaimID";
        public const string CLAIM_SEARCH_SP = "Claim_Search";
        public const string CLAIM_DATA_LOAD_BY_CLAIMID_SP = "Claim_LoadByID";
        public const string CLAIM_INSERT_SP = "Claim_Insert";
        public const string CLAIM_MANAGE_SP = "Claim_InsertUpdateDelete";
        public const string CLAIMS_TVP = "ClaimsTVP";

        public const string CLAIM_SUBMISSION_BATCH_INSERT_SP = "ClaimSubmissionBatch_Insert";
        public const string CLAIM_SUBMISSION_BATCH_MANAGE_SP = "ClaimSubmissionBatch_InsertUpdateDelete";
        public const string CLAIM_SUBMISSION_BATCH_TVP = "ClaimSubmissionBatchTVP";

        public const string CLAIM_PAYMENTS_INSERT_SP = "ClaimPayments_Insert";
        public const string CLAIM_PAYMENTS_MANAGE_SP = "ClaimPayments_InsertUpdateDelete";
        public const string CLAIM_PAYMENTS_TVP = "ClaimPaymentsTVP";

        public const string CLAIM_ADJUSTMENTS_INSERT_SP = "ClaimAdjustments_Insert";
        public const string CLAIM_ADJUSTMENTS_MANAGE_SP = "ClaimAdjustments_InsertUpdateDelete";
        public const string CLAIM_ADJUSTMENTS_TVP = "ClaimAdjustmentsTVP";

        public const string CLAIM_INSURANCE_INSERT_SP = "ClaimInsurance_Insert";
        public const string CLAIM_INSURANCE_DATA_LOAD_BY_CLAIMID_SP = "ClaimInsurance_LoadByClaimID";
        public const string CLAIM_INSURANCE_MANAGE_SP = "ClaimInsurance_InsertUpdateDelete";
        public const string CLAIM_INSURANCE_TVP = "ClaimInsuranceTVP";

        public const string CLAIM_BLLING_STATUS_INSERT_SP = "ClaimBillingStatus_Insert";
        public const string CLAIM_BILLING_STATUS_MANAGE_SP = "ClaimBillingStatus_InsertUpdateDelete";
        public const string CLAIM_BILLING_STATUS_TVP = "ClaimBillingStatusTVP";

        public const string CLAIM_TRANSACTIONS_INSERT_SP = "ClaimTransactions_Insert";
        public const string CLAIM_TRANSACTIONS_DATA_LOAD_BY_CLAIMID_SP = "ClaimTransactions_LoadByClaimID";
        public const string CLAIM_TRANSACTIONS_SEARCH_SP = "ClaimTransactions_Search";
        public const string CLAIM_TRANSACTIONS_MANAGE_SP = "ClaimTransactions_InsertUpdateDelete";
        public const string CLAIM_TRANSACTIONS_TVP = "ClaimTransactionsTVP";

        public const string CLAIM_TRANSACTIONS_SPLIT_SP = "ClaimTransactions_Split";
        public const string CLAIM_TRANSACTIONS_SPLIT_TVP = "ClaimTransactionsSplitTVP";
        

        public const string CLAIM_DTO_CONVERSION_ERROR_MSG = "Error converting Data Reader to DTO";
        public const string CLAIM_TABLE_CONVERSION_ERROR_MSG = "Error converting claim information to Data Table";

        #region Error Codes

        public enum ERROR_CODE
        {
            InvalidArguments,
            ClaimDTOConversion,
            ServiceUnavaliableError,
            RecordNotInserted

        }

        #endregion

        #region EDI Payment Transaction Constants

        public const string ADJUSTMENT_MAPPING_INSERT_SP = "AdjustmentMapping_Insert";
        public const string ADJUSTMENT_MAPPING_LOAD_ALL_SP = "AdjustmentMapping_LoadAll";
        public const string ADJUSTMENT_MAPPING_MANAGE_SP = "AdjustmentMapping_InsertInsertUpdateDelete";
        public const string ADJUSTMENT_MAPPING_TVP = "AdjustmentMappingTVP";

        public const string ADJUSTMENT_REASON_CODES_INSERT_SP = "AdjustmentReasonCodes_Insert";
        public const string ADJUSTMENT_REASON_CODES_LOAD_ALL_SP = "AdjustmentReasonCodes_LoadAll";
        public const string ADJUSTMENT_REASON_CODES_MANAGE_SP = "AdjustmentReasonCodes_InsertInsertUpdateDelete";
        public const string ADJUSTMENT_REASON_CODES_TVP = "AdjustmentReasonCodesTVP";

        #region EDI Control Numbers

        public const string ISA_CONTROL_NUMBERS_INSERT_SP = "ISAControlNumbers_Insert";
        public const string ISA_CONTROL_NUMBERS_BULK_INSERT_SP = "ISAControlNumbers_BulkInsert";
        public const string ISA_CONTROL_NUMBERS_TVP = "ISAControlNumbersTVP";

        public const string GS_CONTROL_NUMBERS_INSERT_SP = "GSControlNumbers_Insert";
        public const string GS_CONTROL_NUMBERS_BULK_INSERT_SP = "GSControlNumbers_BulkInsert";
        public const string GS_CONTROL_NUMBERS_TVP = "GSControlNumbersTVP";

        public const string ST_CONTROL_NUMBERS_INSERT_SP = "STControlNumbers_Insert";
        public const string ST_CONTROL_NUMBERS_BULK_INSERT_SP = "STControlNumbers_BulkInsert";
        public const string ST_CONTROL_NUMBERS_TVP = "STControlNumbersTVP";

        public const string BHT_CONTROL_NUMBERS_INSERT_SP = "BHTControlNumbers_Insert";
        public const string BHT_CONTROL_NUMBERS_BULK_INSERT_SP = "BHTControlNumbers_BulkInsert";
        public const string BHT_CONTROL_NUMBERS_TVP = "BHTControlNumbersTVP";

        #endregion

        #endregion

        #region Billing Types

        public const string ENM_CODE_TYPE_LOAD_ALL_SP = "[type.ENMCodeType_LoadAll]";
        public const string PAYMENT_SOURCE_TYPE_LOAD_ALL_SP = "[type.PaymentSourceTypes_LoadAll]";
        public const string PAYMENT_TYPE_LOAD_ALL_SP = "[type.PaymentTypes_LoadAll]";
        public const string ACCEPT_ASSIGNMENT_TYPE_LOAD_ALL_SP = "[type.AcceptAssignmentTypes_LoadAll]";
        public const string ADJUSTMENT_GROUP_CODE_TYPE_LOAD_ALL_SP = "[type.AdjustmentGroupCodeTypes_LoadAll]";
        public const string BILLING_MODE_TYPE_LOAD_ALL_SP = "[type.BillingModeTypes_LoadAll]";
        public const string BILLING_STATUS_TYPE_LOAD_ALL_SP = "[type.BillingStatusTypes_LoadAll]";
        public const string CLAIM_FREQUENCY_CODE_TYPE_LOAD_ALL_SP = "[type.ClaimFrequencyCodeType_LoadAll]";
        public const string CLAIM_STATUS_CATEGORY_TYPE_LOAD_ALL_SP = "[type.ClaimStatusCategoryTypes_LoadAll]";
        public const string CLAIM_STATUS_CODE_TYPE_LOAD_ALL_SP = "[type.ClaimStatusCodeTypes_LoadAll]";
        public const string CMS_SECONDARY_TYPE_LOAD_ALL_SP = "[type.CMSSecondaryTypes_LoadAll]";
        public const string COMMUNICATION_TYPE_LOAD_ALL_SP = "[type.CommunicationTypes_LoadAll]";
        public const string DELAY_REASON_CODE_TYPE_LOAD_ALL_SP = "[type.DelayReasonCodeTypes_LoadAll]";
        public const string EDI_RECEIVER_TYPE_LOAD_ALL_SP = "[type.EDIReceiverTypes_LoadAll]";
        public const string ENROLLMENT_TYPE_LOAD_ALL_SP = "[type.EnrollmentTypes_LoadAll]";
        public const string HOSPICE_EMPLOYED_TYPE_LOAD_ALL_SP = "[type.HospiceEmployedTypes_LoadAll]";
        public const string MEDIA_TYPE_LOAD_ALL_SP = "[type.MediaTypes_LoadAll]";
        public const string PAYMENT_CLAIM_AMOUNT_QUALIFIER_TYPE_LOAD_ALL_SP = "[type.PaymentClaimAmountQualifierTypes_LoadAll]";
        public const string PAYMENT_LEVEL_TYPE_LOAD_ALL_SP = "[type.PaymentLevelTypes_LoadAll]";
        public const string PAYMENT_METHOD_CODE_TYPE_LOAD_ALL_SP = "[type.PaymentMethodCodeTypes_LoadAll]";
        public const string PAYMENT_PROCDURE_QUALIFIER_TYPE_LOAD_ALL_SP = "[type.PaymentProcdureQualifierTypes_LoadAll]";
        public const string PAYMENT_PROCEDURE_AMOUNT_QUALIFIER_TYPE_LOAD_ALL_SP = "[type.PaymentProcedureAmountQualifierTypes_LoadAll]";
        public const string PROVIDER_ADJUSTMENT_TYPE_LOAD_ALL_SP = "[type.ProviderAdjustmentTypes_LoadAll]";
        public const string STATEMENT_TYPE_LOAD_ALL_SP = "[type.StatementTypes_LoadAll]";
        public const string TRANSACTION_HANDLING_CODE_TYPE_LOAD_ALL_SP = "[type.TransactionHandlingCodeTypes_LoadAll]";

        #endregion

    }
}
