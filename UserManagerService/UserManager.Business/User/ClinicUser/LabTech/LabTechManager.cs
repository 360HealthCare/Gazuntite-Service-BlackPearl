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

namespace UserManager.Business.User.ClinicUser.LabTech
{
    public class LabTechManager
    {

        LabTechDAO ltDAO = null;

        public int ManageLabTechProfile(int userId, LabTechnicianDTO ltDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageLabTechProfile Called PARAM userId::" + userId);

                if (ltDTO.LabTechnicianId > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (ltDAO == null)
                    {
                        ltDAO = (LabTechDAO)DAOFactory.GetDataAccessObject(typeof(LabTechDAO));
                    }

                    profileId = ltDAO.ManageLabTechProfile(userId, ltDTO, IsUpdate);
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


        public LabTechnicianDTO LoadLabTechnicianDTOById(int id)
        {
            LabTechnicianDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadLabTechnicianDTOById Called PARAM doctorId::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (ltDAO == null)
                    {
                        ltDAO = (LabTechDAO)DAOFactory.GetDataAccessObject(typeof(LabTechDAO));
                    }

                    profile = ltDAO.LoadLabTechDTOById(id);
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


        public int ChangeLabTechActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeLabTechActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (ltDAO == null)
                    {
                        ltDAO = (LabTechDAO)DAOFactory.GetDataAccessObject(typeof(LabTechDAO));
                    }

                    result = ltDAO.ChangeLabTechActiveStatus(id, isActive);
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




        public int ChangeLabTechLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeLabTechLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (ltDAO == null)
                    {
                        ltDAO = (LabTechDAO)DAOFactory.GetDataAccessObject(typeof(LabTechDAO));
                    }

                    result = ltDAO.ChangeLabTechLockedStatus(id, isLocked);
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
