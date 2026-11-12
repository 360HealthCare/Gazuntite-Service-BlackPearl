using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HL7Service
{
    public abstract class HL7BaseService
    {
        public abstract string GetServiceVersion();

        public bool PingService()
        {
            return true;
        }
    }
}