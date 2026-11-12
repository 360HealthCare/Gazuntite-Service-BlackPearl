using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HL7.Model.Messages;
using System.ServiceModel.Web;
using HL7.Model.EngineResponses;

namespace HL7Service.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHL7Engine" in both code and config file together.
    [ServiceContract]
    public interface IHL7Engine
    {
        [OperationContract]
        [XmlSerializerFormat]
        [WebInvoke(UriTemplate = "ParseOrderMessage", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST")]
        OMLMessageResponse ParseOrderMessage(OMLMessage ORMMsg);
    }
}
