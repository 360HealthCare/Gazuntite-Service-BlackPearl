using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusModel.Common;

namespace Quivus.Model.Clinic
{
    [DataContract]
    public class ClinicBranchDTO : QuivusBaseDTO
    {
        public ClinicBranchDTO()
        {
            //Clinic = new ClinicDTO();
        }

        [DataMember]
        public int ClinicBranchIdSeq { get; set; }

        [DataMember]
        public string ClinicBranchName { get; set; } // Practice Name

        //DataMember]
        //public ClinicDTO Clinic { get; set; }    

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string FaxNumber { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ClinicStatus { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string ContactPerson { get; set; }

        [DataMember]
        public string ZIP { get; set; }

        [DataMember]
        public StationDTO DefaultStation { get; set; }
    }
}