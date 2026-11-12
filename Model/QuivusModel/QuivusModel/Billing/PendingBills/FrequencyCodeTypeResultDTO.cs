using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using QuivusModel.TypeDTOs.BillingTypeDTOs;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "FrequencyCodeTypeResult")]
    public class FrequencyCodeTypeResultDTO
    {

        [DataMember]
         [XmlArrayAttribute("FrequencyCodeTypes")]
        public List<ClaimFrequencyCodeTypeDTO> FrequencyCodeTypeList { get; set; }

        public FrequencyCodeTypeResultDTO()
        {
            if (FrequencyCodeTypeList == null)
            {
                FrequencyCodeTypeList = new List<ClaimFrequencyCodeTypeDTO>();
            }
        }
    }
}
