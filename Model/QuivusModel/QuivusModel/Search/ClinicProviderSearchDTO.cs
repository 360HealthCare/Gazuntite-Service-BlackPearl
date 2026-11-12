using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Search;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace QuivusModel.Search
{
    [DataContract]
   public class ClinicProviderSearchDTO:ContactSearchDTO
    {
        ///////  FederalTaxId
        //[],GT.,,,,UD.,UD.,UD.
        //,UD.,UD.,UD.,UD.,I.
        [DataMember]
        [XmlAttribute(AttributeName = "SSN")]
        public string SSN { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Gender")]
        public string GenderName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LicenseNumber")]
        public string LicenseNumber { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "EIN")]
        public string EIN { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "NPI")]
        public int NPI { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "DAE")]
        public string DAE { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "UPIN")]
        public string UPIN { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "TPI")]
        public string TPI { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "WorkDays")]
        public string WorkDays { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "WorkStart")]
        public string WorkStart { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "WorkEnd")]
        public string WorkEnd { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "RecGuid")]
        public string RecGuid { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FederalTaxId")]
        public string FederalTaxId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "ImageData")]
        public Byte[] ImageData { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Phone")]
        public string Phone { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "State")]
        public string State { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Address")]
        public string Address { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "City")]
        public string City { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Zip")]
        public string Zip { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }

    }
}
