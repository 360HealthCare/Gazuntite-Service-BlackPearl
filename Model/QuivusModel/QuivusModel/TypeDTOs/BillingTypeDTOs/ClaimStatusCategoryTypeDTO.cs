using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class ClaimStatusCategoryTypeDTO
    {
        [DataMember]
        public int ClaimStatusCategoryTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
