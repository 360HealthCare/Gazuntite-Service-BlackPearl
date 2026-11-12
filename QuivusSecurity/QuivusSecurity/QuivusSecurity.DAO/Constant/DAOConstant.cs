using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusSecurity.DAO.Constant
{
    public class DAOConstant
    {
        #region Stored Procedures
        
        public const string LOAD_ROLE_APPMODULE_ACCESSRIGHT_SP = "Load_Role_AppModule_AccessRight";

        public const string LOAD_USER_APPMODULE_ACCESSRIGHT_SP = "Load_User_AppModule_AccessRight";

        public const string LOAD_APPMODULES_SP = "Load_AppModules";

        public const string MANAGE_ROLE_APPMODULE_ACCESSRIGHT_SP = "Manage_RoleAppModuleAccessRight";

        public const string MANAGE_USER_APPMODULE_ACCESSRIGHT_SP = "Manage_UserAppModuleAccessRight";

        public const string INSERT_USER_ACCESS_RIGHT_SP = "Insert_User_Access_Right";

        public const string INSERT_USERTYPE_APPMODULEACCESSRIGHT_SP = "Insert_UserType_AppModuleAccessRight";
        
        
        #endregion



        #region Messages

        public const string GENERIC_DTO_CONVERSION_ERROR_MSG = "GenericDataReaderToDTOConverter";
        public const string RECORD_NOT_INSERTED_ERROR_MSG = "Record Not Inserted.";

        #endregion



        public enum ERROR_CODE
        {
            GenericDTOConversion,
            RecordNotInserted

        }

    }
}
