using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.DocumentManagerService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.LiveProvider.DocumentManagement
{
    [DataContract]
    public class PatientDocumentResponse : ServiceResponse
    {
        [DataMember]
        public PaggingResponseDTO PatientDocument { get; set; }
    }
}
