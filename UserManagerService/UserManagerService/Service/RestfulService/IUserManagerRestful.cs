using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.Search;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace UserManagerService.Service.RestfulService
{
    [ServiceContract]
    public interface IUserManagerRestful
    {
        [OperationContract]
        [WebGet(UriTemplate = "SearchPatientbyClinicIdAndSearchString/{clinicBranchId}/{providerId}/{userTypeId}/{searchString}/{demandPageNo}", BodyStyle = WebMessageBodyStyle.Bare)]
        PatientSearchResultDTO SearchPatientbyClinicIdAndSearchString(string clinicBranchId, string providerId, string userTypeId, string searchString, string demandPageNo);
    }
}
