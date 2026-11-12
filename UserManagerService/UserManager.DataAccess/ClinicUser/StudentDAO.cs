using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagerService.DataAccess;
using QuivusModel.User;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess.DB_Util;

namespace UserManager.DataAccess.ClinicUser
{
    public class StudentDAO : BaseDAO
    {

        public int ManageStudentProfile(int userId, StudentDTO nDTO, bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.STUDENT_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.STUDENT_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, userId);

                if (nDTO.SpecialityType != null)
                {
                    db.AddInParameter(command, "@SpecialityId", DbType.Int32, nDTO.SpecialityType.SpecialityTypeID);
                }
                else
                {
                    db.AddInParameter(command, "@SpecialityId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@RecGuid", DbType.Guid, nDTO.RecGuid);
                db.AddInParameter(command, "@WORK_START", DbType.String, nDTO.WorkStartTime);
                db.AddInParameter(command, "@WORK_END", DbType.String, nDTO.WorkEndTime);
                db.AddInParameter(command, "@WORK_DAYS", DbType.String, nDTO.WorkingDays);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, nDTO.IsProfileComplete);
                db.AddInParameter(command, "@CreatedBy", DbType.Boolean, nDTO.CreatedBy);





                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@StudentId", DbType.Int32, nDTO.StudentIdSeq);
                    db.AddInParameter(command, "@CreatedAt", DbType.Int32, nDTO.CREATE_DT);
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, nDTO.ModifiedBy);
                    db.AddInParameter(command, "@Active", DbType.Int32, nDTO.IsActive);
                    db.AddInParameter(command, "@IsDeleted", DbType.Int32, nDTO.IsDeleted);
                    db.AddInParameter(command, "@Locked", DbType.Int32, nDTO.IsLocked);

                }
                else
                {
                    db.AddOutParameter(command, "@StudentIDOUT", DbType.Int32, 1);

                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@StudentIDOUT").ToString());
                    }
                    else
                    {
                        UpdateResult = nDTO.StudentIdSeq;
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        public StudentDTO LoadStudentDTOById(int nId)
        {
            StudentDTO dto = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.STUDENT_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@StudentId", DbType.Int32, nId);

                using (objIReader = db.ExecuteReader(command))
                {
                    dto = DataReaderToDTOConverter.ConvertToStudentDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return dto;
        }

        [Obsolete]
        public int ChangeStudentActiveStatus(int studentId, bool isActive)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.STUDENT_CHANGE_ACTIVE_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@studentId", DbType.Int32, studentId);
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
        public int ChangeStudentLockedStatus(int studentId, bool isLocked)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.STUDENT_CHANGE_LOCK_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@studentId", DbType.Int32, studentId);
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
