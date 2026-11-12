using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.IO;
using System.Data;
using Quivus.Utility.Logger;
using QuivusUtility.ProjectConstants;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using QuivusModel.Communication.MailDTO;
using CommunicationDataAccess.DBUtililty;
namespace CommunicationDataAccess.Email
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 1 December 2011
    /// Description: Used for DB insertion of Emails log
    /// </summary>
   public class EmailDAO
    {
       /// <summary>
       /// Author: Zahid Mehmood
       /// </summary>
       /// <param name="oEmailDTO"></param>
       /// <returns></returns>
       public string SaveEmailInformation(EmailDTO oEmailDTO)
       {
           try
           {
               string sProc = string.Empty;
               //if (oEmailDTO.HasAttachment)
               //    sProc = Quivus.Utility.UserConstant.USER_DEFAULT_EMAIL_LOG_WITH_ATTACHMENT_SP;
               //else
               sProc = EmailConstant.EMAIL_LOG_SP;

               

               QuivusLogger.Info("Calling SP:: " + sProc);

               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               
               	

               db.AddInParameter(command, "@UserID", DbType.Int32, oEmailDTO.UserEmailID);
               db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, oEmailDTO.ClinicBranchID);
               db.AddInParameter(command, "@RecipientEmail", DbType.String, oEmailDTO.RecipientEmail);
               db.AddInParameter(command, "@MailBody", DbType.String, oEmailDTO.Message);
               db.AddInParameter(command, "@MailSubject", DbType.String, oEmailDTO.Subject);
               db.AddInParameter(command, "@MailSentAt", DbType.String, System.DateTime.UtcNow);// @MailSentAt		
               db.AddInParameter(command, "@CreatedBy", DbType.Int32, oEmailDTO.SENDERID);// change userid to created by id
               db.AddInParameter(command, "@Attachment", DbType.String,"" );//oEmailDTO.Attachment);
               db.AddInParameter(command, "@AttachedFileName", DbType.String, oEmailDTO.AttachedFileName);//oEmailDTO.AttachedFileName);
               db.AddInParameter(command, "@HasAttachment", DbType.Boolean, oEmailDTO.HasAttachment);
               db.AddInParameter(command, "@StationID", DbType.Int32, 1);
               db.AddInParameter(command, "@TokenID", DbType.Int32, 2);
               db.AddInParameter(command, "@EmailStateID", DbType.Int32, 1);
               db.AddInParameter(command, "@EmailTypeID", DbType.Int32, 1);
               db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

             //  int sResult = db.ExecuteNonQuery(command);


               if (db.ExecuteNonQuery(command) == 0)
               {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
               }
               int iEmailId = int.Parse(db.GetParameterValue(command, "@UserEmailIDOUT").ToString());
               return iEmailId.ToString();
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
          // return "";
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 2 December 2011
       /// </summary>
       /// <param name="sEmailID"></param>
       /// <param name="sUserID"></param>
       /// <returns></returns>
       public string SaveReceieverList(string sEmailID,string sUserID)
       {
           try
           {
           string sProc = string.Empty;
           
           sProc = EmailConstant.EMAIL_LOG_INSERTRECIEVERLIST_SP;

           QuivusLogger.Info("Calling SP:: " + sProc);

           Database db = DatabaseFactory.CreateDatabase();
           DbCommand command = db.GetStoredProcCommand(sProc);

           db.AddInParameter(command, "@UserEmailID", DbType.Int32, int.Parse(sEmailID));
           db.AddInParameter(command, "@UserID", DbType.Int32,int.Parse(sUserID));
          

        //   int sResult = db.ExecuteNonQuery(command);

           if (db.ExecuteNonQuery(command) == 0)
           {
               throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
           }


           }
           catch (Exception ex)
           {

               throw ex;
           }
           return "";
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 2 December 2011
       /// </summary>
       /// <param name="sEmailID"></param>
       /// <param name="sUserID"></param>
       /// <returns></returns>
       public string SaveSender(string sEmailID,string sUserID)
       {
           try
           {
               string sProc = string.Empty;

               sProc = EmailConstant.EMAIL_LOG_INSERTSENDERLIST_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);

               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@UserEmailID", DbType.Int32, sEmailID);
               db.AddInParameter(command, "@UserID", DbType.Int32, sUserID);


              // int sResult = db.ExecuteNonQuery(command);

               if (db.ExecuteNonQuery(command) == 0)
               {
                   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
               }
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
           return "";
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 2 December 2011
       /// </summary>
       /// <param name="iUserEmailID"></param>
       /// <param name="iUserID"></param>
       /// <param name="iClinicBrachID"></param>
       /// <param name="iStationID"></param>
       /// <returns></returns>
       public EmailDTO GetEmailDetails(int iUserEmailID,int iUserID,int iClinicBrachID,int iStationID)
       {
           EmailDTO oEmailDTO = null;

           try
           {
               UpdateEmailState(iUserEmailID);

               string sProc = string.Empty;
               IDataReader objIReader= null;
               sProc = EmailConstant.EMAIL_GETMAILDETAILS_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@UserEmailID", DbType.Int32, iUserEmailID);
               db.AddInParameter(command, "@UserID", DbType.Int32, iUserID);
               db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, iClinicBrachID);
               db.AddInParameter(command, "@StationID", DbType.Int32, iStationID);

                using (objIReader = db.ExecuteReader(command))
                {

                    return oEmailDTO = DataReaderToDTOConverter.ConvertToEmailDTODetail(objIReader);

                 //   GenericDataReaderToDTOConverter.ConvertToDTOList(oEmailDTO, objIReader,typeof(EmailDTO));
                }
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
           //return oEmailDTO;
       }


       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 9 December 2011
       /// </summary>
       /// <param name="iUserEmailID"></param>
       public void UpdateEmailState(int iUserEmailID)
       {
           try
           {
                string sProc = string.Empty;
               sProc = EmailConstant.EMAIL_UpdateEmailState_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@UserEmailID", DbType.Int32, iUserEmailID);

               if(db.ExecuteNonQuery(command)==0)
               {
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }


       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 2 December 2011
       /// </summary>
       /// <param name="iUserEmailID"></param>
       /// <returns></returns>
       public string DeleteEmail(string userEmailID, int userID)
       {
          // EmailDTO emailDTO = null;
           try
           {
               string sProc = string.Empty;
               IDataReader objIReader = null;
               sProc = EmailConstant.EMAIL_DELETE_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@emailIds", DbType.Xml, userEmailID);
               db.AddInParameter(command, "@userID", DbType.String, userID);
              
               if (db.ExecuteNonQuery(command) == 0)
               {
                   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
               }
               return "Record Deleted";
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 2 December 2011
       /// </summary>
       /// <param name="iUserEmailID"></param>
       /// <returns></returns>
       public string DeleteSentEmail(string userEmailID, int userID)
       {
           // EmailDTO emailDTO = null;
           try
           {
               string sProc = string.Empty;
               IDataReader objIReader = null;
               sProc = EmailConstant.EMAIL_DELETE_SENT_MAILS_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@emailIds", DbType.Xml, userEmailID);
               db.AddInParameter(command, "@userID", DbType.String, userID);

               if (db.ExecuteNonQuery(command) == 0)
               {
                   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
               }
               return "Record Deleted";
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 2 December 2011
       /// </summary>
       /// <param name="iUserID"></param>
       /// <param name="iClinicBranchID"></param>
       /// <param name="iStationID"></param>
       /// <returns></returns>
       public MailDTO GetAllEmails(int userID, int clinicBranchID, int stationID, int pageSize, int currentPage)
       {
          // List<EmailDTO> emailDTOList = null;
           MailDTO mailDTO = null;
           try
           {
               string sProc = string.Empty;
               IDataReader objIReader = null;
               sProc = EmailConstant.EMAIL_GetAllEmailsOfUser_SP;
              // sProc = EmailConstant.
               //emailDTOList = new List<EmailDTO>();
               mailDTO = new MailDTO();

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@UserID", DbType.Int32, userID);
               db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
               db.AddInParameter(command, "@StationID", DbType.Int32, stationID);
               db.AddInParameter(command, "@PageNo", DbType.Int32, currentPage);
               db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
               
    
              // db.AddOutParameter(command, "@TotalEmails", DbType.Int32, 0);

               using (objIReader = db.ExecuteReader(command))
               {
                   //GenericDataReaderToDTOConverter.ConvertToDTOList(emailDTOList, objIReader, typeof(EmailDTO));
                   mailDTO = DataReaderToDTOConverter.ConvertToMailDTO(objIReader, false, currentPage);
               }
               
              // int emailCount = int.Parse(db.GetParameterValue(command, "@TotalEmails").ToString());
               return mailDTO;
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 8 December 2011
       /// </summary>
       /// <param name="iUserID"></param>
       /// <param name="iClinicBranchID"></param>
       /// <param name="iStationID"></param>
       /// <returns></returns>
       public MailDTO GetAllSentEmails(int userID, int clinicBranchID, int stationID, int pageSize, int currentPage)
       {
           MailDTO emailDTOList = null;
           try
           {
               string sProc = string.Empty;
               IDataReader objIReader = null;
               sProc = EmailConstant.EMAIL_GetAllSentEmailsOfUser_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               db.AddInParameter(command, "@UserID", DbType.Int32, userID);
               db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
               db.AddInParameter(command, "@StationID", DbType.Int32, stationID);
               db.AddInParameter(command, "@PageNo", DbType.Int32, currentPage);
               db.AddInParameter(command, "@PageSize", DbType.Int32, pageSize);
              
               

               using (objIReader = db.ExecuteReader(command))
               {
                   if (objIReader != null)
                   {
                       emailDTOList = DataReaderToDTOConverter.ConvertToMailDTO(objIReader, true, currentPage);//.ConvertToEmailDTO(objIReader);
                   }
               }
               return emailDTOList;
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 9 December 2011
       /// </summary>
       /// <param name="iUserEmailID"></param>
       /// <param name="sFileName"></param>
       /// <returns></returns>
       public string GetFilePath(string sUserEmailID)
       {
           try
           {
                string sProc = string.Empty;
               IDataReader objIReader = null;
               sProc = EmailConstant.EMAIL_GetPathofAttachment_SP;

               QuivusLogger.Info("Calling SP:: " + sProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(sProc);

               //////  Replace useremailid with serverid/////////////////////////////////////
               db.AddInParameter(command, "@SERVERID", DbType.Int32, 1);
               db.AddInParameter(command, "@ATTACHMENTTYPE", DbType.String, "Email");
               using (objIReader = db.ExecuteReader(command))
               {
                   if (objIReader != null)
                   {//EmailAttachmentPath
                       objIReader.Read();
                       return objIReader["EmailAttachmentPath"].ToString();
                   }
               }
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
           return "";
       }

       /// <summary>
       /// Author: Zahid Mehmood
       /// Date: 20 December 2011 
       /// Description: This function returns list of email sender id.
       /// </summary>
       /// <param name="userID"></param>
       /// <returns></returns>
       public int[] GetEmailSenderList(string userEmailIDList)
       {
           try
           {
                string storedProc = string.Empty;
                int[] emailSenderList=null;
               IDataReader objIReader = null;
               storedProc = EmailConstant.EMAIL_GetEmailSenderList_SP;

               QuivusLogger.Info("Calling SP:: " + storedProc);
               Database db = DatabaseFactory.CreateDatabase();
               DbCommand command = db.GetStoredProcCommand(storedProc);

               db.AddInParameter(command, "@UserEmailIDList", DbType.String, userEmailIDList);
               

               using (objIReader = db.ExecuteReader(command))
               {
                   if (objIReader != null)
                   {
                      

                       emailSenderList = DataReaderToDTOConverter.ConvertToString(objIReader);
                      // emailSenderList = DataReaderToDTOConverter.ConvertToEmailDTO(objIReader);
                   }
               }
               return emailSenderList;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
