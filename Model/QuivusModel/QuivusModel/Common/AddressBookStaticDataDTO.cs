using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.AddressBook;

namespace Quivus.Model.Common
{
     [DataContract]
    public class AddressBookStaticDataDTO : QuivusBaseDTO
    {
        [DataMember]
        public List<PhoneTypeDTO> PhoneTypes { get; set; }

        [DataMember]
        public List<PhoneNumberTypeDTO> PhoneNumberTypes { get; set; }      

        [DataMember]
        public List<CountryDTO> AllCountryList { get; set; }

        [DataMember]
        public List<StateDTO> AllStatesList { get; set; }

        //[DataMember]
        //public List<ZipCodeDTO> AllZipCodesList { get; set; }      

        [DataMember]
        public List<AddressPartyTypeDTO> AllAddressPartyTypeList { get; set; }
       
        [DataMember]
        public List<OwnerTypeDTO> PhoneOwnerTyeDTOList { get; set; }       
    }
}
