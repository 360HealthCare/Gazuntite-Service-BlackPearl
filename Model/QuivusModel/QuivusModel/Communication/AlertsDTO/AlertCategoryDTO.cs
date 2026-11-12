using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Communication.AlertsDTO
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 23 November 2011
    /// Alert categories e.g, inbox, outbox
    /// </summary>
    [DataContract]
   public class AlertCategoryDTO
    {
        [DataMember]
        public string AlertCategoryDescription { get; set; }
    }
}
