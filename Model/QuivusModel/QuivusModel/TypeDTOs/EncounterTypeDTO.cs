using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs
{
    [DataContract]
    public class EncounterTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int VisitEncounterTypeID { get; set; }

        [DataMember]
        public string EncounterTypeName { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}


