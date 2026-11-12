using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using UserManager.DataAccess.ClinicUser;
using Quivus.Utility.Factory;

namespace UserManager.Business.User.ClinicUser.BillingAssistent
{
    public class BillingAssistentManager
    {
        BillingAssestentDAO baDAO = null;

        public int ManageBillingAssistentProfile(int userId, BillingAssistantDTO dto)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageBillingAssistentProfile Called PARAM userId::" + userId);

                if (dto.BillingAssistantId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (baDAO == null)
                    {
                        baDAO = (BillingAssestentDAO)DAOFactory.GetDataAccessObject(typeof(BillingAssestentDAO));
                    }

                    profileId = baDAO.ManageBillingAssestent(userId, dto, IsUpdate);
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

        public BillingAssistantDTO LoadBillingAssistantDTOById(int id)
        {
            BillingAssistantDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadBillingAssistantDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (baDAO == null)
                    {
                        baDAO = (BillingAssestentDAO)DAOFactory.GetDataAccessObject(typeof(BillingAssestentDAO));
                    }

                    profile = baDAO.LoadBillingAssistantDTOById(id);
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


        public int ChangeBillingAssistentActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeBillingAssistentActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (baDAO == null)
                    {
                        baDAO = (BillingAssestentDAO)DAOFactory.GetDataAccessObject(typeof(BillingAssestentDAO));
                    }

                    result = baDAO.ChangeBillingAssistentActiveStatus(id, isActive);
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



        public int ChangeBillingAssistentLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeBillingAssistentLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (baDAO == null)
                    {
                        baDAO = (BillingAssestentDAO)DAOFactory.GetDataAccessObject(typeof(BillingAssestentDAO));
                    }

                    result = baDAO.ChangeBillingAssistentLockedStatus(id, isLocked);
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
