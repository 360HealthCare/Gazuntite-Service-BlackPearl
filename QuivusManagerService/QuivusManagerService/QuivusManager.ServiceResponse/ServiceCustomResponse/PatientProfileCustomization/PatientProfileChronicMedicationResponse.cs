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
    /// This class is a custom wrapper of the Patient Chronic Medication Info
    /// </summary>

    [DataContract]
    public class PatientProfileChronicMedicationResponse : ServiceResponse
    {
        [DataMember]
        public PatientProfileChronicMedicationDTO PatientProfileChronicMedication { get; set; }

        //[DataMember]
        //public List<PatientChronicMedicationInfoDTO> PatientChronicMedicationInfoData { get; set; }
    }
}
