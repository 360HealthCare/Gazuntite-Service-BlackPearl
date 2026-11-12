using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using QuivusModel.TypeDTOs.BillingTypeDTOs;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "POSTypeResult")]
    public class PlaceOfServiceResultDTO
    {
        [DataMember]
        [XmlArrayAttribute("POSTypes")]
        public List<PlaceOfServiceTypeDTO> POSTypeList { get; set; }

        public PlaceOfServiceResultDTO()
        {
            if (POSTypeList == null)
            {
                POSTypeList = new List<PlaceOfServiceTypeDTO>();
            }
        }
    }
}
