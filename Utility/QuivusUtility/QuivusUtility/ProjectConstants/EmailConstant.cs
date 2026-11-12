using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusUtility.ProjectConstants
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 1 December 2011
    /// Description: This class contains constants values related to email
    /// </summary>
    public class EmailConstant
    {
        public const string EMAIL_LOG_SP = "NewEmailAttachment";
        public const string EMAIL_LOG_INSERTRECIEVERLIST_SP = "EmailRecieverList_Insert";
        public const string EMAIL_LOG_INSERTSENDERLIST_SP = "EmailSenderList_Insert";
        public const string EMAIL_GETMAILDETAILS_SP = "GetEmailDetails";
        public const string EMAIL_DELETE_SP = "DeleteEmailByID";
        public const string EMAIL_DELETE_SENT_MAILS_SP = "DeleteSentEmailByID";
        //public const string EMAIL_GetAllEmailsOfUser_SP = "GetAllEmails";
        public const string EMAIL_GetAllEmailsOfUser_SP = "LoadUserEmails";
        public const string EMAIL_GetAllSentEmailsOfUser_SP = "LoadUserSentEmails";
        //public const string EMAIL_GetAllSentEmailsOfUser_SP = "GetAllSentEmails";
        public const string EMAIL_GetPathofAttachment_SP = "GetPathOfAttachment";
        public const string EMAIL_GetEmailServerID_SP = "GetEmailServerID";
        public const string EMAIL_UpdateEmailState_SP = "UpdateEmailState";  //// for updation of email state upon gettting email details
        public const string EMAIL_GetEmailSenderList_SP = "GetEmailSenderListID";
        

        
    } 
}
