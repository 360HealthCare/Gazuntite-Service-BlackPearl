using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
//using System.ServiceModel;
using System.Text;
using QuivusModel.Billing.Claim;
using QuivusModel.Billing.EDIPaymentTransaction;
using System.Xml;
using QuivusBilling.ServiceResponse.Visit;
using QuivusModel.Billing.PendingBills;

namespace Quivus.Billing.Business.Claim
{
    
   /// <summary>
   /// Author: Shahid Munir
   /// Date:   12/19/2011
   /// 
   /// </summary>
    public interface IBillingManager
    {

        /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to save super bill
       /// </summary>
        /// <param name="superBillXml"></param>        
       /// <returns></returns>
       bool SaveSuperBill(XmlDocument superBillXml);

        
        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all the encounter types
        /// </summary>
        /// <returns></returns>
        EncounterTypeResultDTO LoadEncounterTypes();

        /// <summary>
       /// Author: Shahid Munir
       /// Date: 12/27/2011  
       /// to get all place of service types
       /// </summary>
       /// <returns></returns>
        PlaceOfServiceResultDTO LoadPlaceOfServiceTypeByName(string typrName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get ICD codes for super bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        ICDCodeResultDTO LoadICDCodeByName(string queryString);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get procedure modifier types
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        ProcedureModifierTypeResultDTO LoadProcedureModifierTypes(String ModifierCode);

        /// <summary>
       /// Author:  Shahid Munir
       /// Date:    01/10/2011
       /// to add Claim transactions to a claim
       /// </summary>
       /// <param name="claimTransactionIdList"></param>        
       /// <returns></returns>
        bool ManagePendingBillProcedures(XmlDocument ProcedureXml);

        /// <summary>
       /// Author: Shahid Munir
       /// Date:   01/04/2011
       /// to get CPT Medical types
       /// </summary>
       /// <param name="CPTName"></param>
       /// <returns></returns>
       CPTMedicalTypeResultDTO LoadCPTMedicalTypes(String CPTName);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to get signed visits data for which claims not created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        PendingBillsResultDTO LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/04/2011
        /// to create Claim from visit id 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        bool CreateClaim(int VisitId, int managedByUserId, int encounterTypeId, int placeOfServiceTypeId);

        /// <summary>
       /// Author: Shahid Munir
       /// Date:   01/04/2011
       /// to create Claims from visit id list 
       /// </summary>
       /// <param name="VisitIdList"></param>
       /// <returns></returns>
        bool CreateClaim(List<int> VisitIdList, int managedByUserId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/19/2011
        /// to populate provider's claims
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>

        List<ClaimSearchDTO> LoadClaimsByProviderId(int providerId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim data 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        ClaimDTO LoadClaimByClaimId(int claimId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim insurance data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        List<ClaimInsuranceDTO> LoadClaimInsurancesByClaimId(int claimId);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/20/2011
        /// to manage claim data 
        /// </summary>
        /// <param name="claimList"></param>
        /// <returns></returns>
        bool ManageClaim(List<ClaimDTO> claimList);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/20/2011
        /// to insert claim data 
        /// </summary>
        /// <param name="claimDTO"></param>
        /// <returns></returns>        
        int  InsertClaim(ClaimDTO claimDTO);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim insurance data 
        /// </summary>
        /// <param name="claimInsuranceList"></param>
        /// <returns></returns>        
        bool ManageClaimInsurance(List<ClaimInsuranceDTO> claimInsuranceList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim insurance information
        /// </summary>
        /// <param name="claimInsuranceDTO"></param>
        /// <returns>Current ClaimInsuranceID</returns>        
        int InsertClaimInsurance(ClaimInsuranceDTO claimInsuranceDTO);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim billing status data 
        /// </summary>
        /// <param name="claimBillingStatusList"></param>
        /// <returns></returns>
        bool ManageClaimBillingStatus(List<ClaimBillingStatusDTO> claimBillingStatusList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim billing status information
        /// </summary>
        /// <param name="claimBillingStatusDTO"></param>
        /// <returns>Current ClaimBillingStatusID</returns>
        int InsertClaimBillingStatus(ClaimBillingStatusDTO claimBillingStatusDTO);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/26/2011
        /// to manage claim transaction data 
        /// </summary>
        /// <param name="claimTransactionList"></param>
        /// <returns></returns>
        bool ManageClaimTransaction(List<ClaimTransactionDTO> claimTransactionList);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to split Claim transactions to new claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>
        /// <param name="claimId"></param>
        /// <returns></returns>
        bool SplitClaimTransactions(List<int> claimTransactionIdList, int claimId, int managedByUserId);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to remove Claim transactions to from claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        bool RemoveClaimTransactions(string claimTransactionIdList, int managedByUserId);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to add Claim transactions to a claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>        
        /// <returns></returns>
        bool AddClaimTransactions(XmlDocument TransactionXml, int managedByUserId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/26/2011
        /// to insert claim transaction information
        /// </summary>
        /// <param name="claimTransactionDTO"></param>
        /// <returns>Current ClaimTransactionID</returns>
        int InsertClaimTransaction(ClaimTransactionDTO claimTransactionDTO);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/28/2011
        /// to get claim's transactions by claim id
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        List<ClaimTransactionDTO> LoadClaimTransactionsByClaimId(int claimId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim transaction data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        List<ClaimTransactionDTO> LoadClaimMissingTransactionsByVisitId(int visitId, int claimId, int managedByUserId);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim submission batch data 
        /// </summary>
        /// <param name="claimSubmissionBatchDTO"></param>
        /// <returns></returns>
        int InsertClaimSubmissionBatch(ClaimSubmissionBatchDTO claimSubmissionBatchDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim submission batches  
        /// </summary>
        /// <param name="claimSubmissionBatchList"></param>
        /// <returns></returns>
        bool ManageClaimSubmissionBatch(List<ClaimSubmissionBatchDTO> claimSubmissionBatchList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimPaymentDTO"></param>
        /// <returns></returns>
        int InsertClaimPayment(ClaimPaymentsDTO claimPaymentDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim payments  
        /// </summary>
        /// <param name="claimPaymentsList"></param>
        /// <returns></returns>
        bool ManageClaimPayments(List<ClaimPaymentsDTO> claimPaymentsList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimAdjustmentDTO"></param>
        /// <returns></returns>
        int InsertClaimAdjustment(ClaimAdjustmentDTO claimAdjustmentDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim adjustments  
        /// </summary>
        /// <param name="claimAdjustmentList"></param>
        /// <returns></returns>
        bool ManageClaimAdjustments(List<ClaimAdjustmentDTO> claimAdjustmentList);

        #region EDI Payment Transaction Methods
        
        /// <summary>
        /// Author: Shahid Munir
        /// Date:   12/28/2011
        /// to manage Payment adjustment mapping data 
        /// </summary>
        /// <param name="adjustmentMappingList"></param>
        /// <returns></returns>
        bool ManageAdjustmentMapping(List<AdjustmentMappingDTO> adjustmentMappingList);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim data 
        /// </summary>
        /// <param name="adjustmentMappingDTO"></param>
        /// <returns></returns>
        int InsertAdjustmentMapping(AdjustmentMappingDTO adjustmentMappingDTO);

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeDTO"></param>
        /// <returns></returns>
        int InsertAdjustmentReasonCode(AdjustmentReasonCodeDTO adjustmentReasonCodeDTO);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to Insert/Update/Delete Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        bool ManageAdjustmentReasonCodes(List<AdjustmentReasonCodeDTO> adjustmentReasonCodeList);

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to load all Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        List<AdjustmentReasonCodeDTO> LoadAdjustmentReasonCodes();

        #endregion
    }
}
