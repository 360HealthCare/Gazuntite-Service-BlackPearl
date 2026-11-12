using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.Appointment;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Model.Appointment;
using LiveProvider.Model.Common;
using LiveProvider.Model.Response;
using LiveProvider.Model.Appointment.Provider;
using LiveProvider.Model.Response.ClinicRoom;

namespace Scheduler.Business.BL
{
    public interface ISchedulerManager
    {
        List<AppointmentCurrentStatusTypeDTO> ListAllAppointmentCurrentStatusTypeDTOs();

        List<AppointmentRequestStatusTypeDTO> ListAllAppointmentRequestStatusTypeDTOs();

        List<AppointmentTypeDTO> ListAllAppointmentTypeDTOs();

        List<SearchFieldTypeDTO> ListAllSearchFieldTypeDTOs();

        List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID, List<int> stationIds, bool isLoadAll);    
        
        List<StationDTO> LoadAllStationsByClinicId(int clinicBranchID);

        List<LocationDTO> LoadAllAppointmentsByProviderIdAndStationsId(int providerID, int stationId, bool isAllProviderSelected, bool isAllLocationSelected, int viewTypeId);

        int ManageAppointment(AppointmentDTO appointment, int providerId, int userTypeId);

        PatientSearchDTOResponse SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo);

        PatientCompleteInfoDTO LoadPatientCompleteInfo(int patientId, int clinicBranchId, int providerUserId, int stationId);

        List<WorkingHourDTO> LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day);

        List<AppointmentDTO> SearchAppointments(int providerUserId, int providerId, int stationId, int fieldTypeId, string searchText, int pageNo);

        List<AppointmentDTO> LoadWaitingRoom(int stationId);


        AppointmentRequestDTOResponse LoadAllAppointmentRequestByProvider(int providerId, int clinicBranchId);


        int SaveAppointmentRequest(AppointmentRequestDTO appRequest);

        bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt);

        List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId);

        bool ManageAppointmentRequest(int requestId, int statusId, int modifiedBy, DateTime modifiedAt, bool reqStatus, int clinicBranchId);

        ClinicRoomResponse LoadClinicRoomByStationtId(string stationId);

    }
}
