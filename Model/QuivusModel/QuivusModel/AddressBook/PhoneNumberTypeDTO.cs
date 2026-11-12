using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Determines the type of the number. [Example: Cell, Fax, Landline, etc.]
    /// </summary>
    [DataContract]
    public class PhoneNumberTypeDTO : QuivusBaseDTO
    {
        [DataMember]
        public int NumberTypeID { get; set; }

        [DataMember]
        public string NumberTypeName { get; set; }

        [DataMember]
        public string NumberTypeDescription { get; set; }

        [DataMember]
        public string NumberTypeCode { get; set; }
    }
}
