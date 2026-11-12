using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class HospiceEmployedTypeDTO
    {
        [DataMember]
        public int HospiceEmployedTypeID { get; set; }

        [DataMember]
        public string HospiceEmployedType { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
