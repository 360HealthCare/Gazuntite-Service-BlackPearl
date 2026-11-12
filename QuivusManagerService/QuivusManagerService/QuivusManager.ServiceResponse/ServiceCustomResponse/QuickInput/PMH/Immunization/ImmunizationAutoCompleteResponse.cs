using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization
{
    /// <summary>
    /// Author Mansoor
    /// Date 18 Feb 2011
    /// get immunization types static data
    /// </summary>        
    [DataContract]
    public class ImmunizationAutoCompleteResponse : ServiceResponse
    {
        [DataMember]
        public List<string> AutoCompleteList { get; set; }
    }
}
