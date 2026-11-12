using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace QuivusModel.AddressBook
{
    [DataContract]
    public class PatientAddressBookInfoDTO : QuivusBaseDTO
    {
        [DataMember]
        public Guid RecGuid { get; set; }
        [DataMember]
        public int AddressBookID { get; set; }
        [DataMember]
        public string HomePhone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }

    }
}
