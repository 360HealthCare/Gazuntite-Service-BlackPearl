using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Physical;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Physical.Physicals
{
    public class PhysicalValidator : QBaseValidator
    {
        internal static bool ValidatePhysical(List<PhysicalsDTO> PhysicalList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (PhysicalList != null)
            {
                foreach (PhysicalsDTO obj in PhysicalList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientPhysicalID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                         //   flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.PHYSICALS_INVALID_CREATEDBY_ERROR_MSG);
                         //   flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.PHYSICALS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                        //    flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.PHYSICALS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                   //     flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.PHYSICALS_INVALID_VISIT_ID_ERROR_MSG);
                  //      flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.PHYSICALS_INVALID_PATIENT_ID_ERROR_MSG);

                        flag = ValidateKeyId(obj.PhysicalMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_PHYSICAL_MAIN_ID_ERROR_MSG);
                        flag = ValidateKeyId(obj.PhysicalSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_PHYSICAL_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PHYSICAL_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBPhysical(List<PhysicalsDTO> PhysicalList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (PhysicalList != null)
            {
                foreach (PhysicalsDTO obj in PhysicalList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientPhysicalID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                               flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.PHYSICALS_INVALID_CREATEDBY_ERROR_MSG);
                               flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.PHYSICALS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        //if (IsUpdate && obj.IsDirty)
                        if (IsUpdate)
                        {
                                flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.PHYSICALS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        //     flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.PHYSICALS_INVALID_VISIT_ID_ERROR_MSG);
                        //      flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.PHYSICALS_INVALID_PATIENT_ID_ERROR_MSG);

                        flag = ValidateKeyId(obj.PhysicalMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_PHYSICAL_MAIN_ID_ERROR_MSG);
                        flag = ValidateKeyId(obj.PhysicalSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_PHYSICAL_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.PHYSICAL_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
