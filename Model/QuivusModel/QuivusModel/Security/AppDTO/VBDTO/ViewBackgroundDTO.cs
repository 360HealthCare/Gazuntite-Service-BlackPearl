using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using QuivusModel.Security.AppDTO.VBDTO.PMH;
using QuivusModel.Security.AppDTO.VBDTO.Physical;
using QuivusModel.Security.AppDTO.VBDTO.Assesment;
using QuivusModel.Security.AppDTO.BaseDTO;

namespace QuivusModel.Security.AppDTO.VBDTO
{
    [DataContract]
    [XmlType(TypeName = "ViewBackground")]
    public class ViewBackgroundDTO : ModuleARBaseDTO   
    {

        [DataMember]
        [XmlElement(ElementName = "CreateVisit")]
        public CreateVisitDTO CreateVisit { get; set; }


       [DataMember]
       [XmlElement(ElementName = "LoadHistory")]
        public LoadHistoryDTO LoadHistory { get; set; }


       [DataMember]
       [XmlElement(ElementName = "EncounterList")]
        public EncounterListDTO EncounterList { get; set; }


        [DataMember]
        [XmlElement(ElementName = "DocumentList")]
        public DocumentListDTO DocumentList { get; set; }



       [DataMember]
       [XmlElement(ElementName = "VBPMH")]
        public VBPMHDTO VBPMH { get; set; }


        [DataMember]
        [XmlElement(ElementName = "VBPhysical")]
       public VBPhysicalDTO VBPhysical { get; set; }


       [DataMember]
       [XmlElement(ElementName = "VBAssesment")]
        public VBAssesmentDTO VBAssesment { get; set; }

    }
}
