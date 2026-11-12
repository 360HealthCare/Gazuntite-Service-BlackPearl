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
    /// Alert Inbox
    /// </summary>
    [DataContract]
   public class AlertInboxDTO
    {
        [DataMember]
        public int RecieverListID { get; set; }
    }
}
