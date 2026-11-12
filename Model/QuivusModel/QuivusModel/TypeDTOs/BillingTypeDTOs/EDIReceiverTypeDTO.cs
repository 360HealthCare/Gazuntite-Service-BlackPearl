using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class EDIReceiverTypeDTO
    {
        [DataMember]
        public int EDIReceiverTypeID { get; set; }

        [DataMember]
        public string EDIReceiverType { get; set; }
                
    }
}
