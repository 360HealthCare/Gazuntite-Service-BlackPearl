using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomSession;


namespace QuivusManager.Buisness.ManagerBL
{
    public interface IManagerBL
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load the user login history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        void LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull);

         /* Commented By Ismail Moved TO Streaming Service
        EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message); */

        UserSessionDTO UpdateUserSession(UserSessionDTO SessionObj);

        void SignOutUserSession(Guid sessionId);
    }
}
