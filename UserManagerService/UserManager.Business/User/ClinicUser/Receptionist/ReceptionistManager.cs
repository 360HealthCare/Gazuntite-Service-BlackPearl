using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.User;
using Quivus.Utility.Logger;
using UserManager.DataAccess.ClinicUser;
using Quivus.Utility.Factory;
using Quivus.Utility;
using Quivus.Utility.CustomException;

namespace UserManager.Business.User.ClinicUser.Receptionist
{
    public class ReceptionistManager
    {
        ReceptionistDAO rDAO = null;

        public int ManageReceptionistProfile(int userId, ReceptionistDTO dto)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageReceptionistProfile Called PARAM userId::" + userId);

                if (dto.ReceptionistId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (rDAO == null)
                    {
                        rDAO = (ReceptionistDAO)DAOFactory.GetDataAccessObject(typeof(ReceptionistDAO));
                    }

                    profileId = rDAO.ManageReceptionistProfile(userId, dto, IsUpdate);
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



        public ReceptionistDTO LoadReceptionistDTOById(int id)
        {
            ReceptionistDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadReceptionistDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (rDAO == null)
                    {
                        rDAO = (ReceptionistDAO)DAOFactory.GetDataAccessObject(typeof(ReceptionistDAO));
                    }

                    profile = rDAO.LoadReceptionistDTOById(id);
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


        public int ChangeReceptionistActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangePracticeManagerActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (rDAO == null)
                    {
                        rDAO = (ReceptionistDAO)DAOFactory.GetDataAccessObject(typeof(ReceptionistDAO));
                    }

                    result = rDAO.ChangeReceptionistActiveStatus(id, isActive);
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



        public int ChangeReceptionistLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeReceptionistLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (rDAO == null)
                    {
                        rDAO = (ReceptionistDAO)DAOFactory.GetDataAccessObject(typeof(ReceptionistDAO));
                    }

                    result = rDAO.ChangeReceptionistLockedStatus(id, isLocked);
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
