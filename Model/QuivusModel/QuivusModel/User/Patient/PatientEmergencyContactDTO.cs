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
    public class PatientEmergencyContactDTO : QuivusBaseDTO
    {
        public PatientEmergencyContactDTO()
        {
            //RelationShipType = new RelationshipTypeDTO();
            //StatusType = new StatusTypeDTO();
            //AddressBook = new AddressBookDTO();
            //RecGuid = new Guid();
        }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public String FirstName { get; set; }

        [DataMember]
        public String LastName { get; set; }

        [DataMember]
        public int PatientEmergencyContactId { get; set; }

        [DataMember]
        public RelationshipTypeDTO RelationShipType { get; set; }

        [DataMember]
        public StatusTypeDTO StatusType { get; set; }

       
        [DataMember]
        public PhoneDTO EmergencyPhone { get; set; }

        [DataMember]
        public Guid RecGuid { get; set; }

        [DataMember]
        public bool AllowCommunication { get; set; }

   

        //[DataMember]
        //public PhoneDTO PhoneDTOObject { get; set; }
    }
}
