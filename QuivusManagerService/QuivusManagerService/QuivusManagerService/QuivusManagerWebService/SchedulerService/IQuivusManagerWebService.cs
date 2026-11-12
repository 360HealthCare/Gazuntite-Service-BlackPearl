using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.SchedulerService;
using System.ServiceModel;

namespace QuivusManagerWebService
{
    public partial interface IQuivusManagerWebService
    {

        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        WorkingHourResponse LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day);


        [OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        int SaveAppointmentRequest(AppointmentRequestDTO appRequest);

        [OperationContract]
        bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt);


        [OperationContract]
        List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId);


        [OperationContract]
        List<AppointmentRequestStatusTypeDTO> GetAllAppointmentRequestStatusTypes();

        [OperationContract]
        List<AppointmentCurrentStatusTypeDTO> GetAllAppointmentCurrentStatusTypeDTOs();
      
    }
}
