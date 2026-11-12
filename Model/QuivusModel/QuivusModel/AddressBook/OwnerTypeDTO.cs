using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Identifies the Owner Type. [Example: Self, Father, Mother, Guardian, etc.]
    /// </summary>
    [DataContract]
    public class OwnerTypeDTO : QuivusBaseDTO
    {
        [DataMember]
        public int OwnerTypeID { get; set; }

        [DataMember]
        public string OwnerTypeName { get; set; }

        [DataMember]
        public string OwnerTypeDescription { get; set; }

    }
}
