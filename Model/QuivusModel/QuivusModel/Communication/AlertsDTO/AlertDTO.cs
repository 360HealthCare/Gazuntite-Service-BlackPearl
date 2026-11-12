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
    /// Alert information
    /// </summary>
    [DataContract(Namespace = "AlertSaving/")]
    //[DataContract(Namespace = "http://www.AlertCommunication.com/Alert")]
   public class AlertDTO
    {

        [DataMember]
        public int ClinicBranchID { get; set; }

        [DataMember]
        public int AlertTypeID { get; set; }

        [DataMember]
        public int userId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime AlertTime { get; set; }

        [DataMember]
        public DateTime AlertStartDate { get; set; }

        [DataMember]
        public DateTime AlertEndDate { get; set; }

        [DataMember]
        public bool IsReccurence { get; set; }

        [DataMember]
        public int RecurrenceID { get; set; }

        [DataMember]
        public int SeverityID { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public DateTime ModifiedAt { get; set; }

        [DataMember]
        public int ModuleID { get; set; }

        [DataMember]
        public int AlertStatusID { get; set; }

        [DataMember]
        public string AlertModificationReason { get; set; }

        [DataMember]
        public bool AlertRights { get; set; }

        [DataMember]
        public bool IsSnooze { get; set; }

        [DataMember]
        public int TimeInterval { get; set; }

        [DataMember]
        public DateTime ReminderTime { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public int AlertServiceTypeID { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }

        [DataMember]
        public bool IsDismissed { get; set; }

        [DataMember]
        public DateTime DismissDate { get; set; }

        [DataMember]
        public int Dismissedby { get; set; }
    }
}
