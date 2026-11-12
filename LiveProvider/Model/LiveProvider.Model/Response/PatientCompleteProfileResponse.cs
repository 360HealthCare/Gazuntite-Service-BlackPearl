using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using LiveProvider.Model.Appointment;

namespace LiveProvider.Model.Response
{  
    [DataContract]
    [XmlRoot(ElementName = "Response")]
    public class PatientCompleteProfileResponse
    {
        [DataMember]
        [XmlElement(ElementName = "Patient")]
        public PatientCompleteInfoDTO PatientCompleteInfo { get; set; }
    }
}
