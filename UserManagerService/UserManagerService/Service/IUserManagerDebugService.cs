using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Quivus.Utility.Fault;


namespace UserManagerService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserManagerDebugService" in both code and config file together.
    [ServiceContract]
    public interface IUserManagerDebugService
    {
        //  [OperationContract(Name="TestMethod")] Commented By Ismail
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        string Test();
    }
}
