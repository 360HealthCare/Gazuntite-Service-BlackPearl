using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class AcceptAssignmentTypeDTO
    {
        [DataMember]
        public int AcceptAssignmentTypeID { get; set; }

        [DataMember]
        public string AcceptAssignmentType { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}
