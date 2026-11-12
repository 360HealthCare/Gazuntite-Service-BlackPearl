using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Billing.PendingBills;
using QuivusUtility.ProjectConstants;
using System.Data;
using System.Data.SqlClient;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using UserManagerService.DataAccess.DB_Util;
using Quivus.Utility;
using QuivusModel.Billing.User;
using System.Data.Common;

namespace UserManager.DataAccess.Billing
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/31/2012
    /// for Billing related Data access object
    /// </summary>
    public class BillingDAO
    {
        // Billing Service related Mehtods

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/26/2012
        /// to get Patient info for super bill
        /// </summary>
        /// <param name="PatientIdList"></param>       
        /// <returns></returns>
        public List<SuperBillPatientInfoDTO> GetPatientBillingInfoByPatientId(List<int> PatientIdList)
        {
            List<SuperBillPatientInfoDTO> ResponseList = null;

            bool isConnectionOpen = false;

            string sProc = BillingConstant.PATIENT_Billing_INFO_LOAD_BY_PATIENT_ID_SP;

            DataTable PatientIdTable = null;
            SqlConnection connection = null;
            SqlDataReader objIReader = null;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                PatientIdTable = DTOToTableConverter.UserIdListConverter(PatientIdList);
                ResponseList = new List<SuperBillPatientInfoDTO>();

                Database DB = DatabaseFactory.CreateDatabase(); // Load default DB
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpReqIds = sqlCommand.Parameters.AddWithValue("@Rec_Ids", PatientIdTable);
                tvpReqIds.SqlDbType = SqlDbType.Structured;
                tvpReqIds.TypeName = ProjectConstant.SEARCH_ENGINE_TYPE_TVP;

                connection.Open();
                isConnectionOpen = true;

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    GenericDataReaderToDTOConverter.ConvertToDTOList(ResponseList, objIReader, typeof(SuperBillPatientInfoDTO));
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return ResponseList;
        }       
        
    }
}
