using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Billing.PendingBills;
using QuivusModel.Billing.User;

namespace UserManager.Business.Billing
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date:   01/31/2012
    /// Billing Service related Mehtods
    /// </summary>
    public interface IBillingManager
    {
        // Billing Service related Mehtods

        /// <summary>
        /// Author: Shahid Munir
        /// Date:   01/26/2012
        /// to get Patient info for super bill
        /// </summary>
        /// <param name="PatientIdList"></param>       
        /// <returns></returns>
        List<SuperBillPatientInfoDTO> GetPatientBillingInfoByPatientId(List<int> PatientIdList);
                
    }
}
