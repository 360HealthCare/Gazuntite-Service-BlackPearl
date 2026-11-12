using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunicationDataAccess;
using QuivusModel.Communication.SMSDTO;
using Quivus.Utility.Logger;
using CommunicationDataAccess.SMS;
using Quivus.Utility.Factory;
using Communication.Business.SMSManager;
using Communication.Business.EmailManager.EmailManagerImp;
namespace Communication.Business
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 17 November 2011
    /// Description : Contains All business logic of SMS module
    /// </summary>
    public class SMSManagerImp : ISMSManager
    {
        CommunicationDataAccess.SMS.SMSDAO smsDAO = null;
        /// <summary>
        /// Author: Zahid Mehmood
        /// Date : 2 January 2012
        /// </summary>
        /// <param name="smsLogDTO"></param>
        /// <returns></returns>
        public string SendSMS(SMSLogDTO smsLogDTO)
        {
            try
            {
                EmailManagerImp objEmailManager = new EmailManagerImp();
                if (smsDAO == null)
                {
                    smsDAO = (CommunicationDataAccess.SMS.SMSDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.SMS.SMSDAO));
                }
             int smsID=  smsDAO.SaveSMSLog(smsLogDTO);
             if (smsID > 0 && smsLogDTO.PhoneNumber.Length > 0)
             {
                 string[] smsPhoneNumber=smsLogDTO.PhoneNumber.Split(';');
                 for (int phoneNumberIndex = 0; phoneNumberIndex < smsPhoneNumber.Length; phoneNumberIndex++)
                 {
                     if (!string.IsNullOrEmpty(smsPhoneNumber[phoneNumberIndex].Trim()))
                     {
                         smsDAO.SaveSMSRecieverList(smsPhoneNumber[phoneNumberIndex], smsLogDTO.UserId, smsID);
                         objEmailManager.MailSending(smsLogDTO.UserId, smsLogDTO.ClinicBranchID, "", smsLogDTO.PhoneNumber + "@txt.att.net", smsLogDTO.Text);
                     }
                 }
                 
             }
            }
            catch (Exception ex)
            {
                 QuivusLogger.Error(ex); //throw;
            }
            return "";
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 2 January 2012
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clinicBranchID"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public SMSResponseDTO GetSentSMSLog(int userID, int clinicBranchID, int currentPage)
        {
            SMSResponseDTO smsLogDTO = null;
            try
            {
                if (smsDAO == null)
                {
                    smsDAO = (CommunicationDataAccess.SMS.SMSDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.SMS.SMSDAO));
                }

                smsLogDTO = smsDAO.GetSMSList(userID,clinicBranchID,currentPage);

               #region GetUserInfo

               int[] userSMSID = new int[smsLogDTO.smsResponseDetail.Count];
               for (int smsCount = 0; smsCount < smsLogDTO.smsResponseDetail.Count; smsCount++)
               {
                   for (int recieverCount = 0; recieverCount < smsLogDTO.smsResponseDetail[smsCount].smsRecieverList.Count; recieverCount++)
                   {
                       userSMSID[smsCount] = smsLogDTO.smsResponseDetail[smsCount].smsRecieverList[recieverCount].UserId;  
                   }
                   
               }
               //     userEmailID.Remove(userEmailID.Length - 1, 1);
               UserManagerService.UserManagerServiceClient objUserManagerService = new UserManagerService.UserManagerServiceClient();
               var userInfo = objUserManagerService.GetUserBasicInfoById(userSMSID);
               if (userInfo.Length > 0)
               {
                   for (int UserData = 0; UserData < smsLogDTO.smsResponseDetail.Count; UserData++)
                   {
                     
                       for (int counter = 0; counter < userInfo.Length; counter++)
                       {
                           for (int recieverCount = 0; recieverCount < smsLogDTO.smsResponseDetail[UserData].smsRecieverList.Count; recieverCount++)
			                {
			                  if (smsLogDTO.smsResponseDetail[UserData].smsRecieverList[recieverCount].UserId.ToString().Trim() == userInfo[counter].UserId.ToString().Trim())
                                    {
                                        smsLogDTO.smsResponseDetail[UserData].smsRecieverList[recieverCount].To = userInfo[counter].FirstName + " " + userInfo[counter].LastName;
                                    }
                           }
                         
                       }

                   }

               }
               #endregion GetUserInfo
              return smsLogDTO;
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex); //throw;
            }
            return smsLogDTO;
        }

        /// <summary>
        /// Author: Zahid Mehmood
        /// Date: 3 january 2012
        /// </summary>
        /// <param name="smsIDs"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string DeleteSMS(string smsIDs, string userID)
        {
            try
            {
                if (smsDAO == null)
                {
                    smsDAO = (CommunicationDataAccess.SMS.SMSDAO)DAOFactory.GetDataAccessObject(typeof(CommunicationDataAccess.SMS.SMSDAO));
                }

                var userSMSIDList = smsIDs.Split('^');
                smsIDs = "<SMS>";
                for (int emailID = 0; emailID < userSMSIDList.Length; emailID++)
                {
                    smsIDs += "<id>" + userSMSIDList[emailID] + "</id>";
                }
                smsIDs += "</SMS>";

                smsDAO.DeleteSMS(smsIDs, userID);
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex); //throw;
            }
            return "";
        }
    }
}
