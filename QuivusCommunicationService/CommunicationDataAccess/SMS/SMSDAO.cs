using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using QuivusModel.Communication.SMSDTO;
using Quivus.Utility.Logger;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using CommunicationDataAccess.DBUtililty;
using System.Xml;
namespace CommunicationDataAccess.SMS
{   
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 17 November 2011
    /// </summary>
  
    public class SMSDAO
    {
        /// <summary>
        /// This function saves SMS sent by client.
        /// </summary>
        /// <param name="oSMSlogDTO"></param>
        /// <param name="IsUpdate"></param>
        /// <returns></returns>
        public int SaveSMSLog(SMSLogDTO oSMSlogDTO)
        {
            
            try
            {
                string sProc = SMSConstant.SMSLog_INSERT_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); 
                DbCommand command = db.GetStoredProcCommand(sProc); 

                if(oSMSlogDTO.UserId <=0)
                    db.AddInParameter(command, "@UserId", DbType.Int32, "");                    
                else
                db.AddInParameter(command, "@UserId", DbType.Int32, oSMSlogDTO.UserId);
                db.AddInParameter(command, "@ClinicBrachid", DbType.Int32, oSMSlogDTO.ClinicBranchID);
                               
                //db.AddInParameter(command, "@SENDAT", DbType.DateTime, oSMSlogDTO.SendAt);
                db.AddInParameter(command, "@PHONENUMBER", DbType.String, oSMSlogDTO.PhoneNumber);
                if(oSMSlogDTO.SMSTemplateID <=0)
                    db.AddInParameter(command, "@SMSTEMPLATEID", DbType.Int32, 0);
                else
                db.AddInParameter(command, "@SMSTEMPLATEID", DbType.Int32, oSMSlogDTO.SMSTemplateID);
                db.AddInParameter(command, "@SMSTEXT", DbType.String, oSMSlogDTO.Text);
                db.AddInParameter(command, "@CREATEDBY", DbType.Int32, oSMSlogDTO.CreatedBy);
             //   db.AddInParameter(command, "@MODIFIEDBY", DbType.Int32, oSMSlogDTO.ModifiedBy);
            //    db.AddInParameter(command, "@MODIFIEDAT", DbType.Int32, oSMSlogDTO.ModifiedAt);
                db.AddInParameter(command, "@Deleted", DbType.Boolean, oSMSlogDTO.IsDeleted);
                db.AddOutParameter(command, "@SMSIDOUT",DbType.Int32, 1);
                

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                int smsID = int.Parse(db.GetParameterValue(command, "@SMSIDOUT").ToString());
                return smsID;
                
            }
            catch(Exception ex)
            {
                //throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return 0;
        }
       
        public int SaveSMSRecieverList(string phoneNumber,int userID,int smsID)
        {

            try
            {
                string sProc = SMSConstant.SMSRecieverList_INSERT_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                if (userID <= 0)
                    db.AddInParameter(command, "@UserId", DbType.Int32, "");
                else
                    db.AddInParameter(command, "@UserId", DbType.Int32, userID);

                db.AddInParameter(command, "@SMSID", DbType.Int32, smsID);
                db.AddInParameter(command, "@PHONENUMBER", DbType.String, phoneNumber);



                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
            }
            catch (Exception ex)
            {
                //throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return 0;
        }

        public SMSResponseDTO GetSMSList(int userID, int clinicBracnhID,int currentPage)
        {
            SMSResponseDTO smsResponseDTO = null;
            try
            {
               IDataReader dataReader=null;
               
                string sProc = SMSConstant.SMSLog_GET_SP;
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@USERID", DbType.Int32, userID);
                db.AddInParameter(command, "@CLINIC_BRANCH_ID", DbType.Int32, clinicBracnhID);
                db.AddInParameter(command, "@PageNo", DbType.Int32, currentPage);
                db.AddInParameter(command, "@PageSize", DbType.Int32, 10);
               
               //     return oEmailDTO = DataReaderToDTOConverter.ConvertToEmailDTODetail(objIReader);
                 using (dataReader=db.ExecuteReader(command))
                {
                  smsResponseDTO=  DataReaderToDTOConverter.ConvertToSMSDTO(dataReader,currentPage);
                }
                 return smsResponseDTO;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return smsResponseDTO;
        }

        public string DeleteSMS(string smsIDs,string userID)
        {
            try
            {
                 string sProc = string.Empty;
               IDataReader objIReader = null;
               sProc = SMSConstant.SMSLog_DELETE_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@SMSIds", DbType.Xml, smsIDs);
               db.AddInParameter(command, "@userID", DbType.String, userID);
              
               if (db.ExecuteNonQuery(command) == 0)
               {
                   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
               }
               return "Record Deleted";
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
