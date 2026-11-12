using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.Visit
{
    [DataContract]
    public class PatientProcedureModifierDTO
    {
        [DataMember]
        public int ProcedureModifierID { get; set; }

        [DataMember]
        public int PatientProcedureID { get; set; }

        [DataMember]
        public ProcedureModifierTypeDTO ProcedureModifierType { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public Boolean IsDeleted { get; set; }
        
    }
}
