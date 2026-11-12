using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Scheduler.Business.BL;
using LiveProvider.Model.Response;
using Scheduler.Business.BL.Impl;

namespace LiveProvider.Scheduler.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebService" in code, svc and config file together.
    public class WebService : IWebService
    {
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
    }
}
