using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManager.DataAccess.ClinicUser;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using Quivus.Utility.Factory;
using Quivus.Utility.CustomException;
using Quivus.Utility;

namespace UserManager.Business.User.ClinicUser.PracticeManager
{
    public class PracticeMManager
    {
        PracticeManagerDAO dao = null;

        public int ManagePracticeManagerProfile(int userId, PracticeManagerDTO dto)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManagePracticeManagerProfile Called PARAM userId::" + userId);

                if (dto.PracticeManagerId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (PracticeManagerDAO)DAOFactory.GetDataAccessObject(typeof(PracticeManagerDAO));
                    }

                    profileId = dao.ManagePracticeManagerProfile(userId, dto, IsUpdate);
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


        public PracticeManagerDTO LoadPracticeManagerDTOById(int id)
        {
            PracticeManagerDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadPracticeManagerDTOById Called PARAM  Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (PracticeManagerDAO)DAOFactory.GetDataAccessObject(typeof(PracticeManagerDAO));
                    }

                    profile = dao.LoadPracticeManagerDTOById(id);
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




        public int ChangePracticeManagerActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangePracticeManagerActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (PracticeManagerDAO)DAOFactory.GetDataAccessObject(typeof(PracticeManagerDAO));
                    }

                    result = dao.ChangePracticeManagActiveStatus(id, isActive);
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




        public int ChangePracticeManagerLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangePracticeManagerLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (PracticeManagerDAO)DAOFactory.GetDataAccessObject(typeof(PracticeManagerDAO));
                    }

                    result = dao.ChangePracticeManagLockedStatus(id, isLocked);
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
