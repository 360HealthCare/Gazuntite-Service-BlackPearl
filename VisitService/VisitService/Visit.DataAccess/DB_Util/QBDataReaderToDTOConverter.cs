using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.CustomException;
using QuivusUtility.ProjectConstants;
using QuivusModel.Billing.Visit;
using System.Data;
using Quivus.Utility;

namespace Visit.DataAccess.DB_Util
{
    public class QBDataReaderToDTOConverter
    {
        internal static List<QBProcedureDTO> ConvertProcedureDTOByVisitId(IDataReader objReader, bool IsTemplateVisit)
        {
            List<QBProcedureDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<QBProcedureDTO>();

                    while (objReader.Read())
                    {
                        QBProcedureDTO obj = new QBProcedureDTO();

                        obj.PatientProcedureID = GetKeyAsNullIfTemplate(objReader, "PatientProcedureID", obj.PatientProcedureID, IsTemplateVisit);
                       
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);

                        if (objReader["VisitDate"] != DBNull.Value && !IsTemplateVisit)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["CPTMedicalTypeID"] != DBNull.Value)
                        {
                            if (obj.CPTMedicalType == null)
                                obj.CPTMedicalType = new Quivus.Model.TypeDTOs.QuickInputTypeDTOs.CPTMedicalTypeDTO();

                            obj.CPTMedicalType.CPTMedicalTypeID = int.Parse(objReader["CPTMedicalTypeID"].ToString());

                            if (objReader["ProcedureCode"] != DBNull.Value)
                                obj.CPTMedicalType.ProcedureCode = objReader["ProcedureCode"].ToString();

                            if (objReader["ProcedureDescription"] != DBNull.Value)
                                obj.CPTMedicalType.ProcedureDescription = objReader["ProcedureDescription"].ToString();

                        }

                        //obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);
                                              

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        

                        //obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                       

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                       

                        //obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        
                        if (objReader["IncludeInBilling"] != DBNull.Value)
                        {
                            obj.IncludeInBilling = bool.Parse(objReader["IncludeInBilling"].ToString());
                        }

                        if (objReader["IsCreatedFromSuperBill"] != DBNull.Value)
                        {
                            obj.IsCreatedFromSuperBill = bool.Parse(objReader["IsCreatedFromSuperBill"].ToString());
                        }                        

                        if (objReader["Fee"] != DBNull.Value)
                        {
                            obj.Fee = decimal.Parse(objReader["Fee"].ToString());
                        }

                        if (objReader["Unit"] != DBNull.Value)
                        {
                            obj.Unit = int.Parse(objReader["Unit"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(BillingConstant.CLAIM_DTO_CONVERSION_ERROR_MSG + ".ConvertProcedureDTOByVisitId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientProcedureModifierDTO> ConvertModifierDTO(IDataReader objReader)
        {
            List<PatientProcedureModifierDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientProcedureModifierDTO>();

                    while (objReader.Read())
                    {
                        PatientProcedureModifierDTO obj = new PatientProcedureModifierDTO();

                        obj.ProcedureModifierID = GetKeyAsNullIfTemplate(objReader, "ProcedureModifierID", obj.ProcedureModifierID, false);
                        obj.PatientProcedureID = GetKeyAsNullIfTemplate(objReader, "PatientProcedureID", obj.PatientProcedureID, false);


                        if (objReader["ModifierTypeId"] != DBNull.Value)
                        {
                            if (obj.ProcedureModifierType == null)
                                obj.ProcedureModifierType = new ProcedureModifierTypeDTO();

                            obj.ProcedureModifierType.ModifierTypeId = int.Parse(objReader["ModifierTypeId"].ToString());

                            if (objReader["ModifierDescription"] != DBNull.Value)
                                obj.ProcedureModifierType.ModifierDescription = objReader["ModifierDescription"].ToString();                          

                        }                        

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(BillingConstant.CLAIM_DTO_CONVERSION_ERROR_MSG + ".ConvertProcedureDTOByVisitId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        protected static int GetKeyAsNullIfTemplate(IDataReader objIReader, String PropertyName, int Target, bool IsTemplateVisit)
        {
            Target = 0;
            if (!IsTemplateVisit)
            {
                if (objIReader[PropertyName] != DBNull.Value)
                {
                    Target = int.Parse(objIReader[PropertyName].ToString());
                }
            }
            return Target;
        }

        protected static Guid GetDictationGUIDAsNullIfTemplate(IDataReader objIReader, String PropertyName, Guid Target, bool IsTemplateVisit)
        {
            Target = Guid.Empty;

            if (!IsTemplateVisit)
            {
                if (objIReader[PropertyName] != DBNull.Value)
                {
                    Target = Guid.Parse(objIReader[PropertyName].ToString());
                }
            }
            return Target;
        }

        protected static DateTime GetDateAsNullIfTemplate(IDataReader objIReader, String PropertyName, DateTime Target, bool IsTemplateVisit)
        {
            Target = DateTime.MinValue;

            if (!IsTemplateVisit)
            {
                if (objIReader[PropertyName] != DBNull.Value)
                {
                    Target = DateTime.Parse(objIReader[PropertyName].ToString());
                }
            }
            return Target;
        }
    }
}
