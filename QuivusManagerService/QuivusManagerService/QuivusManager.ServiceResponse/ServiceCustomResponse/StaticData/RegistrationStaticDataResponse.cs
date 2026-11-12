using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.StaticData
{
    /// <summary>
    /// Author Mansoor
    /// Date 19 Jan 2011
    /// This class is a custom wrapper of the servcie resposne
    /// </summary>

    [DataContract]
    public class RegistrationStaticDataResponse : ServiceResponse
    {
        [DataMember]
        public RegisterationStaticDataDTO ResponseData { get; set; }
    }
}
