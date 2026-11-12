using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;

using QuivusManager.Buisness.VisitBL;

using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using Quivus.Utility.Fault;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.Buisness.VisitBL.ViewBackgorund.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleDictation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleNotes;




namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <returns></returns>        
        public PatientEncountersResponse LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, UserSessionDTO userSession)
        {
            PatientEncountersResponse response = null;
            //changes by baber 
            #region initialzing block 
            int userId=0;
            int userTypeId=0; 
            #endregion
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientVisitsByPatientIdAndClinicId Called");
                QuivusLogger.Info("LoadPatientVisitsByPatientIdAndClinicId PARAM patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "showAll::" + showAll);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    // Changes by baber  //Commented By Ismail
                    //userId = userSession.UserID;
                    //userTypeId = userSession.UserTypeID;
                    // chages by baber end
                    UserSessionDTO obj = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (obj != null)
                    {
                        userId = obj.UserID;
                        userTypeId = obj.UserTypeID;
                    }
                    response = viewBackImpl.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll, userId, userTypeId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientEncountersResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientEncountersResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientEncountersResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public ImmunizationAutoCompleteResponse LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationLotNumberByClinicId Called");
                QuivusLogger.Info("LoadImmunizationLotNumberByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationLotNumberByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization dose
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationDoseByClinicId Called");
                QuivusLogger.Info("LoadImmunizationDoseByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationDoseByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization Reason
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationRefusalReasonByClinicId Called");
                QuivusLogger.Info("LoadImmunizationRefusalReasonByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationRefusalReasonByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Site
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>           
        public ImmunizationAutoCompleteResponse LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationSiteByClinicId Called");
                QuivusLogger.Info("LoadImmunizationSiteByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationSiteByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization Series
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>                    
        public ImmunizationAutoCompleteResponse LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationSeriesByClinicId Called");
                QuivusLogger.Info("LoadImmunizationSeriesByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationSeriesByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit        
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public PatientImmunizationResponse LoadImmunizationsByVisitId(int visitId, int providerUserId, int userTypeId, UserSessionDTO userSession)
        {
            PatientImmunizationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationsByVisitId Called");
                QuivusLogger.Info("LoadImmunizationsByVisitId PARAM visitId::" + visitId + "providerUserId::" + providerUserId + "userTypeId::" + userTypeId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationsByVisitId(visitId, providerUserId, userTypeId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientImmunizationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientImmunizationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>        
        public VisitNotesResponse LoadModuleNotesByVisitId(int visitId, UserSessionDTO userSession)
        {
            VisitNotesResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadModuleNotesByVisitId Called");
                QuivusLogger.Info("LoadModuleNotesByVisitId PARAM visitId::" + visitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadModuleNotesByVisitId(visitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new VisitNotesResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new VisitNotesResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationManufacturerByClinicId Called");
                QuivusLogger.Info("LoadImmunizationManufacturerByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationManufacturerByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public ImmunizationAutoCompleteResponse LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId, UserSessionDTO userSession)
        {
            ImmunizationAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadImmunizationRoutByClinicId Called");
                QuivusLogger.Info("LoadImmunizationRoutByClinicId PARAM searchCriteria::" + searchCriteria + "clinicBranchId::" + clinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadImmunizationRoutByClinicId(searchCriteria, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>        
        public PatientImmunizationResponse LoadPatientImmunizationsByPatientId(int patientId, int providerUserId, int userTypeId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientImmunizationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientImmunizationsByPatientId Called");
                QuivusLogger.Info("LoadPatientImmunizationsByPatientId PARAM patientId::" + patientId + "providerUserId" + providerUserId + "userTypeId::" + userTypeId + "clinicBranchID::" + clinicBranchID );

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientImmunizationsByPatientId(patientId, providerUserId, userTypeId,clinicBranchID, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientImmunizationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientImmunizationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Medical History by visit Id
        /// </summary>
        /// <returns></returns>
        public PatientMedicalHistoryResponse LoadPatientMedicalHistoryByVisit(int VisitId, UserSessionDTO userSession)
        {
            PatientMedicalHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientMedicalHistoryByVisit Called");
                QuivusLogger.Info("LoadPatientMedicalHistoryByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientMedicalHistoryByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientMedicalHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientMedicalHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;

        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Family History by visit Id
        /// </summary>
        /// <returns></returns>

        public PatientFamilyHistoryResponse LoadPatientFamilyHistoryByVisit(int VisitId, UserSessionDTO userSession)
        {
            PatientFamilyHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientFamilyHistoryByVisit Called");
                QuivusLogger.Info("LoadPatientFamilyHistoryByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientFamilyHistoryByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFamilyHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFamilyHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Vital History by visit Id
        /// </summary>

        /// <returns></returns>

        public PatientVitalResponse LoadPatientVitalByVisit(int VisitId, UserSessionDTO userSession)
        {
            PatientVitalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientFamilyHistoryByVisit Called");
                QuivusLogger.Info("LoadPatientFamilyHistoryByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientVitalByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;

        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public PatientLabResponse LoadPatientLabsByVisit(int VisitId, UserSessionDTO userSession)
        {
            PatientLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientLabsByVisit Called");
                QuivusLogger.Info("LoadPatientLabsByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientLabsByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientLabResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load Patient Previous 10 Vitals History 
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse LoadPatientPreviousVitals(int PatientId, UserSessionDTO userSession)
        {
            PatientVitalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientPreviousVitals Called");
                QuivusLogger.Info("LoadPatientPreviousVitals PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientPreviousVitals(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by Visit 
        /// </summary>
        /// <returns></returns>      
        public PatientHPIResponse LoadPatientHPIByVisit(int visitId, UserSessionDTO userSession)
        {
            PatientHPIResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientHPIByVisit Called");
                QuivusLogger.Info("LoadPatientHPIByVisit PARAM visitId::" + visitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientHPIByVisit(visitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientHPIResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientHPIResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social history Addiction By social History Id
        /// </summary>
        /// <returns></returns>
        public SocialHistoryAddictionResponse LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId, UserSessionDTO userSession)
        {
            SocialHistoryAddictionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSocialHistoryAddictionBySocialHistoryID Called");
                QuivusLogger.Info("LoadSocialHistoryAddictionBySocialHistoryID PARAM PatientSocialHistoryId::" + PatientSocialHistoryId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadSocialHistoryAddictionBySocialHistoryID(PatientSocialHistoryId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryAddictionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryAddictionResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social history by visit Id
        /// </summary>
        /// <returns></returns>
        public SocialHistoryResponse LoadSocialHistoryByVisitId(int VisitId, UserSessionDTO userSession)
        {
            SocialHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSocialHistoryByVisitId Called");

                QuivusLogger.Info("LoadSocialHistoryByVisitId PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadSocialHistoryByVisitId(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>  
        public PatientRecommendationResponse LoadPatientRecommendationByVisit(int visitId, UserSessionDTO userSession)
        {
            PatientRecommendationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientRecommendationByVisit Called");
                QuivusLogger.Info("LoadPatientRecommendationByVisit PARAM visitId::" + visitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientRecommendationByVisit(visitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientRecommendationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientRecommendationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 22 March 2011
        /// This Method Will be used to load Patient physical History by visit Id
        /// </summary>
        /// <returns></returns>
        public PatientPhysicalResponse LoadPatientPhysicalsByVisit(int VisitId, UserSessionDTO userSession)
        {
            PatientPhysicalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientPhysicalsByVisit Called");
                QuivusLogger.Info("LoadPatientPhysicalsByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientPhysicalsByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientPhysicalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientPhysicalResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>           
        public PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int visitId, UserSessionDTO userSession)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientDiagnosisByVisitId Called");
                QuivusLogger.Info("LoadPatientDiagnosisByVisitId PARAM VisitId::" + visitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientDiagnosisByVisitId(visitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// Author Ismail
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        //public PatientDiagnosisResponse LoadPatientDiagnosisByVisitId(int VisitId, UserSessionDTO userSession)
        //{
        //    PatientDiagnosisResponse response = null;
        //    try
        //    {
        //        QuivusLogger.Info("In QuivusManagerService LoadPatientDiagnosisByVisitId Called");
        //        QuivusLogger.Info("LoadPatientDiagnosisByVisitId PARAM VisitId::" + VisitId);

        //        if (ApplicationSessionHandler.IsValidUserSession(userSession))
        //        {
        //            if (viewBackImpl == null)
        //            {
        //                viewBackImpl = new ViewBackgroundBLImpl();
        //            }
        //            response = viewBackImpl.LoadPatientDiagnosisByVisitId(VisitId);
        //        }
        //    }
        //    catch (InvalidSessionException exp)
        //    {
        //        QuivusLogger.Error(exp);
        //        if (response == null)
        //            response = new PatientDiagnosisResponse();

        //        ResponseErrorHandler.HandleException(response, exp, exp.Code);
        //    }
        //    catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
        //    {
        //        QuivusLogger.Error(exp);

        //        if (response == null)
        //            response = new PatientDiagnosisResponse();

        //        ResponseErrorHandler.HandleException(response, exp);
        //    }
        //    return response;
        //}

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>

        public PatientROSResponse LoadPatientROSByVisit(int VisitId, UserSessionDTO userSession)
        {
            PatientROSResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientROSByVisit Called");
                QuivusLogger.Info("LoadPatientROSByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientROSByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientROSResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientROSResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Quick Input by visit Id
        /// </summary>
        /// <returns></returns>
        public QuickInputResponse LoadQuickInputByVisit(int VisitId, UserSessionDTO userSession)
        {
            QuickInputResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadQuickInputByVisit Called");
                QuivusLogger.Info("LoadQuickInputByVisit PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadQuickInputByVisit(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new QuickInputResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new QuickInputResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow UP by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>           
        public PatientFollowUpResponse LoadPatientFollowUpByVisitId(int visitId, UserSessionDTO userSession)
        {
            PatientFollowUpResponse response = null;
           
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientFollowUpByVisitId Called");
                QuivusLogger.Info("LoadPatientFollowUpByVisitId PARAM VisitId::" + visitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }

                    
                    response = viewBackImpl.LoadPatientFollowUpByVisitId(visitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFollowUpResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFollowUpResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 28 March 2011
        /// This Method Will be used to load visit stake holder by visit by Id
        /// </summary>
        /// <returns></returns>
        public VisistStakeHolderResponse LoadVisitStakeHolderByVisitId(int VisitId, UserSessionDTO userSession)
        {
            VisistStakeHolderResponse response = null;
            int UserTypeId = 0;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadVisitStakeHolderByVisitId Called");
                QuivusLogger.Info("LoadVisitStakeHolderByVisitId PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }

                    UserSessionDTO CurrentSession = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (CurrentSession != null)
                        UserTypeId = CurrentSession.UserTypeID;

                    response = viewBackImpl.LoadVisitStakeHolderByVisitId(VisitId, UserTypeId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new VisistStakeHolderResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new VisistStakeHolderResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient medication prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>

        public MedicationPrescribedResponse LoadPatientMedicationPrescribedByPatient(int PatientId, UserSessionDTO userSession)
        {
            MedicationPrescribedResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientMedicationPrescribedByPatient Called");
                QuivusLogger.Info("LoadPatientMedicationPrescribedByPatient PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientMedicationPrescribedByPatient(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new MedicationPrescribedResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new MedicationPrescribedResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        public AllergyResponse LoadPatientAllergyByPatientId(int PatientId, UserSessionDTO userSession)
        {
            int clinicId = 0;
            AllergyResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientAllergyByPatientId Called");
                QuivusLogger.Info("LoadPatientAllergyByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    UserSessionDTO obj = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (obj != null)
                    {
                        clinicId = obj.ClinicBranchID;
                    }
                    response = viewBackImpl.LoadPatientAllergyByPatientId(PatientId, clinicId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new AllergyResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new AllergyResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public PatientVisitResponse LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId, UserSessionDTO userSession)
        {
            PatientVisitResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientVisitByClinicBranchId Called");
                QuivusLogger.Info("LoadPatientVisitByClinicBranchId PARAM VisitId::" + VisitId + "ClinicBranchId" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientVisitByClinicBranchId(VisitId, ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientVisitResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientVisitResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse LoadPatientVitals(int PatientId, int ClinicBranchId, UserSessionDTO userSession)
        {
            PatientVitalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientVisitByClinicBranchId Called");
                QuivusLogger.Info("LoadPatientVisitByClinicBranchId PARAM PatientId::" + PatientId + "ClinicBranchId" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientVitals(PatientId, ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient presription by visit by Id
        /// </summary>
        /// <returns></returns>
        public PatientPrescriptionResponse LoadPateintPrescriptionByVisitId(int VisitId, UserSessionDTO userSession)
        {
            PatientPrescriptionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPateintPrescriptionByVisitId Called");
                QuivusLogger.Info("LoadPateintPrescriptionByVisitId PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPateintPrescriptionByVisitId(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientPrescriptionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientPrescriptionResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId, UserSessionDTO userSession)
        {
            PatientPrescriptionMedicationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPrescriptionMedicationByPatientPrescriptionId Called");
                QuivusLogger.Info("LoadPrescriptionMedicationByPatientPrescriptionId PARAM PatientPrescriptionId::" + PatientPrescriptionId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPrescriptionMedicationByPatientPrescriptionId(PatientPrescriptionId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientPrescriptionMedicationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientPrescriptionMedicationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public PatientMedicationReponse LoadPatientMedicationByPatientId(int PatientId, UserSessionDTO userSession)
        {
            PatientMedicationReponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientMedicationByPatientId Called");
                QuivusLogger.Info("LoadPatientMedicationByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientMedicationByPatientId(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientMedicationReponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientMedicationReponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>

        public PatientDiagnosisResponse LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientDiagnosisByPatientId(PatientId, ClinicBranchId,stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Sami
        /// Date 8/12/2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>

        public PatientDiagnosisResponse LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId, UserSessionDTO userSession)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientDiagnosisByPatientIdANDForRecommendation(PatientId, ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>

        public PatientFollowupInstructionResponse LoadPatientFollowUpInstructionByVisitId(int visitId, UserSessionDTO userSession)
        {
            PatientFollowupInstructionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientFollowUpInstructionByVisitId Called");
                QuivusLogger.Info("LoadPatientDiagnosisByPatientId PARAM visitId::" + visitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientFollowUpInstructionByVisitId(visitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFollowupInstructionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFollowupInstructionResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>

        public PatientPrescriptionMedicationResponse LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicalBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientPrescriptionMedicationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPrescriptionMedicationByPatientId Called");
                QuivusLogger.Info("LoadPrescriptionMedicationByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPrescriptionMedicationByPatientId(PatientId, ClinicalBranchId, stationIDs,  isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientPrescriptionMedicationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientPrescriptionMedicationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Atta
        /// Date 20 April 2011
        /// </summary>
        /// <returns></returns>

        public FollowupLabsResponse LoadPatientLabTestsForFollowupByPatient(int PatientId, UserSessionDTO userSession)
        {
            FollowupLabsResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientLabTestsForFollowupByPatient Called");
                QuivusLogger.Info("LoadPatientLabTestsForFollowupByPatient PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientLabTestsForFollowupByPatient(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new FollowupLabsResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new FollowupLabsResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by visit Id
        /// </summary>
        /// <returns></returns>
        public PatientInHouseLabResponse LoadPateintInHouseLabsByVisitId(int VisitId, UserSessionDTO userSession)
        {
            PatientInHouseLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPateintInHouseLabsByVisitId Called");
                QuivusLogger.Info("LoadPateintInHouseLabsByVisitId PARAM VisitId::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPateintInHouseLabsByVisitId(VisitId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientInHouseLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientInHouseLabResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientInHouseLabResponse LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientInHouseLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPateintInHouseLabsByVisitId Called");
                QuivusLogger.Info("LoadPateintInHouseLabsByVisitId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPateintInHouseLabsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientInHouseLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientInHouseLabResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient vital by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse LoadVitalsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientVitalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadVitalsByPatientId Called");
                QuivusLogger.Info("LoadVitalsByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadVitalsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientHPIResponse LoadHPIByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientHPIResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadHPIByPatientId Called");
                QuivusLogger.Info("LoadHPIByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadHPIByPatientId(PatientId,ClinicBranchId,stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientHPIResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientHPIResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientROSResponse LoadROSByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientROSResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadROSByPatientId Called");
                QuivusLogger.Info("LoadROSByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadROSByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientROSResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientROSResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientPhysicalResponse LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientPhysicalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPhysicalsByPatientId Called");
                QuivusLogger.Info("LoadPhysicalsByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPhysicalsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientPhysicalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientPhysicalResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse LoadPatientResolvedDiagnosisByPatientId(int PatientId, UserSessionDTO userSession)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientResolvedDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientResolvedDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientResolvedDiagnosisByPatientId(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientChronicDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientChronicDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientChronicDiagnosisByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse LoadPatientAcuteDiagnosisByPatientId(int PatientId, UserSessionDTO userSession)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientAcuteDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientAcuteDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientAcuteDiagnosisByPatientId(PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// This method will load Chronic Diagnosis via paitent id 
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="ClinicalBranchId"></param>
        /// <param name="userSession"></param>
        /// <param name="stationIDs"></param>
        /// <param name="isAllSelected"></param>
        /// <author>HassaN Naqvi</author>
        /// <returns></returns>
        public PatientAllDiagnosisResponse LoadPatientAllChronicDiagnosisByPatientId(int PatientId, int ClinicalBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {

            PatientDiagnosisResponse response = null;
            PatientAllDiagnosisResponse allResponse = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientAllChronicDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientAllChronicDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                   
                    response = viewBackImpl.LoadPatientChronicDiagnosisByPatientId(PatientId, ClinicalBranchId, stationIDs, isAllSelected);

                    allResponse = new PatientAllDiagnosisResponse();
                    allResponse.PatientChronicDiagnosisList = new List<PatientDiagnosisDTO>();

                    if (response != null)
                    {
                        allResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                        allResponse.PatientChronicDiagnosisList = response.PatientDiagnosisList;
                    }                  

                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (allResponse == null)
                    allResponse = new PatientAllDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (allResponse == null)
                    allResponse = new PatientAllDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return allResponse;
        }


        /// <summary>
        /// This method will load all three types of diagnosis via paitent id 
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        /// <author> Baber waqas</author>
        public PatientAllDiagnosisResponse LoadPatientAllDiagnosisByPatientId(int PatientId, int ClinicalBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            
            PatientDiagnosisResponse response = null;
            PatientAllDiagnosisResponse allResponse = null; 
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientALlDiagnosisByPatientId Called");
                QuivusLogger.Info("LoadPatientAllDiagnosisByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    // setting all three responses
                    response = viewBackImpl.LoadPatientAcuteDiagnosisByPatientId(PatientId);

                    if (response != null)
                    {
                        if (allResponse == null)
                            allResponse = new PatientAllDiagnosisResponse();

                        allResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                        allResponse.PatientAcuteDiagnosisList = response.PatientDiagnosisList;
                    }

                    response = viewBackImpl.LoadPatientChronicDiagnosisByPatientId(PatientId, ClinicalBranchId, stationIDs, isAllSelected);
                    if (response != null)
                    {
                        if (allResponse == null)
                            allResponse = new PatientAllDiagnosisResponse();

                        allResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                        allResponse.PatientChronicDiagnosisList = response.PatientDiagnosisList;
                    }
                     

                      response = viewBackImpl.LoadPatientResolvedDiagnosisByPatientId(PatientId);
                      if (response != null)
                      {
                          if (allResponse == null)
                              allResponse = new PatientAllDiagnosisResponse();

                          allResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                          allResponse.PatientResolvedDiagnosisList = response.PatientDiagnosisList;
                      }
                                        
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (allResponse == null)
                    allResponse = new PatientAllDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (allResponse == null)
                    allResponse = new PatientAllDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return allResponse;
        }

        /// <summary>
        /// Author Ismail
        /// Date 5 may 2011
        /// This Method Will be used to load patient chronic diagnosis by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientProcedureResponse LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession,List<int> stationIDs, bool isAllSelected)
        {
            PatientProcedureResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientProcedureByPatientId Called");
                QuivusLogger.Info("LoadPatientProcedureByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientProcedureByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProcedureResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProcedureResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }



        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 06 May 2011
        /// This Method Will be used to import visit By template
        /// </summary>
        /// <returns></returns>

        public QuickInputResponse ImportVisitByTemplate(int TemplateId, int ClinicBranchId, UserSessionDTO userSession)
        {
            QuickInputResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ImportVisitByTemplate Called");
                QuivusLogger.Info("ImportVisitByTemplate PARAM TemplateId::" + TemplateId + "ClinicBranchId" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.ImportVisitByTemplate(TemplateId, ClinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new QuickInputResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new QuickInputResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientMedicalHistoryResponse LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientMedicalHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadMedicalHistoryByPatientId Called");
                QuivusLogger.Info("LoadMedicalHistoryByPatientId PARAM PatientId::" + PatientId );

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadMedicalHistoryByPatientId(PatientId,ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientMedicalHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientMedicalHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientFamilyHistoryResponse LoadFamilyHistoryByPatientId(int PatientId, int clinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientFamilyHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadFamilyHistoryByPatientId Called");
                QuivusLogger.Info("LoadFamilyHistoryByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadFamilyHistoryByPatientId(PatientId, clinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFamilyHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFamilyHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient social history by patient Id
        /// </summary>
        /// <returns></returns>
        public SocialHistoryResponse LoadPatientSocialHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            SocialHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientSocialHistoryByPatientId Called");
                QuivusLogger.Info("LoadPatientSocialHistoryByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientSocialHistoryByPatientId(PatientId, ClinicBranchId, stationIDs,  isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        public PatientRecommendationResponse LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientRecommendationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientRecommendationByPatientId Called");
                QuivusLogger.Info("LoadPatientRecommendationByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientRecommendationByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientRecommendationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientRecommendationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>

        public PatientFollowUpResponse LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientFollowUpResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientFollowUpByPatientId Called");
                QuivusLogger.Info("LoadPatientFollowUpByPatientId PARAM PatientId::" + PatientId + "ClinicBranchId::" + ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientFollowUpByPatientId(PatientId, ClinicBranchId, stationIDs,  isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFollowUpResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFollowUpResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 10 May 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        public PatientLabResponse LoadPatientLabsByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientLabsByPatientId Called");
                QuivusLogger.Info("LoadPatientLabsByPatientId PARAM PatientId::" + PatientId + " ClinicBranchId::" +  ClinicBranchId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadPatientLabsByPatientId(PatientId, ClinicBranchId,stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientLabResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>

        public ModuleDictationResponse LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            ModuleDictationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadModuleDictationByPatientId Called");
                QuivusLogger.Info("LoadModuleDictationByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadModuleDictationByPatientId(PatientId, clinicBranchID, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ModuleDictationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ModuleDictationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>

        public ModuleNotesResponse LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            ModuleNotesResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadModuleNotesByPatientId Called");
                QuivusLogger.Info("LoadModuleNotesByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadModuleNotesByPatientId(PatientId, clinicBranchID,stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ModuleNotesResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ModuleNotesResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientFollowupInstructionResponse LoadFollowupInstructionByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            PatientFollowupInstructionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadFollowupInstructionByPatientId Called");
                QuivusLogger.Info("LoadFollowupInstructionByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadFollowupInstructionByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFollowupInstructionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFollowupInstructionResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public SocialHistoryVBResponse LoadSocialHistoryByPatientId(int PatientId, int ClinicBranchId, UserSessionDTO userSession, List<int> stationIDs, bool isAllSelected)
        {
            SocialHistoryVBResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadSocialHistoryByPatientId Called");
                QuivusLogger.Info("LoadSocialHistoryByPatientId PARAM PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    response = viewBackImpl.LoadSocialHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryVBResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new SocialHistoryVBResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
    }
}
