using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Quivus.Utility.Factory;
using UserManagerService.DataAccess;
using Quivus.Model.User;
using Quivus.Model.Common;
using Quivus.Utility.Logger;

using Quivus.Utility.CustomException;
 
using Quivus.Utility;

using UserManager.Business.Utility;

namespace UserManagerService.Business.Authentication.Impl
{
    public class AuthenticationManagerImpl : IAuthenticationManager
    {
        #region Data Fields

        UserDAO userDAO;

        #endregion

        #region Constructor

        public AuthenticationManagerImpl()
        {
            userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
        }

        #endregion

        #region IAuthentication Members
        

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 11 jan 2011
        /// This Method Verfiy the User Credential &
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public LoginDTO AutheticateUser(string userName, string userPassword)
        {
            LoginDTO response = null;
         
            try
            {                
                QuivusLogger.Info("AutheticateUser Called PARAM userName::" + userName + " userPassword::" + userPassword);

                if (String.IsNullOrEmpty(userName))
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (String.IsNullOrEmpty(userPassword))
                {
                    throw new InvalidArgumentException(ProjectConstant.PASSWORD_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                string hashPassword = PasswordHashUtil.ComputeHash(userPassword, ProjectConstant.HASH_ALGO_SHA1);

                response = userDAO.AutheticateUser(userName, userPassword, hashPassword);
            }
            catch 
            {
                //QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new LoginResponse();
                //ResponseErrorHandler.HandleException(response, exp);    
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 14 Jan 2011
        /// log the login histroy for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        public void LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull)
        {
            //LoginDTO loginDTO = null;
            try
            {
                QuivusLogger.Info("LogLoginHistory Called");


                if (userId <= 0)
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                else if (userType <=0 )
                    throw new InvalidArgumentException(ProjectConstant.USER_TYPE_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                else if (clinicBranch <= 0)
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);

                userDAO.LogLoginHistroy(userId, userType, clinicBranch, isSucessFull);
            }   
            catch 
            {
                throw;
            }
        }      

        #endregion
    }
}