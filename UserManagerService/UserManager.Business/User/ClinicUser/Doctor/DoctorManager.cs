using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManager.DataAccess.ClinicUser;
using QuivusModel.User.ClinicUser;
using Quivus.Model.User;
using Quivus.Utility.Factory;
using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;

namespace UserManager.Business.User.ClinicUser.Doctor.Impl
{
    public class DoctorManager
    {
        DoctorDAO doctorDAO = null;

        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>     
        public int ManageDoctorProfile(int userId, DoctorDTO doctorDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageDoctorProfile Called PARAM userId::" + userId);               

                if (doctorDTO.DoctorId > 0)
                {
                    IsUpdate = true;
                }

                
                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (doctorDAO == null)
                    {
                        doctorDAO = (DoctorDAO)DAOFactory.GetDataAccessObject(typeof(DoctorDAO));
                    }

                    profileId = doctorDAO.ManageDoctorProfile(userId, doctorDTO, IsUpdate);
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

        /// <summary>
        /// Author Mansoor
        /// Date 06 Oct 2011        
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>     
        public DoctorDTO LoadDoctorByDoctorId(int doctorId)
        {
            DoctorDTO profile = null;           

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadDoctorProfile Called PARAM doctorId::" + doctorId);


                if (doctorId > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (doctorDAO == null)
                    {
                        doctorDAO = (DoctorDAO)DAOFactory.GetDataAccessObject(typeof(DoctorDAO));
                    }

                    profile = doctorDAO.LoadDoctorByDoctorId(doctorId);
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




        public int ChangeDoctorActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeDoctorActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (doctorDAO == null)
                    {
                        doctorDAO = (DoctorDAO)DAOFactory.GetDataAccessObject(typeof(DoctorDAO));
                    }

                    result = doctorDAO.ChangeDoctorActiveStatus(id, isActive);
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



        public int ChangeDoctorLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeDoctorLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (doctorDAO == null)
                    {
                        doctorDAO = (DoctorDAO)DAOFactory.GetDataAccessObject(typeof(DoctorDAO));
                    }

                    result = doctorDAO.ChangeDoctorLockedStatus(id, isLocked);
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
