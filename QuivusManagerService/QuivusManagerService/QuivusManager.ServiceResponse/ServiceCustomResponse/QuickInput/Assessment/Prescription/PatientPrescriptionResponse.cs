using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription
{
    [DataContract]
    public class PatientPrescriptionResponse : ServiceResponse
    {
        [DataMember]
        public List<PrescriptionDTO> ResponseList { get; set; }

    }
}
