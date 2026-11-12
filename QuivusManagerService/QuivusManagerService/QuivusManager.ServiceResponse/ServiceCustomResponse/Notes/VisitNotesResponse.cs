using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.Notes
{
    /// <summary>
    /// Author Mansoor
    /// Date 03 Feb 2011
    /// This class is a custom wrapper of the Patient Allergy Info
    /// </summary>
    
    [DataContract]
    public class VisitNotesResponse : ServiceResponse
    {
        [DataMember]
        public List<ModuleNoteDTO> ModuleNoteData { get; set; }    
    }
}
