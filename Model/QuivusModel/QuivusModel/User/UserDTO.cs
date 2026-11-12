using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Common;
using System.ComponentModel;
using Quivus.Model.Image;
using Quivus.Model.User.Patient;
using Quivus.Model.User;
using Quivus.Model.AddressBook;
using Quivus.Model.Clinic;

namespace Quivus.Model.User
{
    /// <summary>
    /// Not in use, old one
    /// </summary>

    [Obsolete]

    [DataContract]
    public class UserDTO : QuivusBaseDTO
    {
        public UserDTO()
        {

        }


        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public PrefixTypeDTO Prefix { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string Suffix { get; set; }
        [DataMember]
        public string UserEmail { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
        [DataMember]
        public GenderTypeDTO Gender { get; set; }
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public StatusTypeDTO AccountStatus { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string LicenseNumber { get; set; }
        [DataMember]
        public string FederalTaxId { get; set; }
        [DataMember]
        public string EIN { get; set; }
        [DataMember]
        public ImageDTO Image { get; set; }
        [DataMember]
        public List<UserTypeDTO> AllUserTypes { get; set; }
        [DataMember]
        public PatientDTO PatientDTO { get; set; }
        [DataMember]
        public List<ClinicBranchDTO> UserRegisteredAtClinicBranchesList { get; set; }
        [DataMember]
        public ClinicBranchDTO UserSelectedBranch { get; set; }
        [DataMember]
        public List<UserTypeDTO> SelectedBranchUserTypeList { get; set; } // roles at selected branch
        [DataMember]
        public UserTypeDTO ActiveUserType { get; set; }
        [DataMember]
        public Dictionary<int, List<UserTypeDTO>> ClinicBranchUserTypeList { get; set; }
        [DataMember]
        public DoctorDTO DoctorDTO { get; set; }
        [DataMember]
        public NursePractitionerDTO NursePractitionerDTO { get; set; }
        [DataMember]
        public NurseDTO NurseDTO { get; set; }



        //[DataMember]
        //  public AdminDTO SecurityAdminDTO { get; set; }
        [DataMember]

        public BillingAssistantDTO BillingAssistantDTO { get; set; }
        [DataMember]
        public ReceptionistDTO ReceptionistDTO { get; set; }
        [DataMember]
        public LabTechnicianDTO LabTechnicianDTO { get; set; }
        [DataMember]
        public PracticeManagerDTO PracticeManagerDTO { get; set; }
        [DataMember]
        public AddressBookDTO UserAddressBookDTO { get; set; }
    }
}
