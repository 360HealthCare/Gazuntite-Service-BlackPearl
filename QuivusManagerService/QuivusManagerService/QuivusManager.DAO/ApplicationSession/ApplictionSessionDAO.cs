using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Quivus.Utility.CustomException;
using System.Data;

namespace QuivusManager.DAO.ApplicationSession
{
    public class ApplictionSessionDAO : BaseDAO
    {
      
        public bool AddUserSession(UserSessionDTO userSession)
        {
            bool UpdateResult = false;
            string sProc = UserConstant.USER_SESSION_INSERT_SP;
            bool IsProvider = false;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID", DbType.Int32, userSession.UserID);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, userSession.UserTypeID);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, userSession.ClinicBranchID);

                db.AddInParameter(command, "@TimeZoneID", DbType.Int32, userSession.TimeZoneId);
                db.AddInParameter(command, "@SessionID", DbType.Guid, userSession.SessionID);
                db.AddInParameter(command, "@IsActive", DbType.Boolean, userSession.IsActive);
                db.AddInParameter(command, "@LastActivityPerformedAt", DbType.DateTime, userSession.LastActivityPerformedAt);
                
                db.AddOutParameter(command, "@UserSessionIDOUT", DbType.Int32, 0);

                db.AddOutParameter(command, "@IsUserProviderOUT", DbType.Boolean, 0);
               

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }

                IsProvider = bool.Parse(db.GetParameterValue(command, "@IsUserProviderOUT").ToString());
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return IsProvider;
        }

    }
}
