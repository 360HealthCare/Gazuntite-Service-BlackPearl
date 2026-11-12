using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.User.ClinicUser;
using QuivusModel.Search;
namespace UserManager.Business.User.ClinicUser
{
    public interface IClinicUserManager
    {

        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>
        /// <param name="clinicUser"></param>
        /// <param name="userType"></param>
        /// <returns></returns>     
        int ManageClinicUserProfile(ClinicUserDTO clinicUser, int userTypeId, int clinicBranchId);

        /// <summary>
        /// Author Mansoor
        /// Date 05 Oct 2011        
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>        
        ClinicUserDTO LoadClinicUserProfile(int userId, int profileId, int userTypeId, int clinicBranchId);

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 17 Jan 2012
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        ClinicProviderSearchDTO LoadClinicUserBascInfo(int userId, int clinicBranchId);
        
    
    }
}
