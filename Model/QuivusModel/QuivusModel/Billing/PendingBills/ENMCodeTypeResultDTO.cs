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
    [XmlRoot(ElementName = "ENMCodeTypeResult")]
    public class ENMCodeTypeResultDTO
    {
        [DataMember]
         [XmlArrayAttribute("ENMCodeTypes")]
         public List<ENMCodeTypeDTO> ENMCodeTypeList { get; set; }

        public ENMCodeTypeResultDTO()
        {
            if (ENMCodeTypeList == null)
            {
                ENMCodeTypeList = new List<ENMCodeTypeDTO>();
            }
        }
    }
}
