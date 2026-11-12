using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagerService.DataAccess;
using Quivus.Utility;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess.DB_Util;

namespace UserManager.DataAccess.ClinicUser
{
    public class DoctorDAO : BaseDAO
    {

        /// <summary>
        /// Author Ismail
        /// Date 05 Oct 2011   
        /// Save/Update the patient profile data with the user id given
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="patientDTO"></param>
        /// <param name="IsUpdate"></param>
        /// <returns></returns>
        public int ManageDoctorProfile(int userId, DoctorDTO doctorDTO, bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.DOCTOR_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.DOCTOR_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, userId);

                if (doctorDTO.SpecialityType != null)
                {
                    db.AddInParameter(command, "@SpecialityId", DbType.Int32, doctorDTO.SpecialityType.SpecialityTypeID);
                }
                else
                {
                    db.AddInParameter(command, "@SpecialityId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@NPI", DbType.String, doctorDTO.NPI);
                db.AddInParameter(command, "@UPIN", DbType.String, doctorDTO.UPIN);
                db.AddInParameter(command, "@DAE", DbType.String, doctorDTO.DAE);
                db.AddInParameter(command, "@TPI", DbType.String, doctorDTO.TPI);
                db.AddInParameter(command, "@DentistLicenseNumber", DbType.String, doctorDTO.DentistLicenseNumber);
                db.AddInParameter(command, "@PriorAuthorization", DbType.String, doctorDTO.PriorAuthorization);
                db.AddInParameter(command, "@RecGuid", DbType.Guid, doctorDTO.RecGuid);
                db.AddInParameter(command, "@WorkStart", DbType.String, doctorDTO.WorkStartTime);
                db.AddInParameter(command, "@WorkEnd", DbType.String, doctorDTO.WorkEndTime);
                db.AddInParameter(command, "@WorkDays", DbType.String, doctorDTO.WorkingDays);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, doctorDTO.IsProfileComplete);

                if (!IsUpdate)// Ignore In case Of Update User Section i.e Insert Case 
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, doctorDTO.CreatedBy);
                    db.AddOutParameter(command, "@DoctorIDOUT", DbType.Int32, 1);
                }
                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@DoctorID", DbType.Int32, doctorDTO.DoctorId);
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, doctorDTO.ModifiedBy);
                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@DoctorIDOUT").ToString());
                    }
                    else
                    {
                        UpdateResult = doctorDTO.DoctorId;
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public DoctorDTO LoadDoctorByDoctorId(int doctorId)
        {
            DoctorDTO doctorDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.DOCTOR_LOAD_BY_DOCTOR_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@DoctorID", DbType.Int32, doctorId);

                using (objIReader = db.ExecuteReader(command))
                {
                    doctorDTO = DataReaderToDTOConverter.ConvertToDoctorDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return doctorDTO;
        }

        [Obsolete]
        public int ChangeDoctorActiveStatus(int docId, bool isActive)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.DOCTOR_CHANGE_ACTIVE_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@doctorId", DbType.Int32, docId);
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
        public int ChangeDoctorLockedStatus(int docId, bool isLocked)
        {
            int changed = 0;

            try
            {
                string sProc = UserConstant.DOCTOR_CHANGE_LOCK_STATUS_SP;

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@doctorId", DbType.Int32, docId);
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
