using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using LiveProvider.Model.Test;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using LiveProvider.Model.Response;
using LiveProvider.Model.Response.LabManagement;
using System.Xml;

namespace LiveProvider.ELabsManager.Service
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IElabManagerService
    {

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will load all lab order on the bases of clinic branch ids
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadOrderByClinicBranch/{clinicBranchID}/{pageNo}", BodyStyle = WebMessageBodyStyle.Bare)]
        LabPaggingResponseDTO LoadOrderByClinicBranch(string clinicBranchID, string pageNo);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 20 Feb 2012
        /// This method will return search fields for order and result management on the bases of parameters.
        /// </summary>
        /// <param name="IsLabOrder"></param>
        /// <param name="IsLabResult"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadStaticData", BodyStyle = WebMessageBodyStyle.Bare)]
        StaticDataResponse LoadStaticData();

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 13 Feb 2012
        /// This method will load all lab results on the bases of clinic branch ids
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadLabResultsByClinicBranch/{clinicBranchID}/{pageNo}", BodyStyle = WebMessageBodyStyle.Bare)]
        ResultPagingResponse LoadResultsByClinicBranch(string clinicBranchID, string pageNo);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 21 Feb 2012
        /// This method will process lab order
        /// </summary>
        /// <param name="orderNumbers"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "ProcessLabOrder/{date}", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST")]
        ServiceResponse ProcessLabOrder(List<string> orderNumbers, string date);

        [OperationContract]
        [WebGet(UriTemplate = "Test", BodyStyle = WebMessageBodyStyle.Bare)]
        List<string> Test();

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 21 Feb 2012
        /// This method will search Lab orders
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchText"></param>
        /// <param name="fieldID"></param>
        /// <param name="providerId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "SearchLabOrder/{clinicBranchID}/{pageNo}/{searchText}/{fieldID}/{providerId}/{userTypeId}", BodyStyle = WebMessageBodyStyle.Bare)]
        LabPaggingResponseDTO SearchLabOrder(string clinicBranchID, string pageNo, string searchText, string fieldID, string providerId, string userTypeId);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 21 Feb 2012
        /// This method will search Lab results
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchText"></param>
        /// <param name="fieldID"></param>
        /// <param name="providerId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "SearchLabResult/{clinicBranchID}/{pageNo}/{searchText}/{fieldID}/{providerId}/{userTypeId}", BodyStyle = WebMessageBodyStyle.Bare)]
        ResultPagingResponse SearchLabResult(string clinicBranchID, string pageNo, string searchText, string fieldID, string providerId, string userTypeId);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 01 March 2012
        /// This method will filter lab order and will use with list only popup.
        /// </summary>
        /// <param name="clinicBranchID"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchText"></param>
        /// <param name="fieldID"></param>
        /// <param name="providerId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "FilterlabOrders/{clinicBranchID}/{pageNo}/{searchText}", BodyStyle = WebMessageBodyStyle.Bare)]
        LabPaggingResponseDTO FilterlabOrders(string clinicBranchID, string pageNo, string searchText);
         
        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 06 March 2012
        /// This method will filter lab result and will use with list only popup.</summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "FilterPatientLabResults/{clinicBranchID}/{pageNo}/{searchText}", BodyStyle = WebMessageBodyStyle.Bare)]
        ResultPagingResponse FilterPatientLabResults(string clinicBranchId, string pageNo, string searchText);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 06 March 2012
        /// This method will process lab with lab only popup.</summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "ProcessLabOrderByCriteria/{searchText}", BodyStyle = WebMessageBodyStyle.Bare)]
        ServiceResponse ProcessLabOrderByCriteria(string searchText);

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 07 March 2012
        /// This method will update and delete lab order
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        //[OperationContract]
        [WebInvoke(UriTemplate = "ManageLabOrder", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST")]
        ServiceResponse ManageLabOrder(XmlDocument order);


        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 08 March 2012
        /// This method will return lab test list for auto complete.</summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="pageNo"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadTestTypeByName/{searchText}", BodyStyle = WebMessageBodyStyle.Bare)]
        TestTypeResponse LoadTestTypeByName(string searchText);
    }
}
