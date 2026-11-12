using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using UserManager.DataAccess.ClinicUser;
using Quivus.Utility.Factory;
using Quivus.Utility.CustomException;
using Quivus.Utility;

namespace UserManager.Business.User.ClinicUser.NursePractioner
{
    public class NursePractionerManager
    {
        NursePractionerDAO nursePractionerDAO;

        public int ManageNursePractionerProfile(int userId, NursePractitionerDTO nursePractitionerDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageNursePractionerProfile Called PARAM userId::" + userId);

                if (nursePractitionerDTO.NursePractitionerId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (nursePractionerDAO == null)
                    {
                        nursePractionerDAO = (NursePractionerDAO)DAOFactory.GetDataAccessObject(typeof(NursePractionerDAO));
                    }

                    profileId = nursePractionerDAO.ManageNursePractionerProfile(userId, nursePractitionerDTO, IsUpdate);
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

        public NursePractitionerDTO LoadNursePractitionerDTOById(int id)
        {
            NursePractitionerDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadNursePractitionerDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (nursePractionerDAO == null)
                    {
                        nursePractionerDAO = (NursePractionerDAO)DAOFactory.GetDataAccessObject(typeof(NursePractionerDAO));
                    }

                    profile = nursePractionerDAO.LoadNursePractitionerDTOById(id);
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


        public int ChangeNursePracActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeNursePracActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (nursePractionerDAO == null)
                    {
                        nursePractionerDAO = (NursePractionerDAO)DAOFactory.GetDataAccessObject(typeof(NursePractionerDAO));
                    }

                    result = nursePractionerDAO.ChangeNursePractionerActiveStatus(id, isActive);
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



        public int ChangeNursePracLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeNursePracLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (nursePractionerDAO == null)
                    {
                        nursePractionerDAO = (NursePractionerDAO)DAOFactory.GetDataAccessObject(typeof(NursePractionerDAO));
                    }

                    result = nursePractionerDAO.ChangeNursePractionerLockedStatus(id, isLocked);
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
