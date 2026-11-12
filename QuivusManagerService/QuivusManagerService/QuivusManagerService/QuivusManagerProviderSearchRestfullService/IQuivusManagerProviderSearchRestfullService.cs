using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QuivusManager.IntegratedServies.UserManagerService;
using System.ServiceModel.Web;

namespace QuivusManagerService.QuivusManagerProviderSearchRestfullService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQuivusManagerProviderSearchRestfullService" in both code and config file together.
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IQuivusManagerProviderSearchRestfullService
    {
        #region ProviderSearch
        /// <summary>
        /// Author:Zahid Mehmood
        /// Date: 13 Jan 2012
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "LoadUsersByClinicBranchID/{ClinicBranchId}")]
        List<ContactSearchDTO> LoadUsersByClinicBranchID(string clinicBranchId);


        [OperationContract]
        [WebGet(UriTemplate = "LoadClinicUsersBasicInfo/{userId}/{clinicBranchId}")]
        ClinicProviderSearchDTO LoadUsersDetails(string userId, string clinicBranchId);

        #endregion ProviderSearch
    }
}
