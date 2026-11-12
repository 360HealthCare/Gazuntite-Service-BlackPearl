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

namespace UserManager.Business.User.ClinicUser.Student
{
    public class StudentManager
    {
        StudentDAO dao = null;
        public int ManageStudentProfile(int userId, StudentDTO ltDTO)
        {
            int profileId = 0;
            bool IsUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ManageStudentProfile Called PARAM userId::" + userId);

                if (ltDTO.StudentIdSeq > 0)
                {
                    IsUpdate = true;
                }


                if (true) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (StudentDAO)DAOFactory.GetDataAccessObject(typeof(StudentDAO));
                    }

                    profileId = dao.ManageStudentProfile(userId, ltDTO, IsUpdate);
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


        public StudentDTO LoadStudentDTOById(int id)
        {
            StudentDTO profile = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("LoadStudentDTOById Called PARAM Id::" + id);


                if (id > 0) //M QPatientValidator.ValidatePatientBasicInfo(patientDTO, ref sb))
                {
                    if (dao == null)
                    {
                        dao = (StudentDAO)DAOFactory.GetDataAccessObject(typeof(StudentDAO));
                    }

                    profile = dao.LoadStudentDTOById(id);
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



        public int ChangeStudentActiveStatus(int id, bool isActive)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeStudentActiveStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (StudentDAO)DAOFactory.GetDataAccessObject(typeof(StudentDAO));
                    }

                    result = dao.ChangeStudentActiveStatus(id, isActive);
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



        public int ChangeStudentLockedStatus(int id, bool isLocked)
        {
            int result = 0;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("ChangeStudentLockedStatus Called PARAM Id::" + id);


                if (id > 0)
                {
                    if (dao == null)
                    {
                        dao = (StudentDAO)DAOFactory.GetDataAccessObject(typeof(StudentDAO));
                    }

                    result = dao.ChangeStudentLockedStatus(id, isLocked);
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
