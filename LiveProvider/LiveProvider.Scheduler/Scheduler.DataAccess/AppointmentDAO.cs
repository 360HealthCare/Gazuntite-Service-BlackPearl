using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;

using System.Data.Common;
using LiveProvider.Model.TypeDTOs.Appointment;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.Utilities;

using LiveProvider.Model.TypeDTOs;
using LiveProvider.Model.Appointment;
using Scheduler.DataAccess.DB_Util;
using LiveProvider.Utility;
using LiveProvider.Model.Response;


namespace Scheduler.DataAccess
{
    public class AppointmentDAO : BaseDAO
    {

        public List<AppointmentCurrentStatusTypeDTO> ListAllAppointmentCurrentStatusTypeDTOs()
        {
            List<AppointmentCurrentStatusTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.TYPE_LOAD_ALL_APPOINTMENT_CURRENT_STATUS_SP;
            try
            {
                LiveProviderLogger.Info("Loading ListAllAppointmentCurrentStatusTypeDTOs sProc::" + sProc);
                list = new List<AppointmentCurrentStatusTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AppointmentCurrentStatusTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<AppointmentRequestStatusTypeDTO> ListAllAppointmentRequestStatusTypeDTOs()
        {
            List<AppointmentRequestStatusTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.TYPE_LOAD_ALL_APPOINTMENT_REQUEST_STATUS_SP;
            try
            {
                LiveProviderLogger.Info("Loading ListAllAppointmentRequestStatusTypeDTOs sProc::" + sProc);
                list = new List<AppointmentRequestStatusTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AppointmentRequestStatusTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<AppointmentTypeDTO> ListAllAppointmentTypeDTOs()
        {
            List<AppointmentTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.TYPE_LOAD_ALL_APPOINTMENT_TYPE_SP;
            try
            {
                LiveProviderLogger.Info("Loading ListAllAppointmentTypeDTOs sProc::" + sProc);
                list = new List<AppointmentTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(AppointmentTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<SearchFieldTypeDTO> ListAllSearchFieldTypeDTOs()
        {
            List<SearchFieldTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.TYPE_LOAD_ALL_SEARCH_FIELD_TYPE_SP;
            try
            {
                LiveProviderLogger.Info("Loading ListAllSearchFieldTypeDTOs sProc::" + sProc);
                list = new List<SearchFieldTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SearchFieldTypeDTO));
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<AppointmentDTO> LoadAllAppointmentsByProviderIdAndStationsId(int providerUserId, int stationId, bool isAllProviderSelected, bool isAllLocationSelected, DateTime startDate, DateTime endDate)
        {
            List<AppointmentDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.APPOINTMENT_LOAD_BY_PROVIDER_AND_LOCATION_SP;
            try
            {
                LiveProviderLogger.Info("Loading LoadAllAppointmentsByProviderIdAndStationsId sProc::" + sProc);
                list = new List<AppointmentDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderUserId", DbType.Int32, providerUserId);
                db.AddInParameter(command, "@StationId", DbType.Int32, stationId);
                db.AddInParameter(command, "@IsAllProviderSelected", DbType.Boolean, isAllProviderSelected);
                db.AddInParameter(command, "@IsAllLocationSelected", DbType.Boolean, isAllLocationSelected);

                db.AddInParameter(command, "@StartDate", DbType.DateTime, startDate);
                db.AddInParameter(command, "@EndDate", DbType.DateTime, endDate);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.AppointmentDTOConverter(objIReader);
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public string GetNextVisit(int patientId, int stationId)
        {
            string sNextVisit = "";
            DateTime dtNextVisit = new DateTime();
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.APPOINTMENT_LOAD_NEXT_VISIT_SP;
            try
            {
                LiveProviderLogger.Info("Loading GetNextVisit sProc::" + sProc);


                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientId", DbType.Int32, patientId);
                db.AddInParameter(command, "@StationId", DbType.Int32, stationId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        if (objIReader.Read())
                        {
                            if (objIReader["AppointmentDate"] != DBNull.Value)
                            {
                                dtNextVisit = DateTime.Parse(objIReader["AppointmentDate"].ToString());

                                if (dtNextVisit > DateTime.Now)
                                {
                                    sNextVisit = "After " + dtNextVisit.Subtract(DateTime.Now).Days + " Days (" + dtNextVisit.ToString("MM/dd/yyyy") + ")";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return sNextVisit;
        }

        public List<AppointmentDTO> SearchAppointments(int stationId, string searchText, int feildID, int pageNo, int pageSize, DateTime fromDate, DateTime toDate, bool isDateSearch)
        {
            List<AppointmentDTO> list = null;
            IDataReader objIReader = null;
            string sProc = string.Empty;

            try
            {
                if (isDateSearch)
                {
                    sProc = SchedulerConstant.APPOINTMENT_DATE_SEARCH;
                }
                else
                {
                    sProc = SchedulerConstant.APPOINTMENT_SEARCH_SP;
                }


                LiveProviderLogger.Info("Loading SearchAppointments sProc::" + sProc);
                list = new List<AppointmentDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                PopulateAppointmentSearchParameters(stationId, searchText, feildID, pageNo, pageSize, db, command, fromDate, toDate, isDateSearch);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        if (objIReader.Read())
                        {
                            objIReader.NextResult();

                            list = DataReaderToDTOConverter.AppointmentDTOConverter(objIReader);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<AppointmentDTO> LoadWaitingRoom(int stationId)
        {
            List<AppointmentDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.WAITING_ROOM_LOAD_SP;
            try
            {
                LiveProviderLogger.Info("Loading LoadWaitingRoom sProc::" + sProc);
                list = new List<AppointmentDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@StationId", DbType.Int32, stationId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.AppointmentDTOConverter(objIReader);
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }




        public int ManageAppointment(AppointmentDTO appointment)
        {
            int UpdateResult = 0;
            string sProc_Insert = SchedulerConstant.APPOINTMENT_INSERT_SP;
            string sProc_Update = SchedulerConstant.APPOINTMENT_UPDATE_SP;
            string sproc_Delate = SchedulerConstant.APPOINTMENT_DELETE_SP;
            bool IsUpdate = false;
            string sProc = sProc_Insert;
            try
            {
                if (appointment.AppointmentId > 0) /* If UserUserProfileCategoryID is provide Simply Update Record */
                {
                    sProc = sProc_Update;
                    IsUpdate = true;
                }
                if (appointment.IsDeleted)
                {
                    sProc = sproc_Delate;
                }

                LiveProviderLogger.Info("Loading ManageAppointment sProc::" + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                if (!appointment.IsDeleted)
                {
                    db.AddInParameter(command, "@StationId", DbType.Int32, appointment.StationId);

                    if (appointment.AppointmentRequestId != 0)
                    {
                        db.AddInParameter(command, "@AppointmentRequestId", DbType.Int32, appointment.AppointmentRequestId);
                    }
                    else
                        db.AddInParameter(command, "@AppointmentRequestId", DbType.Int32, DBNull.Value);

                    db.AddInParameter(command, "@ProviderUserId", DbType.Int32, appointment.ProviderUserId);

                    db.AddInParameter(command, "@PatientId", DbType.Int32, appointment.Patient.PatientID);

                    db.AddInParameter(command, "@AppointmentDate", DbType.DateTime, appointment.AppointmentDate);

                    db.AddInParameter(command, "@AppointmentStartTime", DbType.DateTime, DateTime.Parse(appointment.AppointmentStartTime));

                    db.AddInParameter(command, "@AppointmentEndTime", DbType.DateTime, DateTime.Parse(appointment.AppointmentEndTime));

                    db.AddInParameter(command, "@AppointmentTypeId", DbType.Int32, appointment.AppointmentType.AppointmentTypeId);

                    db.AddInParameter(command, "@AppointmentCurrentStatusId", DbType.Int32, appointment.AppointmentCurrentStatus.AppointmentCurrentStatusId);

                    db.AddInParameter(command, "@Complaint", DbType.String, appointment.ChiefComplaint);

                    db.AddInParameter(command, "@RoomId", DbType.Int32, appointment.ClinicRoomId);

                    db.AddInParameter(command, "@InformationAlert", DbType.String, appointment.InformationAlert);


                    db.AddInParameter(command, "@PatientFirstName", DbType.String, appointment.Patient.FirstName);
                    db.AddInParameter(command, "@PatientLastName", DbType.String, appointment.Patient.LastName);
                    db.AddInParameter(command, "@PatientMiddleName", DbType.String, appointment.Patient.MiddleName);

                    db.AddInParameter(command, "@ProviderFirstName", DbType.String, appointment.ProviderFirstName);
                    db.AddInParameter(command, "@ProviderLastName", DbType.String, appointment.ProviderLastName);
                    db.AddInParameter(command, "@ProviderMiddleName", DbType.String, appointment.ProviderMiddleName);

                    db.AddInParameter(command, "@LocationName", DbType.String, appointment.StationName);

                    db.AddInParameter(command, "@QuivusId", DbType.String, appointment.QuivusId);

                    db.AddInParameter(command, "@LastVisit", DbType.String, appointment.LastVisit);

                    db.AddInParameter(command, "@ClinicRoomId", DbType.Int32, appointment.Room.ClinicRoomId);
                    if (IsUpdate)
                    {
                        db.AddInParameter(command, "@ModifiedBy", DbType.Int32, appointment.ModifiedBy);
                        db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, appointment.ModifiedAt);

                        db.AddInParameter(command, "@AppointmentID", DbType.Int32, appointment.AppointmentId);
                        db.AddOutParameter(command, "@AppointmentIDOUT", DbType.Int32, 1);

                    }
                    else
                    {
                        db.AddInParameter(command, "@CreatedBy", DbType.Int32, appointment.CreatedBy);
                        db.AddInParameter(command, "@CreatedAt", DbType.DateTime, appointment.CreatedAt);

                        db.AddOutParameter(command, "@AppointmentIDOUT", DbType.Int32, 1); // Specify Out Param In Insertion Case only
                    }

                    if (db.ExecuteNonQuery(command) == 0)
                    {
                        UpdateResult = 0;
                    }
                    else
                    {
                        UpdateResult = int.Parse(db.GetParameterValue(command, "@AppointmentIDOUT").ToString());
                    }
                }
                else
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, appointment.AppointmentId);
                    db.AddInParameter(command, "@ModifiedBy", DbType.Int32, appointment.ModifiedBy);
                    db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, appointment.ModifiedAt);
                    if (db.ExecuteNonQuery(command) == 0)
                    {
                        UpdateResult = 0;
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        public bool ManageAppointmentRequestStatus(bool requestStatus, int requestId)
        {
            bool UpdateResult = false;
            string sProc = SchedulerConstant.APPOINTMENTREQUEST_UPADTESTATUS_SP;
            try
            {

                LiveProviderLogger.Info("ManageAppointmentRequestStatus sProc::" + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@AppointmentRequestID", DbType.Int32, requestId);
                db.AddInParameter(command, "@AppointmentRequestStatus", DbType.Boolean, requestStatus);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    UpdateResult = false;
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        internal static bool PopulateAppointmentSearchParameters(int clinicBranchID, string searchText, int feildID, int pageNo, int pageSize, Database db, DbCommand command, DateTime fromDate, DateTime toDate, bool isDateSearch)
        {
            bool Response = false;
            //DateTime dateSearch;
            try
            {
                if (clinicBranchID > 0)
                    db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                if (!string.IsNullOrEmpty(searchText) && feildID != SchedulerConstant.DATE_SEARCH_VALUE && feildID != 0)
                    db.AddInParameter(command, "@SearchText", DbType.String, searchText);
                //if (!string.IsNullOrEmpty(searchText) && feildID == SchedulerConstant.DATE_SEARCH_VALUE)
                //{
                //    //dateSearch = DateTime.Parse(searchText);
                //    db.AddInParameter(command, "@DateSearch", DbType.DateTime, toDate);
                //}
                if (feildID > 0 && feildID != SchedulerConstant.DATE_SEARCH_VALUE)
                {
                    db.AddInParameter(command, "@FeildID", DbType.Int32, feildID);
                }

                if (pageNo > 0)
                    db.AddInParameter(command, "@PageNo", DbType.Int32, pageNo);
                if (pageSize > 0)
                    db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);

                if (isDateSearch)
                {
                    //db.AddInParameter(command, "@DateFrom", DbType.DateTime, fromDate);
                    db.AddInParameter(command, "@DateSearch ", DbType.DateTime, searchText);

                }
            }
            catch
            {
                throw;
            }
            return Response;
        }







        public int SaveAppointmentRequest(AppointmentRequestDTO appRequest)
        {
            int UpdateResult = 0;
            string sProc = SchedulerConstant.APP_REQUEST_INSERT_SP;

            try
            {
                LiveProviderLogger.Info("Loading SaveAppointmentRequest sProc::" + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientId", DbType.Int32, appRequest.PatientID);

                db.AddInParameter(command, "@StationId", DbType.Int32, appRequest.StationID);

                db.AddInParameter(command, "@AppointmentRequestDate", DbType.DateTime, appRequest.AppRequestDate);

                db.AddInParameter(command, "@AppointmentStartTime", DbType.DateTime, appRequest.AppointmentStartTime);

                db.AddInParameter(command, "@AppointmentEndTime", DbType.DateTime, appRequest.AppointmentEndTime);

                db.AddInParameter(command, "@AppointmentRequestStatusId", DbType.Int32, appRequest.AppRequestStatus.AppointmentRequestStatusId);

                db.AddInParameter(command, "@IsEmailSent", DbType.Boolean, appRequest.IsEmailSent);

                db.AddInParameter(command, "@CreatedBy", DbType.Int32, appRequest.CreatedBy);

                db.AddInParameter(command, "@CreatedAt", DbType.DateTime, appRequest.CreatedAt);

                db.AddInParameter(command, "@ProviderId", DbType.Int32, appRequest.ProviderID);

                db.AddInParameter(command, "@ProviderUserID", DbType.Int32, appRequest.ProviderUserID);

                db.AddInParameter(command, "@ProviderTypeID", DbType.Int32, appRequest.ProviderTypeID);

                db.AddInParameter(command, "@ProviderName", DbType.String, appRequest.ProviderName);

                db.AddInParameter(command, "@ProviderSpecialityId", DbType.Int32, appRequest.ProviderSpecialityId);

                db.AddInParameter(command, "@LocationName", DbType.String, appRequest.LocationName);

                db.AddInParameter(command, "@LocationAddress", DbType.String, appRequest.LocationAddress);

                db.AddInParameter(command, "@QuivusId", DbType.String, appRequest.QuivusId);   
             

                db.AddOutParameter(command, "@AppointmentRequestIDOUT", DbType.Int32, 1); // Specify Out Param In Insertion Case only

                if (db.ExecuteNonQuery(command) == 0)
                {
                    UpdateResult = 0;
                }
                else
                {
                    UpdateResult = int.Parse(db.GetParameterValue(command, "@AppointmentRequestIDOUT").ToString());
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        public bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt)
        {
            bool UpdateResult = false;
            string sProc = SchedulerConstant.APP_REQUEST_DELETE_SP;
            try
            {

                LiveProviderLogger.Info("Loading DeleteAppointmentRequest sProc::" + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@AppointmentRequestID", DbType.Int32, appointmentId);
                db.AddInParameter(command, "@ModifiedBy", DbType.Int32, modifiedBy);
                db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, modifiedAt);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    UpdateResult = false;
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }



        public List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId)
        {
            List<AppointmentRequestDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.APP_REQUEST_LOAD_BY_PATIENT_SP;
            try
            {
                LiveProviderLogger.Info("Loading ListAllPatientAppointmentRequests sProc::" + sProc);
                list = new List<AppointmentRequestDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientId", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.AppointmentRequestDTOConverter(objIReader);
                    }
                }
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public AppointmentRequestDTO ListAppointmentRequestsByRequestId(int requestId)
        {
            List<AppointmentRequestDTO> list = null;
            AppointmentRequestDTO request = new AppointmentRequestDTO();
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.APP_REQUEST_LOAD_BY_REQUEST_ID_SP;
            try
            {
                LiveProviderLogger.Info("ListAppointmentRequestsByRequestId sProc::" + sProc);
                list = new List<AppointmentRequestDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "requestId", DbType.Int32, requestId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.AppointmentRequestDTOConverter(objIReader);
                    }
                }
                if (list != null && list.Count > 0)
                {
                    request = list[0];
                }

            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return request;
        }

        public AppointmentRequestDTOResponse ListAllPatientAppointmentRequestsByProvider(int providerId)
        {
            AppointmentRequestDTOResponse response = new AppointmentRequestDTOResponse();
            List<AppointmentRequestDTO> list = null;
            IDataReader objIReader = null;
            string sProc = SchedulerConstant.APP_REQUEST_LOAD_BY_PROVIDER_SP;
            try
            {
                LiveProviderLogger.Info("ListAllPatientAppointmentRequestsByProvider sProc::" + sProc);
                list = new List<AppointmentRequestDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ProviderId", DbType.Int32, providerId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.AppointmentRequestDTOConverter(objIReader);
                    }
                }
                response.AppointmentRequestList = list;
            }
            catch (Exception exp)
            {
                LiveProviderLogger.Error(exp);
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return response;
        }


    }
}
