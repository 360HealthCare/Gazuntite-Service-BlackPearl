using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Quivus.Model;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.QuickInput.Procedure;

namespace QuivusModel.Billing.Visit
{
    [DataContract]
    public class QBProcedureDTO : ProcedureDTO
    {        
        [DataMember]
        public Boolean IncludeInBilling { get; set; }

        [DataMember]
        public Boolean IsCreatedFromSuperBill { get; set; }

        //[DataMember]
        //public List<PatientProcedureModifierDTO> ProcedureModifierList { get; set; }

        [DataMember]
        public String Modifier1 { get; set; }
        [DataMember]
        public String Modifier2 { get; set; }
        [DataMember]
        public String Modifier3 { get; set; }
        [DataMember]
        public String Modifier4 { get; set; }

        [DataMember]
        public int ModifierTypeId1 { get; set; }
        [DataMember]
        public int ModifierTypeId2 { get; set; }
        [DataMember]
        public int ModifierTypeId3 { get; set; }
        [DataMember]
        public int ModifierTypeId4 { get; set; }

        [DataMember]
        public Decimal Fee { get; set; }

        [DataMember]
        public int Unit { get; set; }

    }
}
