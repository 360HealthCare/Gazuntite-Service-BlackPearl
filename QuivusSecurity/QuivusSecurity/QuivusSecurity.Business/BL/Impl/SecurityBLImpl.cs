using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceResponce.ResponceDTO.Static;
using QuivusSecurity.Business.ServiceFactory;
using QuivusSecurity.IntegratedService.UserManagerService;
using QuivusSecurity.Logger.Logger;
using ServiceResponce.ResponceDTO;
using QuivusSecurity.DAO.DAO;
using QuivusSecurity.DAO.DAO.Factory;
using QuivusModel.Security;
using QuivusSecurity.Business.Utility;

namespace QuivusSecurity.Business.BL.Impl
{

    /// <summary>
    /// Hassan
    /// Deals with the security logic.
    /// </summary>
    public class SecurityBLImpl : ISecurityBL
    {

        UserManagerServiceClient UserManagerServiceClientObj = null;

        SecurityDAO securityDAO = null;


        /// <summary>
        /// Hassan
        /// Loads roles in the application, it gets them from UserManagerService
        /// </summary>
        /// <returns></returns>
        public UserTypeResponce LoadAllUserTypes()
        {
            UserTypeResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadAllUserTypes in SecurityBLImpl ...");
            try
            {
                if (UserManagerServiceClientObj == null)
                {
                    UserManagerServiceClientObj = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));
                    //UserManagerServiceClientObj = new UserManagerServiceClient();
                }

                List<UserTypeDTO> list = UserManagerServiceClientObj.LoadAllUserType();

                responce = new UserTypeResponce();
                responce.UserTypeDTOList = list;

            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method LoadAllUserTypes...");
                throw;
            }

