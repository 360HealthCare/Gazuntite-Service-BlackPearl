using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.UserManagerService;


namespace QuivusManager.ServiceResponse.ServiceCustomResponse.PatientProfileCustomization
{
    /// <summary>
    /// Author Mansoor
    /// Date 03 Feb 2011
    /// This class is a custom wrapper of the Patient Allergy Info
    /// </summary>

    [DataContract]
    public class PatientProfileAllergyResponse : ServiceResponse
    {
        [DataMember]
        public PatientProfileAllergyDTO PatientProfileAllergy { get; set; }
        //public int PatientId { get; set; }

        //[DataMember]
        //public List<PatientAllergyInfoDTO> PatientAllergyInfoData { get; set; }
    }
}
