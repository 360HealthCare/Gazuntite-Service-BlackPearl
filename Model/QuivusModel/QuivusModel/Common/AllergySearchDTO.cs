using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model;

namespace Quivus.Model.Common
{
    /// <summary>
    /// Auhtor Mansoor
    /// Date 21 Jan 2011
    /// This is to store the response for the auto complete search like in registeration
    /// </summary>
    
    public class AllergySearchDTO : QuivusBaseDTO
    {   
        [DataMember]
        public int AllergyTypeIdSeq { get; set; }
        
        [DataMember]
        public string AllergyName { get; set; }

        //[DataMember]
        //public DateTime CreatedDate { get; set; }        
    }
}
