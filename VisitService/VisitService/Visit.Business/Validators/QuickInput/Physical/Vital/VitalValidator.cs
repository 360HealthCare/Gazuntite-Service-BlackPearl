using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Utility;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Physical.Vital
{
    public class VitalValidator : QBaseValidator
    {
        internal static bool ValidateVitalDTO(VitalDTO VitalObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (VitalObject != null)
            {
                if (!IsUpdate)
                {
                 //   flag = ValidateKeyId(VitalObject.CreatedBy, sb, flag, ErrorMessages.VITAL_INVALID_CREATEDBY_ERROR_MSG);
                  //  flag = ValidateDate(VitalObject.CreatedAt, sb, flag, ErrorMessages.VITAL_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                 //   flag = ValidateKeyId(VitalObject.ModifiedBy, sb, flag, ErrorMessages.VITAL_INVALID_MODIFIEDBY_ERROR_MSG);
                }

             //   flag = ValidateKeyId(VitalObject.VisitID, sb, flag, ErrorMessages.VITAL_INVALID_VISIT_ID_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.VITAL_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBVitalDTO(VitalDTO VitalObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (VitalObject != null)
            {
                if (!IsUpdate)
                {
                       flag = ValidateKeyId(VitalObject.CreatedBy, sb, flag, ErrorMessages.VITAL_INVALID_CREATEDBY_ERROR_MSG);
                      flag = ValidateDate(VitalObject.CreatedAt, sb, flag, ErrorMessages.VITAL_INVALID_CREATEDAT_ERROR_MSG);
                }
               // if (IsUpdate && VitalObject.IsDirty)
                if (IsUpdate )
                {
                       flag = ValidateKeyId(VitalObject.ModifiedBy, sb, flag, ErrorMessages.VITAL_INVALID_MODIFIEDBY_ERROR_MSG);
                }

                   flag = ValidateKeyId(VitalObject.VisitID, sb, flag, ErrorMessages.VITAL_INVALID_VISIT_ID_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.VITAL_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
