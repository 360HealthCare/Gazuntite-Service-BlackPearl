using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdpaterManagers.SSO
{
    /// <summary>
    /// 
    /// </summary>
    public class SingleSignonManger
    {
        private PartnerAuthenticationManger _userAuthenticationManager;
        private ANSManager _ansManager;


        public SingleSignonManger()
        {
            this._ansManager = new ANSManager();
            // T0DO:
            


        }
    }
}
