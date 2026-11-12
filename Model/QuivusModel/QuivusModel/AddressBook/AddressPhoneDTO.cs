using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// This DTO will contain the phone number associated with a specific address.
    /// </summary>
    [DataContract]
    public class AddressPhoneDTO : QuivusBaseDTO
    {

        /// <summary>
        /// Primary key of the address phone.
        /// </summary>
        [DataMember]
        public int AddressPhoneID { get; set; }

        [DataMember]
        public PhoneDTO PhoneDTO { get; set; }

        /// <summary>
        /// Foreign key of the associated phone details.
        /// </summary>
        [DataMember]
        public int PhoneID { get; set; }

        /// <summary>
        /// Foreign key of the associated address details.
        /// </summary>
        [DataMember]
        public int AddressID { get; set; }

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
