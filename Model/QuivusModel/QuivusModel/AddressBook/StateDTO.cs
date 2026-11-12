using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    [DataContract]
    public class StateDTO : QuivusBaseDTO
    {
        [DataMember]
        public int StateID { get; set; }

        [DataMember]
        public string StateName { get; set; }

        [DataMember]
        public string StateCode { get; set; }

        [DataMember]
        public int CountryID { get; set; }
    }
}
