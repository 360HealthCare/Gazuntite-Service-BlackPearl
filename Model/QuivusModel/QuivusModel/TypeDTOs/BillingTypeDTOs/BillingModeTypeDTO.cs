using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public  class BillingModeTypeDTO
    {
        [DataMember]
        public int BillingModeTypeID { get; set; }

        [DataMember]
        public string BillingModeType { get; set; }
        
    }
}
