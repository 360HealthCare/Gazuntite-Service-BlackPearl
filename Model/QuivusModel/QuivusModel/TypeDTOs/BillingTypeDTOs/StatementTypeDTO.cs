using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class StatementTypeDTO
    {
        [DataMember]
        public int StatementTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
