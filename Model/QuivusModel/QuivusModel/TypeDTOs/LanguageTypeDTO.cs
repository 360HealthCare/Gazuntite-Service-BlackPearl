using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.AddressBook;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class LanguageTypeDTO : QuivusBaseTypeDTO
    {
        public LanguageTypeDTO()
        {
           // PatientLAnguageAbilityMode = new LanguageAbilityModeDTO();
           // Country = new CountryDTO();
        }

        [DataMember]
        public int LanguageID { get; set; }

        [DataMember]
        public string LanguageName { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }

        //M [DataMember]
        //M public bool IsPreffered { get; set; }

        // only need for WPF binding
        //[DataMember]
        //public bool IsPrimary { get; set; }

        //M [DataMember]
        //M public int PatientLanguageId { get; set; }

        //M [DataMember]
        //M public int PatientId { get; set; }

        //M [DataMember]
        //M public LanguageAbilityModeDTO PatientLAnguageAbilityMode { get; set; }

        //M [DataMember]
        //M public CountryDTO Country { get; set; }
    }
}
