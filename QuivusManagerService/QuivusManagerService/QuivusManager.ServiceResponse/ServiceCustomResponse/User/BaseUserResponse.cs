using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.User
{
    [DataContract]
    public class BaseUserResponse : ServiceResponse
    {
        [DataMember]
        public BaseUserDTO ResponseData { get; set; }
    }
}
