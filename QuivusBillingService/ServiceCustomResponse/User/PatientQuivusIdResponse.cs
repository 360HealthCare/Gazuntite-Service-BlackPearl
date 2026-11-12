using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusBilling.IntegratedServices.UserManagerService;

namespace QuivusBilling.ServiceResponse.User
{
    [DataContract]
    public class PatientQuivusIdResponse : ServiceResponse
    {
        [DataMember]
        public List<PatientQuivusIDDTO> PatientQuivusIdList { get; set; }
        [DataMember]
        public bool ResponseData { get; set; }
    }
}
