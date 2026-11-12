using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace QuivusManagerService.QuivusManagerService
{
    public abstract class QuivusBaseService
    {
        //public abstract string GetServiceVersion();

        public bool PingService()
        {
            
            return true;
        }
    }
}