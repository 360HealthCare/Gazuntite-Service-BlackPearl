using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Communication.SMSDTO;
namespace Communication.Business.SMSManager
{
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 17 November 2011
    /// </summary>
    public interface ISMSManager
    {
        //[SMSID] [int] NOT NULL,
        //[ClinicBranchID] [int] NOT NULL,
        //[UserID] [int] NULL,
        //[PhoneNumber] [varchar](20) NOT NULL,
        //[Text] [varchar](1000) NULL,
        //[SendAt] [datetime] NOT NULL,
        //[Deleted] [bit] NOT NULL, -------------------- GlobalDTO
        //[CreatedBy] [int] NOT NULL, -------------------- GlobalDTO
        //[CreatedAt] [datetime] NOT NULL, -------------------- GlobalDTO
        //[ModifiedBy] [int] NULL, --------------- Opional
        //[ModifiedAt] [datetime] NULL, --------------- Opional
        //[SMSTemplateID] [int] NULL,
        string SendSMS(SMSLogDTO oSmsLogDTO);

        SMSResponseDTO GetSentSMSLog(int userID, int clinicBranchID, int currentPage);

        string DeleteSMS(string smsIDs, string userID);
    }
}
