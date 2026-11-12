using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

using Quivus.Model.Common;
using Quivus.Model.User.Patient;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using UserManagerService.DataAccess;

namespace UserManagerService.DataAccess
{
    /// <summary>    
    /// Author Mansoor
    /// Date 21 JAn  2011
    /// </summary>
    public class AllergyDAO : BaseDAO
    {
        public List<AllergySearchDTO> SearchAllergyTypeByName(string allergyName)
        {
            List<AllergySearchDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.ALLERGY_SEARCH_BY_NAME_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<AllergySearchDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@AllergyTypeName", DbType.String, allergyName);

                using (objIReader = db.ExecuteReader(command))
                {
                    GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AllergySearchDTO));
                }
            }            
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }           
            return list;
        }

        public bool SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy)
        {
            bool UpdateResult = false;
            string sProc = UserConstant.PATIENT_ALLERGY_INSERT_SINGLE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@AllergyID", DbType.Int32, patientAllergy.AllergyType.AllergyTypeIdSeq);

                if(string.IsNullOrEmpty(patientAllergy.IsActive))                 
                {
                    db.AddInParameter(command, "@IsActive", DbType.Boolean, DBNull.Value);
                }
                else if (patientAllergy.IsActive.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || patientAllergy.IsActive.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                {
                    db.AddInParameter(command, "@IsActive", DbType.Boolean, bool.Parse(patientAllergy.IsActive));
                }
                else
                {
                    db.AddInParameter(command, "@IsActive", DbType.Boolean, DBNull.Value);
                }

                db.AddInParameter(command, "@Reaction", DbType.String, patientAllergy.Reaction);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, createdBy);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    UpdateResult = false;
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }
    }
}
