using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Date 02 Feb 2011
    /// Contains the Patient profile info data
    /// </summary>

    [DataContract] 
    public class PatientProfileResponse : ServiceResponse
    {
        [DataMember]
        public PatientProfileDTO PatientProfileResponseData { get; set; }  // This Will ReponseData
    }
}
