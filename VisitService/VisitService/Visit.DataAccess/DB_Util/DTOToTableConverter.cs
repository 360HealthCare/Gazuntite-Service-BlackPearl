using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.TypeDTOs;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Dictation;
using QuivusUtility.ProjectConstants;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Medication;

namespace Visit.DataAccess.DB_Util
{
    /// <summary>
    /// Author Mansoor
    /// Date 22 Feb 2011
    /// Convert the DTO to the required Table
    /// </summary>

    public class DTOToTableConverter
    {
        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// convert the module notes to TVP
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        public static DataTable ModuleNoteDTOListConverter(List<ModuleNoteDTO> moduleNotes)
        {
            DataTable dtModuleNote = null;

            try
            {
                dtModuleNote = new DataTable();
                dtModuleNote.Columns.Add("ModuleNoteID", typeof(System.Int32));
                dtModuleNote.Columns.Add("ModuleTypeID", typeof(System.Int32));
                dtModuleNote.Columns.Add("VisitID", typeof(System.Int32));
                dtModuleNote.Columns.Add("Note", typeof(System.String));
                dtModuleNote.Columns.Add("NoteSubject", typeof(System.String));
                dtModuleNote.Columns.Add("CreatedByUpdateByUserID", typeof(System.Int32));
                dtModuleNote.Columns.Add("IsDeleted", typeof(System.Boolean));



                dtModuleNote.AcceptChanges();

                ModuleNoteDTO objModuleNoteDTO;

                for (int indexModuleNote = 0; indexModuleNote < moduleNotes.Count; indexModuleNote++)
                {
                    objModuleNoteDTO = moduleNotes[indexModuleNote];
                    DataRow dRow = dtModuleNote.NewRow();

                    dRow["ModuleNoteID"] = objModuleNoteDTO.ModuleNoteId;
                    dRow["ModuleTypeID"] = objModuleNoteDTO.ModuleTypeId;
                    dRow["VisitID"] = objModuleNoteDTO.VisitID;
                    dRow["Note"] = objModuleNoteDTO.Note;
                    dRow["CreatedByUpdateByUserID"] = objModuleNoteDTO.ManagedByUserId;
                    dRow["IsDeleted"] = objModuleNoteDTO.IsDeleted;

                    dRow["NoteSubject"] = objModuleNoteDTO.NoteSubject;

                    dtModuleNote.Rows.Add(dRow);
                }
                dtModuleNote.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ModuleNoteDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtModuleNote;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// convert the module notes to TVP
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>
        public static DataTable ModuleNoteDTOListConverter(List<ModuleNoteDTO> moduleNotes, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            DataTable dtModuleNote = null;

            try
            {
                dtModuleNote = new DataTable();
                dtModuleNote.Columns.Add("ModuleNoteID", typeof(System.Int32));
                dtModuleNote.Columns.Add("ModuleTypeID", typeof(System.Int32));
                dtModuleNote.Columns.Add("VisitID", typeof(System.Int32));
                dtModuleNote.Columns.Add("Note", typeof(System.String));
                dtModuleNote.Columns.Add("NoteSubject", typeof(System.String));
                dtModuleNote.Columns.Add("CreatedByUpdateByUserID", typeof(System.Int32));
                dtModuleNote.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtModuleNote.AcceptChanges();

                ModuleNoteDTO objModuleNoteDTO;

                for (int indexModuleNote = 0; indexModuleNote < moduleNotes.Count; indexModuleNote++)
                {
                    objModuleNoteDTO = moduleNotes[indexModuleNote];

                    if (!string.IsNullOrEmpty(objModuleNoteDTO.Note))
                    {
                        // If Note is Empty then Skip this record ...validation is removed on this field
                        // Requested By Waqas Baber On Tuesday 24 May 2011 8:24 Pm Before Giving  Release
                        // Done By Ismail

                        DataRow dRow = dtModuleNote.NewRow();

                        dRow["ModuleNoteID"] = objModuleNoteDTO.ModuleNoteId;
                        dRow["ModuleTypeID"] = objModuleNoteDTO.ModuleTypeId;
                        dRow["VisitID"] = VisitId;
                        dRow["Note"] = objModuleNoteDTO.Note;
                        dRow["CreatedByUpdateByUserID"] = managedByUserId;
                        dRow["IsDeleted"] = objModuleNoteDTO.IsDeleted;

                        dRow["NoteSubject"] = objModuleNoteDTO.NoteSubject;

                        dtModuleNote.Rows.Add(dRow);
                    }
                }
                dtModuleNote.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ModuleNoteDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtModuleNote;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// convert the Imunization notes to TVP
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <returns></returns>
        public static DataTable ImmunizationDTOListConverter(List<ImmunizationDTO> immunizationList)
        {
            DataTable dtImmunization = null;

            try
            {
                dtImmunization = new DataTable();

                dtImmunization.Columns.Add("PatientImmunizationID", typeof(System.Int32));
                dtImmunization.Columns.Add("ImmunizationTypeID", typeof(System.Int32));
                dtImmunization.Columns.Add("ImmunizationNote", typeof(System.String));
                dtImmunization.Columns.Add("NoteSubject", typeof(System.String));
                dtImmunization.Columns.Add("VisitID", typeof(System.Int32));
                dtImmunization.Columns.Add("Dose", typeof(System.String));
                dtImmunization.Columns.Add("LotNumber", typeof(System.String));
                dtImmunization.Columns.Add("DateAdmin", typeof(System.String));
                dtImmunization.Columns.Add("NextDoseDue", typeof(System.String));
                dtImmunization.Columns.Add("IsRefusal", typeof(System.Boolean));
                dtImmunization.Columns.Add("RefusalReason", typeof(System.String));
                dtImmunization.Columns.Add("Site", typeof(System.String));
                dtImmunization.Columns.Add("Series", typeof(System.String));
                dtImmunization.Columns.Add("DictationID", typeof(System.Guid));
                dtImmunization.Columns.Add("Manufacturer", typeof(System.String));
                dtImmunization.Columns.Add("Route", typeof(System.String));
                dtImmunization.Columns.Add("ExpiryDate", typeof(System.String));
                dtImmunization.Columns.Add("CreatedByUpdateByUserID", typeof(System.Int32));
                dtImmunization.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtImmunization.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtImmunization.Columns.Add("IsUpdated", typeof(System.Boolean));
                dtImmunization.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtImmunization.AcceptChanges();

                ImmunizationDTO objImmunizationDTO;

                ////bool SkipRecord = false;

                for (int indexImmunization = 0; indexImmunization < immunizationList.Count; indexImmunization++)
                {
                    //SkipRecord = false;

                    objImmunizationDTO = immunizationList[indexImmunization];

                    //if (objImmunizationDTO.PatientImmunizationId > 0 && !objImmunizationDTO.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}

                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtImmunization.NewRow();

                    //If inserted, updated, or deleted then add in TVP

                    if (objImmunizationDTO.IsDeleted || objImmunizationDTO.IsDirty || objImmunizationDTO.PatientImmunizationId == 0)
                    {
                        dRow["PatientImmunizationID"] = objImmunizationDTO.PatientImmunizationId;
                        dRow["ImmunizationTypeID"] = objImmunizationDTO.ImmunizationType.ImmunizationTypeId;
                        dRow["VisitID"] = objImmunizationDTO.VisitId;

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Note))
                        {
                            dRow["ImmunizationNote"] = objImmunizationDTO.Note;
                        }
                        else
                        {
                            dRow["ImmunizationNote"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.NoteSubject))
                        {
                            dRow["NoteSubject"] = objImmunizationDTO.NoteSubject;
                        }
                        else
                        {
                            dRow["NoteSubject"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.NoteSubject))
                        {
                            dRow["NoteSubject"] = objImmunizationDTO.NoteSubject;
                        }
                        else
                        {
                            dRow["NoteSubject"] = DBNull.Value;
                        }


                        if (!string.IsNullOrEmpty(objImmunizationDTO.Dose))
                        {
                            dRow["Dose"] = objImmunizationDTO.Dose;
                        }
                        else
                        {
                            dRow["Dose"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.LotNumber))
                        {
                            dRow["LotNumber"] = objImmunizationDTO.LotNumber;
                        }
                        else
                        {
                            dRow["LotNumber"] = DBNull.Value;
                        }

                        dRow["IsRefusal"] = objImmunizationDTO.IsRefusal;

                        if (!string.IsNullOrEmpty(objImmunizationDTO.RefusalReason))
                        {
                            dRow["RefusalReason"] = objImmunizationDTO.RefusalReason;
                        }
                        else
                        {
                            dRow["RefusalReason"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Site))
                        {
                            dRow["Site"] = objImmunizationDTO.Site;
                        }
                        else
                        {
                            dRow["Site"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Series))
                        {
                            dRow["Series"] = objImmunizationDTO.Series;
                        }
                        else
                        {
                            dRow["Series"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Manufacturer))
                        {
                            dRow["Manufacturer"] = objImmunizationDTO.Manufacturer;
                        }
                        else
                        {
                            dRow["Manufacturer"] = DBNull.Value;
                        }

                        if (!string.IsNullOrEmpty(objImmunizationDTO.Rout))
                        {
                            dRow["Route"] = objImmunizationDTO.Rout;
                        }
                        else
                        {
                            dRow["Route"] = DBNull.Value;
                        }

                        //if (objImmunizationDTO.DateAdmin != null && objImmunizationDTO.DateAdmin > ProjectConstant.DATE_MINIMUM)
                        if (objImmunizationDTO.DateAdmin != null)
                        {
                            dRow["DateAdmin"] = objImmunizationDTO.DateAdmin;
                        }
                        else
                        {
                            dRow["DateAdmin"] = DBNull.Value;
                        }

                        // if (objImmunizationDTO.NextDoseDue != null && objImmunizationDTO.NextDoseDue > ProjectConstant.DATE_MINIMUM)
                        if (objImmunizationDTO.NextDoseDue != null)
                        {
                            dRow["NextDoseDue"] = objImmunizationDTO.NextDoseDue;
                        }
                        else
                        {
                            dRow["NextDoseDue"] = DBNull.Value;
                        }

                        //if (objImmunizationDTO.ExpiryDate != null && objImmunizationDTO.ExpiryDate > ProjectConstant.DATE_MINIMUM)
                        if (objImmunizationDTO.ExpiryDate != null)
                        {
                            dRow["ExpiryDate"] = objImmunizationDTO.ExpiryDate;
                        }
                        else
                        {
                            dRow["ExpiryDate"] = DBNull.Value;
                        }

                        if (objImmunizationDTO.DictationID != System.Guid.Empty)
                        {
                            dRow["DictationID"] = objImmunizationDTO.DictationID;
                        }
                        else
                        {
                            dRow["DictationID"] = DBNull.Value;
                        }

                        dRow["CreatedByUpdateByUserID"] = objImmunizationDTO.ManagedBy;

                        if (objImmunizationDTO.IsDirty && objImmunizationDTO.UpdatedAt != null && objImmunizationDTO.UpdatedAt > ProjectConstant.DATE_MINIMUM)
                        {
                            dRow["ModifiedAt"] = objImmunizationDTO.UpdatedAt;
                            dRow["CreatedAt"] = DBNull.Value;
                        }
                        else //if (objImmunizationDTO.CreatedAt != null && objImmunizationDTO.CreatedAt > ProjectConstant.DATE_MINIMUM)
                        {
                            if (objImmunizationDTO.CreatedAt > ProjectConstant.DATE_MINIMUM)
                                dRow["CreatedAt"] = objImmunizationDTO.CreatedAt;


                            dRow["ModifiedAt"] = DBNull.Value;
                        }

                        dRow["IsUpdated"] = objImmunizationDTO.IsDirty;
                        dRow["IsDeleted"] = objImmunizationDTO.IsDeleted;

                        dtImmunization.Rows.Add(dRow);
                    }
                    //}
                }
                dtImmunization.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtImmunization;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// convert the Imunization notes to TVP
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <returns></returns>
        public static DataTable ImmunizationDTOListConverter(List<ImmunizationDTO> immunizationList, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            DataTable dtImmunization = null;

            try
            {
                dtImmunization = new DataTable();


                dtImmunization.Columns.Add("PatientImmunizationID", typeof(System.Int32));
                dtImmunization.Columns.Add("ImmunizationTypeID", typeof(System.Int32));
                dtImmunization.Columns.Add("ImmunizationNote", typeof(System.String));
                dtImmunization.Columns.Add("NoteSubject", typeof(System.String));
                dtImmunization.Columns.Add("VisitID", typeof(System.Int32));
                dtImmunization.Columns.Add("Dose", typeof(System.String));
                dtImmunization.Columns.Add("LotNumber", typeof(System.String));

                dtImmunization.Columns.Add("DateAdmin", typeof(System.String));
                dtImmunization.Columns.Add("NextDoseDue", typeof(System.String));

                dtImmunization.Columns.Add("IsRefusal", typeof(System.Boolean));
                dtImmunization.Columns.Add("RefusalReason", typeof(System.String));
                dtImmunization.Columns.Add("Site", typeof(System.String));
                dtImmunization.Columns.Add("Series", typeof(System.String));
                dtImmunization.Columns.Add("DictationID", typeof(System.Guid));
                dtImmunization.Columns.Add("Manufacturer", typeof(System.String));
                dtImmunization.Columns.Add("Route", typeof(System.String));
                dtImmunization.Columns.Add("ExpiryDate", typeof(System.String));

                dtImmunization.Columns.Add("CreatedByUpdateByUserID", typeof(System.Int32));
                dtImmunization.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtImmunization.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtImmunization.Columns.Add("IsUpdated", typeof(System.Boolean));
                dtImmunization.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtImmunization.AcceptChanges();

                ImmunizationDTO objImmunizationDTO;

                for (int indexImmunization = 0; indexImmunization < immunizationList.Count; indexImmunization++)
                {
                    objImmunizationDTO = immunizationList[indexImmunization];
                    DataRow dRow = dtImmunization.NewRow();

                    //If inserted, updated, or deleted then add in TVP

                    //if (objImmunizationDTO.IsDeleted || objImmunizationDTO.IsDirty || objImmunizationDTO.PatientImmunizationId == 0)
                    //{
                    dRow["PatientImmunizationID"] = objImmunizationDTO.PatientImmunizationId;
                    dRow["ImmunizationTypeID"] = objImmunizationDTO.ImmunizationType.ImmunizationTypeId;
                    
                    //dRow["VisitID"] = VisitId;                    
                    dRow["VisitID"] = objImmunizationDTO.VisitId;

                    if (!string.IsNullOrEmpty(objImmunizationDTO.Note))
                    {
                        dRow["ImmunizationNote"] = objImmunizationDTO.Note;
                    }
                    else
                    {
                        dRow["ImmunizationNote"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.NoteSubject))
                    {
                        dRow["NoteSubject"] = objImmunizationDTO.NoteSubject;
                    }
                    else
                    {
                        dRow["NoteSubject"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.Dose))
                    {
                        dRow["Dose"] = objImmunizationDTO.Dose;
                    }
                    else
                    {
                        dRow["Dose"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.LotNumber))
                    {
                        dRow["LotNumber"] = objImmunizationDTO.LotNumber;
                    }
                    else
                    {
                        dRow["LotNumber"] = DBNull.Value;
                    }

                    dRow["IsRefusal"] = objImmunizationDTO.IsRefusal;

                    if (!string.IsNullOrEmpty(objImmunizationDTO.RefusalReason))
                    {
                        dRow["RefusalReason"] = objImmunizationDTO.RefusalReason;
                    }
                    else
                    {
                        dRow["RefusalReason"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.Site))
                    {
                        dRow["Site"] = objImmunizationDTO.Site;
                    }
                    else
                    {
                        dRow["Site"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.Series))
                    {
                        dRow["Series"] = objImmunizationDTO.Series;
                    }
                    else
                    {
                        dRow["Series"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.Manufacturer))
                    {
                        dRow["Manufacturer"] = objImmunizationDTO.Manufacturer;
                    }
                    else
                    {
                        dRow["Manufacturer"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(objImmunizationDTO.Rout))
                    {
                        dRow["Route"] = objImmunizationDTO.Rout;
                    }
                    else
                    {
                        dRow["Route"] = DBNull.Value;
                    }

                    //  if (objImmunizationDTO.DateAdmin != null && objImmunizationDTO.DateAdmin > ProjectConstant.DATE_MINIMUM)
                    if (objImmunizationDTO.DateAdmin != null)
                    {
                        dRow["DateAdmin"] = objImmunizationDTO.DateAdmin;
                    }
                    else
                    {
                        dRow["DateAdmin"] = DBNull.Value;
                    }

                    //  if (objImmunizationDTO.NextDoseDue != null && objImmunizationDTO.NextDoseDue > ProjectConstant.DATE_MINIMUM)
                    if (objImmunizationDTO.NextDoseDue != null)
                    {
                        dRow["NextDoseDue"] = objImmunizationDTO.NextDoseDue;
                    }
                    else
                    {
                        dRow["NextDoseDue"] = DBNull.Value;
                    }

                    // if (objImmunizationDTO.ExpiryDate != null && objImmunizationDTO.ExpiryDate > ProjectConstant.DATE_MINIMUM)
                    if (objImmunizationDTO.ExpiryDate != null)
                    {
                        dRow["ExpiryDate"] = objImmunizationDTO.ExpiryDate;
                    }
                    else
                    {
                        dRow["ExpiryDate"] = DBNull.Value;
                    }

                    if (objImmunizationDTO.DictationID != System.Guid.Empty)
                    {
                        dRow["DictationID"] = objImmunizationDTO.DictationID;
                    }
                    else
                    {
                        dRow["DictationID"] = DBNull.Value;
                    }

                    dRow["CreatedByUpdateByUserID"] = managedByUserId;

                    if (objImmunizationDTO.IsDirty && objImmunizationDTO.UpdatedAt != null && objImmunizationDTO.UpdatedAt > ProjectConstant.DATE_MINIMUM)
                    {
                        dRow["ModifiedAt"] = ActionDate;
                        dRow["CreatedAt"] = DBNull.Value;
                    }
                    else //if (objImmunizationDTO.CreatedAt != null && objImmunizationDTO.CreatedAt > ProjectConstant.DATE_MINIMUM)
                    {
                        dRow["CreatedAt"] = ActionDate;
                        dRow["ModifiedAt"] = DBNull.Value;
                    }

                    dRow["IsUpdated"] = objImmunizationDTO.IsDirty;
                    dRow["IsDeleted"] = objImmunizationDTO.IsDeleted;

                    dtImmunization.Rows.Add(dRow);
                    //}
                }
                dtImmunization.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //  throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.IMMUNIZATION_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.IMMUNIZATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtImmunization;
        }


        internal static DataTable ROSDTOListConverter(List<ROSDTO> ROSList)
        {
            DataTable dtROS = null;

            try
            {
                dtROS = new DataTable();

                dtROS.Columns.Add("PatientROSID", typeof(System.Int32));
                dtROS.Columns.Add("VisitID", typeof(System.Int32));
                dtROS.Columns.Add("PatientID", typeof(System.Int32));
                dtROS.Columns.Add("ROSMainCategoryTypeID", typeof(System.Int32));
                dtROS.Columns.Add("ROSSubCategoryTypeID", typeof(System.Int32));
                dtROS.Columns.Add("ROSSubLevel3CategoryTypeID", typeof(System.Int32));

                dtROS.Columns.Add("IsNormal", typeof(System.Boolean));

                dtROS.Columns.Add("DictationID", typeof(System.Guid));
                dtROS.Columns.Add("Notes", typeof(System.String));
                dtROS.Columns.Add("NoteSubject", typeof(System.String));
                dtROS.Columns.Add("CreatedBy", typeof(System.Int32));
                dtROS.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtROS.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtROS.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtROS.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtROS.AcceptChanges();

                ROSDTO obj = null;

                //bool SkipRecord = false;

                for (int index = 0; index < ROSList.Count; index++)
                {
                    //SkipRecord = false;

                    obj = ROSList[index];

                    //if (obj.PatientROSID > 0 && !obj.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}

                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtROS.NewRow();


                    dRow["PatientROSID"] = obj.PatientROSID;
                    dRow["VisitID"] = obj.VisitID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["ROSMainCategoryTypeID"] = obj.ROSMainCategoryTypeID;
                    dRow["ROSSubCategoryTypeID"] = obj.ROSSubCategoryTypeID;
                    dRow["ROSSubLevel3CategoryTypeID"] = obj.ROSSubLevel3CategoryTypeID;

                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;

                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);

                    dRow["IsDeleted"] = obj.IsDeleted;

                    if (!string.IsNullOrEmpty(obj.IsNormal))
                    {
                        if (obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)))
                        {
                            dRow["IsNormal"] = true;
                        }
                        else
                        {
                            dRow["IsNormal"] = false;
                        }
                    }
                    else
                    {
                        dRow["IsNormal"] = DBNull.Value;
                    }
                    if (obj.DictationID != System.Guid.Empty)
                    {
                        dRow["DictationID"] = obj.DictationID;
                    }
                    else
                    {
                        dRow["DictationID"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(obj.Notes))
                    {
                        dRow["Notes"] = obj.Notes;
                    }
                    else
                    {
                        dRow["Notes"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(obj.NoteSubject))
                    {
                        dRow["NoteSubject"] = obj.NoteSubject;
                    }
                    else
                    {
                        dRow["NoteSubject"] = DBNull.Value;
                    }

                    dtROS.Rows.Add(dRow);
                    //}
                }
                dtROS.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //   throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.ROS_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtROS;
        }

        internal static DataTable ROSDTOListConverter(List<ROSDTO> ROSList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            DataTable dtROS = null;

            try
            {
                dtROS = new DataTable();


                dtROS.Columns.Add("PatientROSID", typeof(System.Int32));
                dtROS.Columns.Add("VisitID", typeof(System.Int32));
                dtROS.Columns.Add("PatientID", typeof(System.Int32));
                dtROS.Columns.Add("ROSMainCategoryTypeID", typeof(System.Int32));
                dtROS.Columns.Add("ROSSubCategoryTypeID", typeof(System.Int32));
                dtROS.Columns.Add("ROSSubLevel3CategoryTypeID", typeof(System.Int32));
                dtROS.Columns.Add("IsNormal", typeof(System.Boolean));

                dtROS.Columns.Add("DictationID", typeof(System.Guid));
                dtROS.Columns.Add("Notes", typeof(System.String));
                dtROS.Columns.Add("NoteSubject", typeof(System.String));
                dtROS.Columns.Add("CreatedBy", typeof(System.Int32));
                dtROS.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtROS.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtROS.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtROS.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtROS.AcceptChanges();

                ROSDTO obj = null;

                for (int index = 0; index < ROSList.Count; index++)
                {
                    obj = ROSList[index];
                    DataRow dRow = dtROS.NewRow();


                    dRow["PatientROSID"] = obj.PatientROSID;
                    dRow["VisitID"] = VisitId;
                    dRow["PatientID"] = PatientId;
                    dRow["ROSMainCategoryTypeID"] = obj.ROSMainCategoryTypeID;
                    dRow["ROSSubCategoryTypeID"] = obj.ROSSubCategoryTypeID;
                    dRow["ROSSubLevel3CategoryTypeID"] = obj.ROSSubLevel3CategoryTypeID;
                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;

                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);

                    dRow["IsDeleted"] = obj.IsDeleted;

                    if (!string.IsNullOrEmpty(obj.IsNormal))
                    {
                        if (obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)) || obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)))
                        {
                            dRow["IsNormal"] = true;
                        }
                        else
                        {
                            dRow["IsNormal"] = false;
                        }
                    }
                    else
                    {
                        dRow["IsNormal"] = DBNull.Value;
                    }
                    if (obj.DictationID != System.Guid.Empty)
                    {
                        dRow["DictationID"] = obj.DictationID;
                    }
                    else
                    {
                        dRow["DictationID"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(obj.Notes))
                    {
                        dRow["Notes"] = obj.Notes;
                    }
                    else
                    {
                        dRow["Notes"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(obj.NoteSubject))
                    {
                        dRow["NoteSubject"] = obj.NoteSubject;
                    }
                    else
                    {
                        dRow["NoteSubject"] = DBNull.Value;
                    }


                    dtROS.Rows.Add(dRow);
                }

                dtROS.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //   throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.ROS_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtROS;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 21 March 2011
        /// Method Will Verfiy Valid Date IF SO Set Date 
        /// Else Set Null Value to column :)
        /// </summary>
        /// <param name="dRow"></param>
        /// <param name="PropertyName"></param>
        /// <param name="DateValue"></param>
        private static void SetDateIFNotNull(DataRow dRow, String PropertyName, DateTime DateValue)
        {
            //   string InValidDate = "1/1/0001";
            //   if (!DateValue.ToShortDateString().Equals(InValidDate))

            if (DateValue > ProjectConstant.DATE_MINIMUM)
            {
                dRow[PropertyName] = DateValue;
            }
            else
            {
                dRow[PropertyName] = DBNull.Value;
            }
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011       
        /// </summary>
        /// <returns></returns>
        internal static DataTable HPIDTOListConverter(List<HPI_CCDTO> hpiList)
        {
            DataTable dtHPI = null;

            try
            {
                dtHPI = new DataTable();

                dtHPI.Columns.Add("PatientHPIID", typeof(System.Int32));
                dtHPI.Columns.Add("PatientID", typeof(System.Int32));
                dtHPI.Columns.Add("VisitID", typeof(System.Int32));
                dtHPI.Columns.Add("HPINote", typeof(System.String));
                dtHPI.Columns.Add("NoteSubject", typeof(System.String));
                dtHPI.Columns.Add("DictationID", typeof(System.Guid));
                dtHPI.Columns.Add("ChiefComplaint", typeof(System.String));
                dtHPI.Columns.Add("Symptom", typeof(System.String));
                dtHPI.Columns.Add("Duration", typeof(System.String));
                dtHPI.Columns.Add("Location", typeof(System.String));
                dtHPI.Columns.Add("Quality", typeof(System.String));
                dtHPI.Columns.Add("Severity", typeof(System.String));
                dtHPI.Columns.Add("Context", typeof(System.String));
                dtHPI.Columns.Add("ModifyingFactors", typeof(System.String));
                dtHPI.Columns.Add("Exacerbating", typeof(System.String));
                dtHPI.Columns.Add("Progression", typeof(System.String));
                dtHPI.Columns.Add("Radiation", typeof(System.String));
                dtHPI.Columns.Add("Onset", typeof(System.String));

                dtHPI.Columns.Add("CreatedBy", typeof(System.Int32));
                dtHPI.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtHPI.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtHPI.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtHPI.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtHPI.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtHPI.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtHPI.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtHPI.AcceptChanges();

                HPI_CCDTO obj = null;

                bool SkipRecord = false;

                for (int index = 0; index < hpiList.Count; index++)
                {
                    SkipRecord = false;

                    obj = hpiList[index];

                    if (obj.PatientHPIID > 0 && !obj.IsDirty)
                    {
                        // This Record should Not Updated Skip this Recokrd Do Nothing
                        SkipRecord = true;
                    }

                    if (!SkipRecord)
                    {
                        DataRow dRow = dtHPI.NewRow();

                        dRow["PatientHPIID"] = obj.PatientHPIID;
                        dRow["PatientID"] = obj.PatientID;
                        dRow["VisitID"] = obj.VisitID;
                        dRow["HPINote"] = obj.Note;
                        dRow["NoteSubject"] = obj.NoteSubject;
                        dRow["DictationID"] = obj.DictationID;

                        dRow["ChiefComplaint"] = obj.ChiefComplaint;

                        if (obj.HPIFields != null)
                        {
                            dRow["Symptom"] = obj.HPIFields.Symptom;
                            dRow["Duration"] = obj.HPIFields.Duration;
                            dRow["Location"] = obj.HPIFields.Location;
                            dRow["Quality"] = obj.HPIFields.Quality;
                            dRow["Severity"] = obj.HPIFields.Severity;
                            dRow["Context"] = obj.HPIFields.Context;
                            dRow["ModifyingFactors"] = obj.HPIFields.ModifyingFactors;
                            dRow["Exacerbating"] = obj.HPIFields.Exacerbating;
                            dRow["Progression"] = obj.HPIFields.Progression;
                            dRow["Radiation"] = obj.HPIFields.Radiation;
                            dRow["Onset"] = obj.HPIFields.Onset;
                        }

                        dRow["CreatedBy"] = obj.CreatedBy;
                        dRow["CreatedAt"] = obj.CreatedAt;
                        dRow["ModifiedBy"] = obj.ModifiedBy;

                        SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);

                        dRow["IsDeleted"] = obj.IsDeleted;

                        dRow["ClinicBranchID"] = obj.ClinicBranchID;
                        dRow["CreatedByUserTypeID"] = obj.CreatedByUserTypeID;
                        dRow["ModifiedByUserTypeID"] = obj.ModifiedByUserTypeID;

                        dtHPI.Rows.Add(dRow);
                    } //skip record
                }

                dtHPI.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtHPI;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011       
        /// </summary>
        /// <returns></returns>
        internal static DataTable HPIDTOListConverter(List<HPI_CCDTO> hpiList, int managedUsedId, int PatientId, int VisitId, int ClinicBranchId, DateTime ActionDate)
        {
            DataTable dtHPI = null;

            try
            {
                dtHPI = new DataTable();


                dtHPI.Columns.Add("PatientHPIID", typeof(System.Int32));
                dtHPI.Columns.Add("PatientID", typeof(System.Int32));
                dtHPI.Columns.Add("VisitID", typeof(System.Int32));
                dtHPI.Columns.Add("HPINote", typeof(System.String));
                dtHPI.Columns.Add("NoteSubject", typeof(System.String));
                dtHPI.Columns.Add("DictationID", typeof(System.Guid));

                dtHPI.Columns.Add("ChiefComplaint", typeof(System.String));
                dtHPI.Columns.Add("Symptom", typeof(System.String));
                dtHPI.Columns.Add("Duration", typeof(System.String));
                dtHPI.Columns.Add("Location", typeof(System.String));
                dtHPI.Columns.Add("Quality", typeof(System.String));
                dtHPI.Columns.Add("Severity", typeof(System.String));
                dtHPI.Columns.Add("Context", typeof(System.String));
                dtHPI.Columns.Add("ModifyingFactors", typeof(System.String));
                dtHPI.Columns.Add("Exacerbating", typeof(System.String));
                dtHPI.Columns.Add("Progression", typeof(System.String));
                dtHPI.Columns.Add("Radiation", typeof(System.String));
                dtHPI.Columns.Add("Onset", typeof(System.String));

                dtHPI.Columns.Add("CreatedBy", typeof(System.Int32));
                dtHPI.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtHPI.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtHPI.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtHPI.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtHPI.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtHPI.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtHPI.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtHPI.AcceptChanges();

                HPI_CCDTO obj = null;

                for (int index = 0; index < hpiList.Count; index++)
                {
                    obj = hpiList[index];
                    DataRow dRow = dtHPI.NewRow();


                    dRow["PatientHPIID"] = obj.PatientHPIID;
                    dRow["PatientID"] = PatientId;
                    dRow["VisitID"] = VisitId;
                    dRow["HPINote"] = obj.Note;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["DictationID"] = obj.DictationID;

                    dRow["ChiefComplaint"] = obj.ChiefComplaint;

                    if (obj.HPIFields != null)
                    {
                        dRow["Symptom"] = obj.HPIFields.Symptom;
                        dRow["Duration"] = obj.HPIFields.Duration;
                        dRow["Location"] = obj.HPIFields.Location;
                        dRow["Quality"] = obj.HPIFields.Quality;
                        dRow["Severity"] = obj.HPIFields.Severity;
                        dRow["Context"] = obj.HPIFields.Context;
                        dRow["ModifyingFactors"] = obj.HPIFields.ModifyingFactors;
                        dRow["Exacerbating"] = obj.HPIFields.Exacerbating;
                        dRow["Progression"] = obj.HPIFields.Progression;
                        dRow["Radiation"] = obj.HPIFields.Radiation;

                        dRow["Onset"] = obj.HPIFields.Onset;
                        //  SetDateIFNotNull(dRow, "Onset", obj.HPIFields.Onset);
                    }

                    dRow["CreatedBy"] = managedUsedId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedUsedId;

                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);

                    dRow["IsDeleted"] = obj.IsDeleted;

                    dRow["ClinicBranchID"] = ClinicBranchId;
                    dRow["CreatedByUserTypeID"] = managedUsedId;
                    dRow["ModifiedByUserTypeID"] = managedUsedId;

                    dtHPI.Rows.Add(dRow);
                }

                dtHPI.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //    throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ImmunizationDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.HPI_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.HPI_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtHPI;
        }

        private static void SetDateIfNotNull(HPI_CCDTO obj, DataRow dRow, String PropertyName)
        {
            // if (obj.HPIFields.Onset > ProjectConstant.DATE_MINIMUM)
            if (!string.IsNullOrEmpty(obj.HPIFields.Onset))
                dRow[PropertyName] = obj.HPIFields.Onset;
            else
                dRow[PropertyName] = DBNull.Value;
        }

        internal static DataTable PhysicalDTOListConverter(List<PhysicalsDTO> PhysicalList)
        {
            DataTable dtPhysical = null;

            try
            {
                dtPhysical = new DataTable();

                dtPhysical.Columns.Add("PatientPhysicalID", typeof(System.Int32));
                dtPhysical.Columns.Add("VisitID", typeof(System.Int32));
                dtPhysical.Columns.Add("PatientID", typeof(System.Int32));
                dtPhysical.Columns.Add("PhysicalMainCategoryTypeID", typeof(System.Int32));
                dtPhysical.Columns.Add("PhysicalSubCategoryTypeID", typeof(System.Int32));
                dtPhysical.Columns.Add("IsNormal", typeof(System.Boolean));

                dtPhysical.Columns.Add("DictationID", typeof(System.Guid));
                dtPhysical.Columns.Add("Notes", typeof(System.String));
                dtPhysical.Columns.Add("NoteSubject", typeof(System.String));
                dtPhysical.Columns.Add("CreatedBy", typeof(System.Int32));
                dtPhysical.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtPhysical.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtPhysical.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtPhysical.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtPhysical.Columns.Add("PhysicalSubLevel3CategoryTypeID", typeof(System.Int32));

                dtPhysical.AcceptChanges();

                PhysicalsDTO obj = null;

                //bool SkipRecord = false;

                for (int index = 0; index < PhysicalList.Count; index++)
                {
                    //SkipRecord = false;

                    obj = PhysicalList[index];


                    //if (obj.PatientPhysicalID > 0 && !obj.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}

                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtPhysical.NewRow();


                    dRow["PatientPhysicalID"] = obj.PatientPhysicalID;
                    dRow["VisitID"] = obj.VisitID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["PhysicalMainCategoryTypeID"] = obj.PhysicalMainCategoryTypeID;
                    dRow["PhysicalSubCategoryTypeID"] = obj.PhysicalSubCategoryTypeID;

                    dRow["PhysicalSubLevel3CategoryTypeID"] = obj.PhysicalSubLevel3CategoryTypeID; //M

                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;

                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);

                    dRow["IsDeleted"] = obj.IsDeleted;

                    if (!string.IsNullOrEmpty(obj.IsNormal))
                    {
                        if (obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)) || obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)))
                        {
                            dRow["IsNormal"] = true;
                        }
                        else
                        {
                            dRow["IsNormal"] = false;
                        }
                    }
                    else
                    {
                        dRow["IsNormal"] = DBNull.Value;
                    }
                    if (obj.DictationID != System.Guid.Empty)
                    {
                        dRow["DictationID"] = obj.DictationID;
                    }
                    else
                    {
                        dRow["DictationID"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(obj.Notes))
                    {
                        dRow["Notes"] = obj.Notes;
                    }
                    else
                    {
                        dRow["Notes"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(obj.NoteSubject))
                    {
                        dRow["NoteSubject"] = obj.Notes;
                    }
                    else
                    {
                        dRow["NoteSubject"] = DBNull.Value;
                    }

                    dtPhysical.Rows.Add(dRow);
                    //} //skip record
                }

                dtPhysical.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".PhysicalDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtPhysical;
        }

        internal static DataTable PhysicalDTOListConverter(List<PhysicalsDTO> PhysicalList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            DataTable dtPhysical = null;

            try
            {
                dtPhysical = new DataTable();
                dtPhysical.Columns.Add("PatientPhysicalID", typeof(System.Int32));
                dtPhysical.Columns.Add("VisitID", typeof(System.Int32));
                dtPhysical.Columns.Add("PatientID", typeof(System.Int32));
                dtPhysical.Columns.Add("PhysicalMainCategoryTypeID", typeof(System.Int32));
                dtPhysical.Columns.Add("PhysicalSubCategoryTypeID", typeof(System.Int32));
                dtPhysical.Columns.Add("IsNormal", typeof(System.Boolean));

                dtPhysical.Columns.Add("DictationID", typeof(System.Guid));
                dtPhysical.Columns.Add("Notes", typeof(System.String));
                dtPhysical.Columns.Add("NoteSubject", typeof(System.String));
                dtPhysical.Columns.Add("CreatedBy", typeof(System.Int32));
                dtPhysical.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtPhysical.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtPhysical.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtPhysical.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtPhysical.Columns.Add("PhysicalSubLevel3CategoryTypeID", typeof(System.Int32));

                dtPhysical.AcceptChanges();

                PhysicalsDTO obj = null;

                for (int index = 0; index < PhysicalList.Count; index++)
                {
                    obj = PhysicalList[index];
                    DataRow dRow = dtPhysical.NewRow();


                    dRow["PatientPhysicalID"] = obj.PatientPhysicalID;
                    dRow["VisitID"] = VisitId;
                    dRow["PatientID"] = PatientId;
                    dRow["PhysicalMainCategoryTypeID"] = obj.PhysicalMainCategoryTypeID;
                    dRow["PhysicalSubCategoryTypeID"] = obj.PhysicalSubCategoryTypeID;

                    dRow["PhysicalSubLevel3CategoryTypeID"] = obj.PhysicalSubLevel3CategoryTypeID; //M

                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;

                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);

                    dRow["IsDeleted"] = obj.IsDeleted;

                    if (!string.IsNullOrEmpty(obj.IsNormal))
                    {
                        if (obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)) || obj.IsNormal.ToLower().Equals((ProjectConstant.SELECTION_TRUE)))
                        {
                            dRow["IsNormal"] = true;
                        }
                        else
                        {
                            dRow["IsNormal"] = false;
                        }
                    }
                    else
                    {
                        dRow["IsNormal"] = DBNull.Value;
                    }
                    if (obj.DictationID != System.Guid.Empty)
                    {
                        dRow["DictationID"] = obj.DictationID;
                    }
                    else
                    {
                        dRow["DictationID"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(obj.Notes))
                    {
                        dRow["Notes"] = obj.Notes;
                    }
                    else
                    {
                        dRow["Notes"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(obj.NoteSubject))
                    {
                        dRow["NoteSubject"] = obj.Notes;
                    }
                    else
                    {
                        dRow["NoteSubject"] = DBNull.Value;
                    }

                    dtPhysical.Rows.Add(dRow);
                }

                dtPhysical.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                // throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".PhysicalDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.PHYSICALS_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.PHYSICALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtPhysical;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011        
        /// </summary>
        /// <returns></returns>
        public static DataTable DiagnosisDTOListConverter(List<PatientDiagnosisDTO> diagnosisList)
        {
            DataTable dtDiagnosis = null;

            try
            {
                dtDiagnosis = new DataTable();

                dtDiagnosis.Columns.Add("PatientDiagnosisID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("PatientID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("VisitID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("ICDCodeID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("OnSet", typeof(System.String));
                dtDiagnosis.Columns.Add("DictationID", typeof(System.Guid));
                dtDiagnosis.Columns.Add("DiagnosisNote", typeof(System.String));
                dtDiagnosis.Columns.Add("NoteSubject", typeof(System.String));
                dtDiagnosis.Columns.Add("IsChronic", typeof(System.Boolean));
                dtDiagnosis.Columns.Add("IsActive", typeof(System.Boolean));
                dtDiagnosis.Columns.Add("CreatedBy", typeof(System.Int32));
                dtDiagnosis.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtDiagnosis.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtDiagnosis.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtDiagnosis.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtDiagnosis.Columns.Add("RCOPIAID", typeof(System.String));
                dtDiagnosis.Columns.Add("ICDCode", typeof(System.String));
                dtDiagnosis.Columns.Add("ICDDescription", typeof(System.String));

                dtDiagnosis.AcceptChanges();

                PatientDiagnosisDTO obj = null;
                //bool SkipRecord = false;

                for (int index = 0; index < diagnosisList.Count; index++)
                {
                    //SkipRecord = false;

                    obj = diagnosisList[index];

                    //if (obj.PatientDiagnosisID > 0 && !obj.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}
                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtDiagnosis.NewRow();


                    dRow["PatientDiagnosisID"] = obj.PatientDiagnosisID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["VisitID"] = obj.VisitID;

                    if (obj.ICDCode != null)
                        dRow["ICDCodeID"] = obj.ICDCode.IcdCodeIdSeq;

                    //  dRow["OnSet"] = obj.OnSet;
                    //   SetDateIFNotNull(dRow, "OnSet", obj.OnSet);
                    dRow["OnSet"] = obj.OnSet;

                    dRow["CreatedBy"] = obj.CreatedBy;
                    SetDateIFNotNull(dRow, "CreatedAt", obj.CreatedAt);
                    //dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;

                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);

                    dRow["DictationID"] = obj.DictationID;
                    dRow["DiagnosisNote"] = obj.Note;
                    dRow["NoteSubject"] = obj.NoteSubject;

                    dRow["IsChronic"] = obj.IsChronic;

                    //if (string.IsNullOrEmpty(obj.IsChronic))
                    //{
                    //    dRow["IsChronic"] = DBNull.Value;
                    //}
                    //else if (obj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_TRUE))
                    //{
                    //    dRow["IsChronic"] = true;
                    //}
                    //else if (obj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_False))
                    //{
                    //    dRow["IsChronic"] = false;
                    //}
                    //else if (obj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_NULL))
                    //{
                    //    dRow["IsChronic"] = DBNull.Value;
                    //}



                    dRow["IsActive"] = obj.IsActive;

                    dRow["RCOPIAID"] = obj.RCOPIAID;
                    dRow["ICDCode"] = obj.ICDCode;
                    dRow["ICDDescription"] = obj.ICDCodeDescription;

                    dRow["IsDeleted"] = obj.IsDeleted;


                    dtDiagnosis.Rows.Add(dRow);
                    //}
                }

                dtDiagnosis.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG + ".DiagnosisDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtDiagnosis;
        }


        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011        
        /// </summary>
        /// <returns></returns>
        public static DataTable DiagnosisDTOListConverter(List<PatientDiagnosisDTO> diagnosisList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            DataTable dtDiagnosis = null;

            try
            {
                dtDiagnosis = new DataTable();

                dtDiagnosis.Columns.Add("PatientDiagnosisID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("PatientID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("VisitID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("ICDCodeID", typeof(System.Int32));
                dtDiagnosis.Columns.Add("OnSet", typeof(System.String));
                dtDiagnosis.Columns.Add("DictationID", typeof(System.Guid));
                dtDiagnosis.Columns.Add("DiagnosisNote", typeof(System.String));
                dtDiagnosis.Columns.Add("NoteSubject", typeof(System.String));
                dtDiagnosis.Columns.Add("IsChronic", typeof(System.Boolean));
                dtDiagnosis.Columns.Add("IsActive", typeof(System.Boolean));
                dtDiagnosis.Columns.Add("CreatedBy", typeof(System.Int32));
                dtDiagnosis.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtDiagnosis.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtDiagnosis.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtDiagnosis.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtDiagnosis.Columns.Add("RCOPIAID", typeof(System.String));
                dtDiagnosis.Columns.Add("ICDCode", typeof(System.String));
                dtDiagnosis.Columns.Add("ICDDescription", typeof(System.String));

                dtDiagnosis.AcceptChanges();

                PatientDiagnosisDTO obj = null;

                for (int index = 0; index < diagnosisList.Count; index++)
                {
                    obj = diagnosisList[index];
                    DataRow dRow = dtDiagnosis.NewRow();


                    dRow["PatientDiagnosisID"] = obj.PatientDiagnosisID;
                    dRow["PatientID"] = PatientId;
                    //dRow["VisitID"] = VisitId;

                    dRow["VisitID"] = obj.VisitID;

                    if (obj.ICDCode != null)
                        dRow["ICDCodeID"] = obj.ICDCode.IcdCodeIdSeq;
                    else
                        dRow["ICDCodeID"] = DBNull.Value;

                    dRow["OnSet"] = obj.OnSet;

                    //SetDateIFNotNull(dRow, "OnSet", obj.OnSet);

                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;

                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);

                    dRow["DictationID"] = obj.DictationID;
                    dRow["DiagnosisNote"] = obj.Note;
                    dRow["NoteSubject"] = obj.NoteSubject;

                    dRow["IsChronic"] = obj.IsChronic;

                    //if (string.IsNullOrEmpty(obj.IsChronic))
                    //{
                    //    dRow["IsChronic"] = DBNull.Value;
                    //}
                    //else if (obj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_TRUE))
                    //{
                    //    dRow["IsChronic"] = true;
                    //}
                    //else if (obj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_False))
                    //{
                    //    dRow["IsChronic"] = false;
                    //}
                    //else if (obj.IsChronic.Trim().Equals(ProjectConstant.SELECTION_NULL))
                    //{
                    //    dRow["IsChronic"] = DBNull.Value;
                    //}
                    dRow["IsActive"] = obj.IsActive;

                    dRow["IsDeleted"] = obj.IsDeleted;
                    dRow["RCOPIAID"] = obj.RCOPIAID;
                    dRow["ICDCode"] = obj.ICDCode;
                    dRow["ICDDescription"] = obj.ICDCodeDescription;


                    dtDiagnosis.Rows.Add(dRow);
                }

                dtDiagnosis.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                // throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG + ".DiagnosisDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.DIAGNOSIS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtDiagnosis;
        }


        internal static DataTable RecommendationDTOListConverter(List<PatientRecommendationDTO> RecommendationList)
        {
            DataTable dtRecommendation = null;

            try
            {
                dtRecommendation = new DataTable();

                dtRecommendation.Columns.Add("PatientRecommendationID", typeof(System.Int32));
                dtRecommendation.Columns.Add("PatientID", typeof(System.Int32));
                dtRecommendation.Columns.Add("VisitID", typeof(System.Int32));
                dtRecommendation.Columns.Add("RecommendationPlanID", typeof(System.Int32));
                dtRecommendation.Columns.Add("RecommendationMethodID", typeof(System.Int32));
                dtRecommendation.Columns.Add("RecommendationLink", typeof(System.String));
                dtRecommendation.Columns.Add("RecommendationDurationID", typeof(System.Int32));
                dtRecommendation.Columns.Add("IsDiscussedWithPatient", typeof(System.Boolean));
                dtRecommendation.Columns.Add("DictationID", typeof(System.Guid));
                dtRecommendation.Columns.Add("Notes", typeof(System.String));
                dtRecommendation.Columns.Add("NoteSubject", typeof(System.String));
                dtRecommendation.Columns.Add("CreatedBy", typeof(System.Int32));
                dtRecommendation.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtRecommendation.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtRecommendation.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtRecommendation.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtRecommendation.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtRecommendation.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtRecommendation.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));
                dtRecommendation.Columns.Add("Description", typeof(System.String));
                dtRecommendation.Columns.Add("PatientDiagnosisID", typeof(System.Int32));

                //dtRecommendation.Columns.Add("PatientRecommendationID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("PatientID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("VisitID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("RecommendationMethodID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("RecommendationLink", typeof(System.String));
                //dtRecommendation.Columns.Add("RecommendationDurationID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("RecommendationPlanID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("DictationID", typeof(System.Guid));
                //dtRecommendation.Columns.Add("Notes", typeof(System.String));
                //dtRecommendation.Columns.Add("NoteSubject", typeof(System.String));
                //dtRecommendation.Columns.Add("IsDiscussedWithPatient", typeof(System.Boolean));
                //dtRecommendation.Columns.Add("IsActive", typeof(System.Boolean));
                //dtRecommendation.Columns.Add("CreatedBy", typeof(System.Int32));
                //dtRecommendation.Columns.Add("CreatedAt", typeof(System.DateTime));
                //dtRecommendation.Columns.Add("ModifiedBy", typeof(System.Int32));
                //dtRecommendation.Columns.Add("ModifiedAt", typeof(System.DateTime));
                //dtRecommendation.Columns.Add("IsDeleted", typeof(System.Boolean));

                //dtRecommendation.Columns.Add("ClinicBranchID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                //dtRecommendation.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtRecommendation.AcceptChanges();

                PatientRecommendationDTO obj = null;
                //bool SkipRecord = false;
                for (int index = 0; index < RecommendationList.Count; index++)
                {
                    //SkipRecord = false;

                    obj = RecommendationList[index];

                    //if (obj.PatientRecommendationID > 0 && !obj.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}
                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtRecommendation.NewRow();


                    dRow["PatientRecommendationID"] = obj.PatientRecommendationID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["VisitID"] = obj.VisitID;

                    if (obj.Method != null)
                        dRow["RecommendationMethodID"] = obj.Method.RecommendationMethodTypeID;
                    else
                        dRow["RecommendationMethodID"] = DBNull.Value;

                    if (obj.Duration != null)
                        dRow["RecommendationDurationID"] = obj.Duration.RecommendationDurationTypeID;
                    else
                        dRow["RecommendationDurationID"] = DBNull.Value;

                    if (obj.Links != null)
                        dRow["RecommendationLink"] = obj.Links;
                    else
                        dRow["RecommendationLink"] = DBNull.Value;

                    if (obj.Plan != null)
                        dRow["RecommendationPlanID"] = obj.Plan.RecommendationPlanTypeID;
                    else
                        dRow["RecommendationPlanID"] = DBNull.Value;




                    if (string.IsNullOrEmpty(obj.IsDiscussedWithPatient))
                    {
                        dRow["IsDiscussedWithPatient"] = DBNull.Value;
                    }
                    else if (obj.IsDiscussedWithPatient.Trim().Equals(ProjectConstant.SELECTION_TRUE))
                    {
                        dRow["IsDiscussedWithPatient"] = true;
                    }
                    else if (obj.IsDiscussedWithPatient.Trim().Equals(ProjectConstant.SELECTION_False))
                    {
                        dRow["IsDiscussedWithPatient"] = false;
                    }
                    else if (obj.IsDiscussedWithPatient.Trim().Equals(ProjectConstant.SELECTION_NULL))
                    {
                        dRow["IsDiscussedWithPatient"] = DBNull.Value;
                    }

                    //  dRow["IsActive"] = obj.IsActive;

                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;

                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);

                    dRow["DictationID"] = obj.DictationID;
                    dRow["Notes"] = obj.Note;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["IsDeleted"] = obj.IsDeleted;

                    dRow["ClinicBranchID"] = obj.ClinicBranchID;
                    dRow["CreatedByUserTypeID"] = obj.CreatedByUserTypeID;
                    dRow["ModifiedByUserTypeID"] = obj.ModifiedByUserTypeID;
                    dRow["Description"] = obj.Description;
                    dRow["PatientDiagnosisID"] = obj.PatientDiagnosisID;

                    dtRecommendation.Rows.Add(dRow);
                    //}
                }
                dtRecommendation.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG + ".DiagnosisDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtRecommendation;
        }


        internal static DataTable RecommendationDTOListConverter(List<PatientRecommendationDTO> RecommendationList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            DataTable dtRecommendation = null;

            try
            {
                dtRecommendation = new DataTable();

                dtRecommendation.Columns.Add("PatientRecommendationID", typeof(System.Int32));
                dtRecommendation.Columns.Add("PatientID", typeof(System.Int32));
                dtRecommendation.Columns.Add("VisitID", typeof(System.Int32));
                dtRecommendation.Columns.Add("RecommendationPlanID", typeof(System.Int32));
                dtRecommendation.Columns.Add("RecommendationMethodID", typeof(System.Int32));
                dtRecommendation.Columns.Add("RecommendationLink", typeof(System.String));
                dtRecommendation.Columns.Add("RecommendationDurationID", typeof(System.Int32));
                dtRecommendation.Columns.Add("IsDiscussedWithPatient", typeof(System.Boolean));
                dtRecommendation.Columns.Add("DictationID", typeof(System.Guid));
                dtRecommendation.Columns.Add("Notes", typeof(System.String));
                dtRecommendation.Columns.Add("NoteSubject", typeof(System.String));
                dtRecommendation.Columns.Add("CreatedBy", typeof(System.Int32));
                dtRecommendation.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtRecommendation.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtRecommendation.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtRecommendation.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtRecommendation.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtRecommendation.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtRecommendation.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));
                dtRecommendation.Columns.Add("Description", typeof(System.String));
                dtRecommendation.Columns.Add("PatientDiagnosisID", typeof(System.Int32));

                dtRecommendation.AcceptChanges();

                PatientRecommendationDTO obj = null;

                for (int index = 0; index < RecommendationList.Count; index++)
                {
                    obj = RecommendationList[index];
                    DataRow dRow = dtRecommendation.NewRow();


                    dRow["PatientRecommendationID"] = obj.PatientRecommendationID;
                    dRow["PatientID"] = PatientId;
                    dRow["VisitID"] = VisitId;

                    if (obj.Method != null)
                        dRow["RecommendationMethodID"] = obj.Method.RecommendationMethodTypeID;
                    else
                        dRow["RecommendationMethodID"] = DBNull.Value;

                    if (obj.Duration != null)
                        dRow["RecommendationDurationID"] = obj.Duration.RecommendationDurationTypeID;
                    else
                        dRow["RecommendationDurationID"] = DBNull.Value;

                    if (obj.Links != null)
                        dRow["RecommendationLink"] = obj.Links;
                    else
                        dRow["RecommendationLink"] = DBNull.Value;

                    if (obj.Plan != null)
                        dRow["RecommendationPlanID"] = obj.Plan.RecommendationPlanTypeID;
                    else
                        dRow["RecommendationPlanID"] = DBNull.Value;

                    if (string.IsNullOrEmpty(obj.IsDiscussedWithPatient))
                        dRow["IsDiscussedWithPatient"] = DBNull.Value;

                    else if (obj.IsDiscussedWithPatient.Equals(ProjectConstant.SELECTION_TRUE))
                        dRow["IsDiscussedWithPatient"] = true;

                    else
                        dRow["IsDiscussedWithPatient"] = false;
                    // dRow["IsActive"] = obj.IsActive;

                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;

                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);

                    dRow["DictationID"] = obj.DictationID;
                    dRow["Notes"] = obj.Note;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["IsDeleted"] = obj.IsDeleted;

                    dRow["ClinicBranchID"] = ClinicBranchId;
                    dRow["CreatedByUserTypeID"] = obj.CreatedByUserTypeID;
                    dRow["ModifiedByUserTypeID"] = obj.ModifiedByUserTypeID;
                    dRow["Description"] = obj.Description;
                    dRow["PatientDiagnosisID"] = obj.PatientDiagnosisID;

                    dtRecommendation.Rows.Add(dRow);
                }

                dtRecommendation.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //  throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG + ".DiagnosisDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.RECOMMENDATION_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.RECOMMENDATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtRecommendation;
        }



        internal static DataTable ProcedureDTOListConverter(List<ProcedureDTO> InputList)
        {
            DataTable dtProcedure = null;

            try
            {
                dtProcedure = new DataTable();

                dtProcedure.Columns.Add("PatientProcedureID", typeof(System.Int32));
                dtProcedure.Columns.Add("PatientID", typeof(System.Int32));
                dtProcedure.Columns.Add("VisitID", typeof(System.Int32));
                dtProcedure.Columns.Add("CPTMedicalTypeID", typeof(System.Int32));
                dtProcedure.Columns.Add("Notes", typeof(System.String));
                dtProcedure.Columns.Add("NoteSubject", typeof(System.String));
                dtProcedure.Columns.Add("DictationID", typeof(System.Guid));
                dtProcedure.Columns.Add("CreatedBy", typeof(System.Int32));
                dtProcedure.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtProcedure.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtProcedure.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtProcedure.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtProcedure.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtProcedure.AcceptChanges();

                ProcedureDTO obj = null;
                //bool SkipRecord = false;

                for (int index = 0; index < InputList.Count; index++)
                {
                    //SkipRecord = false;

                    obj = InputList[index];

                    //if (obj.PatientProcedureID > 0 && !obj.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}

                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtProcedure.NewRow();


                    dRow["PatientProcedureID"] = obj.PatientProcedureID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["VisitID"] = obj.VisitID;

                    if (obj.CPTMedicalType != null)
                        dRow["CPTMedicalTypeID"] = obj.CPTMedicalType.CPTMedicalTypeID;
                    else
                        dRow["CPTMedicalTypeID"] = DBNull.Value;

                    dRow["Notes"] = obj.Notes;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;
                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;
                    dtProcedure.Rows.Add(dRow);
                    //}

                    dtProcedure.AcceptChanges();
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG + ".ProcedureDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtProcedure;
        }

        internal static DataTable ProcedureDTOListConverter(List<ProcedureDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            DataTable dtProcedure = null;

            try
            {
                dtProcedure = new DataTable();

                dtProcedure.Columns.Add("PatientProcedureID", typeof(System.Int32));
                dtProcedure.Columns.Add("VisitID", typeof(System.Int32));
                dtProcedure.Columns.Add("PatientID", typeof(System.Int32));
                dtProcedure.Columns.Add("CPTMedicalTypeID", typeof(System.Int32));
                dtProcedure.Columns.Add("Notes", typeof(System.String));
                dtProcedure.Columns.Add("NoteSubject", typeof(System.String));
                dtProcedure.Columns.Add("DictationID", typeof(System.Guid));
                dtProcedure.Columns.Add("CreatedBy", typeof(System.Int32));
                dtProcedure.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtProcedure.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtProcedure.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtProcedure.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtProcedure.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtProcedure.AcceptChanges();

                ProcedureDTO obj = null;


                for (int index = 0; index < InputList.Count; index++)
                {
                    obj = InputList[index];
                    DataRow dRow = dtProcedure.NewRow();

                    QuivusLogger.Info("ProcedureDTOListConverter with VisitID: " + VisitId.ToString() + " and PatientID: " + PatientId.ToString());
                    dRow["PatientProcedureID"] = obj.PatientProcedureID;
                    dRow["PatientID"] = PatientId;
                    dRow["VisitID"] = VisitId;

                    if (obj.CPTMedicalType != null)
                        dRow["CPTMedicalTypeID"] = obj.CPTMedicalType.CPTMedicalTypeID;
                    else
                        dRow["CPTMedicalTypeID"] = DBNull.Value;

                    dRow["Notes"] = obj.Notes;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;
                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;
                    dtProcedure.Rows.Add(dRow);
                }

                dtProcedure.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //    throw new TableConversionException(ProjectConstant.DIAGNOSIS_CONVERSION_ERROR_MSG + ".ProcedureDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.PROCEDURE_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.PROCEDURE_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtProcedure;
        }

        internal static DataTable InHouseLabsDTOListConverter(List<InHouseLabDTO> InputList)
        {
            DataTable dtInHouseLabs = null;

            try
            {
                dtInHouseLabs = new DataTable();

                dtInHouseLabs.Columns.Add("PatientInHouseLabTestID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("PatientID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("VisitID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("InHouseLabTestTypeID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("TestResult", typeof(System.String));
                dtInHouseLabs.Columns.Add("Notes", typeof(System.String));
                dtInHouseLabs.Columns.Add("NoteSubject", typeof(System.String));
                dtInHouseLabs.Columns.Add("DictationID", typeof(System.Guid));
                dtInHouseLabs.Columns.Add("CreatedBy", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtInHouseLabs.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtInHouseLabs.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtInHouseLabs.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtInHouseLabs.Columns.Add("ResultAttachment", typeof(System.Byte[]));

                dtInHouseLabs.AcceptChanges();

                InHouseLabDTO obj = null;

                //bool SkipRecord = false;

                for (int index = 0; index < InputList.Count; index++)
                {
                    //SkipRecord = false;

                    obj = InputList[index];

                    //if (obj.PatientInHouseLabTestID > 0 && !obj.IsDirty)
                    //{
                    //    SkipRecord = true;
                    //}
                    //if (!SkipRecord)
                    //{
                    DataRow dRow = dtInHouseLabs.NewRow();


                    dRow["PatientInHouseLabTestID"] = obj.PatientInHouseLabTestID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["VisitID"] = obj.VisitID;

                    if (obj.InHouseLabTestType != null)
                        dRow["InHouseLabTestTypeID"] = obj.InHouseLabTestType.InHouseLabTestTypeID;
                    else
                        dRow["InHouseLabTestTypeID"] = DBNull.Value;

                    dRow["Notes"] = obj.Notes;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["TestResult"] = obj.TestResult;

                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;
                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;

                    dRow["ResultAttachment"] = obj.ResultAttachment;

                    //dtInHouseLabs.Columns.Add("ResultAttachment", typeof(System.Byte[]));

                    dtInHouseLabs.Rows.Add(dRow);
                    //}
                }
                dtInHouseLabs.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.INHOUSE_LABS_CONVERSION_ERROR_MSG + ".InHouseLabsDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtInHouseLabs;
        }

        internal static DataTable InHouseLabsDTOListConverter(List<InHouseLabDTO> InputList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            DataTable dtInHouseLabs = null;

            try
            {
                dtInHouseLabs = new DataTable();

                dtInHouseLabs.Columns.Add("PatientInHouseLabTestID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("VisitID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("PatientID", typeof(System.Int32));

                dtInHouseLabs.Columns.Add("InHouseLabTestTypeID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("TestResult", typeof(System.String));
                dtInHouseLabs.Columns.Add("Notes", typeof(System.String));
                dtInHouseLabs.Columns.Add("NoteSubject", typeof(System.String));
                dtInHouseLabs.Columns.Add("DictationID", typeof(System.Guid));
                dtInHouseLabs.Columns.Add("CreatedBy", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtInHouseLabs.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtInHouseLabs.Columns.Add("IsDeleted", typeof(System.Boolean));


                dtInHouseLabs.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtInHouseLabs.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtInHouseLabs.Columns.Add("ResultAttachment", typeof(System.Byte[]));

                dtInHouseLabs.AcceptChanges();

                InHouseLabDTO obj = null;

                for (int index = 0; index < InputList.Count; index++)
                {
                    obj = InputList[index];
                    DataRow dRow = dtInHouseLabs.NewRow();


                    dRow["PatientInHouseLabTestID"] = obj.PatientInHouseLabTestID;
                    dRow["PatientID"] = PatientId;
                    dRow["VisitID"] = VisitId;

                    if (obj.InHouseLabTestType != null)
                        dRow["InHouseLabTestTypeID"] = obj.InHouseLabTestType.InHouseLabTestTypeID;
                    else
                        dRow["InHouseLabTestTypeID"] = DBNull.Value;

                    dRow["Notes"] = obj.Notes;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["TestResult"] = obj.TestResult;

                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;
                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;

                    dRow["ResultAttachment"] = obj.ResultAttachment;

                    dtInHouseLabs.Rows.Add(dRow);
                }

                dtInHouseLabs.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //  throw new TableConversionException(ProjectConstant.INHOUSE_LABS_CONVERSION_ERROR_MSG + ".InHouseLabsDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.IN_HOUSE_LABS_TABLE_CONVERSION_CONVERSION_ERROR_MSG,
                                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                                   ErrorCodes.IN_HOUSE_LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                                   ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtInHouseLabs;
        }

        internal static DataTable PatientFollowupInstructionDTOListConverter(List<PatientFollowupInstructionDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            DataTable dtFollowupInstruction = null;

            try
            {
                dtFollowupInstruction = new DataTable();

                dtFollowupInstruction.Columns.Add("FollowupInstructionID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("VisitID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("PatientID", typeof(System.Int32));

                dtFollowupInstruction.Columns.Add("FollowupService", typeof(System.String));
                dtFollowupInstruction.Columns.Add("AgeValueTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("AfterDurationTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("AfterDurationValue", typeof(System.Int32));


                dtFollowupInstruction.Columns.Add("ForDurationTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("ForDurationValue", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("AgeTypeID", typeof(System.Int32));

                dtFollowupInstruction.Columns.Add("IsAgeSelected", typeof(System.Boolean));
                dtFollowupInstruction.Columns.Add("IsDurationSelected", typeof(System.Boolean));

                dtFollowupInstruction.Columns.Add("Notes", typeof(System.String));
                dtFollowupInstruction.Columns.Add("NoteSubject", typeof(System.String));
                dtFollowupInstruction.Columns.Add("DictationID", typeof(System.Guid));
                dtFollowupInstruction.Columns.Add("CreatedBy", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtFollowupInstruction.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtFollowupInstruction.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtFollowupInstruction.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtFollowupInstruction.AcceptChanges();

                PatientFollowupInstructionDTO obj = null;

                for (int index = 0; index < InputList.Count; index++)
                {
                    obj = InputList[index];
                    DataRow dRow = dtFollowupInstruction.NewRow();


                    dRow["FollowupInstructionID"] = obj.FollowupInstructionID;
                    dRow["PatientID"] = PatientId;
                    dRow["VisitID"] = VisitId;

                    if (obj.objServiceTypeDTO != null)
                        dRow["FollowupService"] = obj.objServiceTypeDTO.FollowupService; //objServiceTypeDTO Already Validated Not Need to Provide Null Check...For Safe Side Only Placed :)

                    if (obj.AfterInstructionDurationTypeDTO != null)
                    {
                        dRow["AfterDurationTypeID"] = obj.AfterInstructionDurationTypeDTO.DurationTypeID;
                        dRow["AfterDurationValue"] = obj.AfterDurationValue;

                    }
                    else
                    {
                        dRow["AfterDurationTypeID"] = DBNull.Value;
                        dRow["AfterDurationValue"] = DBNull.Value;
                    }

                    if (obj.ForInstructionDurationTypeDTO != null)
                    {
                        dRow["ForDurationTypeID"] = obj.ForInstructionDurationTypeDTO.DurationTypeID;
                        dRow["ForDurationValue"] = obj.ForDurationValue;

                    }
                    else
                    {
                        dRow["ForDurationTypeID"] = DBNull.Value;
                        dRow["ForDurationValue"] = DBNull.Value;
                    }

                    if (obj.AgeTypeDTO != null)
                    {
                        dRow["AgeTypeID"] = obj.AgeTypeDTO.InstructionAgeTypeID;


                    }
                    else
                    {
                        dRow["AgeTypeID"] = DBNull.Value;


                    }
                    if (obj.AgeValueTypeDTO != null)
                    {
                        dRow["AgeValueTypeID"] = obj.AgeValueTypeDTO.InstructionAgeValueTypeID; // Commented By Ismail No Property Ib DB 
                    }
                    else
                    {
                        dRow["AgeValueTypeID"] = DBNull.Value; // Commented By Ismail No Property Ib DB 
                    }

                    dRow["IsAgeSelected"] = obj.IsAgeSelected;
                    dRow["IsDurationSelected"] = obj.IsDurationSelected;
                    dRow["Notes"] = obj.Notes;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["ClinicBranchID"] = ClinicBranchId;
                    dRow["CreatedBy"] = managedByUserId;
                    dRow["CreatedAt"] = ActionDate;
                    dRow["ModifiedBy"] = managedByUserId;
                    SetDateIFNotNull(dRow, "ModifiedAt", ActionDate);
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;
                    dtFollowupInstruction.Rows.Add(dRow);
                }

                dtFollowupInstruction.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //  throw new TableConversionException(ProjectConstant.PATIENT_FOLLOWUP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG + ".PatientFollowupInstructionDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
                throw new TableConversionException(ProjectConstant.PATIENT_FOLLOWUP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.FOLLOW_UP_INSTRUCTION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.TABLE_CONVERSION_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX));
            }
            return dtFollowupInstruction;
        }

        internal static DataTable PatientFollowupInstructionDTOListConverter(List<PatientFollowupInstructionDTO> InputList)
        {
            DataTable dtFollowupInstruction = null;

            try
            {
                dtFollowupInstruction = new DataTable();

                dtFollowupInstruction.Columns.Add("FollowupInstructionID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("VisitID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("PatientID", typeof(System.Int32));

                dtFollowupInstruction.Columns.Add("FollowupService", typeof(System.String));
                dtFollowupInstruction.Columns.Add("AgeValueTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("AfterDurationTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("AfterDurationValue", typeof(System.Int32));


                dtFollowupInstruction.Columns.Add("ForDurationTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("ForDurationValue", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("AgeTypeID", typeof(System.Int32));

                dtFollowupInstruction.Columns.Add("IsAgeSelected", typeof(System.Boolean));
                dtFollowupInstruction.Columns.Add("IsDurationSelected", typeof(System.Boolean));

                dtFollowupInstruction.Columns.Add("Notes", typeof(System.String));
                dtFollowupInstruction.Columns.Add("NoteSubject", typeof(System.String));
                dtFollowupInstruction.Columns.Add("DictationID", typeof(System.Guid));
                dtFollowupInstruction.Columns.Add("CreatedBy", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtFollowupInstruction.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtFollowupInstruction.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtFollowupInstruction.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtFollowupInstruction.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtFollowupInstruction.AcceptChanges();

                PatientFollowupInstructionDTO obj = null;

                for (int index = 0; index < InputList.Count; index++)
                {
                    obj = InputList[index];
                    DataRow dRow = dtFollowupInstruction.NewRow();


                    dRow["FollowupInstructionID"] = obj.FollowupInstructionID;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["VisitID"] = obj.VisitID;

                    if (obj.objServiceTypeDTO != null)
                        dRow["FollowupService"] = obj.objServiceTypeDTO.FollowupService; //objServiceTypeDTO Already Validated Not Need to Provide Null Check...For Safe Side Only Placed :)

                    if (obj.AfterInstructionDurationTypeDTO != null)
                    {
                        dRow["AfterDurationTypeID"] = obj.AfterInstructionDurationTypeDTO.DurationTypeID;
                        dRow["AfterDurationValue"] = obj.AfterDurationValue;

                    }
                    else
                    {
                        dRow["AfterDurationTypeID"] = DBNull.Value;
                        dRow["AfterDurationValue"] = DBNull.Value;
                    }

                    if (obj.ForInstructionDurationTypeDTO != null)
                    {
                        dRow["ForDurationTypeID"] = obj.ForInstructionDurationTypeDTO.DurationTypeID;
                        dRow["ForDurationValue"] = obj.ForDurationValue;

                    }
                    else
                    {
                        dRow["ForDurationTypeID"] = DBNull.Value;
                        dRow["ForDurationValue"] = DBNull.Value;
                    }

                    //if (obj.AgeTypeDTO != null)
                    //{
                    //    dRow["AgeTypeID"] = obj.AgeTypeDTO.InstructionAgeTypeID;
                    //    //     dRow["AgeValueTypeID"] = obj.AgeValueTypeDTO.InstructionAgeValueTypeID; // Commented By Ismail No Property Ib DB 

                    //}
                    //else
                    //{
                    //    dRow["AgeTypeID"] = DBNull.Value;
                    //    //    dRow["AgeValueTypeID"] = DBNull.Value; // Commented By Ismail No Property Ib DB 

                    //}
                    if (obj.AgeTypeDTO != null)
                    {
                        dRow["AgeTypeID"] = obj.AgeTypeDTO.InstructionAgeTypeID;


                    }
                    else
                    {
                        dRow["AgeTypeID"] = DBNull.Value;


                    }
                    if (obj.AgeValueTypeDTO != null)
                    {
                        dRow["AgeValueTypeID"] = obj.AgeValueTypeDTO.InstructionAgeValueTypeID; // Commented By Ismail No Property Ib DB 
                    }
                    else
                    {
                        dRow["AgeValueTypeID"] = DBNull.Value; // Commented By Ismail No Property Ib DB 
                    }

                    dRow["IsAgeSelected"] = obj.IsAgeSelected;
                    dRow["IsDurationSelected"] = obj.IsDurationSelected;
                    dRow["Notes"] = obj.Notes;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["ClinicBranchID"] = obj.ClinicBranchId;
                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;
                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;
                    dtFollowupInstruction.Rows.Add(dRow);
                }

                dtFollowupInstruction.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.PATIENT_FOLLOWUP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG + ".PatientFollowupInstructionDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtFollowupInstruction;
        }


        public static DataTable ModuleDictationDTOListConverter(List<ModuleDictationDTO> ModuleDictationDTOList, int ManagedByUserID, int ClinicBranchID, int VisitId)
        {
            DataTable dtModuleDictation = null;

            try
            {
                dtModuleDictation = new DataTable();
                dtModuleDictation.Columns.Add("ModuleDictationID", typeof(System.Int32));
                dtModuleDictation.Columns.Add("ModuleTypeID", typeof(System.Int32));
                dtModuleDictation.Columns.Add("VisitID", typeof(System.Int32));
                dtModuleDictation.Columns.Add("DictationID", typeof(System.Guid));

                dtModuleDictation.Columns.Add("CreatedBy", typeof(System.Int32));
                dtModuleDictation.Columns.Add("CreatedAt", typeof(System.DateTime));

                dtModuleDictation.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtModuleDictation.Columns.Add("ModifiedAt", typeof(System.DateTime));

                dtModuleDictation.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtModuleDictation.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtModuleDictation.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtModuleDictation.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));

                dtModuleDictation.AcceptChanges();

                ModuleDictationDTO objModuleDictationDTO;

                bool SkipRecord = false;

                for (int indexModuleDictation = 0; indexModuleDictation < ModuleDictationDTOList.Count; indexModuleDictation++)
                {
                    SkipRecord = false;

                    objModuleDictationDTO = ModuleDictationDTOList[indexModuleDictation];

                    if (objModuleDictationDTO.ModuleDictationId > 0 && !objModuleDictationDTO.IsDirty)
                    {
                        SkipRecord = true;
                    }
                    if (!SkipRecord)
                    {

                        DataRow dRow = dtModuleDictation.NewRow();

                        dRow["ModuleDictationID"] = objModuleDictationDTO.ModuleDictationId;
                        dRow["ModuleTypeID"] = objModuleDictationDTO.ModuleTypeID;
                        dRow["VisitID"] = VisitId;
                        dRow["DictationID"] = objModuleDictationDTO.DictationID;

                        if (objModuleDictationDTO.ModuleDictationId == 0)
                        {
                            dRow["CreatedBy"] = ManagedByUserID;
                            dRow["CreatedAt"] = objModuleDictationDTO.DictationTimeStamp;
                            dRow["CreatedByUserTypeID"] = objModuleDictationDTO.ManagedByUserTypeId;
                        }
                        else
                        {
                            dRow["ModifiedBy"] = ManagedByUserID;
                            dRow["ModifiedAt"] = objModuleDictationDTO.DictationTimeStamp;

                            dRow["ModifiedByUserTypeID"] = objModuleDictationDTO.ManagedByUserTypeId;
                        }

                        dRow["IsDeleted"] = objModuleDictationDTO.IsDeleted;
                        dRow["ClinicBranchID"] = ClinicBranchID;

                        dtModuleDictation.Rows.Add(dRow);
                    }
                }
                dtModuleDictation.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.MODULE_NOTE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ModuleNoteDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtModuleDictation;
        }

        internal static DataTable AllergiesDTOListConverter(List<AllergyDTO> Allergylist)
        {
            DataTable dtAllergies = null;

            try
            {
                dtAllergies = new DataTable();

                dtAllergies.Columns.Add("PatientAllergyID", typeof(System.Int32));
                dtAllergies.Columns.Add("AllergyName", typeof(System.String));
                dtAllergies.Columns.Add("AllergyTypeID", typeof(System.Int32));

                dtAllergies.Columns.Add("AllergyNote", typeof(System.String));
                dtAllergies.Columns.Add("NoteSubject", typeof(System.String));
                dtAllergies.Columns.Add("VisitID", typeof(System.Int32));
                dtAllergies.Columns.Add("Reaction", typeof(System.String));
                dtAllergies.Columns.Add("Onset", typeof(System.String));
                dtAllergies.Columns.Add("DictationID", typeof(System.Guid));
                dtAllergies.Columns.Add("CreatedBy", typeof(System.Int32));
                dtAllergies.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtAllergies.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtAllergies.Columns.Add("ModifiedAt", typeof(System.DateTime));

                dtAllergies.Columns.Add("RCOPIAID", typeof(System.Int32));
                dtAllergies.Columns.Add("Drug", typeof(System.String));
                dtAllergies.Columns.Add("Status", typeof(System.String));
                dtAllergies.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtAllergies.AcceptChanges();

                foreach (AllergyDTO obj in Allergylist)
                {
                    DataRow dRow = dtAllergies.NewRow();

                    dRow["PatientAllergyID"] = obj.PatientAllergyId;
                    dRow["AllergyName"] = obj.AllergyName;
                    dRow["AllergyTypeID"] = obj.AllergyTypeID;


                    dRow["AllergyNote"] = obj.Note;
                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["VisitID"] = obj.VisitId;

                    dRow["Reaction"] = obj.Reaction;

                    dRow["Onset"] = obj.OnSet;
                    dRow["DictationID"] = obj.DictationID;

                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;
                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);

                    dRow["RCOPIAID"] = obj.RCOPIAID;
                    // dRow["Status"] = obj.Status;
                    dRow["Drug"] = obj.Drug;

                    dRow["IsDeleted"] = obj.IsDeleted;

                    dtAllergies.Rows.Add(dRow);
                }
                dtAllergies.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ALLERGIES_CONVERSION_ERROR_MSG + ".AllergiesDTOListConverter", ProjectConstant.ERROR_CODE.AllergyTableConversion.ToString());
            }
            return dtAllergies;
        }

        internal static DataTable PatientPrescriptionMedicationDTOListConverter(List<PrescriptionMedicationDTO> PrescriptionMedicationlist)
        {
            DataTable dtPrescription = null;

            try
            {
                dtPrescription = new DataTable();
                dtPrescription.Columns.Add("PrescriptionMedicationID", typeof(System.Int32));
                dtPrescription.Columns.Add("PatientPrescriptionID", typeof(System.Int32));
                dtPrescription.Columns.Add("RCOPIAID", typeof(System.String));
                dtPrescription.Columns.Add("NDCID", typeof(System.String));
                dtPrescription.Columns.Add("FirstDataBankMedID", typeof(System.String));
                dtPrescription.Columns.Add("BrandName", typeof(System.String));
                dtPrescription.Columns.Add("GenericName", typeof(System.String));
                dtPrescription.Columns.Add("Form", typeof(System.String));
                dtPrescription.Columns.Add("Strength", typeof(System.String));
                dtPrescription.Columns.Add("Action", typeof(System.String));
                dtPrescription.Columns.Add("Dose", typeof(System.String));
                dtPrescription.Columns.Add("DoseUnit", typeof(System.String));
                dtPrescription.Columns.Add("Route", typeof(System.String));
                dtPrescription.Columns.Add("DoseTiming", typeof(System.String));
                dtPrescription.Columns.Add("DoseOther", typeof(System.String));
                dtPrescription.Columns.Add("Duration", typeof(System.String));
                dtPrescription.Columns.Add("Quantity", typeof(System.String));
                dtPrescription.Columns.Add("QuantityUnit", typeof(System.String));
                dtPrescription.Columns.Add("Refills", typeof(System.String));
                dtPrescription.Columns.Add("IsSubstitutionPermitted", typeof(System.Boolean));
                dtPrescription.Columns.Add("OtherNotes", typeof(System.String));
                dtPrescription.Columns.Add("PatientNotes", typeof(System.String));
                dtPrescription.Columns.Add("Comments", typeof(System.String));
                dtPrescription.Columns.Add("Status", typeof(System.String));
                dtPrescription.Columns.Add("StartDate", typeof(System.DateTime));
                dtPrescription.Columns.Add("EndDate", typeof(System.DateTime));
                dtPrescription.Columns.Add("NoteSubject", typeof(System.String));
                dtPrescription.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtPrescription.Columns.Add("CreatedBy", typeof(System.Int32));
                dtPrescription.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtPrescription.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtPrescription.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtPrescription.Columns.Add("PatientID", typeof(System.Int32));
                dtPrescription.Columns.Add("VisitID", typeof(System.Int32));

                dtPrescription.AcceptChanges();

                foreach (PrescriptionMedicationDTO obj in PrescriptionMedicationlist)
                {
                    DataRow dRow = dtPrescription.NewRow();

                    dRow["PrescriptionMedicationID"] = obj.PrescriptionMedicationID;
                    dRow["PatientPrescriptionID"] = obj.PatientPrescriptionID;

                    dRow["RCOPIAID"] = obj.RCOPIAID;
                    dRow["NDCID"] = obj.NDCID;

                    dRow["FirstDataBankMedID"] = obj.FirstDataBankMedID;
                    dRow["BrandName"] = obj.BrandName;
                    dRow["GenericName"] = obj.GenericName;
                    dRow["Form"] = obj.Form;
                    dRow["Strength"] = obj.Strength;
                    dRow["Action"] = obj.Action;
                    dRow["Dose"] = obj.Dose;
                    dRow["DoseUnit"] = obj.DoseUnit;
                    dRow["Route"] = obj.Route;
                    dRow["DoseTiming"] = obj.DoseTiming;
                    dRow["DoseOther"] = obj.DoseOther;
                    dRow["Duration"] = obj.Duration;
                    dRow["Quantity"] = obj.Quantity;
                    dRow["QuantityUnit"] = obj.QuantityUnit;
                    dRow["Refills"] = obj.Refills;
                    dRow["IsSubstitutionPermitted"] = obj.IsSubstitutionPermitted;
                    dRow["OtherNotes"] = obj.OtherNotes;
                    dRow["PatientNotes"] = obj.PatientNotes;
                    dRow["Comments"] = obj.Comments;
                    dRow["Status"] = obj.Status;

                    SetDateIFNotNull(dRow, "StartDate", obj.StartDate);
                    SetDateIFNotNull(dRow, "EndDate", obj.EndDate);

                    //  dRow["EndDate"] = obj.EndDate;
                    dRow["NoteSubject"] = obj.NoteSubject;

                    SetDateIFNotNull(dRow, "CreatedAt", obj.CreatedAt);
                    //   dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["CreatedBy"] = obj.CreatedBy;
                    SetDateIFNotNull(dRow, "ModifiedAt", obj.ModifiedAt);
                    // dRow["ModifiedAt"] = obj.ModifiedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;
                    dRow["IsDeleted"] = obj.IsDeleted;
                    dRow["PatientID"] = obj.PatientID;
                    dRow["VisitID"] = obj.VisitID;

                    dtPrescription.Rows.Add(dRow);
                }
                dtPrescription.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.PRESCRIPTION_MEDICATION_CONVERSION_ERROR_MSG + ".PatientPrescriptionMedicationDTOListConverter", ProjectConstant.ERROR_CODE.PrescriptionMedicationTableConversion.ToString());
            }
            return dtPrescription;
        }

        internal static DataTable MedicationDTOListConverter(List<MedicationDTO> MedicationList)
        {
            DataTable dtMedication = null;

            try
            {
                dtMedication = new DataTable();

                dtMedication.Columns.Add("PatientMedicationID", typeof(System.Int32));
                dtMedication.Columns.Add("PatientID", typeof(System.Int32));
                dtMedication.Columns.Add("RCOPIAID", typeof(System.String));
                dtMedication.Columns.Add("NDCID", typeof(System.String));
                dtMedication.Columns.Add("FirstDataBankMedID", typeof(System.String));
                dtMedication.Columns.Add("BrandName", typeof(System.String));
                dtMedication.Columns.Add("GenericName", typeof(System.String));
                dtMedication.Columns.Add("Form", typeof(System.String));
                dtMedication.Columns.Add("Strength", typeof(System.String));
                dtMedication.Columns.Add("Action", typeof(System.String));
                dtMedication.Columns.Add("Dose", typeof(System.String));
                dtMedication.Columns.Add("DoseUnit", typeof(System.String));
                dtMedication.Columns.Add("Route", typeof(System.String));
                dtMedication.Columns.Add("DoseTiming", typeof(System.String));
                dtMedication.Columns.Add("DoseOther", typeof(System.String));
                dtMedication.Columns.Add("Duration", typeof(System.String));
                dtMedication.Columns.Add("Quantity", typeof(System.String));
                dtMedication.Columns.Add("QuantityUnit", typeof(System.String));
                dtMedication.Columns.Add("Refills", typeof(System.String));
                dtMedication.Columns.Add("IsSubstitutionPermitted", typeof(System.Boolean));
                dtMedication.Columns.Add("OtherNotes", typeof(System.String));
                dtMedication.Columns.Add("PatientNotes", typeof(System.String));
                dtMedication.Columns.Add("Comments", typeof(System.String));


                dtMedication.Columns.Add("CreatedAt", typeof(System.DateTime));
                dtMedication.Columns.Add("ModifiedAt", typeof(System.DateTime));
                dtMedication.Columns.Add("CreatedBy", typeof(System.Int32));
                dtMedication.Columns.Add("ModifiedBy", typeof(System.Int32));

                dtMedication.Columns.Add("IsDeleted", typeof(System.Boolean));
                dtMedication.Columns.Add("Status", typeof(System.String));
                // dtMedication.Columns.Add("OtherNotes", typeof(System.String));
                dtMedication.Columns.Add("NoteSubject", typeof(System.String));

                dtMedication.AcceptChanges();

                foreach (MedicationDTO obj in MedicationList)
                {
                    DataRow dRow = dtMedication.NewRow();

                    dRow["PatientMedicationID"] = obj.PatientMedicationID;
                    dRow["PatientID"] = obj.PatientId;
                    dRow["RCOPIAID"] = obj.RCOPIAID;
                    dRow["NDCID"] = obj.NDCID;

                    dRow["FirstDataBankMedID"] = obj.FirstDataBankMedID;
                    dRow["GenericName"] = obj.GenericName;
                    dRow["BrandName"] = obj.BrandName;
                    dRow["Form"] = obj.Form;
                    dRow["Strength"] = obj.Strength;
                    dRow["Action"] = obj.Action;
                    dRow["Dose"] = obj.Dose;
                    dRow["DoseUnit"] = obj.DoseUnit;
                    dRow["Route"] = obj.Route;
                    dRow["DoseTiming"] = obj.DoseTiming;
                    dRow["DoseOther"] = obj.DoseOther;
                    dRow["Duration"] = obj.Duration;
                    dRow["Quantity"] = obj.Quantity;
                    dRow["QuantityUnit"] = obj.QuantityUnit;
                    dRow["Refills"] = obj.Refills;
                    dRow["IsSubstitutionPermitted"] = obj.IsSubstitutionPermitted;
                    dRow["OtherNotes"] = obj.OtherNotes;

                    dRow["NoteSubject"] = obj.NoteSubject;
                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["CreatedAt"] = obj.CreatedAt;
                    dRow["ModifiedBy"] = obj.ModifiedBy;
                    dRow["ModifiedAt"] = obj.ModifiedAt;
                    dRow["IsDeleted"] = obj.IsDeleted;

                    dtMedication.Rows.Add(dRow);
                }
                dtMedication.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ALLERGIES_CONVERSION_ERROR_MSG + ".AllergiesDTOListConverter", ProjectConstant.ERROR_CODE.AllergyTableConversion.ToString());
            }
            return dtMedication;
        }
        internal static DataTable StationIDsListConverter(List<int> stationIDsList)
        {
            DataTable stationIDsTable = null;
            try
            {
                stationIDsTable = new DataTable();
                stationIDsTable.Columns.Add("StationId", typeof(System.Int32));
                stationIDsTable.AcceptChanges();
                foreach (int stationID in stationIDsList)
                {
                    DataRow drStationID = stationIDsTable.NewRow();
                    drStationID["StationId"] = stationID;
                    stationIDsTable.Rows.Add(drStationID);
                }
                stationIDsTable.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.ALLERGIES_CONVERSION_ERROR_MSG + ".StationIDsListConverter", ProjectConstant.ERROR_CODE.AllergyTableConversion.ToString());
            }
            return stationIDsTable;
        }
/// <summary>
///  baber waqas 
/// </summary>
/// <param name="icdCodeDTO"></param>
/// <param name="managedByUserIDForVisit"></param>
/// <param name="clinicBranchID"></param>
/// <returns></returns>
        public static DataTable ICDCustomCodeDTOConverter(ICDCodeDTO icdCodeDTO, int managedByUserIDForVisit, int clinicBranchID)
        {
            DataTable iDataTable = null;

            try
            {
                iDataTable = new DataTable();
                iDataTable.Columns.Add("ICDCustomCodeID", typeof(System.Int32));
                iDataTable.Columns.Add("ICDCodeID", typeof(System.Int32));
                iDataTable.Columns.Add("ICDCodeName", typeof(System.String));
                iDataTable.Columns.Add("ICDCustomDescription", typeof(System.String));
                iDataTable.Columns.Add("ModifiedBy", typeof(System.Int32));
                iDataTable.Columns.Add("ModifiedAt", typeof(System.DateTime));
                iDataTable.Columns.Add("IsDeleted", typeof(System.Boolean));
                iDataTable.Columns.Add("ClinicBranchId", typeof(System.Int32));
                iDataTable.AcceptChanges();

                DataRow dRow = iDataTable.NewRow();

                dRow["ICDCustomCodeID"] = 0;
                dRow["ICDCodeID"] = icdCodeDTO.IcdCodeIdSeq;
                dRow["ICDCodeName"] = icdCodeDTO.IcdCodeName;
                dRow["ICDCustomDescription"] = icdCodeDTO.IcdCodeDescription;

                QuivusLogger.Info(" icd custom dexcription recieved " + icdCodeDTO.IcdCodeDescription+ " for  " + icdCodeDTO.IcdCodeName);
                dRow["ModifiedBy"] = managedByUserIDForVisit;
                dRow["ModifiedAt"] = DBNull.Value;
                dRow["IsDeleted"] = false;
                dRow["ClinicBranchId"] = clinicBranchID;



                iDataTable.Rows.Add(dRow);

                iDataTable.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                //TODO: change the errror string s
                throw new TableConversionException(ProjectConstant.ALLERGIES_CONVERSION_ERROR_MSG + ".ICD CODE Custom to DTO Converter", ProjectConstant.ERROR_CODE.AllergyTableConversion.ToString());
            }
            return iDataTable;
        }
    }
}
