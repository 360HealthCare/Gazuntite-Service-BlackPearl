using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.CustomException;
using System.Data;
using Quivus.Utility;
using Quivus.Model.Communication;
using Quivus.Utility.Logger;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Quivus.Model.Image;

namespace StreamingService.DataAccess
{
    public class StreamingDAO : BaseDAO
    {
        /// <summary>
        /// Author Mansoor
        /// Date 18 Jan 2011
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="clinicBranchID"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int LogEmailSend(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message, DateTime mailSentAt)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.USER_DEFAULT_EMAIL_LOG_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.COMMUNICATION_DB); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID ", DbType.Int32, requestedUserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchID);
                db.AddInParameter(command, "@RecipientEmail", DbType.String, receiptEmail);
                db.AddInParameter(command, "@MailBody", DbType.String, message);
                db.AddInParameter(command, "@MailSubject", DbType.String, subject);
                db.AddInParameter(command, "@MailSentAt", DbType.DateTime, mailSentAt);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, requestedUserId);
                db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }

            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }


        /// <summary>
        /// Author Ali Noor
        /// </summary>
        /// <param name="emailDTO"></param>
        /// <param name="mailSentAt"></param>
        /// <returns></returns>
        public int LogEmailWithAttachment(EmailDTO emailDTO, DateTime mailSentAt)
        {
            int UpdateResult = 0;
            string sProc = UserConstant.USER_DEFAULT_EMAIL_LOG_WITH_ATTACHMENT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(ProjectConstant.COMMUNICATION_DB); // Load DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@UserID ", DbType.Int32, emailDTO.UserId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, emailDTO.ClinicBranchID);
                db.AddInParameter(command, "@RecipientEmail", DbType.String, emailDTO.RecipientEmail);
                db.AddInParameter(command, "@MailBody", DbType.String, emailDTO.Message);
                db.AddInParameter(command, "@MailSubject", DbType.String, emailDTO.Subject);
                db.AddInParameter(command, "@MailSentAt", DbType.DateTime, mailSentAt);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, emailDTO.UserId);
                db.AddInParameter(command, "@AttachedFileName ", DbType.String, emailDTO.AttachedFileName);
                db.AddInParameter(command, "@Attachment ", DbType.Binary, emailDTO.Attachment);
                db.AddOutParameter(command, "@UserEmailIDOUT", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = 1;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return UpdateResult;
        }

        public int InsertUserImage(ImageDTO imageDTO)
        {
            string sProc = UserConstant.USER_IMAGE_INSERT_SP;

            int UserImageId=0;
            int ImageId;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                QuivusLogger.Info("InsertUserImage UserID:: " + imageDTO.UserId + "UserTypeID::" + imageDTO.UserTypeID);
                QuivusLogger.Info("InsertUserImage ImageName:: " + imageDTO.ImageName + "ImageCaption::" + imageDTO.ImageCaption);

                QuivusLogger.Info("InsertUserImage ImageCaption:: " + imageDTO.ImageCaption + "ImageCaption::" + imageDTO.ImageCaption);
                QuivusLogger.Info("InsertUserImage ImagePath:: " + imageDTO.ImagePath + "ImageData::" + imageDTO.ImageData);
                QuivusLogger.Info("InsertUserImage ImageType:: " + imageDTO.ImageTypeId + "CreatedBy::" + imageDTO.CreatedBy);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserID", DbType.Int32, imageDTO.UserId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, imageDTO.UserTypeID);
                db.AddInParameter(command, "@ImageName", DbType.String, imageDTO.ImageName);
                db.AddInParameter(command, "@ImageCaption", DbType.String, imageDTO.ImageCaption);
                db.AddInParameter(command, "@ImagePath", DbType.String, imageDTO.ImagePath);                
                db.AddInParameter(command, "@ImageData", DbType.Binary, imageDTO.ImageData);

                db.AddInParameter(command, "@ThumbnailData", DbType.Binary, imageDTO.ImageThumbnailData);


                db.AddInParameter(command, "@ImageType", DbType.Int32, imageDTO.ImageTypeId);

                //TODO: shd map the dTO field
                db.AddInParameter(command, "@IsDefault", DbType.Boolean, true);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, imageDTO.CreatedBy);

                db.AddOutParameter(command, "@UserImageIDOUT", DbType.Int32, 1);
                db.AddOutParameter(command, "@ImageIDOUT", DbType.Int32, 1);
               
                if (db.ExecuteNonQuery(command) == 0)
                {
                    QuivusLogger.Info("Error Inserting Image");
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG);
                }

                UserImageId = int.Parse(db.GetParameterValue(command,"@UserImageIDOUT").ToString());
                ImageId = int.Parse(db.GetParameterValue(command,"@ImageIDOUT").ToString());
            }
            catch
            {
                throw;
            }
            finally
            {
                //objIReader.Close();
            }

            return UserImageId;
        }

        public ImageDTO LoadUserImage(int UserID, int UserTypeID)
        {
            ImageDTO response = null;
            IDataReader objIReader = null;

            string sProc = UserConstant.USER_IMAGE_LOAD_SP;

            try
            {
                if (response == null)
                {
                    response = new ImageDTO();
                }
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sProc);

                db.AddInParameter(command, "@UserID", DbType.Int32, UserID);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, UserTypeID);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null && objIReader.Read())
                    {
                        response.ImageId = int.Parse(objIReader["ImageID"].ToString());

                        if (objIReader["ImageName"] != DBNull.Value)
                        {
                            response.ImageName = objIReader["ImageName"].ToString();
                        }

                        if (objIReader["ImageCaption"] != DBNull.Value)
                        {
                            response.ImageCaption = objIReader["ImageCaption"].ToString();
                        }

                        if (objIReader["ImageData"] != DBNull.Value)
                        {
                            response.ImageData = (byte[])objIReader["ImageData"];
                        }

                        if (objIReader["ThumbnailData"] != DBNull.Value)
                        {
                            response.ImageThumbnailData = (byte[])objIReader["ThumbnailData"];
                        }

                        if (objIReader["ImageType"] != DBNull.Value)
                        {
                            response.ImageTypeId = int.Parse(objIReader["ImageType"].ToString());
                        }

                    }
                }

            }
            catch
            {

                throw;
            }
            finally
            {
                //objIReader.Close();
            }

            return response;
        }
    }
}
