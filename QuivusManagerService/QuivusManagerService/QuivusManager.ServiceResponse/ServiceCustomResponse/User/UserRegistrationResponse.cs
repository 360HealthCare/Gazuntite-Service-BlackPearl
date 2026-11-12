using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.User
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Created Date 18 Jan 2011
    /// Class Used for User Registration Response
    /// </summary>
    [DataContract]
    public class UserRegistrationResponse : ServiceResponse
    {
        [DataMember]
        public UserDTO ResponseData { get; set; }
       
    }
}
