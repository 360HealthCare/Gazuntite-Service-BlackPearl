using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.User.Patient
{
    [DataContract]
    public class PatientUserInfo : QuivusBaseDTO
    {
        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int UserID { get; set; }
    }
}
