using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization
{
    [DataContract]
    public class ImmunizationTypesResponse : ServiceResponse
    {
        [DataMember]
        public List<ImmunizationTypeDTO> ImmunzationTypeList { get; set; }
    }
}
