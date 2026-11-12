using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.VisitService;
using System.ServiceModel.Web;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using System.IO;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using QuivusManager.IntegratedServies.UserManagerService;

namespace QuivusManagerService.QuivusManagerRestfullService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQuivusManagerRestfullService" in both code and config file together.
    [ServiceContract]
    public interface IQuivusManagerRestfullService
    {


        


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Xml,
                    RequestFormat = WebMessageFormat.Json,
                    Method = "POST",
                    BodyStyle = WebMessageBodyStyle.Bare,UriTemplate = "TestDateConcept")]
        String TestDateConcept(QuickInputDTO p);


        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "TestQIConcept")]
        String TestQIConcept();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Xml, 
                    RequestFormat = WebMessageFormat.Json, 
                    Method = "POST", 
                    BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ManageQuickInputData/{managedByUserId}/{sessionID}")]
        ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, String managedByUserId, String sessionID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customICDCodeDTO"></param>
        /// <param name="userID"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Xml,
            RequestFormat = WebMessageFormat.Json,
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "ManageCustomICDCode/{userID}/{sessionID}")]
        bool ManageCustomICDCode(QuivusManager.IntegratedServies.VisitService.ICDCodeDTO customICDCodeDTO, String userID, String sessionID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="fileName"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Xml,
                    RequestFormat = WebMessageFormat.Json,
                    Method = "POST",
                    BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SaveTempQI/{FileName}/{sessionID}")]
        bool SaveTempQI(QuickInputDTO quickInputDTO, String fileName, String sessionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Xml,
                    RequestFormat = WebMessageFormat.Json,
                    Method = "POST",
                    BodyStyle = WebMessageBodyStyle.Bare, 
                    UriTemplate = "GenerateSoapNoteByVisitID/{VisitID}/{userSession}/{clinicBranchId}/{stationIds}/{isAllSelected}/{isMale}/{isTemp}")]
        string GenerateSoapNoteByVisitID(QuickInputDTO quickInputDTO, string VisitID, string userSession, string clinicBranchId, string stationIds, string isAllSelected,string IsMale, string isTemp);
    }

    

    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name;
        [DataMember]
        public DateTime DOB;
    }
    //[DataContract]
    //public class HTML
    //{
    //    string body { get; set; }
    //}
}
