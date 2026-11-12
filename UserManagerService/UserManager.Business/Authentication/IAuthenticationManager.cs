using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quivus.Model.User;
using Quivus.Model.Common;
 
 

namespace UserManagerService.Business.Authentication
{
    /// <summary>
    /// 
    /// </summary>

    public interface IAuthenticationManager
    {

        #region Contracts
        
        /// <summary>
        /// Authenticate user for login and load the user primary data, its roles and registered locations
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        LoginDTO AutheticateUser(string userEmail, string userPassword);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Jan 2011
        /// Log the user login history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>

        void LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull);

        #endregion
    }
}
