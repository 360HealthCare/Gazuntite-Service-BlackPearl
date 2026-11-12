using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Quivus.Utility;

namespace QuivusManager.Buisness.Utility
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 08 Mar 2011
    /// Class Used To Read Quivus Settings :)
    /// </summary>
    public class QConfigurationManager
    {
        public static String SessionTimeOut
        {
            get
            { // By this Approch you Can Also Read From DB or Other source Also Do Not Need To Change Actual Code :)
                return ConfigurationManager.AppSettings[ProjectConstant.SESSION_TIME_OUT].ToString();
            }
            
        }
    }
}
