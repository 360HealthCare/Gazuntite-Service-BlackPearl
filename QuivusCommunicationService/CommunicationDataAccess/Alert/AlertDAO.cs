using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using QuivusModel.Communication.AlertsDTO;
using System.IO;
using System.Data;
using Quivus.Utility.Logger;
using QuivusUtility.ProjectConstants;

namespace CommunicationDataAccess
{
    
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 21 November 2011
    /// Description: Contains all functions related to Alert managment
    /// </summary>
    public class AlertDAO
    {
        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 22 November 2011
        /// Saves and modifies alert
        /// </summary>
        /// <param name="oAlertDTO"></param>
        /// <returns></returns>
        public int SaveAlert(AlertDTO oAlertDTO)
        {
            try
            {
                #region AlertSave
                string sProc = AlertConstant.ALERT_Insert_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

               
                db.AddInParameter(command, "@ClinicBrachid", DbType.Int32, oAlertDTO.ClinicBranchID);

                db.AddInParameter(command, "@Description", DbType.String, oAlertDTO.Description);
                db.AddInParameter(command, "@AlertTime", DbType.DateTime, oAlertDTO.AlertTime);

                db.AddInParameter(command, "@AlertEndDate", DbType.DateTime, oAlertDTO.AlertEndDate);
                db.AddInParameter(command, "@IsReccurence", DbType.Boolean, oAlertDTO.IsReccurence);
                db.AddInParameter(command, "@RecurrenceID", DbType.Int32, oAlertDTO.RecurrenceID);
                //db.AddInParameter(command, "@IsAlive", DbType.String, oAlertDTO.IsAlive);
                db.AddInParameter(command, "@SeverityID", DbType.Int32, oAlertDTO.SeverityID);
                db.AddInParameter(command, "@Deleted", DbType.Boolean, oAlertDTO.IsDeleted);
                db.AddInParameter(command, "@CREATEDBY", DbType.Int32, oAlertDTO.CreatedBy);
                db.AddInParameter(command, "@MODIFIEDBY", DbType.Int32, oAlertDTO.ModifiedBy);
                db.AddInParameter(command, "@MODIFIEDAT", DbType.DateTime, oAlertDTO.ModifiedAt);
                db.AddInParameter(command, "@ModuleID", DbType.Int32, oAlertDTO.ModuleID);
                db.AddInParameter(command, "@AlertStatusID", DbType.Int32, oAlertDTO.AlertStatusID);
                db.AddInParameter(command, "@AlertModificationReason", DbType.String, oAlertDTO.AlertModificationReason);
                db.AddInParameter(command, "@AlertRights", DbType.Boolean, oAlertDTO.AlertRights);
                db.AddInParameter(command, "@IsSnooze", DbType.Boolean, oAlertDTO.IsSnooze);
                db.AddInParameter(command, "@TimeInterval", DbType.Int32, oAlertDTO.TimeInterval);
                db.AddInParameter(command, "@ReminderTime", DbType.DateTime, oAlertDTO.ReminderTime);
                db.AddInParameter(command, "@Code", DbType.Int32, oAlertDTO.AlertServiceTypeID);
                db.AddInParameter(command, "@AlertServiceTypeID", DbType.Int32, oAlertDTO.AlertServiceTypeID); 
                db.AddInParameter(command, "@IsApproved", DbType.Boolean, oAlertDTO.IsApproved);
                db.AddInParameter(command, "@IsDismissed", DbType.Boolean, oAlertDTO.IsDismissed);
                if(oAlertDTO.DismissDate != null )
                db.AddInParameter(command, "@DismissDate", DbType.DateTime, oAlertDTO.DismissDate);
                else
                    db.AddInParameter(command, "@DismissDate", DbType.DateTime, "");
                db.AddInParameter(command, "@Dismissedby", DbType.Int32, oAlertDTO.Dismissedby);


                
                
                if (oAlertDTO.userId <= 0)
                    db.AddInParameter(command, "@UserId", DbType.Int32, "");
                else
                    db.AddInParameter(command, "@UserId", DbType.Int32, oAlertDTO.userId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    // throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }

                // return 1;
                #endregion AlertSave
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return 0;
        }
    }
}
