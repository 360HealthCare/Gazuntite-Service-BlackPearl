using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI
{
    [DataContract]
    public class HPIDataControlTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int HPIDataControlTypeID { get; set; }
        [DataMember]
        public string DataControlName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
