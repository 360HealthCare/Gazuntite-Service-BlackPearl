using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Communication;
using Quivus.Model.Image;

namespace StreamingService.Buisness.EmailManager
{
    public interface IEmailManager
    {
        /// <summary>
        /// Author Mansoor
        /// Date 17 Jan 2011
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        string SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message);

        /// <summary>
        /// Author Ali Noor
        /// </summary>
        /// <param name="emailDTO"></param>
        /// <returns></returns>
        int SendEmailWithAttachment(EmailDTO emailDTO);

      
    }
}
