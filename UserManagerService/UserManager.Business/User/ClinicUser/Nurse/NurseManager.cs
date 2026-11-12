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

namespace UserManager.Business.User.ClinicUser.Nurse
{
    public class NurseManager
    {
        NurseDAO nurseDAO = null;

        public int ManageNurseProfile(int userId, NurseDTO nurseDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageNurseProfile Called PARAM userId::" + userId);

                if (nurseDTO.NURSE_ID_SEQ > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (nurseDAO == null)
                    {
                        nurseDAO = (NurseDAO)DAOFactory.GetDataAccessObject(typeof(NurseDAO));
                    }

                    profileId = nurseDAO.ManageNursePractionerProfile(userId, nurseDTO, IsUpdate);
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

        public NurseDTO LoadNurseDTOById(int id)
        {
            NurseDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadNurseDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (nurseDAO == null)
                    {
                        nurseDAO = (NurseDAO)DAOFactory.GetDataAccessObject(typeof(NurseDAO));
                    }

                    profile = nurseDAO.LoadNurseDTOById(id);
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



        public int ChangeNurseActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeNurseActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (nurseDAO == null)
                    {
                        nurseDAO = (NurseDAO)DAOFactory.GetDataAccessObject(typeof(NurseDAO));
                    }

                    result = nurseDAO.ChangeNurseActiveStatus(id, isActive);
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


        public int ChangeNurseLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeNurseLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (nurseDAO == null)
                    {
                        nurseDAO = (NurseDAO)DAOFactory.GetDataAccessObject(typeof(NurseDAO));
                    }

                    result = nurseDAO.ChangeNurseLockedStatus(id, isLocked);
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
