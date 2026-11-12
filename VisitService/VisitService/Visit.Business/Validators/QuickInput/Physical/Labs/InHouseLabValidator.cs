using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Labs;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Physical.Labs
{
    public class InHouseLabValidator : QBaseValidator
    {
        internal static bool ValidateInHouseLabs(List<InHouseLabDTO> InputList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (InputList != null)
            {
                foreach (InHouseLabDTO obj in InputList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientInHouseLabTestID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                            //   flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.ROS_INVALID_CREATEDBY_ERROR_MSG);
                            //    flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.ROS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                            //    flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.ROS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        //   flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.ROS_INVALID_VISIT_ID_ERROR_MSG);
                        //    flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.ROS_INVALID_PATIENT_ID_ERROR_MSG);

                        if (obj.InHouseLabTestType != null)
                        {
                            flag = ValidateKeyId(obj.InHouseLabTestType.InHouseLabTestTypeID, sb, flag, ErrorMessages.INVALID_INHOUSELAB_TEST_TYPE_ID_ERROR_MSG);
                        }
                        else
                        {
                            sb.Append("\n" + ErrorMessages.INVALID_IN_HOUSELAB_TEST_TYPE_ERROR_MSG);
                            flag = false;
                        }
                     


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.ROS_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }

        internal static bool ValidateVBInHouseLabs(List<InHouseLabDTO> InputList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (InputList != null)
            {
                foreach (InHouseLabDTO obj in InputList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientInHouseLabTestID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                               flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.ROS_INVALID_CREATEDBY_ERROR_MSG);
                                flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.ROS_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                                flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.ROS_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                           flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.ROS_INVALID_VISIT_ID_ERROR_MSG);
                           flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.ROS_INVALID_PATIENT_ID_ERROR_MSG);

                        if (obj.InHouseLabTestType != null)
                        {
                            flag = ValidateKeyId(obj.InHouseLabTestType.InHouseLabTestTypeID, sb, flag, ErrorMessages.INVALID_INHOUSELAB_TEST_TYPE_ID_ERROR_MSG);
                        }
                        else
                        {
                            sb.Append("\n" + ErrorMessages.INVALID_IN_HOUSELAB_TEST_TYPE_ERROR_MSG);
                            flag = false;
                        }
                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.ROS_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
