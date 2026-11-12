using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using QuivusModel.Security.AppDTO.QI.PMH;
using QuivusModel.Security.AppDTO.QI.Physical;
using QuivusModel.Security.AppDTO.QI.Assesment;
using QuivusModel.Security.AppDTO.BaseDTO;
namespace QuivusModel.Security.AppDTO.QI
{
    [DataContract]
    [XmlType(TypeName = "QI")]
    public class QIDTO : ModuleARBaseDTO
    {

        [DataMember]
        [XmlElement(ElementName = "QIPMH")]
        public QIPMHDTO QIPMH { get; set; }


       [DataMember]
       [XmlElement(ElementName = "QIPhysical")]
        public QIPhysicalDTO QIPhysical { get; set; }


       [DataMember]
       [XmlElement(ElementName = "QIAssesment")]
       public QIAssesmentDTO QIAssesment { get; set; }


    }
}
