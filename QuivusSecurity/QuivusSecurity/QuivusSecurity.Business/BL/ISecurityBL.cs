using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceResponce.ResponceDTO.Static;
using ServiceResponce.ResponceDTO;

namespace QuivusSecurity.Business.BL
{

    [ServiceContract]
    public interface ISecurityBL
    {

        UserTypeResponce LoadAllUserTypes();

        AccessRightResponce LoadRoleAccessRight(string userTypeId, String clinicBranchId);

        AccessRightResponce LoadUserAccessRight(string uId, string uTypeId, string cbId);

        bool ManageRoleAccessRight(AccessRightResponce roleAccessRightResponceDTO, string userTypeId, string clinicBranchId);

        bool ManageUserAccessRight(AccessRightResponce roleAccessRightResponceDTO, string userId, string userTypeId, string clinicBranchId);

        AppModuleTypeResponce LoadAppModules();




        bool GenerateUserTypeAccessRights();

        bool GenerateUserAccessRights();
    }
}
