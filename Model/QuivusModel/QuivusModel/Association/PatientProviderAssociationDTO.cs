using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.Clinic;
using QuivusModel.Common;

namespace QuivusModel.Association
{
    [DataContract]
    public class PatientProviderAssociationDTO
    {
        [DataMember]
        public int PatientProviderID { get; set; }

        [DataMember]
        public int PatientID { get; set; }

        [DataMember]
        public int PatientUserID { get; set; }

        [DataMember]
        public int ProviderID { get; set; }

        [DataMember]
        public int ProviderUserID { get; set; }

        [DataMember]
        public int ProviderSpecialityID { get; set; }

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public string ProviderSepcialityName { get; set; }

        [DataMember]
        public string ProviderName { get; set; }

        [DataMember]
        public int ProviderTypeID { get; set; }

        [DataMember]
        public string ProviderTypeName { get; set; }

        [DataMember]
        public string WorkStart { get; set; }

        [DataMember]
        public string WorkEnd { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public bool IsAuthorized { get; set; }

        //[DataMember]
        //public List<StationDTO> ProviderLocations { get; set; }

        [DataMember]
        public int StationID { get; set; }

        [DataMember]
        public string StationName { get; set; }

        [DataMember]
        public string Address { get; set; }

    }
}
