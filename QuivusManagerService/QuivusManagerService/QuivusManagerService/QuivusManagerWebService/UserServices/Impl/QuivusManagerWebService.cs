using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.Buisness.UserBL;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.Buisness.UserBL.Impl;
using System.ServiceModel;

using Quivus.Utility.Logger;
using QuivusManager.Buisness.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.Buisness.PatientBL;
using QuivusManager.Buisness.PatientBL.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;
using QuivusManager.Buisness.AddressBookBL;
using QuivusManager.Buisness.AddressBookBL.Impl;
using Quivus.Utility;

namespace QuivusManagerWebService
{
    public partial class QuivusManagerWebService
    {

        IUserBL UserImpl = null;

        IPatientBL PatientBL = null;

        IAddressBookBL AddressBookBL = null;

        /// <summary>
        /// Author Mansoor
        /// Created 18 Aug 2011
        /// This Method is Used TO Authenicate the User 
        /// In Case Of Successfull verfication system return User DTO Object        
        /// Incase of unsucessfull scenario system sends the error message and code
        /// Clinet side
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        /// 
        public LoginResponse AuthenticateUser(string UserId, string UserPassword)
        {
            LoginResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerWebService AuthenticateUser Called");
                QuivusLogger.Info("AuthenticateUser PARAM UserId::" + UserId + "UserPassword::" + UserPassword);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                //{
                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                response = UserImpl.AuthenticateUser(UserId, UserPassword);
            }
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new LoginResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        public BaseUserRegistrationResponse RegisterBaseUser(BaseUserDTO baseUserDTO)
        {
            BaseUserRegistrationResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerWebService RegisterBaseUser Called");
                QuivusLogger.Info("RegisterBaseUser PARAM BaseUserDTO::" + baseUserDTO);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.RegisterBaseUser(baseUserDTO);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new BaseUserRegistrationResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new BaseUserRegistrationResponse();
                ResponseErrorHandler.HandleException(response, exp);
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);

                if (response == null)
                    response = new BaseUserRegistrationResponse();
                ResponseErrorHandler.HandleException(response, ex);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 aug 2011
        /// verify email existance 
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>            
        public bool VerifyUserEmail(string email)
        {
            bool response = false;
            try
            {
                QuivusLogger.Info("In QuivusManagerWebService VerifyUserEmail Called");

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }
                    response = UserImpl.VerifyUserEmail(email);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new BaseUserRegistrationResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                //if (response == null)
                //    response = new BaseUserRegistrationResponse();
                //ResponseErrorHandler.HandleException(response, exp);
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);

