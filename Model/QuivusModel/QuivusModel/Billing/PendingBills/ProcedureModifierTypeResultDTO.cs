using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using QuivusModel.Billing.Visit;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "ProcedureModifierTypeResult")]
    public class ProcedureModifierTypeResultDTO
    {
        [DataMember]
        [XmlArrayAttribute("CPTModifierTypes")]
        public List<ProcedureModifierTypeDTO> ProcedureModifierTypeList { get; set; }

        public ProcedureModifierTypeResultDTO()
        {
            if (ProcedureModifierTypeList == null)
            {
                ProcedureModifierTypeList = new List<ProcedureModifierTypeDTO>();
            }
        }
    }
}
