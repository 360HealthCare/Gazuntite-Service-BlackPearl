using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Xml;
using QuivusModel;
using QuivusModel.Communication.MailDTO;
using QuivusModel.Communication.SMSDTO;
namespace QuivusCommunicationService
{
    
    

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    
    //[ServiceContract(Namespace="EmailCommunication")]
    /// <summary>
    /// Author: Zahid Mehmood
    /// Date: 30 December 2011
    /// Description: Interface of Communications model includes SMS,EMAIL
    /// </summary>
    [ServiceContract]
    [XmlSerializerFormat]
    public partial interface ICommunicationService
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "SMSSave/", Method = "POST")]
        string SendSMS(XmlDocument xmlSmsDTO);

        [OperationContract]
        [WebGet(UriTemplate = "SMSSentLog/{userID}/{clinicBranchID}/{currentPage}")]
        SMSResponseDTO GetSMSLog(string userID, string clinicBranchID, string currentPage);

        [OperationContract]
        [WebInvoke(UriTemplate = "DeleteSMS/{smsID}/{userID}", Method = "GET")]
        string DeleteSMS(string smsID, string userID);

        [OperationContract]
        [WebInvoke(UriTemplate = "EmailCommunicationSendEmail/", Method = "POST")]
        string SendEmail(XmlDocument emailDTO);
        //string SendEmail(QuivusModel.Communication.MailDTO.EmailDTO oEmailDTO);

         [OperationContract]
        [WebGet(UriTemplate = "GetEmail/{userEmailID}/{userID}/{clinicBrachID}/{stationID}",BodyStyle=WebMessageBodyStyle.Bare)]
        EmailDTO GetEmailDetails(string userEmailID, string userID, string clinicBrachID, string stationID);

         [OperationContract(Name="Emails")]
         [WebGet(UriTemplate = "EmailCommunicationGetAllEmails/{userID}/{clinicBrachID}/{stationID}/{pageSize}/{currentPage}", BodyStyle = WebMessageBodyStyle.Bare)]
         MailDTO GetAllEmails(string userID, string clinicBrachID, string stationID, string pageSize, string currentPage);

         [OperationContract]
         [WebGet(UriTemplate = "EmailCommunicationGetAllSentMails/{userID}/{clinicBrachID}/{stationID}/{pageSize}/{currentPage}", BodyStyle = WebMessageBodyStyle.Bare)]
         MailDTO GetAllSentMails(string userID, string clinicBrachID, string stationID, string pageSize, string currentPage);

         [OperationContract]
         [WebInvoke(UriTemplate = "EmailCommunicationDelete/{userEmailID}/{userID}", Method = "GET")]
         string DeleteEmail(string userEmailID, string userID);

         [OperationContract]
         [WebInvoke(UriTemplate = "EmailCommunicationDeleteSentMail/{userEmailID}/{userID}", Method = "GET")]
         string DeleteSentEmail(string userEmailID, string userID);

         [OperationContract]
         [WebInvoke(UriTemplate = "UploadFile/{fileName}")]
         void uploadfile( string fileName,Stream fileStream);

         [OperationContract]
         [WebGet(UriTemplate = "EmailCommunicationGetfile/{userEmailID}/{fileName}/{fileExtension}", BodyStyle = WebMessageBodyStyle.Bare)]
         Stream DownloadFile(string userEmailID,string fileName, string fileExtension);

        [OperationContract]
        [WebInvoke(UriTemplate = "AlertSaving/", Method = "POST")]
        string SaveAlert(QuivusModel.Communication.AlertsDTO.AlertDTO oAlertDTO);

        [OperationContract]
        [WebGet(UriTemplate = "Test/{iDigit}")]
        int Multiply(string iDigit);

        [OperationContract]
        [WebInvoke(UriTemplate = "fileupload/{userID}/{sFileName}", Method = "POST")]
        void Upload(string userID,string sFileName, Stream data);
        

        /////////////// Test ///////////////////////
        [OperationContract]
        [WebInvoke(UriTemplate = "fileuploading1/{Testing}")]
        string Uploaded(string Testing, Stream data);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetTestEmails/{iUserID}/{iClinicBrachID}/{iStationID}")]
        //List<MailDTO> GetAllTestMails(string iUserID, string iClinicBrachID, string iStationID);


        [OperationContract]
        [WebInvoke(UriTemplate = "TestUpload/{fileName}", Method = "POST")]
        void TestUpload(string fileName,Stream fileStream);


        ///////////////
    }
}
