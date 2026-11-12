using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace QuivusModel.User.Patient
{
     [DataContract]
    public class PatientBasicInfoDTO : QuivusBaseDTO
    {
        [DataMember]
        public String QuivusId { get; set; }

        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }

    }
}
