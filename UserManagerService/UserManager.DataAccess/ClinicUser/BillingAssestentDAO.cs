using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagerService.DataAccess;
using Quivus.Model.User;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess.DB_Util;

namespace UserManager.DataAccess.ClinicUser
{
    public class BillingAssestentDAO : BaseDAO
    {

        public int ManageBillingAssestent(int userId, BillingAssistantDTO ltDTO, bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.BILLING_ASS_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.BILLING_ASS_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

	 
     


                db.AddInParameter(command, "@UserID", DbType.Int32, userId);


                db.AddInParameter(command, "@RecGuid", DbType.Guid, ltDTO.RecGuid);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, ltDTO.IsProfileComplete);
                db.AddInParameter(command, "@CreatedBy", DbType.Boolean, ltDTO.CreatedBy);
                db.AddInParameter(command, "@ModifiedBy", DbType.Boolean, ltDTO.ModifiedBy);




                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@BillingAssistantId", DbType.Int32, ltDTO.BillingAssistantId);
                    db.AddInParameter(command, "@CreatedAt", DbType.Int32, ltDTO.CreatedDate);
                    db.AddInParameter(command, "@Active", DbType.Int32, ltDTO.IsActive);
                    db.AddInParameter(command, "@IsDeleted", DbType.Int32, ltDTO.IsDeleted);
                    db.AddInParameter(command, "@Locked", DbType.Int32, ltDTO.IsLocked);

                }
                else
                {
                    db.AddOutParameter(command, "@BillingAssistantIDOUT", DbType.Int32, 1);
                    
                }
                

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@BillingAssistantIDOUT").ToString());
                    }
                    else
                    {
                        UpdateResult = ltDTO.BillingAssistantId;
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        public BillingAssistantDTO LoadBillingAssistantDTOById(int nId)
        {
            BillingAssistantDTO ltDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.BILLING_ASS_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@BillingAssistentId", DbType.Int32, nId);

                using (objIReader = db.ExecuteReader(command))
                {
                    ltDTO = DataReaderToDTOConverter.ConvertToBillingAssistant(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return ltDTO;
        }


        [Obsolete]
        public int ChangeBillingAssistentActiveStatus(int baId, bool isActive)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.BILLING_ASS_CHANGE_ACTIVE_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@baId", DbType.Int32, baId);
                db.AddInParameter(command, "@IsActive", DbType.Boolean, isActive);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_UPDATED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    changed = 1;
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }

            return changed;
        }



        [Obsolete]
        public int ChangeBillingAssistentLockedStatus(int baId, bool isLocked)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.BILLING_ASS_CHANGE_LOCK_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@baId", DbType.Int32, baId);
                db.AddInParameter(command, "@IsLocked", DbType.Boolean, isLocked);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_UPDATED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    changed = 1;
                }

            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }

            return changed;
        }
    }
}
