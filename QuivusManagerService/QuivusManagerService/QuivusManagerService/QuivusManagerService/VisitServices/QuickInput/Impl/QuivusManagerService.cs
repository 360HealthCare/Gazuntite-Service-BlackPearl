using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.Buisness.ClinicBL;
using QuivusManager.Buisness.ClinicBL.Impl;
using QuivusManager.Buisness.PatientBL.Impl;
using QuivusManager.Buisness.StaticDataBL.Impl;
using QuivusManager.Buisness.UserBL.Impl;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.VisitBL.QuickInput.Impl;
using QuivusManager.Buisness.VisitBL.ViewBackgorund.Impl;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManagerService.CustomeControls;
using QuivusManagerService.CustomeControls.General;
using QuivusManagerService.CustomeControls.General.SoapNote;
using QuivusFault = QuivusManager.IntegratedServies.VisitService.QuivusFault;
using UserTypeDTO = QuivusManager.IntegratedServies.VisitService.UserTypeDTO;

namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        private IClinicBL clinicBL;
        private VisitDTO visit;
   // Code review and adjusted by Baber waqas on 1/24/2012
        #region Visit Region 

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns>        
        public CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId, UserSessionDTO userSession)
        {
            //ModuleDictationDTO dic = new ModuleDictationDTO();

            int managedByUserTypeId = 0;

            CreateVisitResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService CreateVisit Called");
                QuivusLogger.Info("CreateVisit PARAM visitDTO::" + visitDTO + "managedByUserId::" + managedByUserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }

                    UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);

                    if (session != null)
                    {
                        managedByUserTypeId = session.UserTypeID;

                        if (session.IsProvider)
                        {
                            // This Logged In Usr Is Provider 
                            // Now Check Either Visit Owner is Already Set Or Not 
                            if (visitDTO.OwnerId <= 0)
                            {
                                // Logged In User is Provider & Visit Owner is Null Make This User Owner of Visit
                                visitDTO.OwnerId = session.UserID;
                                visitDTO.OwnerUserType = new UserTypeDTO {UserTypeId = session.UserTypeID};
                            }
                        }
                    }

                    response = quickInputImpl.CreateVisit(visitDTO, managedByUserId, managedByUserTypeId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new CreateVisitResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new CreateVisitResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.UserManagerService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new CreateVisitResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>       
        public ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            int managedByUserTypeId = 0;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SaveVisit Called");
                QuivusLogger.Info("SaveVisit PARAM visitDTO::" + visitDTO + "managedByUserId::" + managedByUserId +
                                  "managedByUserTypeId::" + managedByUserTypeId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (session != null)
                    {
                        managedByUserTypeId = session.UserTypeID;
                        if (session.IsProvider)
                        {
                            // This Logged In Usr Is Provider 
                            // Now Check Either Visit Owner is Already Set Or Not 
                            if (visitDTO.OwnerId <= 0)
                            {
                                // Logged In User is Provider & Visit Owner is Null Make This User Owner of Visit
                                visitDTO.OwnerId = session.UserID;
                                visitDTO.OwnerUserType = new UserTypeDTO {UserTypeId = session.UserTypeID};
                            }
                        }
                    }

                    response = quickInputImpl.SaveVisit(visitDTO, managedByUserId, managedByUserTypeId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>      
        public ServiceResponse SignVisit(int VisitId, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SignVisit Called");
                QuivusLogger.Info("SignVisit PARAM visitDTO::" + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }

                    UserSessionDTO obj = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (obj != null)
                        response = quickInputImpl.SignVisit(VisitId, obj.UserID, obj.UserTypeID, DateTime.Now);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        #endregion

     

        #region IQuivusManagerService Members

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        public ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList,
                                                                  UserSessionDTO userSession, int clinicBranchId, int patientId)
        {
            ImmunizationSaveResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientImmunization Called");
                QuivusLogger.Info("ManagePatientImmunization PARAM immunizationList::" + immunizationList);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientImmunization(immunizationList,clinicBranchId,patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ImmunizationSaveResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ImmunizationSaveResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>        
        public ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageModuleNotes Called");
                QuivusLogger.Info("ManageModuleNotes PARAM moduleNotes::" + moduleNotes);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManageModuleNotes(moduleNotes);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        public PatientMedicalHistoryResponse ManagePatientMedicalHistory(
            List<MedicalHistoryDTO> PateintMedicalHistoryList, UserSessionDTO userSession, int cbID, int patId)
        {
            PatientMedicalHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadMedicalHistoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientMedicalHistory(PateintMedicalHistoryList,  cbID,  patId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientMedicalHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientMedicalHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 11 Mar 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>        
        public ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId,
                                                        UserSessionDTO userSession)
        {
            var stopwatch = new Stopwatch();
            QuivusLogger.Info("PerfomanceTestManageQuickInput:Start time stamp is " + DateTime.Now);
            // Begin timing
            stopwatch.Start();
            ManageVisitResponse response = null;
            int userTypeId = 0;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageQuickInputData Called");
                QuivusLogger.Info("ManageVisitWithData PARAM quickInputDTO::" + quickInputDTO + "managedByUserId" +
                                  managedByUserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (session != null)
                        userTypeId = session.UserTypeID;

                    response = quickInputImpl.ManageQuickInputData(quickInputDTO, managedByUserId, userTypeId);

                    #region Update Allergies from eprescription

                    if (quickInputDTO.PMH.Allergy != null)
                    {
                        IEnumerable<AllergyDTO> qResults = from allergy in quickInputDTO.PMH.Allergy
                                                           where
                                                               allergy.IsActive.Equals(
                                                                   ProjectConstant.STRING_FALSE.ToLower())
                                                           select allergy;

                        quickInputImpl.UpdateAllergiesToEprescription(qResults, quickInputDTO.Visit.PatientId);
                    }

                    #endregion
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ManageVisitResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ManageVisitResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            stopwatch.Stop();

            // Write result
            //Console.WriteLine("Time elapsed: {0}",
            //    stopwatch.Elapsed);
            QuivusLogger.Info("PerfomanceTestManageQuickInput:total time stamp is " + DateTime.Now);
            QuivusLogger.Info("PerfomanceTestManageQuickInput:total time elapsed is " + stopwatch.Elapsed);
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 12 july 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        public ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId, bool IsFinal,
                                                        UserSessionDTO userSession)
        {
            ManageVisitResponse response = null;
            int UserTypeId = 0;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageQuickInputData Called");
                QuivusLogger.Info("ManageVisitWithData PARAM quickInputDTO::" + quickInputDTO + "managedByUserId" +
                                  managedByUserId + "IsFinal " + IsFinal);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (session != null)
                        UserTypeId = session.UserTypeID;

                    response = quickInputImpl.ManageQuickInputData(quickInputDTO, managedByUserId, UserTypeId, IsFinal);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ManageVisitResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ManageVisitResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        public PatientFamilyHistoryResponse ManagePatientFamilyHistory(List<FamilyHistoryDTO> PateintFamilyHistoryList,
                                                                       UserSessionDTO userSession, int cbId, int patId)
        {
            PatientFamilyHistoryResponse response = null;
            int UserTypeId = 0;
            int ClinicBranchId = 0;

            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadMedicalHistoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }

                    UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);

                    //if (session != null)
                    //{
                    //    UserTypeId = session.UserTypeID;
                    //    ClinicBranchId = session.ClinicBranchID;
                    //}
                    response = quickInputImpl.ManagePatientFamilyHistory(PateintFamilyHistoryList, UserTypeId,
                                                                         cbId, patId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFamilyHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse ManagePatientVitals(List<VitalDTO> VitalList, UserSessionDTO userSession)
        {
            PatientVitalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadMedicalHistoryTypes Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientVitals(VitalList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientVitalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Labs
        /// </summary>
        /// <returns></returns>
        public PatientLabResponse ManagePatientLabs(List<LabDTO> LabList, UserSessionDTO userSession)
        {
            PatientLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientLabs Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientLabs(LabList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientLabResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
        public PatientHPIResponse ManagePatientHPI(List<HPI_CCDTO> hpiList, UserSessionDTO userSession)
        {
            PatientHPIResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientHPI Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientHPI(hpiList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientHPIResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientHPIResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public PatientFollowUpResponse ManagePatientFollowUp(List<PatientFollowUpDTO> patientFollowUpList,
                                                             UserSessionDTO userSession)
        {
            PatientFollowUpResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientFollowUp Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientFollowUp(patientFollowUpList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFollowUpResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFollowUpResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList,
                                                               UserSessionDTO userSession, int cbId, int patID)
        {
            PatientDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientFollowUp Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientDiagnosis(diagnosisList, cbId, patID);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Mar 2011
        /// get hpi details for auto complete suggestion box, by type id and clinic branch
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>       
        public HPIDetailAutoCompleteResponse LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId,
                                                                                          int clinicBranchId,
                                                                                          string searchCriteria,
                                                                                          UserSessionDTO userSession)
        {
            HPIDetailAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadHPIDetailAutoCompleteByTypeIdAndClinicId Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.LoadHPIDetailAutoCompleteByTypeIdAndClinicId(hpiTypeId, clinicBranchId,
                                                                                           searchCriteria);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new HPIDetailAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new HPIDetailAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 19 Feb 2011
        /// save/updateSocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public SocialHistoryAddictionResponse ManageSocialHistoryAddiction(
            SocialHistoryAddictionDTO SocialHistoryAddiction, UserSessionDTO userSession,int cbId, int patId)
        {
            SocialHistoryAddictionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadHPIDetailAutoCompleteByTypeIdAndClinicId Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManageSocialHistoryAddiction(SocialHistoryAddiction,cbId,patId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryAddictionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 19 Feb 2011
        /// Delete SocialHistory Addiction
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public SocialHistoryAddictionResponse DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject,
                                                                           UserSessionDTO userSession)
        {
            SocialHistoryAddictionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadHPIDetailAutoCompleteByTypeIdAndClinicId Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.DeleteSocialHistoryAddiction(DeletedObject);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryAddictionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 19 Feb 2011
        /// save/updateSocial History 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public SocialHistoryResponse ManagePatientSocialHistory(List<SocialHistoryDTO> SocialHistoryList,
                                                                UserSessionDTO userSession, int cbId, int patId)
        {
            SocialHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadHPIDetailAutoCompleteByTypeIdAndClinicId Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientSocialHistory(SocialHistoryList, cbId, patId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 19 Feb 2011
        /// Delete SocialHistory 
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public SocialHistoryResponse DeleteSocialHistory(SocialHistoryDTO DeletedObject, UserSessionDTO userSession)
        {
            SocialHistoryResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService DeleteSocialHistory Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.DeleteSocialHistory(DeletedObject);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new SocialHistoryResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 22 March 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public PatientPhysicalResponse ManagePatientPhysical(List<PhysicalsDTO> PhysicalList, UserSessionDTO userSession)
        {
            PatientPhysicalResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientPhysical Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientPhysical(PhysicalList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientPhysicalResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 21 Feb 2011
        /// save/update patient ROS
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public PatientROSResponse ManagePatientROS(List<ROSDTO> ROSList, UserSessionDTO userSession)
        {
            PatientROSResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientROS Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientROS(ROSList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientROSResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientROSResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// get Recommendation auto complete suggestion box, by type id and clinic branch
        /// </summary>        
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public LinksAutoCompleteResponse LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId,
                                                                                       string searchCriteria,
                                                                                       UserSessionDTO userSession)
        {
            LinksAutoCompleteResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadRecommendationLinksAutoCompleteByClinicId Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.LoadRecommendationLinkAutoCompleteByClinicId(clinicBranchId,
                                                                                           searchCriteria);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new LinksAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LinksAutoCompleteResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 22 March 2011
        /// save/update patient Recommendation
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public PatientRecommendationResponse ManagePatientRecommendation(
            List<PatientRecommendationDTO> RecommendationList, UserSessionDTO userSession)
        {
            PatientRecommendationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientRecommendation Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientRecommendation(RecommendationList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientRecommendationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientRecommendationResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>
        public ServiceResponse CoSignVisit(int VisitId, int ProviderId, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SignVisit Called");
                QuivusLogger.Info("SignVisit PARAM ProviderId::" + ProviderId + "VisitId  " + VisitId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    UserSessionDTO obj = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (obj != null)
                        response = quickInputImpl.CoSignVisit(VisitId, ProviderId, obj.UserID, obj.UserTypeID,
                                                              DateTime.Now);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ServiceResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public AllergyResponse ManagePatientAllergy(AllergyDTO AllergyObj, UserSessionDTO userSession)
        {
            AllergyResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientAllergy Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientAllergy(AllergyObj);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new AllergyResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 26 March 2011
        /// save patient chrnoc medication
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public MedicationPrescribedResponse ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj,
                                                                           UserSessionDTO userSession)
        {
            MedicationPrescribedResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientChronicMedication Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientChronicMedication(ChronicMedicationObj);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new MedicationPrescribedResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        public PatientProcedureResponse ManagePatientProcedure(List<ProcedureDTO> InputList, UserSessionDTO userSession)
        {
            PatientProcedureResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientProcedure Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientProcedure(InputList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProcedureResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProcedureResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientInHouseLabResponse ManagePatientInHouseLabs(List<InHouseLabDTO> InputList,
                                                                  UserSessionDTO userSession)
        {
            PatientInHouseLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientInHouseLabs Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientInHouseLabs(InputList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientInHouseLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 06 April 2011
        /// save/update patient in house labs results
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientInHouseLabResponse ManagePatientInHouseLabResults(int PatientInHouseLabTestID, String TestResult,
                                                                        byte[] ResultAttachment, int ModifiedBy,
                                                                        DateTime ModifiedAt, UserSessionDTO userSession)
        {
            PatientInHouseLabResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientInHouseLabs Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientInHouseLabResults(PatientInHouseLabTestID, TestResult,
                                                                             ResultAttachment, ModifiedBy, ModifiedAt);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientInHouseLabResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientMedicationReponse ManagePatientMedication(MedicationDTO InputObj, UserSessionDTO userSession)
        {
            PatientMedicationReponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientMedication Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientMedication(InputObj);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientMedicationReponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 07 April 2011
        /// Delete patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientMedicationReponse DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt,
                                                                UserSessionDTO userSession)
        {
            PatientMedicationReponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientMedication Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.DeletePatientMedication(PatientMedicationID, ModifiedAt);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientMedicationReponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
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
        /// Date 22 May 2011
        /// This Method Will be used to Insert/Update/Delete Patient follow up instructions
        /// </summary>
        /// <returns></returns>
        public PatientFollowupInstructionResponse ManagePatientFollowupInstruction(
            List<PatientFollowupInstructionDTO> InputList, UserSessionDTO userSession)
        {
            PatientFollowupInstructionResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientFollowupInstruction Called");

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                    }
                    response = quickInputImpl.ManagePatientFollowupInstruction(InputList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientFollowupInstructionResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientFollowupInstructionResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 07/26/2011
        /// This method will save autosave data
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="fileName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool SaveTempQI(QuickInputDTO quickInputDTO, string fileName, UserSessionDTO userSession)
        {
            bool response = false;
            try
            {
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                        response = quickInputImpl.SaveTempQI(quickInputDTO, fileName);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                ResponseErrorHandler.HandleException(new PatientFollowupInstructionResponse(), exp);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 07/26/2011
        /// This method will delete autosave data
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="fileName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool DeleteTempQI(string fileName, UserSessionDTO userSession)
        {
            bool response = false;
            try
            {
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                        response = quickInputImpl.DeleteTempQI(fileName);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                ResponseErrorHandler.HandleException(new PatientFollowupInstructionResponse(), exp);
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 07/26/2011
        /// This method will load autosave data
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="fileName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public QuickInputResponse LoadTempQI(string fileName, UserSessionDTO userSession)
        {
            QuickInputResponse tempQIResponse = null;
            if (ApplicationSessionHandler.IsValidUserSession(userSession))
            {
                if (quickInputImpl == null)
                {
                    quickInputImpl = new QuickInputBLImpl();
                    tempQIResponse = quickInputImpl.LoadTempQI(fileName);
                }
            }
            return tempQIResponse;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 07/26/2011
        /// This method will check data is exists or not.
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="fileName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool IsTempQIExists(string fileName, UserSessionDTO userSession)
        {
            bool response = false;
            try
            {
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                        response = quickInputImpl.IsTempQIExists(fileName);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                ResponseErrorHandler.HandleException(new PatientFollowupInstructionResponse(), exp);
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient Family History
        /// </summary>
        /// <returns></returns>
        public bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject, UserSessionDTO userSession)
        {
            bool response = false;
            try
            {
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                        response = quickInputImpl.DeleteFamilyHistory(DeletedObject);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                ResponseErrorHandler.HandleException(new PatientFollowupInstructionResponse(), exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient medical History
        /// PatientMedicalHistory_Delete
        /// </summary>
        /// <returns></returns>
        public bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject, UserSessionDTO userSession)
        {
            bool response = false;
            try
            {
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (quickInputImpl == null)
                    {
                        quickInputImpl = new QuickInputBLImpl();
                        response = quickInputImpl.DeleteMedicalHistory(DeletedObject);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                ResponseErrorHandler.HandleException(new PatientFollowupInstructionResponse(), exp);
            }
            return response;
        }



     

        /// <summary>
        /// Author Tamjeed-ul-Hassan 
        /// Date 08 December 2011
        /// This Method Will be used to generate Soap-Note
        /// </summary>
        /// <returns></returns>
        /// NOT USED Comments by BABER WAQAS
        [Obsolete]
        public string GenerateSoapNoteByVisitID(int VisitID, UserSessionDTO userSession, int clinicbranchId,
                                                List<int> stationIds, bool isAllSelected)
        {
            string note = "";
            try
            {
                QuivusLogger.Info("In QuivusManagerService GenerateSoapNoteByVisitID Called");
                QuivusLogger.Info("GenerateSoapNoteByVisitID PARAM VisitID::" + VisitID);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    var page = new Page();
                    page.EnableEventValidation = false;

                    var soapNote = (SoapNoteView) page.LoadControl("CustomeControls/General/SoapNoteView.ascx");
                    var patientDetails = (SoapNotePatient) soapNote.FindControl("patientDetails");
                    var myHeader = (SoapNoteHeader) soapNote.FindControl("header");


                    if (clinicBL == null)
                    {
                        clinicBL = new ClinicBL();
                    }
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }

                    if (visit != null)
                    {
                        if (PatientBL == null)
                        {
                            PatientBL = new PatientBLImpl();
                        }

                        PatientProfileDTO patientInfo =
                            PatientBL.LoadPatientCompleteProfileForClinic(visit.PatientId,
                                                                          visit.ClinicBranch.ClinicBranchIdSeq,
                                                                          visit.OwnerId).PatientProfileResponseData;
                        BaseUserDTO providerInfo = UserImpl.LoadBaseUserByUserId(visit.OwnerId).ResponseData;

                        patientDetails.PatientName = patientInfo.UserDTO.FirstName + " " + patientInfo.UserDTO.LastName;
                        patientDetails.DOB = patientInfo.PatientDTO.DateOfBirth.ToShortDateString();
                        patientDetails.DateOfService = visit.VisitDate.ToShortDateString();
                        patientDetails.ProviderName = providerInfo.FirstName + " " + providerInfo.LastName;
                    }


                    ClinicDTO clinic = clinicBL.LoadClinicBranchAndStationInfo(stationIds[0]);
                    //myHeader.ClinicName = clinic.ClinicBranchInfo.ClinicBranchName;
                    myHeader.ClinicAddress = clinic.ClinicBranchInfo.Address + ", " + clinic.ClinicBranchInfo.ZIP + "," +
                                             clinic.ClinicBranchInfo.State;
                    myHeader.ClinicPhone = clinic.ClinicBranchInfo.DefaultStation.PhoneNumber;
                    myHeader.ClinicFax = clinic.ClinicBranchInfo.DefaultStation.FaxNumber;


                    soapNote.Controls.Add(myHeader);
                    soapNote.Controls.Add(patientDetails);


                    List<List<SoapNoteCatagoryDTO>> list = getSoapNoteDataListByVisit(VisitID, clinicbranchId,
                                                                                      stationIds, isAllSelected);

                    //History Section
                    if (list.Count >= 1 && list[0].Count > 0)
                    {
                        var history = (SoapNoteGridView) soapNote.FindControl("history");
                        history.Title = "History:";
                        history.Data = list[0];

                        soapNote.Controls.Add(history);
                    }

                    //Subjective Section
                    if (list.Count >= 2 && list[1].Count > 0)
                    {
                        var subjective = (SoapNoteGridView) soapNote.FindControl("subjective");
                        subjective.Title = "Subjective:";
                        subjective.Data = list[1];

                        soapNote.Controls.Add(subjective);
                    }

                    //Objective Section
                    if (list.Count >= 3 && list[2].Count > 0)
                    {
                        var objective = (SoapNoteGridView) soapNote.FindControl("objective");
                        objective.Title = "Objective:";
                        objective.Data = list[2];

                        soapNote.Controls.Add(objective);
                    }

                    //Assessment Section
                    if (list.Count >= 4 && list[3].Count > 0)
                    {
                        var assessment = (SoapNoteGridView) soapNote.FindControl("assessment");
                        assessment.Title = "Assessment:";
                        assessment.Data = list[3];

                        soapNote.Controls.Add(assessment);
                    }

                    //Plan Section
                    if (list.Count >= 5 && list[4].Count > 0)
                    {
                        var plan = (SoapNoteGridView) soapNote.FindControl("plan");
                        plan.Title = "Plan:";
                        plan.Data = list[4];

                        soapNote.Controls.Add(plan);
                    }

                    var signedBy = (SoapNoteSignedBy) soapNote.FindControl("signedBy");
                    signedBy.ProviderName = "Dr. John - at 01/02/2011";
                    //signedBy.CCProviders = "Dr. John <br> Dr. Johnson";
                    soapNote.Controls.Add(signedBy);

                    var footer = (SoapNoteFooter) soapNote.FindControl("footer");
                    footer.ProviderName = "Dr. John - at 01/02/2011";
                    soapNote.Controls.Add(footer);

                    soapNote.BindData();
                    patientDetails.BindData();

                    page.Controls.Add(soapNote);
                    page.DataBind();


                    //note = TempCtrlUtil.RenderControl(soapNote);
                    note = TempCtrlUtil.RenderControl(page);

                    note = HttpUtility.HtmlDecode(note);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Critical(exp.Code + exp.Data.ToString() + "\n" + exp.StackTrace);
            }
            finally
            {
            }

            note = TempCtrlUtil.createHtmlPage(note);
            return note;
        }

        public string GenerateTempSoapNote(string fileName)
        {
            throw new NotImplementedException();
        }

        #endregion

        [Obsolete]
        public List<List<SoapNoteCatagoryDTO>> getSoapNoteDataListByVisit(int VisitID, int clinicBranchId,
                                                                          List<int> stationIds, bool isAllSelected)
        {
            var list = new List<List<SoapNoteCatagoryDTO>>();
            try
            {
                QuivusLogger.Info("In QuivusManagerService GenerateSoapNoteByVisitID Called");
                QuivusLogger.Info("GenerateSoapNoteByVisitID PARAM VisitID::" + VisitID);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    QuickInputResponse response = null;

                    response = viewBackImpl.LoadQuickInputByVisit(VisitID);
                    PatientPrescriptionMedicationResponse preResponse = null;
                    preResponse =
                        viewBackImpl.LoadPrescriptionMedicationByPatientId(response.ResponseData.Visit.PatientId,
                                                                           clinicBranchId, stationIds, isAllSelected);
                    VisitNotesResponse notes = viewBackImpl.LoadModuleNotesByVisitId(VisitID);
                    SoapNoteDataProcessor.ModuleTypeList = StaticDataBL.LoadModuleTypes().ModuleTypeData;
                    SoapNoteDataProcessor.ModuleList = response.ResponseData.ModuleNotes;
                    visit = response.ResponseData.Visit;

                    list = SoapNoteDataProcessor.ProcessCatagories(response.ResponseData, clinicBranchId
                        /*, preResponse.ResponseList*/);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Critical(exp.Code + exp.Data.ToString() + "\n" + exp.StackTrace);
            }

            return list;
        }
    }
}