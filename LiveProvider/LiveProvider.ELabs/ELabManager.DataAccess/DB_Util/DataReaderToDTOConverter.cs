using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Model.TypeDTOs.ELabs.QuickInput;
using LiveProvider.Model.TypeDTOs;
using LiveProvider.Model.QuickInput;
using System.Data;
using LiveProvider.Utility.Constant.ProjectConstant;
using LiveProvider.Utility.CustomExceptions;
using LiveProvider.Utility.Constant.Errors;
using LiveProvider.Model.Response.LabManagement;
using LiveProvider.Model.ELab;
using LiveProvider.Model.TypeDTOs.ELabs.LiveProvider;

namespace ELabManager.DataAccess.DB_Util
{
    public class DataReaderToDTOConverter
    {

        internal static List<LabDTO> PatientLabsByPatientIdConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<LabDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<LabDTO>();

                    while (objReader.Read())
                    {
                        LabDTO LabObj = new LabDTO();

                        LabObj.PatientLabID = GetKeyAsNullIfTemplate(objReader, "PatientLabID", LabObj.PatientLabID, IsTemplateVisit);
                        //if (objReader["PatientLabID"] != DBNull.Value)
                        //{
                        //    LabObj.PatientLabID = int.Parse(objReader["PatientLabID"].ToString());
                        //}

                        LabObj.VisitID = GetKeyAsNullIfTemplate(objReader, ELabContant.PROPERTY_VISIT_ID, LabObj.VisitID, IsTemplateVisit);

                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    LabObj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}
                        if (objReader["Description"] != DBNull.Value)
                        {
                            LabObj.Description = objReader["Description"].ToString();
                        }

