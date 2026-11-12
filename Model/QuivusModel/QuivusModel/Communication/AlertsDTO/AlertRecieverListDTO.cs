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
    /// Alert reciever list
    /// </summary>
    [DataContract]
  public class AlertRecieverListDTO
    {
        [DataMember]
        public int RecieverListID { get; set; }

        [DataMember]
        public int AlertID { get; set; }

        [DataMember]
        public int UserID { get; set; }
    }
}
