using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.User.Patient;
using Quivus.Model;

namespace QuivusModel.User.Patient
{
    [DataContract]
    public class ExternalPatientDTO 
    {
        [DataMember]
        public int TotalRecord { get; set; }

        [DataMember]
        public int FirstRecord { get; set; }
        [DataMember]
        public int LastRecord { get; set; }

        [DataMember]
        public List<ExternalPatientInfoDTO> PatientList { get; set; }

    }
}
