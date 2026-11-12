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
    public class ReceptionistDAO : BaseDAO
    {

        public int ManageReceptionistProfile(int userId, ReceptionistDTO rDTO, bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.RERCEPTIONSIT_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.RERCEPTIONSIT_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name


               

                db.AddInParameter(command, "@UserID", DbType.Int32, userId);


                db.AddInParameter(command, "@RecGuid", DbType.Guid, rDTO.RecGuid);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, rDTO.IsProfileComplete);
                db.AddInParameter(command, "@CreatedBy", DbType.Boolean, rDTO.CreatedBy);
                db.AddInParameter(command, "@ModifiedBy", DbType.Boolean, rDTO.ModifiedBy);


                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@ReceptionistId", DbType.Int32, rDTO.ReceptionistId);
                    db.AddInParameter(command, "@CreatedAt", DbType.Int32, rDTO.CreatedDate);
                    db.AddInParameter(command, "@Active", DbType.Int32, rDTO.IsActive);
                    db.AddInParameter(command, "@IsDeleted", DbType.Int32, rDTO.IsDeleted);
                    db.AddInParameter(command, "@Locked", DbType.Int32, rDTO.IsLocked);

                }
                else
                {
                    db.AddOutParameter(command, "@ReceptionistIdOUT", DbType.Int32, 1);

                }


                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@ReceptionistIdOUT").ToString());
                    }
                    else
                    {
                        UpdateResult = rDTO.ReceptionistId;
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        public ReceptionistDTO LoadReceptionistDTOById(int rId)
        {
            ReceptionistDTO dto = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.RERCEPTIONSIT_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ReceptionistId", DbType.Int32, rId);

                using (objIReader = db.ExecuteReader(command))
                {
                    dto = DataReaderToDTOConverter.ConvertToReceptionistDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return dto;
        }




        [Obsolete]
        public int ChangeReceptionistActiveStatus(int rId, bool isActive)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.RERCEPTIONSIT_CHANGE_ACTIVE_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@receptionistId", DbType.Int32, rId);
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
        public int ChangeReceptionistLockedStatus(int rId, bool isLocked)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.RERCEPTIONSIT_CHANGE_LOCK_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@receptionistId", DbType.Int32, rId);
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
