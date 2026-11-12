using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveProvider.Model.Response;
using LiveProvider.Model.Appointment.Provider;
using LiveProvider.Model.Appointment;

namespace LiveProvider.Scheduler.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebService" in both code and config file together.
    [ServiceContract]
    public interface ISchedulerWebService
    {
        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011        
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
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
        AppointmentRequestStatusTypeDTOResponse GetAllAppointmentRequestStatusTypes();

        [OperationContract]        
        AppointmentCurrentStatusTypeDTOResponse GetAllAppointmentCurrentStatusTypeDTOs();

    }
}
