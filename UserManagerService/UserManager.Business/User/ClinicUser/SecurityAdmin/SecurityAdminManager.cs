using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using QuivusModel.User;
using UserManager.DataAccess.ClinicUser;
using Quivus.Utility.Factory;
using Quivus.Utility.CustomException;
using Quivus.Utility;

namespace UserManager.Business.User.ClinicUser.SecurityAdmin
{
    public class SecurityAdminManager
    {

        SecurityAdminDAO saDAO = null;

        public int ManageSecurityAdminManagerProfile(int userId, SecurityAdminDTO dto)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageSecurityAdminManagerProfile Called PARAM userId::" + userId);

                if (dto.SecurityAdminId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (saDAO == null)
                    {
                        saDAO = (SecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(SecurityAdminDAO));
                    }

                    profileId = saDAO.ManageSecurityAdmin(userId, dto, IsUpdate);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return profileId;
        }

        public SecurityAdminDTO LoadSecurityAdminDTOById(int id)
        {
            SecurityAdminDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadSecurityAdminDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (saDAO == null)
                    {
                        saDAO = (SecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(SecurityAdminDAO));
                    }

                    profile = saDAO.LoadSecurityAdminDTOById(id);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return profile;
        }



        public int ChangeSecurityAdminActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeSecurityAdminActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (saDAO == null)
                    {
                        saDAO = (SecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(SecurityAdminDAO));
                    }

                    result = saDAO.ChangeSecurityAdminActiveStatus(id, isActive);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }



        public int ChangeSecurityAdminLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeSecurityAdminLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (saDAO == null)
                    {
                        saDAO = (SecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(SecurityAdminDAO));
                    }

                    result = saDAO.ChangeSecurityAdminLockedStatus(id, isLocked);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }


    }
}
