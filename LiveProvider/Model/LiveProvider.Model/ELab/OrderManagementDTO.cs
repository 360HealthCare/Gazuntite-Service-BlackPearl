using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.User.Patient;
using LiveProvider.Model.User.Provider;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.Test;

namespace LiveProvider.Model.ELab
{
    [DataContract]
    [XmlType(TypeName = "Lab")]
    public class OrderManagementDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "OID")]
        public int OrderID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public string OrderNumber { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Date")]
        public string OrderDate { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Patient")]
        public PatientDTO Patient { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Test")]
        public TestDTO Test { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Provider")]
        public ProviderDTO Provider { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Laboratory")]
        public string Laboratory { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LabID")]
        public int LaboratoryID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Status")]
        public string Status { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Result")]
        public ResultManagementDTO Result { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ModifiedBy")]
        public int ModifiedBy { get; set; }

    }
}
