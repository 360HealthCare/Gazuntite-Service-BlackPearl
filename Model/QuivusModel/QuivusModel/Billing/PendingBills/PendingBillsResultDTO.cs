using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "PendingBillsResult")]
    public class PendingBillsResultDTO
    {
         [DataMember]
         [XmlArrayAttribute("PendingBills")]
        public List<PendingBillsDTO> PendingBillsList { get; set; }

         public PendingBillsResultDTO()
        {
            if (PendingBillsList == null)
            {
                PendingBillsList = new List<PendingBillsDTO>();
            }
        }
    }
}
