using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Quivus.Model.Clinic;
using Quivus.Model.TypeDTOs;
using Quivus.Model;
using Quivus.Model.Visit;

namespace QuivusModel.Billing.Visit
{
   /// <summary>
   ///added by Shahid Munir for claim in Billing service
   ///Date: 01/27/2012
   /// </summary>
    [DataContract]
    public class QBVisitDTO : VisitDTO 
    {

        public QBVisitDTO()
        {
            
        }

        [DataMember]
        public int PlaceOfServiceTypeID { get; set; }

        [DataMember]
        public bool IsClaimCreated { get; set; }

        [DataMember]
        public DateTime ClaimCreatedDate { get; set; }
    }
}
