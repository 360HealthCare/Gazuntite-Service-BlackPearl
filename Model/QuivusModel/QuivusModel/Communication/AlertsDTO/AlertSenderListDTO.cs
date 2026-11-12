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
    /// Alert sender list
    /// </summary>
    
    [DataContract]
    public class AlertSenderListDTO
    {
        [DataMember]
        public int SenderListID { get; set; }

        [DataMember]
        public int AlertID { get; set; }

        [DataMember]
        public int UserID { get; set; }
    }
}
