using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.User.Patient
{
    [DataContract]
    public class PatientRcopiaDTO
    {
        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public String RecGuid { get; set; }
    }
}
