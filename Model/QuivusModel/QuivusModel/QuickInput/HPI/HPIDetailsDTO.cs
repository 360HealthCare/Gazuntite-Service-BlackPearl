using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.QuickInput.HPI
{
    //Author Mansoor
    //Date 21 Mar 2011
    [DataContract]
    public class HPIDetailsDTO
    {
        [DataMember]
        public string Symptom { get; set; }

        [DataMember]
        public String Onset { get; set; } // This field is updated from DateTime to String As per Requirment By Ismail July 14 2011

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Quality { get; set; }

        [DataMember]
        public string Severity { get; set; }

        [DataMember]
        public string Context { get; set; }

        [DataMember]
        public string ModifyingFactors { get; set; }

        [DataMember]
        public string Exacerbating { get; set; }

        [DataMember]
        public string Progression { get; set; }

        [DataMember]
        public string Radiation { get; set; }
    }
}
