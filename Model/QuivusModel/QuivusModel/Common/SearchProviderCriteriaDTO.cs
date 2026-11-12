using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Common
{
    [DataContract]
    public class SearchProviderCriteriaDTO
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int ProviderTypeId { get; set; }

        [DataMember]
        public int SpecialityTypeId { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Zipcode { get; set; }
    }
}
