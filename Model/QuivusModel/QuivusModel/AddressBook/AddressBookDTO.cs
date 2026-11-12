using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// This is the main DTO which will encapsulate mutiple contact details for any Party.
    /// Contact details comprises of Phone as well as address.
    /// </summary>
    [DataContract]
    public class AddressBookDTO : QuivusBaseDTO
    {
        /// <summary>
        /// Primary key of the AdressBook
        /// </summary>
        [DataMember]
        public int AddressBookID { get; set; }

        [DataMember]
        public List<AddressDTO> AddressDTOlist { get; set; }

        [DataMember]
        public List<PhoneDTO> PhoneDTOlist { get; set; }
        
        [DataMember]
        public int[] DeletedPhoneIds { get; set; }

        [DataMember]
        public int[] DeletedAddressIds { get; set; }

        /// <summary>
        /// This is the ID of the party which is owner of that specific adress book data.
        /// Party can be Patient, Doctr or any Quivus user or contact for which the contact information is needed.
        /// </summary>
        [DataMember]
        public Guid PartyID { get; set; }

        /// <summary>
        /// Party Type explicitly identifies the nature of the party that weather its patient, guarantor, doctor, etc.
        /// </summary>
        [DataMember]
        public int AddressPartyTypeID { get; set; }

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
