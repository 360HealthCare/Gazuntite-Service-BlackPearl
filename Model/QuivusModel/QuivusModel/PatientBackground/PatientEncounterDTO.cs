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
    public class PatientEncounterDTO : QuivusBaseDTO
    {
        [DataMember]
        public int VisitId { get; set; }

        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public string VisitEncounterTypeName { get; set; }

        [DataMember]
        public string VisitProvider { get; set; }

        [DataMember]
        public string VisitStatus { get; set; }

        [DataMember]
        public string VisitNotes { get; set; }
        [DataMember]
        public int StationID { get; set; }

        [DataMember]
        public string StationName { get; set; }

        [DataMember]

        public int OwnerUserId { get; set; }

        [DataMember]

        public int OwnerUserTypeId { get; set; }

         [DataMember]
        public Guid DictationID { get; set; }
         [DataMember]
        public string CanSignCosign { get; set; }
         [DataMember]
        public string NoteSubject { get; set; }
         [DataMember]
        public string Notes { get; set; }
        
 
    }
}
