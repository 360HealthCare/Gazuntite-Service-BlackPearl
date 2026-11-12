using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Quivus.Utility.Logger;
using Quivus.Model.Search;
using Quivus.Utility.CustomException;
using Quivus.Utility;

using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput;
using Quivus.Model.TypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs;
using Quivus.Model.QuickInput.ROS;

using Quivus.Model.QuickInput.ComprehensiveHistory.Social;

using Quivus.Model.QuickInput.HPI;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
//using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.Visit;
using Quivus.Model.Clinic;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Dictation;
using Quivus.Model.QuickInput.Notes;
using QuivusModel.QuickInput.ComprehensiveHistory.Social;
using QuivusUtility.ProjectConstants;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;


namespace Visit.DataAccess.DB_Util
{
    /// <summary>
    /// Auhtor Mansoor
    /// Date 18 Feb 2011
    /// this class used to convert the Reader to Visit specific DTOs
    /// </summary>

    public class DataReaderToDTOConverter
    {
        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// this method convert the Reader to PatientEncounterDTO    
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<PatientEncounterDTO> PatientEncounterConverter(IDataReader objReader)
        {
            List<PatientEncounterDTO> patientEncounterList = null;
            try
            {
                if (objReader != null)
                {
                    patientEncounterList = new List<PatientEncounterDTO>();
                    {
                        while (objReader.Read())
                        {
                            PatientEncounterDTO objPatientEncounter = new PatientEncounterDTO();

                            objPatientEncounter.VisitId = int.Parse(objReader["VisitID"].ToString());

                            objPatientEncounter.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());

                            objPatientEncounter.VisitEncounterTypeName = objReader["VisitEncounterTypeName"].ToString();

                            objPatientEncounter.VisitStatus = objReader["VisitStatus"].ToString();

                            if (objReader["StationID"] != DBNull.Value)
                            {
                                objPatientEncounter.StationID = int.Parse(objReader["StationID"].ToString());

                            }



                            if (objReader["OwnerUserId"] != DBNull.Value)
                            {
                                objPatientEncounter.OwnerUserId = int.Parse(objReader["OwnerUserId"].ToString());
                            }

                            if (objReader["OwnerUserTypeId"] != DBNull.Value)
                            {
                                objPatientEncounter.OwnerUserTypeId = int.Parse(objReader["OwnerUserTypeId"].ToString());
                            }

                            objPatientEncounter.VisitProvider = "Provider Not Avaliable";

                            if (objReader["DictationID"] != DBNull.Value)
                            {
                                objPatientEncounter.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                            }

                            if (objReader["CanSignCosign"] != DBNull.Value)
                            {
                                objPatientEncounter.CanSignCosign = objReader["CanSignCosign"].ToString();
                            }

                            if (objReader["Notes"] != DBNull.Value)
                            {
                                objPatientEncounter.Notes = objReader["Notes"].ToString();
                            }
                            if (objReader["NoteSubject"] != DBNull.Value)
                            {
                                objPatientEncounter.NoteSubject = objReader["NoteSubject"].ToString();
                            }

                            patientEncounterList.Add(objPatientEncounter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.VISIT_DTO_CONVERSION_ERROR_MSG + ".PatientEncounterConverter", ProjectConstant.ERROR_CODE.VisitDTOConversion.ToString());
            }
            return patientEncounterList;
        }

        public static List<ImmunizationDTO> ImmunizationConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<ImmunizationDTO> patientImmunizationList = null;
            try
            {
                if (objReader != null)
                {
                    patientImmunizationList = new List<ImmunizationDTO>();
                    {
                        while (objReader.Read())
                        {
                            ImmunizationDTO objImmunization = new ImmunizationDTO();

                            objImmunization.PatientImmunizationId = GetKeyAsNullIfTemplate(objReader, "PatientImmunizationID", objImmunization.PatientImmunizationId, IsTemplateVisit);
                            //objImmunization.PatientImmunizationId = int.Parse(objReader["PatientImmunizationID"].ToString());

                            objImmunization.ImmunizationType = new ImmunizationTypeDTO();
                            objImmunization.ImmunizationType.ImmunizationTypeId = int.Parse(objReader["ImmunizationTypeID"].ToString());

                            objImmunization.VisitId = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, objImmunization.VisitId, IsTemplateVisit);

                            //  objImmunization.VisitId = int.Parse(objReader["VisitID"].ToString());

                            if (objReader["ImmunizationTypeName"] != DBNull.Value)
                            {
                                objImmunization.ImmunizationType.ImmunizationTypeName = objReader["ImmunizationTypeName"].ToString();
                            }

                            if (objReader["ImmunizationNote"] != DBNull.Value)
                            {
                                objImmunization.Note = objReader["ImmunizationNote"].ToString();
                            }

                            if (objReader["NoteSubject"] != DBNull.Value)
                            {
                                objImmunization.NoteSubject = objReader["NoteSubject"].ToString();
                            }

                            if (objReader["Dose"] != DBNull.Value)
                            {
                                objImmunization.Dose = objReader["Dose"].ToString();
                            }

                            if (objReader["LotNumber"] != DBNull.Value)
                            {
                                objImmunization.LotNumber = objReader["LotNumber"].ToString();
                            }

                            if (objReader["DateAdmin"] != DBNull.Value)
                            {
                                objImmunization.DateAdmin = objReader["DateAdmin"].ToString();
                            }

                            if (objReader["NextDoseDue"] != DBNull.Value)
                            {
                                objImmunization.NextDoseDue = objReader["NextDoseDue"].ToString();
                            }

                            if (objReader["IsRefusal"] != DBNull.Value)
                            {
                                objImmunization.IsRefusal = bool.Parse(objReader["IsRefusal"].ToString());
                            }

                            if (objReader["RefusalReason"] != DBNull.Value)
                            {
                                objImmunization.RefusalReason = objReader["RefusalReason"].ToString();
                            }

                            if (objReader["Site"] != DBNull.Value)
                            {
                                objImmunization.Site = objReader["Site"].ToString();
                            }

                            if (objReader["Series"] != DBNull.Value)
                            {
                                objImmunization.Series = objReader["Series"].ToString();
                            }

                            if (objReader["Manufacturer"] != DBNull.Value)
                            {
                                objImmunization.Manufacturer = objReader["Manufacturer"].ToString();
                            }

                            if (objReader["Route"] != DBNull.Value)
                            {
                                objImmunization.Rout = objReader["Route"].ToString();
                            }

                            //if (objReader["VisitDate"] != DBNull.Value)
                            //{
                            //    objImmunization.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                            //}

                            objImmunization.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, objImmunization.DictationID, IsTemplateVisit);
                            //if (objReader["DictationID"] != DBNull.Value)
                            //{
                            //    objImmunization.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                            //}



                            patientImmunizationList.Add(objImmunization);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.VISIT_DTO_CONVERSION_ERROR_MSG + ".ImmunizationConverter", ProjectConstant.ERROR_CODE.VisitDTOConversion.ToString());
            }

            return patientImmunizationList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Convert Medical History Info TO DTO
        /// </summary>
        /// <returns></returns>
        internal static List<MedicalHistoryTypeDTO> MedicalHistoryConverter(IDataReader objReader)
        {
            List<MedicalHistoryTypeDTO> MedicalHitoryList = null;
            try
            {
                if (objReader != null)
                {
                    MedicalHitoryList = new List<MedicalHistoryTypeDTO>();

                    while (objReader.Read())
                    {
                        MedicalHistoryTypeDTO objMedicalHitory = new MedicalHistoryTypeDTO();

                        if (objReader["MedicalHistoryTypeID"] != DBNull.Value)
                        {
                            objMedicalHitory.MedicalHistoryTypeID = int.Parse(objReader["MedicalHistoryTypeID"].ToString());

                        }
                        if (objReader["MedicalHistoryTypeName"] != DBNull.Value)
                        {
                            objMedicalHitory.MedicalHistoryTypeName = objReader["MedicalHistoryTypeName"].ToString();
                        }
                        MedicalHitoryList.Add(objMedicalHitory);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.MEDICAL_HISTORY_TYPES_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                  ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
                //   throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".MedicalHistoryConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return MedicalHitoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 12 Mar 2011
        /// Method Will be used to Convert Pateint Medical Info
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<MedicalHistoryDTO> PatientMedicalHistoryConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<MedicalHistoryDTO> MedicalHitoryList = null;
            try
            {
                if (objReader != null)
                {
                    MedicalHitoryList = new List<MedicalHistoryDTO>();

                    while (objReader.Read())
                    {
                        MedicalHistoryDTO objMedicalHitory = new MedicalHistoryDTO();

                        objMedicalHitory.PatientMedicalHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientMedicalHistoryID", objMedicalHitory.PatientMedicalHistoryID, IsTemplateVisit);
                        //if (objReader["PatientMedicalHistoryID"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.PatientMedicalHistoryID = int.Parse(objReader["PatientMedicalHistoryID"].ToString());
                        //}
                        if (objReader["MedicalHistoryTypeID"] != DBNull.Value)
                        {
                            objMedicalHitory.MedicalHistoryTypeID = int.Parse(objReader["MedicalHistoryTypeID"].ToString());
                        }

                        objMedicalHitory.VisitID = GetKeyAsNullIfTemplate(objReader, "VisitID", objMedicalHitory.VisitID, IsTemplateVisit);

                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}
                        if (objReader["MedicalHistoryNote"] != DBNull.Value)
                        {
                            objMedicalHitory.MedicalHistoryNote = objReader["MedicalHistoryNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            objMedicalHitory.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["PatientMedicalHistoryDescription"] != DBNull.Value)
                        {
                            objMedicalHitory.PatientMedicalHistoryDescription = objReader["PatientMedicalHistoryDescription"].ToString();
                        }
                        if (objReader["IsNone"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNone"].ToString());
                            if (result)
                            {
                                objMedicalHitory.IsNone = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                objMedicalHitory.IsNone = ProjectConstant.SELECTION_False;
                            }
                        }

                        objMedicalHitory.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, objMedicalHitory.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        objMedicalHitory.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, objMedicalHitory.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        objMedicalHitory.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, objMedicalHitory.CreatedAt, IsTemplateVisit);

                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        objMedicalHitory.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, objMedicalHitory.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        objMedicalHitory.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, objMedicalHitory.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    objMedicalHitory.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        MedicalHitoryList.Add(objMedicalHitory);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".PatientMedicalHistoryConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return MedicalHitoryList;
        }

        internal static List<FamilyHistoryTypeDTO> FamilyHistoryTypeConverter(IDataReader objReader)
        {
            List<FamilyHistoryTypeDTO> HitoryList = null;
            try
            {
                if (objReader != null)
                {
                    HitoryList = new List<FamilyHistoryTypeDTO>();

                    while (objReader.Read())
                    {
                        FamilyHistoryTypeDTO objHistory = new FamilyHistoryTypeDTO();

                        if (objReader["FamilyHistoryTypeID"] != DBNull.Value)
                        {
                            objHistory.FamilyHistoryTypeID = int.Parse(objReader["FamilyHistoryTypeID"].ToString());
                        }
                        if (objReader["FamilyHistoryTypeName"] != DBNull.Value)
                        {
                            objHistory.FamilyHistoryTypeName = objReader["FamilyHistoryTypeName"].ToString();
                        }
                        if (objReader["Description"] != DBNull.Value)
                        {
                            objHistory.Description = objReader["Description"].ToString();
                        }
                        /*    if (objReader["PresentICDCode"] != DBNull.Value)
                            {
                                objHistory.PresentICDCode = objReader["PresentICDCode"].ToString();
                            }
                            if (objReader["PresentICDCodeDescription"] != DBNull.Value)
                            {
                                objHistory.PresentICDCodeDescription = objReader["PresentICDCodeDescription"].ToString();
                            }
                            if (objReader["PastICDCode"] != DBNull.Value)
                            {
                                objHistory.PastICDCode = objReader["PastICDCode"].ToString();
                            }
                            if (objReader["PastICDCodeDescription"] != DBNull.Value)
                            {
                                objHistory.PastICDCodeDescription = objReader["PastICDCodeDescription"].ToString();
                            }
                          */
                        HitoryList.Add(objHistory);
                    }
                }
            }
            catch
            {
                //  throw new DTOConversionException(ProjectConstant.FAMILY_HISTORY_DTO_CONVERSION_ERROR_MSG + ".FamilyHistoryTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
                throw new DTOConversionException(ProjectConstant.FAMILY_HISTORY_DTO_CONVERSION_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.FAMILY_HISTORY_TYPES_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                    ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return HitoryList;
        }

        internal static List<DiseaseTypeDTO> DiseaseTypeConverter(IDataReader objReader)
        {
            List<DiseaseTypeDTO> DiseaseList = null;
            try
            {
                if (objReader != null)
                {
                    DiseaseList = new List<DiseaseTypeDTO>();

                    while (objReader.Read())
                    {
                        DiseaseTypeDTO objDisease = new DiseaseTypeDTO();

                        if (objReader["DiseaseTypeID"] != DBNull.Value)
                        {
                            objDisease.DiseaseTypeID = int.Parse(objReader["DiseaseTypeID"].ToString());
                        }
                        if (objReader["DiseaseTypeName"] != DBNull.Value)
                        {
                            objDisease.DiseaseTypeName = objReader["DiseaseTypeName"].ToString();
                        }
                        if (objReader["DiseaseTypeCode"] != DBNull.Value)
                        {
                            objDisease.DiseaseTypeCode = objReader["DiseaseTypeCode"].ToString();
                        }
                        if (objReader["Description"] != DBNull.Value)
                        {
                            objDisease.Description = objReader["Description"].ToString();
                        }
                        if (objReader["IsSystemSpecified"] != DBNull.Value)
                        {
                            objDisease.IsSystemSpecified = bool.Parse(objReader["IsSystemSpecified"].ToString());
                        }
                        if (objReader["FamilyHistoryTypeId"] != DBNull.Value)
                        {
                            objDisease.FamilyHistoryTypeId = int.Parse(objReader["FamilyHistoryTypeId"].ToString());
                        }

                        /*     if (objReader["UserTypeID"] != DBNull.Value)
                            {
                                objDisease.UserTypeID = int.Parse(objReader["UserTypeID"].ToString());
                            }
                            if (objReader["ClinicBranchID"] != DBNull.Value)
                            {
                                objDisease.ClinicBranchID = int.Parse(objReader["ClinicBranchID"].ToString());
                            }

                           if (objReader["CreatedBy"] != DBNull.Value)
                            {
                                objDisease.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                            }
                            if (objReader["CreatedAt"] != DBNull.Value)
                            {
                                objDisease.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                            }

                            if (objReader["ModifiedBy"] != DBNull.Value)
                            {
                                objDisease.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                            }
                            if (objReader["ModifiedAt"] != DBNull.Value)
                            {
                                objDisease.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                            }

                            if (objReader["IsDeleted"] != DBNull.Value)
                            {
                                objDisease.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                            }
                            */
                        DiseaseList.Add(objDisease);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".DiseaseTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return DiseaseList;
        }

        internal static List<FamilyHistoryDTO> PatientFamilyHistoryConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<FamilyHistoryDTO> HitoryList = null;
            try
            {
                if (objReader != null)
                {
                    HitoryList = new List<FamilyHistoryDTO>();

                    while (objReader.Read())
                    {
                        FamilyHistoryDTO objHistory = new FamilyHistoryDTO();

                        objHistory.PatientFamilyHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientFamilyHistoryID", objHistory.PatientFamilyHistoryID, IsTemplateVisit);
                        //if (objReader["PatientFamilyHistoryID"] != DBNull.Value)
                        //{
                        //    objHistory.PatientFamilyHistoryID = int.Parse(objReader["PatientFamilyHistoryID"].ToString());
                        //}
                        if (objReader["FamilyHistoryNote"] != DBNull.Value)
                        {
                            objHistory.FamilyHistoryNote = objReader["FamilyHistoryNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            objHistory.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["FamilyHistoryTypeID"] != DBNull.Value)
                        {
                            objHistory.FamilyHistoryTypeID = int.Parse(objReader["FamilyHistoryTypeID"].ToString());
                        }

                        objHistory.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, objHistory.VisitID, IsTemplateVisit);


                        //if (objReader["FamilyHistoryTypeName"] != DBNull.Value)
                        //{
                        //    objHistory.FamilyHistoryTypeName = objReader["FamilyHistoryTypeName"].ToString();
                        //}

                        //if (objReader["DiseaseCode"] != DBNull.Value)
                        //{
                        //    objHistory.DiseaseCode = objReader["DiseaseCode"].ToString();
                        //}
                        //if (objReader["DiseaseName"] != DBNull.Value)
                        //{
                        //    objHistory.DiseaseName = objReader["DiseaseName"].ToString();
                        //}


                        //if (objReader["DiseaseID"] != DBNull.Value)
                        //{
                        //    objHistory.DiseaseID = int.Parse(objReader["DiseaseID"].ToString());
                        //}


                        objHistory.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, objHistory.DictationID, IsTemplateVisit);


                        if (objReader["IsPresent"] != DBNull.Value)
                        {
                            objHistory.IsPresent = bool.Parse(objReader["IsPresent"].ToString());
                        }

                        if (objReader["ReletionshipTypeID"] != DBNull.Value)
                        {
                            objHistory.ReletionshipTypeID = int.Parse(objReader["ReletionshipTypeID"].ToString());
                        }


                        if (objReader["IsYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsYesSelected"].ToString());
                            if (result)
                            {
                                objHistory.IsYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                objHistory.IsYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }

                        objHistory.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, objHistory.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    objHistory.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        objHistory.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, objHistory.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    objHistory.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        objHistory.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, objHistory.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    objHistory.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        objHistory.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, objHistory.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    objHistory.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        HitoryList.Add(objHistory);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_FAMILY_HISTORY_DTO_CONVERSION_ERROR_MSG + ".PatientFamilyHistoryConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HitoryList;
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

                        LabObj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, LabObj.VisitID, IsTemplateVisit);

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
                                LabObj.IsFasting = ProjectConstant.SELECTION_NULL;
                            else if (IsFasting == true)
                                LabObj.IsFasting = ProjectConstant.SELECTION_TRUE;
                            else if (IsFasting == false)
                                LabObj.IsFasting = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            LabObj.IsFasting = ProjectConstant.SELECTION_NULL;
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

                        LabObj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, LabObj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    LabObj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        LabObj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, LabObj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        LabObj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, LabObj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        LabObj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, LabObj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    LabObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        LabObj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, LabObj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    LabObj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        HistoryList.Add(LabObj);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientLabsConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }

        internal static List<ROSDTO> PatientROSConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<ROSDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<ROSDTO>();

                    while (objReader.Read())
                    {
                        ROSDTO Obj = new ROSDTO();

                        Obj.PatientROSID = GetKeyAsNullIfTemplate(objReader, "PatientROSID", Obj.PatientROSID, IsTemplateVisit);

                        //if (objReader["PatientROSID"] != DBNull.Value)
                        //{
                        //    Obj.PatientROSID = int.Parse(objReader["PatientROSID"].ToString());
                        //}
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["ROSMainCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.ROSMainCategoryTypeID = int.Parse(objReader["ROSMainCategoryTypeID"].ToString());
                        }

                        Obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, Obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}
                        if (objReader["MainCategoryName"] != DBNull.Value)
                        {
                            Obj.MainCategoryName = objReader["MainCategoryName"].ToString();
                        }
                        if (objReader["ROSSubCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.ROSSubCategoryTypeID = int.Parse(objReader["ROSSubCategoryTypeID"].ToString());
                        }
                        if (objReader["SubCategoryName"] != DBNull.Value)
                        {
                            Obj.SubCategoryName = objReader["SubCategoryName"].ToString();
                        }

                        if (objReader["ROSSubLevel3CategoryTypeID"] != DBNull.Value)
                        {
                            Obj.ROSSubLevel3CategoryTypeID = int.Parse(objReader["ROSSubLevel3CategoryTypeID"].ToString());
                        }
                        if (objReader["ROSSubLevel3CategoryName"] != DBNull.Value)
                        {
                            Obj.ROSSubLevel3CategoryName = objReader["ROSSubLevel3CategoryName"].ToString();
                        }



                        if (objReader["Notes"] != DBNull.Value)
                        {
                            Obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["IsNormal"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNormal"].ToString());
                            if (result)
                                Obj.IsNormal = ProjectConstant.SELECTION_TRUE;
                            else
                                Obj.IsNormal = ProjectConstant.SELECTION_False;
                        }

                        Obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, Obj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        Obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, Obj.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        Obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, Obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        Obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, Obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        Obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, Obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientROSConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }


        internal static List<SocialHistoryAddictionDTO> PatientSocialHistoryAddictionConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<SocialHistoryAddictionDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<SocialHistoryAddictionDTO>();

                    while (objReader.Read())
                    {
                        SocialHistoryAddictionDTO Obj = new SocialHistoryAddictionDTO();

                        Obj.SocialHistoryAddictionID = GetKeyAsNullIfTemplate(objReader, "SocialHistoryAddictionID", Obj.SocialHistoryAddictionID, IsTemplateVisit);

                        //if (objReader["SocialHistoryAddictionID"] != DBNull.Value)
                        //{
                        //    Obj.SocialHistoryAddictionID = int.Parse(objReader["SocialHistoryAddictionID"].ToString());
                        //}
                        if (objReader["IsPresentSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsPresentSelected"].ToString());
                            if (result)
                            {
                                Obj.IsPresentSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                Obj.IsPresentSelected = ProjectConstant.SELECTION_False;
                            }

                        }
                        if (objReader["IsEducated"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsEducated"].ToString());
                            if (result)
                            {
                                Obj.IsEducated = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                Obj.IsEducated = ProjectConstant.SELECTION_False;
                            }
                            // Obj.IsEducated = bool.Parse(objReader["IsEducated"].ToString());
                        }
                        if (objReader["IsUnknown"] != DBNull.Value)
                        {
                            Obj.IsUnknown = bool.Parse(objReader["IsUnknown"].ToString());
                        }
                        if (objReader["IsYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsYesSelected"].ToString());
                            if (result)
                            {
                                Obj.IsYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                Obj.IsYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }

                        if (objReader["IsNoSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNoSelected"].ToString());
                            if (result)
                            {
                                Obj.IsNoSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                Obj.IsNoSelected = ProjectConstant.SELECTION_False;
                            }
                        }

                        //if (objReader["OnSet"] != DBNull.Value)
                        //{
                        //    Obj.OnSet = DateTime.Parse(objReader["OnSet"].ToString());
                        //}
                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            Obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["EndDate"] != DBNull.Value)
                        {
                            Obj.EndDate = objReader["EndDate"].ToString();
                        }

                        Obj.PatientSocialHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientSocialHistoryID", Obj.PatientSocialHistoryID, IsTemplateVisit);
                        //if (objReader["PatientSocialHistoryID"] != DBNull.Value)
                        //{
                        //    Obj.PatientSocialHistoryID = int.Parse(objReader["PatientSocialHistoryID"].ToString());
                        //}

                        if (objReader["SocialHistoryAddictionTypeID"] != DBNull.Value)
                        {
                            Obj.SocialHistoryAddictionTypeID = int.Parse(objReader["SocialHistoryAddictionTypeID"].ToString());
                        }

                        if (objReader["AddictionNotes"] != DBNull.Value)
                        {
                            Obj.AddictionNotes = objReader["AddictionNotes"].ToString();
                        }

                        if (objReader["UsageAmountID"] != DBNull.Value)
                        {
                            Obj.UsageAmountID = int.Parse(objReader["UsageAmountID"].ToString());
                        }
                        if (objReader["MediumTypeID"] != DBNull.Value)
                        {
                            Obj.MediumTypeID = int.Parse(objReader["MediumTypeID"].ToString());
                        }

                        Obj.DictationId = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, Obj.DictationId, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    Obj.DictationId = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        Obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, Obj.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        Obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, Obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        Obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, Obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        Obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, Obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientSocialHistoryAddictionConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011        
        /// </summary>
        /// <returns></returns>
        public static List<HPI_CCDTO> PatientHPIConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<HPI_CCDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<HPI_CCDTO>();

                    while (objReader.Read())
                    {
                        HPI_CCDTO Obj = new HPI_CCDTO();

                        Obj.PatientHPIID = GetKeyAsNullIfTemplate(objReader, "PatientHPIID", Obj.PatientHPIID, IsTemplateVisit);
                        //if (objReader["PatientHPIID"] != DBNull.Value)
                        //{
                        //    Obj.PatientHPIID = int.Parse(objReader["PatientHPIID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        Obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, Obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["HPINote"] != DBNull.Value)
                        {
                            Obj.Note = objReader["HPINote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        Obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, Obj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        if (objReader["ChiefComplaint"] != DBNull.Value)
                        {
                            Obj.ChiefComplaint = objReader["ChiefComplaint"].ToString();
                        }



                        if (objReader["Symptom"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Symptom = objReader["Symptom"].ToString();
                        }

                        if (objReader["Duration"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Duration = objReader["Duration"].ToString();
                        }

                        if (objReader["Location"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Location = objReader["Location"].ToString();
                        }

                        if (objReader["Quality"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Quality = objReader["Quality"].ToString();
                        }

                        if (objReader["Severity"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Severity = objReader["Severity"].ToString();
                        }

                        if (objReader["Context"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Context = objReader["Context"].ToString();
                        }

                        if (objReader["ModifyingFactors"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.ModifyingFactors = objReader["ModifyingFactors"].ToString();
                        }

                        if (objReader["Exacerbating"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Exacerbating = objReader["Exacerbating"].ToString();
                        }

                        if (objReader["Progression"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Progression = objReader["Progression"].ToString();
                        }

                        if (objReader["Radiation"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Radiation = objReader["Radiation"].ToString();
                        }

                        if (objReader["Onset"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }

                            Obj.HPIFields.Onset = objReader["Onset"].ToString();

                            // Obj.HPIFields.Onset = GetDateAsNullIfTemplate(objReader, "Onset", Obj.HPIFields.Onset, IsTemplateVisit);
                            //  Obj.HPIFields.Onset = DateTime.Parse(objReader["Onset"].ToString());
                        }

                        Obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, Obj.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        Obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, Obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        Obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, Obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        Obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, Obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_HPI_DTO_CONVERSION_ERROR_MSG + ".PatientHPIConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }


        internal static List<ROSMainCategoryTypeDTO> ROSMainTypeConverter(IDataReader objReader)
        {
            List<ROSMainCategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ROSMainCategoryTypeDTO>();

                    while (objReader.Read())
                    {
                        ROSMainCategoryTypeDTO obj = new ROSMainCategoryTypeDTO();

                        if (objReader["ROSMainCategoryTypeID"] != DBNull.Value)
                        {
                            obj.ROSMainCategoryTypeID = int.Parse(objReader["ROSMainCategoryTypeID"].ToString());
                        }
                        if (objReader["MainCategoryName"] != DBNull.Value)
                        {
                            obj.MainCategoryName = objReader["MainCategoryName"].ToString();
                            obj.label = objReader["MainCategoryName"].ToString();
                        }

                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }
                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".ROSMainTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<SocialHistoryDTO> PatientSocialHistoryConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<SocialHistoryDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<SocialHistoryDTO>();

                    while (objReader.Read())
                    {
                        SocialHistoryDTO obj = new SocialHistoryDTO();

                        //obj.PatientSocialHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientSocialHistoryID", obj.PatientSocialHistoryID, IsTemplateVisit);
                        if (objReader["PatientSocialHistoryID"] != DBNull.Value)
                        {
                            obj.PatientSocialHistoryID = int.Parse(objReader["PatientSocialHistoryID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        if (objReader["IsWorkStatusYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsWorkStatusYesSelected"].ToString());
                            if (result)
                            {
                                obj.IsWorkStatusYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                obj.IsWorkStatusYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }


                        if (objReader["WorkStatusNotes"] != DBNull.Value)
                        {
                            obj.WorkStatusNotes = objReader["WorkStatusNotes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["MaritalStatusID"] != DBNull.Value)
                        {
                            obj.MaritalStatusID = int.Parse(objReader["MaritalStatusID"].ToString());
                        }

                        if (objReader["IsOtherYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsOtherYesSelected"].ToString());
                            if (result)
                            {
                                obj.IsOtherYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                obj.IsOtherYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }

                        if (objReader["OtherDictationID"] != DBNull.Value)
                        {
                            obj.OtherDictationID = Guid.Parse(objReader["OtherDictationID"].ToString());
                        }
                        if (objReader["WorkStatusDictationID"] != DBNull.Value)
                        {
                            obj.WorkStatusDictationID = Guid.Parse(objReader["WorkStatusDictationID"].ToString());
                        }

                        if (objReader["OtherNotes"] != DBNull.Value)
                        {
                            obj.OtherNotes = objReader["OtherNotes"].ToString();
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        //if (objReader["VisitDate"] != DBNull.Value)
                        //{
                        //    obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        //}

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".PatientSocialHistoryConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PhysicalsDTO> PatientPhysicalConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<PhysicalsDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<PhysicalsDTO>();

                    while (objReader.Read())
                    {
                        PhysicalsDTO Obj = new PhysicalsDTO();

                        Obj.PatientPhysicalID = GetKeyAsNullIfTemplate(objReader, "PatientPhysicalID", Obj.PatientPhysicalID, IsTemplateVisit);

                        //if (objReader["PatientPhysicalID"] != DBNull.Value)
                        //{
                        //    Obj.PatientPhysicalID = int.Parse(objReader["PatientPhysicalID"].ToString());
                        //}
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        Obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, Obj.VisitID, IsTemplateVisit);

                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["PhysicalMainCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.PhysicalMainCategoryTypeID = int.Parse(objReader["PhysicalMainCategoryTypeID"].ToString());
                        }

                        if (objReader["MainCategoryName"] != DBNull.Value)
                        {
                            Obj.MainCategoryName = objReader["MainCategoryName"].ToString();
                        }
                        if (objReader["PhysicalSubCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.PhysicalSubCategoryTypeID = int.Parse(objReader["PhysicalSubCategoryTypeID"].ToString());
                        }
                        if (objReader["SubCategoryName"] != DBNull.Value)
                        {
                            Obj.SubCategoryName = objReader["SubCategoryName"].ToString();
                        }
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            Obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["IsNormal"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNormal"].ToString());
                            if (result)
                                Obj.IsNormal = ProjectConstant.SELECTION_TRUE;
                            else
                                Obj.IsNormal = ProjectConstant.SELECTION_False;
                        }

                        Obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, Obj.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        Obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, Obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        Obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, Obj.CreatedAt, IsTemplateVisit);

                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        Obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, Obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        Obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, Obj.ModifiedAt, IsTemplateVisit);

                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        if (objReader["PhysicalSubLevel3CategoryTypeID"] != DBNull.Value)
                        {
                            Obj.PhysicalSubLevel3CategoryTypeID = int.Parse(objReader["PhysicalSubLevel3CategoryTypeID"].ToString());
                        }
                        if (objReader["SubLevel3CategoryName"] != DBNull.Value)
                        {
                            Obj.SubLevel3CategoryName = objReader["SubLevel3CategoryName"].ToString();
                        }

                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientPhysicalConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }

        internal static List<PatientDiagnosisDTO> PatientDiagnosisConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<PatientDiagnosisDTO> diagnosisList = null;
            try
            {
                if (objReader != null)
                {
                    diagnosisList = new List<PatientDiagnosisDTO>();

                    while (objReader.Read())
                    {
                        PatientDiagnosisDTO Obj = new PatientDiagnosisDTO();

                        Obj.PatientDiagnosisID = GetKeyAsNullIfTemplate(objReader, "PatientDiagnosisID", Obj.PatientDiagnosisID, IsTemplateVisit);
                        //if (objReader["PatientDiagnosisID"] != DBNull.Value)
                        //{
                        //    Obj.PatientDiagnosisID = int.Parse(objReader["PatientDiagnosisID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        Obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, Obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            Obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["ICDCodeId"] != DBNull.Value)
                        {
                            Obj.ICDCode = new ICDCodeDTO();
                            Obj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeId"].ToString());

                            if (objReader["ICDCodeName"] != DBNull.Value)
                            {
                                Obj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();
                            }
                            //if (objReader["ICDCustomCodeDescription"]) != null)
                            if (objReader["ICDCustomDescription"] != DBNull.Value)
                            {
                                Obj.ICDCode.IcdCodeDescription = objReader["ICDCustomDescription"].ToString();
                            }
                            else if (objReader["ICDCodeDescription"] != DBNull.Value)
                            {
                                Obj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();
                            }
                        }

                        //if (objReader["ICDCodeName"] != DBNull.Value)
                        //{
                        //    Obj.ICDCodeName = objReader["ICDCodeName"].ToString();
                        //}
                        //if (objReader["ICDCodeDescription"] != DBNull.Value)
                        //{

                        //    Obj.ICDCodeDescription = objReader["ICDCodeDescription"].ToString();
                        //}


                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            Obj.OnSet = objReader["OnSet"].ToString();
                        }

                        if (objReader["DiagnosisNote"] != null && objReader["DiagnosisNote"] != DBNull.Value)
                        {
                            Obj.Note = objReader["DiagnosisNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        Obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, Obj.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        Obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, Obj.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        Obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, Obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        Obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, Obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        Obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, Obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        // Added by ismail 26 April 2011 coz missing 
                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            //  Obj.IsChronic = objReader["IsChronic"].ToString();
                            Obj.IsChronic = bool.Parse(objReader["IsChronic"].ToString());
                        }

                        diagnosisList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_DIAGNOSIS_DTO_CONVERSION_ERROR_MSG + ".PatientDiagnosisConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return diagnosisList;
        }

        internal static List<PhysicalMainCategoryTypeDTO> PhysicalMainTypeConverter(IDataReader objReader)
        {
            List<PhysicalMainCategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PhysicalMainCategoryTypeDTO>();

                    while (objReader.Read())
                    {
                        PhysicalMainCategoryTypeDTO obj = new PhysicalMainCategoryTypeDTO();

                        if (objReader["PhysicalMainCategoryTypeID"] != DBNull.Value)
                        {
                            obj.PhysicalMainCategoryTypeID = int.Parse(objReader["PhysicalMainCategoryTypeID"].ToString());
                        }

                        if (objReader["MainCategoryName"] != DBNull.Value)
                        {
                            obj.MainCategoryName = objReader["MainCategoryName"].ToString();
                            obj.label = objReader["MainCategoryName"].ToString();
                        }

                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }
                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".PhysicalMainTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PhysicalSubCategoryTypeDTO> PhysicalSubTypeConverter(IDataReader objReader)
        {
            List<PhysicalSubCategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PhysicalSubCategoryTypeDTO>();

                    while (objReader.Read())
                    {

                        PhysicalSubCategoryTypeDTO obj = new PhysicalSubCategoryTypeDTO();


                        if (objReader["PhysicalSubCategoryTypeID"] != DBNull.Value)
                        {
                            obj.PhysicalSubCategoryTypeID = int.Parse(objReader["PhysicalSubCategoryTypeID"].ToString());
                        }
                        if (objReader["PhysicalMainCategoryTypeID"] != DBNull.Value)
                        {
                            obj.PhysicalMainCategoryTypeID = int.Parse(objReader["PhysicalMainCategoryTypeID"].ToString());
                        }
                        if (objReader["SubCategoryName"] != DBNull.Value)
                        {
                            obj.SubCategoryName = objReader["SubCategoryName"].ToString();
                            obj.label = objReader["SubCategoryName"].ToString();
                        }

                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }
                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PHYSICAL_SUB_CATEGORY_DTO_CONVERSION_ERROR_MSG + ".PhysicalSubTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ROSSubCategoryTypeDTO> ROSSubTypeConverter(IDataReader objReader)
        {
            List<ROSSubCategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ROSSubCategoryTypeDTO>();

                    while (objReader.Read())
                    {

                        ROSSubCategoryTypeDTO obj = new ROSSubCategoryTypeDTO();


                        if (objReader["ROSSubCategoryTypeID"] != DBNull.Value)
                        {
                            obj.ROSSubCategoryTypeID = int.Parse(objReader["ROSSubCategoryTypeID"].ToString());
                        }
                        if (objReader["ROSMainCategoryTypeID"] != DBNull.Value)
                        {
                            obj.ROSMainCategoryTypeID = int.Parse(objReader["ROSMainCategoryTypeID"].ToString());
                        }
                        if (objReader["SubCategoryName"] != DBNull.Value)
                        {
                            obj.SubCategoryName = objReader["SubCategoryName"].ToString();
                            obj.label = objReader["SubCategoryName"].ToString();
                        }

                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }
                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".ROSSubTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientRecommendationDTO> ConvertPatientRecommendationByVisit(IDataReader objReader, bool IsTemplateVisit)
        {
            List<PatientRecommendationDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientRecommendationDTO>();

                    while (objReader.Read())
                    {

                        PatientRecommendationDTO obj = new PatientRecommendationDTO();

                        obj.PatientRecommendationID = GetKeyAsNullIfTemplate(objReader, "PatientRecommendationID", obj.PatientRecommendationID, IsTemplateVisit);
                        //if (objReader["PatientRecommendationID"] != DBNull.Value)
                        //{
                        //    obj.PatientRecommendationID = int.Parse(objReader["PatientRecommendationID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["RecommendationPlanID"] != DBNull.Value)
                        {
                            if (obj.Plan == null)
                                obj.Plan = new PlanTypeDTO();
                            obj.Plan.RecommendationPlanTypeID = int.Parse(objReader["RecommendationPlanID"].ToString());
                            obj.Plan.PlanName = objReader["PlanName"].ToString();
                        }

                        if (objReader["RecommendationMethodID"] != DBNull.Value)
                        {
                            if (obj.Method == null)
                                obj.Method = new MethodTypeDTO();
                            obj.Method.RecommendationMethodTypeID = int.Parse(objReader["RecommendationMethodID"].ToString());
                            obj.Method.MethodName = objReader["MethodName"].ToString();
                        }
                        if (objReader["RecommendationDurationID"] != DBNull.Value)
                        {
                            if (obj.Duration == null)
                                obj.Duration = new DurationTypeDTO();
                            obj.Duration.RecommendationDurationTypeID = int.Parse(objReader["RecommendationDurationID"].ToString());
                            obj.Duration.DurationName = objReader["DurationName"].ToString();
                        }
                        if (objReader["RecommendationLink"] != DBNull.Value)
                        {
                            if (obj.Links == null)

                                obj.Links = objReader["RecommendationLink"].ToString();
                        }



                        if (objReader["IsDiscussedWithPatient"] != DBNull.Value)
                        {
                            bool? IsDiscussedWithPatient = null;
                            IsDiscussedWithPatient = bool.Parse(objReader["IsDiscussedWithPatient"].ToString());

                            if (IsDiscussedWithPatient == null)
                                obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_NULL;
                            else if (IsDiscussedWithPatient == true)
                                obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_TRUE;
                            else if (IsDiscussedWithPatient == false)
                                obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_NULL;
                        }

                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Note = objReader["Notes"].ToString();
                        }

                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);

                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        //if (objReader["VisitDate"] != DBNull.Value)
                        //{
                        //    obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        //}

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        if (objReader["Description"] != DBNull.Value)
                        {
                            obj.Description = objReader["Description"].ToString();
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".LoadPatientRecommendationByVisit", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow up by visit Id
        /// </summary>
        /// <returns></returns>
        public static PatientFollowUpDTO ConvertToPatientFollowUp(IDataReader objReader, bool IsTemplateVisit)
        {
            PatientFollowUpDTO followUp = null;
            try
            {
                if (objReader != null && objReader.Read())
                {

                    followUp = new PatientFollowUpDTO();

                    followUp.PatientFollowUpID = GetKeyAsNullIfTemplate(objReader, "PatientFollowUpID", followUp.PatientFollowUpID, IsTemplateVisit);
                    //if (objReader["PatientFollowUpID"] != DBNull.Value)
                    //{
                    //    followUp.PatientFollowUpID = int.Parse(objReader["PatientFollowUpID"].ToString());
                    //}

                    if (objReader["PatientID"] != DBNull.Value)
                    {
                        followUp.PatientID = int.Parse(objReader["PatientID"].ToString());
                    }

                    followUp.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, followUp.VisitID, IsTemplateVisit);

                    //if (objReader["VisitID"] != DBNull.Value)
                    //{
                    //    followUp.VisitID = int.Parse(objReader["VisitID"].ToString());
                    //}


                    if (objReader["FollowupMethodTypeID"] != DBNull.Value)
                    {
                        followUp.FollowUpMethodType = new Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp.FollowUpMethodTypeDTO();

                        followUp.FollowUpMethodType.FollowUpMethodTypeID = int.Parse(objReader["FollowupMethodTypeID"].ToString());
                    }

                    if (objReader["IsByDate"] != DBNull.Value)
                    {
                        followUp.IsByDate = bool.Parse(objReader["IsByDate"].ToString());
                    }

                    if (objReader["FollowupDate"] != DBNull.Value)
                    {
                        followUp.FollowUpDate = objReader["FollowupDate"].ToString();
                    }

                    if (objReader["IsOnCompletion"] != DBNull.Value)
                    {
                        followUp.IsOnCompletion = bool.Parse(objReader["IsOnCompletion"].ToString());
                    }

                    if (objReader["IsLabTestSelected"] != DBNull.Value)
                    {
                        followUp.IsLabTestSelected = bool.Parse(objReader["IsLabTestSelected"].ToString());
                    }

                    if (objReader["IsDrugselected"] != DBNull.Value)
                    {
                        followUp.IsDrugselected = bool.Parse(objReader["IsDrugselected"].ToString());
                    }

                    if (objReader["LabTestID"] != DBNull.Value)
                    {
                        followUp.LabTestID = int.Parse(objReader["LabTestID"].ToString());
                    }


                    if (objReader["DrugID"] != DBNull.Value)
                    {
                        followUp.DrugID = int.Parse(objReader["DrugID"].ToString());
                    }

                    followUp.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, followUp.CreatedBy, IsTemplateVisit);

                    //if (objReader["CreatedBy"] != DBNull.Value)
                    //{
                    //    followUp.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                    //}

                    followUp.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, followUp.CreatedAt, IsTemplateVisit);
                    //if (objReader["CreatedAt"] != DBNull.Value)
                    //{
                    //    followUp.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                    //}

                    followUp.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, followUp.ModifiedBy, IsTemplateVisit);
                    //if (objReader["ModifiedBy"] != DBNull.Value)
                    //{
                    //    followUp.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                    //}

                    followUp.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, followUp.ModifiedAt, IsTemplateVisit);
                    //if (objReader["ModifiedAt"] != DBNull.Value)
                    //{
                    //    followUp.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                    //}

                    //if (objReader["VisitDate"] != DBNull.Value)
                    //{
                    //    followUp.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                    //}

                    if (objReader["IsDeleted"] != DBNull.Value)
                    {
                        followUp.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.VIEW_BACKGROUND_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientFollowUp", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return followUp;
        }

        internal static List<VisitDTO> ConvertToPatientVisitByClinicBranch(IDataReader objReader)
        {
            List<VisitDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<VisitDTO>();

                    while (objReader.Read())
                    {

                        VisitDTO obj = new VisitDTO();

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientId = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitId = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["ClinicBranchID"] != DBNull.Value)
                        {
                            if (obj.ClinicBranch == null)
                                obj.ClinicBranch = new ClinicBranchDTO();

                            obj.ClinicBranch.ClinicBranchIdSeq = int.Parse(objReader["ClinicBranchID"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["OwnerUserID"] != DBNull.Value)
                        {
                            obj.OwnerId = int.Parse(objReader["OwnerUserID"].ToString());
                        }
                        if (objReader["OwnerUserTypeID"] != DBNull.Value)
                        {
                            if (obj.OwnerUserType == null)
                                obj.OwnerUserType = new UserTypeDTO();

                            obj.OwnerUserType.UserTypeId = int.Parse(objReader["OwnerUserTypeID"].ToString());
                        }

                        if (objReader["SignedByUserID"] != DBNull.Value)
                        {
                            obj.SignedByUserId = int.Parse(objReader["SignedByUserID"].ToString());
                        }
                        if (objReader["SignedByUserTypeId"] != DBNull.Value)
                        {
                            if (obj.SignedByUserType == null)
                                obj.SignedByUserType = new UserTypeDTO();

                            obj.SignedByUserType.UserTypeId = int.Parse(objReader["SignedByUserTypeId"].ToString());
                        }
                        if (objReader["SignedDate"] != DBNull.Value)
                        {
                            obj.SignedDate = DateTime.Parse(objReader["SignedDate"].ToString());
                        }

                        if (objReader["SoapTemplateID"] != DBNull.Value)
                        {
                            obj.TemplateId = int.Parse(objReader["SoapTemplateID"].ToString());
                        }

                        if (objReader["EncounterTypeId"] != DBNull.Value)
                        {
                            obj.EncounterTypeId = int.Parse(objReader["EncounterTypeId"].ToString());
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        if (objReader["IsDressedForPhysical"] != DBNull.Value)
                        {
                            bool? IsDressedForPhysical = null;
                            IsDressedForPhysical = bool.Parse(objReader["IsDressedForPhysical"].ToString());

                            if (IsDressedForPhysical == null)
                                obj.IsDressedForPhysical = ProjectConstant.SELECTION_NULL;
                            else if (IsDressedForPhysical == true)
                                obj.IsDressedForPhysical = ProjectConstant.SELECTION_TRUE;
                            else if (IsDressedForPhysical == false)
                                obj.IsDressedForPhysical = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            obj.IsDressedForPhysical = ProjectConstant.SELECTION_NULL;
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientVisitByClinicBranch", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<VisistStakeHolderDTO> ConvertVisitStakeHolderByVisit(IDataReader objReader)
        {
            List<VisistStakeHolderDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<VisistStakeHolderDTO>();

                    while (objReader.Read())
                    {

                        VisistStakeHolderDTO obj = new VisistStakeHolderDTO();

                        if (objReader["ActionByUserID"] != DBNull.Value)
                        {
                            obj.ActionByUserID = int.Parse(objReader["ActionByUserID"].ToString());
                        }
                        if (objReader["ActionByUserTypeID"] != DBNull.Value)
                        {
                            obj.ActionByUserTypeID = int.Parse(objReader["ActionByUserTypeID"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.VISIT_STAKE_HOLDER_DTO_CONVERSION_ERROR_MSG + ".ConvertVisitStakeHolderByVisit", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<MedicationPrescribedDTO> ConvertPatientMedictionPrescribed(IDataReader objReader)
        {
            List<MedicationPrescribedDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<MedicationPrescribedDTO>();

                    while (objReader.Read())
                    {

                        MedicationPrescribedDTO obj = new MedicationPrescribedDTO();

                        if (objReader["PatientMedicationPrescribedID"] != DBNull.Value)
                        {
                            obj.PatientMedicationPrescribedID = int.Parse(objReader["PatientMedicationPrescribedID"].ToString());
                        }
                        if (objReader["MedicationTypeID"] != DBNull.Value)
                        {
                            obj.MedicationTypeID = int.Parse(objReader["MedicationTypeID"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["DrugName"] != DBNull.Value)
                        {
                            obj.DrugName = objReader["DrugName"].ToString();
                        }

                        if (objReader["MedicationPrescribedNote"] != DBNull.Value)
                        {
                            obj.MedicationPrescribedNote = objReader["MedicationPrescribedNote"].ToString();
                        }

                        if (objReader["MedicationStrength"] != DBNull.Value)
                        {
                            obj.MedicationStrength = objReader["MedicationStrength"].ToString();
                        }

                        if (objReader["Status"] != DBNull.Value)
                        {
                            obj.Status = objReader["Status"].ToString();
                        }
                        if (objReader["ApplicableStatus"] != DBNull.Value)
                        {
                            obj.ApplicableStatus = objReader["ApplicableStatus"].ToString();
                        }
                        if (objReader["SIG"] != DBNull.Value)
                        {
                            obj.SIG = objReader["SIG"].ToString();
                        }
                        if (objReader["Quantity"] != DBNull.Value)
                        {
                            obj.Quantity = objReader["Quantity"].ToString();
                        }
                        if (objReader["NoOfRefills"] != DBNull.Value)
                        {
                            obj.NoOfRefills = objReader["NoOfRefills"].ToString();
                        }
                        if (objReader["RecentRefills"] != DBNull.Value)
                        {
                            obj.RecentRefills = objReader["RecentRefills"].ToString();
                        }
                        if (objReader["OrderDays"] != DBNull.Value)
                        {
                            obj.OrderDays = Int32.Parse(objReader["OrderDays"].ToString());
                        }
                        if (objReader["Days"] != DBNull.Value)
                        {
                            obj.Days = float.Parse(objReader["Days"].ToString());
                        }

                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            bool? IsChronic = null;
                            IsChronic = bool.Parse(objReader["IsChronic"].ToString());

                            if (IsChronic == null)
                                obj.IsChronic = ProjectConstant.SELECTION_NULL;
                            else if (IsChronic == true)
                                obj.IsChronic = ProjectConstant.SELECTION_TRUE;
                            else if (IsChronic == false)
                                obj.IsChronic = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            obj.IsChronic = ProjectConstant.SELECTION_NULL;
                        }


                        if (objReader["DosageForm"] != DBNull.Value)
                        {
                            obj.DosageForm = objReader["DosageForm"].ToString();
                        }
                        if (objReader["Form"] != DBNull.Value)
                        {
                            obj.Form = objReader["Form"].ToString();
                        }
                        if (objReader["Dose"] != DBNull.Value)
                        {
                            obj.Dose = objReader["Dose"].ToString();
                        }
                        if (objReader["FrequencyTypeID"] != DBNull.Value)
                        {
                            obj.FrequencyTypeID = int.Parse(objReader["FrequencyTypeID"].ToString());
                        }
                        if (objReader["Route"] != DBNull.Value)
                        {
                            obj.Route = objReader["Route"].ToString();
                        }

                        if (objReader["PRN"] != DBNull.Value)
                        {
                            obj.PRN = bool.Parse(objReader["PRN"].ToString());
                        }
                        if (objReader["Onset"] != DBNull.Value)
                        {
                            obj.Onset = DateTime.Parse(objReader["Onset"].ToString());
                        }
                        if (objReader["Route"] != DBNull.Value)
                        {
                            obj.Route = objReader["Route"].ToString();
                        }


                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
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
                throw new DTOConversionException(ProjectConstant.MEDICATION_PRESCRIBED_DTO_CONVERSION_ERROR_MSG + ".ConvertPatientMedictionPrescribed", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<AllergyDTO> ConvertPatientAllergyDTO(IDataReader objReader)
        {
            List<AllergyDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<AllergyDTO>();

                    while (objReader.Read())
                    {


                        AllergyDTO obj = new AllergyDTO();

                        if (objReader["PatientAllergyID"] != DBNull.Value)
                        {
                            obj.PatientAllergyId = int.Parse(objReader["PatientAllergyID"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitId = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["AllergyName"] != DBNull.Value)
                        {
                            obj.AllergyName = objReader["AllergyName"].ToString();
                        }

                        if (objReader["AllergyNote"] != DBNull.Value)
                        {
                            obj.Note = objReader["AllergyNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["Reaction"] != DBNull.Value)
                        {
                            obj.Reaction = objReader["Reaction"].ToString();
                        }

                        if (objReader["Onset"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["Onset"].ToString();
                        }
                        //if (objReader["Onset"] != DBNull.Value)
                        //{
                        //    obj.OnSet = DateTime.Parse(objReader["Onset"].ToString());
                        //}

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            obj.RCOPIAID = int.Parse(objReader["RCOPIAID"].ToString());
                        }

                        if (objReader["Drug"] != DBNull.Value)
                        {
                            obj.Drug = objReader["Drug"].ToString();
                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.ALLERGY_DTO_CONVERSION_ERROR_MSG + ".ConvertPatientAllergyDTO", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<VitalDTO> ConvertPatientVitals(IDataReader objReader)
        {
            List<VitalDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<VitalDTO>();

                    while (objReader.Read())
                    {
                        VitalDTO obj = new VitalDTO();


                        if (objReader["PatientVitalID"] != DBNull.Value)
                        {
                            obj.PatientVitalID = int.Parse(objReader["PatientVitalID"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["Height"] != DBNull.Value)
                        {
                            obj.Height = objReader["Height"].ToString();
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }


                        if (objReader["VisitEncounterTypeName"] != DBNull.Value)
                        {
                            obj.VisitEncounterTypeName = objReader["VisitEncounterTypeName"].ToString();
                        }

                        if (objReader["Weight"] != DBNull.Value)
                        {
                            obj.Weight = objReader["Weight"].ToString();
                        }
                        if (objReader["Temperature"] != DBNull.Value)
                        {
                            obj.Temperature = objReader["Temperature"].ToString();
                        }
                        if (objReader["Pulse"] != DBNull.Value)
                        {
                            obj.Pulse = objReader["Pulse"].ToString();
                        }
                        if (objReader["Respiration"] != DBNull.Value)
                        {
                            obj.Respiration = objReader["Respiration"].ToString();
                        }
                        if (objReader["BPSys"] != DBNull.Value)
                        {
                            obj.BPSys = objReader["BPSys"].ToString();
                        }
                        if (objReader["BPDys"] != DBNull.Value)
                        {
                            obj.BPDys = objReader["BPDys"].ToString();
                        }
                        if (objReader["BMI"] != DBNull.Value)
                        {
                            obj.BMI = objReader["BMI"].ToString();
                        }

                        if (objReader["IsMetricDisplaySelected"] != DBNull.Value)
                        {
                            obj.IsMetricDisplaySelected = bool.Parse(objReader["IsMetricDisplaySelected"].ToString());
                        }

                        if (objReader["VitalNote"] != DBNull.Value)
                        {
                            obj.VitalNote = objReader["VitalNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["VitalDescription"] != DBNull.Value)
                        {
                            obj.VitalDescription = objReader["VitalDescription"].ToString();
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
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
                throw new DTOConversionException(ProjectConstant.VITAL_DTO_CONVERSION_ERROR_MSG + ".ConvertPatientVitals", ProjectConstant.ERROR_CODE.VITALDTOConversion.ToString());
            }
            return TypeList;
        }

        /// <summary>
        /// Not In Use
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<VisitDTO> ConvertVisitDataByVisit(IDataReader objReader)
        {
            List<VisitDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<VisitDTO>();

                    while (objReader.Read())
                    {

                        VisitDTO obj = new VisitDTO();

                        if (objReader["ActionByUserID"] != DBNull.Value)
                        {
                            // obj.ActionByUserID = int.Parse(objReader["ActionByUserID"].ToString());
                        }
                        if (objReader["ActionByUserTypeID"] != DBNull.Value)
                        {
                            //  obj.ActionByUserTypeID = int.Parse(objReader["ActionByUserTypeID"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.VISIT_DATA_DTO_CONVERSION_ERROR_MSG + ".ConvertVisitDataByVisit", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        /// <summary>
        /// aUTHOR iSMAIL
        /// Date 06 April 2011
        /// </summary>
        /// <param name="objIReader"></param>
        /// <returns></returns>
        internal static List<PrescriptionDTO> ConvertPateintprescriptionByVisit(IDataReader objReader)
        {
            List<PrescriptionDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PrescriptionDTO>();

                    while (objReader.Read())
                    {

                        PrescriptionDTO obj = new PrescriptionDTO();

                        if (objReader["PatientPrescriptionID"] != DBNull.Value)
                        {
                            obj.PatientPrescriptionID = int.Parse(objReader["PatientPrescriptionID"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["IsVoided"] != DBNull.Value)
                        {
                            obj.IsVoided = bool.Parse(objReader["IsVoided"].ToString());
                        }
                        if (objReader["IsDenied"] != DBNull.Value)
                        {
                            obj.IsDenied = bool.Parse(objReader["IsDenied"].ToString());
                        }

                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            obj.RCOPIAID = objReader["RCOPIAID"].ToString();
                        }
                        if (objReader["RxReferenceNo"] != DBNull.Value)
                        {
                            obj.RxReferenceNo = objReader["RxReferenceNo"].ToString();
                        }

                        if (objReader["StopDate"] != DBNull.Value)
                        {
                            obj.StopDate = DateTime.Parse(objReader["StopDate"].ToString());
                        }
                        if (objReader["CompletedDate"] != DBNull.Value)
                        {
                            obj.CompletedDate = DateTime.Parse(objReader["CompletedDate"].ToString());
                        }

                        if (objReader["PreparerUserID"] != DBNull.Value)
                        {
                            obj.PreparerUserID = int.Parse(objReader["PreparerUserID"].ToString());
                        }
                        if (objReader["PreparerUserTypeID"] != DBNull.Value)
                        {
                            obj.PreparerUserTypeID = int.Parse(objReader["PreparerUserTypeID"].ToString());
                        }
                        if (objReader["ProviderUserID"] != DBNull.Value)
                        {
                            obj.ProviderUserID = int.Parse(objReader["ProviderUserID"].ToString());
                        }
                        if (objReader["ProviderUserTypeID"] != DBNull.Value)
                        {
                            obj.ProviderUserTypeID = int.Parse(objReader["ProviderUserTypeID"].ToString());
                        }

                        if (objReader["CompletionAction"] != DBNull.Value)
                        {
                            obj.CompletionAction = objReader["CompletionAction"].ToString();
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintprescriptionByVisit", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<MedicationDTO> ConvertPateintMedicationByprescriptionId(IDataReader objReader)
        {
            List<MedicationDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<MedicationDTO>();


                    while (objReader.Read())
                    {
                        MedicationDTO obj = new MedicationDTO();

                        if (objReader["PatientMedicationID"] != DBNull.Value)
                        {
                            obj.PatientMedicationID = int.Parse(objReader["PatientMedicationID"].ToString());
                        }

                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            obj.RCOPIAID = objReader["RCOPIAID"].ToString();
                        }
                        if (objReader["NDCID"] != DBNull.Value)
                        {
                            obj.NDCID = objReader["NDCID"].ToString();
                        }
                        if (objReader["FirstDataBankMedID"] != DBNull.Value)
                        {
                            obj.FirstDataBankMedID = objReader["FirstDataBankMedID"].ToString();
                        }
                        if (objReader["BrandName"] != DBNull.Value)
                        {
                            obj.BrandName = objReader["BrandName"].ToString();
                        }
                        if (objReader["GenericName"] != DBNull.Value)
                        {
                            obj.GenericName = objReader["GenericName"].ToString();
                        }
                        if (objReader["Form"] != DBNull.Value)
                        {
                            obj.Form = objReader["Form"].ToString();
                        }
                        if (objReader["Strength"] != DBNull.Value)
                        {
                            obj.Strength = objReader["Strength"].ToString();
                        }
                        if (objReader["Action"] != DBNull.Value)
                        {
                            obj.Action = objReader["Action"].ToString();
                        }
                        if (objReader["Dose"] != DBNull.Value)
                        {
                            obj.Dose = objReader["Dose"].ToString();
                        }
                        if (objReader["DoseUnit"] != DBNull.Value)
                        {
                            obj.DoseUnit = objReader["DoseUnit"].ToString();
                        }
                        if (objReader["Route"] != DBNull.Value)
                        {
                            obj.Route = objReader["Route"].ToString();
                        }
                        if (objReader["DoseTiming"] != DBNull.Value)
                        {
                            obj.DoseTiming = objReader["DoseTiming"].ToString();
                        }
                        if (objReader["DoseOther"] != DBNull.Value)
                        {
                            obj.DoseOther = objReader["DoseOther"].ToString();
                        }
                        if (objReader["Duration"] != DBNull.Value)
                        {
                            obj.Duration = objReader["Duration"].ToString();
                        }
                        if (objReader["Quantity"] != DBNull.Value)
                        {
                            obj.Quantity = objReader["Quantity"].ToString();
                        }
                        if (objReader["QuantityUnit"] != DBNull.Value)
                        {
                            obj.QuantityUnit = objReader["QuantityUnit"].ToString();
                        }
                        if (objReader["Refills"] != DBNull.Value)
                        {
                            obj.Refills = objReader["Refills"].ToString();
                        }
                        if (objReader["IsSubstitutionPermitted"] != DBNull.Value)
                        {
                            obj.IsSubstitutionPermitted = bool.Parse(objReader["IsSubstitutionPermitted"].ToString());
                        }

                        if (objReader["OtherNotes"] != DBNull.Value)
                        {
                            obj.OtherNotes = objReader["OtherNotes"].ToString();
                        }
                        if (objReader["PatientNotes"] != DBNull.Value)
                        {
                            obj.PatientNotes = objReader["PatientNotes"].ToString();
                        }
                        if (objReader["Comments"] != DBNull.Value)
                        {
                            obj.Comments = objReader["Comments"].ToString();
                        }
                        if (objReader["Status"] != DBNull.Value)
                        {
                            obj.Status = objReader["Status"].ToString();
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["Signature"] != DBNull.Value)
                        {
                            obj.Signature = objReader["Signature"].ToString();
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICATION_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintMedicationByprescriptionId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientDiagnosisDTO> ConvertPateintDiagnosisByprescriptionId(IDataReader objReader)
        {
            List<PatientDiagnosisDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientDiagnosisDTO>();

                    while (objReader.Read())
                    {
                        PatientDiagnosisDTO obj = new PatientDiagnosisDTO();

                        if (objReader["PatientDiagnosisID"] != DBNull.Value)
                        {
                            obj.PatientDiagnosisID = int.Parse(objReader["PatientDiagnosisID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["ICDCodeID"] != DBNull.Value)
                        {
                            if (obj.ICDCode == null)
                                obj.ICDCode = new ICDCodeDTO();

                            obj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeID"].ToString());


                            if (objReader["ICDCustomDescription"] != DBNull.Value)
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCustomDescription"].ToString();
                            else if (objReader["ICDCodeDescription"] != DBNull.Value)
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();

                            if (objReader["ICDCodeName"] != DBNull.Value)
                                obj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();
                        }
                        if (objReader["ICDDescription"] != DBNull.Value)
                        {
                            obj.ICDCodeDescription = objReader["ICDDescription"].ToString();
                        }
                        if (objReader["ICDCode"] != DBNull.Value)
                        {
                            obj.ICDCodeName = objReader["ICDCode"].ToString();
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["DiagnosisNote"] != DBNull.Value)
                        {
                            obj.Note = objReader["DiagnosisNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        //if (objReader["IsChronic"] != DBNull.Value)
                        //{
                        //    bool? IsChronic = null;
                        //    IsChronic = bool.Parse(objReader["IsChronic"].ToString());

                        //    if (IsChronic == null)
                        //        obj.IsChronic = ProjectConstant.SELECTION_NULL;
                        //    else if (IsChronic == true)
                        //        obj.IsChronic = ProjectConstant.SELECTION_TRUE;
                        //    else if (IsChronic == false)
                        //        obj.IsChronic = ProjectConstant.SELECTION_False;
                        //}
                        //else
                        //{
                        //    obj.IsChronic = ProjectConstant.SELECTION_NULL;
                        //}
                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            obj.IsChronic = bool.Parse(objReader["IsChronic"].ToString());
                        }
                        if (objReader["IsActive"] != DBNull.Value)
                        {
                            obj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }

                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
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
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICATION_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintMedicationByprescriptionId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientFollowupInstructionDTO> ConvertFollowupInstruction(IDataReader objReader, bool IsTemplateVisit)
        {
            List<PatientFollowupInstructionDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientFollowupInstructionDTO>();

                    while (objReader.Read())
                    {
                        PatientFollowupInstructionDTO obj = new PatientFollowupInstructionDTO();

                        obj.FollowupInstructionID = GetKeyAsNullIfTemplate(objReader, "FollowupInstructionID", obj.FollowupInstructionID, IsTemplateVisit);

                        //if (objReader["FollowupInstructionID"] != DBNull.Value)
                        //{
                        //    obj.FollowupInstructionID = int.Parse(objReader["FollowupInstructionID"].ToString());
                        //}
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);

                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        //if (objReader["FollowupServiceTypeId"] != DBNull.Value)
                        //{
                        //    if (obj.objServiceTypeDTO == null)
                        //        obj.objServiceTypeDTO = new ServiceTypeDTO();

                        //    obj.objServiceTypeDTO.FollowupServiceTypeId = int.Parse(objReader["FollowupServiceTypeId"].ToString());

                        //    if (objReader["FollowupServiceTypeName"] != DBNull.Value)
                        //        obj.objServiceTypeDTO.FollowupServiceTypeName = objReader["FollowupServiceTypeName"].ToString();

                        //}

                        if (objReader["FollowupService"] != DBNull.Value)
                        {
                            if (obj.objServiceTypeDTO == null)
                                obj.objServiceTypeDTO = new ServiceTypeDTO();
                            obj.objServiceTypeDTO.FollowupService = objReader["FollowupService"].ToString();
                        }

                        if (objReader["AgeTypeID"] != DBNull.Value)
                        {
                            if (obj.AgeTypeDTO == null)
                                obj.AgeTypeDTO = new InstructionAgeTypeDTO();

                            obj.AgeTypeDTO.InstructionAgeTypeID = int.Parse(objReader["AgeTypeID"].ToString());

                            if (objReader["AgeTypeName"] != DBNull.Value)
                                obj.AgeTypeDTO.AgeTypeName = objReader["AgeTypeName"].ToString();

                        }

                        if (objReader["AgeValueTypeID"] != DBNull.Value)
                        {
                            if (obj.AgeValueTypeDTO == null)
                                obj.AgeValueTypeDTO = new InstructionAgeValueTypeDTO();

                            obj.AgeValueTypeDTO.InstructionAgeValueTypeID = int.Parse(objReader["AgeValueTypeID"].ToString());

                            if (objReader["AgeValueTypeName"] != DBNull.Value)
                                obj.AgeTypeDTO.AgeTypeName = objReader["AgeValueTypeName"].ToString();
                        }


                        if (objReader["AfterDurationTypeID"] != DBNull.Value)
                        {
                            if (obj.AfterInstructionDurationTypeDTO == null)
                                obj.AfterInstructionDurationTypeDTO = new InstructionDurationTypeDTO();

                            obj.AfterInstructionDurationTypeDTO.DurationTypeID = int.Parse(objReader["AfterDurationTypeID"].ToString());

                            if (objReader["AfterDurationTypeName"] != DBNull.Value)
                                obj.AfterInstructionDurationTypeDTO.DurationTypeName = objReader["AfterDurationTypeName"].ToString();

                            if (objReader["AfterDurationValue"] != DBNull.Value)
                            {
                                obj.AfterDurationValue = int.Parse(objReader["AfterDurationValue"].ToString());
                            }
                        }

                        if (objReader["ForDurationTypeID"] != DBNull.Value)
                        {
                            if (obj.ForInstructionDurationTypeDTO == null)
                                obj.ForInstructionDurationTypeDTO = new InstructionDurationTypeDTO();

                            obj.ForInstructionDurationTypeDTO.DurationTypeID = int.Parse(objReader["ForDurationTypeID"].ToString());

                            if (objReader["ForDurationTypeName"] != DBNull.Value)
                                obj.ForInstructionDurationTypeDTO.DurationTypeName = objReader["ForDurationTypeName"].ToString();

                            if (objReader["ForDurationValue"] != DBNull.Value)
                            {
                                obj.ForDurationValue = int.Parse(objReader["ForDurationValue"].ToString());
                            }
                        }

                        obj.IsAgeSelected = bool.Parse(objReader["IsAgeSelected"].ToString());
                        obj.IsDurationSelected = bool.Parse(objReader["IsDurationSelected"].ToString());

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_FOLLOWUP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG + ".ConvertFollowupInstruction", ProjectConstant.ERROR_CODE.FollowupLabsConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PrescriptionMedicationDTO> ConvertPrescriptionMedication(IDataReader objReader)
        {
            List<PrescriptionMedicationDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PrescriptionMedicationDTO>();

                    while (objReader.Read())
                    {
                        PrescriptionMedicationDTO obj = new PrescriptionMedicationDTO();

                        if (objReader["PrescriptionMedicationID"] != DBNull.Value)
                        {
                            obj.PrescriptionMedicationID = int.Parse(objReader["PrescriptionMedicationID"].ToString());
                        }
                        //if (objReader["PatientID"] != DBNull.Value)
                        //{
                        //    obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        //}
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }
                        //if (objReader["Signature"] != DBNull.Value)
                        //{
                        //    obj.Signature = objReader["Signature"].ToString();
                        //}

                        if (objReader["PatientPrescriptionID"] != DBNull.Value)
                        {
                            obj.PatientPrescriptionID = int.Parse(objReader["PatientPrescriptionID"].ToString());
                        }
                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            obj.RCOPIAID = objReader["RCOPIAID"].ToString();
                        }
                        if (objReader["NDCID"] != DBNull.Value)
                        {
                            obj.NDCID = objReader["NDCID"].ToString();
                        }
                        if (objReader["FirstDataBankMedID"] != DBNull.Value)
                        {
                            obj.FirstDataBankMedID = objReader["FirstDataBankMedID"].ToString();
                        }
                        if (objReader["BrandName"] != DBNull.Value)
                        {
                            obj.BrandName = objReader["BrandName"].ToString();
                        }
                        if (objReader["GenericName"] != DBNull.Value)
                        {
                            obj.GenericName = objReader["GenericName"].ToString();
                        }
                        if (objReader["Form"] != DBNull.Value)
                        {
                            obj.Form = objReader["Form"].ToString();
                        }
                        if (objReader["Strength"] != DBNull.Value)
                        {
                            obj.Strength = objReader["Strength"].ToString();
                        }
                        if (objReader["Action"] != DBNull.Value)
                        {
                            obj.Action = objReader["Action"].ToString();
                        }

                        if (objReader["Dose"] != DBNull.Value)
                        {
                            obj.Dose = objReader["Dose"].ToString();
                        }
                        if (objReader["DoseUnit"] != DBNull.Value)
                        {
                            obj.DoseUnit = objReader["DoseUnit"].ToString();
                        }

                        if (objReader["Route"] != DBNull.Value)
                        {
                            obj.Route = objReader["Route"].ToString();
                        }
                        if (objReader["DoseTiming"] != DBNull.Value)
                        {
                            obj.DoseTiming = objReader["DoseTiming"].ToString();
                        }
                        if (objReader["DoseOther"] != DBNull.Value)
                        {
                            obj.DoseOther = objReader["DoseOther"].ToString();
                        }
                        if (objReader["Duration"] != DBNull.Value)
                        {
                            obj.Duration = objReader["Duration"].ToString();
                        }
                        if (objReader["Quantity"] != DBNull.Value)
                        {
                            obj.Quantity = objReader["Quantity"].ToString();
                        }
                        if (objReader["QuantityUnit"] != DBNull.Value)
                        {
                            obj.QuantityUnit = objReader["QuantityUnit"].ToString();
                        }
                        if (objReader["Refills"] != DBNull.Value)
                        {
                            obj.Refills = objReader["Refills"].ToString();
                        }
                        if (objReader["IsSubstitutionPermitted"] != DBNull.Value)
                        {
                            obj.IsSubstitutionPermitted = bool.Parse(objReader["IsSubstitutionPermitted"].ToString());
                        }
                        if (objReader["OtherNotes"] != DBNull.Value)
                        {
                            obj.OtherNotes = objReader["OtherNotes"].ToString();
                        }
                        if (objReader["PatientNotes"] != DBNull.Value)
                        {
                            obj.PatientNotes = objReader["PatientNotes"].ToString();
                        }


                        if (objReader["Comments"] != DBNull.Value)
                        {
                            obj.Comments = objReader["Comments"].ToString();
                        }
                        if (objReader["Status"] != DBNull.Value)
                        {
                            obj.Status = objReader["Status"].ToString();
                        }

                        if (objReader["StartDate"] != DBNull.Value)
                        {
                            obj.StartDate = DateTime.Parse(objReader["StartDate"].ToString());
                        }
                        if (objReader["EndDate"] != DBNull.Value)
                        {
                            obj.EndDate = DateTime.Parse(objReader["EndDate"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["Signature"] != DBNull.Value)
                        {
                            obj.Signature = objReader["Signature"].ToString();
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        //if (objReader["IsRecordEditable"] != DBNull.Value)
                        //{
                        //    obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        //}
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICATION_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintMedicationByprescriptionId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;

        }

        internal static List<ProcedureDTO> ConvertPRocedureDto(IDataReader objReader, bool IsTemplateVisit)
        {
            List<ProcedureDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ProcedureDTO>();

                    while (objReader.Read())
                    {
                        ProcedureDTO obj = new ProcedureDTO();

                        obj.PatientProcedureID = GetKeyAsNullIfTemplate(objReader, "PatientProcedureID", obj.PatientProcedureID, IsTemplateVisit);
                        //if (objReader["PatientProcedureID"] != DBNull.Value)
                        //{
                        //    obj.PatientProcedureID = int.Parse(objReader["PatientProcedureID"].ToString());
                        //}

                        if (objReader["VisitDate"] != DBNull.Value && !IsTemplateVisit)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

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

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICATION_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintMedicationByprescriptionId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ProcedureDTO> ConvertPRocedureDtoByVisitId(IDataReader objReader, bool IsTemplateVisit)
        {
            List<ProcedureDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ProcedureDTO>();

                    while (objReader.Read())
                    {
                        ProcedureDTO obj = new ProcedureDTO();

                        obj.PatientProcedureID = GetKeyAsNullIfTemplate(objReader, "PatientProcedureID", obj.PatientProcedureID, IsTemplateVisit);
                        //if (objReader["PatientProcedureID"] != DBNull.Value)
                        //{
                        //    obj.PatientProcedureID = int.Parse(objReader["PatientProcedureID"].ToString());
                        //}

                        //if (objReader["VisitDate"] != DBNull.Value && !IsTemplateVisit)
                        //{
                        //    obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        //}
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

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

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICATION_PRESCRIPTION_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintMedicationByprescriptionId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
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
                throw new DTOConversionException(ProjectConstant.FOLLOWUP_LBS__DTO_CONVERSION_ERROR_MSG + ".ConvertPatientFollowupLabs", ProjectConstant.ERROR_CODE.FollowupLabsConversion.ToString());
            }
            return TypeList;
        }

        internal static List<InHouseLabDTO> ConvertInHouseLabs(IDataReader objReader, bool IsTemplateVisit)
        {
            List<InHouseLabDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<InHouseLabDTO>();

                    while (objReader.Read())
                    {
                        InHouseLabDTO obj = new InHouseLabDTO();

                        obj.PatientInHouseLabTestID = GetKeyAsNullIfTemplate(objReader, "PatientInHouseLabTestID", obj.PatientInHouseLabTestID, IsTemplateVisit);

                        //if (objReader["PatientInHouseLabTestID"] != DBNull.Value)
                        //{
                        //    obj.PatientInHouseLabTestID = int.Parse(objReader["PatientInHouseLabTestID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}
                        if (objReader["InHouseLabTestTypeID"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.InHouseLabTestTypeID = int.Parse(objReader["InHouseLabTestTypeID"].ToString());
                        }

                        if (objReader["TestCode"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.TestCode = objReader["TestCode"].ToString();
                        }
                        if (objReader["TestName"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.TestName = objReader["TestName"].ToString();
                        }
                        if (objReader["Description"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.Description = objReader["Description"].ToString();
                        }
                        if (objReader["TestRange"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.TestRange = objReader["TestRange"].ToString();
                        }
                        if (objReader["TestResult"] != DBNull.Value)
                        {
                            obj.TestResult = objReader["TestResult"].ToString();
                        }
                        if (objReader["ResultAttachment"] != DBNull.Value)
                        {
                            obj.ResultAttachment = (byte[])objReader["ResultAttachment"];
                        }

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }


                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        //if (objReader["VisitDate"] != DBNull.Value)
                        //{
                        //    obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        //}

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_IN_HOUSE_LABS_DTO_CONVERSION_ERROR_MSG + ".ConvertInHouseLabs", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;

        }

        internal static List<MedicalHistoryDTO> ConvertPatientMedicalHistory(IDataReader objReader)
        {
            List<MedicalHistoryDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<MedicalHistoryDTO>();

                    while (objReader.Read())
                    {
                        MedicalHistoryDTO obj = new MedicalHistoryDTO();

                        if (objReader["PatientMedicalHistoryID"] != DBNull.Value)
                        {
                            obj.PatientMedicalHistoryID = int.Parse(objReader["PatientMedicalHistoryID"].ToString());
                        }

                        if (objReader["MedicalHistoryTypeID"] != DBNull.Value)
                        {
                            if (obj.MedicalHistoryType == null)
                                obj.MedicalHistoryType = new MedicalHistoryTypeDTO();

                            obj.MedicalHistoryTypeID = int.Parse(objReader["MedicalHistoryTypeID"].ToString());
                            obj.MedicalHistoryType.MedicalHistoryTypeID = int.Parse(objReader["MedicalHistoryTypeID"].ToString());
                        }

                        if (objReader["MedicalHistoryTypeName"] != DBNull.Value)
                        {
                            if (obj.MedicalHistoryType == null)
                                obj.MedicalHistoryType = new MedicalHistoryTypeDTO();

                            obj.MedicalHistoryType.MedicalHistoryTypeName = objReader["MedicalHistoryTypeName"].ToString();
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["MedicalHistoryNote"] != DBNull.Value)
                        {
                            obj.MedicalHistoryNote = objReader["MedicalHistoryNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["PatientMedicalHistoryDescription"] != DBNull.Value)
                        {
                            obj.PatientMedicalHistoryDescription = objReader["PatientMedicalHistoryDescription"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }
                        if (objReader["IsNone"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNone"].ToString());
                            if (result)
                            {
                                obj.IsNone = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                obj.IsNone = ProjectConstant.SELECTION_False;
                            }
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".ConvertInHouseLabs", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<FamilyHistoryDTO> ConvertPatientFamilyHistory(IDataReader objReader)
        {
            List<FamilyHistoryDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<FamilyHistoryDTO>();

                    while (objReader.Read())
                    {
                        FamilyHistoryDTO obj = new FamilyHistoryDTO();

                        if (objReader["PatientFamilyHistoryID"] != DBNull.Value)
                        {
                            obj.PatientFamilyHistoryID = int.Parse(objReader["PatientFamilyHistoryID"].ToString());
                        }

                        if (objReader["FamilyHistoryTypeID"] != DBNull.Value)
                        {
                            obj.FamilyHistoryTypeID = int.Parse(objReader["FamilyHistoryTypeID"].ToString());
                        }

                        if (objReader["FamilyHistoryTypeName"] != DBNull.Value)
                        {
                            obj.FamilyHistoryTypeName = objReader["FamilyHistoryTypeName"].ToString();
                        }


                        if (objReader["DiseaseCode"] != DBNull.Value)
                        {
                            obj.DiseaseCode = objReader["DiseaseCode"].ToString();
                        }
                        if (objReader["DiseaseName"] != DBNull.Value)
                        {
                            obj.DiseaseName = objReader["DiseaseName"].ToString();
                        }

                        if (objReader["DiseaseID"] != DBNull.Value)
                        {
                            obj.DiseaseID = int.Parse(objReader["DiseaseID"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["FamilyHistoryNote"] != DBNull.Value)
                        {
                            obj.FamilyHistoryNote = objReader["FamilyHistoryNote"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["IsPresent"] != DBNull.Value)
                        {
                            obj.IsPresent = bool.Parse(objReader["IsPresent"].ToString());
                        }
                        if (objReader["ReletionshipTypeID"] != DBNull.Value)
                        {
                            obj.ReletionshipTypeID = Int32.Parse(objReader["ReletionshipTypeID"].ToString());
                        }


                        if (objReader["IsYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsYesSelected"].ToString());
                            if (result)
                            {
                                obj.IsYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                obj.IsYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
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
                throw new DTOConversionException(ProjectConstant.PATIENT_FAMILY_HISTORY_DTO_CONVERSION_ERROR_MSG + ".ConvertInHouseLabs", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<HPI_CCDTO> ConvertPatientHPI(IDataReader objReader)
        {

            List<HPI_CCDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<HPI_CCDTO>();

                    while (objReader.Read())
                    {
                        HPI_CCDTO Obj = new HPI_CCDTO();

                        if (objReader["PatientHPIID"] != DBNull.Value)
                        {
                            Obj.PatientHPIID = int.Parse(objReader["PatientHPIID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["HPINote"] != DBNull.Value)
                        {
                            Obj.Note = objReader["HPINote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["ChiefComplaint"] != DBNull.Value)
                        {
                            Obj.ChiefComplaint = objReader["ChiefComplaint"].ToString();
                        }



                        if (objReader["Symptom"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Symptom = objReader["Symptom"].ToString();
                        }

                        if (objReader["Duration"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Duration = objReader["Duration"].ToString();
                        }

                        if (objReader["Location"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Location = objReader["Location"].ToString();
                        }

                        if (objReader["Quality"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Quality = objReader["Quality"].ToString();
                        }

                        if (objReader["Severity"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Severity = objReader["Severity"].ToString();
                        }

                        if (objReader["Context"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Context = objReader["Context"].ToString();
                        }

                        if (objReader["ModifyingFactors"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.ModifyingFactors = objReader["ModifyingFactors"].ToString();
                        }

                        if (objReader["Exacerbating"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Exacerbating = objReader["Exacerbating"].ToString();
                        }

                        if (objReader["Progression"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Progression = objReader["Progression"].ToString();
                        }

                        if (objReader["Radiation"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Radiation = objReader["Radiation"].ToString();
                        }

                        if (objReader["Onset"] != DBNull.Value)
                        {
                            if (Obj.HPIFields == null)
                            {
                                Obj.HPIFields = new HPIDetailsDTO();
                            }
                            Obj.HPIFields.Onset = objReader["Onset"].ToString();
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }


                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            Obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            Obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }
                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_HPI_DTO_CONVERSION_ERROR_MSG + ".PatientHPIConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }

        internal static List<ROSDTO> ConvertPatientROS(IDataReader objReader)
        {
            List<ROSDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<ROSDTO>();

                    while (objReader.Read())
                    {
                        ROSDTO Obj = new ROSDTO();

                        if (objReader["PatientROSID"] != DBNull.Value)
                        {
                            Obj.PatientROSID = int.Parse(objReader["PatientROSID"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["ROSMainCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.ROSMainCategoryTypeID = int.Parse(objReader["ROSMainCategoryTypeID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }
                        if (objReader["MainCategoryName"] != DBNull.Value)
                        {
                            Obj.MainCategoryName = objReader["MainCategoryName"].ToString();
                        }
                        if (objReader["ROSSubCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.ROSSubCategoryTypeID = int.Parse(objReader["ROSSubCategoryTypeID"].ToString());
                        }
                        if (objReader["SubCategoryName"] != DBNull.Value)
                        {
                            Obj.SubCategoryName = objReader["SubCategoryName"].ToString();
                        }
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            Obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["ROSSubLevel3CategoryTypeID"] != DBNull.Value)
                        {
                            Obj.ROSSubLevel3CategoryTypeID = int.Parse(objReader["ROSSubLevel3CategoryTypeID"].ToString());
                        }
                        if (objReader["ROSSubLevel3CategoryName"] != DBNull.Value)
                        {
                            Obj.ROSSubLevel3CategoryName = objReader["ROSSubLevel3CategoryName"].ToString();
                        }

                        if (objReader["IsNormal"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNormal"].ToString());
                            if (result)
                                Obj.IsNormal = ProjectConstant.SELECTION_TRUE;
                            else
                                Obj.IsNormal = ProjectConstant.SELECTION_False;
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            Obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            Obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }
                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientROSConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }

        internal static List<PhysicalsDTO> ConvertPatientPhysicals(IDataReader objReader)
        {

            List<PhysicalsDTO> HistoryList = null;
            try
            {
                if (objReader != null)
                {
                    HistoryList = new List<PhysicalsDTO>();

                    while (objReader.Read())
                    {
                        PhysicalsDTO Obj = new PhysicalsDTO();

                        if (objReader["PatientPhysicalID"] != DBNull.Value)
                        {
                            Obj.PatientPhysicalID = int.Parse(objReader["PatientPhysicalID"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            Obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            Obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }
                        if (objReader["PhysicalMainCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.PhysicalMainCategoryTypeID = int.Parse(objReader["PhysicalMainCategoryTypeID"].ToString());
                        }

                        if (objReader["MainCategoryName"] != DBNull.Value)
                        {
                            Obj.MainCategoryName = objReader["MainCategoryName"].ToString();
                        }
                        if (objReader["PhysicalSubCategoryTypeID"] != DBNull.Value)
                        {
                            Obj.PhysicalSubCategoryTypeID = int.Parse(objReader["PhysicalSubCategoryTypeID"].ToString());
                        }
                        if (objReader["SubCategoryName"] != DBNull.Value)
                        {
                            Obj.SubCategoryName = objReader["SubCategoryName"].ToString();
                        }
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            Obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            Obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["IsNormal"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsNormal"].ToString());
                            if (result)
                                Obj.IsNormal = ProjectConstant.SELECTION_TRUE;
                            else
                                Obj.IsNormal = ProjectConstant.SELECTION_False;
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            Obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            Obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            Obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            Obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            Obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            Obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            Obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        if (objReader["PhysicalSubLevel3CategoryTypeID"] != DBNull.Value)
                        {
                            Obj.PhysicalSubLevel3CategoryTypeID = int.Parse(objReader["PhysicalSubLevel3CategoryTypeID"].ToString());
                        }
                        if (objReader["SubLevel3CategoryName"] != DBNull.Value)
                        {
                            Obj.SubLevel3CategoryName = objReader["SubLevel3CategoryName"].ToString();
                        }

                        HistoryList.Add(Obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientPhysicalConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;

        }

        internal static List<VitalDTO> ConvertPatientViewBackgroundVitals(IDataReader objReader)
        {
            List<VitalDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<VitalDTO>();

                    while (objReader.Read())
                    {
                        VitalDTO obj = new VitalDTO();


                        if (objReader["PatientVitalID"] != DBNull.Value)
                        {
                            obj.PatientVitalID = int.Parse(objReader["PatientVitalID"].ToString());
                        }

                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["Height"] != DBNull.Value)
                        {
                            obj.Height = objReader["Height"].ToString();
                        }

                        if (objReader["Weight"] != DBNull.Value)
                        {
                            obj.Weight = objReader["Weight"].ToString();
                        }
                        if (objReader["Temperature"] != DBNull.Value)
                        {
                            obj.Temperature = objReader["Temperature"].ToString();
                        }
                        if (objReader["Pulse"] != DBNull.Value)
                        {
                            obj.Pulse = objReader["Pulse"].ToString();
                        }
                        if (objReader["Respiration"] != DBNull.Value)
                        {
                            obj.Respiration = objReader["Respiration"].ToString();
                        }
                        if (objReader["BPSys"] != DBNull.Value)
                        {
                            obj.BPSys = objReader["BPSys"].ToString();
                        }
                        if (objReader["BPDys"] != DBNull.Value)
                        {
                            obj.BPDys = objReader["BPDys"].ToString();
                        }
                        if (objReader["BMI"] != DBNull.Value)
                        {
                            obj.BMI = objReader["BMI"].ToString();
                        }

                        if (objReader["Oxygen"] != DBNull.Value)
                        {
                            obj.Oxygen = objReader["Oxygen"].ToString();
                        }

                        if (objReader["IsMetricDisplaySelected"] != DBNull.Value)
                        {
                            obj.IsMetricDisplaySelected = bool.Parse(objReader["IsMetricDisplaySelected"].ToString());
                        }

                        if (objReader["VitalNote"] != DBNull.Value)
                        {
                            obj.VitalNote = objReader["VitalNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["VitalDescription"] != DBNull.Value)
                        {
                            obj.VitalDescription = objReader["VitalDescription"].ToString();
                        }


                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        }

                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }

                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        }

                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
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
                throw new DTOConversionException(ProjectConstant.VITAL_DTO_CONVERSION_ERROR_MSG + ".ConvertPatientVitals", ProjectConstant.ERROR_CODE.VITALDTOConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientDiagnosisDTO> ConvertPateintDiagnosisByPatientId(IDataReader objReader)
        {
            List<PatientDiagnosisDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<PatientDiagnosisDTO>();

                    while (objReader.Read())
                    {
                        PatientDiagnosisDTO obj = new PatientDiagnosisDTO();

                        if (objReader["PatientDiagnosisID"] != DBNull.Value)
                        {
                            obj.PatientDiagnosisID = int.Parse(objReader["PatientDiagnosisID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["ICDCodeID"] != DBNull.Value)
                        {
                            if (obj.ICDCode == null)
                                obj.ICDCode = new ICDCodeDTO();

                            obj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeID"].ToString());

                            if (objReader["ICDCodeDescription"] != DBNull.Value)
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();

                            if (objReader["ICDCodeName"] != DBNull.Value)
                                obj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();
                        }
                        if (objReader["ICDDescription"] != DBNull.Value)
                        {
                            obj.ICDCodeDescription = objReader["ICDDescription"].ToString();
                        }
                        if (objReader["ICDCode"] != DBNull.Value)
                        {
                            obj.ICDCodeName = objReader["ICDCode"].ToString();
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["DiagnosisNote"] != DBNull.Value)
                        {
                            obj.Note = objReader["DiagnosisNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            obj.IsChronic = bool.Parse(objReader["IsChronic"].ToString());
                        }
                        if (objReader["IsActive"] != DBNull.Value)
                        {
                            obj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }

                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
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
                throw new DTOConversionException(ProjectConstant.PATIENT_DIAGNOSIS_RESOLVED_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintDiagnosisByPatientId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientDiagnosisDTO> ConvertPateintChronicDiagnosisByPatientId(IDataReader objReader)
        {
            List<PatientDiagnosisDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<PatientDiagnosisDTO>();

                    while (objReader.Read())
                    {
                        PatientDiagnosisDTO obj = new PatientDiagnosisDTO();

                        if (objReader["PatientDiagnosisID"] != DBNull.Value)
                        {
                            obj.PatientDiagnosisID = int.Parse(objReader["PatientDiagnosisID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["ICDCodeID"] != DBNull.Value)
                        {
                            if (obj.ICDCode == null)
                                obj.ICDCode = new ICDCodeDTO();

                            obj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeID"].ToString());


                            if (objReader["ICDCustomDescription"] != DBNull.Value)
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCustomDescription"].ToString();
                            else if (objReader["ICDCodeDescription"] != DBNull.Value)
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();

                            if (objReader["ICDCodeName"] != DBNull.Value)
                                obj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();
                        }
                        if (objReader["ICDDescription"] != DBNull.Value)
                        {
                            obj.ICDCodeDescription = objReader["ICDDescription"].ToString();
                        }
                        if (objReader["ICDCode"] != DBNull.Value)
                        {
                            obj.ICDCodeName = objReader["ICDCode"].ToString();
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["DiagnosisNote"] != DBNull.Value)
                        {
                            obj.Note = objReader["DiagnosisNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            obj.IsChronic = bool.Parse(objReader["IsChronic"].ToString());
                        }
                        if (objReader["IsActive"] != DBNull.Value)
                        {
                            obj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }

                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            obj.RCOPIAID = objReader["RCOPIAID"].ToString();
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
                throw new DTOConversionException(ProjectConstant.PATIENT_DIAGNOSIS_RESOLVED_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintDiagnosisByPatientId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientDiagnosisDTO> ConvertPateintAcuteDiagnosisByPatientId(IDataReader objReader)
        {
            List<PatientDiagnosisDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<PatientDiagnosisDTO>();

                    while (objReader.Read())
                    {
                        PatientDiagnosisDTO obj = new PatientDiagnosisDTO();

                        if (objReader["PatientDiagnosisID"] != DBNull.Value)
                        {
                            obj.PatientDiagnosisID = int.Parse(objReader["PatientDiagnosisID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }

                        if (objReader["ICDCodeID"] != DBNull.Value)
                        {
                            if (obj.ICDCode == null)
                                obj.ICDCode = new ICDCodeDTO();

                            obj.ICDCode.IcdCodeIdSeq = int.Parse(objReader["ICDCodeID"].ToString());

                            if (objReader["ICDCustomDescription"] != DBNull.Value)
                            {
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCustomDescription"].ToString();
                            }
                            else if (objReader["ICDCodeDescription"] != DBNull.Value)
                                obj.ICDCode.IcdCodeDescription = objReader["ICDCodeDescription"].ToString();

                            if (objReader["ICDCodeName"] != DBNull.Value)
                                obj.ICDCode.IcdCodeName = objReader["ICDCodeName"].ToString();
                        }
                        if (objReader["ICDDescription"] != DBNull.Value)
                        {
                            obj.ICDCodeDescription = objReader["ICDDescription"].ToString();
                        }
                        if (objReader["ICDCode"] != DBNull.Value)
                        {
                            obj.ICDCodeName = objReader["ICDCode"].ToString();
                        }

                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["DiagnosisNote"] != DBNull.Value)
                        {
                            obj.Note = objReader["DiagnosisNote"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            obj.IsChronic = bool.Parse(objReader["IsChronic"].ToString());
                        }
                        if (objReader["IsActive"] != DBNull.Value)
                        {
                            obj.IsActive = bool.Parse(objReader["IsActive"].ToString());
                        }

                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["RCOPIAID"] != DBNull.Value)
                        {
                            obj.RCOPIAID = objReader["RCOPIAID"].ToString();
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
                throw new DTOConversionException(ProjectConstant.PATIENT_DIAGNOSIS_RESOLVED_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintDiagnosisByPatientId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientRecommendationDTO> PatientRecommendationConverter(IDataReader objReader)
        {

            List<PatientRecommendationDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<PatientRecommendationDTO>();

                    while (objReader.Read())
                    {
                        PatientRecommendationDTO obj = new PatientRecommendationDTO();

                        if (objReader["PatientRecommendationID"] != DBNull.Value)
                        {
                            obj.PatientRecommendationID = int.Parse(objReader["PatientRecommendationID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        }
                        if (objReader["RecommendationPlanID"] != DBNull.Value)
                        {
                            if (obj.Plan == null)
                                obj.Plan = new PlanTypeDTO();

                            obj.Plan.RecommendationPlanTypeID = int.Parse(objReader["RecommendationPlanID"].ToString());
                        }

                        if (objReader["RecommendationMethodID"] != DBNull.Value)
                        {
                            if (obj.Method == null)
                                obj.Method = new MethodTypeDTO();

                            obj.Method.RecommendationMethodTypeID = int.Parse(objReader["RecommendationMethodID"].ToString());
                        }

                        if (objReader["RecommendationLink"] != DBNull.Value)
                        {
                            //if (obj.Plan == null)
                            //    obj.Plan = new PlanTypeDTO();

                            obj.Links = objReader["RecommendationLink"].ToString();
                        }

                        if (objReader["RecommendationDurationID"] != DBNull.Value)
                        {
                            if (obj.Duration == null)
                                obj.Duration = new DurationTypeDTO();

                            obj.Duration.RecommendationDurationTypeID = int.Parse(objReader["RecommendationDurationID"].ToString());
                        }

                        if (objReader["IsDiscussedWithPatient"] != DBNull.Value)
                        {
                            obj.IsDiscussedWithPatient = objReader["IsDiscussedWithPatient"].ToString();
                        }



                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Note = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }


                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        //if (objReader["RCOPIAID"] != DBNull.Value)
                        //{
                        //    obj.RCOPIAID = objReader["RCOPIAID"].ToString();
                        //}

                        if (objReader["Description"] != DBNull.Value)
                        {
                            obj.Description = objReader["Description"].ToString();
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_DIAGNOSIS_RESOLVED_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintDiagnosisByPatientId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ModuleTypeDTO> ModuleTypeConverter(IDataReader objReader)
        {
            List<ModuleTypeDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<ModuleTypeDTO>();

                    while (objReader.Read())
                    {
                        ModuleTypeDTO obj = new ModuleTypeDTO();

                        if (objReader["ModuleTypeID"] != DBNull.Value)
                        {
                            obj.ModuleTypeId = int.Parse(objReader["ModuleTypeID"].ToString());
                        }
                        if (objReader["ModuleName"] != DBNull.Value)
                        {
                            obj.ModuleName = objReader["ModuleName"].ToString();
                        }
                        if (objReader["ModuleDescription"] != DBNull.Value)
                        {
                            obj.Description = objReader["ModuleDescription"].ToString();
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.ModuleTypeConverter + ".ConvertPateintDiagnosisByPatientId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 05 May 2011
        /// Merthod Will Convert Data Reader to Procedure DTO 
        /// DTOTOVO Converter
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        internal static List<ProcedureDTO> ConvertPateintProcedureByPatientId(IDataReader objReader)
        {
            List<ProcedureDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ProcedureDTO>();

                    while (objReader.Read())
                    {
                        ProcedureDTO obj = new ProcedureDTO();

                        if (objReader["PatientProcedureID"] != DBNull.Value)
                        {
                            obj.PatientProcedureID = int.Parse(objReader["PatientProcedureID"].ToString());
                        }

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }
                        if (objReader["VisitID"] != DBNull.Value)
                        {
                            obj.VisitID = int.Parse(objReader["VisitID"].ToString());
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
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        }
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["CreatedBy"] != DBNull.Value)
                        {
                            obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        }
                        if (objReader["CreatedAt"] != DBNull.Value)
                        {
                            obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        }
                        if (objReader["ModifiedBy"] != DBNull.Value)
                        {
                            obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        }
                        if (objReader["ModifiedAt"] != DBNull.Value)
                        {
                            obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        }
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
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
                throw new DTOConversionException(ProjectConstant.PATIENT_PROCEDURE_DTO_CONVERSION_ERROR_MSG + ".ConvertPateintProcedureByPatientId", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
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

        internal static List<SocialHistoryDTO> PatientSocialHistorByPatientIdyConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<SocialHistoryDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<SocialHistoryDTO>();

                    while (objReader.Read())
                    {
                        SocialHistoryDTO obj = new SocialHistoryDTO();

                        //obj.PatientSocialHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientSocialHistoryID", obj.PatientSocialHistoryID, IsTemplateVisit);
                        if (objReader["PatientSocialHistoryID"] != DBNull.Value)
                        {
                            obj.PatientSocialHistoryID = int.Parse(objReader["PatientSocialHistoryID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        if (objReader["IsWorkStatusYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsWorkStatusYesSelected"].ToString());
                            if (result)
                            {
                                obj.IsWorkStatusYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                obj.IsWorkStatusYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }


                        if (objReader["WorkStatusNotes"] != DBNull.Value)
                        {
                            obj.WorkStatusNotes = objReader["WorkStatusNotes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }
                        if (objReader["MaritalStatusID"] != DBNull.Value)
                        {
                            obj.MaritalStatusID = int.Parse(objReader["MaritalStatusID"].ToString());
                        }

                        if (objReader["IsOtherYesSelected"] != DBNull.Value)
                        {
                            bool result = bool.Parse(objReader["IsOtherYesSelected"].ToString());
                            if (result)
                            {
                                obj.IsOtherYesSelected = ProjectConstant.SELECTION_TRUE;
                            }
                            else
                            {
                                obj.IsOtherYesSelected = ProjectConstant.SELECTION_False;
                            }
                        }
                        if (objReader["OtherDictationID"] != DBNull.Value)
                        {
                            obj.OtherDictationID = Guid.Parse(objReader["OtherDictationID"].ToString());
                        }
                        if (objReader["WorkStatusDictationID"] != DBNull.Value)
                        {
                            obj.WorkStatusDictationID = Guid.Parse(objReader["WorkStatusDictationID"].ToString());
                        }

                        if (objReader["OtherNotes"] != DBNull.Value)
                        {
                            obj.OtherNotes = objReader["OtherNotes"].ToString();
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);
                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".PatientSocialHistoryConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ImmunizationDTO> ImmunizationPatientIdConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<ImmunizationDTO> patientImmunizationList = null;
            try
            {
                if (objReader != null)
                {
                    patientImmunizationList = new List<ImmunizationDTO>();
                    {
                        while (objReader.Read())
                        {
                            ImmunizationDTO objImmunization = new ImmunizationDTO();

                            objImmunization.PatientImmunizationId = GetKeyAsNullIfTemplate(objReader, "PatientImmunizationID", objImmunization.PatientImmunizationId, IsTemplateVisit);
                            //objImmunization.PatientImmunizationId = int.Parse(objReader["PatientImmunizationID"].ToString());

                            objImmunization.ImmunizationType = new ImmunizationTypeDTO();
                            objImmunization.ImmunizationType.ImmunizationTypeId = int.Parse(objReader["ImmunizationTypeID"].ToString());

                            objImmunization.VisitId = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, objImmunization.VisitId, IsTemplateVisit);

                            //  objImmunization.VisitId = int.Parse(objReader["VisitID"].ToString());

                            if (objReader["ImmunizationTypeName"] != DBNull.Value)
                            {
                                objImmunization.ImmunizationType.ImmunizationTypeName = objReader["ImmunizationTypeName"].ToString();
                            }

                            if (objReader["ImmunizationNote"] != DBNull.Value)
                            {
                                objImmunization.Note = objReader["ImmunizationNote"].ToString();
                            }

                            if (objReader["NoteSubject"] != DBNull.Value)
                            {
                                objImmunization.NoteSubject = objReader["NoteSubject"].ToString();
                            }

                            if (objReader["Dose"] != DBNull.Value)
                            {
                                objImmunization.Dose = objReader["Dose"].ToString();
                            }

                            if (objReader["LotNumber"] != DBNull.Value)
                            {
                                objImmunization.LotNumber = objReader["LotNumber"].ToString();
                            }

                            if (objReader["DateAdmin"] != DBNull.Value)
                            {
                                objImmunization.DateAdmin = objReader["DateAdmin"].ToString();
                            }

                            if (objReader["NextDoseDue"] != DBNull.Value)
                            {
                                objImmunization.NextDoseDue = objReader["NextDoseDue"].ToString();
                            }

                            if (objReader["IsRefusal"] != DBNull.Value)
                            {
                                objImmunization.IsRefusal = bool.Parse(objReader["IsRefusal"].ToString());
                            }

                            if (objReader["RefusalReason"] != DBNull.Value)
                            {
                                objImmunization.RefusalReason = objReader["RefusalReason"].ToString();
                            }

                            if (objReader["Site"] != DBNull.Value)
                            {
                                objImmunization.Site = objReader["Site"].ToString();
                            }

                            if (objReader["Series"] != DBNull.Value)
                            {
                                objImmunization.Series = objReader["Series"].ToString();
                            }

                            if (objReader["Manufacturer"] != DBNull.Value)
                            {
                                objImmunization.Manufacturer = objReader["Manufacturer"].ToString();
                            }

                            if (objReader["Route"] != DBNull.Value)
                            {
                                objImmunization.Rout = objReader["Route"].ToString();
                            }

                            if (objReader["VisitDate"] != DBNull.Value)
                            {
                                objImmunization.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                            }
                            if (objReader["IsRecordEditable"] != DBNull.Value)
                            {
                                objImmunization.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                            }
                            objImmunization.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, objImmunization.DictationID, IsTemplateVisit);
                            //if (objReader["DictationID"] != DBNull.Value)
                            //{
                            //    objImmunization.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                            //}



                            patientImmunizationList.Add(objImmunization);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuivusLogger.Error(ex);
                throw new DTOConversionException(ProjectConstant.VISIT_DTO_CONVERSION_ERROR_MSG + ".ImmunizationConverter", ProjectConstant.ERROR_CODE.VisitDTOConversion.ToString());
            }

            return patientImmunizationList;
        }

        internal static List<InHouseLabDTO> ConvertInHouseLabsByPatientId(IDataReader objReader, bool IsTemplateVisit)
        {
            List<InHouseLabDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<InHouseLabDTO>();

                    while (objReader.Read())
                    {
                        InHouseLabDTO obj = new InHouseLabDTO();

                        obj.PatientInHouseLabTestID = GetKeyAsNullIfTemplate(objReader, "PatientInHouseLabTestID", obj.PatientInHouseLabTestID, IsTemplateVisit);

                        //if (objReader["PatientInHouseLabTestID"] != DBNull.Value)
                        //{
                        //    obj.PatientInHouseLabTestID = int.Parse(objReader["PatientInHouseLabTestID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}
                        if (objReader["InHouseLabTestTypeID"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.InHouseLabTestTypeID = int.Parse(objReader["InHouseLabTestTypeID"].ToString());
                        }
                        if (objReader["TestName"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.TestName = objReader["TestName"].ToString();
                        }

                        if (objReader["TestCode"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.TestCode = objReader["TestCode"].ToString();
                        }

                        if (objReader["Description"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.Description = objReader["Description"].ToString();
                        }
                        if (objReader["TestRange"] != DBNull.Value)
                        {
                            if (obj.InHouseLabTestType == null)
                                obj.InHouseLabTestType = new InHouseLabTestTypeDTO();

                            obj.InHouseLabTestType.TestRange = objReader["TestRange"].ToString();
                        }
                        if (objReader["TestResult"] != DBNull.Value)
                        {
                            obj.TestResult = objReader["TestResult"].ToString();
                        }
                        if (objReader["ResultAttachment"] != DBNull.Value)
                        {
                            obj.ResultAttachment = (byte[])objReader["ResultAttachment"];
                        }

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }


                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
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
                throw new DTOConversionException(ProjectConstant.PATIENT_IN_HOUSE_LABS_DTO_CONVERSION_ERROR_MSG + ".ConvertInHouseLabs", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PatientRecommendationDTO> ConvertPatientRecommendationByPatientId(IDataReader objReader, bool IsTemplateVisit)
        {
            List<PatientRecommendationDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientRecommendationDTO>();

                    while (objReader.Read())
                    {

                        PatientRecommendationDTO obj = new PatientRecommendationDTO();

                        obj.PatientRecommendationID = GetKeyAsNullIfTemplate(objReader, "PatientRecommendationID", obj.PatientRecommendationID, IsTemplateVisit);
                        //if (objReader["PatientRecommendationID"] != DBNull.Value)
                        //{
                        //    obj.PatientRecommendationID = int.Parse(objReader["PatientRecommendationID"].ToString());
                        //}

                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);
                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        if (objReader["RecommendationPlanID"] != DBNull.Value)
                        {
                            if (obj.Plan == null)
                                obj.Plan = new PlanTypeDTO();
                            obj.Plan.RecommendationPlanTypeID = int.Parse(objReader["RecommendationPlanID"].ToString());
                            obj.Plan.PlanName = objReader["PlanName"].ToString();
                        }

                        if (objReader["RecommendationMethodID"] != DBNull.Value)
                        {
                            if (obj.Method == null)
                                obj.Method = new MethodTypeDTO();
                            obj.Method.RecommendationMethodTypeID = int.Parse(objReader["RecommendationMethodID"].ToString());
                            obj.Method.MethodName = objReader["MethodName"].ToString();
                        }
                        if (objReader["RecommendationDurationID"] != DBNull.Value)
                        {
                            if (obj.Duration == null)
                                obj.Duration = new DurationTypeDTO();
                            obj.Duration.RecommendationDurationTypeID = int.Parse(objReader["RecommendationDurationID"].ToString());
                            obj.Duration.DurationName = objReader["DurationName"].ToString();
                        }
                        if (objReader["RecommendationLink"] != DBNull.Value)
                        {
                            if (obj.Links == null)

                                obj.Links = objReader["RecommendationLink"].ToString();
                        }



                        if (objReader["IsDiscussedWithPatient"] != DBNull.Value)
                        {
                            bool? IsDiscussedWithPatient = null;
                            IsDiscussedWithPatient = bool.Parse(objReader["IsDiscussedWithPatient"].ToString());

                            if (IsDiscussedWithPatient == null)
                                obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_NULL;
                            else if (IsDiscussedWithPatient == true)
                                obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_TRUE;
                            else if (IsDiscussedWithPatient == false)
                                obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            obj.IsDiscussedWithPatient = ProjectConstant.SELECTION_NULL;
                        }

                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Note = objReader["Notes"].ToString();
                        }

                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);

                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }
                        if (objReader["Description"] != DBNull.Value)
                        {
                            obj.Description = objReader["Description"].ToString();
                        }
                        if (objReader["DiagnosisInfo"] != DBNull.Value)
                        {
                            obj.DiagnosisInfo = objReader["DiagnosisInfo"].ToString();
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.MEDICAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".LoadPatientRecommendationByVisit", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static Quivus.Model.QuickInput.FollowUp.PatientFollowUpDTO ConvertToPatientFollowUpByPatientId(IDataReader objReader, bool IsTemplateVisit)
        {
            PatientFollowUpDTO followUp = null;
            try
            {
                if (objReader != null && objReader.Read())
                {

                    followUp = new PatientFollowUpDTO();

                    followUp.PatientFollowUpID = GetKeyAsNullIfTemplate(objReader, "PatientFollowUpID", followUp.PatientFollowUpID, IsTemplateVisit);
                    //if (objReader["PatientFollowUpID"] != DBNull.Value)
                    //{
                    //    followUp.PatientFollowUpID = int.Parse(objReader["PatientFollowUpID"].ToString());
                    //}

                    if (objReader["PatientID"] != DBNull.Value)
                    {
                        followUp.PatientID = int.Parse(objReader["PatientID"].ToString());
                    }

                    followUp.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, followUp.VisitID, IsTemplateVisit);

                    //if (objReader["VisitID"] != DBNull.Value)
                    //{
                    //    followUp.VisitID = int.Parse(objReader["VisitID"].ToString());
                    //}


                    if (objReader["FollowupMethodTypeID"] != DBNull.Value)
                    {
                        followUp.FollowUpMethodType = new Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp.FollowUpMethodTypeDTO();

                        followUp.FollowUpMethodType.FollowUpMethodTypeID = int.Parse(objReader["FollowupMethodTypeID"].ToString());
                        if (objReader["MethodName"] != DBNull.Value)
                        {
                            followUp.FollowUpMethodType.MethodName = objReader["MethodName"].ToString();
                        }

                    }

                    if (objReader["IsByDate"] != DBNull.Value)
                    {
                        followUp.IsByDate = bool.Parse(objReader["IsByDate"].ToString());
                    }

                    if (objReader["FollowupDate"] != DBNull.Value)
                    {
                        followUp.FollowUpDate = objReader["FollowupDate"].ToString();
                    }

                    if (objReader["IsOnCompletion"] != DBNull.Value)
                    {
                        followUp.IsOnCompletion = bool.Parse(objReader["IsOnCompletion"].ToString());
                    }

                    if (objReader["IsLabTestSelected"] != DBNull.Value)
                    {
                        followUp.IsLabTestSelected = bool.Parse(objReader["IsLabTestSelected"].ToString());
                    }

                    if (objReader["IsDrugselected"] != DBNull.Value)
                    {
                        followUp.IsDrugselected = bool.Parse(objReader["IsDrugselected"].ToString());
                    }

                    if (objReader["LabTestID"] != DBNull.Value)
                    {
                        followUp.LabTestID = int.Parse(objReader["LabTestID"].ToString());
                    }


                    if (objReader["DrugID"] != DBNull.Value)
                    {
                        followUp.DrugID = int.Parse(objReader["DrugID"].ToString());
                    }

                    followUp.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, followUp.CreatedBy, IsTemplateVisit);

                    //if (objReader["CreatedBy"] != DBNull.Value)
                    //{
                    //    followUp.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                    //}

                    followUp.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, followUp.CreatedAt, IsTemplateVisit);
                    //if (objReader["CreatedAt"] != DBNull.Value)
                    //{
                    //    followUp.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                    //}

                    followUp.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, followUp.ModifiedBy, IsTemplateVisit);
                    //if (objReader["ModifiedBy"] != DBNull.Value)
                    //{
                    //    followUp.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                    //}

                    followUp.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, followUp.ModifiedAt, IsTemplateVisit);
                    //if (objReader["ModifiedAt"] != DBNull.Value)
                    //{
                    //    followUp.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                    //}

                    if (objReader["VisitDate"] != DBNull.Value)
                    {
                        followUp.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                    }

                    if (objReader["IsDeleted"] != DBNull.Value)
                    {
                        followUp.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                    }
                    if (objReader["IsRecordEditable"] != DBNull.Value)
                    {
                        followUp.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.VIEW_BACKGROUND_DTO_CONVERSION_ERROR_MSG + ".ConvertToPatientFollowUp", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return followUp;
        }

        internal static List<ModuleDictationDTO> PatientModuleDictationConverter(IDataReader objReader, bool IsTemplateVisit)
        {

            List<ModuleDictationDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ModuleDictationDTO>();

                    while (objReader.Read())
                    {
                        ModuleDictationDTO obj = new ModuleDictationDTO();

                        obj.ModuleDictationId = GetKeyAsNullIfTemplate(objReader, "ModuleDictationID", obj.ModuleDictationId, IsTemplateVisit);


                        if (objReader["ModuleTypeID"] != DBNull.Value)
                        {
                            obj.ModuleTypeID = int.Parse(objReader["ModuleTypeID"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());

                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MODULE_DICATION_DTO_CONVERSION_ERROR_MSG + ".PatientModuleDictationConverter", ProjectConstant.ERROR_CODE.ModuleDictationConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ModuleNoteDTO> PatientModuleNotesConverter(IDataReader objReader, bool IsTemplateVisit)
        {

            List<ModuleNoteDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ModuleNoteDTO>();

                    while (objReader.Read())
                    {
                        ModuleNoteDTO obj = new ModuleNoteDTO();

                        obj.ModuleNoteId = GetKeyAsNullIfTemplate(objReader, "ModuleNoteID", obj.ModuleNoteId, IsTemplateVisit);


                        if (objReader["ModuleTypeID"] != DBNull.Value)
                        {
                            obj.ModuleTypeId = int.Parse(objReader["ModuleTypeID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);

                        if (objReader["Note"] != DBNull.Value)
                        {
                            obj.Note = objReader["Note"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_MODULE_NOTES_DTO_CONVERSION_ERROR_MSG + ".PatientModuleNotesConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

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

                        LabObj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, LabObj.VisitID, IsTemplateVisit);

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
                                LabObj.IsFasting = ProjectConstant.SELECTION_NULL;
                            else if (IsFasting == true)
                                LabObj.IsFasting = ProjectConstant.SELECTION_TRUE;
                            else if (IsFasting == false)
                                LabObj.IsFasting = ProjectConstant.SELECTION_False;
                        }
                        else
                        {
                            LabObj.IsFasting = ProjectConstant.SELECTION_NULL;
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

                        LabObj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, LabObj.DictationID, IsTemplateVisit);

                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    LabObj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}

                        LabObj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, LabObj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedBy = int.Parse(objReader["CreatedBy"].ToString());
                        //}

                        LabObj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, LabObj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    LabObj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        LabObj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, LabObj.ModifiedBy, IsTemplateVisit);
                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    LabObj.ModifiedBy = int.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        LabObj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, LabObj.ModifiedAt, IsTemplateVisit);

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
                throw new DTOConversionException(ProjectConstant.PATIENT_LABS_DTO_CONVERSION_ERROR_MSG + ".PatientLabsConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return HistoryList;
        }

        internal static List<PatientFollowupInstructionDTO> FollowupInstructionByPatientIdConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<PatientFollowupInstructionDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PatientFollowupInstructionDTO>();

                    while (objReader.Read())
                    {
                        PatientFollowupInstructionDTO obj = new PatientFollowupInstructionDTO();


                        obj.FollowupInstructionID = GetKeyAsNullIfTemplate(objReader, "FollowupInstructionID", obj.FollowupInstructionID, IsTemplateVisit);

                        //if (objReader["FollowupInstructionID"] != DBNull.Value)
                        //{
                        //    obj.FollowupInstructionID = int.Parse(objReader["FollowupInstructionID"].ToString());
                        //}
                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }
                        if (objReader["PatientID"] != DBNull.Value)
                        {
                            obj.PatientID = int.Parse(objReader["PatientID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);

                        //if (objReader["VisitID"] != DBNull.Value)
                        //{
                        //    obj.VisitID = int.Parse(objReader["VisitID"].ToString());
                        //}

                        //if (objReader["FollowupServiceTypeId"] != DBNull.Value)
                        //{
                        //    if (obj.objServiceTypeDTO == null)
                        //        obj.objServiceTypeDTO = new ServiceTypeDTO();

                        //    obj.objServiceTypeDTO.FollowupServiceTypeId = int.Parse(objReader["FollowupServiceTypeId"].ToString());

                        //    if (objReader["FollowupServiceTypeName"] != DBNull.Value)
                        //        obj.objServiceTypeDTO.FollowupServiceTypeName = objReader["FollowupServiceTypeName"].ToString();

                        //}
                        if (objReader["FollowupService"] != DBNull.Value)
                        {
                            if (obj.objServiceTypeDTO == null)
                                obj.objServiceTypeDTO = new ServiceTypeDTO();
                            obj.objServiceTypeDTO.FollowupService = objReader["FollowupService"].ToString();
                        }

                        if (objReader["AgeTypeID"] != DBNull.Value)
                        {
                            if (obj.AgeTypeDTO == null)
                                obj.AgeTypeDTO = new InstructionAgeTypeDTO();

                            obj.AgeTypeDTO.InstructionAgeTypeID = int.Parse(objReader["AgeTypeID"].ToString());

                            if (objReader["AgeTypeName"] != DBNull.Value)
                                obj.AgeTypeDTO.AgeTypeName = objReader["AgeTypeName"].ToString();

                        }

                        if (objReader["AgeValueTypeID"] != DBNull.Value)
                        {
                            if (obj.AgeValueTypeDTO == null)
                                obj.AgeValueTypeDTO = new InstructionAgeValueTypeDTO();

                            obj.AgeValueTypeDTO.InstructionAgeValueTypeID = int.Parse(objReader["AgeValueTypeID"].ToString());

                            if (objReader["AgeValueTypeName"] != DBNull.Value)
                                obj.AgeValueTypeDTO.AgeValueTypeName = objReader["AgeValueTypeName"].ToString();
                        }


                        if (objReader["AfterDurationTypeID"] != DBNull.Value)
                        {
                            if (obj.AfterInstructionDurationTypeDTO == null)
                                obj.AfterInstructionDurationTypeDTO = new InstructionDurationTypeDTO();

                            obj.AfterInstructionDurationTypeDTO.DurationTypeID = int.Parse(objReader["AfterDurationTypeID"].ToString());

                            if (objReader["AfterDurationTypeName"] != DBNull.Value)
                                obj.AfterInstructionDurationTypeDTO.DurationTypeName = objReader["AfterDurationTypeName"].ToString();

                            if (objReader["AfterDurationValue"] != DBNull.Value)
                            {
                                obj.AfterDurationValue = int.Parse(objReader["AfterDurationValue"].ToString());
                            }
                        }

                        if (objReader["ForDurationTypeID"] != DBNull.Value)
                        {
                            if (obj.ForInstructionDurationTypeDTO == null)
                                obj.ForInstructionDurationTypeDTO = new InstructionDurationTypeDTO();

                            obj.ForInstructionDurationTypeDTO.DurationTypeID = int.Parse(objReader["ForDurationTypeID"].ToString());

                            if (objReader["ForDurationTypeName"] != DBNull.Value)
                                obj.ForInstructionDurationTypeDTO.DurationTypeName = objReader["ForDurationTypeName"].ToString();

                            if (objReader["ForDurationValue"] != DBNull.Value)
                            {
                                obj.ForDurationValue = int.Parse(objReader["ForDurationValue"].ToString());
                            }
                        }

                        obj.IsAgeSelected = bool.Parse(objReader["IsAgeSelected"].ToString());
                        obj.IsDurationSelected = bool.Parse(objReader["IsDurationSelected"].ToString());

                        obj.DictationID = GetDictationGUIDAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_DICTATION_ID, obj.DictationID, IsTemplateVisit);
                        //if (objReader["DictationID"] != DBNull.Value)
                        //{
                        //    obj.DictationID = Guid.Parse(objReader["DictationID"].ToString());
                        //}
                        if (objReader["Notes"] != DBNull.Value)
                        {
                            obj.Notes = objReader["Notes"].ToString();
                        }
                        if (objReader["NoteSubject"] != DBNull.Value)
                        {
                            obj.NoteSubject = objReader["NoteSubject"].ToString();
                        }

                        obj.CreatedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_BY, obj.CreatedBy, IsTemplateVisit);

                        //if (objReader["CreatedBy"] != DBNull.Value)
                        //{
                        //    obj.CreatedBy = Int32.Parse(objReader["CreatedBy"].ToString());
                        //}

                        obj.CreatedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_CREATED_AT, obj.CreatedAt, IsTemplateVisit);
                        //if (objReader["CreatedAt"] != DBNull.Value)
                        //{
                        //    obj.CreatedAt = DateTime.Parse(objReader["CreatedAt"].ToString());
                        //}

                        obj.ModifiedBy = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_BY, obj.ModifiedBy, IsTemplateVisit);

                        //if (objReader["ModifiedBy"] != DBNull.Value)
                        //{
                        //    obj.ModifiedBy = Int32.Parse(objReader["ModifiedBy"].ToString());
                        //}

                        obj.ModifiedAt = GetDateAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_MODIFIED_AT, obj.ModifiedAt, IsTemplateVisit);
                        //if (objReader["ModifiedAt"] != DBNull.Value)
                        //{
                        //    obj.ModifiedAt = DateTime.Parse(objReader["ModifiedAt"].ToString());
                        //}
                        if (objReader["IsDeleted"] != DBNull.Value)
                        {
                            obj.IsDeleted = bool.Parse(objReader["IsDeleted"].ToString());
                        }
                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.StackTrace);
                throw new DTOConversionException(ProjectConstant.PATIENT_FOLLOW_UP_INSTRUCTION_DTO_CONVERSION_ERROR_MSG + ".FollowupInstructionByPatientIdConverter", ProjectConstant.ERROR_CODE.FollowupLabsConversion.ToString());
            }
            return TypeList;
        }


        internal static List<SocialhistoryVBDTO> SocialHistorByPatientIdyConverter(IDataReader objReader, bool IsTemplateVisit)
        {
            List<SocialhistoryVBDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<SocialhistoryVBDTO>();

                    while (objReader.Read())
                    {
                        SocialhistoryVBDTO obj = new SocialhistoryVBDTO();

                        //obj.PatientSocialHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientSocialHistoryID", obj.PatientSocialHistoryID, IsTemplateVisit);
                        if (objReader["PatientSocialHistoryID"] != DBNull.Value)
                        {
                            obj.PatientSocialHistoryID = int.Parse(objReader["PatientSocialHistoryID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);


                        if (objReader["Social"] != DBNull.Value)
                        {
                            obj.Social = objReader["Social"].ToString();
                        }
                        if (objReader["Status"] != DBNull.Value)
                        {
                            obj.Status = objReader["Status"].ToString();
                        }


                        //if (objReader["WorkStatusNotes"] != DBNull.Value)
                        //{
                        //    obj.WorkStatusNotes = objReader["WorkStatusNotes"].ToString();
                        //}
                        if (objReader["Note"] != DBNull.Value)
                        {
                            obj.Note = objReader["Note"].ToString();
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationId = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["Current"] != DBNull.Value)
                        {
                            obj.Current = objReader["Current"].ToString();
                        }
                        if (objReader["Type"] != DBNull.Value)
                        {
                            obj.Type = objReader["Type"].ToString();
                        }
                        if (objReader["Amount"] != DBNull.Value)
                        {
                            obj.Amount = objReader["Amount"].ToString();
                        }
                        if (objReader["Educated"] != DBNull.Value)
                        {
                            obj.Educated = objReader["Educated"].ToString();
                        }
                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["EndDate"] != DBNull.Value)
                        {
                            obj.EndDate = objReader["EndDate"].ToString();
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
                throw new DTOConversionException(ProjectConstant.SOCIAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".SocialHistorByPatientIdyConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<SocialhistoryVBDTO> SocialHistorByPatientIdyConverter(IDataReader objReader, bool IsTemplateVisit, int PatientId)
        {
            List<SocialhistoryVBDTO> TypeList = null;
            try
            {

                if (objReader != null)
                {
                    TypeList = new List<SocialhistoryVBDTO>();

                    while (objReader.Read())
                    {
                        SocialhistoryVBDTO obj = new SocialhistoryVBDTO();

                        //obj.PatientSocialHistoryID = GetKeyAsNullIfTemplate(objReader, "PatientSocialHistoryID", obj.PatientSocialHistoryID, IsTemplateVisit);
                        if (objReader["PatientSocialHistoryID"] != DBNull.Value)
                        {
                            obj.PatientSocialHistoryID = int.Parse(objReader["PatientSocialHistoryID"].ToString());
                        }

                        obj.VisitID = GetKeyAsNullIfTemplate(objReader, ProjectConstant.PROPERTY_VISIT_ID, obj.VisitID, IsTemplateVisit);


                        if (objReader["Social"] != DBNull.Value)
                        {
                            obj.Social = objReader["Social"].ToString();
                        }
                        if (objReader["Status"] != DBNull.Value)
                        {
                            obj.Status = objReader["Status"].ToString();
                        }


                        //if (objReader["WorkStatusNotes"] != DBNull.Value)
                        //{
                        //    obj.WorkStatusNotes = objReader["WorkStatusNotes"].ToString();
                        //}
                        if (objReader["Note"] != DBNull.Value)
                        {
                            obj.Note = objReader["Note"].ToString();
                        }
                        if (objReader["DictationID"] != DBNull.Value)
                        {
                            obj.DictationId = Guid.Parse(objReader["DictationID"].ToString());
                        }

                        if (objReader["VisitDate"] != DBNull.Value)
                        {
                            obj.VisitDate = DateTime.Parse(objReader["VisitDate"].ToString());
                        }

                        if (objReader["Current"] != DBNull.Value)
                        {
                            obj.Current = objReader["Current"].ToString();
                        }
                        if (objReader["Type"] != DBNull.Value)
                        {
                            obj.Type = objReader["Type"].ToString();
                        }
                        if (objReader["Amount"] != DBNull.Value)
                        {
                            obj.Amount = objReader["Amount"].ToString();
                        }
                        if (objReader["Educated"] != DBNull.Value)
                        {
                            obj.Educated = objReader["Educated"].ToString();
                        }
                        if (objReader["OnSet"] != DBNull.Value)
                        {
                            obj.OnSet = objReader["OnSet"].ToString();
                        }
                        if (objReader["EndDate"] != DBNull.Value)
                        {
                            obj.EndDate = objReader["EndDate"].ToString();
                        }

                        if (objReader["IsRecordEditable"] != DBNull.Value)
                        {
                            obj.IsRecordEditable = bool.Parse(objReader["IsRecordEditable"].ToString());
                        }

                        obj.PatientID = PatientId;

                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.SOCIAL_HISTORY_DTO_CONVERSION_ERROR_MSG + ".SocialHistorByPatientIdyConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ROSSubLevelCategoryTypeDTO> ROSSubLevelCategoryTypeConverter(IDataReader objReader)
        {
            List<ROSSubLevelCategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<ROSSubLevelCategoryTypeDTO>();

                    while (objReader.Read())
                    {
                        ROSSubLevelCategoryTypeDTO obj = new ROSSubLevelCategoryTypeDTO();

                        if (objReader["ROSSubLevel3CategoryTypeID"] != DBNull.Value)
                        {
                            obj.ROSSubLevel3CategoryTypeID = int.Parse(objReader["ROSSubLevel3CategoryTypeID"].ToString());
                        }

                        if (objReader["ROSSubCategoryTypeID"] != DBNull.Value)
                        {
                            obj.ROSSubCategoryTypeID = int.Parse(objReader["ROSSubCategoryTypeID"].ToString());
                        }
                        if (objReader["SubLevel3CategoryName"] != DBNull.Value)
                        {
                            obj.SubLevel3CategoryName = objReader["SubLevel3CategoryName"].ToString();
                        }
                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }

                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_ROS_SUB_LEVEL_CATEGORY_CONVERSION_ERROR_MSG + ".ROSSubLevelCategoryTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PhysicalCategoryTypeDTO> PhysicalCategoryTypeConverter(IDataReader objReader)
        {
            List<PhysicalCategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PhysicalCategoryTypeDTO>();

                    while (objReader.Read())
                    {
                        PhysicalCategoryTypeDTO obj = new PhysicalCategoryTypeDTO();

                        if (objReader["PhysicalCategoryTypeID"] != DBNull.Value)
                        {
                            obj.PhysicalCategoryTypeID = int.Parse(objReader["PhysicalCategoryTypeID"].ToString());
                        }

                        if (objReader["ParentId"] != DBNull.Value)
                        {
                            obj.ParentId = int.Parse(objReader["ParentId"].ToString());
                        }
                        if (objReader["CategoryName"] != DBNull.Value)
                        {
                            obj.CategoryName = objReader["CategoryName"].ToString();
                        }
                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }

                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_ROS_SUB_LEVEL_CATEGORY_CONVERSION_ERROR_MSG + ".ROSSubLevelCategoryTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<PhysicalLevel3CategoryTypeDTO> PhysicalLevel3CategoryTypeConverter(IDataReader objReader)
        {
            List<PhysicalLevel3CategoryTypeDTO> TypeList = null;
            try
            {
                if (objReader != null)
                {
                    TypeList = new List<PhysicalLevel3CategoryTypeDTO>();

                    while (objReader.Read())
                    {
                        PhysicalLevel3CategoryTypeDTO obj = new PhysicalLevel3CategoryTypeDTO();

                        if (objReader["PhysicalSubLevel3CategoryTypeID"] != DBNull.Value)
                        {
                            obj.PhysicalSubLevel3CategoryTypeID = int.Parse(objReader["PhysicalSubLevel3CategoryTypeID"].ToString());
                        }

                        if (objReader["PhysicalSubCategoryTypeID"] != DBNull.Value)
                        {
                            obj.PhysicalSubCategoryTypeID = int.Parse(objReader["PhysicalSubCategoryTypeID"].ToString());
                        }
                        if (objReader["SubLevel3CategoryName"] != DBNull.Value)
                        {
                            obj.SubLevel3CategoryName = objReader["SubLevel3CategoryName"].ToString();
                        }
                        if (objReader["ShortName"] != DBNull.Value)
                        {
                            obj.ShortName = objReader["ShortName"].ToString();
                        }

                        if (objReader["IsMaleApplicable"] != DBNull.Value)
                        {
                            obj.IsMaleApplicable = bool.Parse(objReader["IsMaleApplicable"].ToString());
                        }
                        if (objReader["IsFemaleApplicable"] != DBNull.Value)
                        {
                            obj.IsFemaleApplicable = bool.Parse(objReader["IsFemaleApplicable"].ToString());
                        }
                        if (objReader["UIIndex"] != DBNull.Value)
                        {
                            obj.UIIndex = int.Parse(objReader["UIIndex"].ToString());
                        }
                        TypeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.PATIENT_ROS_SUB_LEVEL_CATEGORY_CONVERSION_ERROR_MSG + ".ROSSubLevelCategoryTypeConverter", ProjectConstant.ERROR_CODE.MedicalHistoryConversion.ToString());
            }
            return TypeList;
        }

        internal static List<ICDCodeDTO> ConvertToICDCodeDTO(IDataReader objReader)
        {
            List<ICDCodeDTO> typeList = null;
            try
            {
                if (objReader != null)
                {
                    typeList = new List<ICDCodeDTO>();

                    while (objReader.Read())
                    {
                        var obj = new ICDCodeDTO();

                        if (objReader["IcdCodeIdSeq"] != DBNull.Value)
                        {
                            obj.IcdCodeIdSeq = int.Parse(objReader["IcdCodeIdSeq"].ToString());
                        }

                        if (objReader["IcdCustomCodeDescription"] != DBNull.Value)
                        {
                            obj.IcdCodeDescription = objReader["IcdCustomCodeDescription"].ToString();
                        }
                        else if (objReader["IcdCodeDescription"] != DBNull.Value)
                        {
                            obj.IcdCodeDescription = objReader["IcdCodeDescription"].ToString();
                        }

                        if (objReader["IcdCodeName"] != DBNull.Value)
                        {
                            obj.IcdCodeName = objReader["IcdCodeName"].ToString();
                        }
                        if (objReader["IsChronic"] != DBNull.Value)
                        {
                            obj.IsChronic = objReader["IsChronic"].ToString();
                        }
                        if (objReader["ProblemType"] != DBNull.Value)
                        {
                            obj.ProblemType = objReader["ProblemType"].ToString();
                        }

                        if (objReader["SNOMEDCode"] != DBNull.Value)
                        {
                            obj.SNOMEDCode = objReader["SNOMEDCode"].ToString();
                        }
                        if (objReader["SNOMEDDescription"] != DBNull.Value)
                        {
                            obj.SNOMEDDescription = objReader["SNOMEDDescription"].ToString();
                        }
                        if (objReader["DiagnosisID"] != DBNull.Value)
                        {
                            obj.DiagnosisId = int.Parse(objReader["DiagnosisID"].ToString());
                        }

                        typeList.Add(obj);
                    }
                }
            }
            catch
            {
                throw new DTOConversionException(ProjectConstant.IcdcodeCodeConversionMsg
                    + " .ICDCODETypeConverter", " ");
            }
            return typeList;
        }
    }
}
