using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.Common;

namespace LiveProvider.Model.Response
{
    [DataContract]
    [XmlRoot(ElementName = "PatientSearch")]
    public class PatientSearchDTOResponse
    {
        [DataMember]
        [XmlArrayAttribute("Patients")]
        public List<PatientSearchDTO> PatientSearchList { get; set; }

        [DataMember]
        [XmlAttribute("CurrentPageNo")]
        public int CurrentPageNo { get; set; }

        [DataMember]
        [XmlAttribute("CurrentFirstRecordNo")]
        public int CurrentFirstRecordNo { get; set; }

        [DataMember]
        [XmlAttribute("CurrentLastRecordNo")]
        public int CurrentLastRecordNo { get; set; }

        [DataMember]
        [XmlAttribute("TotalNoOfRecords")]
        public int TotalNoOfRecords { get; set; }
    }
}
