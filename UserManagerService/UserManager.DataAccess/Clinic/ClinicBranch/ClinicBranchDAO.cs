using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using UserManagerService.DataAccess.DB_Util;
using Quivus.Model.Clinic;

namespace UserManager.DataAccess.Clinic.ClinicBranch
{
    public class ClinicBranchDAO
    {
        /// <summary>
        /// Author Sami
        /// Date 15 Dec 2011      
        /// This method will return clinic, clinicbranch and station info for soap note
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public ClinicDTO LoadClinicBranchAndStationInfo(int stationId)
        {
            ClinicDTO clinic = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_CLINICBRANCH_AND_STATION_LOAD_BY_STATION_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@StationId", DbType.Int32, stationId);

                using (objIReader = db.ExecuteReader(command))
                {
                    clinic = DataReaderToDTOConverter.ConvertToClinicDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return clinic;
        }



        public List<int> LoadAllClinicBranch()
        {
            List<int> clinicBranchIds = null;

            IDataReader objReader = null;
            string sProc = UserConstant.CLINIC_BRANCH_LOAD_ALL_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

              

                using (objReader = db.ExecuteReader(command))
                {
                    if (objReader != null)
                    {
                        while (objReader.Read())
                        {
                            if (clinicBranchIds == null)
                                clinicBranchIds = new List<int>();

                            if (objReader["ClinicBranchID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchID"].ToString()))
                            {
                                int id = Int32.Parse(objReader["ClinicBranchID"].ToString());
                                clinicBranchIds.Add(id);
                            }

                        }
                    }
                    
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return clinicBranchIds;
        }
      

        public List<int> LoadClinicBranchUserIDs(int userTypeId,int clinicBranchId)
        {
            List<int> clinicBranchUIds = null;

            IDataReader objReader = null;
            string sProc = UserConstant.LOAD_CLINICBRANCH_USER_BY_TYPEID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@userTypeIdSeq", DbType.Int32, userTypeId);
                db.AddInParameter(command, "@clinicBranchId", DbType.Int32, clinicBranchId);

                using (objReader = db.ExecuteReader(command))
                {
                    if (objReader != null)
                    {
                        while (objReader.Read())
                        {
                            if (clinicBranchUIds == null)
                                clinicBranchUIds = new List<int>();

                            if (objReader["UserID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["UserID"].ToString()))
                            {
                                int id = Int32.Parse(objReader["UserID"].ToString());
                                clinicBranchUIds.Add(id);
                            }

                        }
                    }

                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return clinicBranchUIds;
        }
    }
}
