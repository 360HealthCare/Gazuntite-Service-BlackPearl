using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;


using Quivus.Model.User.Patient;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;

using Quivus.Utility.Utilities;
using UserManager.Business.Validators;

using Quivus.Model.Common;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.AddressBook;

using UserManagerService.DataAccess.Patient;

using UserManager.Business.Utility;
using Quivus.Model.User.Patient;
using Quivus.Model.User;
using Quivus.Model.User;
using Quivus.Model.User.Patient;
using QuivusModel.User.Patient;
using QuivusModel.User;
using QuivusModel.User.ClinicUser;
using QuivusModel.Common;
using QuivusModel.Association;
using Quivus.Model.QuickLink;


namespace UserManager.Business.User.Impl
{
    public class UserManagerImpl : IUserManager
    {

        UserDAO userDAO = null;
        PatientDAO patientDAO = null;



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
        public PatientProfileCompleteSettingDTO LoadPatientProfileSettings(int clinicBranchId, int providerId, int PatientId)
        {
            PatientProfileCompleteSettingDTO response = null;
            PatientProfileSettingDTO profileSettingsSectionFirst = null;
            PatientProfileSettingDTO profileSettingsSectionSecond = null;
            try
            {
                QuivusLogger.Info("LoadPatientProfileSettings Called PARAM clinicBranchId::" + clinicBranchId + "providerId:: " + providerId);

                if (providerId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (clinicBranchId < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                // Now make Call For Each Section i.e Section 1, 2 & 3

                profileSettingsSectionFirst = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_FIRST);
                profileSettingsSectionSecond = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_SECOND);

                if (profileSettingsSectionFirst != null || profileSettingsSectionSecond != null)
                {
                    if (response == null)
                    {
                        response = new PatientProfileCompleteSettingDTO();
                    }

                    response.SectionFirstData = profileSettingsSectionFirst;
                    response.SectionSecondData = profileSettingsSectionSecond;
                }

                #region Return Section third Info for Time Being By Ismail :)

                List<PatientProfileSettingDTO> ProfileSettingList = userDAO.LoadProfileSettingCollection(clinicBranchId, providerId);

                response = PopulateSectionThree(response, ProfileSettingList, PatientId);

                patientDAO.SaveRecentPatient(clinicBranchId, providerId, PatientId); // Save Into Most Recent Pateint List of provider

                /* For Testing Purpose
                 * 
                 * ProfileSettingsCollectionDTO profileSettingsSectionThird = new ProfileSettingsCollectionDTO();
                 * profileSettingsSectionThird.CollectionType = ProjectConstant.ALLERGY_COLLECTION;
                 * List<PatientAllergyInfoDTO> allergyList = new List<PatientAllergyInfoDTO>();
                 * profileSettingsSectionThird.AllergyList = allergyList;
                 * if (response == null)
                 * {
                 *  response = new PatientProfileSettingResponse();
                 *  response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                 * }
                 * response.SectionThirdData = profileSettingsSectionThird;
                */
                #endregion
            }
            catch
            {
                throw;
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
        public PatientProfileCompleteSettingDTO LoadPatientProfileSettingsBySection(int clinicBranchId, int providerId, int PatientId, int SectionId)
        {
            {
                PatientProfileCompleteSettingDTO response = null;
                PatientProfileSettingDTO profileSettingsSectionFirst = null;
                PatientProfileSettingDTO profileSettingsSectionSecond = null;
                try
                {
                    QuivusLogger.Info("LoadPatientProfileSettingsBySection Called PARAM clinicBranchId::" + clinicBranchId + "providerId:: " + providerId
                        + "PatientId ::" + PatientId + " SectionId:: " + SectionId);

                    if (providerId <= 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                    }
                    else if (clinicBranchId < 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                    }
                    else if (PatientId < 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                    }
                    else if (SectionId < 0)
                    {
                        throw new InvalidArgumentException(ProjectConstant.SECTION_ID_INVALID_ERROR_MSG);
                    }

                    if (userDAO == null)
                    {
                        userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                    }

                    // Now check & make Call For appropriate  Section i.e either Section 1, 2 & 3
                    if (SectionId.Equals(ProjectConstant.SECTION_FIRST))
                    {
                        profileSettingsSectionFirst = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_FIRST);
                    }
                    if (SectionId.Equals(ProjectConstant.SECTION_SECOND))
                    {
                        profileSettingsSectionSecond = userDAO.LoadPatientProfileSectionSettings(clinicBranchId, providerId, PatientId, ProjectConstant.SECTION_SECOND);
                    }

                    if (profileSettingsSectionFirst != null || profileSettingsSectionSecond != null)
                    {
                        if (response == null)
                        {
                            response = new PatientProfileCompleteSettingDTO();
                        }

                        response.SectionFirstData = profileSettingsSectionFirst;
                        response.SectionSecondData = profileSettingsSectionSecond;


                    }
                    #region Return Section third Info  By Ismail :)
                    if (SectionId.Equals(ProjectConstant.SECTION_THIRD))
                    {
                        List<PatientProfileSettingDTO> ProfileSettingList = userDAO.LoadProfileSettingCollection(clinicBranchId, providerId);
                        response = PopulateSectionThree(response, ProfileSettingList, PatientId);
                    }
                    #endregion
                }

                catch
                {
                    throw;
                }

                return response;
            }
        }


        /// <summary>
        /// Author muhammad ismail
        /// Date Feb 04 2011
        /// Method Check UserProfileCategoryName & on UserProfileCategoryName basis load appropriate collection
        /// i.e Allergy or diagnosis or immunization or insurance
        /// </summary>
        /// <param name="response"></param>
        /// <param name="ProfileSettingList"></param>
        /// <returns></returns>
        private PatientProfileCompleteSettingDTO PopulateSectionThree(PatientProfileCompleteSettingDTO response, List<PatientProfileSettingDTO> ProfileSettingList, int patientId)
        {
            ProfileSettingsCollectionDTO profileSettingsSectionThird = new ProfileSettingsCollectionDTO();

            if (ProfileSettingList != null && ProfileSettingList.Count > 0)
            {
                var query =
                 from d in ProfileSettingList
                 where d.SectionNo.Equals(ProjectConstant.SECTION_THIRD)
                 select d;

                if (query != null && query.ToArray().Length > 0)
                {
                    PatientProfileSettingDTO SectionThird = query.First();

                    if (SectionThird != null)
                    {
                        if (response == null)
                        {
                            response = new PatientProfileCompleteSettingDTO();
                        }

                        String catgeoryName = SectionThird.UserProfileCategoryName;
                        profileSettingsSectionThird.CollectionType = catgeoryName;
                        profileSettingsSectionThird.UserUserProfileCategoryID = ProfileSettingList[0].UserUserProfileCategoryID; // Need to To Be Discuss With Atta We are getting mutliple records

                        if (patientDAO == null)
                        {
                            patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                        }

                        if (catgeoryName.Equals(ProjectConstant.ALLERGY_COLLECTION))
                        {
                            profileSettingsSectionThird.AllergyList = patientDAO.LoadPatientAllergiesByPatientId(patientId);
                        }
                        if (catgeoryName.Equals(ProjectConstant.DIAGNOSIS_COLLECTION))
                        {
                            profileSettingsSectionThird.DiagnosisList = patientDAO.LoadPatientDiagnosisByPatientId(patientId);
                        }
                        if (catgeoryName.Equals(ProjectConstant.IMMUNIZATION_COLLECTION))
                        {
                            profileSettingsSectionThird.ImmunizationList = patientDAO.LoadPatientImmunizationByPatientId(patientId);
                        }
                        if (catgeoryName.Equals(ProjectConstant.CHRONICMEDICATION_COLLECTION))
                        {
                            profileSettingsSectionThird.ChronicMedicationList = patientDAO.LoadPatientMedicationByPatientId(patientId);
                        }

                        if (catgeoryName.Equals(ProjectConstant.INSURANCE_COLLECTION))
                        {
                            profileSettingsSectionThird.InsuranceList = patientDAO.LoadPatientInsuranceForProvider(patientId);
                        }

                        //M 07 Feb 2011
                        //response.SectionThirdData = new ProfileSettingsCollectionDTO();
                        //response.SectionThirdData.UserUserProfileCategoryID = SectionThird.UserUserProfileCategoryID; // commented By Ismail not in use

                        response.SectionThirdData = profileSettingsSectionThird;
                    }
                }
            }
            else
            {
                if (response == null)
                {
                    response = new PatientProfileCompleteSettingDTO();
                }

                profileSettingsSectionThird.AllergyList = null;
                profileSettingsSectionThird.CollectionType = ProjectConstant.ALLERGY_COLLECTION;
                response.SectionThirdData = profileSettingsSectionThird;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// This Method Will be used to Update Patient Profile Settings 
        /// </summary>
        /// <param name="ProfileSettingObj"></param>

        /// <returns></returns>
        public int ManagePatientProfileSettings(PatientProfileSettingDTO ProfileSettingObj)
        {
            int UpdateResult = 0;

            try
            {
                QuivusLogger.Info("ManagePatientProfileSettings Called PARAM ProfileSettingObj::" + ProfileSettingObj);

                StringBuilder sb = new StringBuilder();
                if (QUserValidator.ValidatePatientSettingInfo(ProfileSettingObj, ref sb) == false)
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                UpdateResult = userDAO.ManagePatientProfileSettings(ProfileSettingObj);
            }
            catch
            {
                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 01 Feb 2011
        /// Method Used To Load Profile Static Information
        /// </summary>
        /// <returns></returns>
        public List<ProfileStaticDataDTO> LoadProfileStaticData()
        {
            List<ProfileStaticDataDTO> profileSettingsList = null;
            try
            {
                QuivusLogger.Info("LoadProfileStaticData Called PARAM clinicBranchId::");
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                profileSettingsList = userDAO.LoadProfileStaticData();
            }
            catch
            {
                throw;
            }

            return profileSettingsList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 3 Feb 2011
        /// load the user email id by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>        
        public String GetUserEmailByUserId(int userId)
        {
            String response = null;
            string userEmailID = "";

            try
            {
                QuivusLogger.Info("GetUserEmailByUserId Called PARAM userId::");

                if (userId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                userEmailID = userDAO.GetUserEmailByUserId(userId);
                response = userEmailID;
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get the RecGuid of respective table, its a internal call used to get guid before saving Address Book
        /// </summary>        
        /// <param name="roleProfileID"></param>
        /// <param name="addressPartyTypeID"></param> 
        public Guid GetPartyGuidByProfileIdAndTypeId(int roleProfileID, int addressPartyTypeID)
        {
            Guid guid = System.Guid.Empty;

            try
            {
                QuivusLogger.Info("calling GetPartyGuid PARAM :: roleProfileID " + roleProfileID + " addressPartyTypeID " + addressPartyTypeID);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                if (addressPartyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.Patient)
                {
                    guid = userDAO.GetPatientGuidByPatientId(roleProfileID);
                }
                else if (addressPartyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor)
                {
                    guid = userDAO.GetGurantorGuidByGurantorId(roleProfileID);
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw;
            }
            return guid;
        }

        public List<UserBasicInfoDTO> GetUserBasicInfoById(List<int> ProviderIdList)
        {
            List<UserBasicInfoDTO> response = null;

            try
            {
                QuivusLogger.Info("GetUserBasicInfoById Called PARAM ProviderIdList::" + ProviderIdList);

                if (ProviderIdList == null)
                {
                    throw new InvalidArgumentException(ProjectConstant.EMPTY_LIST);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                response = userDAO.GetUserBasicInfoById(ProviderIdList);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// tell if a user is enrolled as provider on a specific branch
        /// </summary> 
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public bool IsProviderAtClinic(int userId, int clinicBranchId)
        {
            bool isProvider = false;

            try
            {
                QuivusLogger.Info("calling IsProviderAtClinic PARAM :: userId " + userId + " clinicBranchId " + clinicBranchId);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                isProvider = userDAO.IsProviderAtClinic(userId, clinicBranchId);
            }
            catch
            {
                throw;
            }
            return isProvider;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 28 Feb 2011
        /// get the RecGuid of respective table, used to load the patient profile customized setting (AB)
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partyTypeID"></param>
        /// <returns></returns>        
        public Guid GetPartyGuidByPatientIdAndPartyTypeId(int patientId, int partyTypeID)
        {
            Guid guid = System.Guid.Empty;

            try
            {
                QuivusLogger.Info("calling GetPartyGuid PARAM :: patientId " + patientId + " partyTypeID " + partyTypeID);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                if (partyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.EmergencyContact)
                {
                    guid = userDAO.GetEmergencyContactGuidByPatientId(patientId);
                }
                else if (partyTypeID == (int)ProjectConstant.ADDRESS_PARTY_TYPE.PatientGuarantor)
                {
                    guid = userDAO.GetGurantorGuidByPatientId(patientId);
                }
            }
            catch
            {
                throw;
            }
            return guid;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Date 29 march 2011
        /// Method WIll be use to verify USer password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public UserVerificationDTO VerifyUser(int UserId, string password)
        {
            UserVerificationDTO Result = null;

            try
            {
                QuivusLogger.Info("calling IsProviderAtClinic PARAM :: userId " + UserId);

                if (userDAO == null)
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));

                if (UserId < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                if (string.IsNullOrEmpty(password))
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_PASSWORD_INVALID_ERROR_MSG);
                }

                Result = userDAO.VerifyUser(UserId, password);
            }
            catch
            {

                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 2 2011
        /// Method Will BE used to load Base User Info By User Id :)
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public BaseUserDTO LoadBaseUserByUserId(int UserId)
        {
            BaseUserDTO response = null;

            try
            {
                QuivusLogger.Info("LoadBaseUserByUserId Called PARAM userId::" + UserId);

                if (UserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.LoadBaseUserByUserId(UserId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 10 2011
        /// Method Will BE used to update e-prescription details :)
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="RCopiaID"></param>
        /// <returns></returns>
        public bool UpdatePatientEPrescriptionDetails(String RCopiaID, int PatientId)
        {
            bool response = false;

            try
            {
                QuivusLogger.Info("LoadBaseUserByUserId Called PARAM userId::");

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.UpdatePatientEPrescriptionDetails(RCopiaID, PatientId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 26 2011
        /// Method Will BE used to load quivus id Info By patient Id :)
        /// Patient_QuivudIDLoadByPatientID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>

        public string LoadQuivusIdByPatientId(int PatientId)
        {
            string response = string.Empty;

            try
            {
                QuivusLogger.Info("LoadQuivusIdByPatientId Called PARAM PatientId::" + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.LoadQuivusIdByPatientId(PatientId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date May 26 2011
        /// Method Will BE used to load rcopia id Info By patient Id :)
        /// Patient_QuivudIDLoadByPatientID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>

        public string LoadRcopiaIdByPatientId(int PatientId)
        {
            string response = string.Empty;

            try
            {
                QuivusLogger.Info("LoadRcopiaIdByPatientId Called PARAM PatientId::" + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.LoadRcopiaIdByPatientId(PatientId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ExternalPatientDTO LoadExternalPatients(int FromRecord, int ToRecord)
        {
            ExternalPatientDTO response = null;

            try
            {
                QuivusLogger.Info("LoadRcopiaIdByPatientId Called PARAM FromRecord::" + FromRecord);

                if (FromRecord <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_FROM_RECORD_INVALID_ERROR_MSG);
                }
                if (ToRecord <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_TO_RECORD_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.LoadExternalPatients(FromRecord, ToRecord);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 05 july 2011
        /// Method Will BE used to update external patients :)
        /// </summary>
        /// <returns></returns>
        public bool UpdatePatientRCopiaId(List<PatientRcopiaDTO> patientList)
        {
            bool response = false;

            try
            {
                QuivusLogger.Info("UpdatePatientRCopiaId Called PARAM UpdatePatientRCopiaId::");

                //if (patientList ==null )
                //{
                //    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                //}

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.UpdatePatientRCopiaId(patientList);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 18/7/2011
        /// Method will return provider List
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID)
        {
            List<ProviderDTO> providerList = null;
            try
            {
                QuivusLogger.Info("LoadAllProviderByClinicBranchId Called PARAM clinicBranchID::" + clinicBranchID);

                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);

                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                providerList = userDAO.LoadAllProviderByClinicBranchId(clinicBranchID);

            }
            catch
            {
                throw;
            }
            return providerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, basic user info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public BaseUserDTO LoadPatientBaseUserInfoByPatientIdForCompleteProfile(int patientId)
        {
            BaseUserDTO Usr = null;
            try
            {
                QuivusLogger.Info("LoadAllProviderByClinicBranchId Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);

                }
                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                Usr = patientDAO.LoadPatientBaseUserInfoByPatientIdForCompleteProfile(patientId);

            }
            catch
            {
                throw;
            }
            return Usr;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 08 Feb 2011
        /// load the patient, allergy info
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>    
        public List<PatientAllergyDTO> LoadPatientAllergyInfoByPatientIdForCompleteProfile(int patientId)
        {
            List<PatientAllergyDTO> providerList = null;
            try
            {
                QuivusLogger.Info("LoadPatientAllergyInfoByPatientIdForCompleteProfile Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                if (patientDAO == null)
                {
                    patientDAO = (PatientDAO)DAOFactory.GetDataAccessObject(typeof(PatientDAO));
                }
                providerList = patientDAO.LoadPatientAllergyInfoByPatientIdForCompleteProfile(patientId);

            }
            catch
            {
                throw;
            }
            return providerList;
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
            bool flag = false;
            try
            {
                QuivusLogger.Info("VerifyUserEmail Called PARAM email::" + email);

                //M
                //if (patientId <= 0)
                //{
                //    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                //}
                if (patientDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                flag = userDAO.VerifyUserEmail(email);
            }
            catch
            {
                throw;
            }
            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 sep 2011        
        /// </summary>
        /// <param name="branchCode"></param>
        /// <returns></returns>        
        public int GetClinicBranchIdByClinicBranchCode(string branchCode)
        {
            int brID = 0;
            try
            {
                QuivusLogger.Info("GetClinicBranchIdByClinicBranchCode Called PARAM branchCode::" + branchCode);

                if (patientDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                brID = userDAO.GetClinicBranchIdByClinicBranchCode(branchCode);
            }
            catch
            {
                throw;
            }
            return brID;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<ProviderDTO> LoadAllProviderByClinicBranchId(int clinicBranchID, List<int> stationIds, bool isLoadAll)
        {
            List<ProviderDTO> providerList = null;
            try
            {
                QuivusLogger.Info("LoadAllProviderByClinicBranchId Called PARAM clinicBranchID::" + clinicBranchID);

                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);

                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                providerList = userDAO.LoadAllProviderByClinicBranchId(clinicBranchID, stationIds, isLoadAll);

            }
            catch
            {
                throw;
            }
            return providerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public List<StationDTO> LoadAllStationsByClinicId(int clinicBranchID)
        {
            List<StationDTO> stationList = null;
            try
            {
                QuivusLogger.Info("LoadAllStationsByClinicId Called PARAM clinicBranchID::" + clinicBranchID);

                if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);

                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                stationList = userDAO.LoadAllStationsByClinicId(clinicBranchID);
            }
            catch
            {
                throw;
            }
            return stationList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Oct 2011
        /// Method will return provider List, if isLoadAll = true then load only by clinic 
        /// </summary>
        /// <param name="ClinicBranchID"></param>
        /// <returns></returns>
        public WorkingHourDTO LoadProviderWorkingHours(int providerUserId)
        {
            WorkingHourDTO workingHour = null;
            try
            {
                QuivusLogger.Info("LoadProviderWorkingHours Called PARAM providerUserId::" + providerUserId);

                if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                workingHour = userDAO.LoadProviderWorkingHours(providerUserId);
            }
            catch
            {
                throw;
            }
            return workingHour;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public List<PatientProviderAssociationDTO> ListAllPatientProvidersWithLocations(int patientId)
        {
            List<PatientProviderAssociationDTO> list = null;
            try
            {
                QuivusLogger.Info("ListAllPatientProvidersWithLocations Called PARAM patientId::" + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                list = userDAO.ListAllPatientProvidersWithLocations(patientId);
            }
            catch
            {
                throw;
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public int CheckProviderAuthorityForPatientInfoAccess(int providerUserId, int patientId, int clinicBranchId)
        {
            int flag = 0;
            try
            {
                QuivusLogger.Info("CheckProviderAuthorityForPatientInfoAccess Called PARAM providerUserId::" + providerUserId + "patientId::" + patientId + "::clinicBranchId" + clinicBranchId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG);
                }
                if (providerUserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                if (clinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                flag = userDAO.CheckProviderAuthorityForPatientInfoAccess(providerUserId, patientId, clinicBranchId);
            }
            catch
            {
                throw;
            }
            return flag;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 31 Oct 2011        
        /// </summary>        
        /// <returns></returns>
        public int ManageProviderAuthorization(PatientProviderAssociationDTO patientProviderAssociationDTO)
        {
            int flag = 0;
            try
            {
                QuivusLogger.Info("ManageProviderAuthorization Called PARAM");

                //if (providerUserId <= 0)
                //{
                //    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                //}
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                flag = userDAO.ManageProviderAuthorization(patientProviderAssociationDTO);
            }
            catch
            {
                throw;
            }
            return flag;
        }


        public List<ProviderSearchDTO> SearchProviderForAppointment(SearchProviderCriteriaDTO criteria)
        {
            List<ProviderSearchDTO> list = null;
            try
            {
                QuivusLogger.Info("SearchProviderForAppointment Called PARAM");

                //if (providerUserId <= 0)
                //{
                //    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                //}
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                list = userDAO.SearchProviderForAppointment();

                //Here LINQ

                ProviderSearchDTO dto = null;
                if (!String.IsNullOrEmpty(criteria.City) || !String.IsNullOrEmpty(criteria.State) || !String.IsNullOrEmpty(criteria.Zipcode))
                {
                    SearchDoctoerWithAddress(criteria, ref list, ref dto);
                }
                else
                {
                    SearchDoctoerWithOutAddress(criteria, ref list, ref dto);
                }

            }
            catch
            {
                throw;
            }
            return list;
        }

        private void SearchDoctoerWithAddress(SearchProviderCriteriaDTO Criteria, ref List<ProviderSearchDTO> list, ref ProviderSearchDTO dto)
        {
            var query = from t in list
                        select t;

            //var query = from doctor in user_qv_doctor
            //            join user in qv_user on
            //            doctor.qv_user.UserId equals user.UserId
            //            join speciality in type_speciality_type on
            //            doctor.type_speciality_type.SPECIALITY_ID_SEQ equals speciality.SPECIALITY_ID_SEQ
            //            join clinicUser in qv_clinic_user on
            //            doctor.qv_user.UserId equals clinicUser.qv_user.UserId
            //            join clinicBranch in qv_clinic_branch on
            //            clinicUser.qv_clinic_branch.CLINIC_BRANCH_ID_SEQ equals clinicBranch.CLINIC_BRANCH_ID_SEQ
            //            join addressBook in AddressBooks on
            //            doctor.RecGuid equals addressBook.PartyGuid
            //            join address in Addresses on
            //            addressBook.AddressBookID equals address.AddressBook.AddressBookID
            //            join zipCode in ZipCodes on
            //            address.ZipCode.ZipCodeID equals zipCode.ZipCodeID
            //            join state in States on
            //            zipCode.State.StateID equals state.StateID
            //            where (clinicUser.USER_TYPE_ID.Equals((int)Constants.UserType.Doctor))
            //            select new
            //            {
            //                doctor,
            //                user,
            //                speciality,
            //                zipCode,
            //                state,
            //                clinicBranch
            //            };


            if (!String.IsNullOrEmpty(Criteria.FirstName))
            {
                query = query.Where(q => q.ProviderName.ToLower().Contains(Criteria.FirstName.ToLower()));
            }
            if (!String.IsNullOrEmpty(Criteria.LastName))
            {
                query = query.Where(q => q.ProviderName.ToLower().Contains(Criteria.LastName.ToLower()));
            }
            if (Criteria.SpecialityTypeId > 0)
            {
                query = query.Where(q => q.SpecialityId.Equals(Criteria.SpecialityTypeId));
            }
            if (!String.IsNullOrEmpty(Criteria.LocationName))
            {
                query = query.Where(q => q.StationName.ToLower().Contains(Criteria.LocationName.ToLower()));
            }
            if (!String.IsNullOrEmpty(Criteria.City))
            {
                query = query.Where(q => q.City.ToLower().Equals(Criteria.City.ToLower()));
            }
            if (!String.IsNullOrEmpty(Criteria.Zipcode))
            {
                query = query.Where(q => q.Zip.ToLower().Equals(Criteria.Zipcode.ToLower()));
            }
            if (!String.IsNullOrEmpty(Criteria.State))
            {
                query = query.Where(q => q.State.ToLower().Equals(Criteria.State.ToLower()));
            }

            List<ProviderSearchDTO> listNew = new List<ProviderSearchDTO>();

            if (query != null && query.Count() > 0)
            {
                foreach (var val in query)
                {
                    ProviderSearchDTO dtoNew = new ProviderSearchDTO();

                    dtoNew = (ProviderSearchDTO)val;

                    listNew.Add(dtoNew);

                }
            }

            list = listNew;

        }

        private void SearchDoctoerWithOutAddress(SearchProviderCriteriaDTO Criteria, ref List<ProviderSearchDTO> list, ref ProviderSearchDTO dto)
        {
            //var query = from doctor in user_qv_doctor
            //            join user in qv_user on
            //            doctor.qv_user.UserId equals user.UserId
            //            join speciality in type_speciality_type on
            //            doctor.type_speciality_type.SPECIALITY_ID_SEQ equals speciality.SPECIALITY_ID_SEQ
            //            join clinicUser in qv_clinic_user on
            //            doctor.qv_user.UserId equals clinicUser.qv_user.UserId
            //            join clinicBranch in qv_clinic_branch on
            //            clinicUser.qv_clinic_branch.CLINIC_BRANCH_ID_SEQ equals clinicBranch.CLINIC_BRANCH_ID_SEQ

            //            where (clinicUser.USER_TYPE_ID.Equals((int)Constants.UserType.Doctor))
            //            select new
            //            {
            //                doctor,
            //                user,
            //                speciality,

            //                clinicBranch
            //            };

            var query = from t in list
                        select t;


            if (!String.IsNullOrEmpty(Criteria.FirstName))
            {
                query = query.Where(q => q.ProviderName.ToLower().Contains(Criteria.FirstName.ToLower()));
            }
            if (!String.IsNullOrEmpty(Criteria.LastName))
            {
                query = query.Where(q => q.ProviderName.ToLower().Contains(Criteria.LastName.ToLower()));
            }
            if (Criteria.SpecialityTypeId > 0)
            {
                query = query.Where(q => q.SpecialityId.Equals(Criteria.SpecialityTypeId));
            }
            if (!String.IsNullOrEmpty(Criteria.LocationName))
            {
                query = query.Where(q => q.SpecialityName.ToLower().Contains(Criteria.LocationName.ToLower()));
            }

            List<ProviderSearchDTO> listNew = new List<ProviderSearchDTO>();

            if (query != null && query.Count() > 0)
            {

                foreach (var val in query)
                {
                    ProviderSearchDTO dtoNew = new ProviderSearchDTO();

                    dtoNew = (ProviderSearchDTO)val;

                    listNew.Add(dtoNew);

                }
            }

            list = listNew;

        }

        /// <summary>
        /// Author Sahibzada Sami uddin
        /// Date 25/11/2011
        /// This method will Get Station Names By StationID
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<StationDTO> GetStationNamesByStationID(List<int> stationIDList)
        {
            List<StationDTO> list = null;
            try
            {

                QuivusLogger.Info("GetStationNamesByStationID Called PARAM" + stationIDList);

                if (stationIDList != null && stationIDList.Count > 0)
                {
                    if (userDAO == null)
                    {
                        userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                    }
                    list = userDAO.GetStationNamesByStationID(stationIDList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.EMPTY_STATION_LIST);
                }

            }
            catch
            {
                throw;
            }
            return list;
        }





        public int UpdateUserStatus(int userId, string update, bool status)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("UpdateClinicUserStatus Called PARAM userId::" + userId + " :: update" + update + " ::status" + status);


                if (userId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }


                if (update == null || update == "" || update.Length == 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_OPERATION);
                }

                if (update != ProjectConstant.LOCKED_STATUS && update != ProjectConstant.ACTIVE_STATUS)
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_OPERATION);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                if (update == ProjectConstant.LOCKED_STATUS)
                {
                    result = userDAO.ChangeUserLockedStatus(userId, status);
                }
                else if (update == ProjectConstant.ACTIVE_STATUS)
                {
                    result = userDAO.ChangeUserActiveStatus(userId, status);
                }




            }
            catch
            {
                throw;
            }

            return result;
        }

        public int InsertUserQuickLinks(List<QuickLinkDTO> list)
        {
            int result = 0;
            try
            {
                if (list != null)
                {
                    if (userDAO == null)
                    {
                        userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        QuickLinkDTO dto = list[i] as QuickLinkDTO;

                        userDAO.InsertUserQuickLinks(dto);
                    }

                }

            }
            catch
            {
                throw;
            }

            return result;
        }
        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012</summary>
        /// This call will load patient user basic info like name email using thier patient Ids :)
        /// <param name="ProviderIdList"></param>
        /// <returns></returns>
        public List<PatientBasicUserDTO> GetUserBasicInfoByPatientIds(List<int> PatientIdList)
        {
            List<PatientBasicUserDTO> response = null;

            try
            {
                QuivusLogger.Info("GetUserBasicInfoByPatientIds Called PARAM PatientIdList::" + PatientIdList);

                if (PatientIdList == null)
                {
                    throw new InvalidArgumentException(ProjectConstant.EMPTY_LIST);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                response = userDAO.GetUserBasicInfoByPatientIds(PatientIdList);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
