using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient
{
    [DataContract]
    public class PatientResponse : ServiceResponse
    {
        [DataMember]
        public PatientDTO ResponseData { get; set; }  // This Will ReponseData
    }
}
