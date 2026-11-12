using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomResponse.RecentPatient;
using QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization;
using QuivusManager.Buisness.UserBL;
using QuivusManager.Buisness.UserBL.Impl;
using System.ServiceModel;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.Utility;
using QuivusManager.Buisness.PatientBL;
using QuivusManager.Buisness.PatientBL.Impl;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.Buisness.ApplicationSession;
using Quivus.Utility.CustomException;



namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    public partial class QuivusManagerService
    {
        IPatientBL PatientBL = null;

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011
        /// Save the patient profile and language w.r.t the userId provided
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PatientSaveResponse SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList, UserSessionDTO userSession)
        {
            PatientSaveResponse response = null;
            try
            {
                QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient string DOB ::" + patientDTO.DateOfBirth);
                QuivusLogger.Error("In SavePatientBasicProfileAndLangauge called and patient DOB ::" + patientDTO.DateOfBirth);
                QuivusLogger.Info("In QuivusManagerService SavePatientBasicProfileAndLangauge Called");
                QuivusLogger.Info("SavePatientBasicProfileAndLangauge Param userId::" + userId + "managedByUserID::" + managedByUserID + "patientDTO" + patientDTO + "PatientLanguageDTOList::" + PatientLanguageDTOList);

                if (PatientLanguageDTOList != null)                
                    QuivusLogger.Info("PatientLanguageDTOList Count " + PatientLanguageDTOList.Count);
                if (!string.IsNullOrEmpty(patientDTO.StrDOB))
                {
                    patientDTO.DateOfBirth = DateTime.Parse(patientDTO.StrDOB);
                }
                QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient DOB after parsing ::" + patientDTO.DateOfBirth);
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientBasicProfileAndLangauge(userId, managedByUserID, patientDTO, PatientLanguageDTOList);
                    QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient DateofBirth ::" + patientDTO.DateOfBirth);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientSaveResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientSaveResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        public ServiceResponse SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientGurantor Called");
                QuivusLogger.Info("SavePatientGurantor Param patientID::" + patientID + "managedByUserID::" + managedByUserID + "patientGurantorDTO::" + patientGurantorDTO);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientGurantor(patientID, managedByUserID, patientGurantorDTO);
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
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
        /// Date 24 Jan 2011        
        /// save the patient health Info like allergies, medical history, family history
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientHealthDTO"></param>
        /// <returns></returns>
        public ServiceResponse SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientHealthInfo Called");
                QuivusLogger.Info("SavePatientHealthInfo Param patientID::" + patientID + "managedByUserID::" + managedByUserID + "patientHealthDTO::" + patientHealthDTO);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientHealthInfo(patientID, managedByUserID, patientHealthDTO);
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
        /// Author Mansoor
        /// Date 24 Jan 2011        
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        public ServiceResponse SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientInsuranceInfo Called");
                QuivusLogger.Info("SavePatientInsuranceInfo Param patientID::" + patientID + "managedByUserID::" + managedByUserID + "patientInsuranceDTOList::" + patientInsuranceDTOList);

                if (patientInsuranceDTOList != null)
                    QuivusLogger.Info("patientInsuranceDTOList Count :: " + patientInsuranceDTOList.Count);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientInsuranceInfo(patientID, managedByUserID, patientInsuranceDTOList);
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
        /// Created Date 24 Jan 2011
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        public PatientEmergencyResponse ManagePatientEmergencyContact(int PatientId, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList, UserSessionDTO userSession)
        {
            PatientEmergencyResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientEmergencyContact Called");
                QuivusLogger.Info("ManagePatientEmergencyContact Param PatientId::" + PatientId + "CreatedBy::" + CreatedBy + "EmergencyContactList::" + EmergencyContactList);
              
                if (EmergencyContactList != null)
                    QuivusLogger.Info("EmergencyContactList Count :: " + EmergencyContactList.Count);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.ManagePatientEmergencyContact(PatientId, CreatedBy, EmergencyContactList);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientEmergencyResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientEmergencyResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientEmergencyResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 27 Jan 2011
        /// Get portion of patient search data, based on query string and demand page no
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchString"></param>
        /// <param name="demandPageNo"></param>
        /// <returns></returns>
        public PatientSearchResponse SearchPatientbyClinicIdAndSearchString(int clinicBranchId, int providerId, int userTypeId, string searchString, int demandPageNo, UserSessionDTO userSession)
        {
            PatientSearchResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchPatientbyClinicIdAndSearchString Called");
                QuivusLogger.Info("SearchPatientbyClinicIdAndSearchString Param clinicBranchId::" + clinicBranchId + "providerId::" + providerId + "userTypeId::" + userTypeId
                    + "searchString::" + searchString + "demandPageNo::" + demandPageNo);
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SearchPatientbyClinicIdAndSearchString(clinicBranchId, providerId, userTypeId, searchString, demandPageNo);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientSearchResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientSearchResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient in the most patient list for that user on this clinic
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>       
        public bool SaveRecentPatient(int clinicBranchId, int providerUserId, int patientId, UserSessionDTO userSession)
        {
            bool response = false;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SaveRecentPatient Called");
                QuivusLogger.Info("SaveRecentPatient Param clinicBranchId::" + clinicBranchId + "providerUserId::" + providerUserId + "patientId::" + patientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SaveRecentPatient(clinicBranchId, providerUserId, patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Load the list of most recent loaded patients for the provider for location
        /// </summary>
        /// <param name="providerUserId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public RecentPatientRespose LoadRecentPatientsByProviderIdAndBranchId(int providerUserId, int clinicBranchId, UserSessionDTO userSession)
        {
            RecentPatientRespose response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadRecentPatientsByProviderIdAndBranchId Called");
                QuivusLogger.Info("LoadRecentPatientsByProviderIdAndBranchId Param clinicBranchId::" + clinicBranchId + "providerUserId::" + providerUserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadRecentPatientsByProviderIdAndBranchId(providerUserId, clinicBranchId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new RecentPatientRespose();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new RecentPatientRespose();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public PatientProfileSettingResponse LoadPatientProfileSettings(int clinicBranchId, int UserId, int PatientId, UserSessionDTO userSession)
        {
            PatientProfileSettingResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientProfileSettings Called");
                QuivusLogger.Info("LoadPatientProfileSettings Param clinicBranchId::" + clinicBranchId + "UserId::" + UserId + "PatientId::" + PatientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientProfileSettings(clinicBranchId, UserId, PatientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileSettingResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileSettingResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileSettingResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to load Patient Profile Setting Providing 
        /// Location-Id & Provider i.e doctor or nurse Id.
        /// Settings are customized by combination of Provider&User Id 
        /// i.e Same user on different location may have different setting Vise Vers :)
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public PatientProfileSettingResponse LoadPatientProfileSettingsBySection(int clinicBranchId, int UserId, int PatientId, int SectionId, UserSessionDTO userSession)
        {
            PatientProfileSettingResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientProfileSettingsBySection Called");
                QuivusLogger.Info("LoadPatientProfileSettingsBySection Param clinicBranchId::" + clinicBranchId + "UserId::" + UserId + "PatientId::" + PatientId+
                    "SectionId:: " + SectionId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientProfileSettingsBySection(clinicBranchId, UserId, PatientId, SectionId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileSettingResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileSettingResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerId"></param>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public PatientProfileSettingResponse ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj, UserSessionDTO userSession)
        {
            PatientProfileSettingResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientProfileSettings Called");
                QuivusLogger.Info("ManagePatientProfileSettings Param ProfileSettingObj::" + ProfileSettingObj);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.ManagePatientProfileSettings(ProfileSettingObj);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileSettingResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileSettingResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 Feb 2011
        /// Save the patient allergy info provided
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="patientAllergy"></param>
        /// <returns></returns>     
        public ServiceResponse SavePatientAllergy(int patientId, int createdBy, PatientAllergyDTO patientAllergy, UserSessionDTO userSession)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientAllergy Called");
                QuivusLogger.Info("SavePatientAllergy Param patientId::" + patientId + "createdBy::" + createdBy + "patientAllergy::" + patientAllergy);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientAllergy(patientId, createdBy, patientAllergy);
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
        /// Author Mansoor
        /// Date 2 Feb 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>       
        public PatientProfileResponse LoadPatientCompleteProfileForClinic(int patientId, int clinicBranchId, int providerUserId, UserSessionDTO userSession)
        {
            PatientProfileResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientCompleteProfileForClinic Called");
                QuivusLogger.Info("LoadPatientCompleteProfileForClinic Param patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "providerUserId::" + providerUserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientCompleteProfileForClinic(patientId, clinicBranchId, providerUserId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient info to show in the left window panel
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>        
        public PatientProfileInfoResposne LoadPatientProfileInfoForProvider(int patientId, int clinicBranchId, int providerUserId, UserSessionDTO userSession)
        {
            PatientProfileInfoResposne response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientProfileInfoForProvider Called");
                QuivusLogger.Info("LoadPatientProfileInfoForProvider Param patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "providerUserId::" + providerUserId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientProfileInfoForProvider(patientId, clinicBranchId, providerUserId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileInfoResposne();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileInfoResposne();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileInfoResposne();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the patient profile allergies for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>       
        public PatientProfileAllergyResponse LoadPatientAllergiesForProvider(int patientId, UserSessionDTO userSession)
        {
            PatientProfileAllergyResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientAllergiesForProvider Called");
                QuivusLogger.Info("LoadPatientAllergiesForProvider Param patientId::" + patientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientAllergiesForProvider(patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileAllergyResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileAllergyResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile diagnosis for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>       

        public PatientProfileDiagnosisResponse LoadPatientDiagnosisForProvider(int patientId, UserSessionDTO userSession)
        {
            PatientProfileDiagnosisResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientAllergiesForProvider Called");
                QuivusLogger.Info("LoadPatientAllergiesForProvider Param patientId::" + patientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientDiagnosisForProvider(patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileDiagnosisResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileDiagnosisResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile immunization for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>           
        public PatientProfileImmunizationResponse LoadPatientImmunizationForProvider(int patientId, UserSessionDTO userSession)
        {
            PatientProfileImmunizationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientImmunizationForProvider Called");
                QuivusLogger.Info("LoadPatientImmunizationForProvider Param patientId::" + patientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientImmunizationForProvider(patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileImmunizationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileImmunizationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 4 Feb 2011
        /// load the patient profile medication for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>            
        public PatientProfileChronicMedicationResponse LoadPatientMedicationForProvider(int patientId, UserSessionDTO userSession)
        {
            PatientProfileChronicMedicationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientMedicationForProvider Called");
                QuivusLogger.Info("LoadPatientMedicationForProvider Param patientId::" + patientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientMedicationForProvider(patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileChronicMedicationResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileChronicMedicationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 4 Feb 2011
        /// load the patient profile Insurance for the provider, to show in patient exam container, and tagging
        /// </summary>
        /// <param name="patientId"></param>        
        /// <returns></returns>       
        public PatientProfileInsuranceResponse LoadPatientInsuranceForProvider(int patientId, UserSessionDTO userSession)
        {
            PatientProfileInsuranceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientInsuranceForProvider Called");
                QuivusLogger.Info("LoadPatientInsuranceForProvider Param patientId::" + patientId);

                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientInsuranceForProvider(patientId);
                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new PatientProfileInsuranceResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new PatientProfileInsuranceResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

    }
}