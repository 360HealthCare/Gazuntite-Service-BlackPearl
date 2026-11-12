using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    [XmlRoot(ElementName = "ClaimTransactionResult")]
    public class ClaimTransactionSearchResultDTO
    {
        [DataMember]       
        [XmlArrayAttribute("ClaimTransactions")]
        public List<ClaimTransactionDTO> ClaimTransactionSearchList { get; set; }

        public ClaimTransactionSearchResultDTO()
        {
            if (ClaimTransactionSearchList == null)
            {
                ClaimTransactionSearchList = new List<ClaimTransactionDTO>();
            }
        }
    }
}
