using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.SchedulerService;

namespace QuivusManager.Buisness.SchedulerBL
{
    public interface ISchedulerBL
    {
        WorkingHourResponse LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day);

        int SaveAppointmentRequest(AppointmentRequestDTO appRequest);

        bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt);

        List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId);
        
        AppointmentRequestStatusTypeDTOResponse GetAllAppointmentRequestStatusTypes();
        
        AppointmentCurrentStatusTypeDTOResponse GetAllAppointmentCurrentStatusTypeDTOs();
    }
}
