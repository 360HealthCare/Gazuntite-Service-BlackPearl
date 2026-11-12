using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.User;
using Quivus.Model.AddressBook;

namespace QuivusModel.User.ClinicUser
{
    /// <summary>
    /// Author Mansoor
    /// Date 05 Oct 2011
    /// Contains the Clinic User Profile Data
    /// </summary>

    [DataContract]
    public class ClinicUserDTO
    {

        [DataMember]
        public BaseUserDTO UserDTO { get; set; }

        [DataMember]
        public DoctorDTO DoctorDTO { get; set; }


        [DataMember]
        public NursePractitionerDTO NursePractitionerDTO { get; set; }


        [DataMember]
        public LabTechnicianDTO LabTechnicianDTO { get; set; }


        [DataMember]
        public ReceptionistDTO ReceptionistDTO { get; set; }

        [DataMember]
        public PracticeManagerDTO PracticeManagerDTO { get; set; }

        [DataMember]
        public BillingAssistantDTO BillingAssistantDTO { get; set; }

        [DataMember]
        public SecurityAdminDTO SecurityAdminDTO { get; set; }


        [DataMember]
        public JnrSecurityAdminDTO JnrSecurityAdminDTO { get; set; }

        [DataMember]
        public StudentDTO StudentDTO { get; set; }

        [DataMember]
        public BillingManagerDTO BillingManagerDTO { get; set; }


        [DataMember]
        public NurseDTO NurseDTO { get; set; }

        [DataMember]
        public AddressBookDTO UserAddressBookDTO { get; set; }
    }
}
