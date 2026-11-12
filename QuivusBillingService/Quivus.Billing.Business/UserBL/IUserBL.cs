using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.ServiceResponse.User;
using QuivusBilling.IntegratedServices.UserManagerService;

namespace Quivus.Billing.Business.UserBL
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/05/2011
    /// </summary>
    public interface IUserBL
    {

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/26/2012
        /// to get Patient info for super bill
        /// </summary>
        /// <param name="PatientIdList"></param>       
        /// <returns></returns>
        List<SuperBillPatientInfoDTO> GetPatientBillingInfoByPatientId(List<int> PatientIdList);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/06/2011
        /// to get Users basic info 
        /// </summary>
        /// <param name="UserIdList"></param>
        /// <returns></returns>
        UserBasicInfoResponse GetUserBasicInfoById(List<int> UserIdList);

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/06/2011
        /// to get quivus ids against patients 
        /// </summary>
        /// <param name="PatientIdList"></param>
        /// <returns></returns>
        PatientQuivusIdResponse GetQuivusIdsByPatientId(List<int> PatientIdList);
    }
}
