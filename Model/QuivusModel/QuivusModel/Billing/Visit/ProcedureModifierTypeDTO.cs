using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Visit
{
    [DataContract]
    [XmlRoot(ElementName = "CPTModifierType"), XmlType(TypeName = "CPTModifierType")]
    public class ProcedureModifierTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ModifierTypeId")]
        public int ModifierTypeId { get; set; }        

        [DataMember]
        [XmlAttribute(AttributeName = "ModifierDescription")]
        public string ModifierDescription { get; set; }
    }
}
