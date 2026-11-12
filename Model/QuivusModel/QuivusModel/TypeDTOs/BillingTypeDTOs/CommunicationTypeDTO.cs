using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.TypeDTOs.BillingTypeDTOs
{
    [DataContract]
    public class CommunicationTypeDTO
    {
        [DataMember]
        public int CommunicationTypeID { get; set; }

        [DataMember]
        public string CommunicationType { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
