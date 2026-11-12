using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.AddressBook;
using Quivus.Model;

namespace QuivusModel.User.Patient
{
    [DataContract]
    public class ExternalPatientInfoDTO 
    {



        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public Guid RecGuid { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }
        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }



      //  public AddressBookDTO AddressBookInfo { get; set; }
    }
}
