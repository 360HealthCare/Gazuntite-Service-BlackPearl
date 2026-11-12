using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using QuivusModel.Security.AppDTO.BaseDTO;
namespace QuivusModel.Security.AppDTO.QI.Assesment
{
    [DataContract]
    [XmlType(TypeName = "QIAssesment")]
    public class QIAssesmentDTO : ModuleARBaseDTO
    {
    }
}
