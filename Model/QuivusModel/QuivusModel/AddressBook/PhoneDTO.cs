using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Contains the phone details for a given Address book entry.
    /// A single address book entry can have multiple phones associated with it.
    /// </summary>
    [DataContract]
    public class PhoneDTO : QuivusBaseDTO
    {
        /// <summary>
        /// Primary key of phone.
        /// </summary>
        [DataMember]
        public int PhoneID { get; set; }

        /// <summary>
        /// Foreign key of parent Address book entry.
        /// </summary>
        [DataMember]
        public int AddressBookID { get; set; }

        /// <summary>
        /// Determines the type of the phone. [Example: Home, Office, etc.]
        /// </summary>
        [DataMember]
        public int PhoneTypeID { get; set; }

        [DataMember]
        public string NumberTypeName { get; set; }

        //just for WPF binding.
        [DataMember]
        public string PhoneTypeName { get; set; }

        /// <summary>
        /// Determines the type of the number. [Example: Cell, Fax, Landline, etc.]
        /// </summary>
        [DataMember]
        public int NumberTypeID { get; set; }

        /// <summary>
        /// Determines the owner of the number. [Example: Mother, Father, etc.]
        /// </summary>
        [DataMember]
        public int OwnerTypeID { get; set; }

        /// <summary>
        /// Phone number string.
        /// </summary>
        [DataMember]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Extension of the phone, if any.
        /// </summary>
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        /// A given address book entry can have multiple phones associated with it, 
        /// but only one of them will be primary.
        /// In case of a single phone, it will be primary by default.
        /// </summary>
        [DataMember]
        public bool IsPrimary { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public int UpdatedBy { get; set; }

        [DataMember]
        public DateTime UpdatedAt { get; set; }
    }
}
