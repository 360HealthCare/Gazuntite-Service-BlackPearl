using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI
{
    //Author Mansoor
    //Date 21 Mar 2011

    [DataContract]
    public class HPITypeDTO : QuivusBaseTypeDTO
    {   
        [DataMember]
        public int HPIDataControlTypeID { get; set; }

        [DataMember]
        public string DataControlName { get; set; }
        
        [DataMember]
        public string Description { get; set; }
    }
}
