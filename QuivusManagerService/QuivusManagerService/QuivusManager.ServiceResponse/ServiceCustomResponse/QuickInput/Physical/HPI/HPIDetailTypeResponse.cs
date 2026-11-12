using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI
{
    //Author Mansoor
    //Date 21 Mar 2011

    [DataContract]
    public class HPIDetailTypeResponse : ServiceResponse
    {
        [DataMember]
        public List<HPITypeDTO> ResponseList { get; set; }
    }    
}
