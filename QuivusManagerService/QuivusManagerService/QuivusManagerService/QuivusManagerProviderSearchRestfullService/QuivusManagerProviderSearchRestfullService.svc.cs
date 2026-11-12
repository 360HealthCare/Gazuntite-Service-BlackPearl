using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.UserBL;
using QuivusManager.Buisness.UserBL.Impl;
using Quivus.Utility.CustomException;

namespace QuivusManagerService.QuivusManagerProviderSearchRestfullService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QuivusManagerProviderSearchRestfullService" in code, svc and config file together.
    public class QuivusManagerProviderSearchRestfullService : IQuivusManagerProviderSearchRestfullService
    {
        IUserBL UserImpl = null;

        /// <summary>
        /// Author:Zahid Mehmood
        /// Date: 13 Jan 2012
        /// Description: This function Returns Contact list for Communication Provider Search List
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public List<ContactSearchDTO> LoadUsersByClinicBranchID(string clinicBranchId)
        {
            List<ContactSearchDTO> response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerRestFullService LoadUsersByClinicBranchID Called");
                QuivusLogger.Info("LoadUsersByClinicBranchID PARAM ClinicBranchId::" + clinicBranchId);

                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                response = UserImpl.LoadProviderByClinicBranchID(int.Parse(clinicBranchId));
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
            }
            catch (FaultException<Quivus.Utility.Fault.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 17 January 2012
        /// Description: Load Specific clinic user details for provider search in communication module.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>
        public ClinicProviderSearchDTO LoadUsersDetails(string userId, string clinicBranchId)
        {
            ClinicProviderSearchDTO response = null;
            try
            {
                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
               response =UserImpl.LoadClinicUserBasicInfo(int.Parse(userId), int.Parse(clinicBranchId));
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
            }
            catch (FaultException<Quivus.Utility.Fault.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
            }
            return response;
        }
}
}
