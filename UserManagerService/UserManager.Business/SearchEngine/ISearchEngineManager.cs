using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.SearchEngine;
 
 

namespace UserManagerService.Business.SearchEngine
{
    public interface ISearchEngineManager
    {
        #region Contracts
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Search Engine List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        List<SearchEngineDTO> LoadUserSearchEnginebyLocation(int UserId, int ClinicBranchId);


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load All Search Engine List
        /// </summary>
        /// 
        List<SearchEngineDTO> LoadAllSearchEngines();
        #endregion


        /// <summary>
        /// Author Muhammad Ismail
        /// 17 Jan 2011 Created
        /// Method Used TO Set Default Search Engine For User
        /// </summary>
        /// <param name="ProviderUserID"></param>
        /// <param name="ClinicBranchID"></param>
        /// <param name="UserSearchEngineID"></param>
        int SetSearchEngineAsDefault(int ProviderUserID, int ClinicBranchID, int UserSearchEngineID);

        int  ManageUserSearchEngine(int ProviderUserID, int ClinicBranchID, List<int> SearchEngineTypeIDs, int CreatedBy);

        int ClearUserAddedSearchEngines(int ProviderUserID, int ClinicBranchID, int ModifiedBy);
    }
}
