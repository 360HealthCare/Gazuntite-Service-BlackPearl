using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.User
{
    [DataContract]
    public class BaseUserRegistrationResponse : ServiceResponse
    {
        [DataMember]
        public int UserId { get; set; }  // This Will Be User ID
    }
}
