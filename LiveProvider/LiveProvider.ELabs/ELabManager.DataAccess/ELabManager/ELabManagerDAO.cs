using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.Constant.StoreProcedureConstant;
using LiveProvider.Utility;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.Constant.Errors;
using ELabManager.DataAccess.DB_Util;
using LiveProvider.Model.QuickInput;
using System.Data.SqlClient;
using LiveProvider.Utility.Utilities;
using LiveProvider.Model.Response.LabManagement;
using LiveProvider.Model.ELab;

namespace ELabManager.DataAccess.ELabManager
{
    public class ELabManagerDAO : BaseDAO
    {
        public List<LabNatureTypeDTO> LoadLabNatureTypes()
        {
            List<LabNatureTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LAB_NATURE_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LabNatureTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabNatureTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes()
        {
            List<LabTestPriorityTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LAB_TEST_PRIORITY_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestPriorityTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestPriorityTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes()
        {
            List<LabTestSpecimenTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LAB_TEST_SPECIMENT_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestSpecimenTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestSpecimenTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestTypeDTO> LoadLabTestTypes()
        {
            List<LabTestTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LAB_TEST_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTypeDTO> LoadLabTypes()
        {
            List<LabTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LAB_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes()
        {

            List<LabTestOrderMethodTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LAB_TEST_ORDER_METHOD_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestOrderMethodTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestOrderMethodTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.ICD_CODE_BY_NAME_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<ICDCodeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ICDCodeName", DbType.String, queryString);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ICDCodeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
        {
            List<LIONICLabTestTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LIONICLAB_TEST_TYPE_LOAD_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                list = new List<LIONICLabTestTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@LabtestTypeID", DbType.Int32, LabtestTypeID);
                db.AddInParameter(command, "@LabTestCodeOrName", DbType.String, LabTestCodeOrName);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LIONICLabTestTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public int ManagePatientLabs(LabDTO LabObject, bool IsUpdate)
        {
            int result = 0;
            string sProc = ELabSPConstant.LABS_INSERT_SP;
            try
            {
                if (IsUpdate)
                {
                    sProc = ELabSPConstant.LABS_UPDATE_SP;
                }

                LiveProviderLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                bool SkipRecord = ParameterPopulator.PopulateLabParameters(LabObject, IsUpdate, db, command);

                if (!SkipRecord)
                {
                    if (db.ExecuteNonQuery(command) == 0)
                    {
                        //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());

                        throw new RecordNotInsertException(ELabContant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                   ErrorCodes.SECTION_ELAB_MANAGEMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
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

        public List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, bool IsTemplateVisit, List<int> stationIDs, bool isAllSelected)
        {
            List<LabDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = ELabSPConstant.PATIENT_LABS_LOAD_BY_PATIENT_ID_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                List = new List<LabDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", clinicBranchID);
                clinicBranchIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter isAllSelectedPara = sqlCommand.Parameters.AddWithValue("@IsAllSelected", isAllSelected);
                isAllSelectedPara.SqlDbType = SqlDbType.Bit;
                if (stationIDs != null && stationIDs.Count > 0)
                {
                    dtStationIDs = DTOToTableConverter.StationIDsListConverter(stationIDs);
                }
                SqlParameter stationIdsTVPPara = sqlCommand.Parameters.AddWithValue("@StationIdsTVP", dtStationIDs);
                stationIdsTVPPara.SqlDbType = SqlDbType.Structured;
                stationIdsTVPPara.TypeName = ELabContant.STATION_IDS_TVP;
                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.PatientLabsByPatientIdConverter(objIReader, IsTemplateVisit);

                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        /// <summary>
        /// Author Sahibzada Sami 
        /// Date 10 Oct 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<LabDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.PATIENT_LABS_LOAD_BY_VISIT_ID_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<LabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientLabsConverter(objIReader, IsTemplateVisit);
                        //  GenericDataReaderToDTOConverter.ConvertToDTOList(HistoryList, objIReader, typeof(LabDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            List<LabDTO> LabList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = ELabSPConstant.PATIENT_LOAD_ALL_LABS;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                LabList = new List<LabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        LabList = DataReaderToDTOConverter.PatientLabsByPatientIdConverter(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return LabList;
        }

        public List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            List<FollowupLabsDTO> List = new List<FollowupLabsDTO>();

            IDataReader objIReader = null;
            string sProc = ELabSPConstant.FOLLOWUP_LABS_LOAD_BY_PATIENT_SP;

            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);

                List = new List<FollowupLabsDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name


                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.ConvertPatientFollowupLabs(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public LabPaggingResponseDTO LoadLabOrdersByClinicBranch(int clinicBranchID, int pageNo, int pageSize)
        {
            LabPaggingResponseDTO response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LOAD_ALL_LAB_ORDER_BY_CINIC_BRANCH;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            try
            {
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabOrderList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public StaticDataResponse LoadSearchFeilds()
        {
            StaticDataResponse response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LOAD_ALL_SEARCH_FIELDS;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            try
            {
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertSearchFeildsList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ResultPagingResponse LoadLabResultsByClinicBranch(int clinicBranchID, int pageNo, int pageSize)
        {
            ResultPagingResponse response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.LOAD_ALL_LAB_RESULT_BY_CINIC_BRANCH;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            try
            {
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabResultList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ProcessLabOrder(List<string> orderNumbers)
        {
            bool response = false;
            string sProc = ELabSPConstant.LABORDER_PROCESS_BY_ORDERNUMBERS;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            DataTable dtOrderNumber = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                SqlConnection connection = null;
                if (orderNumbers != null && orderNumbers.Count > 0)
                    dtOrderNumber = DTOToTableConverter.OrderNumbersListConverter(orderNumbers);
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpImmunization = sqlCommand.Parameters.AddWithValue("@OrderNumberList", dtOrderNumber);
                tvpImmunization.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpImmunization.TypeName = ELabContant.PATIENT_ORDERNUMBER_TVP;

                connection.Open();

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ELabContant.RECORD_NOT_INSERTED_ERROR_MSG, ELabContant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    response = true;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ProcessLabOrder(DateTime date)
        {
            bool response = false;
            string sProc = ELabSPConstant.LABORDER_PROCESS_BYDATE;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);
                db.AddInParameter(command, "@Date", DbType.DateTime, date);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ELabContant.RECORD_NOT_INSERTED_ERROR_MSG, ELabContant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    response = true;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LabPaggingResponseDTO SearchLabOrderByPatienOrProvidertIDs(int clinicBranchId,int pageNumber,int pageSize,int fieldId,List<int> patientIds)
        {
            LabPaggingResponseDTO response = null;
            IDataReader objIReader = null;
            DataTable dtPatientIDs = null;
            SqlConnection connection = null;
            string sProc = ELabSPConstant.SEARCH_ORDER_BY_PATIENT_OR_PROVIDER_IDS_SP;
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter clinicbranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchId", clinicBranchId);
                clinicbranchIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter pageNoPara = sqlCommand.Parameters.AddWithValue("@PageNo", pageNumber);
                pageNoPara.SqlDbType = SqlDbType.Int;
                SqlParameter pageSizePara = sqlCommand.Parameters.AddWithValue("@PageSize", pageSize);
                pageSizePara.SqlDbType = SqlDbType.Int;
                SqlParameter isAllSelectedPara = sqlCommand.Parameters.AddWithValue("@FieldId", fieldId);
                isAllSelectedPara.SqlDbType = SqlDbType.Int;
                if (patientIds != null && patientIds.Count > 0)
                {
                    dtPatientIDs = DTOToTableConverter.StationIDsListConverter(patientIds);
                }
                SqlParameter patientOrProviderIdsTVPPara = sqlCommand.Parameters.AddWithValue("@IDsList", dtPatientIDs);
                patientOrProviderIdsTVPPara.SqlDbType = SqlDbType.Structured;
                patientOrProviderIdsTVPPara.TypeName = ELabContant.STATION_IDS_TVP;
                connection.Open();
                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabOrderList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ResultPagingResponse SearchLabResultByPatientOrProviderIDs(int clinicBranchId, int pageNumber, int pageSize, int fieldId, List<int> patientIds)
        {
            ResultPagingResponse response = null;
            IDataReader objIReader = null;
            DataTable dtPatientIDs = null;
            SqlConnection connection = null;
            string sProc = ELabSPConstant.SEARCH_RESULT_BY_PATIENT_OR_PROVIDER_IDS_SP;
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter clinicbranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchId", clinicBranchId);
                clinicbranchIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter pageNoPara = sqlCommand.Parameters.AddWithValue("@PageNo", pageNumber);
                pageNoPara.SqlDbType = SqlDbType.Int;
                SqlParameter pageSizePara = sqlCommand.Parameters.AddWithValue("@PageSize", pageSize);
                pageSizePara.SqlDbType = SqlDbType.Int;
                SqlParameter isAllSelectedPara = sqlCommand.Parameters.AddWithValue("@FieldId", fieldId);
                isAllSelectedPara.SqlDbType = SqlDbType.Int;
                if (patientIds != null && patientIds.Count > 0)
                {
                    dtPatientIDs = DTOToTableConverter.StationIDsListConverter(patientIds);
                }
                SqlParameter patientOrProviderIdsTVPPara = sqlCommand.Parameters.AddWithValue("@IDsList", dtPatientIDs);
                patientOrProviderIdsTVPPara.SqlDbType = SqlDbType.Structured;
                patientOrProviderIdsTVPPara.TypeName = ELabContant.STATION_IDS_TVP;
                connection.Open();
                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabResultList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LabPaggingResponseDTO LoadLabOrdersByClinicBranchAndSearchString(int clinicBranchID, int pageNo, int pageSize, string searchText,DateTime date,int fieldID)
        {
            LabPaggingResponseDTO response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.SEARCH_ORDER_BY_SEARCHSTRING_IDS_SP;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            try
            {
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(command, "@FieldID", DbType.Int32, fieldID);
                if (fieldID == ELabContant.DATE_SEARCH_FIELD_ID)
                {
                    db.AddInParameter(command, "@Date", DbType.DateTime, date);
                }
                else
                {
                    db.AddInParameter(command, "@SearchText", DbType.String, searchText);
                }

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabOrderList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ResultPagingResponse LoadLabResultsByClinicBranchAndSearchString(int clinicBranchID, int pageNo, int pageSize, string searchText,DateTime date,int fieldID)
        {
            ResultPagingResponse response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.SEARCH_RESULT_BY_SEARCHSTRING_IDS_SP;
            LiveProviderLogger.Info("Calling SP:: " + sProc);
            try
            {
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(command, "@FieldID", DbType.Int32, fieldID);
                if (fieldID == ELabContant.DATE_SEARCH_FIELD_ID)
                {
                    db.AddInParameter(command, "@Date", DbType.DateTime, date);
                }
                else
                {
                    db.AddInParameter(command, "@SearchText", DbType.String, searchText);
                }

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabResultList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LabPaggingResponseDTO FilterPatientLabOrders(int clinicBranchId, int pageNumber, int pageSize, int labId, 
            int patientId, bool isPatientFilter, bool isLabFilter , bool isDatefilter, DateTime DateTo, DateTime DateFrom)
        {
            LabPaggingResponseDTO response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.FILTER_ORDER_BY_CLINIC_BRANCH_SP;
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@PageNo", DbType.Int32, pageNumber);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@LabID", DbType.Int32, labId);
                db.AddInParameter(command, "@DateTo", DbType.DateTime, DateTo);
                db.AddInParameter(command, "@DateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@IsDateFilter", DbType.Boolean, isDatefilter);
                db.AddInParameter(command, "@IsLabFilter", DbType.Boolean, isLabFilter);
                db.AddInParameter(command, "@IsPatientFilter", DbType.Boolean, isPatientFilter);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabOrderList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ResultPagingResponse FilterPatientLabResults(int clinicBranchId, int pageNumber, int pageSize, int labId, int patientId, 
            bool isPatientFilter, bool isLabFilter, bool isDatefilter, bool isStatusFilter, string status, DateTime DateTo, DateTime DateFrom)
        {
            ResultPagingResponse response = null;
            IDataReader objIReader = null;
            string sProc = ELabSPConstant.FILTER_RESULT_BY_CLINIC_BRANCH_SP;
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@ClinicBranchId", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@PageNo", DbType.Int32, pageNumber);
                db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@LabID", DbType.Int32, labId);
                db.AddInParameter(command, "@DateTo", DbType.DateTime, DateTo);
                db.AddInParameter(command, "@DateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@IsDateFilter", DbType.Boolean, isDatefilter);
                db.AddInParameter(command, "@IsLabFilter", DbType.Boolean, isLabFilter);
                db.AddInParameter(command, "@IsPatientFilter", DbType.Boolean, isPatientFilter);
                db.AddInParameter(command, "@IsLabStatus", DbType.Boolean, isStatusFilter);
                db.AddInParameter(command, "@Status", DbType.String, status);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        response = DataReaderToDTOConverter.ConvertLabResultList(objIReader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ProcessLabOrderByCriteria(int labId, int patientId, bool isPatientFilter, bool isLabFilter, bool isDatefilter, DateTime DateTo, DateTime DateFrom)
        {
            bool response = false;
            string sProc = ELabSPConstant.PROCESS_ORDER_BY_FILTER_SP;
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@LabID", DbType.Int32, labId);
                db.AddInParameter(command, "@DateTo", DbType.DateTime, DateTo);
                db.AddInParameter(command, "@DateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@IsDateFilter", DbType.Boolean, isDatefilter);
                db.AddInParameter(command, "@IsLabFilter", DbType.Boolean, isLabFilter);
                db.AddInParameter(command, "@IsPatientFilter", DbType.Boolean, isPatientFilter);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ELabContant.RECORD_NOT_INSERTED_ERROR_MSG, ELabContant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    response = true;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ManageLabOrder(OrderManagementDTO order)
        {
            bool response = false;
            string sProc = ELabSPConstant.UPDATE_LAB_ORDER_SP;
            if (order.IsDeleted)
            {
                sProc = ELabSPConstant.DELETE_LAB_ORDER_SP;
            }
            try
            {
                LiveProviderLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                if (!order.IsDeleted)
                {
                    db.AddInParameter(command, "@TestID", DbType.Int32, order.Test.TestId);
                    db.AddInParameter(command, "@LabID", DbType.Int32, order.LaboratoryID);
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, order.ModifiedBy);
                }
                db.AddInParameter(command, "@OrderID", DbType.Int32, order.OrderID);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ELabContant.RECORD_NOT_INSERTED_ERROR_MSG, ELabContant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    response = true;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool SignedLab(int VisitID)
        {
            bool response = false;
            string sProc = ELabSPConstant.SIGN_LABS_BY_VISIT;
            return response;
        }
    }
}
