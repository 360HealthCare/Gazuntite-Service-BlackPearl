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
    [XmlRoot(ElementName = "DiseaseTypeResult")]
    public class ICDCodeResultDTO
    {
        [DataMember]
        [XmlArrayAttribute("DiseaseTypes")]
         public List<QBICDCodeDTO> ICDCodeList { get; set; }

        public ICDCodeResultDTO()
        {
            if (ICDCodeList == null)
            {
                ICDCodeList = new List<QBICDCodeDTO>();
            }
        }
    }
}
