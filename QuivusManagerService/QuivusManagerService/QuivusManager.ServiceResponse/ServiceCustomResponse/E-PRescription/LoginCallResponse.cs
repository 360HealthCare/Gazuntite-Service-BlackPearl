using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription
{
     
   [DataContract]
    public class LoginCallResponse : ServiceResponse
    {
        [DataMember]
        public String ResponseData { get; set; }
    }
}

