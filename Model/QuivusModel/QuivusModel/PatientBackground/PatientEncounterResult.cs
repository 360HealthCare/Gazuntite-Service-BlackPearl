using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.PatientBackground
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// this will contain the data related to patient visit lists, acting as light weight DTO
    /// </summary>

    [DataContract]
    public class PatientEncounterResult
    {
        [DataMember]
        public int TotalRecords  { get; set; }

        [DataMember]
        public List<PatientEncounterDTO> PatientEncounterList { get; set; }
    }
}
