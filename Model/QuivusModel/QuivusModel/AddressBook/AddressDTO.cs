using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.AddressBook
{
    /// <summary>
    /// Contains the Address information.
    /// There can be multiple addresses for a given addressbook entry in the system.
    /// </summary>
    [DataContract]
    public class AddressDTO : QuivusBaseDTO
    {
        /// <summary>
        /// Primary key of the Address.
        /// </summary>
        [DataMember]
        public int AddressID { get; set; }

        /// <summary>
        /// Foreign key of Address book's parent record.
        /// </summary>
        [DataMember]
        public int AddressBookID { get; set; }

        /// <summary>
        /// Determines the owner of the number. [Example: Mother, Father, etc.]
        /// </summary>
        [DataMember]
        public int OwnerTypeID { get; set; }

        /// <summary>
        /// Foreign key of Address type. [Exapmle: Office, Home]
        /// </summary>
        [DataMember]
        public int AddressTypeID { get; set; }

        /// <summary>
        /// Name of the Address type.
        /// </summary>
        [DataMember]
        public string AddressTypeName { get; set; }

        /// <summary>
        /// Street No to identify the house/office/flat/appartment number.
        /// </summary>
        [DataMember]
        public string StreetNo { get; set; }

        /// <summary>
        /// Name of the street/road.
        /// </summary>
        [DataMember]
        public string StreetName { get; set; }

        /// <summary>
        /// For a given Address book record only a single record can be primary, if there is only one address
        /// goven, it will be primary by default.
        /// </summary>
        [DataMember]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Foreign key of the corresponding Zip Code.
        /// </summary>
        [DataMember]
        public int ZipCodeID { get; set; }

        /// <summary>
        /// City code of the corresponding city based on Zip Code ID.
        /// </summary>
        [DataMember]
        public string CityCode { get; set; }

        /// <summary>
        /// City name of the corresponding city based on Zip Code ID.
        /// </summary>
        [DataMember]
        public string CityName { get; set; }

        /// <summary>
        /// Zip Code based on Zip Code ID.
        /// </summary>
        [DataMember]
        public string ZipCode { get; set; }

        /// <summary>
        /// Foreign key of the state.
        /// </summary>
        [DataMember]
        public int StateID { get; set; }

        /// <summary>
        /// State name of the corresponding state based on State ID.
        /// </summary>
        [DataMember]
        public string StateName { get; set; }

        /// <summary>
        /// State code of the corresponding state based on State ID.
        /// </summary>
        [DataMember]
        public string StateCode { get; set; }

        /// <summary>
        /// Foreign key of the country.
        /// </summary>
        [DataMember]
        public int CountryID { get; set; }

        /// <summary>
        /// Country name of the corresponding country based on country ID.
        /// </summary>
        [DataMember]
        public string CountryName { get; set; }

        /// <summary>
        /// Country code of the corresponding country based on country ID.
        /// </summary>
        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public List<AddressPhoneDTO> AddressPhoneDTOlist { get; set; }

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
