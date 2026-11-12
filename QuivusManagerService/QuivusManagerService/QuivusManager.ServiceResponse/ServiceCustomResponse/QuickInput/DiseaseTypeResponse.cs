using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput
{
    [DataContract]
    public class DiseaseTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<DiseaseTypeDTO> DiseaseList { get; set; }
    }
}
