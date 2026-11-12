using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "WRSResult"), XmlType(TypeName = "WRSResult")]
    public class WRSResultDTO
    {
        [DataMember]
        [XmlArrayAttribute("WRSInfo")]
        public List<SuperBillWorkRelatedServicesDTO> WRSList { get; set; }

        public WRSResultDTO()
        {
            if (WRSList == null)
            {
                WRSList = new List<SuperBillWorkRelatedServicesDTO>();
            }
        }
    }
}
