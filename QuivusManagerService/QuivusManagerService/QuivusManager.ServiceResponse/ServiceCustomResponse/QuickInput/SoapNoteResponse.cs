using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput
{
    [XmlRoot(ElementName = "html")]
    public class SoapNoteResponse
    {
        [XmlElement(ElementName="body")]
        public string body { get; set; }
    }
}
