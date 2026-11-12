using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.SchedulerService;
using Quivus.Utility.Logger;
using QuivusManager.IntegratedServies.ReferenceFactory;
using Quivus.Utility;

namespace QuivusManager.Buisness.SchedulerBL.Impl
{
    public class SchedulerBLImpl : ISchedulerBL
    {
        SchedulerWebServiceClient schedulerClient = null;

        
        public WorkingHourResponse LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day)
        {
            WorkingHourResponse response = null;

            try
            {
                if (schedulerClient == null)
                {
                    schedulerClient = (SchedulerWebServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(SchedulerWebServiceClient));
                }

                QuivusLogger.Info("Dispatching LoadProviderWorkingHours Call TO Schedluler Service");

                response = schedulerClient.LoadProviderWorkingHours( providerUserId, providerId, stationId, day);

                //QuivusLogger.Info("Response Recieved From Schedluler Service LoadProviderWorkingHours::" + PatientProfileInsurance);

                //if (response == null)
                //{
                //    response = new WorkingHourResponse();
                //}

                //if (PatientProfileInsurance != null)
                //{
                //    response.ProfileInsuranceDTO = PatientProfileInsurance;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return response;
        }

        public int SaveAppointmentRequest(AppointmentRequestDTO appRequest)
        {
            int flag = 0;

            try
            {
                if (schedulerClient == null)
                {
                    schedulerClient = (SchedulerWebServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(SchedulerWebServiceClient));
                }

                QuivusLogger.Info("Dispatching SaveAppointmentRequest Call TO Schedluler Service");

                flag = schedulerClient.SaveAppointmentRequest(appRequest);

                //QuivusLogger.Info("Response Recieved From Schedluler Service LoadProviderWorkingHours::" + PatientProfileInsurance);

                //if (response == null)
                //{
                //    response = new WorkingHourResponse();
                //}

                //if (PatientProfileInsurance != null)
                //{
                //    response.ProfileInsuranceDTO = PatientProfileInsurance;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return flag;
        }

        public bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt)
        {
            bool response = false;

            try
            {
                if (schedulerClient == null)
                {
                    schedulerClient = (SchedulerWebServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(SchedulerWebServiceClient));
                }

                QuivusLogger.Info("Dispatching DeleteAppointmentRequest Call TO Schedluler Service");

                response = schedulerClient.DeleteAppointmentRequest(appointmentId, modifiedBy, modifiedAt);

                //QuivusLogger.Info("Response Recieved From Schedluler Service LoadProviderWorkingHours::" + PatientProfileInsurance);

                //if (response == null)
                //{
                //    response = new WorkingHourResponse();
                //}

                //if (PatientProfileInsurance != null)
                //{
                //    response.ProfileInsuranceDTO = PatientProfileInsurance;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
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
                if (schedulerClient == null)
                {
                    schedulerClient = (SchedulerWebServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(SchedulerWebServiceClient));
                }

                QuivusLogger.Info("Dispatching ListAllPatientAppointmentRequests Call TO Schedluler Service");

                list = schedulerClient.ListAllPatientAppointmentRequests(patientId);

                //QuivusLogger.Info("Response Recieved From Schedluler Service LoadProviderWorkingHours::" + PatientProfileInsurance);

                //if (response == null)
                //{
                //    response = new WorkingHourResponse();
                //}

                //if (PatientProfileInsurance != null)
                //{
                //    response.ProfileInsuranceDTO = PatientProfileInsurance;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return list;
        }

        public AppointmentRequestStatusTypeDTOResponse GetAllAppointmentRequestStatusTypes()
        {
            AppointmentRequestStatusTypeDTOResponse response = null;

            try
            {
                if (schedulerClient == null)
                {
                    schedulerClient = (SchedulerWebServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(SchedulerWebServiceClient));
                }

                QuivusLogger.Info("Dispatching GetAllAppointmentRequestStatusTypes Call TO Schedluler Service");

                response = schedulerClient.GetAllAppointmentRequestStatusTypes();

                //QuivusLogger.Info("Response Recieved From Schedluler Service LoadProviderWorkingHours::" + PatientProfileInsurance);

                //if (response == null)
                //{
                //    response = new WorkingHourResponse();
                //}

                //if (PatientProfileInsurance != null)
                //{
                //    response.ProfileInsuranceDTO = PatientProfileInsurance;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return response;
        }

        public AppointmentCurrentStatusTypeDTOResponse GetAllAppointmentCurrentStatusTypeDTOs()
        {
            AppointmentCurrentStatusTypeDTOResponse response = null;

            try
            {
                if (schedulerClient == null)
                {
                    schedulerClient = (SchedulerWebServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(SchedulerWebServiceClient));
                }

                QuivusLogger.Info("Dispatching GetAllAppointmentCurrentStatusTypeDTOs Call TO Schedluler Service");

                response = schedulerClient.GetAllAppointmentCurrentStatusTypeDTOs();

                //QuivusLogger.Info("Response Recieved From Schedluler Service LoadProviderWorkingHours::" + PatientProfileInsurance);

                //if (response == null)
                //{
                //    response = new WorkingHourResponse();
                //}

                //if (PatientProfileInsurance != null)
                //{
                //    response.ProfileInsuranceDTO = PatientProfileInsurance;
                //    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                //}
                //else
                //{
                //    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                //}
            }
            catch
            {
                throw;
            }
            return response;
        }
    
    }
}
