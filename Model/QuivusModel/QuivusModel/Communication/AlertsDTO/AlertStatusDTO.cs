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
    /// Alert Status e.g, Approve,decline,dismiss,done
    /// </summary>
    [DataContract]
    public class AlertStatusDTO
    {
        [DataMember]
        public int AlertStatusID { get; set; }

        [DataMember]
        public string AlertStatusDescription { get; set; }
    }
}
