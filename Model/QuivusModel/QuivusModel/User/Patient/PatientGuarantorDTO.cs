using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.AddressBook;
using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientGuarantorDTO : QuivusBaseDTO
    {
        public PatientGuarantorDTO()
        {
            //RelationshipType = new RelationshipTypeDTO();
            //GenderType = new GenderTypeDTO();
            //AddressBook = new AddressBookDTO();
            //StatusType = new StatusTypeDTO();
            //MaritalStatusType = new MaritalStatusTypeDTO();
            //RecGuid = new Guid();
        }

        [DataMember]
        public int GuarantorId { get; set; }

        [DataMember]
        public String GurantorFirstName { get; set; }

        [DataMember]
        public String GuarantorLastName { get; set; }

        [DataMember]
        public GenderTypeDTO GenderType { get; set; }

        [DataMember]
        public RelationshipTypeDTO RelationshipType { get; set; }

        [DataMember]
        public DateTime DOB { get; set; }

        [DataMember]
        public StatusTypeDTO StatusType { get; set; }

        [DataMember]
        public AddressBookDTO AddressBook { get; set; }

        [DataMember]
        public MaritalStatusTypeDTO MaritalStatusType { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public Guid RecGuid { get; set; }

        //[DataMember]
        //public int CreatedBy { get; set; }

      

     
    }
}
