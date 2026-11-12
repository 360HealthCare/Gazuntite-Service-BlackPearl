using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LiveProvider.Model.Appointment.Patient
{
    [DataContract]
    [XmlType(TypeName = "Insurance")]
    public class PatientInsuranceDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "UserId")]
        public int PatientInsuranceId { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserId")]
        public InsuranceTypeDTO InsuranceType { get; set; }

        //[DataMember]
        //public InsuranceNatureDTO InsuranceNature { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Copay")]
        public string Copay { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PatientId")]
        public int PatientId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "PolicyNo")]
        public string PolicyNumber { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "GroupNo")]
        public string GroupNumber { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Plan")]
        public string InsurancePlan { get; set; }

        //M private string insuredPersonName;

        [DataMember]
        [XmlAttribute(AttributeName = "InsuredName")]
        public string InsuredPersonName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "InsuredDOB")]
        public DateTime InsuredPersonDOB { get; set; }

        //[DataMember]
        //public GenderTypeDTO InsuredPersonGender { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EndDate")]
        public DateTime EndDate { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "InsuranceCoPay")]
        public string InsuranceCoPay { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "IsEligible")]
        public string IsEligible { get; set; } // handled as bool     

        [DataMember]
        [XmlAttribute(AttributeName = "EligibilityCheckOn")]
        public DateTime EligibilityCheckOn { get; set; } // handled as bool     

        //[DataMember]
        //public StatusTypeDTO InsuranceStatus { get; set; }

        //[DataMember]
        //public InsuranceOrganizationDTO InsuranceOrganization { get; set; }

        //[DataMember]
        //public ImageDTO InsuranceImage { get; set; }
    }
}
