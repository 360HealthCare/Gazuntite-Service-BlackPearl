using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.Insurance
{
    [DataContract]
    public class InsuranceOrganizationDTO : QuivusBaseDTO
    {
        [DataMember]
        public int InsuranceOrganizationID { get; set; }

        [DataMember]
        public string InsuranceOrganizationName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZIPCode { get; set; }

        [DataMember]
        public string StateCode { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string AlternatePhone { get; set; }

        [DataMember]
        public string FAX { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string ContactPerson { get; set; }

        [DataMember]
        public string IDNumber { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
