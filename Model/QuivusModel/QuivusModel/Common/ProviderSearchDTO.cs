using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Common
{
    [DataContract]
    public class ProviderSearchDTO
    {
        [DataMember]
        public int ProviderUserId { get; set; }

        [DataMember]
        public int ProviderId { get; set; }

        [DataMember]
        public int ProviderTypeId { get; set; }

        [DataMember]
        public string ProviderTypeName { get; set; }

        [DataMember]
        public string ProviderName { get; set; }

        [DataMember]
        public int SpecialityId { get; set; }

        [DataMember]
        public string SpecialityName { get; set; }

        [DataMember]
        public int ClinicBranchId { get; set; }

        [DataMember]
        public int StationId { get; set; }

        [DataMember]
        public string StationName { get; set; }

        [DataMember]
        public string StationAddress { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Zip { get; set; }
    }
}
