using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusSecurity.Logger.Logger;
using ServiceResponce.ResponceDTO.Static;
using QuivusSecurity.Business.BL;
using QuivusSecurity.Business.BL.Impl;
using QuivusSecurityService.Utility;
using ServiceResponce.ResponceDTO;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace QuivusSecurityService.Service.Rest
{

    public class QuivusSecurityService : IQuivusSecurityService
    {

        ISecurityBL securityBL = null;

        public string TestSecurityService()
        {
            string status = "Call Received...";
            QuivusLogger.Info("Call Received...");
            return status;
        }

        /// <summary>
        /// Hassan
        /// Loads roles in the application, it gets them from UserManagerService
        /// </summary>
        /// <returns></returns>
        public UserTypeResponce LoadAllUserTypes()
        {
            QuivusLogger.Info("Call Received for method LoadAllUserTypes in service ...");
            UserTypeResponce responce = null;
            try
            {
                if (securityBL == null)
                {
                    securityBL = new SecurityBLImpl();
                }

                responce = securityBL.LoadAllUserTypes();

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                ResponseErrorHandler.HandleException(responce, ex);
            }

            return responce;

        }

        /// <summary>
        /// Hassan
        /// loads access rights of a role on app modules
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public AccessRightResponce LoadRoleAccessRight(string userTypeId, string clinicBranchId)
        {
            AccessRightResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadRoleAccessRight in service ...");
            try
            {
                if (securityBL == null)
                {
                    securityBL = new SecurityBLImpl();
                }
                responce = securityBL.LoadRoleAccessRight(userTypeId, clinicBranchId);

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                ResponseErrorHandler.HandleException(responce, ex);
            }

            return responce;
        }

        /// <summary>
        /// Hassan
        /// loads access rights of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public AccessRightResponce LoadUserAccessRight(string userId, string userTypeId, string clinicBranchId)
        {
            AccessRightResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadUserAccessRight in service ...");
            try
            {
                if (securityBL == null)
                {
                    securityBL = new SecurityBLImpl();
                }
                responce = securityBL.LoadUserAccessRight(userId, userTypeId, clinicBranchId);

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                ResponseErrorHandler.HandleException(responce, ex);
            }

            return responce;


        }

        /// <summary>
        /// Hassan
        /// Loads application modules
        /// </summary>
        /// <returns></returns>
        public AppModuleTypeResponce LoadAppModules()
        {

            AppModuleTypeResponce responce = null;
            QuivusLogger.Info("Call Received for method LoadAppModules in service ...");
            try
            {
                if (securityBL == null)
                {
                    securityBL = new SecurityBLImpl();
                }
                responce = securityBL.LoadAppModules();

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                ResponseErrorHandler.HandleException(responce, ex);
            }

            return responce;

        }

        /// <summary>
        /// Hassan
        /// receives role access rights and update its status 
        /// </summary>
        /// <param name="appointmentXml"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public bool ManageRoleAccessRight(XmlDocument appointmentXml, string userTypeId, string clinicBranchId)
        {
            
            bool updated = false;
            if (appointmentXml != null)
            {
                AccessRightResponce responceDTO =new AccessRightResponce();

                XmlDocument doc = new XmlDocument();

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Responce";
                xRoot.IsNullable = true;


                XmlSerializer serializer = new XmlSerializer(typeof(AccessRightResponce), xRoot);

                string xml = appointmentXml.InnerXml;




                responceDTO = (AccessRightResponce)serializer.Deserialize(new StringReader(xml));

                try
                {
                    if (securityBL == null)
                    {
                        securityBL = new SecurityBLImpl();
                    }

                    updated = securityBL.ManageRoleAccessRight(responceDTO, userTypeId, clinicBranchId);
                }
                catch (Exception ex)
                {
                    QuivusLogger.Error(ex);
                }
            }
            return updated;
        }




        public bool ManageUserAccessRight(XmlDocument appointmentXml, string userId, string userTypeId, string clinicBranchId)
        {
           
            bool updated = false;
            if (appointmentXml != null)
            {
                AccessRightResponce responceDTO = new AccessRightResponce();

                XmlDocument doc = new XmlDocument();

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Responce";
                xRoot.IsNullable = true;


                XmlSerializer serializer = new XmlSerializer(typeof(AccessRightResponce), xRoot);

                string xml = appointmentXml.InnerXml;




                responceDTO = (AccessRightResponce)serializer.Deserialize(new StringReader(xml));

                try
                {
                    if (securityBL == null)
                    {
                        securityBL = new SecurityBLImpl();
                    }

                    updated = securityBL.ManageUserAccessRight(responceDTO, userId, userTypeId, clinicBranchId);
                }
                catch (Exception ex)
                {
                    QuivusLogger.Error(ex);
                }
            }

            return updated;
        }



        /// <summary>
        /// Hassan
        /// it generates access rights of roles of a practice on app modules
        /// </summary>
        /// <returns></returns>
        public bool GenerateUserTypeAccessRights()
        {

            bool updated = false;
            try
            {

                if (securityBL == null)
                {
                    securityBL = new SecurityBLImpl();
                }

                updated = securityBL.GenerateUserTypeAccessRights();

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
            }
            return updated;

        }


        /// <summary>
        /// hassan
        /// it generates access rights of the users of practices
        /// </summary>
        /// <returns></returns>
        public bool GenerateUserAccessRights()
        {

            bool updated = false;
            try
            {

                if (securityBL == null)
                {
                    securityBL = new SecurityBLImpl();
                }

                updated = securityBL.GenerateUserAccessRights();

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
            }
            return updated;

        }

    }
}
