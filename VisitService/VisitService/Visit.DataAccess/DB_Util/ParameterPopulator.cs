using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.Labs;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Utility;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput.Medication;
using QuivusUtility.CustomException;
using QuivusUtility.ProjectConstants;
using Quivus.Utility.Logger;

namespace Visit.DataAccess.DB_Util
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
                    else if (LabObject.IsFasting.Trim().Equals(ProjectConstant.SELECTION_TRUE) || LabObject.IsFasting.Trim().Equals(ProjectConstant.SELECTION_TRUE))
                    {
                        db.AddInParameter(command, "@IsFasting", DbType.Boolean, true);
                    }
                    else if (LabObject.IsFasting.Trim().Equals(ProjectConstant.SELECTION_False))
                    {
                        db.AddInParameter(command, "@IsFasting", DbType.Boolean, false);
                    }
                    else if (LabObject.IsFasting.Trim().Equals(ProjectConstant.SELECTION_NULL))
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

                // Changes by baber on 19/10/2011 for fixing error converting date time to nvarchar in lab out 
                  //// Buggy Code: db.AddInParameter(command, "@Onset", DbType.DateTime, LabObject.Onset);
                    db.AddInParameter(command, "@Onset", DbType.String, LabObject.Onset);

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
                        QuivusLogger.Info("Modified At in PopulateLabParameters" + LabObject.ModifiedAt);
                        db.AddInParameter(command, "@PatientLabID", DbType.Int32, LabObject.PatientLabID);
                        db.AddInParameter(command, "@IsDeleted", DbType.Boolean, LabObject.IsDeleted);

                        SetValueIfNotNull("@ModifiedBy", LabObject.ModifiedBy, db, command);
                        QuivusLogger.Info("Setting Date for Modified At in PatientManageLabs :: ModifiedAt " + LabObject.ModifiedAt);
                        SetDateIfNotNull("@ModifiedAt", LabObject.ModifiedAt, db, command);
                        //db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, LabObject.ModifiedAt);
                    }
                //}
            }
            catch (Exception exp)
            {
                // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
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
                    QuivusLogger.Info("Setting date for ModifiedAt in populater with 7 arg and date is " + ActionDate);

                    // db.AddInParameter(command, "@ModifiedBy", DbType.Int32, LabObject.ModifiedBy);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                    //db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, LabObject.ModifiedAt);
                }
            }
            catch(Exception exp)
            {
               // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }


        internal static void PopulateVitalParameters(VitalDTO VitalObject, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@VitalNote", DbType.String, VitalObject.VitalNote);
                db.AddInParameter(command, "@NoteSubject", DbType.String, VitalObject.NoteSubject);
                db.AddInParameter(command, "@VisitID", DbType.Int32, VitalObject.VisitID);
                db.AddInParameter(command, "@VitalDescription", DbType.String, VitalObject.VitalDescription);
                db.AddInParameter(command, "@Height", DbType.String, VitalObject.Height);
                db.AddInParameter(command, "@Weight", DbType.String, VitalObject.Weight);
                db.AddInParameter(command, "@Temperature", DbType.String, VitalObject.Temperature);
                db.AddInParameter(command, "@Pulse", DbType.String, VitalObject.Pulse);
                db.AddInParameter(command, "@Respiration", DbType.String, VitalObject.Respiration);

                db.AddInParameter(command, "@BPSys", DbType.String, VitalObject.BPSys);
                db.AddInParameter(command, "@BPDys", DbType.String, VitalObject.BPDys);
                db.AddInParameter(command, "@BMI", DbType.String, VitalObject.BMI);
                db.AddInParameter(command, "@IsMetricDisplaySelected", DbType.Boolean, VitalObject.IsMetricDisplaySelected);

                db.AddInParameter(command, "@Oxygen", DbType.String, VitalObject.Oxygen);

                SetDictationIfNotNull("@DictationID", VitalObject.DictationID, db, command);

                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, VitalObject.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, VitalObject.CreatedBy);
                    db.AddInParameter(command, "@PatientVitalIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientVitalID", DbType.Int32, VitalObject.PatientVitalID);
                    SetValueIfNotNull("@ModifiedBy", VitalObject.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", VitalObject.ModifiedAt, db, command);
                }
            }
            catch (Exception exp)
            {
                // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                  ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
            }
        }

        internal static void PopulateVitalParameters(VitalDTO VitalObject, bool IsUpdate, int managedUsedId, int VisitId, DateTime ActionDate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@VitalNote", DbType.String, VitalObject.VitalNote);
                db.AddInParameter(command, "@NoteSubject", DbType.String, VitalObject.NoteSubject);
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@VitalDescription", DbType.String, VitalObject.VitalDescription);
                db.AddInParameter(command, "@Height", DbType.String, VitalObject.Height);
                db.AddInParameter(command, "@Weight", DbType.String, VitalObject.Weight);
                db.AddInParameter(command, "@Temperature", DbType.String, VitalObject.Temperature);
                db.AddInParameter(command, "@Pulse", DbType.String, VitalObject.Pulse);
                db.AddInParameter(command, "@Respiration", DbType.String, VitalObject.Respiration);

                db.AddInParameter(command, "@BPSys", DbType.String, VitalObject.BPSys);
                db.AddInParameter(command, "@BPDys", DbType.String, VitalObject.BPDys);
                db.AddInParameter(command, "@BMI", DbType.String, VitalObject.BMI);
                db.AddInParameter(command, "@IsMetricDisplaySelected", DbType.Boolean, VitalObject.IsMetricDisplaySelected);

                db.AddInParameter(command, "@Oxygen", DbType.String, VitalObject.Oxygen);

                SetDictationIfNotNull("@DictationID", VitalObject.DictationID, db, command);

                if (!IsUpdate)
                {

                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedUsedId);
                    db.AddInParameter(command, "@PatientVitalIDOUT", DbType.Int32, 1);

                }
                else
                {

                    db.AddInParameter(command, "@PatientVitalID", DbType.Int32, VitalObject.PatientVitalID);
                    SetValueIfNotNull("@ModifiedBy", managedUsedId, db, command);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                }
            }
            catch(Exception exp)
            {
               // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.PARAMETER_POPULATOR_PREFIX+ ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }

        internal static void PopulateMedicalHistoryParameters(MedicalHistoryDTO PateintMedicalHistoryObject, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@MedicalHistoryTypeID", DbType.Int32, PateintMedicalHistoryObject.MedicalHistoryTypeID);
                db.AddInParameter(command, "@MedicalHistoryNote", DbType.String, PateintMedicalHistoryObject.MedicalHistoryNote);
                db.AddInParameter(command, "@NoteSubject", DbType.String, PateintMedicalHistoryObject.NoteSubject);
                db.AddInParameter(command, "@PatientMedicalHistoryDescription", DbType.String, PateintMedicalHistoryObject.PatientMedicalHistoryDescription);

                db.AddInParameter(command, "@VisitID", DbType.Int32, PateintMedicalHistoryObject.VisitID);
                SetDictationIfNotNull("@DictationID", PateintMedicalHistoryObject.DictationID, db, command);
                if (string.IsNullOrEmpty(PateintMedicalHistoryObject.IsNone))
                {
                    SetNullValue("@IsNone", db, command, DbType.Boolean);
                }
                else if (PateintMedicalHistoryObject.IsNone.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsNone", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsNone", DbType.Boolean, false);
                }

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, PateintMedicalHistoryObject.CreatedBy);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, PateintMedicalHistoryObject.CreatedAt);
                    db.AddOutParameter(command, "@MedicalHistoryIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientMedicalHistoryID", DbType.Int32, PateintMedicalHistoryObject.PatientMedicalHistoryID);
                    SetValueIfNotNull("@ModifiedBy", PateintMedicalHistoryObject.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", PateintMedicalHistoryObject.ModifiedAt, db, command);
                }
            }
            catch (Exception exp)
            {
              // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG,exp.Message,
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                    ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.PARAMETER_POPULATOR_PREFIX));
            }
        }

        internal static void PopulateMedicalHistoryParameters(MedicalHistoryDTO PateintMedicalHistoryObject, bool IsUpdate, int managedUsedId,
                                                                    int VisitId, DateTime ActionDate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@MedicalHistoryTypeID", DbType.Int32, PateintMedicalHistoryObject.MedicalHistoryTypeID);
                db.AddInParameter(command, "@MedicalHistoryNote", DbType.String, PateintMedicalHistoryObject.MedicalHistoryNote);
                db.AddInParameter(command, "@PatientMedicalHistoryDescription", DbType.String, PateintMedicalHistoryObject.PatientMedicalHistoryDescription);
                db.AddInParameter(command, "@NoteSubject", DbType.String, PateintMedicalHistoryObject.NoteSubject);
                //db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@VisitID", DbType.Int32, PateintMedicalHistoryObject.VisitID);
                SetDictationIfNotNull("@DictationID", PateintMedicalHistoryObject.DictationID, db, command);
                if (string.IsNullOrEmpty(PateintMedicalHistoryObject.IsNone))
                {
                    SetNullValue("@IsNone", db, command, DbType.Boolean);
                }
                else if (PateintMedicalHistoryObject.IsNone.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsNone", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsNone", DbType.Boolean, false);
                }

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedUsedId);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddOutParameter(command, "@MedicalHistoryIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientMedicalHistoryID", DbType.Int32, PateintMedicalHistoryObject.PatientMedicalHistoryID);
                    SetValueIfNotNull("@ModifiedBy", managedUsedId, db, command);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                }
            }
            catch (Exception exp)
            {
                // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }

        internal static void PopulateFamilyHistoryParameters(FamilyHistoryDTO PateintFamilyHistoryObject, bool IsUpdate, int UserTypeId, int ClinicBranchId, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@FamilyHistoryTypeID", DbType.Int32, PateintFamilyHistoryObject.FamilyHistoryTypeID);
                db.AddInParameter(command, "@FamilyHistoryNote", DbType.String, PateintFamilyHistoryObject.FamilyHistoryNote);
                db.AddInParameter(command, "@IsPresent", DbType.Boolean, PateintFamilyHistoryObject.IsPresent);
                db.AddInParameter(command, "@NoteSubject", DbType.String, PateintFamilyHistoryObject.NoteSubject);
                SetValueIfNotNull("@ReletionshipTypeID", PateintFamilyHistoryObject.ReletionshipTypeID, db, command, DbType.Int32);

                db.AddInParameter(command, "@IsSystemSpecifiedeDisease", DbType.Boolean, PateintFamilyHistoryObject.IsSystemSpecified);

                db.AddInParameter(command, "@UserTypeID", DbType.Int32, UserTypeId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                SetValueIfNotNull("@DiseaseID", PateintFamilyHistoryObject.DiseaseID, db, command, DbType.Int32);
                /* Flex Does Not Support Nullable Data Types SO We read string from flex & Make decision either it is null true or false :)*/
                # region Special check

                if (string.IsNullOrEmpty(PateintFamilyHistoryObject.IsYesSelected))
                {
                    SetNullValue("@IsYesSelected", db, command, DbType.Boolean);
                }
                else if (PateintFamilyHistoryObject.IsYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, false);
                }

                #endregion special check

                db.AddInParameter(command, "@VisitID", DbType.Int32, PateintFamilyHistoryObject.VisitID);
                db.AddInParameter(command, "@DictationID", DbType.Guid, PateintFamilyHistoryObject.DictationID);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, PateintFamilyHistoryObject.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, PateintFamilyHistoryObject.CreatedBy);
                    db.AddInParameter(command, "@PatientFamilyHistoryIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientFamilyHistoryID", DbType.Int32, PateintFamilyHistoryObject.PatientFamilyHistoryID);
                    SetValueIfNotNull("@ModifiedBy", PateintFamilyHistoryObject.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", PateintFamilyHistoryObject.ModifiedAt, db, command);
                }
            }
            catch (Exception exp)
            {
                // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                   ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                   ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX));
            }
        }

        internal static void PopulateFamilyHistoryParameters(FamilyHistoryDTO PateintFamilyHistoryObject, bool IsUpdate, Database db, DbCommand command, int managedUsedId, int VisitId, DateTime ModificationDate, int UserTypeId, int ClinicBranchId)
        {
            try
            {


                db.AddInParameter(command, "@FamilyHistoryTypeID", DbType.Int32, PateintFamilyHistoryObject.FamilyHistoryTypeID);
                db.AddInParameter(command, "@FamilyHistoryNote", DbType.String, PateintFamilyHistoryObject.FamilyHistoryNote);
                db.AddInParameter(command, "@IsPresent", DbType.Boolean, PateintFamilyHistoryObject.IsPresent);
                db.AddInParameter(command, "@NoteSubject", DbType.String, PateintFamilyHistoryObject.NoteSubject);
                SetValueIfNotNull("@ReletionshipTypeID", PateintFamilyHistoryObject.ReletionshipTypeID, db, command, DbType.Int32);

                SetValueIfNotNull("@DiseaseID", PateintFamilyHistoryObject.DiseaseID, db, command, DbType.Int32);
              //  db.AddInParameter(command, "@DiseaseCode", DbType.String, PateintFamilyHistoryObject.DiseaseCode);
               // db.AddInParameter(command, "@DiseaseName", DbType.String, PateintFamilyHistoryObject.DiseaseName);

                db.AddInParameter(command, "@IsSystemSpecifiedeDisease", DbType.Boolean, PateintFamilyHistoryObject.IsSystemSpecified);


                db.AddInParameter(command, "@UserTypeID", DbType.Int32, UserTypeId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                //PatientFamilyHistory_Insert
                /* Flex Does Not Support Nullable Data Types SO We read string from flex & Make decision either it is null true or false :)*/
                # region Special check

                if (string.IsNullOrEmpty(PateintFamilyHistoryObject.IsYesSelected))
                {
                    SetNullValue("@IsYesSelected", db, command, DbType.Boolean);
                }
                else if (PateintFamilyHistoryObject.IsYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, false);
                }

                #endregion special check

                //db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@VisitID", DbType.Int32, PateintFamilyHistoryObject.VisitID);
                
                db.AddInParameter(command, "@DictationID", DbType.Guid, PateintFamilyHistoryObject.DictationID);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ModificationDate);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedUsedId);
                    db.AddInParameter(command, "@PatientFamilyHistoryIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientFamilyHistoryID", DbType.Int32, PateintFamilyHistoryObject.PatientFamilyHistoryID);
                    SetValueIfNotNull("@ModifiedBy", managedUsedId, db, command);
                    SetDateIfNotNull("@ModifiedAt", ModificationDate, db, command);
                }
            }
            catch(Exception exp)
            {
               // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                   ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }



        /// <summary>
        /// Author Muhammad Ismail
        /// Date 21 March 2011
        /// 
        /// </summary>
        /// <param name="SocialHistoryAddiction"></param>
        /// <param name="IsUpdate"></param>
        /// <param name="db"></param>
        /// <param name="command"></param>
        internal static void PopulateSocialAddictionParameters(SocialHistoryAddictionDTO SocialHistoryAddiction, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {       // fix for presend and educated history

                if (string.IsNullOrEmpty(SocialHistoryAddiction.IsPresentSelected))
                {
                    SetNullValue("@IsPresentSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryAddiction.IsPresentSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {

                    db.AddInParameter(command, "@IsPresentSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsPresentSelected", DbType.Boolean, false);
                }
             //   db.AddInParameter(command, "@IsPresentSelected", DbType.Boolean, SocialHistoryAddiction.IsPresentSelected);

                if (string.IsNullOrEmpty(SocialHistoryAddiction.IsEducated))
                {
                    SetNullValue("@IsEducated", db, command, DbType.Boolean);
                }
                else if (SocialHistoryAddiction.IsEducated.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {

                    db.AddInParameter(command, "@IsEducated", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsEducated", DbType.Boolean, false);
                }
                db.AddInParameter(command, "@IsUnknown", DbType.Boolean, SocialHistoryAddiction.IsUnknown);
                db.AddInParameter(command, "@NoteSubject", DbType.String, SocialHistoryAddiction.NoteSubject);
                if (string.IsNullOrEmpty(SocialHistoryAddiction.IsYesSelected))
                {
                    SetNullValue("@IsYesSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryAddiction.IsYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryAddiction.IsYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, false);
                }

                if (string.IsNullOrEmpty(SocialHistoryAddiction.IsNoSelected))
                {
                    SetNullValue("@IsNoSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryAddiction.IsNoSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryAddiction.IsNoSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsNoSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsNoSelected", DbType.Boolean, false);
                }

                //  db.AddInParameter(command, "@OnSet", DbType.DateTime, SocialHistoryAddiction.OnSet);
                //  db.AddInParameter(command, "@EndDate", DbType.DateTime, SocialHistoryAddiction.EndDate);
            //    SetDateIfNotNull("@OnSet", SocialHistoryAddiction.OnSet, db, command);
                db.AddInParameter(command, "@OnSet", DbType.String, SocialHistoryAddiction.OnSet);
                db.AddInParameter(command, "@EndDate", DbType.String, SocialHistoryAddiction.EndDate);
               // SetDateIfNotNull("@EndDate", SocialHistoryAddiction.EndDate, db, command);

                //     SetValueIfNotNull("@PatientSocialHistoryID",SocialHistoryAddiction.PatientSocialHistoryID,db,command);
                //     SetValueIfNotNull("@PatientSocialHistoryID", SocialHistoryAddiction.SocialHistoryAddictionTypeID, db, command);

                db.AddInParameter(command, "@PatientSocialHistoryID", DbType.Int32, SocialHistoryAddiction.PatientSocialHistoryID);
                db.AddInParameter(command, "@SocialHistoryAddictionTypeID", DbType.Int32, SocialHistoryAddiction.SocialHistoryAddictionTypeID);

                db.AddInParameter(command, "@AddictionNotes", DbType.String, SocialHistoryAddiction.AddictionNotes);

                SetValueIfNotNull("@MediumTypeID", SocialHistoryAddiction.MediumTypeID, db, command);
                SetValueIfNotNull("@UsageAmountID", SocialHistoryAddiction.UsageAmountID, db, command);

                //    db.AddInParameter(command, "@MediumTypeID", DbType.Int32, SocialHistoryAddiction.MediumTypeID);
                //    db.AddInParameter(command, "@UsageAmountID", DbType.Int32, SocialHistoryAddiction.UsageAmountID);

                SetDictationIfNotNull("@DictationID", SocialHistoryAddiction.DictationId, db, command);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@VisitID", DbType.Int32, SocialHistoryAddiction.VisitId);

                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, SocialHistoryAddiction.CreatedBy);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, SocialHistoryAddiction.CreatedAt);
                    db.AddOutParameter(command, "@SocialHistoryAddictionIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@SocialHistoryAddictionID", DbType.Int32, SocialHistoryAddiction.SocialHistoryAddictionID);
                    SetValueIfNotNull("@ModifiedBy", SocialHistoryAddiction.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", SocialHistoryAddiction.ModifiedAt, db, command);
                }
            }
            catch (Exception exp)
            {
             //   throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.SOCIAL_HISTORY_ADDICTION_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 21 March 2011
        /// 
        /// </summary>
        /// <param name="SocialHistoryAddiction"></param>
        /// <param name="IsUpdate"></param>
        /// <param name="db"></param>
        /// <param name="command"></param>
        internal static void PopulateSocialAddictionParameters(SocialHistoryAddictionDTO SocialHistoryAddiction, bool IsUpdate, int managedUsedId, int VisitId, DateTime ActionDate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@IsPresentSelected", DbType.Boolean, SocialHistoryAddiction.IsPresentSelected);
                db.AddInParameter(command, "@IsEducated", DbType.Boolean, SocialHistoryAddiction.IsEducated);
                db.AddInParameter(command, "@IsUnknown", DbType.Boolean, SocialHistoryAddiction.IsUnknown);
                db.AddInParameter(command, "@NoteSubject", DbType.String, SocialHistoryAddiction.NoteSubject);
                if (string.IsNullOrEmpty(SocialHistoryAddiction.IsYesSelected))
                {
                    SetNullValue("@IsYesSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryAddiction.IsYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryAddiction.IsYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsYesSelected", DbType.Boolean, false);
                }

                if (string.IsNullOrEmpty(SocialHistoryAddiction.IsNoSelected))
                {
                    SetNullValue("@IsNoSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryAddiction.IsNoSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryAddiction.IsNoSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsNoSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsNoSelected", DbType.Boolean, false);
                }

                //db.AddInParameter(command, "@OnSet", DbType.DateTime, SocialHistoryAddiction.OnSet);
                //db.AddInParameter(command, "@EndDate", DbType.DateTime, SocialHistoryAddiction.EndDate);
                
                db.AddInParameter(command, "@OnSet", DbType.String, SocialHistoryAddiction.OnSet);

                //  SetDateIfNotNull("@OnSet", SocialHistoryAddiction.OnSet, db, command);
                //SetDateIfNotNull("@EndDate", SocialHistoryAddiction.EndDate, db, command);
                db.AddInParameter(command, "@EndDate", DbType.String, SocialHistoryAddiction.EndDate);

                db.AddInParameter(command, "@PatientSocialHistoryID", DbType.Int32, SocialHistoryAddiction.PatientSocialHistoryID);
                db.AddInParameter(command, "@SocialHistoryAddictionTypeID", DbType.Int32, SocialHistoryAddiction.SocialHistoryAddictionTypeID);

                db.AddInParameter(command, "@AddictionNotes", DbType.String, SocialHistoryAddiction.AddictionNotes);

                db.AddInParameter(command, "@MediumTypeID", DbType.Int32, SocialHistoryAddiction.MediumTypeID);
                db.AddInParameter(command, "@UsageAmountID", DbType.Int32, SocialHistoryAddiction.UsageAmountID);

                SetDictationIfNotNull("@DictationID", SocialHistoryAddiction.DictationId, db, command);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedUsedId);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddOutParameter(command, "@SocialHistoryAddictionIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@SocialHistoryAddictionID", DbType.Int32, SocialHistoryAddiction.SocialHistoryAddictionID);
                    SetValueIfNotNull("@ModifiedBy", managedUsedId, db, command);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                }
            }
            catch
            {
                throw;
            }
        }


        internal static void PopulatePatientSocialParameters(SocialHistoryDTO SocialHistoryObject, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@VisitID", DbType.Int32, SocialHistoryObject.VisitID);
                db.AddInParameter(command, "@PatientID", DbType.Int32, SocialHistoryObject.PatientID);

                db.AddInParameter(command, "@NoteSubject", DbType.String, SocialHistoryObject.NoteSubject);
                if (string.IsNullOrEmpty(SocialHistoryObject.IsWorkStatusYesSelected))
                {
                    SetNullValue("@IsWorkStatusYesSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryObject.IsWorkStatusYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryObject.IsWorkStatusYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, false);
                }

                //  db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, SocialHistoryObject.IsWorkStatusYesSelected);

                db.AddInParameter(command, "@WorkStatusNotes", DbType.String, SocialHistoryObject.WorkStatusNotes);

                SetValueIfNotNull("@MaritalStatusID", SocialHistoryObject.MaritalStatusID, db, command);

                if (string.IsNullOrEmpty(SocialHistoryObject.IsOtherYesSelected))
                {
                    SetNullValue("@IsOtherYesSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryObject.IsOtherYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryObject.IsOtherYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsOtherYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsOtherYesSelected", DbType.Boolean, false);
                }

                db.AddInParameter(command, "@OtherDictationID", DbType.Guid, SocialHistoryObject.OtherDictationID);
                db.AddInParameter(command, "@WorkStatusDictationID", DbType.Guid, SocialHistoryObject.WorkStatusDictationID);

                db.AddInParameter(command, "@OtherNotes", DbType.String, SocialHistoryObject.OtherNotes);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, SocialHistoryObject.CreatedBy);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, SocialHistoryObject.CreatedAt);
                    db.AddOutParameter(command, "@PatientSocialHistoryIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientSocialHistoryID", DbType.Int32, SocialHistoryObject.PatientSocialHistoryID);
                    SetValueIfNotNull("@ModifiedBy", SocialHistoryObject.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", SocialHistoryObject.ModifiedAt, db, command);
                }
            }
            catch
            {
                throw;
            }

        }

        internal static void PopulatePatientSocialParameters(SocialHistoryDTO SocialHistoryObject, bool IsUpdate,
            int managedUsedId, int PatientId, int VisitId, DateTime ActionDate, Database db, DbCommand command)
        {
            try
            {
                //db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@VisitID", DbType.Int32, SocialHistoryObject.VisitID);
                
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                db.AddInParameter(command, "@NoteSubject", DbType.String, SocialHistoryObject.NoteSubject);
                if (string.IsNullOrEmpty(SocialHistoryObject.IsWorkStatusYesSelected))
                {
                    SetNullValue("@IsWorkStatusYesSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryObject.IsWorkStatusYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryObject.IsWorkStatusYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, false);
                }

                //  db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, SocialHistoryObject.IsWorkStatusYesSelected);

                db.AddInParameter(command, "@WorkStatusNotes", DbType.String, SocialHistoryObject.WorkStatusNotes);

                SetValueIfNotNull("@MaritalStatusID", SocialHistoryObject.MaritalStatusID, db, command);

                if (string.IsNullOrEmpty(SocialHistoryObject.IsOtherYesSelected))
                {
                    SetNullValue("@IsOtherYesSelected", db, command, DbType.Boolean);
                }
                else if (SocialHistoryObject.IsOtherYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE) || SocialHistoryObject.IsOtherYesSelected.ToLower().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsOtherYesSelected", DbType.Boolean, true);
                }
                else
                {
                    db.AddInParameter(command, "@IsOtherYesSelected", DbType.Boolean, false);
                }

                db.AddInParameter(command, "@OtherDictationID", DbType.Guid, SocialHistoryObject.OtherDictationID);
                db.AddInParameter(command, "@WorkStatusDictationID", DbType.Guid, SocialHistoryObject.WorkStatusDictationID);

                db.AddInParameter(command, "@OtherNotes", DbType.String, SocialHistoryObject.OtherNotes);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedUsedId);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddOutParameter(command, "@PatientSocialHistoryIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientSocialHistoryID", DbType.Int32, SocialHistoryObject.PatientSocialHistoryID);
                    SetValueIfNotNull("@ModifiedBy", managedUsedId, db, command);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                }
            }
            catch (Exception exp)
            {
              //  throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.SOCIAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }

        }

        /// <summary>
        /// Author Mansoor 
        /// Date 25 Mar 2011        
        /// </summary>
        /// <returns></returns>
        internal static void PopulatePatientFollowUpParameters(PatientFollowUpDTO patientFollowUp, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@VisitID", DbType.Int32, patientFollowUp.VisitID);
                db.AddInParameter(command, "@PatientID", DbType.Int32, patientFollowUp.PatientID);

                //  db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, SocialHistoryObject.IsWorkStatusYesSelected);

                if (patientFollowUp.FollowUpMethodType != null)
                    db.AddInParameter(command, "@FollowupMethodTypeID", DbType.Int32, patientFollowUp.FollowUpMethodType.FollowUpMethodTypeID);

                db.AddInParameter(command, "@IsByDate", DbType.Boolean, patientFollowUp.IsByDate);

                db.AddInParameter(command, "@FollowupDate", DbType.String, patientFollowUp.FollowUpDate);
             //   SetDateIfNotNull("@FollowupDate", patientFollowUp.FollowUpDate, db, command);

                db.AddInParameter(command, "@IsOnCompletion", DbType.Boolean, patientFollowUp.IsOnCompletion);
                db.AddInParameter(command, "@IsLabTestSelected", DbType.Boolean, patientFollowUp.IsLabTestSelected);
                db.AddInParameter(command, "@IsDrugselected", DbType.Boolean, patientFollowUp.IsDrugselected);

                db.AddInParameter(command, "@LabTestID", DbType.Boolean, patientFollowUp.LabTestID);
                db.AddInParameter(command, "@DrugID", DbType.Boolean, patientFollowUp.DrugID);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, patientFollowUp.CreatedBy);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, patientFollowUp.CreatedAt);
                    db.AddOutParameter(command, "@PatientFollowUpIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientFollowUpID", DbType.Int32, patientFollowUp.PatientFollowUpID);
                    SetValueIfNotNull("@ModifiedBy", patientFollowUp.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", patientFollowUp.ModifiedAt, db, command);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 25 Mar 2011        
        /// </summary>
        /// <returns></returns>
        internal static void PopulatePatientFollowUpParameters(PatientFollowUpDTO patientFollowUp, bool IsUpdate, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate, Database db, DbCommand command)
        {
            try
            {
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                //  db.AddInParameter(command, "@IsWorkStatusYesSelected", DbType.Boolean, SocialHistoryObject.IsWorkStatusYesSelected);

                if (patientFollowUp.FollowUpMethodType != null)
                    db.AddInParameter(command, "@FollowupMethodTypeID", DbType.Int32, patientFollowUp.FollowUpMethodType.FollowUpMethodTypeID);
                else
                    db.AddInParameter(command, "@FollowupMethodTypeID", DbType.Int32, DBNull.Value);

                db.AddInParameter(command, "@IsByDate", DbType.Boolean, patientFollowUp.IsByDate);

                db.AddInParameter(command, "@FollowupDate", DbType.String, patientFollowUp.FollowUpDate);
                //SetDateIfNotNull("@FollowupDate", patientFollowUp.FollowUpDate, db, command);

                db.AddInParameter(command, "@IsOnCompletion", DbType.Boolean, patientFollowUp.IsOnCompletion);
                db.AddInParameter(command, "@IsLabTestSelected", DbType.Boolean, patientFollowUp.IsLabTestSelected);
                db.AddInParameter(command, "@IsDrugselected", DbType.Boolean, patientFollowUp.IsDrugselected);

                db.AddInParameter(command, "@LabTestID", DbType.Boolean, patientFollowUp.LabTestID);
                db.AddInParameter(command, "@DrugID", DbType.Boolean, patientFollowUp.DrugID);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedByUserId);
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddOutParameter(command, "@PatientFollowUpIDOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientFollowUpID", DbType.Int32, patientFollowUp.PatientFollowUpID);
                    SetValueIfNotNull("@ModifiedBy", managedByUserId, db, command);
                    SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                }
            }
            catch (Exception exp)
            {
               // throw;
                throw new ParameterPopulatorException(ProjectConstant.PARAMETER_POPULATOR_ERROR_MSG, exp.Message,
                  ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                  ErrorCodes.FOLLOW_UP_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.PARAMETER_POPULATOR_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
        }



        internal static void PopulatePatientAllergyParameters(AllergyDTO AllergyObj, bool IsUpdate, Database db, DbCommand command)
        {

            try
            {
               
                db.AddInParameter(command, "@AllergyName", DbType.String, AllergyObj.AllergyName);
                SetValueIfNotNull("@AllergyTypeID", AllergyObj.AllergyTypeID, db, command, DbType.Int32);
                db.AddInParameter(command, "@AllergyNote", DbType.String, AllergyObj.Note);
                db.AddInParameter(command, "@NoteSubject", DbType.String, AllergyObj.NoteSubject);
                db.AddInParameter(command, "@VisitID", DbType.Int32, AllergyObj.VisitId);
                db.AddInParameter(command, "@Reaction", DbType.String, AllergyObj.Reaction);
                db.AddInParameter(command, "@Onset", DbType.String, AllergyObj.OnSet);
                //SetDateIfNotNull("@Onset", AllergyObj.OnSet, db, command);

                db.AddInParameter(command, "@DictationID", DbType.Guid, AllergyObj.DictationID);
               SetValueIfNotNull("@RCOPIAID", AllergyObj.RCOPIAID, db, command, DbType.Int32);
                db.AddInParameter(command, "@Drug", DbType.String, AllergyObj.Drug);
               
                db.AddInParameter(command, "@Status", DbType.String, DBNull.Value);

                /* Added On Main As SP required to Have it .... filter SP it should not take in Update Case :)*/
                db.AddInParameter(command, "@ModifiedBy", DbType.Int32, DBNull.Value);
                db.AddInParameter(command, "@ModifiedAt", DbType.DateTime, DBNull.Value);


                db.AddOutParameter(command, "@PatientAllergyOUT", DbType.Int32, 1);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, AllergyObj.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, AllergyObj.CreatedBy);
                 //   db.AddInParameter(command, "@PatientAllergyOUT", DbType.Int32, 1);
                }
                else
                {
                    db.AddInParameter(command, "@PatientFamilyHistoryID", DbType.Int32, AllergyObj.PatientAllergyId);
                    // SetValueIfNotNull("@ModifiedBy", AllergyObj.ModifiedBy, db, command);
                    //  SetDateIfNotNull("@ModifiedAt", AllergyObj.ModifiedAt, db, command);
                }
            }
            catch
            {
                throw;
            }
        }

        internal static void PopulatePatientChronicMedicationParameters(MedicationPrescribedDTO ChronicMedicationObj, bool IsUpdate, Database db, DbCommand command)
        {
            try
            {


                db.AddInParameter(command, "@MedicationPrescribedNote", DbType.String, ChronicMedicationObj.MedicationPrescribedNote);
                SetValueIfNotNull("@MedicationTypeID", ChronicMedicationObj.MedicationTypeID, db, command, DbType.Int32);
                //    db.AddInParameter(command, "@AllergyNote", DbType.String, ChronicMedicationObj.Note);
                db.AddInParameter(command, "@NoteSubject", DbType.String, ChronicMedicationObj.NoteSubject);
                db.AddInParameter(command, "@MedicationStrength", DbType.String, ChronicMedicationObj.MedicationStrength);
                db.AddInParameter(command, "@Status", DbType.String, ChronicMedicationObj.Status);

                db.AddInParameter(command, "@SIG", DbType.String, ChronicMedicationObj.SIG);

                SetValueIfNotNull("@OrderDays", ChronicMedicationObj.OrderDays, db, command, DbType.Int32);
                db.AddInParameter(command, "@Quantity", DbType.String, ChronicMedicationObj.Quantity);

                db.AddInParameter(command, "@RecentRefills", DbType.String, ChronicMedicationObj.RecentRefills);
                db.AddInParameter(command, "@NoOfRefills", DbType.String, ChronicMedicationObj.NoOfRefills);


                db.AddInParameter(command, "@ApplicableStatus", DbType.String, ChronicMedicationObj.ApplicableStatus);

                db.AddInParameter(command, "@Days", DbType.Double, ChronicMedicationObj.Days);


                if (string.IsNullOrEmpty(ChronicMedicationObj.IsChronic))
                {
                    db.AddInParameter(command, "@IsChronic", DbType.Boolean, DBNull.Value);
                }
                else if (ChronicMedicationObj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_TRUE) || ChronicMedicationObj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsChronic", DbType.Boolean, true);
                }
                else if (ChronicMedicationObj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_False))
                {
                    db.AddInParameter(command, "@IsChronic", DbType.Boolean, false);
                }
                else if (ChronicMedicationObj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_NULL))
                {
                    db.AddInParameter(command, "@IsChronic", DbType.Boolean, DBNull.Value);
                }



                db.AddInParameter(command, "@DosageForm", DbType.String, ChronicMedicationObj.DosageForm);
                db.AddInParameter(command, "@Form", DbType.String, ChronicMedicationObj.Form);
                db.AddInParameter(command, "@Dose", DbType.String, ChronicMedicationObj.Dose);
                SetValueIfNotNull("@FrequencyTypeID", ChronicMedicationObj.FrequencyTypeID, db, command, DbType.Int32);

                db.AddInParameter(command, "@Route", DbType.String, ChronicMedicationObj.Route);
                db.AddInParameter(command, "@RCOPIAID", DbType.String, ChronicMedicationObj.RCOPIAID);

                db.AddInParameter(command, "@PRN", DbType.Boolean, ChronicMedicationObj.PRN);


                SetDateIfNotNull("@Onset", ChronicMedicationObj.Onset, db, command);


                db.AddInParameter(command, "@Route", DbType.String, ChronicMedicationObj.Route);
                db.AddInParameter(command, "@IsDeleted", DbType.Boolean, ChronicMedicationObj.IsDeleted);

                db.AddInParameter(command, "@VisitID", DbType.Int32, ChronicMedicationObj.VisitID);
                db.AddInParameter(command, "@DictationID", DbType.Guid, ChronicMedicationObj.DictationID);


                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ChronicMedicationObj.CreatedAt);
                    db.AddInParameter(command, "@CreatedBy", DbType.Int32, ChronicMedicationObj.CreatedBy);
                    db.AddInParameter(command, "@PatientMedicationIDOUT", DbType.Int32, 1);
                }
                else
                {
                    // db.AddInParameter(command, "@PatientFamilyHistoryID", DbType.Int32, ChronicMedicationObj.PatientAllergyId);
                    SetValueIfNotNull("@ModifiedBy", ChronicMedicationObj.ModifiedBy, db, command);
                    SetDateIfNotNull("@ModifiedAt", ChronicMedicationObj.ModifiedAt, db, command);
                }
            }
            catch
            {
                throw;
            }
        }

        internal static void PopulateMedicationParameters(MedicationDTO InputObj, bool IsUpdate, Database db, DbCommand command)
        {

            try
            {
               
                db.AddInParameter(command, "@RCOPIAID", DbType.String, InputObj.RCOPIAID);
                //db.AddInParameter(command, "@NoteSubject", DbType.String, InputObj.NoteSubject);
                db.AddInParameter(command, "@NDCID", DbType.String, InputObj.NDCID);
                db.AddInParameter(command, "@FirstDataBankMedID", DbType.String, InputObj.FirstDataBankMedID);
                db.AddInParameter(command, "@BrandName", DbType.String, InputObj.BrandName);
                db.AddInParameter(command, "@GenericName", DbType.String, InputObj.GenericName);
                db.AddInParameter(command, "@Form", DbType.String, InputObj.Form);
                db.AddInParameter(command, "@Strength", DbType.String, InputObj.Strength);
                db.AddInParameter(command, "@Action", DbType.String, InputObj.Action);
                db.AddInParameter(command, "@Dose", DbType.String, InputObj.Dose);
                db.AddInParameter(command, "@DoseUnit", DbType.String, InputObj.DoseUnit);
                db.AddInParameter(command, "@Route", DbType.String, InputObj.Route);
                db.AddInParameter(command, "@DoseTiming", DbType.String, InputObj.DoseTiming);
                db.AddInParameter(command, "@DoseOther", DbType.String, InputObj.DoseOther);
                db.AddInParameter(command, "@Duration", DbType.String, InputObj.Duration);
                db.AddInParameter(command, "@Quantity", DbType.String, InputObj.Quantity);
                db.AddInParameter(command, "@QuantityUnit", DbType.String, InputObj.QuantityUnit);
                db.AddInParameter(command, "@Refills", DbType.String, InputObj.Refills);

                db.AddInParameter(command, "@PatientID", DbType.Int32, InputObj.PatientId);

                db.AddInParameter(command, "@IsSubstitutionPermitted", DbType.Boolean, InputObj.IsSubstitutionPermitted);

                db.AddInParameter(command, "@Status", DbType.String, InputObj.Status);
                db.AddInParameter(command, "@OtherNotes", DbType.String, InputObj.OtherNotes);
                db.AddInParameter(command, "@PatientNotes", DbType.String, InputObj.PatientNotes);
                db.AddInParameter(command, "@Comments", DbType.String, InputObj.Comments);

                SetDateIfNotNull("@ModifiedAt", InputObj.ModifiedAt, db, command);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, InputObj.CreatedAt);
                   
                    
                    db.AddInParameter(command, "@PatientMedicationIDOUT", DbType.Int32, 1);
                }
                else
                {
                    //SetDateIfNotNull("@ModifiedAt", InputObj.ModifiedAt, db, command);
                }
            }
            catch
            {
                throw;
            }
        }

        internal static void PopulateMedicationParameters(MedicationDTO InputObj, bool IsUpdate, int managedByUserId, int PatientId, DateTime ActionDate, Database db, DbCommand command)
        {

            try
            {
                db.AddInParameter(command, "@RCOPIAID", DbType.String, InputObj.RCOPIAID);

                db.AddInParameter(command, "@NDCID", DbType.String, InputObj.NDCID);
                db.AddInParameter(command, "@FirstDataBankMedID", DbType.String, InputObj.FirstDataBankMedID);
                db.AddInParameter(command, "@BrandName", DbType.String, InputObj.BrandName);
                db.AddInParameter(command, "@GenericName", DbType.String, InputObj.GenericName);
                db.AddInParameter(command, "@Form", DbType.String, InputObj.Form);
                db.AddInParameter(command, "@Strength", DbType.String, InputObj.Strength);
                db.AddInParameter(command, "@Action", DbType.String, InputObj.Action);
                db.AddInParameter(command, "@Dose", DbType.String, InputObj.Dose);
                db.AddInParameter(command, "@DoseUnit", DbType.String, InputObj.DoseUnit);
                db.AddInParameter(command, "@Route", DbType.String, InputObj.Route);
                db.AddInParameter(command, "@DoseTiming", DbType.String, InputObj.DoseTiming);
                db.AddInParameter(command, "@DoseOther", DbType.String, InputObj.DoseOther);
                db.AddInParameter(command, "@Duration", DbType.String, InputObj.Duration);
                db.AddInParameter(command, "@Quantity", DbType.String, InputObj.Quantity);
                db.AddInParameter(command, "@QuantityUnit", DbType.String, InputObj.QuantityUnit);
                db.AddInParameter(command, "@Refills", DbType.String, InputObj.Refills);

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                db.AddInParameter(command, "@IsSubstitutionPermitted", DbType.Boolean, InputObj.IsSubstitutionPermitted);

                db.AddInParameter(command, "@Status", DbType.String, InputObj.Status);
                db.AddInParameter(command, "@OtherNotes", DbType.String, InputObj.OtherNotes);
                db.AddInParameter(command, "@PatientNotes", DbType.String, InputObj.PatientNotes);
                db.AddInParameter(command, "@Comments", DbType.String, InputObj.Comments);

                SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);

                if (!IsUpdate)
                {
                    db.AddInParameter(command, "@CreatedAt", DbType.DateTime, ActionDate);
                    db.AddInParameter(command, "@PatientMedicationIDOUT", DbType.Int32, 1);
                }
                else
                {
                  //  SetDateIfNotNull("@ModifiedAt", ActionDate, db, command);
                }
            }
            catch
            {
                throw;
            }
        }



    } // End Class
}
