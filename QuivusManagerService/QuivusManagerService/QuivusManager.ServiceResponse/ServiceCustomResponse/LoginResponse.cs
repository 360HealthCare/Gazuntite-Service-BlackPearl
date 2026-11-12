using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse
{
    /// <summary>
    /// Author Mansoor
    /// Date 14 Jan 2011
    /// This class is a custom wrapper of the servcie resposne
    /// </summary>
    [DataContract]
    public class LoginResponse : ServiceResponse
    {
        [DataMember]
        public LoginDTO ResponseData { get; set; }
    }
}