using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagerService.DataAccess;
using Quivus.Utility.Factory;
using Quivus.Model.SearchEngine;

using System.Configuration;
using System.Reflection;
using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;
 
using UserManager.Business.Utility;

namespace UserManagerService.Business.SearchEngine.Impl
{
    public class SearchEngineManagerImpl : ISearchEngineManager
    {
        UserDAO userDAO = null;

        /// <summary>
        /// Author M.Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        public List<SearchEngineDTO> LoadUserSearchEnginebyLocation(int userId, int clinicBranchId)
        {
            List<SearchEngineDTO> SearchengineList = null;
            try
            {
                QuivusLogger.Info("LoadUserSearchEnginebyLocation Called PARAM userId::" + userId + "clinicBranchId:: " + clinicBranchId);
                if (userId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (clinicBranchId < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                SearchengineList = userDAO.LoadUserSearchEngineByLocation(userId, clinicBranchId);

            }
            catch
            {
                throw;
            }

            return SearchengineList;
        }


        /// <summary>
        /// Author M.Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load all Search Engine List
        /// </summary>

        public List<SearchEngineDTO> LoadAllSearchEngines()
        {
            List<SearchEngineDTO> SearchengineList = null;
            try
            {
                QuivusLogger.Info("LoadAllSearchEngines Called");
                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                SearchengineList = userDAO.LoadAllSearchEngines();
            }
            catch
            {
                throw;
            }
            return SearchengineList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 17 Jan 2011
        /// Method used to set Deafult Search enigne for user
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        public int SetSearchEngineAsDefault(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID)
        {
            int UpdateResult = 0;
            try
            {
                QuivusLogger.Info("SetSearchEngineAsDefault Called PARAM ProviderUserID:: " + ProviderUserID
                    + " ClinicBranchID:: " + ClinicBranchID + " UserSearchEngineID:: " + UserSearchEngineID);

                if (ProviderUserID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.USER_ID_INVALID_ERROR_MSG);
                }
                else if (ClinicBranchID < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG);
                }
                else if (UserSearchEngineID < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.SEARCH_ENGINE_ID_INVALID_ERROR_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                UpdateResult = userDAO.SetSearchEngineAsDefault(ProviderUserID, ClinicBranchID, UserSearchEngineID);
            }
            catch
            {
                throw;
            }

            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Created jan 17 2011
        /// Method used to add Search engine to user list
        /// Changed 07 Feb 2011
        /// Changed by Mansoor
        /// Params removed [int UserSearchEngineID, bool IsDefault] //M
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="SearchEngineTypeIDs"></param>
        /// <param name="UserSearchEngineID"></param>
        /// <param name="IsDefault"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy)
        {
            int UpdateResult = 0;
            try
            {
                QuivusLogger.Info("ManageUserSearchEngine Called PARAM ProviderUserID:: " + ProviderUserID + " ClinicBranchID:: " + ClinicBranchID +
                    "SearchEngineTypeIDs:: " + SearchEngineTypeIDs + "CreatedBy:: " + CreatedBy);

                if (ProviderUserID <= 0 || ClinicBranchID < 0 || CreatedBy < 0 || SearchEngineTypeIDs == null || SearchEngineTypeIDs.Count <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }

                UpdateResult = userDAO.ManageUserSearchEngine(ProviderUserID, ClinicBranchID, SearchEngineTypeIDs, CreatedBy);
            }
            catch
            {
                throw;
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Muhammad ismail 
        /// Date 09 Feb 2011
        /// Method Used TO clear Search engines Added by User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="ModifiedBy"></param>
        /// <returns></returns>
        public int ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy)
        {
       
            int UpdateResult = 0;
            try
            {
                QuivusLogger.Info("ClearUserAddedSearchEngines Called PARAM ProviderUserID:: " + ProviderUserID + " ClinicBranchID:: " + ClinicBranchID +
                     "ModifiedBy:: " + ModifiedBy);

                if (ProviderUserID <= 0 || ClinicBranchID < 0 || ModifiedBy < 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG);
                }

                if (userDAO == null)
                {
                    userDAO = (UserDAO)DAOFactory.GetDataAccessObject(typeof(UserDAO));
                }
                UpdateResult = userDAO.ClearUserAddedSearchEngines(ProviderUserID, ClinicBranchID, ModifiedBy);
            }
            catch 
            {
                throw;
            }
            return UpdateResult;
        }
    }// Class 
} // NameSpace