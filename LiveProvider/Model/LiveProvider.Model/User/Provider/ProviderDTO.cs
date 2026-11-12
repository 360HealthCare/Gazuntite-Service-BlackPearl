using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace LiveProvider.Model.User.Provider
{
    [DataContract]
    [XmlRoot(ElementName = "Provider")]
    public class ProviderDTO
    {
        [DataMember]
        [XmlAttribute(AttributeName = "ID")]
        public int ProviderID { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "FirstName")]
        public string ProviderFirstName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "LastName")]
        public string ProviderLastName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "MiddleName")]
        public string ProviderMiddleName { get; set; }
    }
}
