using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickLinks;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient;


namespace QuivusManager.Buisness.ClinicBL
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 15 Dec 2011
    /// This interface provides method for clinic,clinic branch and station
    /// </summary>
    public interface IClinicBL
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
    }
}
