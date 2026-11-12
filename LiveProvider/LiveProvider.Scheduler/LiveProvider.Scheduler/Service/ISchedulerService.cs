using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LiveProvider.Model.Response;
using LiveProvider.Model.TypeDTOs.Appointment;
using LiveProvider.Model.Appointment;
using System.Xml;
using LiveProvider.Model.Appointment.ClinicRoom;
using LiveProvider.Model.Response.ClinicRoom;


namespace SchedulerService.Service
{    
    [ServiceContract]
    [XmlSerializerFormat]
    public interface ISchedulerService
    {

        [OperationContract]
        [WebGet(UriTemplate = "GetAllAppointmentTypes", BodyStyle = WebMessageBodyStyle.Bare)]        
        AppointmentTypeDTOResponse GetAllAppointmentTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetAllAppointmentRequestStatusTypes", BodyStyle = WebMessageBodyStyle.Bare)]
        AppointmentRequestStatusTypeDTOResponse GetAllAppointmentRequestStatusTypes();

        [OperationContract]
        [WebGet(UriTemplate = "GetAllAppointmentCurrentStatusTypes", BodyStyle = WebMessageBodyStyle.Bare)]
        AppointmentCurrentStatusTypeDTOResponse GetAllAppointmentCurrentStatusTypeDTOs();

        [OperationContract]
        [WebGet(UriTemplate = "GetAllSearchFieldTypes", BodyStyle = WebMessageBodyStyle.Bare)]
        SearchFieldTypeDTOResponse GetAllSearchFieldTypeDTOs();

        [OperationContract]
        [WebGet(UriTemplate = "LoadAllProviderByClinicBranchId/{clinicBranchID}/{stationId}/{isLoadAll}", BodyStyle = WebMessageBodyStyle.Bare)]
        ProviderDTOResponse LoadAllProviderByClinicBranchId(string clinicBranchID, string stationId, string isLoadAll);

        [OperationContract]
        [WebGet(UriTemplate = "LoadAllStationsByClinicId/{clinicBranchID}", BodyStyle = WebMessageBodyStyle.Bare)]
        StationDTOResponse LoadAllStationsByClinicId(string clinicBranchID);

        [OperationContract]
        [WebGet(UriTemplate = "LoadAllAppointmentsByProviderIdAndStationsId/{providerUserID}/{stationId}/{isAllProviderSelected}/{isAllLocationSelected}/{viewTypeId}", BodyStyle = WebMessageBodyStyle.Bare)]
        AppointmentDTOResponse LoadAllAppointmentsByProviderIdAndStationsId(string providerUserID, string stationId, string isAllProviderSelected, string isAllLocationSelected, string viewTypeId);

        [OperationContract]
        [WebGet(UriTemplate = "SearchPatientbyClinicIdAndSearchString/{clinicBranchId}/{providerId}/{userTypeId}/{searchString}/{demandPageNo}", BodyStyle = WebMessageBodyStyle.Bare)]
        PatientSearchDTOResponse SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo);

        [OperationContract]
        [WebGet(UriTemplate = "LoadPatientCompleteInfo/{patientId}/{clinicBranchId}/{providerUserId}/{stationId}", BodyStyle = WebMessageBodyStyle.Bare)]
        PatientCompleteProfileResponse LoadPatientCompleteInfo(string patientId, string clinicBranchId, string providerUserId, string stationId);

        [OperationContract]
        [WebGet(UriTemplate = "LoadProviderWorkingHours/{providerUserId}/{providerId}/{stationId}/{day}", BodyStyle = WebMessageBodyStyle.Bare)]
        WorkingHourResponse LoadProviderWorkingHours(string providerUserId, string providerId, string stationId, string day);

        [OperationContract]
        [WebGet(UriTemplate = "SearchAppointments/{providerUserId}/{providerId}/{stationId}/{fieldTypeId}/{searchText}/{pageNo}", BodyStyle = WebMessageBodyStyle.Bare)]
        AppointmentSearchResponse SearchAppointments(string providerUserId, string providerId, string stationId, string fieldTypeId, string searchText, string pageNo);

        [OperationContract]
        [WebGet(UriTemplate = "LoadWaitingRoom/{stationId}", BodyStyle = WebMessageBodyStyle.Bare)]
        WaitingRoomResponse LoadWaitingRoom(string stationId);

        [OperationContract]
        [WebGet(UriTemplate = "ManageAppointmentRequest/{requestId}/{statusId}/{modifiedBy}/{modifiedAt}/{reqStatus}/{clinicBranchId}", BodyStyle = WebMessageBodyStyle.Bare)]
        bool ManageAppointmentRequest(string requestId, string statusId, string modifiedBy, string modifiedAt, string reqStatus, string clinicBranchId);

        [OperationContract]
        [WebGet(UriTemplate = "LoadAllAppointmentRequestByProvider/{providerId}/{clinicBranchId}", BodyStyle = WebMessageBodyStyle.Bare)]
        AppointmentRequestDTOResponse LoadAllAppointmentRequestByProvider(string providerId, string clinicBranchId);

        [OperationContract]
        [WebInvoke(Method="POST",BodyStyle=WebMessageBodyStyle.Bare,
        UriTemplate="ManageAppointment/{providerId}/{userTypeId}")]
        bool ManageAppointment(XmlDocument appointmentXml/*AppointmentDTO dto*/, string providerId, string userTypeId);

       

        // this is for test
        //[OperationContract]
        //[WebGet(UriTemplate = "ManageAllAppointmentTypeByXML/{xml}", BodyStyle = WebMessageBodyStyle.Bare)]
        //void ManageAllAppointmentTypeByXML(string xml);

        //// this is for test
        //[OperationContract]
        //[WebGet(UriTemplate = "ManageAllAppointmentTypeByDTO/{dto}", BodyStyle = WebMessageBodyStyle.Bare)]
        //void ManageAllAppointmentTypeByDTO(AppointmentTypeDTO dto);

        [OperationContract]
        [WebGet(UriTemplate = "LoadClinicRoomByStationtId/{stationId}", BodyStyle = WebMessageBodyStyle.Bare)]
        ClinicRoomResponse LoadClinicRoomByStationtId(string stationId);

        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        bool PingService();
    }
}