            return responce;
        }



        /// <summary>
        /// Hassan
        /// it generates access rights of roles of a practice on app modules
        /// </summary>
        /// <returns></returns>
        public bool GenerateUserTypeAccessRights()
        {
            bool generated = false;
            QuivusLogger.Info("Call Received for method GenerateUserTypeAccessRights in SecurityBLImpl ...");
            try
            {
                if (UserManagerServiceClientObj == null)
                {
                    UserManagerServiceClientObj = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));

                }

                List<int> clinicBranchIds = UserManagerServiceClientObj.LoadAllClinicBranch();

                List<UserTypeDTO> roles = UserManagerServiceClientObj.LoadAllUserType();

                if (securityDAO == null)
                {
                    securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                }

                List<AppModuleDTO> appModule = securityDAO.LoadAppModules();

                if (clinicBranchIds != null && roles != null && appModule != null)
                {
                    for (int i = 0; i < clinicBranchIds.Count; i++)
                    {
                        int clinicBranchId = clinicBranchIds[i];

                        for (int j = 0; j < roles.Count; j++)
                        {
                            UserTypeDTO role = roles[j] as UserTypeDTO;

                            if (role.UserTypeName != Constant.PATIENT_ROLE_NAME)
                            {
                                for (int k = 0; k < appModule.Count; k++)
                                {
                                    AppModuleDTO module = appModule[k] as AppModuleDTO;

                                    securityDAO.InsertUserTypeApModuleAccessRight(module.IdSeq, role.UserTypeId, clinicBranchId, true, true);
                                }
                            }

                        }
                    }
                    generated = true;
                }

            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method GenerateUserTypeAccessRights...");
                throw;
            }

            return generated;
        }



        /// <summary>
        /// generates default access rights for all the users of all the pactices ...
        /// </summary>
        /// <returns></returns>
        public bool GenerateUserAccessRights()
        {
            bool generated = false;
            QuivusLogger.Info("Call Received for method GenerateUserAccessRights in SecurityBLImpl ...");
            try
            {
                if (UserManagerServiceClientObj == null)
                {
                    UserManagerServiceClientObj = (UserManagerServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(UserManagerServiceClient));

                }

                List<int> clinicBranchIds = UserManagerServiceClientObj.LoadAllClinicBranch();

                List<UserTypeDTO> roles = UserManagerServiceClientObj.LoadAllUserType();

                if (securityDAO == null)
                {
                    securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                }



                if (clinicBranchIds != null && roles != null)
                {
                    for (int i = 0; i < clinicBranchIds.Count; i++)
                    {
                        int clinicBranchId = clinicBranchIds[i];

                        for (int j = 0; j < roles.Count; j++)
                        {
                            UserTypeDTO role = roles[j] as UserTypeDTO;

                            if (role.UserTypeName != Constant.PATIENT_ROLE_NAME)
                            {


                                List<int> userIds = UserManagerServiceClientObj.LoadClinicBranchUsers(role.UserTypeId, clinicBranchId);

                                if (userIds != null && userIds.Count > 0)
                                {
                                    List<AccessRightDTO> userTypeappModuleAR = securityDAO.LoadRoleAccessRight(role.UserTypeId, clinicBranchId);


                                    for (int k = 0; k < userIds.Count; k++)
                                    {
                                        int userId = userIds[k];


                                        for (int l = 0; l < userTypeappModuleAR.Count; l++)
                                        {
                                            int userTypeAppModuleARIdSeq = (userTypeappModuleAR[l] as AccessRightDTO).UserTypeAppModuleAccessRightIdSeq;

                                            securityDAO.InsertUserApModuleAccessRight(userId, userTypeAppModuleARIdSeq, role.UserTypeId, clinicBranchId, true, true);
                                        }

                                    }

                                }



                            }

                        }
                    }
                    generated = true;
                }

            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method GenerateUserTypeAccessRights...");
                throw;
            }

            return generated;
        }

        /// <summary>
        /// Hassan
        /// loads access rights of a role on app modules
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public AccessRightResponce LoadRoleAccessRight(string uTypeId, string cbId)
        {

            AccessRightResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadRoleAccessRight in SecurityBLImpl ... uTypeId=" + uTypeId +" cbId="+cbId);
            try
            {
                int userTypeId = 0, clinicBranchId = 0;

                if (!string.IsNullOrEmpty(uTypeId))
                    userTypeId = int.Parse(uTypeId);

                if (!string.IsNullOrEmpty(cbId))
                    clinicBranchId = int.Parse(cbId);


                if (securityDAO == null)
                {
                    securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                }

                if (userTypeId > 0 && clinicBranchId > 0)
                {
                    List<AccessRightDTO> list = securityDAO.LoadRoleAccessRight(userTypeId, clinicBranchId);

                    responce = new AccessRightResponce();

                    PopulateResponceSection(responce, list);

                    responce.ResponseStatus = true;
                }

            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method LoadRoleAccessRight...");
                throw;
            }

            return responce;

        }

        private static void PopulateResponceSection(AccessRightResponce responce, List<AccessRightDTO> list)
        {

            AccessRightDTO dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_QUICK_LINKS);
            responce.QLink = new QuivusModel.Security.AppDTO.QLinkDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.QLink, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_COMMUNICATION);
            responce.Communication = new QuivusModel.Security.AppDTO.CommunicationDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.Communication, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_WEB_SEARCH);
            responce.WebSearch = new QuivusModel.Security.AppDTO.WebSearchDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.WebSearch, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_PROFILE);
            responce.Profile = new QuivusModel.Security.AppDTO.ProfileDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.Profile, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_SEARCH_PATIENT);
            responce.SearchPatient = new QuivusModel.Security.AppDTO.SearchPatientDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.SearchPatient, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_DOCUMENT_IMPORT);
            responce.ImportDocument = new QuivusModel.Security.AppDTO.ImportDocumentDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ImportDocument, dto);





            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_MOST_RECENT_PATIENT);
            responce.MostRecentPatient = new QuivusModel.Security.AppDTO.MostRecentPatientDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.MostRecentPatient, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_PRESCRIPTION);
            responce.Prescription = new QuivusModel.Security.AppDTO.PrescriptionDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.Prescription, dto);





            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_NOTES);
            responce.Notes = new QuivusModel.Security.AppDTO.NotesDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.Notes, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_PATIENT_PROFILE);
            responce.PatientProfile = new QuivusModel.Security.AppDTO.PatientProfileDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.PatientProfile, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_VIEW_BACKGROUND);
            responce.ViewBackground = new QuivusModel.Security.AppDTO.VBDTO.ViewBackgroundDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground, dto);



            PopulateResponceVB(responce, list);





            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_QI);
            responce.QI = new QuivusModel.Security.AppDTO.QI.QIDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.QI, dto);



            PopulateResponceQISection(responce, list);


        }

        private static void PopulateResponceQISection(AccessRightResponce responce, List<AccessRightDTO> list)
        {

            AccessRightDTO dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_QI_PMH);
            responce.QI.QIPMH = new QuivusModel.Security.AppDTO.QI.PMH.QIPMHDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.QI.QIPMH, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_QI_ALLERGIES);
            responce.QI.QIPMH.Allergy = new QuivusModel.Security.AppDTO.QI.PMH.QIAllergyDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.QI.QIPMH.Allergy, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_QI_PHYSICAL);
            responce.QI.QIPhysical = new QuivusModel.Security.AppDTO.QI.Physical.QIPhysicalDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.QI.QIPhysical, dto);




            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_QI_ASSESMENT);
            responce.QI.QIAssesment = new QuivusModel.Security.AppDTO.QI.Assesment.QIAssesmentDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.QI.QIAssesment, dto);

        }

        private static void PopulateResponceVB(AccessRightResponce responce, List<AccessRightDTO> list)
        {

            AccessRightDTO dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_CREATE_VISIT);
            responce.ViewBackground.CreateVisit = new QuivusModel.Security.AppDTO.VBDTO.CreateVisitDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.CreateVisit, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_LOAD_HISTORY);
            responce.ViewBackground.LoadHistory = new QuivusModel.Security.AppDTO.VBDTO.LoadHistoryDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.LoadHistory, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_ENCOUNTER_VIEW);
            responce.ViewBackground.EncounterList = new QuivusModel.Security.AppDTO.VBDTO.EncounterListDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.EncounterList, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_DOCUMENT_VIEW);
            responce.ViewBackground.DocumentList = new QuivusModel.Security.AppDTO.VBDTO.DocumentListDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.DocumentList, dto);


            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_VB_PMH);
            responce.ViewBackground.VBPMH = new QuivusModel.Security.AppDTO.VBDTO.PMH.VBPMHDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.VBPMH, dto);


            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_VB_ALLERGIES);
            responce.ViewBackground.VBPMH.VBAllergy = new QuivusModel.Security.AppDTO.VBDTO.PMH.VBAllergyDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.VBPMH.VBAllergy, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_VB_PHYSICAL);
            responce.ViewBackground.VBPhysical = new QuivusModel.Security.AppDTO.VBDTO.Physical.VBPhysicalDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.VBPhysical, dto);



            dto = UtilityClass.GetAppSection(list, Constant.APP_MODULE_VB_SSESMENT);
            responce.ViewBackground.VBAssesment = new QuivusModel.Security.AppDTO.VBDTO.Assesment.VBAssesmentDTO();
            UtilityClass.PopulateDTOForAccessRight(responce.ViewBackground.VBAssesment, dto);

        }

        /// <summary>
        /// Hassan
        /// Loads app modules
        /// </summary>
        /// <returns></returns>
        public AppModuleTypeResponce LoadAppModules()
        {
            AppModuleTypeResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadAppModules in SecurityBLImpl ...");
            try
            {


                if (securityDAO == null)
                {
                    securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                }

                responce = new AppModuleTypeResponce();
                responce.AppModuleDTOList = securityDAO.LoadAppModules();

            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method LoadAppModules...");
                throw;
            }

            return responce;
        }



        /// <summary>
        /// Hassan
        /// receives role access rights and update its status 
        /// </summary>
        /// <param name="roleARResponceDTO"></param>
        /// <param name="uTypeId"></param>
        /// <param name="cbId"></param>
        /// <returns></returns>
        public bool ManageRoleAccessRight(AccessRightResponce roleARResponceDTO, string uTypeId, String cbId)
        {
            bool isSuccessful = false;
            QuivusLogger.Info("Call Received for method ManageRoleAccessRight in SecurityBLImpl ... uTypeId=" + uTypeId + " cbId=" + cbId);
            try
            {
                int userTypeId = 0, clinicBranchId = 0;

                if (!string.IsNullOrEmpty(uTypeId))
                    userTypeId = int.Parse(uTypeId);

                if (!string.IsNullOrEmpty(cbId))
                    clinicBranchId = int.Parse(cbId);


                if (roleARResponceDTO != null)
                {
                    if (securityDAO == null)
                    {
                        securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                    }

                    int userId = 0;

                    if (userTypeId > 0 && clinicBranchId > 0 )
                    {
                        UpdateRoleOrUserAppSectionStatus(roleARResponceDTO, userTypeId, clinicBranchId, userId);
                        isSuccessful = true;
                    }

                    
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method ManageRoleAccessRight...");
                throw;
            }


            return isSuccessful;
        }


        /// <summary>
        /// if userId is 0 then it will update role access right otherwise it will
        /// update user access right
        /// </summary>
        /// <param name="roleARResponceDTO"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="userId"></param>
        private void UpdateRoleOrUserAppSectionStatus(AccessRightResponce roleARResponceDTO, int userTypeId, int clinicBranchId, int userId)
        {
            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
                roleARResponceDTO.QLink.RoleAppModuleAccessRightIdSeq,
                roleARResponceDTO.QLink.IsViewAllowed,
                roleARResponceDTO.QLink.IsEditAllowed,
                clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
                roleARResponceDTO.Communication.RoleAppModuleAccessRightIdSeq,
                roleARResponceDTO.Communication.IsViewAllowed,
                roleARResponceDTO.Communication.IsEditAllowed,
                clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
               roleARResponceDTO.WebSearch.RoleAppModuleAccessRightIdSeq,
               roleARResponceDTO.WebSearch.IsViewAllowed,
               roleARResponceDTO.WebSearch.IsEditAllowed,
               clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
              roleARResponceDTO.Profile.RoleAppModuleAccessRightIdSeq,
              roleARResponceDTO.Profile.IsViewAllowed,
              roleARResponceDTO.Profile.IsEditAllowed,
              clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
             roleARResponceDTO.SearchPatient.RoleAppModuleAccessRightIdSeq,
             roleARResponceDTO.SearchPatient.IsViewAllowed,
             roleARResponceDTO.SearchPatient.IsEditAllowed,
             clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
            roleARResponceDTO.ImportDocument.RoleAppModuleAccessRightIdSeq,
            roleARResponceDTO.ImportDocument.IsViewAllowed,
            roleARResponceDTO.ImportDocument.IsEditAllowed,
            clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
           roleARResponceDTO.MostRecentPatient.RoleAppModuleAccessRightIdSeq,
           roleARResponceDTO.MostRecentPatient.IsViewAllowed,
           roleARResponceDTO.MostRecentPatient.IsEditAllowed,
           clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
          roleARResponceDTO.Prescription.RoleAppModuleAccessRightIdSeq,
          roleARResponceDTO.Prescription.IsViewAllowed,
          roleARResponceDTO.Prescription.IsEditAllowed,
          clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
          roleARResponceDTO.Notes.RoleAppModuleAccessRightIdSeq,
          roleARResponceDTO.Notes.IsViewAllowed,
          roleARResponceDTO.Notes.IsEditAllowed,
          clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
         roleARResponceDTO.PatientProfile.RoleAppModuleAccessRightIdSeq,
         roleARResponceDTO.PatientProfile.IsViewAllowed,
         roleARResponceDTO.PatientProfile.IsEditAllowed,
         clinicBranchId, userId);


            UpdateRoleOrUserAppSectionQIStatus(roleARResponceDTO, userTypeId, clinicBranchId, userId);


            UpdateRoleOrUserAppSectionVBStatus(roleARResponceDTO, userTypeId, clinicBranchId, userId);


        }


        /// <summary>
        /// if userId is 0 then it will update role access right otherwise it will
        /// update user access right
        /// </summary>
        /// <param name="roleARResponceDTO"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="userId"></param>
        private void UpdateRoleOrUserAppSectionVBStatus(AccessRightResponce roleARResponceDTO, int userTypeId, int clinicBranchId, int userId)
        {
            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
        roleARResponceDTO.ViewBackground.RoleAppModuleAccessRightIdSeq,
        roleARResponceDTO.ViewBackground.IsViewAllowed,
        roleARResponceDTO.ViewBackground.IsEditAllowed,
        clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
        roleARResponceDTO.ViewBackground.CreateVisit.RoleAppModuleAccessRightIdSeq,
        roleARResponceDTO.ViewBackground.CreateVisit.IsViewAllowed,
        roleARResponceDTO.ViewBackground.CreateVisit.IsEditAllowed,
        clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
        roleARResponceDTO.ViewBackground.LoadHistory.RoleAppModuleAccessRightIdSeq,
        roleARResponceDTO.ViewBackground.LoadHistory.IsViewAllowed,
        roleARResponceDTO.ViewBackground.LoadHistory.IsEditAllowed,
        clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
      roleARResponceDTO.ViewBackground.EncounterList.RoleAppModuleAccessRightIdSeq,
      roleARResponceDTO.ViewBackground.EncounterList.IsViewAllowed,
      roleARResponceDTO.ViewBackground.EncounterList.IsEditAllowed,
      clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
      roleARResponceDTO.ViewBackground.DocumentList.RoleAppModuleAccessRightIdSeq,
      roleARResponceDTO.ViewBackground.DocumentList.IsViewAllowed,
      roleARResponceDTO.ViewBackground.DocumentList.IsEditAllowed,
      clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
     roleARResponceDTO.ViewBackground.VBPMH.RoleAppModuleAccessRightIdSeq,
     roleARResponceDTO.ViewBackground.VBPMH.IsViewAllowed,
     roleARResponceDTO.ViewBackground.VBPMH.IsEditAllowed,
     clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
    roleARResponceDTO.ViewBackground.VBPMH.VBAllergy.RoleAppModuleAccessRightIdSeq,
    roleARResponceDTO.ViewBackground.VBPMH.VBAllergy.IsViewAllowed,
    roleARResponceDTO.ViewBackground.VBPMH.VBAllergy.IsEditAllowed,
    clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
    roleARResponceDTO.ViewBackground.VBPhysical.RoleAppModuleAccessRightIdSeq,
    roleARResponceDTO.ViewBackground.VBPhysical.IsViewAllowed,
    roleARResponceDTO.ViewBackground.VBPhysical.IsEditAllowed,
    clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
   roleARResponceDTO.ViewBackground.VBAssesment.RoleAppModuleAccessRightIdSeq,
   roleARResponceDTO.ViewBackground.VBAssesment.IsViewAllowed,
   roleARResponceDTO.ViewBackground.VBAssesment.IsEditAllowed,
   clinicBranchId, userId);

        }


        /// <summary>
        /// if userId is 0 then it will update role access right otherwise it will
        /// update user access right
        /// </summary>
        /// <param name="roleARResponceDTO"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="userId"></param>
        private void UpdateRoleOrUserAppSectionQIStatus(AccessRightResponce roleARResponceDTO, int userTypeId, int clinicBranchId, int userId)
        {

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
         roleARResponceDTO.QI.RoleAppModuleAccessRightIdSeq,
         roleARResponceDTO.QI.IsViewAllowed,
         roleARResponceDTO.QI.IsEditAllowed,
         clinicBranchId, userId);



            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
         roleARResponceDTO.QI.QIPMH.RoleAppModuleAccessRightIdSeq,
         roleARResponceDTO.QI.QIPMH.IsViewAllowed,
         roleARResponceDTO.QI.QIPMH.IsEditAllowed,
         clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
        roleARResponceDTO.QI.QIPMH.Allergy.RoleAppModuleAccessRightIdSeq,
        roleARResponceDTO.QI.QIPMH.Allergy.IsViewAllowed,
        roleARResponceDTO.QI.QIPMH.Allergy.IsEditAllowed,
        clinicBranchId, userId);

            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
         roleARResponceDTO.QI.QIPhysical.RoleAppModuleAccessRightIdSeq,
         roleARResponceDTO.QI.QIPhysical.IsViewAllowed,
         roleARResponceDTO.QI.QIPhysical.IsEditAllowed,
         clinicBranchId, userId);


            securityDAO.ManageRoleOrUserAccessRight(userTypeId,
         roleARResponceDTO.QI.QIAssesment.RoleAppModuleAccessRightIdSeq,
         roleARResponceDTO.QI.QIAssesment.IsViewAllowed,
         roleARResponceDTO.QI.QIAssesment.IsEditAllowed,
         clinicBranchId, userId);
        }




        /// <summary>
        /// Hassan
        /// Loads access rights of a user
        /// </summary>
        /// <param name="uTypeId"></param>
        /// <param name="cbId"></param>
        /// <returns></returns>
        public AccessRightResponce LoadUserAccessRight(string uId, string uTypeId, string cbId)
        {

            AccessRightResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadUserAccessRight in SecurityBLImpl ... uId="+uId+ " uTypeId="+uTypeId+" cbId="+cbId);
            try
            {
                int userTypeId = 0, clinicBranchId = 0, userId = 0;

                if (!string.IsNullOrEmpty(uTypeId))
                    userTypeId = int.Parse(uTypeId);

                if (!string.IsNullOrEmpty(cbId))
                    clinicBranchId = int.Parse(cbId);

                if (!string.IsNullOrEmpty(uId))
                    userId = int.Parse(uId);


                if (securityDAO == null)
                {
                    securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                }

                if (userTypeId > 0 && clinicBranchId > 0 && userId > 0)
                {

                    List<AccessRightDTO> list = securityDAO.LoadUserAccessRight(userId, userTypeId, clinicBranchId);

                    responce = new AccessRightResponce();

                    PopulateResponceSection(responce, list);

                    responce.ResponseStatus = true;
                }

            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method LoadUserAccessRight...");
                throw;
            }

            return responce;

        }


        /// <summary>
        /// manages accrss right for a perticular user
        /// </summary>
        /// <param name="roleARResponceDTO"></param>
        /// <param name="uId"></param>
        /// <param name="uTypeId"></param>
        /// <param name="cbId"></param>
        /// <returns></returns>
        public bool ManageUserAccessRight(AccessRightResponce roleARResponceDTO, string uId, string uTypeId, String cbId)
        {
            bool isSuccessful = false;
            QuivusLogger.Info("Call Received for method ManageUserAccessRight in SecurityBLImpl ... uId=" + uId + " uTypeId=" + uTypeId + " cbId=" + cbId);
            try
            {
                int userTypeId = 0, clinicBranchId = 0, userId = 0;

                if (!string.IsNullOrEmpty(uTypeId))
                    userTypeId = int.Parse(uTypeId);

                if (!string.IsNullOrEmpty(cbId))
                    clinicBranchId = int.Parse(cbId);

                if (!string.IsNullOrEmpty(uId))
                    userId = int.Parse(uId);


                if (roleARResponceDTO != null)
                {
                    if (securityDAO == null)
                    {
                        securityDAO = (SecurityDAO)DAOFactory.GetDataAccessObject(typeof(SecurityDAO));
                    }


                    if (userId > 0 && clinicBranchId > 0 && userTypeId > 0)
                    {
                        UpdateRoleOrUserAppSectionStatus(roleARResponceDTO, userTypeId, clinicBranchId, userId);
                        isSuccessful = true;
                    }
                                        
                }
            }
            catch
            {
                QuivusLogger.Error("Exception in SecurityBLImpl method ManageUserAccessRight...");
                throw;
            }


            return isSuccessful;
        }
    }
}
