using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Billing.EDIPaymentTransaction;
using System.Data;
using System.Data.SqlClient;
using Quivus.Utility.Logger;
using QuivusUtility.ProjectConstants;
using Quivus.Billing.DataAccess.DBUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System.Data.Common;

namespace Quivus.Billing.DataAccess1.DAO.EDI
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   12/28/2011
    /// </summary>
    public class EDIPaymentTransactionDAO
    {
        /// <summary>
        /// Author:     Shahid Munir
        /// Date:       12/16/2011
        /// to insert adjustment mapping data 
        /// </summary>
        /// <param name="adjustmentMappingDTO"></param>
        /// <returns></returns>
        public int InsertAdjustmentMapping(AdjustmentMappingDTO adjustmentMappingDTO)
        {
            int AdjustmentMappingID = 0;

            string sProc = BillingConstant.ADJUSTMENT_MAPPING_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@AdjustmentReasonCodeID", DbType.Int32, adjustmentMappingDTO.AdjustmentReasonCodeID);
                db.AddInParameter(command, "@AdjustmentGroupCodeTypeID", DbType.Int32, adjustmentMappingDTO.AdjustmentGroupCodeTypeID);
                db.AddInParameter(command, "@AdjustmentMappingTypeID", DbType.Int32, adjustmentMappingDTO.AdjustmentMappingTypeID);             
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, adjustmentMappingDTO.CreatedBy);

                db.AddOutParameter(command, "@AdjustmentMappingIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    AdjustmentMappingID = int.Parse(db.GetParameterValue(command, "@AdjustmentMappingIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return AdjustmentMappingID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to Insert/Update/Delete Adjustment mapping data 
        /// </summary>
        /// <param name="adjustmentMappingList"></param>
        /// <returns></returns>
        public bool ManageAdjustmentMapping(List<AdjustmentMappingDTO> adjustmentMappingList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.ADJUSTMENT_MAPPING_MANAGE_SP;
            DataTable dtAdjustmentMapping = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (adjustmentMappingList != null && adjustmentMappingList.Count > 0)
                {
                    dtAdjustmentMapping = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(adjustmentMappingList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpClaims = sqlCommand.Parameters.AddWithValue("@AdjustmentMappingTVP", dtAdjustmentMapping);
                tvpClaims.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpClaims.TypeName = BillingConstant.ADJUSTMENT_MAPPING_TVP;

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
                throw; 
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
        /// to insert adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeDTO"></param>
        /// <returns></returns>
        public int InsertAdjustmentReasonCode(AdjustmentReasonCodeDTO adjustmentReasonCodeDTO)
        {
            int AdjustmentReasonCodeID = 0;

            string sProc = BillingConstant.ADJUSTMENT_REASON_CODES_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                QuivusLogger.Info("Calling SP with command:: " + command);

                db.AddInParameter(command, "@AdjustmentReasonCodeID", DbType.Int32, adjustmentReasonCodeDTO.AdjustmentReasonCodeID);
                db.AddInParameter(command, "@AdjustmentReasonCode", DbType.String, adjustmentReasonCodeDTO.AdjustmentReasonCode);
                db.AddInParameter(command, "@Description", DbType.String, adjustmentReasonCodeDTO.Description);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, adjustmentReasonCodeDTO.CreatedBy);

                db.AddOutParameter(command, "@AdjustmentReasonCodeIDOUT", DbType.Int32, 1);


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    AdjustmentReasonCodeID = int.Parse(db.GetParameterValue(command, "@AdjustmentReasonCodeIDOUT").ToString());
                }

            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return AdjustmentReasonCodeID;
        }

        /// <summary>
        /// Author:  Shahid Munir
        /// Date:    12/28/2011
        /// to Insert/Update/Delete Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        public bool ManageAdjustmentReasonCodes(List<AdjustmentReasonCodeDTO> adjustmentReasonCodeList)
        {
            bool UpdateResult = false;

            string sProc = BillingConstant.ADJUSTMENT_REASON_CODES_MANAGE_SP;
            DataTable dtAdjustmentReasonCodes = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (adjustmentReasonCodeList != null && adjustmentReasonCodeList.Count > 0)
                {
                    dtAdjustmentReasonCodes = GenericDTOListToDataTableConverter.ConvertDTOListToDataTable(adjustmentReasonCodeList);
                }

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpAdjustmentReasonCodes = sqlCommand.Parameters.AddWithValue("@AdjustmentReasonCodesTVP", dtAdjustmentReasonCodes);
                tvpAdjustmentReasonCodes.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpAdjustmentReasonCodes.TypeName = BillingConstant.ADJUSTMENT_MAPPING_TVP;

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
        /// Date:    12/28/2011
        /// to load all Adjustment reason codes data 
        /// </summary>
        /// <param name="adjustmentReasonCodeList"></param>
        /// <returns></returns>
        public List<AdjustmentReasonCodeDTO> LoadAdjustmentReasonCodes()
        {
            List<AdjustmentReasonCodeDTO> AdjustmentReasonCodeList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.ADJUSTMENT_REASON_CODES_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                AdjustmentReasonCodeList = new List<AdjustmentReasonCodeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);
               

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        Quivus.Utility.GenericDataReaderToDTOConverter.ConvertToDTOList(AdjustmentReasonCodeList, objIReader, typeof(AdjustmentReasonCodeDTO));
                    }
                }
            }
            catch
            {

                throw;
            }
            return AdjustmentReasonCodeList;
        }       
    }
}
