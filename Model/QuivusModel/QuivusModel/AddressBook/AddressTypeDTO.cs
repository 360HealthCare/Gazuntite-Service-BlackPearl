using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Identifies the address type.
    /// </summary>
    [DataContract]
    public class AddressTypeDTO : QuivusBaseDTO
    {

        [DataMember]
        public int AddressTypeID { get; set; }

        [DataMember]
        public string AddressTypeName { get; set; }

        [DataMember]
        public string AddressTypeDescription { get; set; }

    }
}