                //if (response == null)
                //    response = new BaseUserRegistrationResponse();
                //ResponseErrorHandler.HandleException(response, ex);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 24 Aug 2011
        /// Save the patient profile and language w.r.t the userId provided
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PatientSaveResponse SavePatientBasicProfileAndLangauge(int userId, int managedByUserID, PatientDTO patientDTO, List<PatientLanguageDTO> PatientLanguageDTOList)
        {
            PatientSaveResponse response = null;
            try
            {
                QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient string DOB ::" + patientDTO.DateOfBirth);
                QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient DOB ::" + patientDTO.DateOfBirth);
                QuivusLogger.Info("In QuivusManagerService SavePatientBasicProfileAndLangauge Called");
                QuivusLogger.Info("SavePatientBasicProfileAndLangauge Param userId::" + userId + "managedByUserID::" + managedByUserID + "patientDTO" + patientDTO + "PatientLanguageDTOList::" + PatientLanguageDTOList);

                if (PatientLanguageDTOList != null)
                    QuivusLogger.Info("PatientLanguageDTOList Count " + PatientLanguageDTOList.Count);
                if (!string.IsNullOrEmpty(patientDTO.StrDOB))
                {
                    patientDTO.DateOfBirth = DateTime.Parse(patientDTO.StrDOB);
                }
                QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient DOB after parsing ::" + patientDTO.DateOfBirth);
                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientBasicProfileAndLangauge(userId, managedByUserID, patientDTO, PatientLanguageDTOList);
                    QuivusLogger.Info("In SavePatientBasicProfileAndLangauge called and patient DateofBirth ::" + patientDTO.DateOfBirth);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new PatientSaveResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 2 Feb 2011
        /// load the patient profile and also save the required info in the provider's most recent patient list.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>       
        public PatientProfileResponse LoadPatientCompleteProfile(int patientId, int clinicBranchId, int providerUserId)
        {
            PatientProfileResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadPatientCompleteProfileForClinic Called");
                QuivusLogger.Info("LoadPatientCompleteProfileForClinic Param patientId::" + patientId + "clinicBranchId::" + clinicBranchId + "providerUserId::" + providerUserId);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.LoadPatientCompleteProfileForClinic(patientId, clinicBranchId, providerUserId);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new PatientProfileResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 8 Sep 2011         
        /// save the patient Gurantor Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientGurantorDTO"></param>
        /// <returns></returns>
        public ServiceResponse SavePatientGurantor(int patientID, int managedByUserID, PatientGuarantorDTO patientGurantorDTO)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientGurantor Called");
                QuivusLogger.Info("SavePatientGurantor Param patientID::" + patientID + "managedByUserID::" + managedByUserID + "patientGurantorDTO::" + patientGurantorDTO);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientGurantor(patientID, managedByUserID, patientGurantorDTO);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 8 Sep 2011         
        /// save the patient health Info like allergies, medical history, family history
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="managedByUserID"></param>
        /// <param name="patientHealthDTO"></param>
        /// <returns></returns>
        public ServiceResponse SavePatientHealthInfo(int patientID, int managedByUserID, PatientHealthDTO patientHealthDTO)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientHealthInfo Called");
                QuivusLogger.Info("SavePatientHealthInfo Param patientID::" + patientID + "managedByUserID::" + managedByUserID + "patientHealthDTO::" + patientHealthDTO);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientHealthInfo(patientID, managedByUserID, patientHealthDTO);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 8 Sep 2011         
        /// save the patient Insurance Info
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientInsuranceDTOList"></param>
        /// <returns></returns>
        public ServiceResponse SavePatientInsuranceInfo(int patientID, int managedByUserID, List<PatientInsuranceDTO> patientInsuranceDTOList)
        {
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SavePatientInsuranceInfo Called");
                QuivusLogger.Info("SavePatientInsuranceInfo Param patientID::" + patientID + "managedByUserID::" + managedByUserID + "patientInsuranceDTOList::" + patientInsuranceDTOList);

                if (patientInsuranceDTOList != null)
                    QuivusLogger.Info("patientInsuranceDTOList Count :: " + patientInsuranceDTOList.Count);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.SavePatientInsuranceInfo(patientID, managedByUserID, patientInsuranceDTOList);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 8 Sep 2011 
        /// This Method Used To Insert/Update/Delete The patient emergency contact Information :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="EmergencyContactList"></param>
        /// <returns></returns>
        public PatientEmergencyResponse ManagePatientEmergencyContact(int PatientId, int CreatedBy, List<PatientEmergencyContactDTO> EmergencyContactList)
        {
            PatientEmergencyResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManagePatientEmergencyContact Called");
                QuivusLogger.Info("ManagePatientEmergencyContact Param PatientId::" + PatientId + "CreatedBy::" + CreatedBy + "EmergencyContactList::" + EmergencyContactList);

                if (EmergencyContactList != null)
                    QuivusLogger.Info("EmergencyContactList Count :: " + EmergencyContactList.Count);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (PatientBL == null)
                    {
                        PatientBL = new PatientBLImpl();
                    }
                    response = PatientBL.ManagePatientEmergencyContact(PatientId, CreatedBy, EmergencyContactList);
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new PatientEmergencyResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
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
        /// Date 12 Sep 2011        
        /// save the user address book, specified in the Party Type
        /// </summary>
        /// <param name="addressBookDTO"></param>
        /// <returns></returns>
        public ServiceResponse SaveAddressBook(int roleProfileID, int managedByUserID, QuivusManager.IntegratedServies.AddressBookService.AddressBookDTO addressBookDTO)
        {
            int UpdateResult = 0;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SaveAddressBook Called");
                QuivusLogger.Info("SaveAddressBook Param roleProfileID::" + roleProfileID + " managedByUserID::" + managedByUserID + " addressBookDTO" + addressBookDTO);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (AddressBookBL == null)
                    {
                        AddressBookBL = new AddressBookBLImpl();
                    }

                    UpdateResult = AddressBookBL.SaveUserAddressBook(roleProfileID, managedByUserID, addressBookDTO);

                    if (UpdateResult > 0)
                    {
                        if (response == null)
                        {
                            response = new ServiceResponse();
                        }
                        response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    }
                    else
                    {
                        response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                    }
                }
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
        /// Date 14 Sep 2011               
        /// </summary>
        /// <param name="addressBookDTO"></param>
        /// <returns></returns>
        public int GetClinicBranchIdByClinicBranchCode(string branchCode)
        {
            int ClinicBranchId = 0;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GetClinicBranchIdByClinicBranchCode Called");
                QuivusLogger.Info("GetClinicBranchIdByClinicBranchCode Param branchCode::" + branchCode);

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                ClinicBranchId = UserImpl.GetClinicBranchIdByClinicBranchCode(branchCode);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return ClinicBranchId;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011       
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>        
        public int ManageClinicUserProfile(ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId)
        {
            int updatedID = 0;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageClinicUserProfile Called");
                QuivusLogger.Info("ManageClinicUserProfile Param userTypeId::" + userTypeId + " ::clinicBranchId" + clinicBranchId);

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                updatedID = UserImpl.ManageClinicUserProfile(clinicUser, userTypeId, clinicBranchId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return updatedID;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011    
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId)
        {
            ClinicUserDTO cUserDTO = null;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService LoadClinicUserProfile Called");
                QuivusLogger.Info("LoadClinicUserProfile Param userTypeId::" + userTypeId + " ::clinicBranchId" + clinicBranchId);

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                cUserDTO = UserImpl.LoadClinicUserProfile(userId, profileId, userTypeId, clinicBranchId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return cUserDTO;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011
        /// Method Used To load Speciality Types
        /// </summary>
        /// <returns></returns>     
        public List<SpecialityTypeDTO> LoadSpecialityTypes()
        {
            List<SpecialityTypeDTO> list = null;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadSpecialityTypes Called");
                QuivusLogger.Error("LoadSpecialityTypes");

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                list = UserImpl.LoadSpecialityTypes();
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>       
        public List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId)
        {
            List<PatientProviderAssociationDTO> list = null;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ListAllPatientProvidersWithLocations Called");
                QuivusLogger.Info("ListAllPatientProvidersWithLocations");

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                list = UserImpl.ListAllPatientProvidersWithLocations(patientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return list;
        }

        public int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId)
        {
            int flag = 0;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService CheckProviderAuthorityForPatientInfoAccess Called");
                QuivusLogger.Info("CheckProviderAuthorityForPatientInfoAccess");

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                flag = UserImpl.CheckProviderAuthorityForPatientInfoAccess(providerUserId, patientId, clinicBranchId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return flag;
        }


        public int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO)
        {
            int flag = 0;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService ManageProviderAuthorization Called");
                QuivusLogger.Info("ManageProviderAuthorization");

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                flag = UserImpl.ManageProviderAuthorization(patientProviderAssociationDTO);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return flag;
        }


        public List<ProviderSearchDTO> SearchProviderForAppointment(SearchProviderCriteriaDTO criteria)
        {   
            List<ProviderSearchDTO> list = null;
            ServiceResponse response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerService SearchProviderForAppointment Called");
                QuivusLogger.Info("SearchProviderForAppointment");

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                list = UserImpl.SearchProviderForAppointment(criteria);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.AddressBookService.QuivusFault> exp)
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
            return list;
        }

    }
}