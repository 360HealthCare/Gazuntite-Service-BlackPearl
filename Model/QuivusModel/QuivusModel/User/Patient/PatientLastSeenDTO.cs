using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace QuivusModel.User.Patient
{
    [DataContract]
    public class PatientLastSeenDTO : QuivusBaseDTO
    {

        [DataMember]
        public DateTime LastSeen { get; set; }

        [DataMember]
        public int LastSeenByUserID { get; set; }
        [DataMember]
        public int LastSeenByUserTypeID { get; set; }
    }
}
