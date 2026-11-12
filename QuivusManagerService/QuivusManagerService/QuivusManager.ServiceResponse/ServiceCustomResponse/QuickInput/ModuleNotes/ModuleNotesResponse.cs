using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.ModuleNotes
{
    [DataContract]
    public class ModuleNotesResponse : ServiceResponse
    {
        [DataMember]
        public List<ModuleNoteDTO> ResponseList { get; set; }
    }
}
