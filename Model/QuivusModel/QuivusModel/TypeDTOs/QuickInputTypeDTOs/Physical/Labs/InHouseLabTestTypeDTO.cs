using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs
{
    [DataContract]
    public class InHouseLabTestTypeDTO : QuivusBaseTypeDTO
    {
        [DataMember]
        public int InHouseLabTestTypeID { get; set; }

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public string TestCode { get; set; }

        [DataMember]
        public string TestName { get; set; }

        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public string TestRange { get; set; }
    }
}
