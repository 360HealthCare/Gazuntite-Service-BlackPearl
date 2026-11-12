using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Clinic;

namespace UserManager.Business.Clinic
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 15 Dec 2011
    /// This interface provides method for clinic,clinic branch and station
    /// </summary>
   
    public interface IClinicManager 
    {
        /// <summary>
        /// Author Sami
        /// Date 15 Dec 2011      
        /// This method will return clinic, clinicbranch and station info for soap note
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="clinicBranchId"></param>
        ClinicDTO LoadClinicBranchAndStationInfo(int stationId);

        /// <summary>
        /// HAssam
        /// loads CB ids
        /// </summary>
        /// <returns></returns>
        List<int> LoadAllClinicBranch();



        /// <summary>
        /// HAssam
        /// loads ClinicBranchUsers ids
        /// </summary>
        /// <returns></returns>
        List<int> LoadClinicBranchUsers(int userTypeId, int clinicBranchId);
    }
}
