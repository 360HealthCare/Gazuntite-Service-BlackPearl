using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisitService
{
    /// <summary>
    /// All the Service Classes have to extend this class, and override the GetServiceVersion()
    /// </summary>
    
    public abstract class QuivusBaseService
    {
        public abstract string GetServiceVersion();

        public bool PingService()
        {
            return true;
        }
    }
}