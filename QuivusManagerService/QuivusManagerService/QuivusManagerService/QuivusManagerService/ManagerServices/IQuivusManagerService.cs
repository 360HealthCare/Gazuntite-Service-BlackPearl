using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using QuivusManager.ServiceResponse.ServiceCustomSession;


namespace QuivusManagerService.QuivusManagerService.ManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQuivusManagerService" in both code and config file together.

    [ServiceContract(Namespace = "http://quivus.com/QuivusManagerService")]
   //[ServiceContract]
    public partial interface IQuivusManagerService
    {
        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 14 Jan 2011
        /// This Method Is Used To Load the user login history
        /// Update Date 07 Mar-2011
        /// Method Return Type Change From Void TO User-Session DTO
        /// This method will also be used to maintain user session
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="clinicBranch"></param>
        /// <param name="isSucessFull"></param>
        [OperationContract]
        UserSessionDTO LogLoginHistory(int userId, int userType, int clinicBranch, bool isSucessFull);

        /// <summary>
        /// Author Mansoor
        /// Date 8 Sep 2011
        /// </summary>
        /// <param name="sessionId"></param>
        [OperationContract]
        void SignOutUserSession(Guid sessionId);

        /// <summary>
        /// Author Mansoor
        /// Created Date 17 Jan 2011
        /// This method is used to implement send email functionality
        /// </summary>
        /// <param name="requestedUserId"></param>
        /// <param name="subject"></param>
        /// <param name="receiptEmail"></param>
        /// <param name="message"></param>
        //M [OperationContract]        
        //M QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(int requestedUserId, int clinicBranchID, string subject, string receiptEmail, string message);

        //[OperationContract(Name="SendEmailWithAttachment")]
        //QuivusManager.ServiceResponse.ServiceCustomResponse.EmailResponse SendEmail(EmailDTO data);

        [OperationContract]
         bool PingService();

        [OperationContract]
        string PingAllClientServices();

        [OperationContract]
        string GetResponseAsXML();


        [OperationContract]
        DateTime DateTestServices(DateTime date);


        /// <summary>
        /// Author Muhammad Ismail
        /// Created Date 08 Sep 2011
        /// This Method Is Used To Update the user session
        /// In Case User Set KickOutPreviousSession to true we will discard previous active session
        /// In Case User Set KickOutPreviousSession to false we will discard new crested session
        /// </summary>
        /// <param name="SessionObj"></param>
        [OperationContract]
        UserSessionDTO UpdateUserSession(UserSessionDTO SessionObj);

        //[OperationContract]
        //string GetServiceVersion();
    }
}
