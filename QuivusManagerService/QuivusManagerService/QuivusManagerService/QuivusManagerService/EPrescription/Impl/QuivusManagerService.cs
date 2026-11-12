using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using QuivusManager.Buisness.UserBL;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.Buisness.UserBL.Impl;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using System.ServiceModel;


using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.PatientBL;

using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription;
using QuivusManager.Buisness.E_PrescriptionBL;
using QuivusManager.Buisness.E_PrescriptionBL.Impl;
using QuivusManager.IntegratedServies.EprescriptionService;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {

        IEPrescriptionBL EPrescriptionBL = null;
        public ANSWebURLResponse GetANSUrl(string PracticeName, UserSessionDTO userSession)
        {
            ANSWebURLResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetANSUrl Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                       EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.GetANSUrl(PracticeName);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ANSWebURLResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ANSWebURLResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        public LoginCallResponse PrepareLoginCallForEprescription(string RCOPIAUserExternalID,int PatientId, UserSessionDTO userSession)
        {
            LoginCallResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetANSUrl Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.prepareLoginCallForEprescription(RCOPIAUserExternalID,PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LoginCallResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LoginCallResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 april 2011
        /// Method Will be used to load url for edit allergies
        /// </summary>
        /// <param name="rcopiaIDForUser"></param>
        /// <param name="rcopiaIDForPatient"></param>
        /// <param name="practiceName"></param>
        /// <param name="practiceUserName"></param>
        /// <returns></returns>
        public UrlResponse GetUrlForEditAllergies(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName, String practiceUserName, UserSessionDTO userSession)
        {
            UrlResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetUrlForEditAllergies Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.GetUrlForEditAllergies( rcopiaIDForUser, rcopiaIDForPatient, practiceName, practiceUserName);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new UrlResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new UrlResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// Method Used To Load prescription url
        /// </summary>
        /// <param name="rcopiaIDForUser"></param>
        /// <param name="rcopiaIDForPatient"></param>
        /// <param name="practiceName"></param>
        /// <param name="practiceUserName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public UrlResponse GetUrlForEditPrescription(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
        String practiceUserName, UserSessionDTO userSession)
        {
            UrlResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetUrlForEditAllergies Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.GetUrlForEditPrescription(rcopiaIDForUser, rcopiaIDForPatient, practiceName, practiceUserName);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new UrlResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new UrlResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

      /// <summary>
      /// Author Ismail
      /// Date 30 April 2011
      /// Method will be used to load medication url from e-prescrption service :)
      /// </summary>
      /// <param name="rcopiaIDForUser"></param>
      /// <param name="rcopiaIDForPatient"></param>
      /// <param name="practiceName"></param>
      /// <param name="practiceUserName"></param>
      /// <param name="userSession"></param>
      /// <returns></returns>
        public UrlResponse GetUrlForEditMedication(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
        String practiceUserName, UserSessionDTO userSession)
        {
            UrlResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetUrlForEditMedication Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.GetUrlForEditMedication(rcopiaIDForUser, rcopiaIDForPatient, practiceName, practiceUserName);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new UrlResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new UrlResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 2 May 2011
        /// Method Upload Patient To Dr First Return RCopiaId
        /// </summary>
        /// <param name="patientInfoDTO"></param>
        /// <returns></returns>
        public String UploadPatient(PatientInfoDTO patientInfoDTO)
        {
            String response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService UploadPatient Called");


                if (EPrescriptionBL == null)
                {
                    EPrescriptionBL = new EPrescriptionBLImpl();
                }
                response = EPrescriptionBL.UploadPatient(patientInfoDTO);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }

        public ServiceResponse SyncEPrescription(int patientID, string rcopiaID, int clinicBranchID,int VisitId, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SyncEPrescription Called");
                QuivusLogger.Info("SyncEPrescription PARAM patientID::" + patientID + "clinicBranchId::" + clinicBranchID + "rcopiaID::" + rcopiaID);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.SyncEPrescription(patientID, rcopiaID, clinicBranchID, VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        public NotificationResponse GetNotificationsForProvider(string ExternalId, UserSessionDTO userSession)
        {
            NotificationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SyncEPrescription Called");
                QuivusLogger.Info("SyncEPrescription PARAM ExternalId::" + ExternalId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (EPrescriptionBL == null)
                    {
                        EPrescriptionBL = new EPrescriptionBLImpl();
                    }
                    response = EPrescriptionBL.GetNotificationsForProvider(ExternalId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new NotificationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new NotificationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new NotificationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
    }
}