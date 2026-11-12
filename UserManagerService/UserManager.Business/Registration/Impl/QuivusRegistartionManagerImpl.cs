using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.User;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;
using Quivus.Utility;
using System.Configuration;
using Quivus.Utility.Logger;

using System.Reflection;

using UserManager.Business.Validators;
using Quivus.Utility.CustomException;
using Quivus.Model.User.Patient;

using Quivus.Model.TypeDTOs;
using Quivus.Model.Image;
using UserManager.Business.Utility;


namespace UserManager.Business.Registration.Impl
{
    public class QuivusRegistartionManagerImpl : IQuivusRegistartionManager
    {
        UserDAO userDAO = null;

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 18 jan 2011
        /// Method Used To Register User
        /// </summary>
        /// <param name="registrationDTO"></param>
        /// <returns></returns>
        [Obsolete]
        public UserDTO RegisterNewUser(RegistrationDTO registrationDTO)
        {
            UserDTO response = null;
            int UpdateResult = 0;
            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling RegisterNewUser:: " + registrationDTO);

                if (QRegistrationValidator.ValidateRegistrationInput(registrationDTO, ref sb))
                {

                    if (userDAO == null)
                    {
                        userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                    }

                    UpdateResult = userDAO.RegisterNewUser(registrationDTO);
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }// End Try Block

            catch
            {
                throw;
            }

            return response;
        }

        /// <summary>
        /// Author Muhaammad Ismail
        /// Created Date 24 Jan 2011
        /// This Method will  be used as an abstarct interface to save user that might belong 
        /// to any category i.e either patient or clinical User
        /// </summary>
        /// <param name="baseUserDTO"></param>
        /// <returns></returns>
        public int RegisterBaseUser(BaseUserDTO baseUserDTO)
        {
            int UpdateResult = 0;
            bool IsUpdate = false;
            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling RegisterBaseUser:: " + baseUserDTO);

                if (baseUserDTO != null && baseUserDTO.UserId > 0) // This Is Update Case 
                {
                    IsUpdate = true;
                }
                if (QRegistrationValidator.ValidateBaseUserInput(baseUserDTO, ref sb, IsUpdate))
                {
                    if (userDAO == null)
                    {
                        userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                    }

                    UpdateResult = userDAO.RegisterBaseUser(baseUserDTO, IsUpdate);

                    // Commented By Ismail Moved TO Streaming Service
                    //if (UpdateResult > 0)
                    //{
                    //    // Now Save User Image If Exist
                    //    if (baseUserDTO.Image != null)
                    //    {
                    //        baseUserDTO.Image.CreatedAt = DateTime.Now;
                    //        baseUserDTO.Image.CreatedBy = UpdateResult;
                    //        UpdateResult = ManageUserImage(baseUserDTO.Image);
                    //    }
                    //}
                }
                else
                {
                    // Validation Gets Failed Thorw Exception
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }// End Try Block
            catch 
            {
                throw;
            }

            return UpdateResult;
        }

        public int ManageUserImage(ImageDTO imageDTO)
        {

            int updateResult;
            try
            {
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                updateResult = userDAO.ManageUserImage(imageDTO);

            }
            catch          
            {
                throw;
            }

            return updateResult;

        }

        public bool VerifyUserEmail(String UserEmail)
        {
            if (userDAO == null)
            {
                userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
            }

            return userDAO.VerifyUserEmail(UserEmail);
        }

        /// <summary>
        /// Author Muhammad ismail
        /// Date Feb 11 2011
        /// Method Used To load Image Types
        /// </summary>
        /// <returns></returns>
        public List<ImageTypeDTO> LoadImageTypes()
        {
            List<ImageTypeDTO> response = null;

            try
            {
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                response = userDAO.LoadImageTypes();

            }
            catch 
            {
                throw;
            }
            return response;
        }

    }
}
