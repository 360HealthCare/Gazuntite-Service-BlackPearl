using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using ServiceResponce.ResponceDTO.Static;
using ServiceResponce.ResponceDTO;
using System.Xml;

namespace QuivusSecurityService.Service.Rest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQuivusSecurityService" in both code and config file together.
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IQuivusSecurityService
    {
        [OperationContract]
        [WebGet(UriTemplate = "TestSecurityService")]
        string TestSecurityService();


        [OperationContract]
        [WebGet(UriTemplate = "LoadAllUserTypes", BodyStyle = WebMessageBodyStyle.Bare)]
        UserTypeResponce LoadAllUserTypes();


        [OperationContract]
        [WebGet(UriTemplate = "LoadAppModules")]
        AppModuleTypeResponce LoadAppModules();


        [OperationContract]
        [WebGet(UriTemplate = "LoadRoleAccessRight/{userTypeId}/{clinicBranchId}")]
        AccessRightResponce LoadRoleAccessRight(string userTypeId, String clinicBranchId);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ManageRoleAccessRight/{userTypeId}/{clinicBranchId}")]
        bool ManageRoleAccessRight(XmlDocument manageRoleAccessRightXml, string userTypeId, string clinicBranchId);




        [OperationContract]
        [WebGet(UriTemplate = "LoadUserAccessRight/{userId}/{userTypeId}/{clinicBranchId}")]
        AccessRightResponce LoadUserAccessRight(string userId, string userTypeId, string clinicBranchId);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ManageUserAccessRight/{userId}/{userTypeId}/{clinicBranchId}")]
        bool ManageUserAccessRight(XmlDocument manageRoleAccessRightXml, string userId, string userTypeId, string clinicBranchId);






        [OperationContract]
        [WebGet(UriTemplate = "GenerateUserTypeAccessRights")]
        bool GenerateUserTypeAccessRights();

        [OperationContract]
        [WebGet(UriTemplate = "GenerateUserAccessRights")]
        bool GenerateUserAccessRights();

    }
}
