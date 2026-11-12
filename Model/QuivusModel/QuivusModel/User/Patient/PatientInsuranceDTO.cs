using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model.TypeDTOs;
using Quivus.Model.Insurance;
using Quivus.Model.Image;
using System.ComponentModel;

namespace Quivus.Model.User.Patient
{
    [DataContract]
    public class PatientInsuranceDTO : QuivusBaseDTO
    {
        public PatientInsuranceDTO()
        {
            //InsuranceType = new InsuranceTypeDTO();
            //InsuranceNature = new InsuranceNatureDTO();
            //InsuredPersonGender = new GenderTypeDTO();
            //InsuranceStatus = new StatusTypeDTO();
            //InsuranceOrganization = new InsuranceOrganizationDTO();
            //InsuranceImage = new ImageDTO();
        }
        [DataMember]
        public int PatientInsuranceId { get; set; }

        [DataMember]
        public InsuranceTypeDTO InsuranceType { get; set; }

        [DataMember]
        public InsuranceNatureDTO InsuranceNature { get; set; }

        [DataMember]
        public string Copay { get; set; }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        public string PolicyNumber { get; set; }

        [DataMember]
        public string GroupNumber { get; set; }

        [DataMember]
        public string InsurancePlan { get; set; }

        //M private string insuredPersonName;

        [DataMember]
        public string InsuredPersonName { get; set; }        

        [DataMember]
        public DateTime InsuredPersonDOB { get; set; } 
        
        [DataMember]
        public GenderTypeDTO InsuredPersonGender { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string InsuranceCoPay { get; set; }

        [DataMember]
        public string IsEligible { get; set; } // handled as bool     

        [DataMember]
        public StatusTypeDTO InsuranceStatus { get; set; }

        [DataMember]
        public InsuranceOrganizationDTO InsuranceOrganization { get; set; }

        [DataMember]
        public ImageDTO InsuranceImage { get; set; }
    }
}
