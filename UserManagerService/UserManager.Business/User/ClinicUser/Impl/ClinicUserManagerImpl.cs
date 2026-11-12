using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility;
using QuivusModel.User.ClinicUser;
using Quivus.Utility.CustomException;
using Quivus.Utility.Factory;
using Quivus.Utility.Logger;
using UserManager.Business.User.ClinicUser.Doctor.Impl;
using UserManager.Business.User.Impl;
using UserManager.Business.User.ClinicUser.NursePractioner;
using UserManager.Business.User.ClinicUser.Nurse;
using UserManager.Business.User.ClinicUser.LabTech;
using UserManager.Business.User.ClinicUser.Receptionist;
using UserManager.Business.User.ClinicUser.PracticeManager;
using UserManager.Business.User.ClinicUser.BillingAssistent;
using UserManager.Business.User.ClinicUser.SecurityAdmin;
using UserManager.Business.User.ClinicUser.JrSecurityAdmin;
using UserManager.Business.User.ClinicUser.Student;
using UserManager.Business.User.ClinicUser.BillingManager;
using UserManagerService.Business.Common;
using QuivusModel.TypeDTOs;
using Quivus.Model.QuickLink;
using UserManagerService.Business.SearchEngine;
using UserManagerService.Business.SearchEngine.Impl;
using Quivus.Model.User.Patient;
using QuivusModel.Search;
using UserManagerService.DataAccess;

namespace UserManager.Business.User.ClinicUser.Impl
{
    public class ClinicUserManagerImpl : IClinicUserManager
    {
        #region Clinical Users managers

        DoctorManager doctorManager = null;

        NursePractionerManager npManager = null;

        NurseManager nurseManager = null;

        LabTechManager ltManager = null;

        ReceptionistManager rManager = null;

        PracticeMManager pManager = null;

        BillingAssistentManager baManager = null;

        SecurityAdminManager saManager = null;

        JrSecurityAdminManager jrManager = null;

        StudentManager studentManager = null;

        BillingMManager bManager = null;

        #endregion

        StaticDataManager sdManager = null;

        ISearchEngineManager searchEngineManager = null;

        UserManagerImpl userManager = null;

        UserDAO userDAO = null;

        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>     
        public int ManageClinicUserProfile(ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId)
        {
            int profileId = 0;
            bool registrationCase = true;
            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageClinicUserProfile Called PARAM userTypeId::" + userTypeId + ":: clinicBranchId" + clinicBranchId);

                if (userTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_ID_INVALID_ERROR_MSG);
                }

                if (clinicUser == null)
                {
                    throw new InvalidArgumentException(ProjectConstant.NULL_CLINIC_USER_DTO_RECEIVED);
                }


