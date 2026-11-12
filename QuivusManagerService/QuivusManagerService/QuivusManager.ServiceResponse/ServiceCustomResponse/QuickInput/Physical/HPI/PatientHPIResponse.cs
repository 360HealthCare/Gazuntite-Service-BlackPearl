using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI
{   
    /// <summary>
    /// Author Mansoor
    /// Date 21 March 2011 
    /// </summary>
    [DataContract]
    public class PatientHPIResponse : ServiceResponse
    {
        [DataMember]
        public List<HPI_CCDTO> HPIList { get; set; }
         [DataMember]
        public bool ResponseData { get; set; }
    }
}
