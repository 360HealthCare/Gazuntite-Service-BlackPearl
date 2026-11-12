using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Identifies the address party type.
    /// </summary>
    [DataContract]
    public class AddressPartyTypeDTO : QuivusBaseDTO
    {
        [DataMember]
        public int AddressPartyTypeID { get; set; }

        [DataMember]
        public string AddressPartyName { get; set; }

        [DataMember]
        public string AddressPartyDescription { get; set; }
    }
}
