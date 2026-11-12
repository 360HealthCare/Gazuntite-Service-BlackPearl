using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.IntegratedServies.SchedulerService;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.SchedulerBL.Impl;
using QuivusManager.Buisness.SchedulerBL;

namespace QuivusManagerWebService
{
    public partial class QuivusManagerWebService
    {
        ISchedulerBL SchedulerBL = null;
  
        public WorkingHourResponse LoadProviderWorkingHours(int providerUserId, int providerId, int stationId, DateTime day)
        {
            WorkingHourResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadProviderWorkingHours Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (SchedulerBL == null)
                    {
                        SchedulerBL = new SchedulerBLImpl();
                    }
                    response = SchedulerBL.LoadProviderWorkingHours(providerUserId, providerId, stationId, day);

                    //if (response != null && response.ResponseData != null)
                    //{
                    //    QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                    //    QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    //}
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new RegistrationStaticDataResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        public int SaveAppointmentRequest(AppointmentRequestDTO appRequest)
        {
            int response = 0;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SaveAppointmentRequest Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (SchedulerBL == null)
                    {
                        SchedulerBL = new SchedulerBLImpl();
                    }
                    response = SchedulerBL.SaveAppointmentRequest(appRequest);

                    //if (response != null && response.ResponseData != null)
                    //{
                    //    QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                    //    QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    //}
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new RegistrationStaticDataResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        public bool DeleteAppointmentRequest(int appointmentId, int modifiedBy, DateTime modifiedAt)
        {
            bool flag = false;
            try
            {
                QuivusLogger.Info("In QuivusManagerService DeleteAppointmentRequest Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (SchedulerBL == null)
                    {
                        SchedulerBL = new SchedulerBLImpl();
                    }
                    flag = SchedulerBL.DeleteAppointmentRequest(appointmentId, modifiedBy, modifiedAt);

                    //if (response != null && response.ResponseData != null)
                    //{
                    //    QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                    //    QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    //}
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new RegistrationStaticDataResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return flag;
        }

        public List<AppointmentRequestDTO> ListAllPatientAppointmentRequests(int patientId)
        {
            List<AppointmentRequestDTO> list = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ListAllPatientAppointmentRequests Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (SchedulerBL == null)
                    {
                        SchedulerBL = new SchedulerBLImpl();
                    }
                    list = SchedulerBL.ListAllPatientAppointmentRequests(patientId);

                    //if (response != null && response.ResponseData != null)
                    //{
                    //    QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                    //    QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    //}
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new RegistrationStaticDataResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return list;
        }

        public List<AppointmentRequestStatusTypeDTO> GetAllAppointmentRequestStatusTypes()
        {   
            AppointmentRequestStatusTypeDTOResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetAllAppointmentRequestStatusTypes Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (SchedulerBL == null)
                    {
                        SchedulerBL = new SchedulerBLImpl();
                    }
                    response = SchedulerBL.GetAllAppointmentRequestStatusTypes();

                    //if (response != null && response.ResponseData != null)
                    //{
                    //    QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                    //    QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    //}
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new RegistrationStaticDataResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response.RequestStatusTypeList;
        }

        public List<AppointmentCurrentStatusTypeDTO> GetAllAppointmentCurrentStatusTypeDTOs()
        {
            AppointmentCurrentStatusTypeDTOResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetAllAppointmentCurrentStatusTypeDTOs Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (SchedulerBL == null)
                    {
                        SchedulerBL = new SchedulerBLImpl();
                    }
                    response = SchedulerBL.GetAllAppointmentCurrentStatusTypeDTOs();

                    //if (response != null && response.ResponseData != null)
                    //{
                    //    QApplicationDataCache.RELATIONSHIP_INFO_CACHE_DATA = response.ResponseData.RelationshipTypes;
                    //    QApplicationDataCache.GENDER_INFO_CACHE_DATA = response.ResponseData.GenderTypes;
                    //}
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new RegistrationStaticDataResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new RegistrationStaticDataResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response.AppointmentCurrentSatatusList;
        }

    }
}