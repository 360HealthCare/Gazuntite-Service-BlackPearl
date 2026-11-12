using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs
{
    [DataContract]
    public class CPTMedicalTypeDTO : QuivusBaseTypeDTO
    {

        [DataMember]
        public int CPTMedicalTypeID { get; set; }

        [DataMember]
        public string ProcedureCode { get; set; }

        [DataMember]
        public string ProcedureDescription { get; set; }
    }
}
