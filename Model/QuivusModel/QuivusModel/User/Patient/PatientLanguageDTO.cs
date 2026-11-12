using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.AddressBook;
using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientLanguageDTO : QuivusBaseDTO
    {
        public PatientLanguageDTO()
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

        [DataMember]
        public bool IsPreffered { get; set; }

        // only need for WPF binding
        //[DataMember]
        //public bool IsPrimary { get; set; }

        [DataMember]
        public int PatientLanguageId { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public LanguageAbilityModeDTO PatientLanguageAbilityMode { get; set; }

        [DataMember]
        public CountryDTO Country { get; set; }       

       

    }
}
