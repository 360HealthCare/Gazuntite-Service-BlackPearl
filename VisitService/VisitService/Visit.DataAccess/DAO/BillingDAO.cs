using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Billing.Visit;
using System.Data;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Visit.DataAccess.DB_Util;
using System.Data.SqlClient;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using QuivusModel.Billing.PendingBills;
using QuivusModel.TypeDTOs.BillingTypeDTOs;
using Quivus.Model.QuickInput.Diagnosis;

namespace Visit.DataAccess.DAO
{
    /// <summary>
    /// Auhtor: Shahid Munir
    /// Date:   01/27/2012
    /// </summary>
    public class BillingDAO : BaseDAO
    {
        #region E/M calculation for Super bill

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get History Examination and Decision Level counts
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public SuperBillHEDLevelDTO LoadVisitHEDLevelCounts(int visitId)
        {
            SuperBillHEDLevelDTO objHEDLevelCounts = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.VISIT_HISTORY_EXAMINATION_DECISION_LEVEL_COUNTS_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    objHEDLevelCounts = ConvertVisitHEDData(objHEDLevelCounts, objIReader);
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return objHEDLevelCounts;
        }
       
        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get count of patient HPI against visit for Super Bill E/M calculation
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int CountHPIforVisit(int visitId)
        {
            int hpiCount = 0;
            string sProc = BillingConstant.VISIT_COUNT_HPI;
            IDataReader objIReader = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                QuivusLogger.Info("CountHPIforVisit with visitId: " + visitId);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@VisitID", DbType.String, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        hpiCount = int.Parse(objIReader["NoOfHPI"].ToString());
                    }
                }
            }
            catch
            {
                throw;
            }
            return hpiCount;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get count of choronic diagnosis against visit for Super Bill E/M calculation
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public int CountVisitChronicDiagnosis(int visitId)
        {
            int diagCount = 0;
            string sProc = BillingConstant.VISIT_COUNT_CHRONIC_DIAGNOSIS;
            IDataReader objIReader = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                QuivusLogger.Info("CountVisitChronicDiagnosis with visitId: " + visitId);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@VisitID", DbType.String, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        diagCount = int.Parse(objIReader["NoOfChronicDiagnosis"].ToString());
                    }
                }
            }
            catch
            {
                throw;
            }
            return diagCount;
        }
        

        #endregion

        /// <summary>
        /// /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to manage diagnosis updated created from Super Bill 
        /// </summary>
        /// <param name="diagnosisList"></param>
        /// <returns></returns>
        public bool ManageVisitDiagnosis(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool UpdateResult = false;
            string sProc = BillingConstant.VISIT_DIAGNOSIS_MANAGE_SP;
            DataTable dtDiagnosis = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (diagnosisList != null && diagnosisList.Count > 0)
                {
                    QuivusLogger.Info("Before converting:: " + sProc);
                    dtDiagnosis = DTOToTableConverter.DiagnosisDTOListConverter(diagnosisList, diagnosisList[0].ModifiedBy, diagnosisList[0].VisitID, diagnosisList[0].PatientID, DateTime.Now);
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

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 12/27/2011  
        /// to get all place of service types
        /// </summary>
        /// <returns></returns>
        public List<PlaceOfServiceTypeDTO> LoadPlaceOfServiceTypeByName(string typrName)
        {
            List<PlaceOfServiceTypeDTO> List = null;
            IDataReader objIReader = null;
            string sProc = "";
            if (typrName.Equals("All"))
            {
                sProc = BillingConstant.PLACE_OF_SERVICE_TYPE_LOAD_ALL_SP;
            }
            else
            {
                sProc = BillingConstant.PLACE_OF_SERVICE_TYPE_LOAD_BY_NAME_SP;
            }
            

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc + " PARAM:: typrName " + typrName);

                List = new List<PlaceOfServiceTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                if (typrName.Equals("All") == false)
                {
                    db.AddInParameter(command, "@TypeName", DbType.String, typrName);
                }

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(List, objIReader, typeof(PlaceOfServiceTypeDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return List;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage procedure modifiers from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        public bool ManagePatientProcedureModifier(List<PatientProcedureModifierDTO> InputList)
        {
            bool UpdateResult = false;
            string sProc = BillingConstant.PATIENT_PROCEDURE_MODIFIER_MANAGE_SP;
            DataTable dtprocedureModifier = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                QuivusLogger.Info("ManagePatientProcedure with InputList: " + InputList);
                if (InputList != null && InputList.Count > 0)
                    dtprocedureModifier = QBDTOToTableConverter.ProcedureModofierDTOListConverter(InputList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpPatientProcedureModifier = sqlCommand.Parameters.AddWithValue("@PatientProcedureModifierTVP", dtprocedureModifier);
                tvpPatientProcedureModifier.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpPatientProcedureModifier.TypeName = BillingConstant.PATIENT_PROCEDURE_MODIFIER_TVP;

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

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get procedure modifiers for Super Bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="IsTemplateVisit"></param>
        /// <returns></returns>
        public List<PatientProcedureModifierDTO> LoadProcedureModifierDTOListByPatientProcedureId(int patientProcedureId)
        {
            List<PatientProcedureModifierDTO> modifierDTOList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.PROCEDURE_MODIFIER_LOAD_BY_PATIENT_PROCEDURE_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                modifierDTOList = new List<PatientProcedureModifierDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientProcedureId", DbType.Int32, patientProcedureId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        modifierDTOList = QBDataReaderToDTOConverter.ConvertModifierDTO(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return modifierDTOList;
        }

        /// <summary>
        /// Auhtor: Shahid Munir
        /// Date:   01/27/2012
        /// to get Procedure modifier types
        /// </summary>
        /// <param name="ModifierCode"></param>
        /// <returns></returns>
        public List<ProcedureModifierTypeDTO> LoadProcedureModifierTypes(String ModifierCode)
        {
            List<ProcedureModifierTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.PROCEDURE_MODIFIER_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ProcedureModifierTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name


                db.AddInParameter(command, "@ModifierCode", DbType.String, ModifierCode);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ProcedureModifierTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to manage visit procedures from Super Bill
        /// </summary>
        /// <param name="InputList"></param>        
        /// <returns></returns>
        public bool ManageVisitProcedure(List<QBProcedureDTO> InputList)
        {
            bool UpdateResult = false;
            string sProc = BillingConstant.VISIT_PROCEDURE_MANAGE_SP;
            DataTable dtprocedure = null;
            SqlConnection connection = null;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                QuivusLogger.Info("ManagePatientProcedure with InputList: " + InputList);
                if (InputList != null && InputList.Count > 0)
                    dtprocedure = QBDTOToTableConverter.ProcedureDTOListConverter(InputList);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter

                foreach (DataRow dr in dtprocedure.Rows)
                {
                    QuivusLogger.Info("Final Calling DB with VisitID: " + dr["VisitID"].ToString() + " and PatientID: " + dr["PatientID"].ToString());
                }

                SqlParameter tvpPatientProcedure = sqlCommand.Parameters.AddWithValue("@VisitProcedureTVP", dtprocedure);
                tvpPatientProcedure.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpPatientProcedure.TypeName = BillingConstant.VISIT_PROCEDURE_TVP;

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

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get visit Data for Billing service 
        /// </summary>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        public QBVisitDTO LoadVisitDataByVisitId(int visitId)
        {
            QBVisitDTO VisitObj = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.VISIT_DATA_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    VisitObj = ConvertVisitData(VisitObj, objIReader, false);
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitObj;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date: 01/27/2012
        /// to get visit procedures for Patient Bill
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="IsTemplateVisit"></param>
        /// <returns></returns>
        public List<QBProcedureDTO> LoadProcedureDTOListByVisitId(int VisitId)
        {
            List<QBProcedureDTO> ProcedureDTOList = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.PATIENT_PROCEDURE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ProcedureDTOList = new List<QBProcedureDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        ProcedureDTOList = QBDataReaderToDTOConverter.ConvertProcedureDTOByVisitId(objIReader, false);
                    }
                }
               
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ProcedureDTOList;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark Diagnosis as created from super bill
        /// </summary>d
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientDaignosisCreatedFromBilling(List<int> PatientDiagnosisProcedureIds, int UpdatedByUserID)
        {
            bool result = false;
            string sProc = BillingConstant.PATIENT_DIAGNOSIS_MARK_CREATED_FROM_BILLING_SP;

            DataTable VisitIdTable = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                VisitIdTable = QBDTOToTableConverter.GeneralIdListConverter(PatientDiagnosisProcedureIds);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpVisitIds = sqlCommand.Parameters.AddWithValue("@GeneralIdsTVP", VisitIdTable);
                DB.AddInParameter(sqlCommand, "@UpdatedBy", DbType.Int32, UpdatedByUserID);                

                tvpVisitIds.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpVisitIds.TypeName = BillingConstant.GENERAL_ID_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
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
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark Diagnosis or procedures as include in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientDaignosisProceduresMarkAsIncludeInBilling(List<int> PatientDiagnosisProcedureIds, int UpdatedByUserID, bool IsIncluded, string CodeType)
        {
            bool result = false;
            string sProc = BillingConstant.PATIENT_DIAGNOSIS_PROCEDURE_MARK_INCLUDE_IN_BILLING_SP;

            DataTable VisitIdTable = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                VisitIdTable = QBDTOToTableConverter.GeneralIdListConverter(PatientDiagnosisProcedureIds);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpVisitIds = sqlCommand.Parameters.AddWithValue("@GeneralIdsTVP", VisitIdTable);
                DB.AddInParameter(sqlCommand, "@UpdatedBy", DbType.Int32, UpdatedByUserID);
                DB.AddInParameter(sqlCommand, "@IsIncluded", DbType.Int32, IsIncluded);
                DB.AddInParameter(sqlCommand, "@Type", DbType.String, CodeType);

                tvpVisitIds.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpVisitIds.TypeName = BillingConstant.GENERAL_ID_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
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
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to load all visit that for which claim not created for billing
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public List<PendingBillsDTO> LoadAllPendingBillsByProviderId(int providerId, int clinincBranchId)
        {
            List<PendingBillsDTO> list = null;
            IDataReader objIReader = null;
            string sProc = BillingConstant.VISIT_LOAD_ALL_SIGNED_PENDING_BILLS_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PendingBillsDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@ProviderId", DbType.Int32, providerId);
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinincBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(PendingBillsDTO));
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark visit that its claim created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        public bool VisitClaimCreated(int VisitId, int ClaimCreatedByUserID, DateTime OperationDate, int encounterTypeId, int placeOfServiceTypeId)
        {
            bool result = false;
            string sProc = BillingConstant.VISIT_MARK_CLAIM_CREATED_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
               
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                //db.AddInParameter(command, "@ClaimCreatedByUserID", DbType.Int32, ClaimCreatedByUserID);
                //db.AddInParameter(command, "@ClaimCreatedByUserTypeId", DbType.Int32, ClaimCreatedByUserTypeId);
                db.AddInParameter(command, "@ClaimCreatedDate", DbType.DateTime, OperationDate);
                db.AddInParameter(command, "@UpdatedBy", DbType.Int32, ClaimCreatedByUserID);
                db.AddInParameter(command, "@EncounterTypeID", DbType.Int32, encounterTypeId);
                db.AddInParameter(command, "@POSTypeID", DbType.Int32, placeOfServiceTypeId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
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
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark visit that its claim created for billing
        /// </summary>
        /// <param name="VisitId"></param>
        /// <param name="ClaimCreatedByUserID"></param>
        /// <param name="OperationDate"></param>
        /// <returns></returns>
        public bool VisitClaimListCreated(List<int> VisitIds, int ClaimCreatedByUserID, DateTime OperationDate)
        {
            bool result = false;
            string sProc = BillingConstant.VISIT_MARK_LIST_CLAIM_CREATED_SP;

            DataTable VisitIdTable = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                VisitIdTable = QBDTOToTableConverter.GeneralIdListConverter(VisitIds);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpVisitIds = sqlCommand.Parameters.AddWithValue("@GeneralIdsTVP", VisitIdTable);
                DB.AddInParameter(sqlCommand, "@ClaimCreatedDate", DbType.DateTime, OperationDate);
                DB.AddInParameter(sqlCommand, "@UpdatedBy", DbType.Int32, ClaimCreatedByUserID);

                tvpVisitIds.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpVisitIds.TypeName = BillingConstant.GENERAL_ID_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
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
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return result;
        }

        /*
        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/05/2012
        /// to mark procedures as included in billing
        /// </summary>
        /// <param name="PatientProcedureIds"></param>
        /// <param name="UpdatedByUserID"></param>        
        /// <returns></returns>
        public bool PatientProceduresMarkIncludedInBilling(List<int> PatientProcedureIds, int UpdatedByUserID, bool IsIncluded)
        {
            bool result = false;
            string sProc = BillingConstant.PATIENT_PROCEDURE_MARK_INCLUDED_IN_BILLING_SP;

            DataTable VisitIdTable = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                VisitIdTable = QBDTOToTableConverter.GeneralIdListConverter(PatientProcedureIds);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpVisitIds = sqlCommand.Parameters.AddWithValue("@GeneralIdsTVP", VisitIdTable);
                DB.AddInParameter(sqlCommand, "@UpdatedBy", DbType.Int32, UpdatedByUserID);
                DB.AddInParameter(sqlCommand, "@IsIncluded", DbType.Int32, IsIncluded);

                tvpVisitIds.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpVisitIds.TypeName = BillingConstant.GENERAL_ID_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
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
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return result;
        }*/

        private static SuperBillHEDLevelDTO ConvertVisitHEDData(SuperBillHEDLevelDTO VisitHEDLObj, IDataReader objIReader)
        {
            if (objIReader != null)
            {
                if (objIReader.Read())
                {
                    VisitHEDLObj = new SuperBillHEDLevelDTO();

                    if (objIReader["VisitID"] != DBNull.Value)
                    {
                        VisitHEDLObj.VisitID = int.Parse(objIReader["VisitID"].ToString());
                    }

                    if (objIReader["HPI"] != DBNull.Value)
                    {
                        VisitHEDLObj.VisitID = int.Parse(objIReader["HPI"].ToString());
                    }

                    if (objIReader["ROS"] != DBNull.Value)
                    {
                        VisitHEDLObj.VisitID = int.Parse(objIReader["ROS"].ToString());
                    }

                    if (objIReader["MedicalHistory"] != DBNull.Value)
                    {
                        VisitHEDLObj.VisitID = int.Parse(objIReader["MedicalHistory"].ToString());
                    }

                    if (objIReader["FamilyHistory"] != DBNull.Value)
                    {
                        VisitHEDLObj.VisitID = int.Parse(objIReader["FamilyHistory"].ToString());
                    }

                    if (objIReader["SocialHistory"] != DBNull.Value)
                    {
                        VisitHEDLObj.VisitID = int.Parse(objIReader["SocialHistory"].ToString());
                    }

                    if (objIReader["PhysicalTypes"] != DBNull.Value)
                    {
                        VisitHEDLObj.PhysicalTypes = int.Parse(objIReader["PhysicalTypes"].ToString());
                    }

                    if (objIReader["IsNewEncounter"] != DBNull.Value)
                    {
                        VisitHEDLObj.IsNewEncounter = bool.Parse(objIReader["IsNewEncounter"].ToString());
                    }
                    
                }
            }
            return VisitHEDLObj;
        }

        private static QBVisitDTO ConvertVisitData(QBVisitDTO VisitObj, IDataReader objIReader, bool IsTemplateVisit)
        {
            if (objIReader != null)
            {
                if (objIReader.Read())
                {
                    VisitObj = new QBVisitDTO();

                    VisitObj.VisitId = GetKeyAsNullIfTemplate(objIReader, "VisitID", VisitObj.VisitId, IsTemplateVisit);

                    
                    if (objIReader["PatientID"] != DBNull.Value)
                    {
                        VisitObj.PatientId = int.Parse(objIReader["PatientID"].ToString());
                    }
                    if (objIReader["ClinicBranchID"] != DBNull.Value)
                    {
                        if (VisitObj.ClinicBranch == null)
                            VisitObj.ClinicBranch = new Quivus.Model.Clinic.ClinicBranchDTO();

                        VisitObj.ClinicBranch.ClinicBranchIdSeq = int.Parse(objIReader["ClinicBranchID"].ToString());
                    }
                    if (objIReader["VisitDate"] != DBNull.Value)
                    {
                        VisitObj.VisitDate = DateTime.Parse(objIReader["VisitDate"].ToString());
                    }

                    if (objIReader["RecordStatus"] != DBNull.Value)
                    {
                        VisitObj.RecordStatus = objIReader["RecordStatus"].ToString();
                    }

                    if (objIReader["UserID"] != DBNull.Value)
                    {
                        VisitObj.UserId = int.Parse(objIReader["UserID"].ToString());
                    }

                    if (objIReader["UserTypeId"] != DBNull.Value)
                    {
                        if (VisitObj.UserType == null)
                            VisitObj.UserType = new Quivus.Model.TypeDTOs.UserTypeDTO();

                        VisitObj.UserType.UserTypeId = int.Parse(objIReader["UserTypeId"].ToString());
                    }
                    if (objIReader["VisitStatus"] != DBNull.Value)
                    {
                        VisitObj.VisitStatus = objIReader["VisitStatus"].ToString();
                    }

                    VisitObj.RecGuid = GetDictationGUIDAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_GUID, VisitObj.RecGuid, IsTemplateVisit);
                    
                    VisitObj.DictationID = GetDictationGUIDAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_DICTATION_ID, VisitObj.DictationID, IsTemplateVisit);
                    
                    if (objIReader["OwnerUserID"] != DBNull.Value)
                    {
                        VisitObj.OwnerId = int.Parse(objIReader["OwnerUserID"].ToString());
                    }
                    if (objIReader["OwnerUserTypeID"] != DBNull.Value)
                    {
                        if (VisitObj.OwnerUserType == null)
                            VisitObj.OwnerUserType = new Quivus.Model.TypeDTOs.UserTypeDTO();

                        VisitObj.OwnerUserType.UserTypeId = int.Parse(objIReader["OwnerUserTypeID"].ToString());
                    }
                    if (objIReader["IsVirtualVisit"] != DBNull.Value)
                    {
                        VisitObj.IsVirtualVisit = bool.Parse(objIReader["IsVirtualVisit"].ToString());
                    }


                    VisitObj.SignedByUserId = GetKeyAsNullIfTemplate(objIReader, "SignedByUserID", VisitObj.SignedByUserId, IsTemplateVisit);

                    
                    if (objIReader["SignedByUserTypeId"] != DBNull.Value)
                    {
                        if (VisitObj.SignedByUserType == null)
                            VisitObj.SignedByUserType = new Quivus.Model.TypeDTOs.UserTypeDTO();

                        VisitObj.SignedByUserType.UserTypeId = GetKeyAsNullIfTemplate(objIReader, "SignedByUserTypeId", VisitObj.SignedByUserType.UserTypeId, IsTemplateVisit);
                        
                    }
                    if (objIReader["SignedDate"] != DBNull.Value)
                    {
                        VisitObj.SignedDate = DateTime.Parse(objIReader["SignedDate"].ToString());
                    }
                    if (objIReader["AppointmentID"] != DBNull.Value)
                    {
                        VisitObj.AppointmentId = int.Parse(objIReader["AppointmentID"].ToString());
                    }
                    if (objIReader["SoapTemplateID"] != DBNull.Value)
                    {
                        VisitObj.TemplateId = int.Parse(objIReader["SoapTemplateID"].ToString());
                    }
                    if (objIReader["EncounterTypeId"] != DBNull.Value)
                    {
                        VisitObj.EncounterTypeId = int.Parse(objIReader["EncounterTypeId"].ToString());
                    }

                    VisitObj.CreatedBy = GetKeyAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_CREATED_BY, VisitObj.CreatedBy, IsTemplateVisit);
                    
                    VisitObj.CreatedAt = GetDateAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_CREATED_AT, VisitObj.CreatedAt, IsTemplateVisit);
                    
                    VisitObj.ModifiedBy = GetKeyAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_MODIFIED_BY, VisitObj.ModifiedBy, IsTemplateVisit);
                    
                    VisitObj.ModifiedAt = GetDateAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_MODIFIED_AT, VisitObj.ModifiedAt, IsTemplateVisit);
                    
                    if (objIReader["IsValidDataExists"] != DBNull.Value)
                    {
                        VisitObj.IsValidDataExists = bool.Parse(objIReader["IsValidDataExists"].ToString());
                    }

                    if (objIReader["StationID"] != DBNull.Value)
                    {
                        VisitObj.CreatedStationID = int.Parse(objIReader["StationID"].ToString());
                    }

                    ////new fields for Billing 

                    //if (objIReader["PlaceOfServiceTypeID"] != DBNull.Value)
                    //{
                    //    VisitObj.PlaceOfServiceTypeID = int.Parse(objIReader["PlaceOfServiceTypeID"].ToString());
                    //}

                    //if (objIReader["IsClaimCreated"] != DBNull.Value)
                    //{
                    //    VisitObj.IsClaimCreated = bool.Parse(objIReader["IsClaimCreated"].ToString());
                    //}

                    //if (objIReader["ClaimCreatedDate"] != DBNull.Value)
                    //{
                    //    VisitObj.SignedDate = DateTime.Parse(objIReader["ClaimCreatedDate"].ToString());
                    //}
                }
            }
            return VisitObj;
        }
    }
}
