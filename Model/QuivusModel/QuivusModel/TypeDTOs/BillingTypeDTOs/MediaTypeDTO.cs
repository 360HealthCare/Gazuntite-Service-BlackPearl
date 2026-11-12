using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class MediaTypeDTO
    {
        [DataMember]
        public int MediaTypeID { get; set; }

        [DataMember]
        public string MediaType { get; set; }
                
    }
}
