using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace QuivusModel.Billing.PendingBills
{
    [DataContract]
    [XmlRoot(ElementName = "Question"), XmlType(TypeName = "Question")]
    public class CMS1500QuestionsDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "CMSID")]
        public int CMSID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ClaimID")]
        public int ClaimID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block10A")]
        public Boolean Block10A { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block10B")]
        public Boolean Block10B { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "BlockBStateCode")]
        public String BlockBStateCode { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block10C")]
        public Boolean Block10C { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block16DateFrom")]
        public DateTime Block16DateFrom { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block16DateTo")]
        public DateTime Block16DateTo { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block18DateFrom")]
        public DateTime Block18DateFrom { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block18DateTo")]
        public DateTime Block18DateTo { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block20")]
        public Boolean Block20 { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block20Charges")]
        public Decimal Block20Charges { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block22")]
        public String Block22 { get; set; }
        [DataMember]

        [XmlAttribute(AttributeName = "Block22RefNumber")]
        public String Block22RefNumber { get; set; }
        
        [DataMember]
        [XmlAttribute(AttributeName = "Block24B")]
        public String Block24B { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Block32")]
        public String Block32 { get; set; }
                
    }
}
