using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.TypeDTOs;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Utility.CustomException;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Visit.DataAccess.DB_Util;
using Quivus.Model.QuickInput;
using System.Data.SqlClient;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.User.Patient;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using QuivusUtility.ProjectConstants;
using Quivus.Model.QuickInput.Prescription;

namespace Visit.DataAccess.DAO
{
    public class QuickInputDAO : BaseDAO
    {
        public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, bool IsUpdate)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_MEDICAL_INSERT_SP;
           
            //bool SkipRecord = false;

            try
            {
                //if (IsUpdate && !PateintMedicalHistoryObject.IsDirty)
                //{
                //    // This Record should Not Updated Skip this Recokrd Do Nothing
                //    SkipRecord = true;
                //}

                //if (!SkipRecord)
                //{
                    if (IsUpdate)
                    {
                        sProc = VisitConstant.PATIENT_MEDICAL_UPDATE_SP;
                    }

                    /* Now delete case is also handled in this method
                  * Updated by Ismail on request of Waqas Baber
                  */
                    if (PateintMedicalHistoryObject != null && PateintMedicalHistoryObject.IsDeleted)
                    {
                        // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                        bool status = DeleteMedicalHistory(PateintMedicalHistoryObject);
                        if (status)
                            result = 1;

                        return result;
                    }

                    QuivusLogger.Info("Calling SP:: " + sProc);

                    Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                    DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                    ParameterPopulator.PopulateMedicalHistoryParameters(PateintMedicalHistoryObject, IsUpdate, db, command);

                    if (db.ExecuteNonQuery(command) == 0)
                    {
                      //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                        throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                    ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                    }
                    else
                    {
                        if (!IsUpdate)
                            int.TryParse(db.GetParameterValue(command, "MedicalHistoryIDOUT").ToString(), out result);
                        else
                            result = PateintMedicalHistoryObject.PatientMedicalHistoryID;
                    }
                //}// Skip Record
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, bool IsUpdate, int managedUsedId, int VisitId, DateTime ModificationDate)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_MEDICAL_INSERT_SP;
            try
            {
               
                /* Now delete case is also handled in this method
                * Updated by Ismail on request of Waqas Baber
                */
                if (PateintMedicalHistoryObject != null && PateintMedicalHistoryObject.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                    bool status = DeleteMedicalHistory(PateintMedicalHistoryObject);
                    if (status)
                        result = 1;

                    return result;
                }

                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_MEDICAL_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateMedicalHistoryParameters(PateintMedicalHistoryObject, IsUpdate, managedUsedId, VisitId, ModificationDate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "MedicalHistoryIDOUT").ToString(), out result);
                    else
                        result = PateintMedicalHistoryObject.PatientMedicalHistoryID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, bool IsUpdate, Database db, DbTransaction transaction)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_MEDICAL_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_MEDICAL_UPDATE_SP;
                }

                /* Now delete case is also handled in this method
                 * Updated by Ismail on request of Waqas Baber
                 */
                if (PateintMedicalHistoryObject != null && PateintMedicalHistoryObject.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                    bool status = DeleteMedicalHistory(PateintMedicalHistoryObject);
                    if (status)
                        result = 1;

                    return result;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                //    Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateMedicalHistoryParameters(PateintMedicalHistoryObject, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command, transaction) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "MedicalHistoryIDOUT").ToString(), out result);
                    else
                        result = PateintMedicalHistoryObject.PatientMedicalHistoryID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }


        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, bool IsUpdate, int UserTypeId, int ClinicBranchId)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_FAMILY_INSERT_SP;

           // bool SkipRecord = false;
            try
            {
                //if (IsUpdate && !PateintFamilyHistoryObject.IsDirty)
                //{
                //    // This Record should Not Updated Skip this Recokrd Do Nothing
                //    SkipRecord = true;
                //}

                //if (!SkipRecord)
                //{
                    if (IsUpdate)
                    {
                        sProc = VisitConstant.PATIENT_FAMILY_UPDATE_SP;
                    }

                    if (PateintFamilyHistoryObject != null && PateintFamilyHistoryObject.IsDeleted)
                    {
                        // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                        bool status = DeleteFamilyHistory(PateintFamilyHistoryObject);
                        if (status)
                            result = 1;

                        return result;
                    }

                    QuivusLogger.Info("Calling SP:: " + sProc);

                    Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                    DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                    ParameterPopulator.PopulateFamilyHistoryParameters(PateintFamilyHistoryObject, IsUpdate, UserTypeId, ClinicBranchId, db, command);



                    if (db.ExecuteNonQuery(command) == 0)
                    {
                     //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                        throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                 ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                 ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
                    }
                    else
                    {
                        if (!IsUpdate)
                            int.TryParse(db.GetParameterValue(command, "PatientFamilyHistoryIDOUT").ToString(), out result);
                        else
                            result = PateintFamilyHistoryObject.PatientFamilyHistoryID;
                    }
                //}// SKip Record
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, bool IsUpdate, int managedUsedId, int VisitId, DateTime ModificationDate, int UserTypeId, int ClinicBranchId)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_FAMILY_INSERT_SP;
            
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_FAMILY_UPDATE_SP;
                }
                
                /* Now delete case is also handled in this method
                 * Updated by Ismail on request of Waqas Baber
                 */
                if (PateintFamilyHistoryObject != null && PateintFamilyHistoryObject.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                   bool status= DeleteFamilyHistory(PateintFamilyHistoryObject);
                   if (status)
                       result = 1;

                   return result;
                }


                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateFamilyHistoryParameters(PateintFamilyHistoryObject, IsUpdate, db, command, managedUsedId, VisitId, ModificationDate, UserTypeId, ClinicBranchId);
                
                if (db.ExecuteNonQuery(command) == 0)
                {
                  //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientFamilyHistoryIDOUT").ToString(), out result);
                    else
                        result = PateintFamilyHistoryObject.PatientFamilyHistoryID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }



        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, bool IsUpdate, Database db, DbTransaction transaction)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_FAMILY_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_FAMILY_UPDATE_SP;
                }

                /* Now delete case is also handled in this method
                 * Updated by Ismail on request of Waqas Baber
                 */
                if (PateintFamilyHistoryObject != null && PateintFamilyHistoryObject.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                    bool status = DeleteFamilyHistory(PateintFamilyHistoryObject);
                    if (status)
                        result = 1;

                    return result;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                //Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                //DbConnection connection = db.CreateConnection();
                //connection.Open();
                //IDbTransaction transaction = connection.BeginTransaction();


                db.AddInParameter(command, "@FamilyHistoryTypeID", DbType.Int32, PateintFamilyHistoryObject.FamilyHistoryTypeID);
                db.AddInParameter(command, "@FamilyHistoryNote", DbType.String, PateintFamilyHistoryObject.FamilyHistoryNote);
                db.AddInParameter(command, "@IsPresent", DbType.Boolean, PateintFamilyHistoryObject.IsPresent);
                db.AddInParameter(command, "@ReletionshipTypeID", DbType.Int32, PateintFamilyHistoryObject.ReletionshipTypeID);
       
                db.AddInParameter(command, "IsYesSelected", DbType.Boolean, PateintFamilyHistoryObject.IsYesSelected);
                db.AddInParameter(command, "@VisitID", DbType.Int32, PateintFamilyHistoryObject.VisitID);
                db.AddInParameter(command, "@DictationID", DbType.Guid, PateintFamilyHistoryObject.DictationID);

                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, PateintFamilyHistoryObject.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, PateintFamilyHistoryObject.CreatedBy);
                    db.AddInParameter(command, "@PatientFamilyHistoryIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientFamilyHistoryID", DbType.Int32, PateintFamilyHistoryObject.PatientFamilyHistoryID);
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, PateintFamilyHistoryObject.ModifiedBy);
                    db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, PateintFamilyHistoryObject.ModifiedAt);
                }

                if (db.ExecuteNonQuery(command, transaction) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientFamilyHistoryID").ToString(), out result);
                    else
                        result = PateintFamilyHistoryObject.PatientFamilyHistoryID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 22 March 2011
        /// </summary>
        /// <param name="VitalObject"></param>
        /// <param name="IsUpdate"></param>
        /// <returns></returns>
        public int ManagePatientVitals(VitalDTO VitalObject, bool IsUpdate)
        {
            int result = 0;
            string sProc = VisitConstant.VITAL_INSERT_SP;

            //bool SkipRecord = false;

            //if (IsUpdate && !VitalObject.IsDirty)
            //{
            //    // This Record should Not Updated Skip this Recokrd Do Nothing
            //    SkipRecord = true;
            //}

            try
            {
                //if (!SkipRecord)
                //{
                    if (IsUpdate)
                    {
                        sProc = VisitConstant.VITAL_UPDATE_SP;
                    }

                    QuivusLogger.Info("Calling SP:: " + sProc);

                    Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                    DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                    ParameterPopulator.PopulateVitalParameters(VitalObject, IsUpdate, db, command);

                    if (db.ExecuteNonQuery(command) == 0)
                    {
                       // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());

                        throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                  ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
                    }
                    else
                    {
                        if (!IsUpdate)
                            int.TryParse(db.GetParameterValue(command, "PatientVitalIDOUT").ToString(), out result);
                        else
                            result = VitalObject.PatientVitalID;
                    }
                //}
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 22 March 2011
        /// </summary>
        /// <param name="VitalObject"></param>
        /// <param name="IsUpdate"></param>
        /// <returns></returns>
        public int ManagePatientVitals(VitalDTO VitalObject, bool IsUpdate, int managedUsedId, int VisitId, DateTime ActionDate)
        {
            int result = 0;
            string sProc = VisitConstant.VITAL_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.VITAL_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateVitalParameters(VitalObject, IsUpdate, managedUsedId, VisitId, ActionDate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                   // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientVitalIDOUT").ToString(), out result);
                    else
                        result = VitalObject.PatientVitalID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }


        public int ManagePatientVitals(VitalDTO VitalObject, bool IsUpdate, Database db, DbTransaction transaction)
        {
            int result = 0;
            string sProc = VisitConstant.VITAL_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.VITAL_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                //   Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                // SetValueIfNotNull("@LabTestPriorityTypeID", LabObject.LabTestPriorityTypeID, db, command);

                db.AddInParameter(command, "@VitalNote", DbType.String, VitalObject.VitalNote);
                db.AddInParameter(command, "@VisitID", DbType.Int32, VitalObject.VisitID);
                db.AddInParameter(command, "@VitalDescription", DbType.String, VitalObject.VitalDescription);
                db.AddInParameter(command, "@Height", DbType.String, VitalObject.Height);
                db.AddInParameter(command, "@Weight", DbType.String, VitalObject.Weight);
                db.AddInParameter(command, "@Temperature", DbType.String, VitalObject.Temperature);
                db.AddInParameter(command, "@Pulse", DbType.String, VitalObject.Pulse);
                db.AddInParameter(command, "@Respiration", DbType.String, VitalObject.Respiration);

                db.AddInParameter(command, "@BPSys", DbType.String, VitalObject.BPSys);
                db.AddInParameter(command, "@BPDys", DbType.String, VitalObject.BPDys);
                db.AddInParameter(command, "@BMI", DbType.String, VitalObject.BMI);
                db.AddInParameter(command, "@IsMetricDisplaySelected", DbType.Boolean, VitalObject.IsMetricDisplaySelected);

                db.AddInParameter(command, "@Oxygen", DbType.String, VitalObject.Oxygen);

                //  db.AddInParameter(command, "@DictationID", DbType.Guid, VitalObject.DictationID);
                if (!VitalObject.DictationID.Equals(Guid.Empty))
                    db.AddInParameter(command, "@DictationID", DbType.Guid, VitalObject.DictationID);
                else
                    SetNullValue("@DictationID", db, command, DbType.Guid);

                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, VitalObject.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, VitalObject.CreatedBy);
                    db.AddInParameter(command, "@PatientVitalIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientVitalID", DbType.Int32, VitalObject.PatientVitalID);
                    SetValueIfNotNull("@ModifiedBy", VitalObject.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", VitalObject.ModifiedAt, db, command);

                    // db.AddInParameter(command, "@ModifiedBy", DbType.Int32, VitalObject.ModifiedBy);
                    // db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, VitalObject.ModifiedAt);
                }

                if (db.ExecuteNonQuery(command, transaction) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientVitalIDOUT").ToString(), out result);
                    else
                        result = VitalObject.PatientVitalID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientLabs(LabDTO LabObject, bool IsUpdate)
        {
            QuivusLogger.Info("Calling ManagePatientLabs 3 PARAM :: LabObject " + LabObject);
            int result = 0;
            string sProc = VisitConstant.LABS_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.LABS_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP :: " + sProc + " ManagePatientLabs with 2 arg");

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

              bool SkipRecord=  ParameterPopulator.PopulateLabParameters(LabObject, IsUpdate, db, command);

              if (!SkipRecord)
              {
                  if (db.ExecuteNonQuery(command) == 0)
                  {
                    //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());

                      throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                 ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                 ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
                  }
                  else
                  {
                      if (!IsUpdate)
                          int.TryParse(db.GetParameterValue(command, "PatientLabIDOUT").ToString(), out result);
                      else
                          result = LabObject.PatientLabID;
                  }
              }
              
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientLabs(LabDTO LabObject, bool IsUpdate, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            int result = 0;
            string sProc = VisitConstant.LABS_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.LABS_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP :: " + sProc + " ManagePatientLabs with 5 arg");

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateLabParameters(LabObject, IsUpdate, managedByUserId, VisitId, ActionDate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                  //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX+ ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    if (!IsUpdate)
                    {
                        
                        
                        int.TryParse(db.GetParameterValue(command, "PatientLabIDOUT").ToString(), out result);


                    }
                    else
                        result = LabObject.PatientLabID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }


        public int ManagePatientLabs(LabDTO LabObject, bool IsUpdate, Database db, DbTransaction transaction)
        {
            int result = 0;
            string sProc = VisitConstant.LABS_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.LABS_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP :: " + sProc + " ManagePatientLabs with 4 arg");

                //  Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateLabParameters(LabObject, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command, transaction) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientLabIDOUT").ToString(), out result);
                    else
                        result = LabObject.PatientLabID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>        
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.IMMUNIZATION_MANAGE_SP;
            DataTable dtImmunization = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (immunizationList != null && immunizationList.Count > 0)
                    dtImmunization = DTOToTableConverter.ImmunizationDTOListConverter(immunizationList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientImmunizationTVP", dtImmunization);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.IMMUNIZATION_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                   // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                    ErrorCodes.IMMUNIZATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX +ErrorCodes.LEVEL_ONE_PREFIX));
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
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>        
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.IMMUNIZATION_MANAGE_SP;
            DataTable dtImmunization = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (immunizationList != null && immunizationList.Count > 0)
                    dtImmunization = DTOToTableConverter.ImmunizationDTOListConverter(immunizationList, managedByUserId, VisitId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientImmunizationTVP", dtImmunization);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.IMMUNIZATION_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                   // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.IMMUNIZATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
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
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>        
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, Database DB, DbTransaction transaction)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.IMMUNIZATION_MANAGE_SP;
            DataTable dtImmunization = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {

                QuivusLogger.Info("Calling SP:: " + sProc);

                if (immunizationList != null && immunizationList.Count > 0)
                    dtImmunization = DTOToTableConverter.ImmunizationDTOListConverter(immunizationList);

                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientImmunizationTVP", dtImmunization);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.IMMUNIZATION_TVP;

                connection.Open();
                isConnectionOpen = true;

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
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientROS(List<ROSDTO> ROSList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_ROS_MANAGE_SP;
            DataTable dtROS = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (ROSList != null && ROSList.Count > 0)
                    dtROS = DTOToTableConverter.ROSDTOListConverter(ROSList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientROSTVP", dtROS);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_ROS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                  //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                   ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientROS(List<ROSDTO> ROSList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_ROS_MANAGE_SP;
            DataTable dtROS = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (ROSList != null && ROSList.Count > 0)
                    dtROS = DTOToTableConverter.ROSDTOListConverter(ROSList, managedByUserId, VisitId, PatientId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientROSTVP", dtROS);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_ROS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                   // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientHPI(List<HPI_CCDTO> hpiList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.HPI_MANAGE_SP;
            DataTable dtHPI = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (hpiList != null && hpiList.Count > 0)
                    dtHPI = DTOToTableConverter.HPIDTOListConverter(hpiList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpHPI = sqlCommand.Parameters.AddWithValue("@PatientHPITVP", dtHPI);
                tvpHPI.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpHPI.TypeName = ProjectConstant.PATIENT_HPI_TVP;

                connection.Open();
                isConnectionOpen = true;

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
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientHPI(List<HPI_CCDTO> hpiList, int managedUsedId, int PatientId, int VisitId, int ClinicBranchId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.HPI_MANAGE_SP;
            DataTable dtHPI = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (hpiList != null && hpiList.Count > 0)
                    dtHPI = DTOToTableConverter.HPIDTOListConverter(hpiList, managedUsedId, PatientId, VisitId, ClinicBranchId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpHPI = sqlCommand.Parameters.AddWithValue("@PatientHPITVP", dtHPI);
                tvpHPI.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpHPI.TypeName = ProjectConstant.PATIENT_HPI_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                  //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.HPI_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX+ ErrorCodes.LEVEL_TWO_PREFIX));
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
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }


        public int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, bool IsUpdate)
        {
            int result = 0;
            string sProc = VisitConstant.SOCIAL_ADICTION_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.SOCIAL_ADICTION_UPDATE_SP;
                }

                if (SocialHistoryAddiction != null && SocialHistoryAddiction.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                    bool status = DeleteSocialHistoryAddiction(SocialHistoryAddiction);
                    if (status)
                        result = 1;

                    return result;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateSocialAddictionParameters(SocialHistoryAddiction, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                   // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                   ErrorCodes.SOCIAL_HISTORY_ADDICTION_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "SocialHistoryAddictionIDOUT").ToString(), out result);
                    else
                        result = SocialHistoryAddiction.SocialHistoryAddictionID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, bool IsUpdate, int managedUsedId, int VisitId, DateTime ActionDate)
        {
            int result = 0;
            string sProc = VisitConstant.SOCIAL_ADICTION_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.SOCIAL_ADICTION_UPDATE_SP;
                }

                if (SocialHistoryAddiction != null && SocialHistoryAddiction.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                    bool status = DeleteSocialHistoryAddiction(SocialHistoryAddiction);
                    if (status)
                        result = 1;

                    return result;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateSocialAddictionParameters(SocialHistoryAddiction, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());

                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.SOCIAL_HISTORY_ADDICTION_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));

                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "SocialHistoryAddictionIDOUT").ToString(), out result);
                    else
                        result = SocialHistoryAddiction.SocialHistoryAddictionID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject)
        {
            bool result = false;
            string sProc = VisitConstant.SOCIAL_ADICTION_DELETED_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@SocialHistoryAddictionID", DbType.Int32, DeletedObject.SocialHistoryAddictionID);
                SetValueIfNotNull("@ModifiedBy", DeletedObject.ModifiedBy, db, command);
                SetDateIfNotNull("@ModifiedAt", DeletedObject.ModifiedAt, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManageSocialHistory(SocialHistoryDTO SocialHistoryObject, bool IsUpdate)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_SOCIAL_INSERT_SP;

          //  bool SkipRecord = false;

            //if (IsUpdate && !SocialHistoryObject.IsDirty)
            //{
            //    // This Record should Not Updated Skip this Recokrd Do Nothing
            //    SkipRecord = true;
            //}

            try
            {
                //if (!SkipRecord)
                //{
                    if (IsUpdate)
                    {
                        sProc = VisitConstant.PATIENT_SOCIAL_UPDATE_SP;
                    }

                    if (SocialHistoryObject != null && SocialHistoryObject.IsDeleted)
                    {
                        // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                        bool status = DeleteSocialHistory(SocialHistoryObject);
                        if (status)
                            result = 1;

                        return result;
                    }
                    QuivusLogger.Info("Calling SP:: " + sProc);

                    Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                    DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                    ParameterPopulator.PopulatePatientSocialParameters(SocialHistoryObject, IsUpdate, db, command);

                    if (db.ExecuteNonQuery(command) == 0)
                    {
                        throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    }
                    else
                    {
                        if (!IsUpdate)
                            int.TryParse(db.GetParameterValue(command, "PatientSocialHistoryIDOUT").ToString(), out result);
                        else
                            result = SocialHistoryObject.PatientSocialHistoryID;
                    }
              //  }//skip record
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManageSocialHistory(SocialHistoryDTO SocialHistoryObject, bool IsUpdate, int managedUsedId, int PatientId, int VisitId, DateTime ActionDate)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_SOCIAL_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_SOCIAL_UPDATE_SP;
                }

                if (SocialHistoryObject != null && SocialHistoryObject.IsDeleted)
                {
                    // This is the special case to be accomodate in our current manage call as current call only handle Insert/Update Before :)
                    bool status = DeleteSocialHistory(SocialHistoryObject);
                    if (status)
                        result = 1;

                    return result;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulatePatientSocialParameters(SocialHistoryObject, IsUpdate, managedUsedId, PatientId, VisitId, ActionDate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.SOCIAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientSocialHistoryIDOUT").ToString(), out result);
                    else
                        result = SocialHistoryObject.PatientSocialHistoryID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }


        public bool DeleteSocialHistory(SocialHistoryDTO DeletedObject)
        {
            bool result = false;
            string sProc = VisitConstant.PATIENT_SOCIAL_DELETED_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientSocialHistoryID", DbType.Int32, DeletedObject.PatientSocialHistoryID);
                SetValueIfNotNull("@ModifiedBy", DeletedObject.ModifiedBy, db, command);
                SetDateIfNotNull("@ModifiedAt", DeletedObject.ModifiedAt, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_PHYSICAL_MANAGE_SP;
            DataTable dtPhysical = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (PhysicalList != null && PhysicalList.Count > 0)
                    dtPhysical = DTOToTableConverter.PhysicalDTOListConverter(PhysicalList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientPhysicalTVP", dtPhysical);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_PHYSICAL_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }


        public bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_PHYSICAL_MANAGE_SP;
            DataTable dtPhysical = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (PhysicalList != null && PhysicalList.Count > 0)
                    dtPhysical = DTOToTableConverter.PhysicalDTOListConverter(PhysicalList, managedByUserId, VisitId, PatientId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientPhysicalTVP", dtPhysical);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_PHYSICAL_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.PHYSICALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_MANAGE_SP;
            DataTable dtDiagnosis = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (diagnosisList != null && diagnosisList.Count > 0)
                    dtDiagnosis = DTOToTableConverter.DiagnosisDTOListConverter(diagnosisList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientDiagnosisTVP", dtDiagnosis);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_DIAGNOSIS_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_MANAGE_SP;
            DataTable dtDiagnosis = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (diagnosisList != null && diagnosisList.Count > 0)
                {
                    QuivusLogger.Info("Before converting:: " + sProc);
                    dtDiagnosis = DTOToTableConverter.DiagnosisDTOListConverter(diagnosisList, managedByUserId, VisitId, PatientId, ActionDate);
                    QuivusLogger.Info("after converting:: " + sProc);
                }
                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientDiagnosisTVP", dtDiagnosis);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_DIAGNOSIS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.DIAGNOSIS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }


        public bool ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_RECOMMENDATION_MANAGE_SP;
            DataTable dtRecommendation = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (RecommendationList != null && RecommendationList.Count > 0)
                    dtRecommendation = DTOToTableConverter.RecommendationDTOListConverter(RecommendationList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpRecommendation = sqlCommand.Parameters.AddWithValue("@PatientRecommendationTVP", dtRecommendation);
                tvpRecommendation.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpRecommendation.TypeName = ProjectConstant.PATIENT_RECOMMENDATION_TVP;

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
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_RECOMMENDATION_MANAGE_SP;
            DataTable dtRecommendation = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (RecommendationList != null && RecommendationList.Count > 0)
                {
                    QuivusLogger.Info("Before converting :: " + RecommendationList);
                    dtRecommendation = DTOToTableConverter.RecommendationDTOListConverter(RecommendationList, managedByUserId, VisitId, PatientId, ClinicBranchId, ActionDate);
                    QuivusLogger.Info("After converting :: " + RecommendationList);
                }
                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientRecommendationTVP", dtRecommendation);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_RECOMMENDATION_TVP;
                QuivusLogger.Info("opening connection :: " + sProc);
                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                  //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());

                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                                             ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                                        ErrorCodes.RECOMMENDATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                                        ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    UpdateResult = true;
                }

                QuivusLogger.Info("closing connection :: " + sProc);
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 25 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient FollowUp data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp, bool isUpdate)
        {
            bool result = true;

            string sProc = VisitConstant.PATIENT_FOLLOWUP_INSERT_SP;
            //bool SkipRecord = false;

            //if (isUpdate && !patientFollowUp.IsDirty)
            //{
            //    // This Record should Not Updated Skip this Recokrd Do Nothing
            //    SkipRecord = true;
            //}
            try
            {

                //if (!SkipRecord)
                //{
                    if (isUpdate)
                    {
                        sProc = VisitConstant.PATIENT_FOLLOWUP_UPDATE_SP;
                    }

                    QuivusLogger.Info("Calling SP:: " + sProc);

                    Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                    DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                    ParameterPopulator.PopulatePatientFollowUpParameters(patientFollowUp, isUpdate, db, command);

                    if (db.ExecuteNonQuery(command) == 0)
                    {
                        throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    }
                    else
                    {
                        result = true;

                        //if (!isUpdate)
                        //    int.TryParse(db.GetParameterValue(command, "PatientSocialHistoryIDOUT").ToString(), out result);
                        //else
                        //    result = SocialHistoryObject.PatientSocialHistoryID;
                    }
                //}
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 25 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient FollowUp data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp, bool isUpdate, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool result = true;

            string sProc = VisitConstant.PATIENT_FOLLOWUP_INSERT_SP;
            try
            {
                if (isUpdate)
                {
                    sProc = VisitConstant.PATIENT_FOLLOWUP_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulatePatientFollowUpParameters(patientFollowUp, isUpdate, managedByUserId, VisitId, PatientId, ActionDate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.FOLLOW_UP_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX+ ErrorCodes.LEVEL_TWO_PREFIX));
                }
                else
                {
                    result = true;

                    //if (!isUpdate)
                    //    int.TryParse(db.GetParameterValue(command, "PatientSocialHistoryIDOUT").ToString(), out result);
                    //else
                    //    result = SocialHistoryObject.PatientSocialHistoryID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }



        public int ManagePatientAllergy(AllergyDTO AllergyObj, bool IsUpdate)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_ALLERGY_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_ALLERGY_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulatePatientAllergyParameters(AllergyObj, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientAllergyOUT").ToString(), out result);
                    else
                        result = AllergyObj.PatientAllergyId;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool ManagePatientAllergies(List<AllergyDTO> Allergylist)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_ALLERGIES_MANAGE_SP;
            DataTable dtAllergies = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (Allergylist != null && Allergylist.Count > 0)
                {
                    dtAllergies = DTOToTableConverter.AllergiesDTOListConverter(Allergylist);

                    Database DB = DatabaseFactory.CreateDatabase();
                    connection = new SqlConnection(DB.ConnectionString);

                    SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    //use SqlParameter to be able to define the TypeName of the parameter
                    SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientAllergyTVP", dtAllergies);
                    tvpImmunization.SqlDbType = SqlDbType.Structured;
                    //should be the name of your TVP
                    tvpImmunization.TypeName = ProjectConstant.PATIENT_ALLERGIES_TVP;

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

        public int ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj, bool IsUpdate)
        {
            int result = 0;
            string sProc = VisitConstant.PATIENT_CHRONIC_MEDICATION_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.PATIENT_CHRONIC_MEDICATION_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulatePatientChronicMedicationParameters(ChronicMedicationObj, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientMedicationIDOUT").ToString(), out result);
                    else
                        result = ChronicMedicationObj.PatientMedicationPrescribedID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool ManagePatientProcedure(List<ProcedureDTO> InputList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_PROCEDURE_MANAGE_SP;
            DataTable dtprocedure = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (InputList != null && InputList.Count > 0)
                    dtprocedure = DTOToTableConverter.ProcedureDTOListConverter(InputList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientProcedureTVP", dtprocedure);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_PROCEDURE_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientProcedure(List<ProcedureDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_PROCEDURE_MANAGE_SP;
            DataTable dtprocedure = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                QuivusLogger.Info("ManagePatientProcedure with VisitID: " + VisitId.ToString() + " and PatientID: " + PatientId.ToString());
                if (InputList != null && InputList.Count > 0)
                    dtprocedure = DTOToTableConverter.ProcedureDTOListConverter(InputList, managedByUserId, VisitId, PatientId, ClinicBranchId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter

                foreach (DataRow dr in dtprocedure.Rows)
                {
                    QuivusLogger.Info("Final Calling DB with VisitID: " + dr["VisitID"].ToString() + " and PatientID: " + dr["PatientID"].ToString());
                }

                SqlParameter tvpPatientProcedure = sqlCommand.Parameters.AddWithValue("@PatientProcedureTVP", dtprocedure);
                tvpPatientProcedure.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpPatientProcedure.TypeName = ProjectConstant.PATIENT_PROCEDURE_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.PROCEDURE_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }


        public bool ManagePatientInHouseLabs(List<InHouseLabDTO> InputList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_IN_HOUSE_LABS_MANAGE_SP;
            DataTable dtInHouseLabs = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (InputList != null && InputList.Count > 0)
                    dtInHouseLabs = DTOToTableConverter.InHouseLabsDTOListConverter(InputList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientInHouseLabTestTVP", dtInHouseLabs);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_INHOUSE_LABS_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientInHouseLabs(List<InHouseLabDTO> InputList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_IN_HOUSE_LABS_MANAGE_SP;
            DataTable dtInHouseLabs = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (InputList != null && InputList.Count > 0)
                    dtInHouseLabs = DTOToTableConverter.InHouseLabsDTOListConverter(InputList, managedByUserId, VisitId, PatientId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientInHouseLabTestTVP", dtInHouseLabs);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_INHOUSE_LABS_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
               //     throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.IN_HOUSE_LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX+ ErrorCodes.LEVEL_TWO_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientInHouseLabResults(int PatientInHouseLabTestID, string TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt)
        {
            bool result = false;
            string sProc = VisitConstant.PATIENT_INHOUSELAB_RESULT_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientInHouseLabTestID", DbType.Int32, PatientInHouseLabTestID);

                if (!string.IsNullOrEmpty(TestResult))
                    db.AddInParameter(command, "@TestResult", DbType.String, TestResult);

                else
                    db.AddInParameter(command, "@TestResult", DbType.String, DBNull.Value);

                if (ResultAttachment != null)
                    db.AddInParameter(command, "@ResultAttachment", DbType.Binary, ResultAttachment);

                else
                    db.AddInParameter(command, "@ResultAttachment", DbType.String, DBNull.Value);

                SetValueIfNotNull("@ModifiedBy", ModifiedBy, db, command);
                SetDateIfNotNull("@ModifiedAt", ModifiedAt, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientMedication(MedicationDTO InputObj, bool IsUpdate)
        {
            int result = 0;

            string sProc = VisitConstant.MEDICATION_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.MEDICATION_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateMedicationParameters(InputObj, IsUpdate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientMedicationIDOUT").ToString(), out result);
                    else
                        result = InputObj.PatientMedicationID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public int ManagePatientMedication(MedicationDTO InputObj, bool IsUpdate, int managedByUserId, int PatientId, DateTime ActionDate)
        {
            int result = 0;

            string sProc = VisitConstant.MEDICATION_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = VisitConstant.MEDICATION_UPDATE_SP;
                }

                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                ParameterPopulator.PopulateMedicationParameters(InputObj, IsUpdate, managedByUserId, PatientId, ActionDate, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    if (!IsUpdate)
                        int.TryParse(db.GetParameterValue(command, "PatientMedicationIDOUT").ToString(), out result);
                    else
                        result = InputObj.PatientMedicationID;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }



        public bool DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt)
        {
            bool result = false;
            string sProc = VisitConstant.DELETE_PATIENT_MEDICTION_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientMedicationID", DbType.Int32, PatientMedicationID);
                SetDateIfNotNull("@ModifiedAt", ModifiedAt, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }
        // /////////////////////


        public bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_FOLLOWUP_INSTRUCTION_MANAGE_SP;
            DataTable dtPatientFollowupInstruction = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (InputList != null && InputList.Count > 0)
                    dtPatientFollowupInstruction = DTOToTableConverter.PatientFollowupInstructionDTOListConverter(InputList, managedByUserId, VisitId, PatientId, ClinicBranchId, ActionDate);
                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpPatientFollowupInstructionTVP = sqlCommand.Parameters.AddWithValue("@PatientFollowupInstructionTVP", dtPatientFollowupInstruction);
                tvpPatientFollowupInstructionTVP.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpPatientFollowupInstructionTVP.TypeName = ProjectConstant.PATIENT_FOLLOWUP_INSTRUCTION_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.FOLLOW_UP_INSTRUCTION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientFollowupInstruction(List<Quivus.Model.QuickInput.FollowUp.PatientFollowupInstructionDTO> InputList)
        {
          
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_FOLLOWUP_INSTRUCTION_MANAGE_SP;
            DataTable dtPatientFollowupInstruction = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (InputList != null && InputList.Count > 0)
                    dtPatientFollowupInstruction = DTOToTableConverter.PatientFollowupInstructionDTOListConverter(InputList);
                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpPatientFollowupInstructionTVP = sqlCommand.Parameters.AddWithValue("@PatientFollowupInstructionTVP", dtPatientFollowupInstruction);
                tvpPatientFollowupInstructionTVP.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpPatientFollowupInstructionTVP.TypeName = ProjectConstant.PATIENT_FOLLOWUP_INSTRUCTION_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }





        public bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_PRESCRIPTION_MEDICATION_MANAGE_SP;
            DataTable dtPatientPrescriptionMedication = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (PrescriptionMedicationlist != null && PrescriptionMedicationlist.Count > 0)
                    dtPatientPrescriptionMedication = DTOToTableConverter.PatientPrescriptionMedicationDTOListConverter(PrescriptionMedicationlist);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpPatientFollowupInstructionTVP = sqlCommand.Parameters.AddWithValue("@PrescriptionMedicationTVP", dtPatientPrescriptionMedication);
                tvpPatientFollowupInstructionTVP.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpPatientFollowupInstructionTVP.TypeName = ProjectConstant.PATIENT_PRESCRIPTION_MEDICATION_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_BY_RCOPIA_MANAGE_SP;
            DataTable dtDiagnosis = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (diagnosisList != null && diagnosisList.Count > 0)
                    dtDiagnosis = DTOToTableConverter.DiagnosisDTOListConverter(diagnosisList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientDiagnosisTVP", dtDiagnosis);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_DIAGNOSIS_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }

        public bool ManagePatientMedication(List<MedicationDTO> MedicationList)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.PATIENT_MEDICATION_MANAGE_SP;
            DataTable dtMedication = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (MedicationList != null && MedicationList.Count > 0)
                    dtMedication = DTOToTableConverter.MedicationDTOListConverter(MedicationList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@PatientMedicationTVP", dtMedication);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ProjectConstant.PATIENT_MEDICATION_TVP;

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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return UpdateResult;
        }



        public bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject)
        {
            bool result = false;
            string sProc = VisitConstant.PATIENT_FAMILY_DELETED_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientFamilyHistoryID", DbType.Int32, DeletedObject.PatientFamilyHistoryID);
                SetValueIfNotNull("@ModifiedBy", DeletedObject.ModifiedBy, db, command);
                SetDateIfNotNull("@ModifiedAt", DeletedObject.ModifiedAt, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject)
        {
            bool result = false;
            string sProc = VisitConstant.PATIENT_MEDICAL_DELETED_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientMedicalHistoryID", DbType.Int32, DeletedObject.PatientMedicalHistoryID);
                SetValueIfNotNull("@ModifiedBy", DeletedObject.ModifiedBy, db, command);
                SetDateIfNotNull("@ModifiedAt", DeletedObject.ModifiedAt, db, command);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Baber waqas at 1/24/2012
        /// </summary>
        /// <param name="icdCodeDTO"></param>
        /// <param name="managedByUserIDForVisit"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        public bool ManageCustomICDCode(ICDCodeDTO icdCodeDTO, int managedByUserIDForVisit, int clinicBranchID)
        {
            bool updateResult = false;
            string sProc = VisitConstant.ICD_CODE_MANAGE_CUSTOM;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

               
               DataTable icDataTable = DTOToTableConverter.ICDCustomCodeDTOConverter(icdCodeDTO, managedByUserIDForVisit, clinicBranchID);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpICDCustom = sqlCommand.Parameters.AddWithValue("@ICDCustomCodeTVP", icDataTable);
                tvpICDCustom.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpICDCustom.TypeName = ProjectConstant.ICDCustomCodeTVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    updateResult = true;
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

                //if (isConnectionOpen)
                //    connection.Close();
            }
            return updateResult;  

        }
    }
}