using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class BillingStatusTypeDTO
    {
        [DataMember]
        public int BillingStatusTypeID { get; set; }

        [DataMember]
        public string BillingStatusType { get; set; }        
    }
}
