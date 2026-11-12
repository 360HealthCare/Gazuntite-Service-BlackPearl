using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Business.VideoChatManager
{
   public interface IVideoChatManager
    {
       String GenerateCall(String callto, String callfrom,string clinicBranchID, String opentokSessionId,String tokenID);

       String UpdateVideoCall(String videoChatID, String callto, String callfrom, String opentokSessionId, String modifiedby);

       String GetIncomingCallInfo(String callto, String clinicBranchID);
    }
}
