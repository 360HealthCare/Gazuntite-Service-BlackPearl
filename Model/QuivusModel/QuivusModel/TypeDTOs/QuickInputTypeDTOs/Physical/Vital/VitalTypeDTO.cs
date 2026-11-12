using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
 

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Vital
{
    /// <summary>
    /// Author Mansoor
    /// Date 14 March 2011
    /// This class is a type dto for Vital types
    /// </summary>    
    [DataContract]
    public class VitalTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int VitalTypeId { get; set; }

        [DataMember]
        public string VitalTypeName { get; set; }

        [DataMember]
        public string UserAgeType { get; set; }
    }
}
