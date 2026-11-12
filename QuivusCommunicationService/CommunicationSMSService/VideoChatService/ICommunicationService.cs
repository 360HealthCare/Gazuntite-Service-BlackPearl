using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.Xml;
namespace QuivusCommunicationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVideoChatService" in both code and config file together.
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 27 Jan 2012
    /// Description: Video Chat
    /// </summary>
    public partial interface ICommunicationService
    {

      

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveVideoSession/", Method = "POST")]//{callto}/{callfrom}/{opentokSessionId}/{tokenID}")] /////[WebInvoke(UriTemplate = "SaveVideoSession/{callto}/{callfrom}/{opentokSessionId}", Method = "Get")]
        String GenerateCall(XmlDocument videochatInfo);//, String callfrom, String opentokSessionId, String tokenID);

        [OperationContract]
        [WebGet(UriTemplate = "UpdateVideoSession/{videoChatID}/{callto}/{callfrom}/{opentokSessionId}/{modifiedby}")]
        String EndCall(String videoChatID, String callto, String callfrom, String opentokSessionId, String modifiedby);

        [OperationContract]
        [WebGet(UriTemplate = "GetVideoSession/{callto}/{clinicBranchID}")]  ///{callto}/{opentokSessionId}   Later on user these param also
        String GetIncomingCallInfo(String callto, String clinicBranchID);
    }
}
