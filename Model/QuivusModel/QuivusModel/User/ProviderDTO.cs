using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.User
{
    [DataContract]
    public class ProviderDTO : QuivusBaseDTO
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int StationId { get; set; }

        [DataMember]        
        public int ProviderId { get; set; }
    }
}
