using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.PatientProfileCustomization
{
    [DataContract]
    public class PatientImmunizationInfoDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PatientImmunizationId { get; set; }        

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string ImmunizationName { get; set; }

        [DataMember]
        public string Series { get; set; }

        [DataMember]
        public DateTime AdminDate { get; set; }

        [DataMember]
        public DateTime NextDueDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
