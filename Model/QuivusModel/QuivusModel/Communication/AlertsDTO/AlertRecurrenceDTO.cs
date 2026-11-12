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
    /// Alert frequency or reoccurence
    /// </summary>
    [DataContract]
    public class AlertRecurrenceDTO
    {
        [DataMember]
        public int AlertID { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public int Hour { get; set; }

        [DataMember]
        public string RecurrencePattern { get; set; }

        [DataMember]
        public string RecurrenceWeekdays { get; set; }

        [DataMember]
        public DateTime SpecificDate { get; set; }
    }
}