                #region Doctor
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Doctor)
                {
                    if (clinicUser.DoctorDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }


                        if (doctorManager == null)
                        {
                            doctorManager = new DoctorManager();
                        }
                        if (clinicUser.DoctorDTO.DoctorId > 0)
                            registrationCase = false;

                        profileId = doctorManager.ManageDoctorProfile(clinicUser.UserDTO.UserId, clinicUser.DoctorDTO);

                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_DOCTOR_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype doctor end
                #endregion

                #region NursePractitioner
                if (userTypeId == (int)ProjectConstant.USER_TYPE.NursePractitioner)
                {
                    if (clinicUser.NursePractitionerDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (npManager == null)
                        {
                            npManager = new NursePractionerManager();
                        }

                        if (clinicUser.NursePractitionerDTO.NursePractitionerId > 0)
                            registrationCase = false;


                        profileId = npManager.ManageNursePractionerProfile(clinicUser.UserDTO.UserId, clinicUser.NursePractitionerDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_NURSEPRACTIOER_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region Nurse
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Nurse)
                {
                    if (clinicUser.NurseDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (npManager == null)
                        {
                            nurseManager = new NurseManager();
                        }

                        if (clinicUser.NurseDTO.NURSE_ID_SEQ > 0)
                            registrationCase = false;

                        profileId = nurseManager.ManageNurseProfile(clinicUser.UserDTO.UserId, clinicUser.NurseDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_NURSE_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region LabTech
                if (userTypeId == (int)ProjectConstant.USER_TYPE.LabTechnician)
                {
                    if (clinicUser.LabTechnicianDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (npManager == null)
                        {
                            ltManager = new LabTechManager();
                        }

                        if (clinicUser.LabTechnicianDTO.LabTechnicianId > 0)
                            registrationCase = false;

                        profileId = ltManager.ManageLabTechProfile(clinicUser.UserDTO.UserId, clinicUser.LabTechnicianDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_LAB_TECH_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region Receptionist
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Receptionist)
                {
                    if (clinicUser.ReceptionistDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (rManager == null)
                        {
                            rManager = new ReceptionistManager();
                        }

                        if (clinicUser.ReceptionistDTO.ReceptionistId > 0)
                            registrationCase = false;

                        profileId = rManager.ManageReceptionistProfile(clinicUser.UserDTO.UserId, clinicUser.ReceptionistDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_RECEPTIONIST_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region Practice Manager
                if (userTypeId == (int)ProjectConstant.USER_TYPE.PracticeManager)
                {
                    if (clinicUser.PracticeManagerDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (pManager == null)
                        {
                            pManager = new PracticeMManager();
                        }

                        if (clinicUser.PracticeManagerDTO.PracticeManagerId > 0)
                            registrationCase = false;

                        profileId = pManager.ManagePracticeManagerProfile(clinicUser.UserDTO.UserId, clinicUser.PracticeManagerDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_PRACTICE_MANAGER_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion


                #region Billing Assistent
                if (userTypeId == (int)ProjectConstant.USER_TYPE.BillingAssistant)
                {
                    if (clinicUser.BillingAssistantDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (baManager == null)
                        {
                            baManager = new BillingAssistentManager();
                        }

                        if (clinicUser.BillingAssistantDTO.BillingAssistantId > 0)
                            registrationCase = false;


                        profileId = baManager.ManageBillingAssistentProfile(clinicUser.UserDTO.UserId, clinicUser.BillingAssistantDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_BillingASS_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion


                #region Security Admin
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Admin)
                {
                    if (clinicUser.SecurityAdminDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (saManager == null)
                        {
                            saManager = new SecurityAdminManager();
                        }

                        if (clinicUser.SecurityAdminDTO.SecurityAdminId > 0)
                            registrationCase = false;

                        profileId = saManager.ManageSecurityAdminManagerProfile(clinicUser.UserDTO.UserId, clinicUser.SecurityAdminDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_SECURITY_ADMIN_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region Jr Security Admin
                if (userTypeId == (int)ProjectConstant.USER_TYPE.JnrSecurityAdmin)
                {
                    if (clinicUser.JnrSecurityAdminDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (jrManager == null)
                        {
                            jrManager = new JrSecurityAdminManager();
                        }

                        if (clinicUser.JnrSecurityAdminDTO.JrSecurityAdminId > 0)
                            registrationCase = false;

                        profileId = jrManager.ManageJrSecurityAdminProfile(clinicUser.UserDTO.UserId, clinicUser.JnrSecurityAdminDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_JrSECURITY_ADMIN_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region Student
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Student)
                {
                    if (clinicUser.StudentDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (studentManager == null)
                        {
                            studentManager = new StudentManager();
                        }


                        if (clinicUser.StudentDTO.StudentIdSeq > 0)
                            registrationCase = false;


                        profileId = studentManager.ManageStudentProfile(clinicUser.UserDTO.UserId, clinicUser.StudentDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_STUDENT_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion

                #region Billing Manager
                if (userTypeId == (int)ProjectConstant.USER_TYPE.BillingManager)
                {
                    if (clinicUser.BillingManagerDTO != null && clinicUser.UserDTO != null)
                    {

                        if (clinicUser.UserDTO.UserId <= 0)
                        {
                            throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                        }

                        if (bManager == null)
                        {
                            bManager = new BillingMManager();
                        }


                        if (clinicUser.BillingManagerDTO.BillingManagerId > 0)
                            registrationCase = false;


                        profileId = bManager.ManageBillingManagerProfile(clinicUser.UserDTO.UserId, clinicUser.BillingManagerDTO);



                    }
                    else
                    {
                        throw new ArgumentException(ProjectConstant.NULL_BILLING_MANAGER_DTO_RECIEVED_WHILE_REGISTRATION);
                    }
                }//usertype np end
                #endregion


                if (registrationCase)
                {
                    InsertUserQuickLinks(clinicUser, clinicBranchId);

                    InsertUserWebSearchEngine(clinicUser, clinicBranchId);

                    InsertPatientProfileBarSetting(clinicUser, clinicBranchId);
                }


            }
            catch
            {
                throw;
            }
            return profileId;
        }

        private void InsertPatientProfileBarSetting(ClinicUserDTO clinicUser, int clinicBranchId)
        {
            if (userManager == null)
            {
                userManager = new UserManagerImpl();
            }

            PatientProfileSettingDTO settingDTO = new PatientProfileSettingDTO();
            settingDTO.UserID = clinicUser.UserDTO.UserId;
            settingDTO.ClinicBranchID = clinicBranchId;
            settingDTO.SectionNo = ProjectConstant.SECTION_FIRST;
            settingDTO.UserProfileCategoryID = 1; //Name
            settingDTO.CreatedBy = 480;

            userManager.ManagePatientProfileSettings(settingDTO);


            settingDTO.SectionNo = ProjectConstant.SECTION_SECOND;
            settingDTO.UserProfileCategoryID = 2; //DOB

            userManager.ManagePatientProfileSettings(settingDTO);

            settingDTO.SectionNo = ProjectConstant.SECTION_THIRD;
            settingDTO.UserProfileCategoryID = 31; //Allergy


            userManager.ManagePatientProfileSettings(settingDTO);


        }

        private void InsertUserWebSearchEngine(ClinicUserDTO clinicUser, int clinicBranchId)
        {
            if (sdManager == null)
            {
                sdManager = new StaticDataManager();
            }


            List<SearchEngineTypeDTO> list = sdManager.ListSystemGeneratedSearchEngineTypeDTOs();

            if (list != null)
            {

                if (searchEngineManager == null)
                {
                    searchEngineManager = new SearchEngineManagerImpl();
                }

                List<int> searchEngineIds = new List<int>();


                for (int i = 0; i < list.Count; i++)
                {
                    SearchEngineTypeDTO dto = list[i] as SearchEngineTypeDTO;
                    searchEngineIds.Add(dto.SearchEngineTypeID);
                }

                searchEngineManager.ManageUserSearchEngine(clinicUser.UserDTO.UserId, clinicBranchId, searchEngineIds, 480);

            }
        }

        private void InsertUserQuickLinks(ClinicUserDTO clinicUser, int clinicBranchId)
        {
            if (sdManager == null)
            {
                sdManager = new StaticDataManager();
            }

            List<QuickLinkTypeDTO> quickLinkTypeList = sdManager.ListDefaultQuickLinkTypeDTOs();

            if (quickLinkTypeList != null)
            {
                List<QuickLinkDTO> userList = new List<QuickLinkDTO>();

                QuickLinkDTO dto = null;

                for (int i = 0; i < quickLinkTypeList.Count; i++)
                {
                    QuickLinkTypeDTO typeDTO = quickLinkTypeList[i] as QuickLinkTypeDTO;

                    dto = new QuickLinkDTO();
                    dto.UserId = clinicUser.UserDTO.UserId;
                    dto.ClinicBranchId = clinicBranchId;
                    dto.QuickLinkTypeID = typeDTO.QuickLinkTypeID;
                    dto.CreatedBy = 480; // this is defult because if change then web needsto be changed as well, change in future
                    dto.ListOrder = typeDTO.QuickLinkTypeID;

                    userList.Add(dto);
                }

                if (userManager == null)
                {
                    userManager = new UserManagerImpl();
                }

                userManager.InsertUserQuickLinks(userList);

            }
        }

        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        public ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId)
        {
            ClinicUserDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadClinicUserProfile Called PARAM profileId::" + profileId + " ::userTypeId" + userTypeId + ":: clinicBranchId" + clinicBranchId);


                if (profileId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PROFILE_ID_INVALID_ERROR_MSG);
                }

                if (userId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }

                if (userTypeId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_ID_INVALID_ERROR_MSG);
                }

                if (userManager == null)
                {
                    userManager = new UserManagerImpl();
                }



                #region Doctor
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Doctor)
                {


                    if (doctorManager == null)
                    {
                        doctorManager = new DoctorManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.DoctorDTO = doctorManager.LoadDoctorByDoctorId(profileId);

                }//usertype doctor end
                #endregion

                #region NursePractitioner
                if (userTypeId == (int)ProjectConstant.USER_TYPE.NursePractitioner)
                {


                    if (npManager == null)
                    {
                        npManager = new NursePractionerManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.NursePractitionerDTO = npManager.LoadNursePractitionerDTOById(profileId);


                }//usertype np end
                #endregion

                #region Nurse
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Nurse)
                {


                    if (nurseManager == null)
                    {
                        nurseManager = new NurseManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.NurseDTO = nurseManager.LoadNurseDTOById(profileId);


                }//usertype np end
                #endregion

                #region Lab Technician
                if (userTypeId == (int)ProjectConstant.USER_TYPE.LabTechnician)
                {


                    if (ltManager == null)
                    {
                        ltManager = new LabTechManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.LabTechnicianDTO = ltManager.LoadLabTechnicianDTOById(profileId);

                }//usertype np end
                #endregion

                #region Receptionist
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Receptionist)
                {


                    if (rManager == null)
                    {
                        rManager = new ReceptionistManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.ReceptionistDTO = rManager.LoadReceptionistDTOById(profileId);

                }//usertype np end
                #endregion

                #region Practice Manager
                if (userTypeId == (int)ProjectConstant.USER_TYPE.PracticeManager)
                {


                    if (pManager == null)
                    {
                        pManager = new PracticeMManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.PracticeManagerDTO = pManager.LoadPracticeManagerDTOById(profileId);

                }//usertype np end
                #endregion

                #region Billing Assistant
                if (userTypeId == (int)ProjectConstant.USER_TYPE.BillingAssistant)
                {


                    if (baManager == null)
                    {
                        baManager = new BillingAssistentManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.BillingAssistantDTO = baManager.LoadBillingAssistantDTOById(profileId);

                }//usertype np end
                #endregion

                #region Security Admin
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Admin)
                {


                    if (saManager == null)
                    {
                        saManager = new SecurityAdminManager();
                    }



                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.SecurityAdminDTO = saManager.LoadSecurityAdminDTOById(profileId);

                }//usertype np end
                #endregion

                #region Jr Security Admin
                if (userTypeId == (int)ProjectConstant.USER_TYPE.JnrSecurityAdmin)
                {




                    if (jrManager == null)
                    {
                        jrManager = new JrSecurityAdminManager();
                    }

                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.JnrSecurityAdminDTO = jrManager.LoadJnrSecurityAdminDTOById(profileId);

                }//usertype np end
                #endregion

                #region Student
                if (userTypeId == (int)ProjectConstant.USER_TYPE.Student)
                {




                    if (studentManager == null)
                    {
                        studentManager = new StudentManager();
                    }

                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.StudentDTO = studentManager.LoadStudentDTOById(profileId);

                }//usertype np end
                #endregion

                #region Billing MAnager
                if (userTypeId == (int)ProjectConstant.USER_TYPE.BillingManager)
                {




                    if (bManager == null)
                    {
                        bManager = new BillingMManager();
                    }

                    profile = new ClinicUserDTO();

                    profile.UserDTO = userManager.LoadBaseUserByUserId(userId);

                    profile.BillingManagerDTO = bManager.LoadBillingManagerDTOById(profileId);

                }//usertype np end
                #endregion
            }
            catch
            {
                throw;
            }
            return profile;
        }


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 17 Jan 2012
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public ClinicProviderSearchDTO LoadClinicUserBascInfo(int userId, int clinicBranchId)
        {
            ClinicProviderSearchDTO response = null;
            try
            {
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                response = userDAO.GetClinicUserBasciInfo(userId, clinicBranchId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }



    }
}
