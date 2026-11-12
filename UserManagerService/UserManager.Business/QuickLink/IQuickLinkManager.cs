using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickLink;



namespace UserManagerService.Business.QuickLink
{
    public interface IQuickLinkManager
    {
        #region Contracts
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load User Default Quick link List
        /// W.R.T location where he/she gets logged in
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ClinicBranchId"></param>
        List<QuickLinkDTO> LoadUserDefaultQuickLinksByLocation(int UserId, int ClinicBranchId);

        #endregion
    }
}
