using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.User;
using Quivus.Utility.Logger;
using UserManager.DataAccess.ClinicUser;
using Quivus.Utility.Factory;
using Quivus.Utility;
using Quivus.Utility.CustomException;

namespace UserManager.Business.User.ClinicUser.BillingManager
{
    public class BillingMManager
    {
        BillingManagerDAO dao = null;

        public int ManageBillingManagerProfile(int userId, BillingManagerDTO ltDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageBillingManagerProfile Called PARAM userId::" + userId);

                if (ltDTO.BillingManagerId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (BillingManagerDAO)DAOFactory.GetDataAccessObject(typeof(BillingManagerDAO));
                    }

                    profileId = dao.ManageBillingManagerProfile(userId, ltDTO, IsUpdate);
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


        public BillingManagerDTO LoadBillingManagerDTOById(int id)
        {
            BillingManagerDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadLabTechnicianDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (BillingManagerDAO)DAOFactory.GetDataAccessObject(typeof(BillingManagerDAO));
                    }

                    profile = dao.LoadBillingManagerDTOById(id);
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




        public int ChangeBillingManagerActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeBillingManagerActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (BillingManagerDAO)DAOFactory.GetDataAccessObject(typeof(BillingManagerDAO));
                    }

                    result = dao.ChangeBMActiveStatus(id, isActive);
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


        public int ChangeBillingManagerLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeBillingManagerLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (BillingManagerDAO)DAOFactory.GetDataAccessObject(typeof(BillingManagerDAO));
                    }

                    result = dao.ChangeBMLockedStatus(id, isLocked);
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
