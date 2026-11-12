using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Configuration;
using System.Reflection;
using System.Data.SqlClient;
using Quivus.Utility;

using Quivus.Model.User;
using Quivus.Model.User.Patient;
using Quivus.Model.PatientProfileCustomization;
using UserManagerService.DataAccess.DB_Util;

using Quivus.Model.QuickLink;
using Quivus.Model.Search;
using Quivus.Model.RecentPatient;
using Quivus.Utility.Utilities;
using Quivus.Utility.CustomException;
using UserManager.DataAccess.DB_Util;
using QuivusModel.User.Patient;
using QuivusModel.Clinic;

namespace UserManagerService.DataAccess.Patient
{
    public class PatientDAO : BaseDAO
    {
        /// <summary>
        /// Author Mansoor
        /// Updated By Ismail
        /// Updated At 21 june 2011
        /// Method Uses new TVP with changes
        /// </summary>
        /// <param name="EmergencyInsertContactList"></param>
        /// <param name="EmergencyUpdateContactList"></param>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int ManagePatientEmergencyContact(List<PatientEmergencyContactDTO> EmergencyInsertContactList, List<PatientEmergencyContactDTO> EmergencyUpdateContactList,
            int PatientId, int CreatedBy)
        {
            bool SqlConnectioStatus = false;

            int UpdateResult = 0;
            string sProc = UserConstant.MANAGE_PATIENT_EMERGENCY_CONTACT_SP;
            SqlConnection connection = null;
            SqlCommand sqlCommand = null;

            foreach (var test in EmergencyInsertContactList)
            {
                QuivusLogger.Info("value of isDeleted is  " + test.IsDeleted);
                QuivusLogger.Info("value of isDirty is  " + test.IsDeleted);
            }
            try
            {
                if (EmergencyInsertContactList != null && EmergencyInsertContactList.Count > 0)
                {
                    SqlConnectioStatus = true;

                    DataTable emergencyInsertList = DTOToTableConverter.EmergencyContactDTOListConverter(EmergencyInsertContactList);
                   // Code changed by baber on 28 of aug due to a exception is deleted is recieved null at sp level
                    if (emergencyInsertList != null )
                    {
                        QuivusLogger.Info("value of isDeleted IN TABLE IS   " + emergencyInsertList.Rows.Count);
                        try
                        {
                            QuivusLogger.Info("value of isDeleted is  " + emergencyInsertList.Rows[0]["isDeleted"]);
                            QuivusLogger.Info("value of isDeleted is  " + emergencyInsertList.Rows[1]["isDeleted"]);
                        }
                        catch { }

                    }
                 // Commented As Not In Use    DataTable emergencyUpdateList = DTOToTableConverter.EmergencyContactDTOListConverter(EmergencyUpdateContactList); // Use Update List to Update Record

                    QuivusLogger.Info("Calling SP:: " + sProc);

                    Database DB = DatabaseFactory.CreateDatabase();

                    connection = new SqlConnection(DB.ConnectionString);

                    sqlCommand = new SqlCommand(sProc, connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure; // Specify Command Type To Store Procedure in Case Of Saving TVP Other Wise you will get Error

                    sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);

                    SqlParameter tvpEmergencyContact = sqlCommand.Parameters.AddWithValue("@PatientEmergencyContactTVP", emergencyInsertList);
                    tvpEmergencyContact.SqlDbType = SqlDbType.Structured;
                    tvpEmergencyContact.TypeName = ProjectConstant.EMERGENCY_CONTACT_TVP;

                    //sqlCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    sqlCommand.Parameters.AddWithValue("@ManagedBy", CreatedBy); // SP Updated By Ismail

                    connection.Open();

                    if (sqlCommand.ExecuteNonQuery() == 0)
                    {
                        throw new InsertionFailedException(ProjectConstant.INSERTION_FAILED_EXCEPTION);
                    }
                    else
                    {
                        //M set by me - 07 Feb 2011
                        UpdateResult = 1;
                    }
                } // If List not Null
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (SqlConnectioStatus)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save the patient profile data with the user id given
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        public int SavePatientBasicProfile(int userId, PatientDTO patientDTO)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.PATIENT_INSERT_SP;
            try
            {
                //if (IsUpdate)
                //{
                //    sProc = UserConstant.PATIENT_UPDATE_SP;
                
                //}
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, userId);

                if (patientDTO.MaritalStatusType != null)
                {
                    db.AddInParameter(command, "@MaritalStatusId", DbType.Int32, patientDTO.MaritalStatusType.MaritalStatusId);
                }
                else
                {
                    db.AddInParameter(command, "@MaritalStatusId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(command, "@DateOfBirth", DbType.DateTime, patientDTO.DateOfBirth);
                db.AddInParameter(command, "@QuivusId", DbType.String, patientDTO.QuivusId);

                db.AddInParameter(command, "@AllowEmailCommunication", DbType.Boolean, patientDTO.AllowEmailCommunication);
                db.AddInParameter(command, "@SpouseName", DbType.String, patientDTO.SpouseName);
                db.AddInParameter(command, "@MotherMaidenName", DbType.String, patientDTO.MotherMaidenName);
                db.AddInParameter(command, "@FatherName", DbType.String, patientDTO.FatherName);
                
                db.AddInParameter(command, "@IVR_PINCODE", DbType.Int32, patientDTO.IVR_PINCODE);
                db.AddInParameter(command, "@PatientStatusId", DbType.Int32, patientDTO.StatusType.StatusID);

                db.AddInParameter(command, "@FileId", DbType.String, "");

                db.AddInParameter(command, "@ExemptReporting", DbType.Boolean, patientDTO.IsReportingExempt);

                
                if (string.IsNullOrEmpty(patientDTO.MedicareNumber))
                {
                    db.AddInParameter(command, "@MedicareNumber", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(command, "@MedicareNumber", DbType.String, patientDTO.MedicareNumber);
                }

                if (patientDTO.RaceType != null)
                {
                    db.AddInParameter(command, "@RaceId", DbType.Int32, patientDTO.RaceType.RaceId);
                }
                else
                {
                    db.AddInParameter(command, "@RaceId", DbType.Int32, DBNull.Value);
                }

                if (patientDTO.EthinicityType != null)
                {
                    db.AddInParameter(command, "@EthinicityId", DbType.Int32, patientDTO.EthinicityType.EthinicityId);
                }
                else
                {
                    db.AddInParameter(command, "@EthinicityId", DbType.Int32, DBNull.Value);
                }

                if (patientDTO.OccupationType != null)
                {
                    db.AddInParameter(command, "@OccupationId", DbType.Int32, patientDTO.OccupationType.OccupationId);
                }
                else
                {
                    db.AddInParameter(command, "@OccupationId", DbType.Int32, DBNull.Value);
                }

                if (string.IsNullOrEmpty(patientDTO.AlcoholStatus))
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.String, DBNull.Value);
                }
                else if (patientDTO.AlcoholStatus.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || patientDTO.AlcoholStatus.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.Boolean, bool.Parse(patientDTO.AlcoholStatus));
                }
                else
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(patientDTO.AlcoholStatusData))
                {
                    db.AddInParameter(command, "@AlcoholStatusData", DbType.String, patientDTO.AlcoholStatusData);
                }
                else
                {
                    db.AddInParameter(command, "@AlcoholStatusData", DbType.String, DBNull.Value);
                }

                if (string.IsNullOrEmpty(patientDTO.DrugStatus))
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.String, DBNull.Value);
                }
                else if (patientDTO.DrugStatus.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || patientDTO.DrugStatus.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.Boolean, bool.Parse(patientDTO.DrugStatus));
                }
                else
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(patientDTO.DrugStatusData))
                {
                    db.AddInParameter(command, "@DrugStatusData", DbType.String, patientDTO.DrugStatusData);
                }
                else
                {
                    db.AddInParameter(command, "@DrugStatusData", DbType.String, DBNull.Value);
                } 

                db.AddInParameter(command, "@EmployerName", DbType.String, patientDTO.EmployerName);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, patientDTO.IsProfileComplete);
                db.AddInParameter(command, "@RecGuid", DbType.Guid, patientDTO.RecGuid);

                if (patientDTO.SmokingType != null)
                {
                    db.AddInParameter(command, "@SmokingTypeId", DbType.Int32, patientDTO.SmokingType.SmokingTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@SmokingTypeId", DbType.Int32, null);
                }

                db.AddInParameter(command, "@CreatedBy", DbType.Int32, patientDTO.CreatedBy);
                db.AddOutParameter(command, "@PatientIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = int.Parse(db.GetParameterValue(command, "@PatientIDOUT").ToString());
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Ismail
        /// Date 09 Mar 2011
        /// Save/Update the patient profile data with the user id given
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="patientDTO"></param>
        /// <param name="IsUpdate"></param>
        /// <returns></returns>
        public int SavePatientBasicProfile(int userId, PatientDTO patientDTO,bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.PATIENT_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.PATIENT_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, userId);

                if (patientDTO.MaritalStatusType != null)
                {
                    db.AddInParameter(command, "@MaritalStatusId", DbType.Int32, patientDTO.MaritalStatusType.MaritalStatusId);
                }
                else
                {
                    db.AddInParameter(command, "@MaritalStatusId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(command, "@DateOfBirth", DbType.DateTime, patientDTO.DateOfBirth);
                

                db.AddInParameter(command, "@AllowEmailCommunication", DbType.Boolean, patientDTO.AllowEmailCommunication);
                db.AddInParameter(command, "@SpouseName", DbType.String, patientDTO.SpouseName);
                db.AddInParameter(command, "@MotherMaidenName", DbType.String, patientDTO.MotherMaidenName);
                db.AddInParameter(command, "@FatherName", DbType.String, patientDTO.FatherName);

                db.AddInParameter(command, "@IVR_PINCODE", DbType.Int32, patientDTO.IVR_PINCODE);
                db.AddInParameter(command, "@PatientStatusId", DbType.Int32, patientDTO.StatusType.StatusID);

                db.AddInParameter(command, "@FileId", DbType.String, "");

                db.AddInParameter(command, "@ExemptReporting", DbType.Boolean, patientDTO.IsReportingExempt);


                if (string.IsNullOrEmpty(patientDTO.MedicareNumber))
                {
                    db.AddInParameter(command, "@MedicareNumber", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(command, "@MedicareNumber", DbType.String, patientDTO.MedicareNumber);
                }

                if (patientDTO.RaceType != null)
                {
                    db.AddInParameter(command, "@RaceId", DbType.Int32, patientDTO.RaceType.RaceId);
                }
                else
                {
                    db.AddInParameter(command, "@RaceId", DbType.Int32, DBNull.Value);
                }

                if (patientDTO.EthinicityType != null)
                {
                    db.AddInParameter(command, "@EthinicityId", DbType.Int32, patientDTO.EthinicityType.EthinicityId);
                }
                else
                {
                    db.AddInParameter(command, "@EthinicityId", DbType.Int32, DBNull.Value);
                }

                if (patientDTO.OccupationType != null)
                {
                    db.AddInParameter(command, "@OccupationId", DbType.Int32, patientDTO.OccupationType.OccupationId);
                }
                else
                {
                    db.AddInParameter(command, "@OccupationId", DbType.Int32, DBNull.Value);
                }

                if (string.IsNullOrEmpty(patientDTO.AlcoholStatus))
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.String, DBNull.Value);
                }
                else if (patientDTO.AlcoholStatus.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || patientDTO.AlcoholStatus.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.Boolean, bool.Parse(patientDTO.AlcoholStatus));
                }
                else
                {
                    db.AddInParameter(command, "@IsAlcoholStatusYes", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(patientDTO.AlcoholStatusData))
                {
                    db.AddInParameter(command, "@AlcoholStatusData", DbType.String, patientDTO.AlcoholStatusData);
                }
                else
                {
                    db.AddInParameter(command, "@AlcoholStatusData", DbType.String, DBNull.Value);
                }

                if (string.IsNullOrEmpty(patientDTO.DrugStatus))
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.String, DBNull.Value);
                }
                else if (patientDTO.DrugStatus.ToLower() == ProjectConstant.STRING_TRUE.ToLower() || patientDTO.DrugStatus.ToLower() == ProjectConstant.STRING_FALSE.ToLower())
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.Boolean, bool.Parse(patientDTO.DrugStatus));
                }
                else
                {
                    db.AddInParameter(command, "@IsDrugStatusYes", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(patientDTO.DrugStatusData))
                {
                    db.AddInParameter(command, "@DrugStatusData", DbType.String, patientDTO.DrugStatusData);
                }
                else
                {
                    db.AddInParameter(command, "@DrugStatusData", DbType.String, DBNull.Value);
                }

                db.AddInParameter(command, "@EmployerName", DbType.String, patientDTO.EmployerName);
                db.AddInParameter(command, "@IsProfileComplete", DbType.Boolean, patientDTO.IsProfileComplete);
                db.AddInParameter(command, "@RecGuid", DbType.Guid, patientDTO.RecGuid);

                if (patientDTO.SmokingType != null)
                {
                    db.AddInParameter(command, "@SmokingTypeId", DbType.Int32, patientDTO.SmokingType.SmokingTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@SmokingTypeId", DbType.Int32, DBNull.Value);
                }

                if (!IsUpdate)// Ignore In case Of Update User Section i.e Insert Case 
                {
                    db.AddInParameter(command, "@QuivusId", DbType.String, patientDTO.QuivusId);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, patientDTO.CreatedBy);
                    db.AddOutParameter(command, "@PatientIDOUT", DbType.Int32, 1);
                }
                if (IsUpdate) // Update Case 
                {
                    db.AddInParameter(command, "@PatientID", DbType.String, patientDTO.PatientId);
                    db.AddInParameter(command, "@ModifiedBy", DbType.String, patientDTO.ModifiedBy);
                }
                
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@PatientIDOUT").ToString());
                    }
                    else
                    {
                        UpdateResult = patientDTO.PatientId;
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
        /// Date 24 Jan 2011
        /// Save Patient Language Data
        /// Update Date 21 june 2011
        /// Update By Ismail :)
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="PatientLanguageDTOList"></param>
        /// <returns></returns>
        public bool SavePatientLanguages(int patientId, int managedByUserID, List<PatientLanguageDTO> PatientLanguageDTOList)
        {
            bool isConnectionOpen = false;
            bool result = false;
          //  string sProc = UserConstant.PATIENT_LANGUAGE_INSERT_SP;
            string sProc = UserConstant.PATIENT_LANGUAGE_TVP_SP; // Old One Discard By Ismail
            
            DataTable dtLanaguages = null;
            SqlConnection connection = null;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtLanaguages = DTOToTableConverter.PatientLanguageDTOListConverter(PatientLanguageDTOList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@PatientID", patientId);

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpLanguage = sqlCommand.Parameters.AddWithValue("@PatientLanguageTVP", dtLanaguages);
                tvpLanguage.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpLanguage.TypeName = ProjectConstant.PATIENT_LANGUAGE_TVP;

               // sqlCommand.Parameters.AddWithValue("@CreatedBy", managedByUserID);
                sqlCommand.Parameters.AddWithValue("@ManagedBy", managedByUserID);// Renamed of managed By 

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
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
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save Patient Gurantor
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        public int ManagePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO, bool IsUpdate)
        {
            int UpdateResult = 0;

            string sProc = UserConstant.PATIENT_GURANTOR_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.PATIENT_GURANTOR_UPDATE_SP;
                }
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientID);
                db.AddInParameter(command, "@GuarantorFirstName", DbType.String, patientGurantorDTO.GurantorFirstName);
                db.AddInParameter(command, "@GuarantorLastName", DbType.String, patientGurantorDTO.GuarantorLastName);

                if (patientGurantorDTO.GenderType != null)
                {
                    db.AddInParameter(command, "@GenderID", DbType.Int32, patientGurantorDTO.GenderType.GenderId);
                }
                else
                {
                    db.AddInParameter(command, "@GenderID", DbType.Int32, DBNull.Value);
                }

                if (patientGurantorDTO.RelationshipType != null)
                {
                    db.AddInParameter(command, "@RelationShipID", DbType.Int32, patientGurantorDTO.RelationshipType.RelationshipId);
                }
                else
                {
                    db.AddInParameter(command, "@RelationShipID", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(command, "@DateOfBirth", DbType.DateTime, patientGurantorDTO.DOB);
                db.AddInParameter(command, "@StatusID", DbType.Int32, patientGurantorDTO.StatusType.StatusID);

                if (patientGurantorDTO.MaritalStatusType != null)
                {
                    db.AddInParameter(command, "@MaritalStatusID", DbType.Int32, patientGurantorDTO.MaritalStatusType.MaritalStatusId);
                }
                else
                {
                    db.AddInParameter(command, "@MaritalStatusID", DbType.Int32, DBNull.Value);
                }

              
                db.AddInParameter(command, "@RecGuid", DbType.Guid, patientGurantorDTO.RecGuid);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedByUserID);
                    db.AddOutParameter(command, "@PatientGuarantorIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientGuarantorID", DbType.Int32, patientGurantorDTO.GuarantorId);
                    db.AddInParameter(command, "@ManagedBy", DbType.Int32, managedByUserID);
                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                   
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG + " or "  + ProjectConstant.RECORD_NOT_UPDATED_ERROR_MSG , ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@PatientGuarantorIDOUT").ToString());
                    else
                        UpdateResult = patientGurantorDTO.GuarantorId;
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
        /// Date 24 Jan 2011
        /// Save Patient Gurantor
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        public int ManagePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
         //   string sProc = UserConstant.PATIENT_INSURANCE_INSERT_SP;

            string sProc = UserConstant.PATIENT_INSURANCE_MANAGE_SP;
            //
            DataTable dtInsurance = null;
            SqlConnection connection = null;

            try
            {
                if (patientInsuranceDTOList != null && patientInsuranceDTOList.Count > 0)
                {
                    QuivusLogger.Info("Calling SP:: " + sProc);

                    dtInsurance = DTOToTableConverter.PatientInsuranceDTOListConverter(patientInsuranceDTOList, patientID);

                    Database DB = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB);
                    connection = new SqlConnection(DB.ConnectionString);

                    SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@PatientID", patientID);

                    //use SqlParameter to be able to define the TypeName of the parameter @PatientInsuranceTVP
                    SqlParameter tvpInsurance = sqlCommand.Parameters.AddWithValue("@PatientInsuranceTVP", dtInsurance);
                    tvpInsurance.SqlDbType = SqlDbType.Structured;
                    //should be the name of your TVP
                    tvpInsurance.TypeName = ProjectConstant.PATIENT_INSURANCE_TVP;

                   // sqlCommand.Parameters.AddWithValue("@CreatedBy", managedByUserID);
                    sqlCommand.Parameters.AddWithValue("@ManagedBy", managedByUserID);
                    
                    connection.Open();
                    isConnectionOpen = false;

                    if (sqlCommand.ExecuteNonQuery() == 0)
                    {
                        throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    }
                    else
                    {
                        UpdateResult = 1;
                    }
                }
                else
                {
                    QuivusLogger.Info("ManagePatientInsuranceInfo List is empty Do not Save In DB ");
                    UpdateResult = 1;
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
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// save the patient health Info like allergies, medical history, family history
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientHealthDTO"></param>
        public int ManagePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO)
        {
            int UpdateResult = 0;

            try
            {
                if (patientHealthDTO.PatientAllergyDTOList != null && patientHealthDTO.PatientAllergyDTOList.Count > 0)
                    ManagePatientAllergyInfo(patientID, managedByUserID, patientHealthDTO.PatientAllergyDTOList);

                if (patientHealthDTO.PatientMedicalHistoryDTOList != null && patientHealthDTO.PatientMedicalHistoryDTOList.Count > 0)
                    ManagePatientMedicalHistoryInfo(patientID, managedByUserID, patientHealthDTO.PatientMedicalHistoryDTOList);

                if (patientHealthDTO.PatientFamilyHistoryDTOList != null && patientHealthDTO.PatientFamilyHistoryDTOList.Count > 0)
                    ManagePatientFamilyHistoryInfo(patientID, managedByUserID, patientHealthDTO.PatientFamilyHistoryDTOList);

                UpdateResult = 1;
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }

            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// save the patient allergy info       
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientAllergyDTOList"></param>
        /// <returns></returns>
        private int ManagePatientAllergyInfo(int patientID, int managedByUserID, List<PatientAllergyDTO> patientAllergyDTOList)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
         //   string sProc = UserConstant.PATIENT_ALLERGY_INSERT_SP;
            string sProc = UserConstant.PATIENT_ALLERGY_MANAGE_SP;

            DataTable dtAllergy = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtAllergy = DTOToTableConverter.PatientAllergyDTOListConverter(patientAllergyDTOList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@PatientID", patientID);
              //  sqlCommand.Parameters.AddWithValue("@CreatedBy", managedByUserID);
                sqlCommand.Parameters.AddWithValue("@ManagedBy", managedByUserID);

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpAllergy = sqlCommand.Parameters.AddWithValue("@PatientHistoryAllergyTVP", dtAllergy);
                tvpAllergy.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpAllergy.TypeName = ProjectConstant.PATIENT_ALLERGY_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// save the patient medical history info               
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientAllergyDTOList"></param>
        /// <returns></returns>
        private int ManagePatientMedicalHistoryInfo(int patientID, int managedByUserID, List<PatientMedicalHistoryDTO> patientMedicalHistoryDTOList)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
           // string sProc = UserConstant.PATIENT_MEDICAL_HISTORY_INSERT_SP;
            string sProc = UserConstant.PATIENT_MEDICAL_HISTORY_MANAGE_SP;
            DataTable dtMedicalHistory = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtMedicalHistory = DTOToTableConverter.PatientMedicalHistoryDTOListConverter(patientMedicalHistoryDTOList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@PatientID", patientID);
                //sqlCommand.Parameters.AddWithValue("@CreatedBy", managedByUserID);
                sqlCommand.Parameters.AddWithValue("@ManagedBy", managedByUserID);

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpMedical = sqlCommand.Parameters.AddWithValue("@PatientHistoryMedical", dtMedicalHistory);
                tvpMedical.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpMedical.TypeName = ProjectConstant.PATIENT_MEDICAL_HISTORY_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        private int ManagePatientFamilyHistoryInfo(int patientID, int managedByUserID, List<PatientFamilyHistoryDTO> patientFamilyHistoryDTOList)
        {
            bool isConnectionOpen = false;
            int UpdateResult = 0;
          //  string sProc = UserConstant.PATIENT_FAMILY_HISTORY_INSERT_SP;
            string sProc = UserConstant.PATIENT_FAMILY_HISTORY_MANAGE_SP;
            DataTable dtFamilyHistory = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                dtFamilyHistory = DTOToTableConverter.PatientFamilyHistoryDTOListConverter(patientFamilyHistoryDTOList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@PatientID", patientID);
                //sqlCommand.Parameters.AddWithValue("@CreatedBy", managedByUserID);
                sqlCommand.Parameters.AddWithValue("@ManagedBy", managedByUserID);
                
                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpFamily = sqlCommand.Parameters.AddWithValue("@PatientHistoryFamily", dtFamilyHistory);
                tvpFamily.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpFamily.TypeName = ProjectConstant.PATIENT_FAMILY_HISTORY_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }


        /// <summary>
        /// Auhtor Mansoor
        /// Date 2 Feb 2011
        /// This Method will load the patients basic info for search      
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientSearchResultDTO LaodPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo, int pageSize, bool isDateSearch, DateTime DOB, bool isSSNSearch, string SSN, string dashSSN)
        {
            PatientSearchResultDTO searchResult = null;

            IDataReader objIReader = null;
           // SEARCH_PATIENT_LOAD_BY_CLINIC_ID_AND_CRITERIA_SP
            string sProc = UserConstant.SEARCH_PATIENT_LOAD_BY_CLINIC_ID_AND_CRITERIA_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchString);
                db.AddInParameter(command, "@PageNo", DbType.Int32, demandPageNo);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(command, "@IsDOBSearch", DbType.Boolean, isDateSearch);

                if (isDateSearch)
                {
                    db.AddInParameter(command, "@DOB", DbType.DateTime, DOB);
                }
                else
                {
                    db.AddInParameter(command, "@DOB", DbType.DateTime, DateTime.Now);
                }

                db.AddInParameter(command, "@IsSSNSearch", DbType.Boolean, isSSNSearch);
                db.AddInParameter(command, "@SSN", DbType.String, SSN);
                db.AddInParameter(command, "@DashSSN", DbType.String, dashSSN);

                using (objIReader = db.ExecuteReader(command))
                {
                    searchResult = DataReaderToDTOConverter.ConvertToPatientSearchResultDTO(objIReader);

                    searchResult.CurrentPageNo = demandPageNo;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return searchResult;
        }

        /// <summary>
        /// Auhtor Mansoor
        /// Date 30 Nov 2011
        /// This Method will load all the patients for document utility     
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientSearchResultDTO LaodPatientAll(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo, int pageSize, bool isDateSearch, DateTime DOB, bool isSSNSearch, string SSN, string dashSSN)
        {
            PatientSearchResultDTO searchResult = null;

            IDataReader objIReader = null;
            // SEARCH_PATIENT_LOAD_BY_CLINIC_ID_AND_CRITERIA_SP
            string sProc = UserConstant.SEARCH_PATIENT_LOAD_ALL;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchString);
                db.AddInParameter(command, "@PageNo", DbType.Int32, demandPageNo);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(command, "@IsDOBSearch", DbType.Boolean, isDateSearch);

                if (isDateSearch)
                {
                    db.AddInParameter(command, "@DOB", DbType.DateTime, DOB);
                }
                else
                {
                    db.AddInParameter(command, "@DOB", DbType.DateTime, DateTime.Now);
                }

                db.AddInParameter(command, "@IsSSNSearch", DbType.Boolean, isSSNSearch);
                db.AddInParameter(command, "@SSN", DbType.String, SSN);
                db.AddInParameter(command, "@DashSSN", DbType.String, dashSSN);

                using (objIReader = db.ExecuteReader(command))
                {
                    searchResult = DataReaderToDTOConverter.ConvertToPatientSearchResultDTO(objIReader);

                    searchResult.CurrentPageNo = demandPageNo;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return searchResult;
        }



        /// <summary>
        /// Auhtor Mansoor
        /// Date 1 Feb 2011
        /// this method will insert the last patient acccessed in the provider recent patients table
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId)
        {
            bool UpdateResult = false;
            string sProc = UserConstant.USER_RECENT_PATIENT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserId", DbType.Int32, providerUserId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                UpdateResult = true;
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        /// <summary>
        /// Auhtor Mansoor
        /// Date 1 Feb 2011
        /// this method will insert the last patient acccessed in the provider recent patients table
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>

        public List<RecentPatientDTO> LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId)
        {
            List<RecentPatientDTO> list = null;
            IDataReader objIReader = null;
            string sProc = UserConstant.USER_RECENT_PATIENT_LOAD_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<RecentPatientDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.String, providerUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.String, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    list = DataReaderToDTOConverter.ConvertToRecentPatientDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }        

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public PatientProfileBasicInfoDTO LoadPatientProfileBasicInfo(int patientId)
        {
            PatientProfileBasicInfoDTO basicProfileInfo = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_BASIC_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    basicProfileInfo = DataReaderToDTOConverter.ConvertToPatientProfileBasicInfoDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return basicProfileInfo;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public PatientInsuracneInfoDTO LoadPatientInsuranceInfo(int patientId)
        {
            PatientInsuracneInfoDTO insuranceInfo = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_INSURANCE_LOAD_FOR_TAG_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB);
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    insuranceInfo = DataReaderToDTOConverter.ConvertToPatientInsuranceInfoDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return insuranceInfo;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 04 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientAllergyInfoDTO> LoadPatientAllergiesByPatientId(int patientId)
        {
            List<PatientAllergyInfoDTO> allergyList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_ALLERGY_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    allergyList = DataReaderToDTOConverter.ConvertToPatientProfileAllergyInfo(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return allergyList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 04 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientDiagnosisInfoDTO> LoadPatientDiagnosisByPatientId(int patientId)
        {
            List<PatientDiagnosisInfoDTO> diagnosisList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_DIAGNOSIS_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.ENCOUNTER_DB);
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    diagnosisList = DataReaderToDTOConverter.ConvertToPatientProfileDiagnosisInfo(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return diagnosisList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 04 Feb 2011
        /// load the patient info to show in the left window panel
        /// Currenty it will return null as Requirements and DB sturcture is not provided
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientImmunizationInfoDTO> LoadPatientImmunizationByPatientId(int patientId)
        {
            List<PatientImmunizationInfoDTO> immunizationList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_IMMUNIZATION_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.ENCOUNTER_DB);
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                
                immunizationList = new List<PatientImmunizationInfoDTO>();

                using (objIReader = db.ExecuteReader(command))
                {

                     immunizationList = DataReaderToDTOConverter.ConvertToPatientProfileImmunizationInfo(objIReader);
                }

                ///
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return immunizationList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 04 Feb 2011
        /// load the patient info to show in the left window panel
        /// Currenty it will return null as Requirements and DB sturcture is not provided
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientChronicMedicationInfoDTO> LoadPatientMedicationByPatientId(int patientId)
        {
            List<PatientChronicMedicationInfoDTO> medicationList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_MEDICATION_INFO_LOAD_FOR_TAG_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.ENCOUNTER_DB);
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                //M TODO: implement it properly
                ///

                medicationList = new List<PatientChronicMedicationInfoDTO>();

                using (objIReader = db.ExecuteReader(command))
                {
                    medicationList = DataReaderToDTOConverter.ConvertToPatientProfileMedicationInfo(objIReader);
                }
                ///
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return medicationList;
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date 4 feb 2011
        /// Method Used to load insurance Info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public List<PatientInsuracneInfoDTO> LoadPatientInsuranceForProvider(int patientId)
        {
            List<PatientInsuracneInfoDTO> insuranceList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_INSURANCE_INFO_LOAD_FOR_TAG_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB);
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    insuranceList = DataReaderToDTOConverter.ConvertToPatientInsuranceInfo(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return insuranceList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, basic user info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public BaseUserDTO LoadPatientBaseUserInfoByPatientIdForCompleteProfile(int patientId)
        {
            BaseUserDTO userDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.USER_INFO_LOAD_ALL_BY_PATIENT_ID_SP;
            
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    userDTO = DataReaderToDTOConverter.ConvertToBaseUserDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return userDTO;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, profile info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public PatientDTO LoadPatientInfoByPatientIdForCompleteProfile(int patientId)
        {
            PatientDTO objPatientDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_INFO_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    objPatientDTO = DataReaderToDTOConverter.ConvertToPatientDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return objPatientDTO;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, lanaguages
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientLanguageDTO> LoadPatientLanguageInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientLanguageDTO> PatientLanguageList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_LANGUAGE_INFO_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    PatientLanguageList = DataReaderToDTOConverter.ConvertToPatientLanguageDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return PatientLanguageList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, emergency conatct
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientEmergencyContactDTO> LoadPatientEmergencyContactInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientEmergencyContactDTO> PatientEmergencyContactList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_EMERGENCY_CONATCT_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    PatientEmergencyContactList = DataReaderToDTOConverter.ConvertToPatientEmergencyContactDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return PatientEmergencyContactList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, gurantor info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public PatientGuarantorDTO LoadPatientGurantorInfoByPatientIdForCompleteProfile(int patientId)
        {
            PatientGuarantorDTO objGurantorDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_GURANTOR_INFO_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    objGurantorDTO = DataReaderToDTOConverter.ConvertToPatientGurantorDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return objGurantorDTO;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, allergy info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientAllergyDTO> LoadPatientAllergyInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientAllergyDTO> PatientAllergyList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_ALLERGY_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    PatientAllergyList = DataReaderToDTOConverter.ConvertToPatientAllergyDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return PatientAllergyList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, medical history info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientMedicalHistoryDTO> LoadPatientMedicalHistoryInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientMedicalHistoryDTO> PatientMedicalHistoryList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_MEDICAL_HISTORY_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    PatientMedicalHistoryList = DataReaderToDTOConverter.ConvertToPatientMedicalHistoryDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return PatientMedicalHistoryList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, family history info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientFamilyHistoryDTO> LoadPatientFamilyHistoryInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientFamilyHistoryDTO> PatientFamilyHistoryList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_FAMILY_HISTORY_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    PatientFamilyHistoryList = DataReaderToDTOConverter.ConvertToPatientFamilyHistoryDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return PatientFamilyHistoryList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, family history info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientInsuranceDTO> LoadPatientInsuranceInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientInsuranceDTO> PatientInsuranceList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_INSURANCE_INFO_LOAD_ALL_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.INSURANCE_DB);
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    PatientInsuranceList = DataReaderToDTOConverter.ConvertToPatientInsuranceDTO(objIReader);
                }
            }
            catch 
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return PatientInsuranceList;
        }

        public int ManagePatient(PatientDTO patientDTO, bool IsUpdate)
        {
            int result = 0;
            string sProc = UserConstant.PATIENT_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = UserConstant.PATIENT_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulatePatientDTOParameters(patientDTO, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientIDOUT").ToString(), out result);
                    else
                        result = patientDTO.PatientId;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public PatientBasicInfoDTO LoadPatientBasicInfoByPatientId(int PatientId)
        {
            PatientBasicInfoDTO objPatientDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_BASIC_INFO_LOAD_BY_PATIENT_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    objPatientDTO = DataReaderToDTOConverter.ConvertToPatientBasicInfoDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return objPatientDTO;
        }

        public PatientUserInfo LoadPatientUserInfoByQuivusId(string QuivusId)
        {
            PatientUserInfo objPatientDTO = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_USER_INFO_LOAD_BY_QUVIUS_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@QuivusID", DbType.String, QuivusId);

                using (objIReader = db.ExecuteReader(command))
                {
                    objPatientDTO = DataReaderToDTOConverter.ConvertToPatientUserInfoDTO(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return objPatientDTO;
        }

        public int LoadClinicBranchIdByClinicBranchName(string ClicniBranchName)
        {
            int obj = 0;

            IDataReader objReader = null;
            string sProc = UserConstant.CLINIC_BRANCH_ID_LOAD_BY_NAME_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBrName", DbType.String, ClicniBranchName);

                using (objReader = db.ExecuteReader(command))
                {
                    if (objReader != null)
                    {
                        if (objReader.Read())
                        {
                            if (objReader["ClinicBranchID"] != DBNull.Value && !string.IsNullOrEmpty(objReader["ClinicBranchID"].ToString()))
                            {
                                obj = Int32.Parse(objReader["ClinicBranchID"].ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return obj;
        }

        public List<ClinicRoomDTO> LoadClinicRoomByStationtId(int stationId)
        {
            List<ClinicRoomDTO> clinicRoomList = null;

            IDataReader objIReader = null;
            string sProc = UserConstant.PATIENT_CLINICROOM_LOAD_BY_STATION_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@StationId", DbType.Int32, stationId);

                using (objIReader = db.ExecuteReader(command))
                {
                    clinicRoomList = DataReaderToDTOConverter.ConvertToClinicRoomList(objIReader);
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return clinicRoomList;
        }
    }
}
