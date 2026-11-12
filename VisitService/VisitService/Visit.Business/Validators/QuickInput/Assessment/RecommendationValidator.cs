using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Recommendation;
using QuivusUtility.ProjectConstants;

namespace Visit.Business.Validators.QuickInput.Assessment
{
    public class RecommendationValidator : QBaseValidator
    {
        internal static bool ValidateRecommendation(List<PatientRecommendationDTO> RecommendationList, ref StringBuilder sb)
        {

            bool flag = true;

            bool IsUpdate = false;
            if (RecommendationList != null)
            {
                foreach (PatientRecommendationDTO obj in RecommendationList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientRecommendationID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                         //   flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.Recommendation_INVALID_CREATEDBY_ERROR_MSG);
                         //   flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.Recommendation_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                       //     flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.Recommendation_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                     //   flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.Recommendation_INVALID_VISIT_ID_ERROR_MSG);
                     //   flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.Recommendation_INVALID_PATIENT_ID_ERROR_MSG);

                        //  flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //  flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.RECOMMENDATION_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }


        internal static bool ValidateVBRecommendation(List<PatientRecommendationDTO> RecommendationList, ref StringBuilder sb)
        {
            bool flag = true;

            bool IsUpdate = false;
            if (RecommendationList != null)
            {
                foreach (PatientRecommendationDTO obj in RecommendationList)
                {
                    if (obj != null)
                    {
                        if (obj.PatientRecommendationID > 0)
                            IsUpdate = true;

                        if (!IsUpdate)
                        {
                               flag = ValidateKeyId(obj.CreatedBy, sb, flag, ErrorMessages.RECOMMENDATION_INVALID_CREATEDBY_ERROR_MSG);
                               flag = ValidateDate(obj.CreatedAt, sb, flag, ErrorMessages.RECOMMENDATION_INVALID_CREATEDAT_ERROR_MSG);
                        }
                        if (IsUpdate)
                        {
                            flag = ValidateKeyId(obj.ModifiedBy, sb, flag, ErrorMessages.RECOMMENDATION_INVALID_MODIFIEDBY_ERROR_MSG);
                        }

                        flag = ValidateKeyId(obj.VisitID, sb, flag, ErrorMessages.RECOMMENDATION_INVALID_VISIT_ID_ERROR_MSG);
                        flag = ValidateKeyId(obj.PatientID, sb, flag, ErrorMessages.RECOMMENDATION_INVALID_PATIENT_ID_ERROR_MSG);

                        //  flag = ValidateKeyId(obj.ROSMainCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_MAIN_ID_ERROR_MSG);
                        //  flag = ValidateKeyId(obj.ROSSubCategoryTypeID, sb, flag, ErrorMessages.INVALID_ROS_SUB_ID_ERROR_MSG);


                    }
                }
            }
            else
            {
                sb.Append("\n" + ErrorMessages.RECOMMENDATION_LIST_INVALID_ERROR_MSG);
                flag = false;
            }
            return flag;
        }
    }
}
