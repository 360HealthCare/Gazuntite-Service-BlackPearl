using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

using Quivus.Model.TypeDTOs;

namespace Quivus.Model.User.Patient
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Jan 2011
    /// </summary>
    [DataContract]
    public class PatientDTO : QuivusBaseDTO
    {
        
       
        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string QuivusId { get; set; }

        [DataMember]
        public int IVR_PINCODE { get; set; }

        [DataMember]
        public bool IsReportingExempt { get; set; }

        [DataMember]
        public bool AllowEmailCommunication { get; set; }

        [DataMember]
        public string MotherMaidenName { get; set; }

        [DataMember]
        public string FatherName { get; set; }

        [DataMember]
        public string SpouseName { get; set; }

        [DataMember]
        public string MedicareNumber { get; set; }

        [DataMember]
        public bool IsProfileComplete { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public RaceTypeDTO RaceType { get; set; }

        [DataMember]
        public EthinicityTypeDTO EthinicityType { get; set; }

        [DataMember]
        public OccupationTypeDTO OccupationType { get; set; }

        [DataMember]
        public MaritalStatusTypeDTO MaritalStatusType { get; set; }

        [DataMember]
        public StatusTypeDTO StatusType { get; set; }

        //M front end will send NULL/TRUE/FALSE
        [DataMember]
        public string AlcoholStatus { get; set; }

        [DataMember]
        public string AlcoholStatusData { get; set; }

        //M front end will send NULL/TRUE/FALSE
        [DataMember]
        public string DrugStatus { get; set; }

        [DataMember]
        public string DrugStatusData { get; set; }

        [DataMember]
        public string EmployerName { get; set; }

        [DataMember]
        public Guid RecGuid { get; set; }

        [DataMember]
        public SmokingTypeDTO SmokingType { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public string FileId { get; set; }


        [DataMember]
        public string DefaultPharmacyRCOPIAID { get; set; }

        [DataMember]
        public string RCOPIAID { get; set; }

        [DataMember]
        public string RCOPIAPracticeID { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public string SmokingStatus { get; set; }

        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string StrDOB { get; set; }

        [DataMember]
        public string PatientPhoneNumber { get; set; }

        //  [DataMember]
        //public bool IsAlcoholStatusYes { get; set; } // Not Used Handled using DrugStatusData of type string 

        //[DataMember]
        //  public bool IsDrugStatusYes { get; set; } Not Used Handled using AlcoholStatus of type string 


    }
}
