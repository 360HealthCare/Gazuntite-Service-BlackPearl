using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI
{
    /// <summary>
    /// Author Mansoor
    /// Date 21 Mar 2011
    /// get hpi types static data
    /// </summary>        
    
    [DataContract]
    public class HPIDetailAutoCompleteResponse : ServiceResponse
    {
        [DataMember]
        public List<string> AutoCompleteList { get; set; }
    }
}
