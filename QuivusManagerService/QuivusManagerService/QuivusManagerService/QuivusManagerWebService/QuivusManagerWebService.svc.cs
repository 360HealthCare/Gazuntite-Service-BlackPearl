using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Quivus.Utility.Logger;
using QuivusManager.Buisness.CommonBL;
using QuivusManager.Buisness.CommonBL.Impl;
using QuivusManagerService.QuivusManagerService;

namespace QuivusManagerWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QuivusManagerWebService" in code, svc and config file together.
    public partial class QuivusManagerWebService : QuivusBaseService, IQuivusManagerWebService
    {
        public string PingAllClientServices()
        {
            string result = "";
            //  Logger.Write("New Error ", "Error", 1, 1, System.Diagnostics.TraceEventType.Error);
            QuivusLogger.Debug("Test Email Manager Working");

            ICommonBL common = new CommonBLImpl();
            result = common.PingAllServices();
            return result;
        }
    }
}
