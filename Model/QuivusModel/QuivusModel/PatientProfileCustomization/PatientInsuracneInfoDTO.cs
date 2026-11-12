using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.PatientProfileCustomization
{
    /// <summary>
    /// This is a light weight DTO which contains the patient insuracne info, for tagged windows 
    /// </summary>

    [DataContract]
    public class PatientInsuracneInfoDTO : QuivusBaseDTO
    {

        [DataMember]
        public int PatientInsuranceId { get; set; }

        [DataMember]
        public string InsuranceOrganizationName { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string IsEligible { get; set; }

        [DataMember]
        public int InsuranceBalance { get; set; }

        [DataMember]
        public int PatientBalance { get; set; }

        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public string InsuranceTypeName { get; set; }
        [DataMember]
        public String PolicyNumber { get; set; }

    }
}
