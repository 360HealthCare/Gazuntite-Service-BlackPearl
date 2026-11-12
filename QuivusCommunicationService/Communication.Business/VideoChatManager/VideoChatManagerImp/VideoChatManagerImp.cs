using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunicationDataAccess.VideoChat;
using Quivus.Utility.Factory;
namespace Communication.Business.VideoChatManager.VideoChatManagerImp
{
   public class VideoChatManagerImp:IVideoChatManager
    {

       VideoChatDAO videoChatDAO =null;
        public String GenerateCall(string callto, string callfrom,string clinicBranchID ,string opentokSessionId,string tokenID)
        {
            try
            {
                if (videoChatDAO == null)
                {
                    videoChatDAO = (VideoChatDAO)DAOFactory.GetDataAccessObject(typeof(VideoChatDAO));
                }
                String response =  videoChatDAO.SaveVideoCallInfo(callto, callfrom,clinicBranchID ,opentokSessionId,tokenID);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        public String UpdateVideoCall(string videoChatID,string callto, string callfrom, string opentokSessionId,string modifiedby)
        {
            try
            {
                if (videoChatDAO == null)
                {
                    videoChatDAO = (VideoChatDAO)DAOFactory.GetDataAccessObject(typeof(VideoChatDAO));
                }
                String response = videoChatDAO.UpdateVideoCallInfo(videoChatID,callto, callfrom, opentokSessionId,modifiedby);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        public String GetIncomingCallInfo(String callto, String clinicBranchID)
        {
            try
            {
                if (videoChatDAO == null)
                {
                    videoChatDAO = (VideoChatDAO)DAOFactory.GetDataAccessObject(typeof(VideoChatDAO));
                }
                String response = videoChatDAO.GetIncomingCallInfo(callto,clinicBranchID);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
    }
}
