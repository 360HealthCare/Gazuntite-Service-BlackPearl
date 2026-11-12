using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp
{
    /// <summary>
    /// Author Mansoor 
    /// Date 25 Mar 2011
    /// </summary>

     [DataContract]
    public class FollowUpMethodTypesResponse : ServiceResponse
    {
        [DataMember]
        public List<FollowUpMethodTypeDTO> FolowUpMethodTypeList { get; set; }
    }
}
