using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagerService.DataAccess;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using Quivus.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess.DB_Util;

namespace UserManager.DataAccess.ClinicUser
{
    public class NursePractionerDAO : BaseDAO
    {


        public int ManageNursePractionerProfile(int userId, NursePractitionerDTO npDTO, bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.NURSEPRACTIONER_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.NURSEPRACTIONER_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, userId);

                if (npDTO.SpecialityType != null)
                {
                    db.AddInParameter(command, "@SpecialityId", DbType.Int32, npDTO.SpecialityType.SpecialityTypeID);
                }
                else
                {
                    db.AddInParameter(command, "@SpecialityId", DbType.Int32, DBNull.Value);
                }



                db.AddInParameter(command, "@RecGuid", DbType.Guid, npDTO.RecGuid);
                db.AddInParameter(command, "@WorkStart", DbType.String, npDTO.WorkStartTime);
                db.AddInParameter(command, "@WorkEnd", DbType.String, npDTO.WorkEndTime);
                db.AddInParameter(command, "@WorkDays", DbType.String, npDTO.WorkingDays);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, npDTO.IsProfileComplete);
                db.AddInParameter(command, "@CreatedBy", DbType.Boolean, npDTO.CreatedBy);

                if (IsUpdate) // Update Case 
                {

                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, npDTO.ModifiedBy);
                    db.AddInParameter(command, "@NursePractitionerId", DbType.Int32, npDTO.NursePractitionerId);
                    db.AddInParameter(command, "@CreatedAt", DbType.Int32, npDTO.CreatedDate);
                    db.AddInParameter(command, "@Active", DbType.Int32, npDTO.IsActive);
                    db.AddInParameter(command, "@IsDeleted", DbType.Int32, npDTO.IsDeleted);
                    db.AddInParameter(command, "@Locked", DbType.Int32, npDTO.IsLocked);

                }
                else
                {
                    db.AddOutParameter(command, "@NursePractitionerIDOUT", DbType.Int32, 1);

                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@NursePractitionerIDOUT").ToString());
                    }
                    else
                    {
                        UpdateResult = npDTO.NursePractitionerId;
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        public NursePractitionerDTO LoadNursePractitionerDTOById(int npId)
        {
            NursePractitionerDTO nursePractitionerDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.NURSEPRACTIONER_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@NursePractionerID", DbType.Int32, npId);

                using (objIReader = db.ExecuteReader(command))
                {
                    nursePractitionerDTO = DataReaderToDTOConverter.ConvertToNursePractionerDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return nursePractitionerDTO;
        }



        [Obsolete]
        public int ChangeNursePractionerActiveStatus(int npId, bool isActive)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.NURSEPRACTIONER_CHANGE_ACTIVE_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@nursePractitionerId", DbType.Int32, npId);
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
        public int ChangeNursePractionerLockedStatus(int npId, bool isLocked)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.NURSEPRACTIONER_CHANGE_LOCK_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@nursePractitionerId", DbType.Int32, npId);
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
