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
    [XmlRoot(ElementName = "CPTMedicalTypeResult")]
    public class CPTMedicalTypeResultDTO
    {
         [DataMember]
         [XmlArrayAttribute("CPTMedicalTypes")]
         public List<QBCPTMedicalTypeDTO> CPTMedicalTypeList { get; set; }

         public CPTMedicalTypeResultDTO()
        {
            if (CPTMedicalTypeList == null)
            {
                CPTMedicalTypeList = new List<QBCPTMedicalTypeDTO>();
            }
        }
    }
}
