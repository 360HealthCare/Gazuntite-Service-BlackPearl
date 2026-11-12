using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusModel.Billing.Claim;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using QuivusModel.Billing.EDIPaymentTransaction;
using System.ServiceModel.Web;
using QuivusBilling.ServiceResponse.Visit;
using System.Xml;
using QuivusModel.Billing.PendingBills;

namespace QuivusBillingService
{    
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IBillingService
    {

         /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to save super bill
       /// </summary>
       /// <param name="superBillXml"></param>        
       /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SaveSuperBill")]
        bool SaveSuperBill(XmlDocument superBillXml);

         /// <summary>
       /// Author: Shahid Munir
       /// Date: 12/27/2011  
       /// to get all the E/M Code types
       /// </summary>
       /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadENMCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        ENMCodeTypeResultDTO LoadENMCodeTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all the encounter types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadEncounterTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        EncounterTypeResultDTO LoadEncounterTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadPlaceOfServiceTypeByName/{typrName}", BodyStyle = WebMessageBodyStyle.Bare)]
        PlaceOfServiceResultDTO LoadPlaceOfServiceTypeByName(string typrName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get ICD codes for super bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadICDCodeByName/{queryString}", BodyStyle = WebMessageBodyStyle.Bare)]
        ICDCodeResultDTO LoadICDCodeByName(string queryString);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get procedure modifier types
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadProcedureModifierTypes/{ModifierCode}", BodyStyle = WebMessageBodyStyle.Bare)]
        ProcedureModifierTypeResultDTO LoadProcedureModifierTypes(String ModifierCode);

        /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to add Claim transactions to a claim
       /// </summary>
       /// <param name="claimTransactionIdList"></param>        
       /// <returns></returns>
       [OperationContract]
       [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ManagePendingBillProcedures")]
       bool ManagePendingBillProcedures(XmlDocument ProcedureXml);

         /// <summary>
       /// Author: Shahid Munir
       /// Date:   01/04/2011
       /// to get CPT Medical types
       /// </summary>
       /// <param name="CPTName"></param>
       /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadCPTMedicalTypes/{CPTName}", BodyStyle = WebMessageBodyStyle.Bare)]
        CPTMedicalTypeResultDTO LoadCPTMedicalTypes(String CPTName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetProviderPendingBills/{providerId}/{clinincBranchId}", BodyStyle = WebMessageBodyStyle.Bare)]
        PendingBillsResultDTO LoadAllPendingBillsByProviderId(string providerId, string clinincBranchId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to create Claim from visit id 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "CreateClaimByVisitId/{VisitId}/{managedByUserId}", BodyStyle = WebMessageBodyStyle.Bare)]
        bool CreateClaim(string VisitId, string managedByUserId);

         /// <summary>
       /// Author: Shahid Munir
       /// Date:   01/04/2011
       /// to create Claims from visit id list 
       /// </summary>
       /// <param name="VisitIdList"></param>
       /// <returns></returns>
        //[OperationContract]
        //[WebInvoke(UriTemplate = "CreateClaimByVisitIdList/", Method = "POST")]
        //bool CreateClaim(List<string> VisitIdList, string managedByUserId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to get already created claims of Provider
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetProviderClaims/{providerId}", BodyStyle = WebMessageBodyStyle.Bare)]
        ClaimSearchResultDTO LoadClaimsByProviderId(string providerId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim data 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetClaim/{claimId}", BodyStyle = WebMessageBodyStyle.Bare)]
        ClaimDTO LoadClaimByClaimId(string claimId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim insurance data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetClaimInsurance/{claimId}", BodyStyle = WebMessageBodyStyle.Bare)]
        List<ClaimInsuranceDTO> LoadClaimInsurancesByClaimId(string claimId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/20/2011
        /// to manage claim data 
        /// </summary>
        /// <param name="claimList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaim/", Method = "POST")]
        bool ManageClaim(List<ClaimDTO> claimList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim information
        /// </summary>
        /// <param name="claimDTO"></param>
        /// <returns>Current ClaimID</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaim/", Method = "POST")]
        int InsertClaim(ClaimDTO claimDTO);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim insurance data 
        /// </summary>
        /// <param name="claimInsuranceList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaimInsurance/", Method = "POST")]
        bool ManageClaimInsurance(List<ClaimInsuranceDTO> claimInsuranceList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim insurance information
        /// </summary>
        /// <param name="claimInsuranceDTO"></param>
        /// <returns>Current ClaimInsuranceID</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaimInsurance/", Method = "POST")]
        int InsertClaimInsurance(ClaimInsuranceDTO claimInsuranceDTO);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim billing status data 
        /// </summary>
        /// <param name="claimBillingStatusList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaimBillingStatus/", Method = "POST")]
        bool ManageClaimBillingStatus(List<ClaimBillingStatusDTO> claimBillingStatusList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim billing status information
        /// </summary>
        /// <param name="claimBillingStatusDTO"></param>
        /// <returns>Current ClaimBillingStatusID</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaimBillingStatus/", Method = "POST")]
        int InsertClaimBillingStatus(ClaimBillingStatusDTO claimBillingStatusDTO);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim transaction data 
        /// </summary>
        /// <param name="claimTransactionList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaimTransaction/", Method = "POST")]
        bool ManageClaimTransaction(List<ClaimTransactionDTO> claimTransactionList);


        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to split Claim transactions to new claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "SplitClaimTransactions/{claimTransactionIdList}/{claimId}/{managedByUserId}", BodyStyle = WebMessageBodyStyle.Bare)]
        bool SplitClaimTransactions(string claimTransactionIdList, string claimId, string managedByUserId);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to remove Claim transactions to from claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "RemoveClaimTransactions/{claimTransactionIdList}/{managedByUserId}", BodyStyle = WebMessageBodyStyle.Bare)]
        bool RemoveClaimTransactions(string claimTransactionIdList, string managedByUserId);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to add Claim transactions to a claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "AddClaimTransactions/{managedByUserId}")]
        bool AddClaimTransactions(XmlDocument TransactionXml, string managedByUserId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim transaction information
        /// </summary>
        /// <param name="claimTransactionDTO"></param>
        /// <returns>Current ClaimTransactionID</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaimTransaction/", Method = "POST")]
        int InsertClaimTransaction(ClaimTransactionDTO claimTransactionDTO);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/28/2011
        /// to get claim's transactions by claim id
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetClaimTransactionsByClaimId/{claimId}", BodyStyle = WebMessageBodyStyle.Bare)]
        ClaimTransactionSearchResultDTO LoadClaimTransactionsByClaimId(string claimId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim transaction data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetClaimMissingTransactionsByVisitId/{visitId}/{claimId}/{managedByUserId}", BodyStyle = WebMessageBodyStyle.Bare)]
        ClaimTransactionSearchResultDTO LoadClaimMissingTransactionsByVisitId(string visitId, string claimId, string managedByUserId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get visit diagnosis
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetVisitDiagnosisByVisitId/{visitId}", BodyStyle = WebMessageBodyStyle.Bare)]
        string LoadVisitDiagnosisByVisitId(string visitId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim submission batch data 
        /// </summary>
        /// <param name="claimSubmissionBatchDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaimSubmissionBatch/", Method = "POST")]
        int InsertClaimSubmissionBatch(ClaimSubmissionBatchDTO claimSubmissionBatchDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim submission batches  
        /// </summary>
        /// <param name="claimSubmissionBatchList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaimSubmissionBatch/", Method = "POST")]
        bool ManageClaimSubmissionBatch(List<ClaimSubmissionBatchDTO> claimSubmissionBatchList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimPaymentDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaimPayment/", Method = "POST")]
        int InsertClaimPayment(ClaimPaymentsDTO claimPaymentDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim payments  
        /// </summary>
        /// <param name="claimPaymentsList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaimPayments/", Method = "POST")]
        bool ManageClaimPayments(List<ClaimPaymentsDTO> claimPaymentsList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimAdjustmentDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateClaimAdjustment/", Method = "POST")]
        int InsertClaimAdjustment(ClaimAdjustmentDTO claimAdjustmentDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim adjustments  
        /// </summary>
        /// <param name="claimAdjustmentList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageClaimAdjustments/", Method = "POST")]
        bool ManageClaimAdjustments(List<ClaimAdjustmentDTO> claimAdjustmentList);

        #region EDI Payment Transaction Methods

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/28/2011
        /// to manage Payment adjustment mapping data 
        /// </summary>
        /// <param name="adjustmentMappingList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageAdjustmentMapping/", Method = "POST")]
        bool ManageAdjustmentMapping(List<AdjustmentMappingDTO> adjustmentMappingList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim data 
        /// </summary>
        /// <param name="adjustmentMappingDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateAdjustmentMapping/", Method = "POST")]
        int InsertAdjustmentMapping(AdjustmentMappingDTO adjustmentMappingDTO);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeDTO"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateAdjustmentReasonCode/", Method = "POST")]
        int InsertAdjustmentReasonCode(AdjustmentReasonCodeDTO adjustmentReasonCodeDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to Insert/Update/Delete Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ManageAdjustmentReasonCodes/", Method = "POST")]
        bool ManageAdjustmentReasonCodes(List<AdjustmentReasonCodeDTO> adjustmentReasonCodeList);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to load all Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAdjustmentReasonCodes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<AdjustmentReasonCodeDTO> LoadAdjustmentReasonCodes();

        #endregion

        #region Population of Type DTOs

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment soyrce types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentSourceTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentSourceTypeDTO> GetAllPaymentSourceTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment  types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentTypeDTO> GetAllPaymentTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all accept assigmnet types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAcceptAssignmentTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<AcceptAssignmentTypeDTO> GetAllAcceptAssignmentTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all adjustment group code types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAdjustmentGroupCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<AdjustmentGroupCodeTypeDTO> GetAllAdjustmentGroupCodeTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all billing mode types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetBillingModeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<BillingModeTypeDTO> GetAllBillingModeTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all billing status types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetBillingStatusTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<BillingStatusTypeDTO> GetAllBillingStatusTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim frequency code types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadFrequencyCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        FrequencyCodeTypeResultDTO LoadFrequencyCodeTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim status category types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetClaimStatusCategoryTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<ClaimStatusCategoryTypeDTO> GetAllClaimStatusCategoryTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim status code types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetClaimStatusCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<ClaimStatusCodeTypeDTO> GetAllClaimStatusCodeTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all CMS secondary types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetCMSSecondaryTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<CMSSecondaryTypeDTO> GetAllCMSSecondaryTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all communication types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetCommunicationTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<CommunicationTypeDTO> GetAllCommunicationTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all delay reason code types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetDelayReasonCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<DelayReasonCodeTypeDTO> GetAllDelayReasonCodeTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all EDI receiver types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetEDIReceiverTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<EDIReceiverTypeDTO> GetAllEDIReceiverTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all enrollment types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetEnrollmentTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<EnrollmentTypeDTO> GetAllEnrollmentTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all hospice employed types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetHospiceEmployedTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<HospiceEmployedTypeDTO> GetAllHospiceEmployedTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all media types (paper or electronic)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetMediaTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<MediaTypeDTO> GetAllMediaTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all claim amount qualifier types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentClaimAmountQualifierTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentClaimAmountQualifierTypeDTO> GetAllPaymentClaimAmountQualifierTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment level types (procedure or claim)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentLevelTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentLevelTypeDTO> GetAllPaymentLevelTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment method code types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentMethodCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentMethodCodeTypeDTO> GetAllPaymentMethodCodeTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment peocedure amount qualifier types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentProcedureAmountQualifierTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentProcedureAmountQualifierTypeDTO> GetAllPaymentProcedureAmountQualifierTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all payment procedure qualifier types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPaymentProcedureQualifierTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<PaymentProcdureQualifierTypeDTO> GetAllPaymentProcdureQualifierTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all provider adjustment types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetProviderAdjustmentTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<ProviderAdjustmentTypeDTO> GetAllProviderAdjustmentTypes();

         /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all statement types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetStatementTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<StatementTypeDTO> GetAllStatementTypes();

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all accept assigmnet types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetTransactionHandlingCodeTypes/", BodyStyle = WebMessageBodyStyle.Bare)]
        List<TransactionHandlingCodeTypeDTO> GetAllTransactionHandlingCodeTypes();

        #endregion

    }
}
