using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals
{
    [DataContract]
    public class PhysicalSubCategoryTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<PhysicalSubCategoryTypeDTO> ResponseList { get; set; }
    }
}
