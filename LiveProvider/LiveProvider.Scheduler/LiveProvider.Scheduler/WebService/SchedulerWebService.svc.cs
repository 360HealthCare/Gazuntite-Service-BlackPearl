using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Scheduler.Business.BL;
using LiveProvider.Model.Response;
using Scheduler.Business.BL.Impl;
using LiveProvider.Model.Appointment;

namespace LiveProvider.Scheduler.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebService" in code, svc and config file together.
    public class SchedulerWebService : ISchedulerWebService
    {
        ISchedulerManager scheduler = null;

        public WorkingHourResponse LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day)
        {
            WorkingHourResponse response = new WorkingHourResponse();

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response.WorkingHoursList = scheduler.LoadProviderWorkingHours(providerUserId, providerId, stationId, day);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public int SaveAppointmentRequest(AppointmentRequestDTO appRequest)
        {
            int response = 0;

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response = scheduler.SaveAppointmentRequest(appRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt)
        {
            bool response = false;

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                response = scheduler.DeleteAppointmentRequest(appointmentId, modifiedBy, modifiedAt);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId)
        {
            List<AppointmentRequestDTO> list = null;

            try
            {
                if (scheduler == null)
                {
                    scheduler = new SchedulerManagerImpl();
                }
                list = scheduler.ListAllPatientAppointmentRequests(patientId);
            }
            catch
            {
                throw;
            }
            return list;
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

    }
}
