using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuivusModel.Communication.MailDTO;
using QuivusModel.Communication.SMSDTO;
using Quivus.Utility.ProjectConstants;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using Quivus.Utility.Logger;
namespace CommunicationDataAccess.DBUtililty
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 2 December 2011
    /// Description: Class converts datareader to EmailDTO
    /// </summary>
    public class DataReaderToDTOConverter
    {
        #region EmailDTO

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 December 2011
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<EmailDTO> ConvertToEmailDTO(IDataReader objReader)
        {
            EmailDTO oEmailDTOList;
            List<EmailDTO> EmailList = null;
            try
            {
                if(objReader != null)
                {
                    oEmailDTOList = new EmailDTO();
                    EmailList = new List<EmailDTO>();
                while (objReader.Read())
                {
                    //if (objReader.Read())
                    //{
                    EmailDTO oEmailDTO = new EmailDTO();
                    
                        oEmailDTO.UserEmailID = int.Parse(objReader["RECIEVERID"].ToString());
                        oEmailDTO.Subject = objReader["Subject"].ToString();
                        oEmailDTO.SENDERID = int.Parse(objReader["SenderID"].ToString());
                        oEmailDTO.RecipientEmail = "";//objReader["RECIEVERID"].ToString();
                        oEmailDTO.Message = objReader["Message"].ToString();
                     /////////   oEmailDTO.IsDirty = false;
                        oEmailDTO.HasAttachment = Convert.ToBoolean(objReader["HasAttachment"]);
                        oEmailDTO.EmailTemplateID = int.Parse(objReader["EmailTemplateID"].ToString());
                        oEmailDTO.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                        oEmailDTO.Attachment = null;//Convert.ToByte(objReader[]);
                        oEmailDTO.AttachedFileName = "";//objReader[].ToString();
                        oEmailDTO.EmailStateID = int.Parse(objReader["EmailStateID"].ToString());
                        oEmailDTO.EmailTypeID = int.Parse(objReader["EmailTypeID"].ToString());
                        oEmailDTO.RECIEVERID = int.Parse(objReader["RECIEVERID"].ToString());
                        EmailList.Add(oEmailDTO);
                    }
                return EmailList;
                }

            }
            catch (Exception ex)
            {
                 QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".DataReaderToDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return EmailList;
        }



        public static int[] ConvertToString(IDataReader objReader)
        {
            try
            {
                int[] emailSenderList = null;
                if (objReader != null)
                {
                    int counter = 0;
                    emailSenderList =new int[10];
                    while (objReader.Read())
                    {
                        emailSenderList[counter] = int.Parse(objReader["UserID"].ToString());
                        counter++;
                    }
                    
                }
                return emailSenderList;
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw ex;
            }
        }



        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 16 December 2011
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static EmailDTO ConvertToEmailDTODetail(IDataReader objReader)
        {
            EmailDTO emailDTO = null;
           
            try
            {
                if (objReader != null)
                {
                    emailDTO = new EmailDTO();

                    while (objReader.Read())
                    {
                        emailDTO.UserEmailID = int.Parse(objReader["UserEmailID"].ToString());
                        emailDTO.RECIEVERID = int.Parse(objReader["RECIEVERID"].ToString());
                        emailDTO.Subject = objReader["Subject"].ToString();
                        emailDTO.SENDERID = int.Parse(objReader["SenderID"].ToString());
                        emailDTO.RecipientEmail = "";//objReader["RECIEVERID"].ToString();
                        emailDTO.Message = objReader["Message"].ToString();
                        emailDTO.MailSentAt = objReader["MailSentAt"].ToString();
                        /////////   oEmailDTO.IsDirty = false;
                        emailDTO.HasAttachment = Convert.ToBoolean(objReader["HasAttachment"]);
                        emailDTO.EmailTemplateID = int.Parse(objReader["EmailTemplateID"].ToString());
                        emailDTO.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                        emailDTO.Attachment = null;//Convert.ToByte(objReader[]);
                        emailDTO.AttachedFileName = objReader["AttachedFileName"].ToString();
                        emailDTO.EmailStateID = int.Parse(objReader["EmailStateID"].ToString());
                        emailDTO.EmailTypeID = int.Parse(objReader["EmailTypeID"].ToString());
                        emailDTO.RECIEVERID = int.Parse(objReader["RECIEVERID"].ToString());
                      
                    }
                    return emailDTO;
                }

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".DataReaderToDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return emailDTO;
        }


        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 27 December 2011
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static MailDTO ConvertToMailDTO(IDataReader objReader, bool isSentItem, int currentPage)
        {
            MailDTO mailDTO = null;
            List<EmailDTO> emailDTOList = new List<EmailDTO>();
            try
            {
                if (objReader != null)
                {
                    if (objReader.Read())
                    {
                        mailDTO = new MailDTO();
                        mailDTO.FirstRecord = currentPage;//int.Parse(objReader["FirstRecord"].ToString());
                        mailDTO.LastRecord = int.Parse(objReader["LastRecord"].ToString());
                        mailDTO.TotalRecords = int.Parse(objReader["TotalRecords"].ToString());
                        int tpage = mailDTO.TotalRecords / 10;
                        int tRemainPage = mailDTO.TotalRecords % 10;
                        if (tpage == 0)
                            mailDTO.TotalRecords = 1;
                        else if (tRemainPage > 0)
                            mailDTO.TotalRecords = tpage + 1;
                        else
                            mailDTO.TotalRecords = tpage;
                    }
                    objReader.NextResult();
                    while (objReader.Read())
                    {
                      
                        EmailDTO emailDTO = new EmailDTO();
                        emailDTO.EmailTemplateID = int.Parse(objReader["EmailTemplateID"].ToString());
                        emailDTO.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                        emailDTO.UserEmailID = int.Parse(objReader["UserEmailID"].ToString());
                        emailDTO.Subject = objReader["Subject"].ToString();
                        emailDTO.HasAttachment = Convert.ToBoolean(objReader["HasAttachment"]);
                        emailDTO.RECIEVERID = int.Parse(objReader["Token_ID"].ToString());
                        emailDTO.EmailStateID = int.Parse(objReader["EmailStateID"].ToString());
                        emailDTO.EmailTypeID = int.Parse(objReader["EmailTypeID"].ToString());
                        if(isSentItem)
                            emailDTO.RECIEVERID = int.Parse(objReader["RECIEVERID"].ToString());
                        else
                        emailDTO.SENDERID = int.Parse(objReader["SENDERID"].ToString());
                        emailDTO.MailSentAt = objReader["MailSentAt"].ToString();
                        emailDTO.IsSelected = Convert.ToBoolean(objReader["IsSelected"]);
                        /////// Need to Remove this block
                        emailDTO.FirstRecord =mailDTO.FirstRecord;
                        emailDTO.LastRecord = mailDTO.LastRecord;
                        emailDTO.TotalRecords = mailDTO.TotalRecords;
                        ///////
                        emailDTOList.Add(emailDTO);
                    }
                    mailDTO.Emails = emailDTOList;
                    return mailDTO;
                }

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".DataReaderToDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return mailDTO;
        }

        #endregion EmailDTO

        #region SMS
        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 January 2012
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static SMSResponseDTO ConvertToSMSDTO(IDataReader objReader,int currentPage)
        {
            SMSResponseDTO smsResponseDTO = null;
           List<SMSResponseDetailDTO> smsResponseDetailDTOList = null;
            try
            {
                if (objReader != null)
                {
                    smsResponseDTO = new SMSResponseDTO();
                    smsResponseDetailDTOList = new List<SMSResponseDetailDTO>();
                    SMSResponseDetailDTO smsresponseDTODetail=null;
                    smsResponseDTO.smsResponseDetail = new List<SMSResponseDetailDTO>();
                    int smsIDPrevious = 0;
                    int smsID=0;
                    SMSRecieverDTO smsRecieverDTO;
                    #region mainblock
                    if (objReader.Read())
                        {
                            smsResponseDTO.TotalPages = int.Parse(objReader["TotalRecords"].ToString());
                            smsResponseDTO.CurrentPage = currentPage;
                            int tpage = smsResponseDTO.TotalPages / 10;
                            int tRemainPage = smsResponseDTO.TotalPages % 10;
                            if (tpage == 0)
                                smsResponseDTO.TotalPages = 1;
                            else if (tRemainPage > 0)
                                smsResponseDTO.TotalPages = tpage + 1;
                            else
                                smsResponseDTO.TotalPages = tpage;
                        }
                    #endregion mainblock

                    #region response Detail
                    objReader.NextResult();
                        while (objReader.Read())
                        {
                            smsID = int.Parse(objReader["SMSID"].ToString());
                            if (smsID != smsIDPrevious && smsresponseDTODetail != null)
                            {

                          //      smsResponseDetailDTOList.Add(smsresponseDTODetail);
                                smsResponseDTO.smsResponseDetail.Add(smsresponseDTODetail);
                             //   smsResponseDTO.smsResponseDetail = smsResponseDetailDTOList;       
                            }
                            if (smsID != smsIDPrevious)
                            {
                                smsresponseDTODetail = new SMSResponseDetailDTO();
                                
                                smsRecieverDTO = new SMSRecieverDTO();
                                smsresponseDTODetail.SMSID = smsID;
                                smsresponseDTODetail.SendAt = DateTime.Parse(objReader["SendAt"].ToString());
                                smsresponseDTODetail.Message = objReader["MESSAGE"].ToString();

                                smsresponseDTODetail.smsRecieverList = new List<SMSRecieverDTO>();
                                smsRecieverDTO.UserId = int.Parse(objReader["TO"].ToString());
                                smsresponseDTODetail.smsRecieverList.Add(smsRecieverDTO);
                                smsIDPrevious = smsID;
                            }
                            else
                            {
                                smsRecieverDTO = new SMSRecieverDTO();
                                smsRecieverDTO.UserId = int.Parse(objReader["TO"].ToString());
                                smsresponseDTODetail.smsRecieverList.Add(smsRecieverDTO);
                            }

                        }

                        #endregion response Detail

                        return smsResponseDTO;

                        //   }
                   // smsLogDTOList.Add(smsLogDTO);

                  //  smsResponseDTO.smsResponseDetai
                       // smsResponseDTO.smsResponseDetail = smsresponseDTODetail;
                    /////    smsResponseDTO.smsResponseDetail.Add(smsResponseDetailDTOList);
                    
                }

            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.USER_MANAGER_DTO_CONVERSION_ERROR_MSG + ".DataReaderToDTOConverter", ProjectConstant.ERROR_CODE.UserDTOConversion.ToString());
            }
            return smsResponseDTO;
        }


        #endregion SMS

        #region VideoChat
        public static String ConvertToSessionString(IDataReader objReader)
        {
            String videoChatSession = null;
            try
            {

                if (objReader != null)
                {
                    while (objReader.Read())
                    {
                        videoChatSession = objReader["OpenTokSessionID"].ToString() + "^";
                        videoChatSession += objReader["CallInitiator"].ToString() + "^";
                        videoChatSession += objReader["VideoChatID"].ToString() +"^";
                        videoChatSession += objReader["OpenTokTokenID"].ToString();
                    }
                }
                return videoChatSession;
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw ex;
            }
        }
        #endregion VideoChat
    }
}
