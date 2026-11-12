using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Labs;
using Quivus.Utility;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Physical.Labs
{
    public class LabValidator : QBaseValidator
    {
        internal static bool ValidatLabDTO(LabDTO LabObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (LabObject != null)
            {
                if (!IsUpdate)
                {
               //     flag = ValidateKeyId(LabObject.CreatedBy, sb, flag, ErrorMessages.LABS_INVALID_CREATEDBY_ERROR_MSG);
               //     flag = ValidateDate(LabObject.CreatedAt, sb, flag, ErrorMessages.LABS_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
               //     flag = ValidateKeyId(LabObject.ModifiedBy, sb, flag, ErrorMessages.LABS_INVALID_MODIFIEDBY_ERROR_MSG);
                }

             //   flag = ValidateKeyId(LabObject.VisitID, sb, flag, ErrorMessages.LABS_INVALID_VISIT_ID_ERROR_MSG);

                if (LabObject.LabTestType == null)
                {
                    sb.Append("\n" + ErrorMessages.INVALID_LAB_TEST_ERROR_MSG);
                    flag = false;
                }
                else
                {
                    flag = ValidateKeyId(LabObject.LabTestType.LabTestTypeID, sb, flag, ErrorMessages.INVALID_LAB_TEST_ID_ERROR_MSG);
                }

                if (LabObject.LIONICLabTestType == null)
                {
                    sb.Append("\n" + ErrorMessages.INVALID_LAB_LIONIC_TEST_TYPE_ERROR_MSG);
                    flag = false;
                }
                else
                {
                    flag = ValidateKeyId(LabObject.LIONICLabTestType.LionicLabTestTypeID, sb, flag, ErrorMessages.INVALID_LAB_LIONIC_TEST_TYPE_ID_ERROR_MSG);
                }

                flag = ValidateStringInfo(LabObject.Onset, sb, flag, ErrorMessages.INVALID_LAB_ONSET_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.LABS_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }



        internal static bool ValidatVBLabDTO(LabDTO LabObject, ref StringBuilder sb, bool IsUpdate)
        {
            bool flag = true;

            //M TODO: implement it           

            if (LabObject != null)
            {
                if (!IsUpdate)
                {
                         flag = ValidateKeyId(LabObject.CreatedBy, sb, flag, ErrorMessages.LABS_INVALID_CREATEDBY_ERROR_MSG);
                        flag = ValidateDate(LabObject.CreatedAt, sb, flag, ErrorMessages.LABS_INVALID_CREATEDAT_ERROR_MSG);
                }
                if (IsUpdate)
                {
                         flag = ValidateKeyId(LabObject.ModifiedBy, sb, flag, ErrorMessages.LABS_INVALID_MODIFIEDBY_ERROR_MSG);
                }

                   flag = ValidateKeyId(LabObject.VisitID, sb, flag, ErrorMessages.LABS_INVALID_VISIT_ID_ERROR_MSG);

                if (LabObject.LabTestType == null)
                {
                    sb.Append("\n" + ErrorMessages.INVALID_LAB_TEST_ERROR_MSG);
                    flag = false;
                }
                else
                {
                    flag = ValidateKeyId(LabObject.LabTestType.LabTestTypeID, sb, flag, ErrorMessages.INVALID_LAB_TEST_ID_ERROR_MSG);
                }

                if (LabObject.LIONICLabTestType == null)
                {
                    sb.Append("\n" + ErrorMessages.INVALID_LAB_LIONIC_TEST_TYPE_ERROR_MSG);
                    flag = false;
                }
                else
                {
                    flag = ValidateKeyId(LabObject.LIONICLabTestType.LionicLabTestTypeID, sb, flag, ErrorMessages.INVALID_LAB_LIONIC_TEST_TYPE_ID_ERROR_MSG);
                }

                flag = ValidateStringInfo(LabObject.Onset, sb, flag, ErrorMessages.INVALID_LAB_ONSET_ERROR_MSG);

            }
            else
            {
                sb.Append("\n" + ErrorMessages.LABS_DTO_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
