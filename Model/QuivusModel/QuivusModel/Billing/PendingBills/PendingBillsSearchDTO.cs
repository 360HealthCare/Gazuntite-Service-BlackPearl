using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    public class PendingBillsSearchDTO
    {
        [DataMember]
        int VisitID { get; set; }

        [DataMember]
        int PatientID { get; set; }

        [DataMember]
        string PatientName { get; set; }

        [DataMember]
        int ProviderID { get; set; }

        [DataMember]
        string ProviderName { get; set; }

        [DataMember]
        DateTime DateOfService { get; set; }

        [DataMember]
        string BillPayment { get; set; } 
    }
}
