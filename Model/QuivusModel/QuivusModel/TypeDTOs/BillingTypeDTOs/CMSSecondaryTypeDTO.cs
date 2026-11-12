using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class CMSSecondaryTypeDTO
    {
        [DataMember]
        public int CMSSecondaryTypeID { get; set; }

        [DataMember]
        public string CMSSecondaryType { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
