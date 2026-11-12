using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using QuivusModel.Security;
using QuivusModel.Security.AppDTO;
using QuivusModel.Security.AppDTO.VBDTO;
using QuivusModel.Security.AppDTO.QI;

namespace ServiceResponce.ResponceDTO
{
    [DataContract]
    [XmlType(TypeName = "Responce")]
    public class AccessRightResponce : BaseResponce
    {

        [DataMember]
        [XmlElement(ElementName = "QLink")]
        public QLinkDTO QLink { get; set; }


        [DataMember]
        [XmlElement(ElementName = "Communication")]
        public CommunicationDTO Communication { get; set; }


        [DataMember]
        [XmlElement(ElementName = "WebSearch")]
        public WebSearchDTO WebSearch { get; set; }



        [DataMember]
        [XmlElement(ElementName = "Profile")]
        public ProfileDTO Profile { get; set; }


        [DataMember]
        [XmlElement(ElementName = "SearchPatient")]
        public SearchPatientDTO SearchPatient { get; set; }


        [DataMember]
        [XmlElement(ElementName = "ImportDocument")]
        public ImportDocumentDTO ImportDocument { get; set; }


        [DataMember]
        [XmlElement(ElementName = "MostRecentPatient")]
        public MostRecentPatientDTO MostRecentPatient { get; set; }


        [DataMember]
        [XmlElement(ElementName = "Prescription")]
        public PrescriptionDTO Prescription { get; set; }


        [DataMember]
        [XmlElement(ElementName = "Notes")]
        public NotesDTO Notes { get; set; }


        [DataMember]
        [XmlElement(ElementName = "PatientProfile")]
        public PatientProfileDTO PatientProfile { get; set; }


        [DataMember]
        [XmlElement(ElementName = "ViewBackground")]
        public ViewBackgroundDTO ViewBackground { get; set; }


        [DataMember]
        [XmlElement(ElementName = "QI")]
        public QIDTO QI { get; set; }

    }
}