                        if (objReader["LabTestPriorityTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestPriorityType == null)
                                LabObj.LabTestPriorityType = new LabTestPriorityTypeDTO();

                            LabObj.LabTestPriorityType.LabTestPriorityTypeID = int.Parse(objReader["LabTestPriorityTypeID"].ToString());

                            if (objReader["PriorityName"] != DBNull.Value)
                                LabObj.LabTestPriorityType.PriorityName = objReader["PriorityName"].ToString();
                        }

                        if (objReader["ICDCodeID"] != DBNull.Value)
                        {
                            if (LabObj.ICDCode == null)
                                LabObj.ICDCode = new ICDCodeDTO();

                            LabObj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeID"].ToString());

                            if (objReader["ICDCodeName"] != DBNull.Value)
                                LabObj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();

                            if (objReader["ICDCodeDescription"] != DBNull.Value)
                                LabObj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();
                        }

                        if (objReader["LabTestTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestType == null)
                                LabObj.LabTestType = new LabTestTypeDTO();

                            LabObj.LabTestType.LabTestTypeID = int.Parse(objReader["LabTestTypeID"].ToString());

                            //M DB changed
                            //  if (objReader["Code"] != DBNull.Value)
                            //      LabObj.LabTestType.Code = objReader["Code"].ToString();

                            if (objReader["TestCode"] != DBNull.Value)
                                LabObj.LabTestType.Code = objReader["TestCode"].ToString();

                            //M DB changed
                            //if (objReader["TestTypeName"] != DBNull.Value)
                            //  LabObj.LabTestType.TestTypeName = objReader["TestTypeName"].ToString();

                            if (objReader["TestTypeName"] != DBNull.Value)
                                LabObj.LabTestType.TestTypeName = objReader["TestTypeName"].ToString();
                        }


                        if (objReader["LabTestSpecimenTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestSpecimenType == null)
                                LabObj.LabTestSpecimenType = new LabTestSpecimenTypeDTO();

                            LabObj.LabTestSpecimenType.LabTestSpecimenTypeID = int.Parse(objReader["LabTestSpecimenTypeID"].ToString());

                            if (objReader["SpecimenName"] != DBNull.Value)
                                LabObj.LabTestSpecimenType.SpecimenName = objReader["SpecimenName"].ToString();
                        }

                        if (objReader["LabTestOrderMethodTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestOrderMethodType == null)
                                LabObj.LabTestOrderMethodType = new LabTestOrderMethodTypeDTO();

                            LabObj.LabTestOrderMethodType.LabTestOrderMethodTypeID = int.Parse(objReader["LabTestOrderMethodTypeID"].ToString());

                            if (objReader["OrderMethodTypeName"] != DBNull.Value)
                                LabObj.LabTestOrderMethodType.OrderMethodTypeName = objReader["OrderMethodTypeName"].ToString();
                        }
                        /*  if (objReader["LabTestNatureTypeID"] != DBNull.Value)
                          {
                              if (LabObj.LabTestNatureType == null)
                                  LabObj.LabTestNatureType = new LabNatureTypeDTO();

                              LabObj.LabTestNatureType.LabTestNatureTypeID = int.Parse(objReader["LabTestNatureTypeID"].ToString());

                              if (objReader["LabTestNatureName"] != DBNull.Value)
                                  LabObj.LabTestNatureType.LabTestNatureName = objReader["LabTestNatureName"].ToString();
                          }
                         */
                        if (objReader["LionicLabTestTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LIONICLabTestType == null)
                                LabObj.LIONICLabTestType = new LIONICLabTestTypeDTO();

                            LabObj.LIONICLabTestType.LionicLabTestTypeID = int.Parse(objReader["LionicLabTestTypeID"].ToString());

                            if (objReader["SHORTNAME"] != DBNull.Value)
                                LabObj.LIONICLabTestType.SHORTNAME = objReader["SHORTNAME"].ToString();

                            if (objReader["LOINC_NUM"] != DBNull.Value)
                                LabObj.LIONICLabTestType.LOINC_NUM = objReader["LOINC_NUM"].ToString();

                        }

                        if (objReader["LabTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabType == null)
                                LabObj.LabType = new LabTypeDTO();

                            LabObj.LabType.LabTypeID = int.Parse(objReader["LabTypeID"].ToString());

                            if (objReader["LabName"] != DBNull.Value)
                                LabObj.LabType.LabName = objReader["LabName"].ToString();
                        }

                        if (objReader["OrderDate"] != DBNull.Value)
                        {
                            LabObj.OrderDate = objReader["OrderDate"].ToString();
                        }
                        if (objReader["CollectionDate"] != DBNull.Value)
                        {
                            LabObj.CollectionDate = objReader["CollectionDate"].ToString();
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            LabObj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }


                        if (objReader["UserID"] != DBNull.Value)
                        {
                            LabObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }


                        if (objReader["IsFasting"] != DBNull.Value)
                        {
                            bool? IsFasting = null;
                            IsFasting = bool.Parse(objReader["IsFasting"].ToString());

                            if (IsFasting == null)
                                LabObj.IsFasting = ELabContant.SELECTION_NULL;
                            else if (IsFasting == true)
                                LabObj.IsFasting = ELabContant.SELECTION_TRUE;
                            else if (IsFasting == false)
                                LabObj.IsFasting = ELabContant.SELECTION_False;
                        }
                        else
                        {
                            LabObj.IsFasting = ELabContant.SELECTION_NULL;
                        }

                        if (objReader["Status"] != DBNull.Value)
                        {
                            LabObj.Status = objReader["Status"].ToString();
                        }
                        if (objReader["Onset"] != DBNull.Value)
                        {
                            LabObj.Onset = objReader["Onset"].ToString();
                        }
                        if (objReader["LabID"] != DBNull.Value)
                        {
                            LabObj.LabID = int.Parse(objReader["LabID"].ToString());
                        }
                        if (objReader["IsLabIn"] != DBNull.Value)
                        {
                            LabObj.IsLabIn = bool.Parse(objReader["IsLabIn"].ToString());
                        }
                        if (objReader["LabNote"] != DBNull.Value)
                        {
                            LabObj.LabNote = objReader["LabNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            LabObj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        LabObj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ELabContant.PROPERTY_DICTATION_ID, LabObj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    LabObj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        LabObj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ELabContant.PROPERTY_CREATED_BY, LabObj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        LabObj.CreatedAt = GetDateAsNullIfTemplate(objReader, ELabContant.PROPERTY_CREATED_AT, LabObj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        LabObj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ELabContant.PROPERTY_MODIFIED_BY, LabObj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    LabObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        LabObj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ELabContant.PROPERTY_MODIFIED_AT, LabObj.ModifiedAt, IsTemplateVisit);

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            LabObj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        HistoryList.Add(LabObj);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new DTOConversionException(ELabContant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientLabsConverter", ErrorCodes.DTO_CONVERSION_ERROR_PREFIX.ToString());
            }
            return HistoryList;
        }

        internal static List<LabDTO> PatientLabsConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<LabDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<LabDTO>();

                    while (objReader.Read())
                    {
                        LabDTO LabObj = new LabDTO();

                        LabObj.PatientLabID = GetKeyAsNullIfTemplate(objReader, "PatientLabID", LabObj.PatientLabID, IsTemplateVisit);
                        //if (objReader["PatientLabID"] != DBNull.Value)
                        //{
                        //    LabObj.PatientLabID = int.Parse(objReader["PatientLabID"].ToString());
                        //}

                        LabObj.VisitID = GetKeyAsNullIfTemplate(objReader, ELabContant.PROPERTY_VISIT_ID, LabObj.VisitID, IsTemplateVisit);

                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    LabObj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}
                        if (objReader["Description"] != DBNull.Value)
                        {
                            LabObj.Description = objReader["Description"].ToString();
                        }

                        if (objReader["LabTestPriorityTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestPriorityType == null)
                                LabObj.LabTestPriorityType = new LabTestPriorityTypeDTO();

                            LabObj.LabTestPriorityType.LabTestPriorityTypeID = int.Parse(objReader["LabTestPriorityTypeID"].ToString());

                            if (objReader["PriorityName"] != DBNull.Value)
                                LabObj.LabTestPriorityType.PriorityName = objReader["PriorityName"].ToString();
                        }

                        if (objReader["ICDCodeID"] != DBNull.Value)
                        {
                            if (LabObj.ICDCode == null)
                                LabObj.ICDCode = new ICDCodeDTO();

                            LabObj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeID"].ToString());

                            if (objReader["ICDCodeName"] != DBNull.Value)
                                LabObj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();

                            if (objReader["ICDCodeDescription"] != DBNull.Value)
                                LabObj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();
                        }

                        if (objReader["LabTestTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestType == null)
                                LabObj.LabTestType = new LabTestTypeDTO();

                            LabObj.LabTestType.LabTestTypeID = int.Parse(objReader["LabTestTypeID"].ToString());

                            //M DB changed
                            //  if (objReader["Code"] != DBNull.Value)
                            //      LabObj.LabTestType.Code = objReader["Code"].ToString();

                            if (objReader["TestCode"] != DBNull.Value)
                                LabObj.LabTestType.Code = objReader["TestCode"].ToString();

                            //M DB changed
                            //if (objReader["TestTypeName"] != DBNull.Value)
                            //  LabObj.LabTestType.TestTypeName = objReader["TestTypeName"].ToString();

                            if (objReader["TestName"] != DBNull.Value)
                                LabObj.LabTestType.TestTypeName = objReader["TestName"].ToString();
                        }


                        if (objReader["LabTestSpecimenTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestSpecimenType == null)
                                LabObj.LabTestSpecimenType = new LabTestSpecimenTypeDTO();

                            LabObj.LabTestSpecimenType.LabTestSpecimenTypeID = int.Parse(objReader["LabTestSpecimenTypeID"].ToString());

                            if (objReader["SpecimenName"] != DBNull.Value)
                                LabObj.LabTestSpecimenType.SpecimenName = objReader["SpecimenName"].ToString();
                        }

                        if (objReader["LabTestOrderMethodTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabTestOrderMethodType == null)
                                LabObj.LabTestOrderMethodType = new LabTestOrderMethodTypeDTO();

                            LabObj.LabTestOrderMethodType.LabTestOrderMethodTypeID = int.Parse(objReader["LabTestOrderMethodTypeID"].ToString());

                            if (objReader["OrderMethodTypeName"] != DBNull.Value)
                                LabObj.LabTestOrderMethodType.OrderMethodTypeName = objReader["OrderMethodTypeName"].ToString();
                        }
                        /*  if (objReader["LabTestNatureTypeID"] != DBNull.Value)
                          {
                              if (LabObj.LabTestNatureType == null)
                                  LabObj.LabTestNatureType = new LabNatureTypeDTO();

                              LabObj.LabTestNatureType.LabTestNatureTypeID = int.Parse(objReader["LabTestNatureTypeID"].ToString());

                              if (objReader["LabTestNatureName"] != DBNull.Value)
                                  LabObj.LabTestNatureType.LabTestNatureName = objReader["LabTestNatureName"].ToString();
                          }
                         */
                        if (objReader["LionicLabTestTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LIONICLabTestType == null)
                                LabObj.LIONICLabTestType = new LIONICLabTestTypeDTO();

                            LabObj.LIONICLabTestType.LionicLabTestTypeID = int.Parse(objReader["LionicLabTestTypeID"].ToString());

                            if (objReader["SHORTNAME"] != DBNull.Value)
                                LabObj.LIONICLabTestType.SHORTNAME = objReader["SHORTNAME"].ToString();

                            if (objReader["LOINC_NUM"] != DBNull.Value)
                                LabObj.LIONICLabTestType.LOINC_NUM = objReader["LOINC_NUM"].ToString();

                        }

                        if (objReader["LabTypeID"] != DBNull.Value)
                        {
                            if (LabObj.LabType == null)
                                LabObj.LabType = new LabTypeDTO();

                            LabObj.LabType.LabTypeID = int.Parse(objReader["LabTypeID"].ToString());

                            if (objReader["LabName"] != DBNull.Value)
                                LabObj.LabType.LabName = objReader["LabName"].ToString();
                        }

                        if (objReader["OrderDate"] != DBNull.Value)
                        {
                            LabObj.OrderDate = objReader["OrderDate"].ToString();
                        }
                        if (objReader["CollectionDate"] != DBNull.Value)
                        {
                            LabObj.CollectionDate = objReader["CollectionDate"].ToString();
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            LabObj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }


                        if (objReader["UserID"] != DBNull.Value)
                        {
                            LabObj.UserID = int.Parse(objReader["UserID"].ToString());
                        }


                        if (objReader["IsFasting"] != DBNull.Value)
                        {
                            bool? IsFasting = null;
                            IsFasting = bool.Parse(objReader["IsFasting"].ToString());

                            if (IsFasting == null)
                                LabObj.IsFasting = ELabContant.SELECTION_NULL;
                            else if (IsFasting == true)
                                LabObj.IsFasting = ELabContant.SELECTION_TRUE;
                            else if (IsFasting == false)
                                LabObj.IsFasting = ELabContant.SELECTION_False;
                        }
                        else
                        {
                            LabObj.IsFasting = ELabContant.SELECTION_NULL;
                        }

                        if (objReader["Status"] != DBNull.Value)
                        {
                            LabObj.Status = objReader["Status"].ToString();
                        }
                        if (objReader["Onset"] != DBNull.Value)
                        {
                            LabObj.Onset = objReader["Onset"].ToString();
                        }
                        if (objReader["LabID"] != DBNull.Value)
                        {
                            LabObj.LabID = int.Parse(objReader["LabID"].ToString());
                        }
                        if (objReader["IsLabIn"] != DBNull.Value)
                        {
                            LabObj.IsLabIn = bool.Parse(objReader["IsLabIn"].ToString());
                        }
                        if (objReader["LabNote"] != DBNull.Value)
                        {
                            LabObj.LabNote = objReader["LabNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            LabObj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        LabObj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ELabContant.PROPERTY_DICTATION_ID, LabObj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    LabObj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        LabObj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ELabContant.PROPERTY_CREATED_BY, LabObj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        LabObj.CreatedAt = GetDateAsNullIfTemplate(objReader, ELabContant.PROPERTY_CREATED_AT, LabObj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        LabObj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ELabContant.PROPERTY_MODIFIED_BY, LabObj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    LabObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        LabObj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ELabContant.PROPERTY_MODIFIED_AT, LabObj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    LabObj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        HistoryList.Add(LabObj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ELabContant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientFamilyHistoryConverter", ELabContant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }

        internal static List<FollowupLabsDTO> ConvertPatientFollowupLabs(IDataReader objReader)
        {
            List<FollowupLabsDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<FollowupLabsDTO>();

                    while (objReader.Read())
                    {
                        FollowupLabsDTO obj = new FollowupLabsDTO();

                        if (objReader["PatientLabID"] != DBNull.Value)
                        {
                            obj.PatientFollowUpLabID = int.Parse(objReader["PatientLabID"].ToString());
                        }
                        if (objReader["LabTestDescription"] != DBNull.Value)
                        {
                            obj.LabTestDescription = objReader["LabTestDescription"].ToString();
                        }
                        if (objReader["IsInHouseLab"] != DBNull.Value)
                        {
                            obj.IsInHouseLab = bool.Parse(objReader["IsInHouseLab"].ToString());
                        }
                        if (objReader["TestCode"] != DBNull.Value)
                        {
                            obj.TestCode = objReader["TestCode"].ToString();
                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ELabContant.FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG + ".ConvertPatientFollowupLabs", ELabContant.ERROR_CODE.FollowupLabsConversion.ToString());
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

        internal static LabPaggingResponseDTO ConvertLabOrderList(IDataReader objIReader)
        {
            LabPaggingResponseDTO response = null;
            try
            {
                if (objIReader != null && objIReader.Read())
                {
                    response = new LabPaggingResponseDTO();
                    response.FirstRecord = int.Parse(objIReader["FirstRecord"].ToString());
                    response.TotalRecords = int.Parse(objIReader["TotalRecords"].ToString());
                    response.LastRecord = int.Parse(objIReader["LastRecord"].ToString());
                    objIReader.NextResult();

                    while (objIReader.Read())
                    {
                        OrderManagementDTO obj = new OrderManagementDTO();

                        if (objIReader["LabOrderId"] != DBNull.Value)
                        {
                            obj.OrderID = int.Parse(objIReader["LabOrderId"].ToString());
                        }
                        if (objIReader["OrderNumber"] != DBNull.Value)
                        {
                            obj.OrderNumber = objIReader["OrderNumber"].ToString();
                        }
                        if (objIReader["OrderDate"] != DBNull.Value)
                        {
                            obj.OrderDate = objIReader["OrderDate"].ToString();
                        }
                        if (objIReader["PatientID"] != DBNull.Value)
                        {
                            if (obj.Patient == null)
                            {
                                obj.Patient = new LiveProvider.Model.User.Patient.PatientDTO();
                            }
                            obj.Patient.PatientId = int.Parse(objIReader["PatientID"].ToString());
                        }
                        if (objIReader["UserID"] != DBNull.Value)
                        {
                            if (obj.Provider == null)
                            {
                                obj.Provider = new LiveProvider.Model.User.Provider.ProviderDTO();
                            }
                            obj.Provider.ProviderID = int.Parse(objIReader["UserID"].ToString());
                        }
                        if (objIReader["TestTypeName"] != DBNull.Value)
                        {
                            if (obj.Test == null)
                            {
                                obj.Test = new LiveProvider.Model.Test.TestDTO();
                            }
                            obj.Test.Name = objIReader["TestTypeName"].ToString();
                            if (objIReader["TestTypeId"] != DBNull.Value)
                            {
                                obj.Test.TestId = int.Parse(objIReader["TestTypeId"].ToString());
                            }
                        }
                        if (objIReader["LabName"] != DBNull.Value)
                        {
                            obj.Laboratory = objIReader["LabName"].ToString();
                        }
                        if (objIReader["LabID"] != DBNull.Value)
                        {
                            obj.LaboratoryID = int.Parse(objIReader["LabID"].ToString());
                        }
                        if (response.LabList == null)
                        {
                            response.LabList = new List<OrderManagementDTO>();
                        }
                        response.LabList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ELabContant.FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG + ".ConvertPatientFollowupLabs", ELabContant.ERROR_CODE.FollowupLabsConversion.ToString());
            }
            return response;
        }

        internal static StaticDataResponse ConvertSearchFeildsList(IDataReader objIReader)
        {
            StaticDataResponse response = null;
            try
            {
                if (objIReader != null)
                {
                    response = new StaticDataResponse();
                    SearchFieldsResponse commonResponse = new SearchFieldsResponse();
                    commonResponse.Type="Common";
                    SearchFieldsResponse resultResponse = new SearchFieldsResponse();
                    resultResponse.Type="Result";
                    while (objIReader.Read())
                    {
                        SearchFieldsDTO obj = new SearchFieldsDTO();

                        if (objIReader["FieldId"] != DBNull.Value)
                        {
                            obj.FieldID = int.Parse(objIReader["FieldId"].ToString());
                        }
                        if (objIReader["FieldName"] != DBNull.Value)
                        {
                            obj.FieldName = objIReader["FieldName"].ToString();
                        }
                        if (objIReader["IsResultFeild"] != DBNull.Value)
                        {
                            obj.IsResult = bool.Parse(objIReader["IsResultFeild"].ToString());
                        }
                        if (objIReader["IsOrderField"] != DBNull.Value)
                        {
                            obj.IsOrder = bool.Parse(objIReader["IsOrderField"].ToString());
                        }
                        if (obj.IsResult && !obj.IsOrder)
                        {
                            if (resultResponse.Feilds == null)
                            {
                                resultResponse.Feilds = new List<SearchFieldsDTO>();
                            }
                            resultResponse.Feilds.Add(obj);
                        }
                        else
                        {
                            if (commonResponse.Feilds == null)
                            {
                                commonResponse.Feilds = new List<SearchFieldsDTO>();
                            }
                            commonResponse.Feilds.Add(obj);
                        }
                    }
                    if (response.Fields == null)
                    {
                        response.Fields = new List<SearchFieldsResponse>();
                    }
                    response.Fields.Add(commonResponse);
                    response.Fields.Add(resultResponse);
                }
            }
            catch
            {
                throw new DTOConversionException(ELabContant.FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG + ".ConvertPatientFollowupLabs", ELabContant.ERROR_CODE.FollowupLabsConversion.ToString());
            }
            return response;
        }

        internal static ResultPagingResponse ConvertLabResultList(IDataReader objIReader)
        {
            ResultPagingResponse response = null;
            try
            {
                if (objIReader != null && objIReader.Read())
                {
                    response = new ResultPagingResponse();
                    response.FirstRecord = int.Parse(objIReader["FirstRecord"].ToString());
                    response.TotalRecords = int.Parse(objIReader["TotalRecords"].ToString());
                    response.LastRecord = int.Parse(objIReader["LastRecord"].ToString());
                    objIReader.NextResult();

                    while (objIReader.Read())
                    {
                        OrderManagementDTO obj = new OrderManagementDTO();

                        if (objIReader["OrderNumber"] != DBNull.Value)
                        {
                            obj.OrderNumber = objIReader["OrderNumber"].ToString();
                        }
                        if (objIReader["OrderDate"] != DBNull.Value)
                        {
                            obj.OrderDate = objIReader["OrderDate"].ToString();
                        }
                        if (objIReader["PatientID"] != DBNull.Value)
                        {
                            if (obj.Patient == null)
                            {
                                obj.Patient = new LiveProvider.Model.User.Patient.PatientDTO();
                            }
                            obj.Patient.PatientId = int.Parse(objIReader["PatientID"].ToString());
                        }
                        if (objIReader["UserID"] != DBNull.Value)
                        {
                            if (obj.Provider == null)
                            {
                                obj.Provider = new LiveProvider.Model.User.Provider.ProviderDTO();
                            }
                            obj.Provider.ProviderID = int.Parse(objIReader["UserID"].ToString());
                        }
                        if (objIReader["TestTypeName"] != DBNull.Value || objIReader["SHORTNAME"]!=DBNull.Value)
                        {
                            if (obj.Test == null)
                            {
                                obj.Test = new LiveProvider.Model.Test.TestDTO();
                            }
                            obj.Test.Name = objIReader["TestTypeName"].ToString();
                            obj.Test.Type = objIReader["SHORTNAME"].ToString();

                        }
                        if (objIReader["LabName"] != DBNull.Value)
                        {
                            obj.Laboratory = objIReader["LabName"].ToString();
                        }
                        if (objIReader["Status"] != DBNull.Value || objIReader["Comments"] != DBNull.Value || objIReader["ResultFileAddress"] != DBNull.Value || objIReader["NormalRange"] != DBNull.Value)
                        {
                            if (obj.Result == null)
                            {
                                obj.Result = new ResultManagementDTO();
                            }
                            obj.Result.Status = objIReader["Status"].ToString();
                            obj.Result.ResultFile = objIReader["Comments"].ToString();
                            obj.Result.Comments = objIReader["ResultFileAddress"].ToString();
                            obj.Result.Ranage = objIReader["NormalRange"].ToString();
                        }
                        if (response.LabList == null)
                        {
                            response.LabList = new List<OrderManagementDTO>();
                        }
                        response.LabList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ELabContant.FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG + ".ConvertPatientFollowupLabs", ELabContant.ERROR_CODE.FollowupLabsConversion.ToString());
            }
            return response;
        }
    }
}
