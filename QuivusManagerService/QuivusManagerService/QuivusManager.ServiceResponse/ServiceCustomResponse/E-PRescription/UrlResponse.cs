using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription
{
    [DataContract]
    public class UrlResponse : ServiceResponse
    {
        [DataMember]
        public String AllergyUrl { get; set; }

        [DataMember]
        public String PrescriptionUrl { get; set; }

        [DataMember]
        public String MedicationUrl { get; set; }
    }
}
