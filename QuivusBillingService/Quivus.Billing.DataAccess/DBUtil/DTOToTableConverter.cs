using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuivusModel.Billing.Claim;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility.ProjectConstants;
using Quivus.Utility;
using QuivusUtility.ProjectConstants;
using QuivusModel.Billing.EDIPaymentTransaction;
using System.Reflection;
using QuivusModel.Billing.PendingBills;

namespace Quivus.Billing.DataAccess.DBUtil
{
    public class DTOToTableConverter
    {

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/23/2011
        /// convert the claim WRS list to TVP
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        public static DataTable ClaimENMDTOListConverter(List<SuperBillENMServiceDTO> claimENM, int claimID)
        {
            DataTable dtClaims = null;

            try
            {
                dtClaims = new DataTable();
                dtClaims.Columns.Add("ENMServiceID", typeof(System.Int32));
                dtClaims.Columns.Add("ClaimID", typeof(System.Int32));                
                dtClaims.Columns.Add("ENMCodeTypeID", typeof(System.Int32));
                dtClaims.Columns.Add("Fee", typeof(System.Decimal));
                dtClaims.Columns.Add("CodeType", typeof(System.Int32));


                dtClaims.AcceptChanges();

                SuperBillENMServiceDTO objClaimDTO;

                for (int indexClaim = 0; indexClaim < claimENM.Count; indexClaim++)
                {
                    objClaimDTO = claimENM[indexClaim];
                    DataRow dRow = dtClaims.NewRow();

                    dRow["ENMServiceID"] = 0;
                    dRow["ClaimID"] = claimID;
                    dRow["ENMCodeTypeID"] = objClaimDTO.ENMCodeTypeID;
                    dRow["Fee"] = objClaimDTO.Fee;
                    dRow["CodeType"] = objClaimDTO.CodeType;

                    dtClaims.Rows.Add(dRow);
                }
                dtClaims.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaims;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/23/2011
        /// convert the claim WRS list to TVP
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        public static DataTable ClaimWRSDTOListConverter(List<SuperBillWorkRelatedServicesDTO> claimWRS, int claimID)
        {
            DataTable dtClaims = null;

            try
            {
                dtClaims = new DataTable();
                dtClaims.Columns.Add("WorkServiceID", typeof(System.Int32));
                dtClaims.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaims.Columns.Add("WorkServiceTypeId", typeof(System.Int32));               
                dtClaims.Columns.Add("Fee", typeof(System.Decimal));    


                dtClaims.AcceptChanges();

                SuperBillWorkRelatedServicesDTO objClaimDTO;

                for (int indexClaim = 0; indexClaim < claimWRS.Count; indexClaim++)
                {
                    objClaimDTO = claimWRS[indexClaim];
                    DataRow dRow = dtClaims.NewRow();

                    dRow["WorkServiceID"] = 0;
                    dRow["ClaimID"] = claimID;
                    dRow["WorkServiceTypeId"] = objClaimDTO.WorkServiceTypeId;
                    dRow["Fee"] = objClaimDTO.Fee;                  

                    dtClaims.Rows.Add(dRow);
                }
                dtClaims.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaims;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/23/2011
        /// convert the claim list to TVP
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        public static DataTable ClaimDTOListConverter(List<ClaimDTO> claims)
        {
            DataTable dtClaims = null;

            try
            {
                dtClaims = new DataTable();
                dtClaims.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaims.Columns.Add("PatientID", typeof(System.Int32));
                dtClaims.Columns.Add("ProviderID", typeof(System.Int32));
                dtClaims.Columns.Add("QuivusID", typeof(System.Int32));
                dtClaims.Columns.Add("VisitID", typeof(System.Int32));
                dtClaims.Columns.Add("TotalCharges", typeof(System.Decimal));
                dtClaims.Columns.Add("CreatedBy", typeof(System.Int32));
                dtClaims.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtClaims.Columns.Add("IsDeleted", typeof(System.Boolean));


                dtClaims.AcceptChanges();

                ClaimDTO objClaimDTO;

                for (int indexClaim = 0; indexClaim < claims.Count; indexClaim++)
                {
                    objClaimDTO = claims[indexClaim];
                    DataRow dRow = dtClaims.NewRow();

                    dRow["ClaimID"] = objClaimDTO.ClaimID;
                    dRow["PatientID"] = objClaimDTO.PatientID;
                    dRow["ProviderID"] = objClaimDTO.ProviderID;
                    dRow["QuivusID"] = objClaimDTO.QuivusID;
                    dRow["VisitID"] = objClaimDTO.VisitID;
                    dRow["TotalCharges"] = objClaimDTO.TotalCharges;
                    dRow["CreatedBy"] = objClaimDTO.CreatedBy;
                    dRow["ModifiedBy"] = objClaimDTO.ModifiedBy;
                    dRow["IsDeleted"] = objClaimDTO.IsDeleted;                    

                    dtClaims.Rows.Add(dRow);
                }
                dtClaims.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaims;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/26/2011
        /// convert the claim insurance list to TVP 
        /// </summary>
        /// <param name="claimInsurances"></param>
        /// <returns></returns>
        public static DataTable ClaimInsuranceDTOListConverter(List<ClaimInsuranceDTO> claimInsurances)
        {
            DataTable dtClaimInsurances = null;

            try
            {
                dtClaimInsurances = new DataTable();
                dtClaimInsurances.Columns.Add("ClaimInsuranceID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("InsuranceCarrierID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("InsuranceTypeId", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("MediaTypeID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("RelationShipID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("PatientGuarantorID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("ClaimFrequencyCodeTypeID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("DelayReasonCodeTypeID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("PolicyNumber", typeof(System.String));
                dtClaimInsurances.Columns.Add("GroupName", typeof(System.String));
                dtClaimInsurances.Columns.Add("CMSSecondaryTypeID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("ICN", typeof(System.String));
                dtClaimInsurances.Columns.Add("ClaimNote", typeof(System.String));
                dtClaimInsurances.Columns.Add("EDIReceiverTypeID", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("CreatedBy", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtClaimInsurances.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtClaimInsurances.AcceptChanges();

                ClaimInsuranceDTO objClaimInsuranceDTO;

                for (int indexClaimInsurance = 0; indexClaimInsurance < claimInsurances.Count; indexClaimInsurance++)
                {
                    objClaimInsuranceDTO = claimInsurances[indexClaimInsurance];
                    DataRow dRow = dtClaimInsurances.NewRow();

                    dRow["ClaimInsuranceID"] = objClaimInsuranceDTO.ClaimInsuranceID;
                    dRow["ClaimID"] = objClaimInsuranceDTO.ClaimID;
                    dRow["InsuranceCarrierID"] = objClaimInsuranceDTO.InsuranceCarrierID;
                    dRow["InsuranceTypeId"] = objClaimInsuranceDTO.InsuranceTypeId;
                    dRow["MediaTypeID"] = objClaimInsuranceDTO.MediaTypeID;
                    dRow["RelationShipID"] = objClaimInsuranceDTO.RelationShipID;
                    dRow["PatientGuarantorID"] = objClaimInsuranceDTO.PatientGuarantorID;
                    dRow["ClaimFrequencyCodeTypeID"] = objClaimInsuranceDTO.ClaimFrequencyCodeTypeID;
                    dRow["DelayReasonCodeTypeID"] = objClaimInsuranceDTO.DelayReasonCodeTypeID;
                    dRow["PolicyNumber"] = objClaimInsuranceDTO.PolicyNumber;
                    dRow["GroupName"] = objClaimInsuranceDTO.GroupName;
                    dRow["GroupNumber"] = objClaimInsuranceDTO.GroupNumber;
                    dRow["CMSSecondaryTypeID"] = objClaimInsuranceDTO.CMSSecondaryTypeID;
                    dRow["ICN"] = objClaimInsuranceDTO.ICN;
                    dRow["ClaimNote"] = objClaimInsuranceDTO.ClaimNote;
                    dRow["EDIReceiverTypeID"] = objClaimInsuranceDTO.EDIReceiverTypeID;                    
                    dRow["CreatedBy"] = objClaimInsuranceDTO.CreatedBy;
                    dRow["ModifiedBy"] = objClaimInsuranceDTO.ModifiedBy;
                    dRow["IsDeleted"] = objClaimInsuranceDTO.IsDeleted;

                    dtClaimInsurances.Rows.Add(dRow);
                }
                dtClaimInsurances.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimInsuranceDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaimInsurances;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/26/2011
        /// convert the claim transaction list to TVP 
        /// </summary>
        /// <param name="claimtransactions"></param>
        /// <returns></returns>
        public static DataTable ClaimTransactionDTOListConverter(List<ClaimTransactionDTO> claimtransactions)
        {
            DataTable dtClaimTransactions = null;

            try
            {
                dtClaimTransactions = new DataTable();
                dtClaimTransactions.Columns.Add("ClaimTransactionID", typeof(System.Int32));
                dtClaimTransactions.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaimTransactions.Columns.Add("ProcedureCode", typeof(System.String));
                dtClaimTransactions.Columns.Add("ServiceDateFrom", typeof(System.DateTime));
                dtClaimTransactions.Columns.Add("ServiceDateTo", typeof(System.DateTime));               
                dtClaimTransactions.Columns.Add("Amount", typeof(System.Decimal));
                dtClaimTransactions.Columns.Add("Document", typeof(System.String));
                dtClaimTransactions.Columns.Add("Insurance1Response", typeof(System.Boolean));
                dtClaimTransactions.Columns.Add("Insurance2Response", typeof(System.Boolean));
                dtClaimTransactions.Columns.Add("Insurance3Response", typeof(System.Boolean));
                dtClaimTransactions.Columns.Add("CreatedBy", typeof(System.Int32));
                dtClaimTransactions.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtClaimTransactions.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtClaimTransactions.AcceptChanges();

                ClaimTransactionDTO objClaimTransactionDTO;

                for (int indexClaimTransaction = 0; indexClaimTransaction < claimtransactions.Count; indexClaimTransaction++)
                {
                    objClaimTransactionDTO = claimtransactions[indexClaimTransaction];
                    DataRow dRow = dtClaimTransactions.NewRow();

                    dRow["ClaimTransactionID"] = objClaimTransactionDTO.ID;
                    dRow["ClaimID"] = objClaimTransactionDTO.ClaimID;
                    dRow["PatientProcedureId"] = objClaimTransactionDTO.ProcedureID;
                    dRow["ProcedureCode"] = objClaimTransactionDTO.ProcedureCode;
                    dRow["ServiceDateFrom"] = objClaimTransactionDTO.DateFrom;
                    dRow["ServiceDateTo"] = objClaimTransactionDTO.DateTo;
                    dRow["Amount"] = objClaimTransactionDTO.Amount;
                    dRow["Document"] = objClaimTransactionDTO.Document;
                    dRow["Insurance1Response"] = objClaimTransactionDTO.Ins1Resp;
                    dRow["Insurance12Response"] = objClaimTransactionDTO.Ins2Resp;
                    dRow["Insurance3Response"] = objClaimTransactionDTO.Ins3Resp;
                    dRow["CreatedBy"] = objClaimTransactionDTO.CreatedBy;
                    dRow["ModifiedBy"] = objClaimTransactionDTO.ModifiedBy;
                    dRow["IsDeleted"] = objClaimTransactionDTO.IsDeleted;

                    dtClaimTransactions.Rows.Add(dRow);
                }
                dtClaimTransactions.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimTransactionDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaimTransactions;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/26/2011
        /// convert the claim billing status list to TVP  
        /// </summary>
        /// <param name="claimBillingStatus"></param>
        /// <returns></returns>
        public static DataTable ClaimBillingStatusDTOListConverter(List<ClaimBillingStatusDTO> claimBillingStatus)
        {
            DataTable dtClaimBillingStatus = null;

            try
            {
                dtClaimBillingStatus = new DataTable();
                dtClaimBillingStatus.Columns.Add("ClaimBillingStatusID", typeof(System.Int32));
                dtClaimBillingStatus.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaimBillingStatus.Columns.Add("BillingModeTypeID", typeof(System.Int32));
                dtClaimBillingStatus.Columns.Add("BillingStatusTypeID", typeof(System.Int32));
                dtClaimBillingStatus.Columns.Add("CreatedBy", typeof(System.Int32));
                dtClaimBillingStatus.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtClaimBillingStatus.Columns.Add("IsDeleted", typeof(System.Boolean));


                dtClaimBillingStatus.AcceptChanges();

                ClaimBillingStatusDTO objClaimBillingStatusDTO;

                for (int indexClaim = 0; indexClaim < claimBillingStatus.Count; indexClaim++)
                {
                    objClaimBillingStatusDTO = claimBillingStatus[indexClaim];
                    DataRow dRow = dtClaimBillingStatus.NewRow();

                    dRow["ClaimBillingStatusID"] = objClaimBillingStatusDTO.ClaimBillingStatusID;
                    dRow["ClaimID"] = objClaimBillingStatusDTO.ClaimID;
                    dRow["BillingModeTypeID"] = objClaimBillingStatusDTO.BillingModeTypeID;
                    dRow["BillingStatusTypeID"] = objClaimBillingStatusDTO.BillingStatusTypeID;
                    dRow["CreatedBy"] = objClaimBillingStatusDTO.CreatedBy;
                    dRow["ModifiedBy"] = objClaimBillingStatusDTO.ModifiedBy;
                    dRow["IsDeleted"] = objClaimBillingStatusDTO.IsDeleted;

                    dtClaimBillingStatus.Rows.Add(dRow);
                }
                dtClaimBillingStatus.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaimBillingStatus;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/28/2011
        /// convert the adjustment mapping list to TVP
        /// </summary>
        /// <param name="adjustmentMapping"></param>
        /// <returns></returns>
        public static DataTable AdjustmentMappingDTOListConverter(List<AdjustmentMappingDTO> adjustmentMapping)
        {
            DataTable dtAdjustmentMapping = null;

            try
            {
                dtAdjustmentMapping = new DataTable();
                dtAdjustmentMapping.Columns.Add("AdjustmentMappingID", typeof(System.Int32));
                dtAdjustmentMapping.Columns.Add("AdjustmentReasonCodeID", typeof(System.Int32));
                dtAdjustmentMapping.Columns.Add("AdjustmentGroupCodeTypeID", typeof(System.Int32));
                dtAdjustmentMapping.Columns.Add("AdjustmentMappingTypeID", typeof(System.Int32));               
                dtAdjustmentMapping.Columns.Add("CreatedBy", typeof(System.Int32));
                dtAdjustmentMapping.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtAdjustmentMapping.Columns.Add("IsDeleted", typeof(System.Boolean));


                dtAdjustmentMapping.AcceptChanges();

                AdjustmentMappingDTO objAdjustmentMappingDTO;

                for (int indexAM = 0; indexAM < adjustmentMapping.Count; indexAM++)
                {
                    objAdjustmentMappingDTO = adjustmentMapping[indexAM];
                    DataRow dRow = dtAdjustmentMapping.NewRow();

                    dRow["AdjustmentMappingID"] = objAdjustmentMappingDTO.AdjustmentMappingID;
                    dRow["AdjustmentReasonCodeID"] = objAdjustmentMappingDTO.AdjustmentReasonCodeID;
                    dRow["AdjustmentGroupCodeTypeID"] = objAdjustmentMappingDTO.AdjustmentGroupCodeTypeID;
                    dRow["AdjustmentMappingTypeID"] = objAdjustmentMappingDTO.AdjustmentMappingTypeID;                   
                    dRow["CreatedBy"] = objAdjustmentMappingDTO.CreatedBy;
                    dRow["ModifiedBy"] = objAdjustmentMappingDTO.ModifiedBy;
                    dRow["IsDeleted"] = objAdjustmentMappingDTO.IsDeleted;

                    dtAdjustmentMapping.Rows.Add(dRow);
                }
                dtAdjustmentMapping.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtAdjustmentMapping;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/29/2011
        /// convert the claim submission batch list to TVP
        /// </summary>
        /// <param name="claimSubmissionBatches"></param>
        /// <returns></returns>
        public static DataTable ClaimSubmissionBatchDTOListConverter(List<ClaimSubmissionBatchDTO> claimSubmissionBatches)
        {
            DataTable dtClaimSubmissionBatch = null;

            try
            {
                dtClaimSubmissionBatch = new DataTable();
                dtClaimSubmissionBatch.Columns.Add("BatchID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("BillingDate", typeof(System.DateTime));
                dtClaimSubmissionBatch.Columns.Add("BillingModeTypeID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("BillingStatusTypeID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("TotalCharges", typeof(System.Decimal));
                dtClaimSubmissionBatch.Columns.Add("DueAmount", typeof(System.Decimal));
                dtClaimSubmissionBatch.Columns.Add("MediaTypeID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("EDIReceiverTypeID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("BillingPhysicianID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("InsuranceCarrierID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("ISAControlNumberID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("GSControlNumberID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("STControlNnumberID", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("BHTControlNumberID", typeof(System.Int32));                
                dtClaimSubmissionBatch.Columns.Add("CreatedBy", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtClaimSubmissionBatch.Columns.Add("IsDeleted", typeof(System.Boolean));


                dtClaimSubmissionBatch.AcceptChanges();

                ClaimSubmissionBatchDTO objClaimSubmissionBatchDTO;

                for (int indexAM = 0; indexAM < claimSubmissionBatches.Count; indexAM++)
                {
                    objClaimSubmissionBatchDTO = claimSubmissionBatches[indexAM];
                    DataRow dRow = dtClaimSubmissionBatch.NewRow();

                    dRow["BatchID"] = objClaimSubmissionBatchDTO.BatchID;
                    dRow["ClaimID"] = objClaimSubmissionBatchDTO.ClaimID;
                    dRow["BillingDate"] = objClaimSubmissionBatchDTO.BillingDate;
                    dRow["BillingModeTypeID"] = objClaimSubmissionBatchDTO.BillingModeTypeID;
                    dRow["BillingStatusTypeID"] = objClaimSubmissionBatchDTO.BillingStatusTypeID;
                    dRow["TotalCharges"] = objClaimSubmissionBatchDTO.TotalCharges;
                    dRow["DueAmount"] = objClaimSubmissionBatchDTO.DueAmount;
                    dRow["MediaTypeID"] = objClaimSubmissionBatchDTO.MediaTypeID;
                    dRow["EDIReceiverTypeID"] = objClaimSubmissionBatchDTO.EDIReceiverTypeID;
                    dRow["BillingPhysicianID"] = objClaimSubmissionBatchDTO.BillingPhysicianID;
                    dRow["InsuranceCarrierID"] = objClaimSubmissionBatchDTO.InsuranceCarrierID;
                    dRow["ISAControlNumberID"] = objClaimSubmissionBatchDTO.ISAControlNumberID;
                    dRow["GSControlNumberID"] = objClaimSubmissionBatchDTO.GSControlNumberID;
                    dRow["STControlNnumberID"] = objClaimSubmissionBatchDTO.STControlNnumberID;
                    dRow["BHTControlNumberID"] = objClaimSubmissionBatchDTO.BHTControlNumberID;
                    dRow["CreatedBy"] = objClaimSubmissionBatchDTO.CreatedBy;
                    dRow["ModifiedBy"] = objClaimSubmissionBatchDTO.ModifiedBy;
                    dRow["IsDeleted"] = objClaimSubmissionBatchDTO.IsDeleted;

                    dtClaimSubmissionBatch.Rows.Add(dRow);
                }
                dtClaimSubmissionBatch.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimDTOListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaimSubmissionBatch;
        }


        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/10/2012
        /// convert the claim transaction Ids list with claim id to TVP 
        /// </summary>
        /// <param name="claimsTransactionIdList"></param>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public static DataTable ClaimTransactionIdListConverter(List<int> claimsTransactionIdList, int claimId, int managedByUserId)
        {
            DataTable dtClaimTransactions = null;

            try
            {
                dtClaimTransactions = new DataTable();
                dtClaimTransactions.Columns.Add("ClaimID", typeof(System.Int32));
                dtClaimTransactions.Columns.Add("ClaimTransactionId", typeof(System.Int32));
                dtClaimTransactions.Columns.Add("ModifiedBy", typeof(System.Int32));

                dtClaimTransactions.AcceptChanges();

                for (int indexClaimTransaction = 0; indexClaimTransaction < claimsTransactionIdList.Count; indexClaimTransaction++)
                {
                    DataRow dRow = dtClaimTransactions.NewRow();

                    dRow["ClaimID"] = claimId;
                    dRow["ClaimTransactionId"] = claimsTransactionIdList[indexClaimTransaction];
                    dRow["ModifiedBy"] = managedByUserId;

                    dtClaimTransactions.Rows.Add(dRow);
                }
                dtClaimTransactions.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(BillingConstant.CLAIM_TABLE_CONVERSION_ERROR_MSG + ".ClaimTransactionIdListConverter", BillingConstant.ERROR_CODE.ClaimDTOConversion.ToString());
            }
            return dtClaimTransactions;
        }
                
    }
}
