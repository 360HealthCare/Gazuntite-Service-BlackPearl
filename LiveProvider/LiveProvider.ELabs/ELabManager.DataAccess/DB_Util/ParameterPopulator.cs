using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.QuickInput;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.Errors;

namespace ELabManager.DataAccess.DB_Util
{
    public class ParameterPopulator : BasePopulator
    {
        internal static bool PopulateLabParameters(LabDTO LabObject, bool IsUpdate, Database db, DbCommand command)
        {
            bool SkipRecord = false;
            //if (IsUpdate && !LabObject.IsDirty)
            //{
            //    // This Record should Not Updated Skip this Recokrd Do Nothing
            //    SkipRecord = true;
            //}
            try
            {
                //if (!SkipRecord)
                //{

                db.AddInParameter(command, "@Description", DbType.String, LabObject.Description);

                if (LabObject.LabTestPriorityType != null)
                    SetValueIfNotNull("@LabTestPriorityTypeID", LabObject.LabTestPriorityType.LabTestPriorityTypeID, db, command);
                else
                    SetNullValue("@LabTestPriorityTypeID", db, command);

                if (LabObject.ICDCode != null)
                    SetValueIfNotNull("@ICDCodeID", LabObject.ICDCode.IcdCodeIdSeq, db, command);
                else
                    SetNullValue("@ICDCodeID", db, command);



                //
                if (string.IsNullOrEmpty(LabObject.IsFasting))
                {
                    db.AddInParameter(command, "@IsFasting", DbType.Boolean, DBNull.Value);
                }
                else if (LabObject.IsFasting.Trim().Equals(ELabContant.SELECTION_TRUE) || LabObject.IsFasting.Trim().Equals(ELabContant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsFasting", DbType.Boolean, true);
                }
                else if (LabObject.IsFasting.Trim().Equals(ELabContant.SELECTION_False))
                {
                    db.AddInParameter(command, "@IsFasting", DbType.Boolean, false);
                }
                else if (LabObject.IsFasting.Trim().Equals(ELabContant.SELECTION_NULL))
                {
                    db.AddInParameter(command, "@IsFasting", DbType.Boolean, DBNull.Value);
                }
                //
                if (LabObject.LabTestSpecimenType != null)
                    SetValueIfNotNull("@LabTestSpecimenTypeID", LabObject.LabTestSpecimenType.LabTestSpecimenTypeID, db, command);
                else
                    SetNullValue("@LabTestSpecimenTypeID", db, command);

                if (LabObject.LabTestOrderMethodType != null)
                    SetValueIfNotNull("@LabTestOrderMethodTypeID", LabObject.LabTestOrderMethodType.LabTestOrderMethodTypeID, db, command);
                else
                    SetNullValue("@LabTestOrderMethodTypeID", db, command);

                //if (LabObject.LabTestNatureType != null)
                //    SetValueIfNotNull("@LabTestNatureTypeID", LabObject.LabTestNatureType.LabTestNatureTypeID, db, command);

                if (LabObject.LIONICLabTestType != null)
                    db.AddInParameter(command, "@LionicLabTestTypeID", DbType.Int32, LabObject.LIONICLabTestType.LionicLabTestTypeID);



                if (LabObject.LabType != null)
                    SetValueIfNotNull("@LabTypeID", LabObject.LabType.LabTypeID, db, command);
                else
                    SetNullValue("@LabTypeID", db, command);


                db.AddInParameter(command, "@CollectionDate", DbType.String, LabObject.CollectionDate);
                db.AddInParameter(command, "@OrderDate", DbType.String, LabObject.OrderDate);

                //   SetDateIfNotNull("@CollectionDate", LabObject.CollectionDate, db, command);

                //    SetDateIfNotNull("@OrderDate", LabObject.OrderDate, db, command);

                SetValueIfNotNull("@UserID", LabObject.UserID, db, command);

                db.AddInParameter(command, "@Status", DbType.String, LabObject.Status);

                db.AddInParameter(command, "@Onset", DbType.DateTime, LabObject.Onset);

                db.AddInParameter(command, "@LabTestTypeID", DbType.Int32, LabObject.LabTestType.LabTestTypeID);

                SetValueIfNotNull("@LabID", LabObject.LabID, db, command);
                db.AddInParameter(command, "@IsLabIn", DbType.Boolean, LabObject.IsLabIn);

                db.AddInParameter(command, "@LabNote", DbType.String, LabObject.LabNote);

                db.AddInParameter(command, "@NoteSubject", DbType.String, LabObject.NoteSubject);

                db.AddInParameter(command, "@VisitID", DbType.Int32, LabObject.VisitID);

                SetDictationIfNotNull("@DictationID", LabObject.DictationID, db, command);

                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, LabObject.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, LabObject.CreatedBy);
                    db.AddInParameter(command, "@PatientLabIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientLabID", DbType.Int32, LabObject.PatientLabID);
                    db.AddInParameter(command, "@IsDeleted", DbType.Boolean, LabObject.IsDeleted);

                    SetValueIfNotNull("@ModifiedBy", LabObject.ModifiedBy, db, command);
                    // db.AddInParameter(command, "@ModifiedBy", DbType.Int32, LabObject.ModifiedBy);
                    SetDateIfNotNull("@ModifiedAt", LabObject.ModifiedAt, db, command);
                    //db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, LabObject.ModifiedAt);
                }
                //}
            }
            catch (Exception exp)
            {
                // throw;
                throw new ParameterPopulatorException(ELabContant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                  ErrorCodes.SECTION_ELAB_MANAGEMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return SkipRecord;
        }

        internal static void PopulateLabParameters(LabDTO LabObject, bool IsUpdate, int managedByUserId, int VisitId, DateTime ActionDate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@Description", DbType.String, LabObject.Description);

                if (LabObject.LabTestPriorityType != null)
                    SetValueIfNotNull("@LabTestPriorityTypeID", LabObject.LabTestPriorityType.LabTestPriorityTypeID, db, command);
                else
                    SetNullValue("@LabTestPriorityTypeID", db, command);

                if (LabObject.ICDCode != null)
                    SetValueIfNotNull("@ICDCodeID", LabObject.ICDCode.IcdCodeIdSeq, db, command);
                else
                    SetNullValue("@ICDCodeID", db, command);

                db.AddInParameter(command, "@IsFasting", DbType.Boolean, LabObject.IsFasting);

                if (LabObject.LabTestSpecimenType != null)
                    SetValueIfNotNull("@LabTestSpecimenTypeID", LabObject.LabTestSpecimenType.LabTestSpecimenTypeID, db, command);
                else
                    SetNullValue("@LabTestSpecimenTypeID", db, command);

                if (LabObject.LabTestOrderMethodType != null)
                    SetValueIfNotNull("@LabTestOrderMethodTypeID", LabObject.LabTestOrderMethodType.LabTestOrderMethodTypeID, db, command);
                else
                    SetNullValue("@LabTestOrderMethodTypeID", db, command);

                if (LabObject.LIONICLabTestType != null)
                    db.AddInParameter(command, "@LionicLabTestTypeID", DbType.Int32, LabObject.LIONICLabTestType.LionicLabTestTypeID);

                if (LabObject.LabType != null)
                    SetValueIfNotNull("@LabTypeID", LabObject.LabType.LabTypeID, db, command);
                else
                    SetNullValue("@LabTypeID", db, command);

                db.AddInParameter(command, "@CollectionDate", DbType.String, LabObject.CollectionDate);
                db.AddInParameter(command, "@OrderDate", DbType.String, LabObject.OrderDate);
                //  SetDateIfNotNull("@CollectionDate", LabObject.CollectionDate, db, command);

                //   SetDateIfNotNull("@OrderDate", LabObject.OrderDate, db, command);

                SetValueIfNotNull("@UserID", managedByUserId, db, command);

                db.AddInParameter(command, "@Status", DbType.String, LabObject.Status);

                db.AddInParameter(command, "@Onset", DbType.DateTime, LabObject.Onset);

                db.AddInParameter(command, "@LabTestTypeID", DbType.Int32, LabObject.LabTestType.LabTestTypeID);

                SetValueIfNotNull("@LabID", LabObject.LabID, db, command);
                db.AddInParameter(command, "@IsLabIn", DbType.Boolean, LabObject.IsLabIn);

                db.AddInParameter(command, "@LabNote", DbType.String, LabObject.LabNote);

                db.AddInParameter(command, "@NoteSubject", DbType.String, LabObject.NoteSubject);

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                if(LabObject.PatientID > 0)
                    db.AddInParameter(command, "@PatientID", DbType.Int32, LabObject.PatientID);
                if(LabObject.SignedByUserID > 0)
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, LabObject.SignedByUserID);
                if(LabObject.ClinicBranchID > 0)
                    db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, LabObject.ClinicBranchID);
                if(LabObject.StationID > 0)
                    db.AddInParameter(command, "@StationID", DbType.Int32, LabObject.StationID);

                SetDictationIfNotNull("@DictationID", LabObject.DictationID, db, command);

                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedByUserId);
                    db.AddInParameter(command, "@PatientLabIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientLabID", DbType.Int32, LabObject.PatientLabID);
                    db.AddInParameter(command, "@IsDeleted", DbType.Int32, LabObject.IsDeleted);

                    SetValueIfNotNull("@ModifiedBy", managedByUserId, db, command);
                    // db.AddInParameter(command, "@ModifiedBy", DbType.Int32, LabObject.ModifiedBy);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                    //db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, LabObject.ModifiedAt);
                }
            }
            catch (Exception exp)
            {
                // throw;
                throw new ParameterPopulatorException(ELabContant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_LIVE_PROVIDER_PREFIX,
                                  ErrorCodes.SECTION_ELAB_MANAGEMENT_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }
    }
}
