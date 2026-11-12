using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using System.Data;
using CommunicationDataAccess.DBUtililty;

namespace CommunicationDataAccess.VideoChat
{
   public  class VideoChatDAO
    {
       public String SaveVideoCallInfo(string callto, string callfrom, string clinicBranchID, string opentokSessionId, string tokenId)
        {
            try
            {
                 string sProc = CommunicationConstant.VIDEO_CHAT_INSERT_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@OpenTokSessionID", DbType.String, opentokSessionId);
                db.AddInParameter(command, "@TokenID", DbType.String, tokenId);
                db.AddInParameter(command, "@CallReciever", DbType.Int32, int.Parse(callto));
                db.AddInParameter(command, "@CallInitiator", DbType.Int32, int.Parse(callfrom));
                db.AddInParameter(command, "@clinicBranchID", DbType.Int32, int.Parse(clinicBranchID));

                db.AddOutParameter(command, "@VideoChatID", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                String videoChatID = db.GetParameterValue(command, "@VideoChatID").ToString();
                return videoChatID;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return "";
        }

        public String UpdateVideoCallInfo(string videoChatID,string callto, string callfrom, string opentokSessionId,string modifiedby)
        {
            try
            {
                string sProc = CommunicationConstant.VIDEO_CHAT_UPDATE_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@VideoChatID", DbType.Int32, int.Parse(videoChatID));
                db.AddInParameter(command, "@CallDuration", DbType.Time, Convert.ToDateTime("10:00"));
                //db.AddInParameter(command, "@CallInitiator", DbType.Int32, int.Parse(callfrom));
                //db.AddInParameter(command, "@OpenTokSessionID", DbType.Int32, int.Parse(opentokSessionId));
                db.AddInParameter(command, "@ModifiedBy", DbType.Int32, int.Parse(modifiedby));
                
                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                return "Call Ended";
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
                String response = null;
                string sProc = CommunicationConstant.GET_VIDEO_CHAT_SESSION_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);
                IDataReader objIReader = null;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@CallReciever", DbType.Int32, int.Parse(callto));
                db.AddInParameter(command, "@CallReciever", DbType.Int32, int.Parse(clinicBranchID));

                using (objIReader = db.ExecuteReader(command))
                {
                    response = DataReaderToDTOConverter.ConvertToSessionString(objIReader);
                 //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                return response;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
