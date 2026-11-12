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
    /// Alert severity e.g, (Urgent, High,Normal …)	or critical, warning, information		
    /// </summary>
    [DataContract]
   public class AlertSeverityDTO
    {
        [DataMember]
        public string AlertSeverityDescription { get; set; }
    }
}
