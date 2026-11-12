using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Dictation.DataAccess;
using System.Data;

namespace Dictation.DataAccess.DAO
{
    public static class DictationDAO
    {
        public static void SaveDictation(Guid DictationID, byte[] DictationData, DateTime RecordTimeStamp, Guid SessionID, DateTime CreatedAt, int CreatedBy)
        {

            string sProc = DBConstants.DICTATION_INSERT_SP;
            Database db = DatabaseFactory.CreateDatabase(); // Load default DB
            DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
            db.AddInParameter(command, "@DictationID", DbType.Guid, DictationID);
            db.AddInParameter(command, "@DictationData", DbType.Binary, DictationData);
            db.AddInParameter(command, "@RecordTimeStamp", DbType.Boolean, RecordTimeStamp);
            db.AddInParameter(command, "@SessionID", DbType.Guid, SessionID);
            db.AddInParameter(command, "@CreatedAt", DbType.DateTime, CreatedAt);
            db.AddInParameter(command, "@CreatedBy", DbType.Int32, CreatedBy);


            if (db.ExecuteNonQuery(command) == 0) 
            {
                //no record inserted 
            }


        }

        public static void UpdateDictation(Guid DictationID, byte[] DictationData, DateTime RecordTimeStamp, Guid SessionID, DateTime CreatedAt, int CreatedBy)
        {

            string sProc = DBConstants.DICTATION_UPDATE_SP;
            Database db = DatabaseFactory.CreateDatabase(); // Load default DB
            DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
            db.AddInParameter(command, "@DictationID", DbType.Guid, DictationID);
            db.AddInParameter(command, "@DictationData", DbType.Binary, DictationData);
            db.AddInParameter(command, "@RecordTimeStamp", DbType.Boolean, RecordTimeStamp);
            db.AddInParameter(command, "@SessionID", DbType.Guid, SessionID);
            db.AddInParameter(command, "@CreatedAt", DbType.DateTime, CreatedAt);
            db.AddInParameter(command, "@CreatedBy", DbType.Int32, CreatedBy);


            if (db.ExecuteNonQuery(command) == 0)
            {
                //no record inserted 
            }


        }

        public static void MarkDictationAsDeleted(Guid DictationID, Guid SessionID, DateTime CreatedAt, int CreatedBy)
        {

            string sProc = DBConstants.DICTATION_MARK_AS_DELETED_SP;
            Database db = DatabaseFactory.CreateDatabase(); // Load default DB
            DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
            db.AddInParameter(command, "@DictationID", DbType.Guid, DictationID);
            db.AddInParameter(command, "@SessionID", DbType.Guid, SessionID);
            db.AddInParameter(command, "@CreatedAt", DbType.DateTime, CreatedAt);
            db.AddInParameter(command, "@CreatedBy", DbType.Int32, CreatedBy);


            if (db.ExecuteNonQuery(command) == 0)
            {
                //no record inserted 
            }
        }

        public static byte[] StreamDictationByID(Guid DictationID, Guid SessionID)
        {
            string sProc = DBConstants.DICTATION_STREAM_BY_ID_SP;
            Database db = DatabaseFactory.CreateDatabase(); // Load default DB
            DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
            db.AddInParameter(command, "@DictationID", DbType.Guid, DictationID);
            byte[] dictationData = (byte[])db.ExecuteScalar(command);

            return dictationData;
            
        }
    }
}
