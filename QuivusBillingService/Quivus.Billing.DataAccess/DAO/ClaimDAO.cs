using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using QuivusModel.Billing.Claim;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System;
using QuivusUtility.ProjectConstants;
using Quivus.Billing.DataAccess.DBUtil;
using QuivusModel.Billing.PendingBills;

namespace Quivus.Billing.DataAccess.DAO
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   12/16/2011
    /// </summary>
    public class ClaimDAO
    {

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim HCFA Info
        /// </summary>
        /// <param name="claimInsertDTO"></param>
        /// <returns></returns>
        public bool InsertClaimCMSQuestions(CMS1500QuestionsDTO claimDTO)
        {
            bool result = false;

            string sProc = BillingConstant.CLAIM_CMS_QUESTION_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimDTO.ClaimID);
                db.AddInParameter(command, "@Block10A", DbType.Boolean, claimDTO.Block10A);
                db.AddInParameter(command, "@Block10B", DbType.Boolean, claimDTO.Block10B);
                db.AddInParameter(command, "@BlockBStateCode", DbType.String, claimDTO.BlockBStateCode);
                db.AddInParameter(command, "@Block10C", DbType.Boolean, claimDTO.Block10C);
                db.AddInParameter(command, "@Block16DateFrom", DbType.DateTime, claimDTO.Block16DateFrom);
                db.AddInParameter(command, "@Block16DateTo", DbType.DateTime, claimDTO.Block16DateTo);
                db.AddInParameter(command, "@Block18DateFrom", DbType.DateTime, claimDTO.Block18DateFrom);
                db.AddInParameter(command, "@Block18DateTo", DbType.DateTime, claimDTO.Block18DateTo);
                db.AddInParameter(command, "@Block20", DbType.Boolean, claimDTO.Block20);
                db.AddInParameter(command, "@Block20Charges", DbType.Currency, claimDTO.Block20Charges);
                db.AddInParameter(command, "@Block22", DbType.String, claimDTO.Block22);
                db.AddInParameter(command, "@Block22RefNumber", DbType.String, claimDTO.Block22RefNumber);
                db.AddInParameter(command, "@Block24B", DbType.String, claimDTO.Block24B);
                db.AddInParameter(command, "@Block32", DbType.String, claimDTO.Block32);
                
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }


            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return result;
        }

        /// <summary>
        /// to insert claims ENM Code
        /// </summary>
        /// <param name="WRSList"></param>
        /// <param name="claimID"></param>
        /// <returns></returns>
        public bool InsertClaimENMCode(List<SuperBillENMServiceDTO> ENMList, int claimID)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_ENM_CODE_INSERT_SP;
            DataTable dtClaimENM = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (ENMList != null && ENMList.Count > 0)
                {
                    dtClaimENM = DTOToTableConverter.ClaimENMDTOListConverter(ENMList, claimID);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimENM = sqlCommand.Parameters.AddWithValue("@ClaimENMServiceTVP", dtClaimENM);
                tvpClaimENM.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaimENM.TypeName = BillingConstant.CLAIM_ENM_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// to insert claims work related services
        /// </summary>
        /// <param name="WRSList"></param>
        /// <param name="claimID"></param>
        /// <returns></returns>
        public bool InsertClaimWRS(List<SuperBillWorkRelatedServicesDTO> WRSList, int claimID)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_WRS_INSERT_SP;
            DataTable dtClaimWRS = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (WRSList != null && WRSList.Count > 0)
                {
                    dtClaimWRS = DTOToTableConverter.ClaimWRSDTOListConverter(WRSList,claimID);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimWRS = sqlCommand.Parameters.AddWithValue("@ClaimWRSTVP", dtClaimWRS);
                tvpClaimWRS.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaimWRS.TypeName = BillingConstant.CLAIM_WRS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }
        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to get work related services
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<SuperBillWorkRelatedServicesDTO> LoadWRSByClaimId(int ClaimId)
        {
            List<SuperBillWorkRelatedServicesDTO> WRSList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_WRS_LOAD_BY_CLAIMID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                WRSList = new List<SuperBillWorkRelatedServicesDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, ClaimId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        Quivus.Utility.GenericDataReaderToDTOConverter.ConvertToDTOList(WRSList, objIReader, typeof(SuperBillWorkRelatedServicesDTO));
                    }
                }
            }
            catch
            {

                throw;
            }
            return WRSList;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to get already created claims of Provider
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<ClaimSearchDTO> LoadClaimsByProviderId(int providerId)
        {
            List<ClaimSearchDTO> ClaimsList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_SEARCH_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                ClaimsList = new List<ClaimSearchDTO>();

                Database db = DatabaseFactory.CreateDatabase(); 
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ProviderID", DbType.Int32, providerId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        Quivus.Utility.GenericDataReaderToDTOConverter.ConvertToDTOList(ClaimsList, objIReader, typeof(ClaimSearchDTO));
                    }
                }
            }
            catch
            {

                throw;
            }
            return ClaimsList;
        }


        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim data 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public ClaimDTO LoadClaimByClaimId(int claimId)
        {
            List<ClaimDTO> claimDTO = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_DATA_LOAD_BY_CLAIMID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                claimDTO = new List<ClaimDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ClaimId", DbType.Int32, claimId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        Quivus.Utility.GenericDataReaderToDTOConverter.ConvertToDTOList(claimDTO, objIReader, typeof(ClaimDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return claimDTO[0];
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to load claim insurance data by claimid 
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public List<ClaimInsuranceDTO> LoadClaimInsurancesByClaimId(int claimId)
        {
            List<ClaimInsuranceDTO> claimInsuranceList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_INSURANCE_DATA_LOAD_BY_CLAIMID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                claimInsuranceList = new List<ClaimInsuranceDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ClaimId", DbType.Int32, claimId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        Quivus.Utility.GenericDataReaderToDTOConverter.ConvertToDTOList(claimInsuranceList, objIReader, typeof(ClaimInsuranceDTO));
                    }
                }
            }
            catch
            {

                throw;
            }
            return claimInsuranceList;
        }
        

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert claim data 
        /// </summary>
        /// <param name="claimInsertDTO"></param>
        /// <returns></returns>
        public int InsertClaim(ClaimDTO claimDTO)
        {
            int ClaimID = 0;
            
            string sProc = BillingConstant.CLAIM_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ProviderID", DbType.Int32, claimDTO.ProviderID);
                db.AddInParameter(command, "@QuivusId", DbType.String, claimDTO.QuivusID);
                db.AddInParameter(command, "@PatientID", DbType.Int32, claimDTO.PatientID);
                db.AddInParameter(command, "@VisitID", DbType.Int32, claimDTO.VisitID);
                db.AddInParameter(command, "@TotalCharges", DbType.Currency, claimDTO.TotalCharges);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, claimDTO.CreatedBy);
                db.AddInParameter(command, "@PatientUserID", DbType.Int32, claimDTO.PatientUserID);

                db.AddOutParameter(command, "@ClaimIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimID = int.Parse(db.GetParameterValue(command, "@ClaimIDOUT").ToString());
                }
                
                
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/23/2011
        /// to Insert/Update/Delete Claims data 
        /// </summary>
        /// <param name="claimList"></param>
        /// <returns></returns>
        public bool ManageClaim(List<ClaimDTO> claimList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_MANAGE_SP;
            DataTable dtClaims = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimList != null && claimList.Count >0 )
                {
                    dtClaims = Quivus.Billing.DataAccess.DBUtil.DTOToTableConverter.ClaimDTOListConverter(claimList);                   
                    //dtClaims = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(claimList);
                }

                #region Management of inner DTOs
                
                
                foreach (ClaimDTO claim in claimList)
                {
                    if (claim.Transaction != null && claim.Transaction.Count > 0)
                    {
                        if (claim.Transaction.Count == 1)
                        {
                            InsertClaimTransactions(claim.Transaction[0]);
                        }
                        else
                        {
                            ManageClaimTransactions(claim.Transaction);
                        }
                        
                    }

                    if (claim.BillingStatus != null && claim.BillingStatus.Count > 0)
                    {

                        if (claim.BillingStatus.Count == 1)
                        {
                            InsertClaimBillingStatus(claim.BillingStatus[0]);
                        }
                        else
                        {
                            ManageClaimBillingStatus(claim.BillingStatus);
                        }
                    }

                    if (claim.Insurance != null && claim.Insurance.Count > 0)
                    {

                        if (claim.Insurance.Count == 1)
                        {
                            InsertClaimInsurance(claim.Insurance[0]);
                        }
                        else
                        {
                            ManageClaimInsurance(claim.Insurance);
                        }
                    }
                }

                #endregion

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaims = sqlCommand.Parameters.AddWithValue("@ClaimsTVP", dtClaims);
                tvpClaims.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaims.TypeName = BillingConstant.CLAIMS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();               
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/22/2011
        /// to insert claim's insurance data 
        /// </summary>
        /// <param name="claimInsuranceDTO"></param>
        /// <returns></returns>
        public int InsertClaimInsurance(ClaimInsuranceDTO claimInsuranceDTO)
        {
            int ClaimInsuranceID = 0;

            string sProc = BillingConstant.CLAIM_INSURANCE_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimInsuranceDTO.ClaimID);
                db.AddInParameter(command, "@InsuranceCarrierID", DbType.Int32, claimInsuranceDTO.InsuranceCarrierID);
                db.AddInParameter(command, "@InsuranceTypeId", DbType.Int32, claimInsuranceDTO.InsuranceTypeId);
                db.AddInParameter(command, "@MediaTypeID", DbType.Int32, claimInsuranceDTO.MediaTypeID);
                db.AddInParameter(command, "@RelationShipID", DbType.Int32, claimInsuranceDTO.RelationShipID);
                db.AddInParameter(command, "@PatientGuarantorID", DbType.Int32, claimInsuranceDTO.PatientGuarantorID);
                db.AddInParameter(command, "@ClaimFrequencyCodeTypeID", DbType.Int32, claimInsuranceDTO.ClaimFrequencyCodeTypeID);
                db.AddInParameter(command, "@DelayReasonCodeTypeID", DbType.Int32, claimInsuranceDTO.DelayReasonCodeTypeID);
                db.AddInParameter(command, "@PolicyNumber", DbType.String, claimInsuranceDTO.PolicyNumber);
                db.AddInParameter(command, "@GroupName", DbType.String, claimInsuranceDTO.GroupName);
                db.AddInParameter(command, "@GroupNumber", DbType.String, claimInsuranceDTO.GroupNumber);
                db.AddInParameter(command, "@CMSSecondaryTypeID", DbType.Int32, claimInsuranceDTO.CMSSecondaryTypeID);
                db.AddInParameter(command, "@ICN", DbType.String, claimInsuranceDTO.ICN);
                db.AddInParameter(command, "@ClaimNote", DbType.String, claimInsuranceDTO.ClaimNote);
                db.AddInParameter(command, "@EDIReceiverTypeID", DbType.Int32, claimInsuranceDTO.EDIReceiverTypeID);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, claimInsuranceDTO.CreatedBy);

                db.AddOutParameter(command, "@ClaimInsuranceIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimInsuranceID = int.Parse(db.GetParameterValue(command, "@ClaimInsuranceIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimInsuranceID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/26/2011
        /// to Insert/Update/Delete Claims data  
        /// </summary>
        /// <param name="claimInsuranceList"></param>
        /// <returns></returns>
        public bool ManageClaimInsurance(List<ClaimInsuranceDTO> claimInsuranceList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_INSURANCE_MANAGE_SP;
            DataTable dtClaimInsurance = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimInsuranceList != null && claimInsuranceList.Count > 0)
                {                    
                    dtClaimInsurance = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(claimInsuranceList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimInsurance = sqlCommand.Parameters.AddWithValue("@ClaimInsuranceTVP", dtClaimInsurance);
                tvpClaimInsurance.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaimInsurance.TypeName = BillingConstant.CLAIM_INSURANCE_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/22/2011
        /// to insert claim's Billing Status data 
        /// </summary>
        /// <param name="claimBillingStatusDTO"></param>
        /// <returns></returns>
        public int InsertClaimBillingStatus(ClaimBillingStatusDTO claimBillingStatusDTO)
        {
            int ClaimBillingStatusID = 0;

            string sProc = BillingConstant.CLAIM_BLLING_STATUS_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimBillingStatusDTO.ClaimID);
                db.AddInParameter(command, "@BillingModeTypeID", DbType.Int32, claimBillingStatusDTO.BillingModeTypeID);
                db.AddInParameter(command, "@BillingStatusTypeID", DbType.Int32, claimBillingStatusDTO.BillingStatusTypeID);
                db.AddInParameter(command, "@CreatedBy", DbType.DateTime, claimBillingStatusDTO.CreatedBy);


                db.AddOutParameter(command, "@ClaimBillingStatusIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimBillingStatusID = int.Parse(db.GetParameterValue(command, "@ClaimBillingStatusIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimBillingStatusID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/26/2011
        /// to Insert/Update/Delete Claim billing status data   
        /// </summary>
        /// <param name="claimBillingStatusList"></param>
        /// <returns></returns>
        public bool ManageClaimBillingStatus(List<ClaimBillingStatusDTO> claimBillingStatusList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_BILLING_STATUS_MANAGE_SP;
            DataTable dtClaimBillingStatus = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimBillingStatusList != null && claimBillingStatusList.Count > 0)
                {
                    dtClaimBillingStatus = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(claimBillingStatusList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimBillingStatus = sqlCommand.Parameters.AddWithValue("@ClaimBillingStatusTVP", dtClaimBillingStatus);
                tvpClaimBillingStatus.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaimBillingStatus.TypeName = BillingConstant.CLAIM_BILLING_STATUS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/22/2011
        /// to insert claim's transactions data 
        /// </summary>
        /// <param name="claimTransactionDTO"></param>
        /// <returns></returns>
        public int InsertClaimTransactions(ClaimTransactionDTO claimTransactionDTO)
        {
            int ClaimTransactionID = 0;

            string sProc = BillingConstant.CLAIM_TRANSACTIONS_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimTransactionDTO.ClaimID);
                db.AddInParameter(command, "@PatientProcedureID", DbType.Int32, claimTransactionDTO.ProcedureID);
                db.AddInParameter(command, "@ProcedureCode", DbType.String, claimTransactionDTO.ProcedureCode);
                db.AddInParameter(command, "@ServiceDateFrom", DbType.DateTime, Convert.ToDateTime(claimTransactionDTO.DateFrom.ToShortDateString()));
                db.AddInParameter(command, "@ServiceDateTo", DbType.DateTime, Convert.ToDateTime(claimTransactionDTO.DateTo.ToShortDateString()));
                db.AddInParameter(command, "@Amount", DbType.Decimal, claimTransactionDTO.Amount);
                db.AddInParameter(command, "@Document", DbType.String, claimTransactionDTO.Document);
                db.AddInParameter(command, "@Insurance1Response", DbType.Boolean, claimTransactionDTO.Ins1Resp);
                db.AddInParameter(command, "@Insurance2Response", DbType.Boolean, claimTransactionDTO.Ins2Resp);
                db.AddInParameter(command, "@Insurance3Response", DbType.Boolean, claimTransactionDTO.Ins3Resp);                               
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, claimTransactionDTO.CreatedBy);


                db.AddOutParameter(command, "@ClaimTransactionIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimTransactionID = int.Parse(db.GetParameterValue(command, "@ClaimTransactionIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimTransactionID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/26/2011
        /// to Insert/Update/Delete Claim transactions  
        /// </summary>
        /// <param name="claimTransactionList"></param>
        /// <returns></returns>
        public bool ManageClaimTransactions(List<ClaimTransactionDTO> claimTransactionList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_TRANSACTIONS_MANAGE_SP;
            DataTable dtClaimTransactions = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimTransactionList != null && claimTransactionList.Count > 0)
                {
                    dtClaimTransactions = GenericDTOListToDataTableConverter.ConvertTransactionDTOListToDataTable(claimTransactionList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimTransactions = sqlCommand.Parameters.AddWithValue("@ClaimTransactionsTVP", dtClaimTransactions);
                tvpClaimTransactions.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaimTransactions.TypeName = BillingConstant.CLAIM_TRANSACTIONS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    01/10/2011
        /// to split Claim transactions to new claim
        /// </summary>
        /// <param name="claimTransactionIdList"></param>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public bool SplitClaimTransactions(List<int> claimTransactionIdList, int claimId, int managedByUserId)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_TRANSACTIONS_SPLIT_SP;
            DataTable dtClaimTransactions = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimTransactionIdList != null && claimTransactionIdList.Count > 0)
                {
                    dtClaimTransactions = DTOToTableConverter.ClaimTransactionIdListConverter(claimTransactionIdList, claimId, managedByUserId);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimTransactionsSplit = sqlCommand.Parameters.AddWithValue("@ClaimTransactionsSplitTVP", dtClaimTransactions);
                tvpClaimTransactionsSplit.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaimTransactionsSplit.TypeName = BillingConstant.CLAIM_TRANSACTIONS_SPLIT_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/28/2011
        /// to get claim's transactions by claim id
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        public List<ClaimTransactionDTO> LoadClaimTransactionsByClaimId(int claimId)
        {
            List<ClaimTransactionDTO> ClaimTransactionList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.CLAIM_TRANSACTIONS_SEARCH_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                ClaimTransactionList = new List<ClaimTransactionDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        Quivus.Billing.DataAccess.DBUtil.GenericDataReaderToDTOConverter.ConvertToTransactionDTOList(ClaimTransactionList, objIReader, typeof(ClaimTransactionDTO));
                    }
                }
            }
            catch
            {

                throw;
            }
            return ClaimTransactionList;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim submission batch data 
        /// </summary>
        /// <param name="claimSubmissionBatchDTO"></param>
        /// <returns></returns>
        public int InsertClaimSubmissionBatch(ClaimSubmissionBatchDTO claimSubmissionBatchDTO)
        {
            int ClaimSubmissionBatchID = 0;

            string sProc = BillingConstant.CLAIM_SUBMISSION_BATCH_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);
                
                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimSubmissionBatchDTO.ClaimID);
                db.AddInParameter(command, "@BillingDate", DbType.DateTime, claimSubmissionBatchDTO.BillingDate);
                db.AddInParameter(command, "@InsuranceCarrierID", DbType.Int32, claimSubmissionBatchDTO.InsuranceCarrierID);
                db.AddInParameter(command, "@BHTControlNumberID", DbType.Int32, claimSubmissionBatchDTO.BHTControlNumberID);
                db.AddInParameter(command, "@BillingModeTypeID", DbType.Int32, claimSubmissionBatchDTO.BillingModeTypeID);
                db.AddInParameter(command, "@BillingPhysicianID", DbType.Int32, claimSubmissionBatchDTO.BillingPhysicianID);
                db.AddInParameter(command, "@BillingStatusTypeID", DbType.Int32, claimSubmissionBatchDTO.BillingStatusTypeID);
                db.AddInParameter(command, "@TotalCharges", DbType.Decimal, claimSubmissionBatchDTO.TotalCharges);
                db.AddInParameter(command, "@DueAmount", DbType.Decimal, claimSubmissionBatchDTO.DueAmount);
                db.AddInParameter(command, "@EDIReceiverTypeID", DbType.Int32, claimSubmissionBatchDTO.EDIReceiverTypeID);
                db.AddInParameter(command, "@GSControlNumberID", DbType.Int32, claimSubmissionBatchDTO.GSControlNumberID);
                db.AddInParameter(command, "@ISAControlNumberID", DbType.Int32, claimSubmissionBatchDTO.ISAControlNumberID);
                db.AddInParameter(command, "@STControlNnumberID", DbType.Int32, claimSubmissionBatchDTO.STControlNnumberID);                
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, claimSubmissionBatchDTO.CreatedBy);

                db.AddOutParameter(command, "@BatchIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimSubmissionBatchID = int.Parse(db.GetParameterValue(command, "@BatchIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimSubmissionBatchID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim submission batches  
        /// </summary>
        /// <param name="claimSubmissionBatchList"></param>
        /// <returns></returns>
        public bool ManageClaimSubmissionBatch(List<ClaimSubmissionBatchDTO> claimSubmissionBatchList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_SUBMISSION_BATCH_MANAGE_SP;
            DataTable dtClaimSubmissionBatches = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimSubmissionBatchList != null && claimSubmissionBatchList.Count > 0)
                {
                    dtClaimSubmissionBatches = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(claimSubmissionBatchList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimSubmissionBatches = sqlCommand.Parameters.AddWithValue("@ClaimSubmissionBatchTVP", dtClaimSubmissionBatches);
                tvpClaimSubmissionBatches.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVPs
                tvpClaimSubmissionBatches.TypeName = BillingConstant.CLAIM_SUBMISSION_BATCH_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimPaymentDTO"></param>
        /// <returns></returns>
        public int InsertClaimPayment(ClaimPaymentsDTO claimPaymentDTO)
        {
            int ClaimPaymentID = 0;

            string sProc = BillingConstant.CLAIM_PAYMENTS_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ClaimID", DbType.Int32, claimPaymentDTO.ClaimID);
                db.AddInParameter(command, "@InsuranceCarrierId", DbType.Int32, claimPaymentDTO.InsuranceCarrierId);
                db.AddInParameter(command, "@EntryDate", DbType.DateTime, claimPaymentDTO.EntryDate);
                db.AddInParameter(command, "@PaymentSourceTypeID", DbType.Int32, claimPaymentDTO.PaymentSourceTypeID);
                db.AddInParameter(command, "@PaymentTypeID", DbType.Int32, claimPaymentDTO.PaymentTypeID);
                db.AddInParameter(command, "@ChargedProcedure", DbType.String, claimPaymentDTO.ChargedProcedure);
                db.AddInParameter(command, "@PaidProcedure", DbType.String, claimPaymentDTO.PaidProcedure);
                db.AddInParameter(command, "@Modifier1", DbType.String, claimPaymentDTO.Modifier1);
                db.AddInParameter(command, "@Modifier2", DbType.String, claimPaymentDTO.Modifier2);
                db.AddInParameter(command, "@Modifier3", DbType.String, claimPaymentDTO.Modifier3);
                db.AddInParameter(command, "@Modifier4", DbType.String, claimPaymentDTO.Modifier4);
                db.AddInParameter(command, "@ServiceDateFrom", DbType.DateTime, claimPaymentDTO.ServiceDateFrom);
                db.AddInParameter(command, "@ServiceDateTo", DbType.DateTime, claimPaymentDTO.ServiceDateTo);                
                db.AddInParameter(command, "@CheckNumber", DbType.String, claimPaymentDTO.CheckNumber);
                db.AddInParameter(command, "@FilingDate", DbType.DateTime, claimPaymentDTO.FilingDate);
                db.AddInParameter(command, "@AllowedAmount", DbType.Decimal, claimPaymentDTO.AllowedAmount);
                db.AddInParameter(command, "@PaidAmount", DbType.Decimal, claimPaymentDTO.PaidAmount);
                db.AddInParameter(command, "@AdjustedAmount", DbType.Decimal, claimPaymentDTO.AdjustedAmount);
                db.AddInParameter(command, "@RejectedAmount", DbType.Decimal, claimPaymentDTO.RejectedAmount);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, claimPaymentDTO.CreatedBy);

                db.AddOutParameter(command, "@ClaimPaymentIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimPaymentID = int.Parse(db.GetParameterValue(command, "@ClaimPaymentIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimPaymentID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim payments  
        /// </summary>
        /// <param name="claimPaymentsList"></param>
        /// <returns></returns>
        public bool ManageClaimPayments(List<ClaimPaymentsDTO> claimPaymentsList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_PAYMENTS_MANAGE_SP;
            DataTable dtClaimPayments = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimPaymentsList != null && claimPaymentsList.Count > 0)
                {
                    dtClaimPayments = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(claimPaymentsList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimpayments = sqlCommand.Parameters.AddWithValue("@ClaimPaymentsTVP", dtClaimPayments);
                tvpClaimpayments.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVPs
                tvpClaimpayments.TypeName = BillingConstant.CLAIM_PAYMENTS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/29/2011
        /// to insert claim payments data 
        /// </summary>
        /// <param name="claimAdjustmentDTO"></param>
        /// <returns></returns>
        public int InsertClaimAdjustment(ClaimAdjustmentDTO claimAdjustmentDTO)
        {
            int ClaimAdjustmentID = 0;

            string sProc = BillingConstant.CLAIM_ADJUSTMENTS_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@ClaimPaymentID", DbType.Int32, claimAdjustmentDTO.ClaimPaymentID);
                db.AddInParameter(command, "@AdjustmentReasonCodeID", DbType.Int32, claimAdjustmentDTO.AdjustmentReasonCodeID);
                db.AddInParameter(command, "@AdjustmentGroupCodeTypeID", DbType.Int32, claimAdjustmentDTO.AdjustmentGroupCodeTypeID);
                db.AddInParameter(command, "@AdjustmentAmount", DbType.Decimal, claimAdjustmentDTO.AdjustmentAmount);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, claimAdjustmentDTO.CreatedBy);

                db.AddOutParameter(command, "@ClaimAdjustmentIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    ClaimAdjustmentID = int.Parse(db.GetParameterValue(command, "@ClaimAdjustmentIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return ClaimAdjustmentID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/29/2011
        /// to Insert/Update/Delete Claim adjustments  
        /// </summary>
        /// <param name="claimAdjustmentList"></param>
        /// <returns></returns>
        public bool ManageClaimAdjustments(List<ClaimAdjustmentDTO> claimAdjustmentList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.CLAIM_ADJUSTMENTS_MANAGE_SP;
            DataTable dtClaimAdjustments = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (claimAdjustmentList != null && claimAdjustmentList.Count > 0)
                {
                    dtClaimAdjustments = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(claimAdjustmentList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaimAdjustments = sqlCommand.Parameters.AddWithValue("@ClaimAdjustmentsTVP", dtClaimAdjustments);
                tvpClaimAdjustments.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVPs
                tvpClaimAdjustments.TypeName = BillingConstant.CLAIM_ADJUSTMENTS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return UpdateResult;
        }

    }
}
