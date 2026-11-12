using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Quivus.Model.TypeDTOs;
using QuivusModel.Billing.Visit;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "TOSTypeResult")]
    public class EncounterTypeResultDTO
    {
        [DataMember]
        [XmlArrayAttribute("TOSTypes")]
        public List<QBEncounterTypeDTO> TOSTypeList { get; set; }

        public EncounterTypeResultDTO()
        {
            if (TOSTypeList == null)
            {
                TOSTypeList = new List<QBEncounterTypeDTO>();
            }
        }
    }
}
