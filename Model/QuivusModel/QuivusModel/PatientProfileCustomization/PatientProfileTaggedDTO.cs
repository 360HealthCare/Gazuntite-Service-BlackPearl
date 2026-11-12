using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.PatientProfileCustomization;
using Quivus.Model.AddressBook;

namespace Quivus.Model.PatientProfileCustomization
{
    [DataContract]
    public class PatientProfileTaggedDTO
    {
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public PatientProfileBasicInfoDTO PatientProfileBasicInfoData { get; set; }

        [DataMember]
        public PhoneDTO PrimaryPhoneNo { get; set; }

        [DataMember]
        public PatientInsuracneInfoDTO PatientInsuracneInfoData { get; set; }
    }
}
