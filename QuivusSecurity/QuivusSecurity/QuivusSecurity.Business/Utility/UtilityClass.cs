using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusModel.Security.AppDTO.BaseDTO;
using QuivusModel.Security;
using System.Reflection;

namespace QuivusSecurity.Business.Utility
{
    public class UtilityClass
    {
        public static void PopulateDTOForAccessRight(ModuleARBaseDTO dto, AccessRightDTO arDTO)
        {

            dto.ModuleName = arDTO.AppModuleName;
            dto.ModuleId = arDTO.AppModuleIdSeq;
            dto.ParentId = arDTO.ParentId;
            dto.IsViewAllowed = arDTO.IsViewable;
            dto.IsEditAllowed = arDTO.IsEditable;
            dto.RoleAppModuleAccessRightIdSeq = arDTO.UserTypeAppModuleAccessRightIdSeq;
        }


        public static AccessRightDTO GetAppSection(List<AccessRightDTO> list, string sectionName)
        {
            AccessRightDTO toReturn = null;
            if(list != null)
            {
                for(int i=0;i<list.Count;i++)
                {
                    AccessRightDTO dto = list[i];
                    if (dto.AppModuleName == sectionName)
                    {
                        toReturn = dto;
                        break;
                    }
                }
            }
            return toReturn;

        }

        
    }
}
