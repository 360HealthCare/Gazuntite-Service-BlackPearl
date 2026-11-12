using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.User;
using Quivus.Utility.Logger;
using UserManager.DataAccess.ClinicUser;
using Quivus.Utility.Factory;
using Quivus.Utility.CustomException;
using Quivus.Utility;

namespace UserManager.Business.User.ClinicUser.JrSecurityAdmin
{
    public class JrSecurityAdminManager
    {
        JrSecurityAdminDAO dao = null;

        public int ManageJrSecurityAdminProfile(int userId, JnrSecurityAdminDTO ltDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageJrSecurityAdminProfile Called PARAM userId::" + userId);

                if (ltDTO.JrSecurityAdminId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (JrSecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(JrSecurityAdminDAO));
                    }

                    profileId = dao.ManageJrSecurityAdminProfile(userId, ltDTO, IsUpdate);
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


        public JnrSecurityAdminDTO LoadJnrSecurityAdminDTOById(int id)
        {
            JnrSecurityAdminDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadJnrSecurityAdminDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (JrSecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(JrSecurityAdminDAO));
                    }

                    profile = dao.LoadJnrSecurityAdminDTOById(id);
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



        public int ChangeJrSecurityAdminActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeJrSecurityAdminActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (JrSecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(JrSecurityAdminDAO));
                    }

                    result = dao.ChangeJrSecurityAdminActiveStatus(id, isActive);
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


        public int ChangeJrSecurityAdminLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeJrSecurityAdminLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (JrSecurityAdminDAO)DAOFactory.GetDataAccessObject(typeof(JrSecurityAdminDAO));
                    }

                    result = dao.ChangeJrSecurityAdminLockedStatus(id, isLocked);
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
