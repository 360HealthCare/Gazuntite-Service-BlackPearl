using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using QuivusModel.Security.AppDTO.BaseDTO;

namespace QuivusModel.Security.AppDTO
{
    [DataContract]
    [XmlType(TypeName = "Communication")]
    public class CommunicationDTO : ModuleARBaseDTO
    {
    }
}
