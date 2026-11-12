using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuivusDictationService
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