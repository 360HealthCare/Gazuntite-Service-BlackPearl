using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusBilling.ServiceResponse;
using System.Runtime.Serialization;
using QuivusBilling.IntegratedServices.VisitService;
using System.Xml.Serialization;

namespace QuivusBilling.ServiceResponse.Visit
{
    [DataContract]
    [XmlRoot(ElementName = "PendingBills")]
    public class PendingBillsResponse : ServiceResponse
    {         
        [DataMember]
        [XmlArrayAttribute("PendingBill")]
        public List<PendingBillsDTO> PendingBillsList { get; set; }
        [DataMember]
        public bool ResponseData { get; set; }
    }
}
