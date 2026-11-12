using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Quivus.Model.QuickLink;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;
using Quivus.Utility;
using System.Configuration;

using Quivus.Utility.Logger;
using System.Reflection;
using Quivus.Utility.CustomException;

using UserManager.Business.Utility;

namespace UserManagerService.Business.QuickLink
{
    public class QuickLinkManagerImpl : IQuickLinkManager
    {
        UserDAO userDAO = null;

        /// <summary>
        /// Author M.Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Quick Link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<QuickLinkDTO> LoadUserDefaultQuickLinksByLocation(int UserId, int ClinicBranchId)
        {

            List<QuickLinkDTO> QuickLinkList = null;
            try
            {
                QuivusLogger.Info("LoadUserDefaultQuickLinksByLocation Called PARAM UserId:: " + UserId + " ClinicBranchId:: " + ClinicBranchId);

                if (UserId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (ClinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                QuickLinkList = userDAO.LoadUserDefaultQuickLinksByLocation(UserId, ClinicBranchId);

            }
            catch
            {
                throw;
            }

            return QuickLinkList;
        }
    }
}