using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.Appointment;
using Scheduler.DataAccess;
using LiveProvider.Utility.Logger;
using LiveProvider.Utility.Factory;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Business;
using LiveProvider.Utility.Utilities;
using LiveProvider.Model.Appointment;
using LiveProvider.Model.Response;
using Scheduler.Business.Converter;

using Scheduler.Business.UserManagerService;
using Scheduler.Business.VisitService;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Model.Response.ClinicRoom;
using LiveProvider.Model.Appointment.ClinicRoom;

namespace Scheduler.Business.BL.Impl
{
    public class SchedulerManagerImpl : ISchedulerManager
    {
        AppointmentDAO appointmentDAO = null;

        UserManagerServiceClient UserServiceClient = null;
        VisitServiceClient clientVisit = null;
        List<RoomDTO> RoomList=new List<RoomDTO>();

        public List<AppointmentCurrentStatusTypeDTO> ListAllAppointmentCurrentStatusTypeDTOs()
        {
            List<AppointmentCurrentStatusTypeDTO> list = null;

            try
            {
                LiveProviderLogger.Info("ListAllAppointmentCurrentStatusTypeDTOs Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                list = appointmentDAO.ListAllAppointmentCurrentStatusTypeDTOs();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<AppointmentRequestStatusTypeDTO> ListAllAppointmentRequestStatusTypeDTOs()
        {
            List<AppointmentRequestStatusTypeDTO> list = null;

            try
            {
                LiveProviderLogger.Info("ListAllAppointmentRequestStatusTypeDTOs Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                list = appointmentDAO.ListAllAppointmentRequestStatusTypeDTOs();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<AppointmentTypeDTO> ListAllAppointmentTypeDTOs()
        {
            List<AppointmentTypeDTO> list = null;

            try
            {
                LiveProviderLogger.Info("ListAllAppointmentTypeDTOs Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                list = appointmentDAO.ListAllAppointmentTypeDTOs();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<SearchFieldTypeDTO> ListAllSearchFieldTypeDTOs()
        {
            List<SearchFieldTypeDTO> list = null;

            try
            {
                LiveProviderLogger.Info("ListAllSearchFieldTypeDTOs Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                list = appointmentDAO.ListAllSearchFieldTypeDTOs();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<LiveProvider.Model.Appointment.ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID, List<int> stationIds, bool isLoadAll)
        {
            List<Scheduler.Business.UserManagerService.ProviderDTO> uList = null;

            List<LiveProvider.Model.Appointment.ProviderDTO> sList = null;

            try
            {
                LiveProviderLogger.Info("LoadAllProviderByClinicBranchId Called");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                uList = UserServiceClient.LoadAllProviderByClinicBranchIdAndStationId(clinicBranchID, stationIds, isLoadAll);

                sList = new List<LiveProvider.Model.Appointment.ProviderDTO>();

                GenericCloneCreator.convertList(uList, ref sList, typeof(LiveProvider.Model.Appointment.ProviderDTO));

            }
            catch
            {
                throw;
            }
            return sList;
        }

        public List<LiveProvider.Model.Common.StationDTO> LoadAllStationsByClinicId(int clinicBranchID)
        {
            List<Scheduler.Business.UserManagerService.StationDTO> uList = null;

            List<LiveProvider.Model.Common.StationDTO> sList = null;

            try
            {
                LiveProviderLogger.Info("LoadAllStationsByClinicId Called");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                uList = UserServiceClient.LoadAllStationsByClinicId(clinicBranchID);

                sList = new List<LiveProvider.Model.Common.StationDTO>();

                GenericCloneCreator.convertList(uList, ref sList, typeof(LiveProvider.Model.Common.StationDTO));

            }
            catch
            {
                throw;
            }
            return sList;
        }

        public List<LocationDTO> LoadAllAppointmentsByProviderIdAndStationsId(int providerID, int stationId, bool isAllProviderSelected, bool isAllLocationSelected, int viewTypeId)
        {
            List<LocationDTO> locList = null;

            try
            {
                DateTime dtNow = DateTime.Now;

                DateTime startDate = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day);

                DateTime endDate = AppointmentUtility.GetEndDateByViewType(viewTypeId);

                locList = new List<LocationDTO>();

                List<AppointmentDTO> appList = new List<AppointmentDTO>();

                LiveProviderLogger.Info("LoadAllAppointmentsByProviderIdAndStationsId Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }
                

                appList = appointmentDAO.LoadAllAppointmentsByProviderIdAndStationsId(providerID, stationId, isAllProviderSelected, isAllLocationSelected, startDate, endDate);

                LoadClinicRoomByStationtId(stationId.ToString());

                CustomDTOConverter.PoulateAppointmentDictionary(appList, ref locList, RoomList);

            }
            catch
            {
                throw;
            }
            return locList;
        }

        public PatientSearchDTOResponse SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo)
        {
            PatientSearchDTOResponse sResponse = null;

            Scheduler.Business.UserManagerService.PatientSearchResultDTO uResponse = null;

            try
            {
                LiveProviderLogger.Info("SearchPatientbyClinicIdAndSearchString Called");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                uResponse = UserServiceClient.SearchPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo);

                sResponse = new PatientSearchDTOResponse();

                GenericCloneCreator.convertObject(uResponse, ref sResponse);
                
                //PatientList
                List<LiveProvider.Model.Common.PatientSearchDTO> sPatientSearchList = new List<LiveProvider.Model.Common.PatientSearchDTO>();

                GenericCloneCreator.convertList(uResponse.PatientSearchList, ref sPatientSearchList, typeof(LiveProvider.Model.Common.PatientSearchDTO));

                sResponse.PatientSearchList = sPatientSearchList;

            }
            catch
            {
                throw;
            }
            return sResponse;
        }

        public PatientCompleteInfoDTO LoadPatientCompleteInfo(int patientId, int clinicBranchId, int providerUserId, int stationId)
        {
            PatientCompleteInfoDTO sResponse = null;

            Scheduler.Business.UserManagerService.PatientProfileDTO uResponse = null;

            try
            {
                LiveProviderLogger.Info("LoadPatientCompleteInfo Called");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                // Patient Info                

                uResponse = UserServiceClient.LoadPatientCompleteProfileForClinic(patientId, clinicBranchId, providerUserId);

                sResponse = new PatientCompleteInfoDTO();

                CustomDTOConverter.PopulatePatientInfo(uResponse, ref sResponse);

                // VisitInfo

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                List<PatientLastSeenDTO> listPatientSeen = clientVisit.LoadPatientLastSeenByPatientId(patientId);

                CustomDTOConverter.PopulateVisitInfo(listPatientSeen, ref sResponse);

                //Next Appintment
                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                //sResponse.Visit = new LiveProvider.Model.Appointment.VisitDTO();

                sResponse.Visit.NextVisit = appointmentDAO.GetNextVisit(patientId, stationId);

            }
            catch (Exception ex)
            {
                LiveProviderLogger.Error(ex.Message + " /n" + ex.InnerException);

                throw;
            }
            return sResponse;
        }

        public List<LiveProvider.Model.Appointment.Provider.WorkingHourDTO> LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day)
        {
            List<LiveProvider.Model.Appointment.Provider.WorkingHourDTO> responseList = null;

            Scheduler.Business.UserManagerService.WorkingHourDTO uResponse = null;

            try
            {
                LiveProviderLogger.Info("LoadProviderWorkingHours Called");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }

                //Working Hours               

                uResponse = UserServiceClient.LoadProviderWorkingHours(providerUserId);


                //Next Appintment
                List<AppointmentDTO> appList = new List<AppointmentDTO>();
                
                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                appList = appointmentDAO.LoadAllAppointmentsByProviderIdAndStationsId(providerId, stationId, false, false, day, day);

                
                //Ceate Slots

                //List<string> timeSlots = new List<string>();
                responseList = new List<LiveProvider.Model.Appointment.Provider.WorkingHourDTO>();
                
                if (appList.Count > 0)
                {
                    DateTime start = DateTime.Parse(uResponse.StartTime);
                    DateTime end = DateTime.Parse(uResponse.EndTime);

                    do
                    {
                        DateTime next = start.AddMinutes(15);

                        LiveProvider.Model.Appointment.Provider.WorkingHourDTO workSlot = new LiveProvider.Model.Appointment.Provider.WorkingHourDTO();
                        workSlot.StartTime = start.ToString("hh:mm tt");
                        workSlot.EndTime = next.ToString("hh:mm tt");

                        responseList.Add(workSlot);

                        start = next;
                    }
                    while (start < end);

                    foreach (AppointmentDTO appointment in appList)
                    {
                        DateTime tempEndTime = DateTime.Parse(appointment.AppointmentStartTime);
                        DateTime tempStartTime = DateTime.Parse(appointment.AppointmentStartTime);
                        if (tempEndTime.Subtract(tempStartTime) == TimeSpan.FromMinutes(15))
                        {
                            LiveProvider.Model.Appointment.Provider.WorkingHourDTO bookedSlot = new LiveProvider.Model.Appointment.Provider.WorkingHourDTO();

                            bookedSlot.StartTime = tempStartTime.ToString("hh:mm tt");
                            bookedSlot.EndTime = tempEndTime.ToString("hh:mm tt");

                            responseList.Remove(bookedSlot);
                        }
                        else
                        {
                            DateTime startTime = tempEndTime;
                            DateTime endTime = tempEndTime;
                            do
                            {
                                DateTime nextTime = startTime.AddMinutes(15);

                                LiveProvider.Model.Appointment.Provider.WorkingHourDTO time = new LiveProvider.Model.Appointment.Provider.WorkingHourDTO();
                                time.StartTime = start.ToString("hh:mm tt");
                                time.EndTime = nextTime.ToString("hh:mm tt");

                                responseList.Remove(time);

                                startTime = nextTime;
                            }
                            while (startTime < endTime);
                        }
                    }
                }
                else
                {
                    //user_qv_doctor doctor = user_qv_doctor.Where("it.DoctorId = @doctorId", new System.Data.Objects.ObjectParameter("doctorId", doctorId)).First();
                    DateTime start = DateTime.Parse(uResponse.StartTime);
                    DateTime end = DateTime.Parse(uResponse.EndTime);
                    do
                    {
                        DateTime next = start.AddMinutes(15);
                        LiveProvider.Model.Appointment.Provider.WorkingHourDTO time = new LiveProvider.Model.Appointment.Provider.WorkingHourDTO();

                        time.StartTime = start.ToString("hh:mm tt");
                        time.EndTime = next.ToString("hh:mm tt");

                        responseList.Add(time);

                        start = next;
                    }
                    while (start < end);
                }
            }
            catch
            {
                throw;
            }
            return responseList;
        }

        public List<AppointmentDTO> SearchAppointments(int providerUserId, int providerId, int stationId, int fieldTypeId, string searchText, int pageNo)
        {
            List<AppointmentDTO> list = null;
            bool isDateSearch = false;
            try
            {
                LiveProviderLogger.Info("SearchAppointments Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                if (fieldTypeId == SchedulerConstant.DATE_SEARCH_VALUE)
                    isDateSearch = true;

                list = appointmentDAO.SearchAppointments(stationId, searchText, fieldTypeId, pageNo, SchedulerConstant.PAGE_SIZE, DateTime.Now, DateTime.Now, isDateSearch);
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<AppointmentDTO> LoadWaitingRoom(int stationId)
        {
            List<AppointmentDTO> list = null;

            try
            {
                LiveProviderLogger.Info("LoadWaitingRoom Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                list = appointmentDAO.LoadWaitingRoom(stationId);
                LoadClinicRoomByStationtId(stationId.ToString());
                foreach (AppointmentDTO app in list)
                {
                    foreach (RoomDTO room in RoomList)
                    {
                        if (app.Room.ClinicRoomId== room.ClinicRoomId)
                        {
                            app.Room = room;
                            break;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return list;
        }


        public int SaveAppointmentRequest(AppointmentRequestDTO appRequest)
        {
            int result = 0;

            try
            {
                LiveProviderLogger.Info("SaveAppointmentRequest Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                result = appointmentDAO.SaveAppointmentRequest(appRequest);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt)
        {
            bool result = false;

            try
            {
                LiveProviderLogger.Info("DeleteAppointmentRequest Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                result = appointmentDAO.DeleteAppointmentRequest(appointmentId, modifiedBy, modifiedAt);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId)
        {
            List<AppointmentRequestDTO> list = null;

            try
            {
                LiveProviderLogger.Info("ListAllPatientAppointmentRequests Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }

                list = appointmentDAO.ListAllPatientAppointmentRequests(patientId);
            }
            catch
            {
                throw;
            }
            return list;
        }

        public AppointmentRequestDTOResponse LoadAllAppointmentRequestByProvider(int providerId, int clinicBanchId)
        {
            AppointmentRequestDTOResponse response = null;
            try
            {
                LiveProviderLogger.Info("LoadAllAppointmentRequestByProvider Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }
                response = appointmentDAO.ListAllPatientAppointmentRequestsByProvider(providerId);
                response.AppointmentRequestList = appendPatientInfo(response.AppointmentRequestList, clinicBanchId);

            }
            catch
            {
                throw;
            }
            return response;
        }

        private List<AppointmentRequestDTO> appendPatientInfo(List<AppointmentRequestDTO> list,int clinicBranchId)
        {
            List<AppointmentRequestDTO> destList = new List<AppointmentRequestDTO>();
            foreach (AppointmentRequestDTO request in list)
            {
                PatientCompleteInfoDTO patInfo = LoadPatientCompleteInfo(request.PatientID, 7, clinicBranchId, request.StationID);
                request.Patient = new PatientInfoDTO();
                request.Patient.FirstName = patInfo.FirstName;
                request.Patient.LastName = patInfo.LastName;
                request.Patient.MiddleName = patInfo.MiddleName;
                request.Patient.PatientID = patInfo.PatientId;
                destList.Add(request);
            }
            return destList;
        }


        public int ManageAppointment(AppointmentDTO appointment, int providerId, int userTypeId)
        {
            int result = 0;

            try
            {
                LiveProviderLogger.Info("ListAllAppointmentRequestStatusTypeDTOs Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }
                if (appointment.Room == null)
                    appointment.Room = new RoomDTO();

                result = appointmentDAO.ManageAppointment(appointment);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool ManageAppointmentRequest(int requestId, int statusId, int modifiedBy, DateTime modifiedAt, bool reqStatus, int clinicBranchId)
        {
            bool response = false;
            AppointmentRequestDTO request = null;
            AppointmentDTO app = null;
            try
            {
                LiveProviderLogger.Info("ManageAppointmentRequest Called");

                if (appointmentDAO == null)
                {
                    appointmentDAO = (AppointmentDAO)DAOFactory.GetDataAccessObject(typeof(AppointmentDAO));
                }
                if (reqStatus)
                {
                    request = appointmentDAO.ListAppointmentRequestsByRequestId(requestId);
                    PatientCompleteInfoDTO patInfo = LoadPatientCompleteInfo(request.PatientID, 7, clinicBranchId, request.StationID);

                    LiveProviderLogger.Info("ManageAppointmentRequest :: LoadPatientCompleteInfo called Sucesfully");

                    request.Patient = new PatientInfoDTO();
                    request.Patient.FirstName = patInfo.FirstName;
                    request.Patient.LastName = patInfo.LastName;
                    request.Patient.MiddleName = patInfo.MiddleName;
                    request.Patient.PatientID = patInfo.PatientId;
                    

                    app = createAppointment(statusId, modifiedBy, modifiedAt, reqStatus, request);

                    LiveProviderLogger.Info("ManageAppointmentRequest :: createAppointment called Sucesfully");

                    int res = appointmentDAO.ManageAppointment(app);
                    if (res > 0)
                    {
                        response = appointmentDAO.ManageAppointmentRequestStatus(reqStatus, requestId);
                    }                    
                }
                else
                {
                    response = appointmentDAO.ManageAppointmentRequestStatus(reqStatus, requestId);
                }
            }
            catch (Exception ex)
            {
                LiveProviderLogger.Error("EX :: " + ex.InnerException);

                throw;
            }
            return response;
        }

        private AppointmentDTO createAppointment(int statusId, int modifiedBy, DateTime modifiedAt, bool reqStatus, AppointmentRequestDTO request)
        {
            AppointmentDTO app = new AppointmentDTO();
            if (request != null)
            {
                app.AppointmentId = 0;
                app.AppointmentCurrentStatus = new AppointmentCurrentStatusTypeDTO();
                app.AppointmentCurrentStatus.AppointmentCurrentStatusId = statusId;
                app.AppointmentDate = request.AppRequestDate.ToString();
                app.AppointmentEndTime = request.AppointmentEndTime.ToString();
                app.AppointmentRequestId = request.RequestID;
                app.AppointmentStartTime = request.AppointmentStartTime.ToString();
                app.AppointmentStatus = request.AppRequestStatus;
                app.AppointmentType = new AppointmentTypeDTO();
                app.AppointmentType.AppointmentTypeId = request.AppointmentTypeId;
                //app.ClinicRoomId
                app.CreatedAt = request.CreatedAt;
                app.CreatedBy = request.CreatedBy;
                //app.InformationAlert=request.
                //app.LastVisit=request.l
                app.ModifiedAt = modifiedAt;
                app.ModifiedBy = modifiedBy;
                app.Patient = request.Patient;
                app.QuivusId = request.QuivusId;

                string[] providerFullName = request.ProviderName.Split(' ');

                if (providerFullName.Length > 2)
                { 
                    app.ProviderFirstName = providerFullName[0];
                    app.ProviderMiddleName = providerFullName[1];
                    app.ProviderLastName = providerFullName[2];
                }
                if (providerFullName.Length > 1)
                {
                    app.ProviderFirstName = providerFullName[0];
                    app.ProviderLastName = providerFullName[1];
                }
                else
                {
                    app.ProviderFirstName = request.ProviderName;
                    app.ProviderLastName = "";          
                }
                app.ProviderUserId = request.ProviderUserID.ToString();
                //app.QuivusId=request.q
                app.StationId = request.StationID;
                app.StationName = request.LocationName;
                app.Room = new RoomDTO();
            }
            return app;
        }

        public ClinicRoomResponse LoadClinicRoomByStationtId(string stationId)
        {
            ClinicRoomResponse response = null;


            List<UserManagerService.ClinicRoomDTO> uList = null;

            try
            {
                LiveProviderLogger.Info("LoadAllStationsByClinicId Called");

                if (UserServiceClient == null)
                {
                    UserServiceClient = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                }
                //if (RoomList.Count == 0)
                {
                    uList = UserServiceClient.LoadClinicRoomByStationId(int.Parse(stationId));
                    GenericCloneCreator.convertList(uList, ref RoomList, typeof(LiveProvider.Model.Appointment.ClinicRoom.RoomDTO));
                }
                //else
                //{
                //    int id =int.Parse(stationId);
                //    IEnumerable<RoomDTO> tempList = from room in RoomList
                //                                    where room.StationId == id
                //                                    select room;
                //    RoomList = tempList.ToList<RoomDTO>();
                //    if (RoomList.Count == 0)
                //    {
                //        uList = UserServiceClient.LoadClinicRoomByStationId(int.Parse(stationId));
                //        GenericCloneCreator.convertList(uList, ref RoomList, typeof(LiveProvider.Model.Appointment.ClinicRoom.RoomDTO));
                //    }
                //}
                response = new ClinicRoomResponse();
                response.ClinicRoomList = RoomList;
            }
            catch
            {
                throw;
            }
            return response;
        }

    }
}
