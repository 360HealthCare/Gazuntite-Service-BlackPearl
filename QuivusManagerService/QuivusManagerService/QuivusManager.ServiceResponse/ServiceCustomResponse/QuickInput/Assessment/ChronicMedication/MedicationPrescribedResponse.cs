using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication
{
    [DataContract]
    public class MedicationPrescribedResponse : ServiceResponse
    {
        [DataMember]
        public List<MedicationPrescribedDTO> ResponseData { get; set; }
    }
}
