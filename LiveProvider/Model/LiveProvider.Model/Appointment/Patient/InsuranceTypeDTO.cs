using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment.Patient
{
    [DataContract]
    [XmlType(TypeName = "InsuranceType")]
    public class InsuranceTypeDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "InsuranceTypeId")]
        public int InsuranceTypeId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "InsuranceTypeName")]
        public string InsuranceTypeName { get; set; }
    }
}
