using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QuivusManagerWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQuivusManagerWebService" in both code and config file together.
    [ServiceContract]
    public partial interface IQuivusManagerWebService
    {
        [OperationContract]
        bool PingService();

        [OperationContract]
        string PingAllClientServices();


        /// <summary>
        /// Author Mansoor Ud Din
        /// Created Date 18 Aug 2011
        /// This Method Is Used To Load the user login history        
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        //[OperationContract]
        //bool LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull);
    }
}
