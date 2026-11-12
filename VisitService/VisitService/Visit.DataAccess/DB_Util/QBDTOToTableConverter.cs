using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using Quivus.Utility;
using System.Data;
using QuivusModel.Billing.Visit;

namespace Visit.DataAccess.DB_Util
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date: 01/30/2012
    /// </summary>
    public class QBDTOToTableConverter
    {
        internal static DataTable ProcedureModofierDTOListConverter(List<PatientProcedureModifierDTO> InputList)
        {
            DataTable dtProcedure = null;

            try
            {
                dtProcedure = new DataTable();

                dtProcedure.Columns.Add("ProcedureModifierID", typeof(System.Int32));
                dtProcedure.Columns.Add("PatientProcedureID", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifierTypeId", typeof(System.Int32));                           
                dtProcedure.Columns.Add("CreatedBy", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifiedBy", typeof(System.Int32));
                dtProcedure.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtProcedure.AcceptChanges();

                PatientProcedureModifierDTO obj = null;
                //bool SkipRecord = false;

                for (int index = 0; index < InputList.Count; index++)
                {
                    obj = InputList[index];

                    DataRow dRow = dtProcedure.NewRow();

                    dRow["ProcedureModifierID"] = obj.ProcedureModifierID;
                    dRow["PatientProcedureID"] = obj.PatientProcedureID;                   

                    if (obj.ProcedureModifierType != null)
                        dRow["ModifierTypeId"] = obj.ProcedureModifierType.ModifierTypeId;
                    else
                        dRow["ModifierTypeId"] = DBNull.Value;

                    dRow["CreatedBy"] = obj.CreatedBy;
                    dRow["ModifiedBy"] = obj.ModifiedBy;                  
                    dRow["IsDeleted"] = obj.IsDeleted;

                    dtProcedure.Rows.Add(dRow);

                    dtProcedure.AcceptChanges();
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.PROCEDURE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ProcedureDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtProcedure;
        }
        internal static DataTable ProcedureDTOListConverter(List<QBProcedureDTO> InputList)
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
                dtProcedure.Columns.Add("ModifiedBy", typeof(System.Int32));                
                dtProcedure.Columns.Add("IsDeleted", typeof(System.Boolean));

                dtProcedure.Columns.Add("ClinicBranchID", typeof(System.Int32));
                dtProcedure.Columns.Add("CreatedByUserTypeID", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifiedByUserTypeID", typeof(System.Int32));
                //fro super bill
                dtProcedure.Columns.Add("IncludeInBilling", typeof(System.Boolean));
                dtProcedure.Columns.Add("IsCreatedFromSuperBill", typeof(System.Boolean));
                dtProcedure.Columns.Add("ModifierTypeId1", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifierTypeId2", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifierTypeId3", typeof(System.Int32));
                dtProcedure.Columns.Add("ModifierTypeId4", typeof(System.Int32));
                             
                dtProcedure.Columns.Add("Fee", typeof(System.Decimal));
                dtProcedure.Columns.Add("Unit", typeof(System.Int32));

                dtProcedure.AcceptChanges();

                QBProcedureDTO obj = null;
                //bool SkipRecord = false;

                for (int index = 0; index < InputList.Count; index++)
                {
                    obj = InputList[index];
                    
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
                    dRow["ModifiedBy"] = obj.ModifiedBy;                   
                    dRow["DictationID"] = obj.DictationID;
                    dRow["IsDeleted"] = obj.IsDeleted;
                    dRow["IncludeInBilling"] = obj.IncludeInBilling;
                    dRow["IsCreatedFromSuperBill"] = obj.IsCreatedFromSuperBill;
                    dRow["ModifierTypeId1"] = obj.ModifierTypeId1;
                    dRow["ModifierTypeId2"] = obj.ModifierTypeId2;
                    dRow["ModifierTypeId3"] = obj.ModifierTypeId3;
                    dRow["ModifierTypeId4"] = obj.ModifierTypeId4;
                    
                    dRow["Fee"] = obj.Fee;
                    dRow["Unit"] = obj.Unit;


                    dtProcedure.Rows.Add(dRow);
                    

                    dtProcedure.AcceptChanges();
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.PROCEDURE_TABLE_CONVERSION_CONVERSION_ERROR_MSG + ".ProcedureDTOListConverter", ProjectConstant.ERROR_CODE.ModuleNoteTableConversion.ToString());
            }
            return dtProcedure;
        }
        private static void SetDateIFNotNull(DataRow dRow, String PropertyName, DateTime DateValue)
        {            

            if (DateValue > ProjectConstant.DATE_MINIMUM)
            {
                dRow[PropertyName] = DateValue;
            }
            else
            {
                dRow[PropertyName] = DBNull.Value;
            }
        }

        internal static DataTable GeneralIdListConverter(List<int> GeneralIdList)
        {
            DataTable dtGeneralTable = null;
            try
            {
                dtGeneralTable = new DataTable();

                dtGeneralTable.Columns.Add("Id", typeof(System.Int32));

                dtGeneralTable.AcceptChanges();

                foreach (int obj in GeneralIdList)
                {
                    DataRow dRow = dtGeneralTable.NewRow();

                    dRow["Id"] = obj;

                    dtGeneralTable.Rows.Add(dRow);
                }
                dtGeneralTable.AcceptChanges();
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw new TableConversionException(ProjectConstant.USER_ID_DTO_CONVERSION_ERROR_MSG + ".UserIdListConverter", ProjectConstant.ERROR_CODE.UserManagerTableConversion.ToString());
            }
            return dtGeneralTable;
        }
    }
}
