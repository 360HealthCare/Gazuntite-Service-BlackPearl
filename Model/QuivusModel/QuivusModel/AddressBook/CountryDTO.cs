using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    [DataContract]
    public class CountryDTO : QuivusBaseDTO
    {
        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string CountryCode { get; set; }
    }
}
