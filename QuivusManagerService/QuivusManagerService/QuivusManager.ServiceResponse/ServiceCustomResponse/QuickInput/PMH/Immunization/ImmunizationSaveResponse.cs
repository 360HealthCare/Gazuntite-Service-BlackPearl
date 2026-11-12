using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization
{
    [DataContract]
    public class ImmunizationSaveResponse : ServiceResponse
    {
        [DataMember]
        public int ImmunizationId { get; set; }
    }
}
