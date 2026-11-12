using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.Claim
{
    [DataContract]
    [XmlRoot(ElementName = "ClaimResult")]
    //[XmlRoot(ElementName = "Claims"), XmlType(TypeName = "Claims")]
    public class ClaimSearchResultDTO
    {
        [DataMember]
        //[XmlArray("Claims")]
        [XmlArrayAttribute("Claims")]
        public List<ClaimSearchDTO> ClaimSearchList { get; set; }

        public ClaimSearchResultDTO()
        {
            if (ClaimSearchList == null)
            {
                ClaimSearchList = new List<ClaimSearchDTO>();
            }
        }

        //[DataMember]
        //public bool Status { get; set; }

        //[DataMember]
        //public string Message { get; set; }

    }
}
