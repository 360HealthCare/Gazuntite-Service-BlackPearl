using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Security.AppDTO.BaseDTO
{
   
    [DataContract]
    [XmlType(TypeName = "ModuleARBaseDTO")]
    public class ModuleARBaseDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ModuleName")]
        public string ModuleName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModuleId")]
        public int ModuleId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ParentId")]
        public int ParentId { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "IsViewAllowed")]
        public bool IsViewAllowed { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "IsEditAllowed")]
        public bool IsEditAllowed { get; set; }


        [DataMember]
        [XmlAttribute(AttributeName = "RoleAppModuleAccessRightIdSeq")]
        public int RoleAppModuleAccessRightIdSeq { get; set; }
    }
}
