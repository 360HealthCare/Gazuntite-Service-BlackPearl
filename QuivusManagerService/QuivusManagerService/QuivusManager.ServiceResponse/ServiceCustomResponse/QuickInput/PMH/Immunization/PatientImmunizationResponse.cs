using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Feb 2011
    /// get immunization types static data
    /// </summary>        

    [DataContract]
    public class PatientImmunizationResponse : ServiceResponse
    {
        [DataMember]
        public List<ImmunizationDTO> PatientImmunizationData { get; set; }
    }
}
