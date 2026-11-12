using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Determines the type of the phone. [Example: Home, Office, etc.]
    /// </summary>
    [DataContract]
    public class PhoneTypeDTO : QuivusBaseDTO
    {
        [DataMember]
        public int PhoneTypeID { get; set; }

        [DataMember]
        public string PhoneTypeName { get; set; }

        [DataMember]
        public string PhoneTypeDescription { get; set; }

        [DataMember]
        public string PhoneTypeCode { get; set; }
    }
}
