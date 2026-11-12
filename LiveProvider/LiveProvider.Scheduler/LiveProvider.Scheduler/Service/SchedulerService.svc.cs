using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LiveProvider.Model.Response;
using LiveProvider.Model.TypeDTOs.Appointment;
using Scheduler.Business.BL;
using Scheduler.Business.BL.Impl;
using LiveProvider.Business;
using LiveProvider.Model.Appointment;
using System.Xml.Linq;
using System.Xml;
using LiveProvider.Utility.Utilities;
using System.IO;
using System.Xml.Serialization;
using LiveProvider.Model.Response.ClinicRoom;
//using LiveProvider.Model.Request.Scheduler;


namespace SchedulerService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class SchedulerService : QuivusBaseService, ISchedulerService
    {
        #region Default Implementation of GetServiceVersion
        public override string GetServiceVersion()
        {
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return strVersion;
        }
        #endregion

        ISchedulerManager scheduler = null;


        public AppointmentTypeDTOResponse GetAllAppointmentTypes()
        {
            AppointmentTypeDTOResponse response = new AppointmentTypeDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.AppointmentTypeList = scheduler.ListAllAppointmentTypeDTOs();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public AppointmentRequestStatusTypeDTOResponse GetAllAppointmentRequestStatusTypes()
        {
            AppointmentRequestStatusTypeDTOResponse response = new AppointmentRequestStatusTypeDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.RequestStatusTypeList = scheduler.ListAllAppointmentRequestStatusTypeDTOs();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public AppointmentCurrentStatusTypeDTOResponse GetAllAppointmentCurrentStatusTypeDTOs()
        {
            AppointmentCurrentStatusTypeDTOResponse response = new AppointmentCurrentStatusTypeDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.AppointmentCurrentSatatusList = scheduler.ListAllAppointmentCurrentStatusTypeDTOs();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public SearchFieldTypeDTOResponse GetAllSearchFieldTypeDTOs()
        {
            SearchFieldTypeDTOResponse response = new SearchFieldTypeDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.SearchTypeList = scheduler.ListAllSearchFieldTypeDTOs();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ProviderDTOResponse LoadAllProviderByClinicBranchId(string clinicBranchID, string stationIds, string isLoadAll)
        {
            ProviderDTOResponse response = new ProviderDTOResponse();

            List<int> list = new List<int>();
            list.Add(int.Parse(stationIds));

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.ProviderList = scheduler.LoadAllProviderByClinicBranchId(int.Parse(clinicBranchID), list, bool.Parse(isLoadAll));
            }
            catch
            {
                throw;
            }
            return response;
        }

        public StationDTOResponse LoadAllStationsByClinicId(string clinicBranchID)
        {
            StationDTOResponse response = new StationDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.StationsList = scheduler.LoadAllStationsByClinicId(int.Parse(clinicBranchID));
            }
            catch
            {
                throw;
            }
            return response;
        }

        public AppointmentDTOResponse LoadAllAppointmentsByProviderIdAndStationsId(string providerID, string stationId, string isAllProviderSelected, string isAllLocationSelected, string viewTypeId)
        {
            AppointmentDTOResponse response = new AppointmentDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.LocationList = scheduler.LoadAllAppointmentsByProviderIdAndStationsId(int.Parse(providerID), int.Parse(stationId), bool.Parse(isAllProviderSelected), bool.Parse(isAllLocationSelected), int.Parse(viewTypeId));
            }
            catch
            {
                throw;
            }
            return response;
        }

        public PatientSearchDTOResponse SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo)
        {
            PatientSearchDTOResponse response = new PatientSearchDTOResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response = scheduler.SearchPatientbyClinicIdAndSearchString(int.Parse(clinicBranchId), int.Parse(providerId), int.Parse(userTypeId), searchString, int.Parse(demandPageNo));
            }
            catch
            {
                throw;
            }
            return response;
        }

        public PatientCompleteProfileResponse LoadPatientCompleteInfo(string patientId, string clinicBranchId, string providerUserId, string stationId)
        {
            PatientCompleteProfileResponse response = new PatientCompleteProfileResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.PatientCompleteInfo = scheduler.LoadPatientCompleteInfo(int.Parse(patientId), int.Parse(clinicBranchId), int.Parse(providerUserId), int.Parse(stationId));
            }
            catch
            {
                throw;
            }
            return response;
        }

        public WorkingHourResponse LoadProviderWorkingHours(string providerUserId, string providerId, string stationId, string day)
        {
            WorkingHourResponse response = new WorkingHourResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.WorkingHoursList = scheduler.LoadProviderWorkingHours(int.Parse(providerUserId), int.Parse(providerId), int.Parse(stationId), DateTime.Parse(day));
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ManageAppointment(XmlDocument appointmentXml/*AppointmentDTO dto*/, string providerId, string userTypeId)
        {
            #region TEST CODE

            //string path = @"F:\\MansoorXMLTest\\test" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm") + ".txt";

            //StreamWriter f = new StreamWriter(path);
            //f.Write(appointmentXml);
            //f.Flush();
            //f.Close();

            #endregion
            bool flag = false;

           AppointmentDTO dto = new AppointmentDTO();

            XmlDocument doc = new XmlDocument();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Appointment";
            //xRoot.Namespace = null;
            xRoot.IsNullable = true;


            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentDTO), xRoot);

            string xml = appointmentXml.InnerXml;

            

          
           dto = (AppointmentDTO)serializer.Deserialize(new StringReader(xml));
           
            
            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }

                if (scheduler.ManageAppointment(dto, int.Parse(providerId), int.Parse(userTypeId)) > 0)
                {
                    flag = true;
                }
            }
            catch
            {
                throw;
            }

            return true;
        }

        public AppointmentSearchResponse SearchAppointments(string providerUserId, string providerId, string stationId, string fieldTypeId, string searchText, string pageNo)
        {
            AppointmentSearchResponse response = new AppointmentSearchResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.AppointmentList = scheduler.SearchAppointments(int.Parse(providerUserId), int.Parse(providerId), int.Parse(stationId), int.Parse(fieldTypeId), searchText, int.Parse(pageNo));
            }
            catch
            {
                throw;
            }
            return response;
        }


        public WaitingRoomResponse LoadWaitingRoom(string stationId)
        {
            WaitingRoomResponse response = new WaitingRoomResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.AppointmentsList = scheduler.LoadWaitingRoom(int.Parse(stationId));
            }
            catch
            {
                throw;
            }
            return response;
        }


        public void ManageAllAppointmentTypeByXML(string xml)
        {

        }

        public void ManageAllAppointmentTypeByDTO(AppointmentTypeDTO dto)
        {

        }

        public AppointmentRequestDTOResponse LoadAllAppointmentRequestByProvider(string providerId, string clinicBranchId)
        {
            AppointmentRequestDTOResponse response = null;
            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response = scheduler.LoadAllAppointmentRequestByProvider(int.Parse(providerId),int.Parse(clinicBranchId));
                
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool ManageAppointmentRequest(string requestId, string statusId, string modifiedBy, string modifiedAt, string reqStatus, string clinicBranchId)
        {
            bool response = false;
            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                scheduler.ManageAppointmentRequest(int.Parse(requestId), int.Parse(statusId), int.Parse(modifiedBy), DateTime.Parse(modifiedAt), bool.Parse(reqStatus), int.Parse(clinicBranchId));

            }
            catch
            {
                throw;
            }
            return response;
        }

        public ClinicRoomResponse LoadClinicRoomByStationtId(string stationId)
        {
            ClinicRoomResponse response = null;
            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response = scheduler.LoadClinicRoomByStationtId(stationId);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
