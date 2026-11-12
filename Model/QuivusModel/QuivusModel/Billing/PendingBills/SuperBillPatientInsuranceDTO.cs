using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "Insurance"), XmlType(TypeName = "Insurance")]
    public class SuperBillPatientInsuranceDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "InsOrganizationID")]
        public int InsOrganizationID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "InsuranceName")]
        public string InsuranceName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsEligible")]
        public string IsEligible { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "InsuranceTypeName")]
        public string InsuranceTypeName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PolicyNumber")]
        public String PolicyNumber { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "InsurancePlan")]
        public String InsurancePlan { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Copay")]
        public string Copay { get; set; }
    }
}
