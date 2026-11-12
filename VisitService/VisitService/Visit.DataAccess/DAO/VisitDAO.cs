using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Quivus.Utility.Logger;
using Quivus.Utility;
using Quivus.Utility.CustomException;

using Visit.DataAccess.DB_Util;

using Quivus.Model.TypeDTOs;
using Quivus.Model.QuickInput;
using Quivus.Model.Visit;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.MedicalHistory;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.FamilyHistory;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Vital;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Labs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.HPI;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.PMH.ComprehensiveHistory.SocialHistory;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;
//using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Recommendation;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.FollowUp;
using Quivus.Model.QuickInput.Dictation;
using QuivusUtility.ProjectConstants;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.ROS;
using QuivusModel.TypeDTOs.QuickInputTypeDTOs.Physical.Physicals;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.TypeDTOs.QuickInputTypeDTOs.Assessment.Followup;
using QuivusModel.User.Patient;

namespace Visit.DataAccess.DAO
{
    /// <summary>
    /// Author Mansoor
    /// Date 17 Feb 2011
    /// </summary>
    public class VisitDAO : BaseDAO
    {
        /// <summary>
        /// Author  Mansoor
        /// Date 17 Feb 2011
        /// To Get List of all Encounter Types
        /// </summary>
        /// <returns></returns>
        public List<EncounterTypeDTO> GetAllEncounterTypes()
        {
            List<EncounterTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ENCOUNTER_TYPE_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<EncounterTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(EncounterTypeDTO));
                    }
                }
            }
            catch
            {
                //  throw;// Just Re-Throw Exception From DAO LEVEL
                throw new DTOConversionException(ProjectConstant.DTO_CONVERSION_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.ENCOUNTER_TYPES_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                   ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return list;
        }

        /// <summary>
        /// Author  Mansoor
        /// Date 17 Feb 2011
        ///  To Get List soap templates depends upon clinic
        /// </summary>
        /// <returns></returns>
        public List<SoapTemplateTypeDTO> LoadSoapTemplateByClinicBranchId(int clinicBranchId)
        {
            List<SoapTemplateTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SOAP_TEMPALTE_TYPE_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<SoapTemplateTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name                

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SoapTemplateTypeDTO));
                    }
                }
            }
            catch
            {
                //   throw;// Just Re-Throw Exception From DAO LEVEL
                throw new DTOConversionException(ProjectConstant.DTO_CONVERSION_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.SOAP_TEMPLATE_PREFIX, ErrorCodes.OPERATION_LOAD_PREFIX,
                                    ErrorCodes.DTO_CONVERSION_ERROR_PREFIX));
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit/ save        
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>       
        public int CreateVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitID = 0;
            string sProc = VisitConstant.VISIT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, visitDTO.PatientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                db.AddInParameter(command, "@VisitDate", DbType.DateTime, visitDTO.VisitDate);
                db.AddInParameter(command, "@RecordStatus", DbType.String, visitDTO.RecordStatus);
                db.AddInParameter(command, "@UserID", DbType.Int32, visitDTO.UserId);
                db.AddInParameter(command, "@UserTypeId", DbType.Int32, visitDTO.UserType.UserTypeId);
                db.AddInParameter(command, "@VisitStatus", DbType.String, visitDTO.VisitStatus);
                db.AddInParameter(command, "@GUID", DbType.Guid, visitDTO.RecGuid);
                db.AddInParameter(command, "@DictationID", DbType.Guid, visitDTO.DictationID);

                if (visitDTO.OwnerId > 0)
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, visitDTO.OwnerId);
                    if (visitDTO.OwnerUserType != null)
                        db.AddInParameter(command, "@OwnerUserTypeId", DbType.Int32, visitDTO.OwnerUserType.UserTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@OwnerUserTypeId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@IsVirtualVisit", DbType.Boolean, visitDTO.IsVirtualVisit);

                if (visitDTO.SignedByUserId > 0)
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, visitDTO.SignedByUserId);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, visitDTO.SignedByUserType.UserTypeId);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, visitDTO.SignedDate);
                }
                else
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, DBNull.Value);
                }

                if (visitDTO.AppointmentId != 0)
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, visitDTO.AppointmentId);
                }
                else
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.TemplateId != 0)
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, visitDTO.TemplateId);
                }
                else
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.EncounterTypeId != 0)
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, visitDTO.EncounterTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedByUserId);

                db.AddOutParameter(command, "@VisitIDOUT", DbType.Int32, 1);

                if (visitDTO.CreatedStationID > 0)
                {
                    db.AddInParameter(command, "@CreatedStationID", DbType.Int32, visitDTO.CreatedStationID);
                }

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SAVE_PREFIX,
                                   ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    visitID = int.Parse(db.GetParameterValue(command, "@VisitIDOUT").ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return visitID;
        }


        public int CheckZeroVisitExists(int patientId)
        {
            int visitID = 0;
            string sProc = VisitConstant.CHECK_ZERO_VISIT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                

                db.AddOutParameter(command, "@VisitIDOUT", DbType.Int32, 0);

               

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SAVE_PREFIX,
                                   ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    string id = db.GetParameterValue(command, "@VisitIDOUT").ToString();
                    if (id != null && id.Length > 0)
                    {
                        visitID = int.Parse(id);
                    }
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return visitID;
        }


        /// <summary>
        /// HAssan Raza
        /// Date 1 FEB 2012
        /// create zero visit
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>       
        public int CreateZeroVisit(VisitDTO visitDTO, int managedByUserId)
        {
            int visitID = 0;
            string sProc = VisitConstant.ZERO_VISIT_INSERT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, visitDTO.PatientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                db.AddInParameter(command, "@VisitDate", DbType.DateTime, visitDTO.VisitDate);
                db.AddInParameter(command, "@RecordStatus", DbType.String, visitDTO.RecordStatus);
                db.AddInParameter(command, "@UserID", DbType.Int32, visitDTO.UserId);
                db.AddInParameter(command, "@UserTypeId", DbType.Int32, visitDTO.UserType.UserTypeId);
                db.AddInParameter(command, "@VisitStatus", DbType.String, visitDTO.VisitStatus);
                db.AddInParameter(command, "@GUID", DbType.Guid, visitDTO.RecGuid);
                db.AddInParameter(command, "@DictationID", DbType.Guid, visitDTO.DictationID);
                db.AddInParameter(command, "@OwnerId", DbType.Int32, visitDTO.PatientId);
                db.AddInParameter(command, "@OwnerUserTypeId", DbType.Int32, ProjectConstant.USER_TYPE.Patient);
                db.AddInParameter(command, "@IsVirtualVisit", DbType.Boolean, 4);
                db.AddInParameter(command, "@SignedByUserID", DbType.Int32, DBNull.Value);
                db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, DBNull.Value);
                db.AddInParameter(command, "@SignedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(command, "@AppointmentID", DbType.Int32, DBNull.Value);


                db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, DBNull.Value);


                db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, DBNull.Value);


                db.AddInParameter(command, "@CreatedBy", DbType.Int32, managedByUserId);

                db.AddOutParameter(command, "@VisitIDOUT", DbType.Int32, 1);

                db.AddInParameter(command, "@CreatedStationID", DbType.Int32, DBNull.Value);
                

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SAVE_PREFIX,
                                   ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    visitID = int.Parse(db.GetParameterValue(command, "@VisitIDOUT").ToString());
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return visitID;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed       
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns> 
        public bool SignVisit(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_MARK_SIGNED_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@SignedByUserID", DbType.Int32, SignedByUserID);
                db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, SignedByUserTypeId);
                db.AddInParameter(command, "@SignedDate", DbType.DateTime, OperationDate);
                db.AddInParameter(command, "@UpdatedBy", DbType.Int32, SignedByUserID);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_SIGN_PREFIX,
                                  ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed       
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns> 
        public PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, int UserId, int UserTypeId)
        {
            PatientEncounterResult result = null;
            List<PatientEncounterDTO> encounterList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.VISIT_LOAD_BY_PATIENT_AND_CLINIC_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                //list = new List<EncounterTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@ShowAllRecords", DbType.Boolean, showAll);
                //db.AddInParameter(command, "@PatientID", DbType.Int32, patientId);

                db.AddInParameter(command, "@UserID", DbType.Int32, UserId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, UserTypeId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        if (objIReader.Read())
                        {
                            result = new PatientEncounterResult();

                            if (objIReader["TotalRecordsFound"] != DBNull.Value)
                            {
                                result.TotalRecords = int.Parse(objIReader["TotalRecordsFound"].ToString());
                            }

                            if (objIReader.NextResult())
                            {
                                encounterList = DataReaderToDTOConverter.PatientEncounterConverter(objIReader);

                                result.PatientEncounterList = encounterList;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }



        /// <summary>
        /// Author  Mansoor
        /// Date 18 Feb 2011
        /// To Get List of all immunization Types
        /// </summary>
        /// <returns></returns>
        public List<ImmunizationTypeDTO> GetAllImmunizationTypes()
        {
            List<ImmunizationTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.IMMUNIZATION_LOAD_ALL_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ImmunizationTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ImmunizationTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>   
        public List<string> LoadImmunizationLotNumberByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> lotNumberList = null;
            string lotNumber = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LOTNUMBER_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        lotNumberList = new List<string>();

                        while (objIReader.Read())
                        {
                            lotNumber = objIReader["LotNumber"].ToString();

                            lotNumberList.Add(lotNumber);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return lotNumberList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization lot number
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationDoseByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> doseList = null;
            string dose = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.DOSE_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        doseList = new List<string>();

                        while (objIReader.Read())
                        {
                            dose = objIReader["Dose"].ToString();

                            doseList.Add(dose);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return doseList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization Refusal Reason
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationRefusalReasonByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> refusalReasonList = null;
            string refusalReason = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.REFUSAL_REASON_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        refusalReasonList = new List<string>();

                        while (objIReader.Read())
                        {
                            refusalReason = objIReader["RefusalReason"].ToString();

                            refusalReasonList.Add(refusalReason);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return refusalReasonList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization site
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationSiteByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> refusalReasonList = null;
            string refusalReason = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SITE_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        refusalReasonList = new List<string>();

                        while (objIReader.Read())
                        {
                            refusalReason = objIReader["Site"].ToString();

                            refusalReasonList.Add(refusalReason);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return refusalReasonList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// get immunization series
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>  
        public List<string> LoadImmunizationSeriesByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> refusalReasonList = null;
            string refusalReason = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SERIES_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        refusalReasonList = new List<string>();

                        while (objIReader.Read())
                        {
                            refusalReason = objIReader["Series"].ToString();

                            refusalReasonList.Add(refusalReason);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return refusalReasonList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization manufacturer
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationManufacturerByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> ManufacturerList = null;
            string manufacturer = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MANUFACTURER_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        ManufacturerList = new List<string>();

                        while (objIReader.Read())
                        {
                            manufacturer = objIReader["Manufacturer"].ToString();

                            ManufacturerList.Add(manufacturer);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ManufacturerList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get immunization rout
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>        
        public List<string> LoadImmunizationRoutByClinicId(string searchCriteria, int clinicBranchId)
        {
            List<string> RoutList = null;
            string rout = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ROUT_LOAD_BY_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);
                db.AddInParameter(command, "@SearchString", DbType.String, searchCriteria);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        RoutList = new List<string>();

                        while (objIReader.Read())
                        {
                            rout = objIReader["Route"].ToString();

                            RoutList.Add(rout);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return RoutList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns> 
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.MODULE_NOTES_MANAGE_SP;
            DataTable dtModuleNote = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (moduleNotes != null && moduleNotes.Count > 0)
                    dtModuleNote = DTOToTableConverter.ModuleNoteDTOListConverter(moduleNotes);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpModuleNotes = sqlCommand.Parameters.AddWithValue("@ModuleNotesTVP", dtModuleNote);
                tvpModuleNotes.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpModuleNotes.TypeName = ProjectConstant.MODULE_NOTE_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.MODULE_NOTES_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns> 
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.MODULE_NOTES_MANAGE_SP;
            DataTable dtModuleNote = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (moduleNotes != null && moduleNotes.Count > 0)
                    dtModuleNote = DTOToTableConverter.ModuleNoteDTOListConverter(moduleNotes, managedByUserId, VisitId, ActionDate);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpModuleNotes = sqlCommand.Parameters.AddWithValue("@ModuleNotesTVP", dtModuleNote);
                tvpModuleNotes.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpModuleNotes.TypeName = ProjectConstant.MODULE_NOTE_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ImmunizationDTO> LoadPatientImmunizationsByPatientId(int patientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ImmunizationDTO> immunizationList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.IMMUNIZATION_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                immunizationList = new List<ImmunizationDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", patientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", clinicBranchID);
                clinicBranchIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter isAllSelectedPara = sqlCommand.Parameters.AddWithValue("@IsAllSelected", isAllSelected);
                isAllSelectedPara.SqlDbType = SqlDbType.Bit;
                if (stationIDs != null && stationIDs.Count > 0)
                {
                    dtStationIDs = DTOToTableConverter.StationIDsListConverter(stationIDs);
                }
                SqlParameter stationIdsTVPPara = sqlCommand.Parameters.AddWithValue("@StationIdsTVP", dtStationIDs);
                stationIdsTVPPara.SqlDbType = SqlDbType.Structured;
                stationIdsTVPPara.TypeName = ProjectConstant.STATION_IDS_TVP;
                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        immunizationList = DataReaderToDTOConverter.ImmunizationPatientIdConverter(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return immunizationList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// save visit
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <returns></returns>       
        public bool SaveVisit(VisitDTO visitDTO, int managedByUserId)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_UPDATE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                QuivusLogger.Info("Calling SP with command:: " + command);
                db.AddInParameter(command, "@VisitID", DbType.Int32, visitDTO.VisitId);
                db.AddInParameter(command, "@PatientID", DbType.Int32, visitDTO.PatientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                db.AddInParameter(command, "@VisitDate", DbType.DateTime, visitDTO.VisitDate);
                db.AddInParameter(command, "@RecordStatus", DbType.String, visitDTO.RecordStatus);
                db.AddInParameter(command, "@UserID", DbType.Int32, visitDTO.UserId);
                db.AddInParameter(command, "@UserTypeId", DbType.Int32, visitDTO.UserType.UserTypeId);
                db.AddInParameter(command, "@VisitStatus", DbType.String, visitDTO.VisitStatus);
                db.AddInParameter(command, "@GUID", DbType.Guid, visitDTO.RecGuid);
                db.AddInParameter(command, "@DictationID", DbType.Guid, visitDTO.DictationID);

                QuivusLogger.Info("Calling SP with dto:: " + visitDTO);

                if (visitDTO.OwnerId > 0)
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, visitDTO.OwnerId);
                    if (visitDTO.OwnerUserType != null)
                        db.AddInParameter(command, "@OwnerUserTypeId", DbType.Int32, visitDTO.OwnerUserType.UserTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@OwnerId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@OwnerUserTypeId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@IsVirtualVisit", DbType.Boolean, visitDTO.IsVirtualVisit);

                if (visitDTO.SignedByUserId > 0)
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, visitDTO.SignedByUserId);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, visitDTO.SignedByUserType.UserTypeId);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, visitDTO.SignedDate);
                }
                else
                {
                    db.AddInParameter(command, "@SignedByUserID", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedByUserTypeId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(command, "@SignedDate", DbType.DateTime, DBNull.Value);
                }

                if (visitDTO.AppointmentId != 0)
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, visitDTO.AppointmentId);
                }
                else
                {
                    db.AddInParameter(command, "@AppointmentID", DbType.Int32, DBNull.Value);
                }

                if (visitDTO.TemplateId != 0)
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, visitDTO.TemplateId);
                }
                else
                {
                    db.AddInParameter(command, "@SoapTemplateID", DbType.Int32, DBNull.Value);
                }

                QuivusLogger.Info("adding IsDressedForPhysical :: " + visitDTO.IsDressedForPhysical);
                if (string.IsNullOrEmpty(visitDTO.IsDressedForPhysical))
                {
                    db.AddInParameter(command, "@IsDressedForPhysical", DbType.Boolean, DBNull.Value);
                }
                else if (visitDTO.IsDressedForPhysical.Trim().Equals(ProjectConstant.SELECTION_TRUE))
                {
                    db.AddInParameter(command, "@IsDressedForPhysical", DbType.Boolean, true);
                }
                else if (visitDTO.IsDressedForPhysical.Trim().Equals(ProjectConstant.SELECTION_False))
                {
                    db.AddInParameter(command, "@IsDressedForPhysical", DbType.Boolean, false);
                }
                else if (visitDTO.IsDressedForPhysical.Trim().Equals(ProjectConstant.SELECTION_NULL))
                {
                    db.AddInParameter(command, "@IsDressedForPhysical", DbType.Boolean, DBNull.Value);
                }

                if (visitDTO.EncounterTypeId > 0)
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, visitDTO.EncounterTypeId);
                }
                else
                {
                    db.AddInParameter(command, "@EncounterTypeId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(command, "@ModifiedBy", DbType.Int32, managedByUserId);
                QuivusLogger.Info("Executing SP:: " + sProc);
                if (db.ExecuteNonQuery(command) == 0)
                {
                    //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                     ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_UPDATE_PREFIX,
                                ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Medical History Types Used 
        /// Under Comprehensive History Section
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryTypeDTO> LoadMedicalHistoryTypes()
        {
            List<MedicalHistoryTypeDTO> MedicalHistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MEDICAL_HISTORY_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                MedicalHistoryList = new List<MedicalHistoryTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        MedicalHistoryList = DataReaderToDTOConverter.MedicalHistoryConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return MedicalHistoryList;
        }




        public int ManageVisitWithData(VisitDTO visitDTO, List<ImmunizationDTO> immunizationList, List<MedicationPrescribedDTO> chronicMedicationList, int managedByUserId)
        {
            //M TODO look with transaction
            throw new NotImplementedException();
        }



        public List<FamilyHistoryTypeDTO> LoadFamilyHistoryTypes()
        {
            List<FamilyHistoryTypeDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FAMILY_HISTORY_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<FamilyHistoryTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.FamilyHistoryTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        public List<DiseaseTypeDTO> LoadDiseaseTypes(int ClinicBranchID, int FamilyHistoryTypeID)
        {
            List<DiseaseTypeDTO> DiseaseList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.DISEASE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiseaseList = new List<DiseaseTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchID);
                db.AddInParameter(command, "@FamilyHistoryTypeID", DbType.Int32, FamilyHistoryTypeID);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        DiseaseList = DataReaderToDTOConverter.DiseaseTypeConverter(objIReader);
                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(DiseaseList, objIReader, typeof(DiseaseTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiseaseList;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 14 March 2011
        /// This Method Will be used to load Vital Types 
        /// </summary>
        /// <returns></returns> 
        public List<VitalTypeDTO> LoadVitalTypes()
        {
            List<VitalTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.VITAL_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<VitalTypeDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(VitalTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabNatureTypeDTO> LoadLabNatureTypes()
        {
            List<LabNatureTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LAB_NATURE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LabNatureTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabNatureTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestPriorityTypeDTO> LoadLabTestPriorityTypes()
        {
            List<LabTestPriorityTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LAB_TEST_PRIORITY_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestPriorityTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestPriorityTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestSpecimenTypeDTO> LoadLabTestSpecimenTypes()
        {
            List<LabTestSpecimenTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LAB_TEST_SPECIMENT_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestSpecimenTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestSpecimenTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestTypeDTO> LoadLabTestTypes()
        {
            List<LabTestTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LAB_TEST_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTypeDTO> LoadLabTypes()
        {
            List<LabTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LAB_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LabTestOrderMethodTypeDTO> LoadLabTestOrderMethodTypes()
        {
            List<LabTestOrderMethodTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LAB_TEST_ORDER_METHOD_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LabTestOrderMethodTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LabTestOrderMethodTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<ICDCodeDTO> LoadICDCodeByName(string queryString)
        {
            List<ICDCodeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ICD_CODE_BY_NAME_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ICDCodeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ICDCodeName", DbType.String, queryString);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ICDCodeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Main Category
        /// </summary>
        /// <returns></returns>
        public List<ROSMainCategoryTypeDTO> LoadROSMainCategoryTypes()
        {
            List<ROSMainCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ROS_MAIN_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ROSMainCategoryTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.ROSMainTypeConverter(objIReader);

                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ROSMainCategoryTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load ROS Sub Category
        /// </summary>
        /// <returns></returns>
        public List<ROSSubCategoryTypeDTO> LoadROSSubCategoryTypes()
        {
            List<ROSSubCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ROS_SUB_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ROSSubCategoryTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.ROSSubTypeConverter(objIReader);
                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ROSSubCategoryTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        //Commented by Mansoor
        ///// <summary>
        ///// Author Muhammad Ismail 
        ///// Date 21 March 2011
        ///// This Method Will be used to load HPI Data Control Types
        ///// </summary>
        ///// <returns></returns>
        //public List<HPIDataControlTypeDTO> LoadHPIDataControlTypes()
        //{
        //    List<HPIDataControlTypeDTO> list = null;
        //    IDataReader objIReader = null;
        //    string sProc = VisitConstant.HPI_DATA_TYPE_LOAD_SP;

        //    try
        //    {
        //        QuivusLogger.Info("Calling SP:: " + sProc);

        //        list = new List<HPIDataControlTypeDTO>();
        //        Database db = DatabaseFactory.CreateDatabase(); // Load default DB
        //        DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

        //        using (objIReader = db.ExecuteReader(command))
        //        {
        //            if (objIReader != null)
        //            {
        //                GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(HPIDataControlTypeDTO));
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;// Just Re-Throw Exception From DAO LEVEL
        //    }
        //    return list;
        //}


        public List<HPITypeDTO> LoadHPIDetailTypes()
        {
            List<HPITypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.HPI_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<HPITypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(HPITypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 Mar 2011
        /// get hpi details for auto complete suggestion box, by type id and clinic branch
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns>  
        public List<string> LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId, int clinicBranchId, string searchCriteria)
        {
            List<string> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.HPI_DETAILS_LOAD_BY_TYPE_ID_AND_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<string>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                if (hpiTypeId != 0)
                {
                    db.AddInParameter(command, "@HPIDataControlTypeID", DbType.Int32, hpiTypeId);
                }

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        string value = "";

                        while (objIReader.Read())
                        {
                            value = "";

                            if (objIReader["Value"] != DBNull.Value)
                            {
                                value = objIReader["Value"].ToString();
                            }

                            list.Add(value);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social Addication types 
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryAddictionTypeDTO> SocialHistoryAddictionTypeDTO()
        {
            List<SocialHistoryAddictionTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SOCIAL_ADDICTION_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<SocialHistoryAddictionTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SocialHistoryAddictionTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<SocialMaritalStatusTypeDTO> LoadMaritalStatusTypes()
        {
            List<SocialMaritalStatusTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SOCIAL_MARITAL_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<SocialMaritalStatusTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SocialMaritalStatusTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        public List<SocialHistoryUsageAmountTypeDTO> LoadSocialHistoryUsageAmountTypes()
        {
            List<SocialHistoryUsageAmountTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SOCIAL_USAGE_AMOUNT_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<SocialHistoryUsageAmountTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SocialHistoryUsageAmountTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<SocialHistoryMediumTypeDTO> LoadSocialHistoryMediumTypes()
        {
            List<SocialHistoryMediumTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.SOCIAL_MEDIUM_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<SocialHistoryMediumTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(SocialHistoryMediumTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// This Method Will be used to load Plan types 
        /// </summary>
        /// <returns></returns>
        public List<PlanTypeDTO> LoadAllRecommendationPlanTypes()
        {
            List<PlanTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PLAN_TYPES_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PlanTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        //M TODO 
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(PlanTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Duration types 
        /// </summary>
        /// <returns></returns>
        public List<DurationTypeDTO> LoadAllRecommendationDurationTypes()
        {
            List<DurationTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.DURATION_TYPES_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<DurationTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        //M TODO 
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(DurationTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Method types 
        /// </summary>
        /// <returns></returns>       
        public List<MethodTypeDTO> LoadAllRecommendationMethodTypes()
        {
            List<MethodTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.METHOD_TYPES_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<MethodTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        //M TODO 
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(MethodTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// get Recommendation auto complete suggestion box, by type id and clinic branch
        /// </summary>        
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public List<string> LoadRecommendationLinksAutoCompleteByClinicId(int clinicBranchId, string searchCriteria)
        {
            List<string> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LINKS_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<string>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = new List<string>();
                        // GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(RecommendationLinksTypeDTO));
                        while (objIReader.Read())
                        {
                            list.Add(objIReader["Link"].ToString());
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        public List<PhysicalSubCategoryTypeDTO> LoadPhysicalSubCategoryTypes()
        {
            List<PhysicalSubCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PHYSICAL_SUB_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PhysicalSubCategoryTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.PhysicalSubTypeConverter(objIReader);
                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(PhysicalSubCategoryTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<PhysicalMainCategoryTypeDTO> LoadPhysicalMainCategoryTypes()
        {
            List<PhysicalMainCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PHYSICAL_MAIN_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PhysicalMainCategoryTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.PhysicalMainTypeConverter(objIReader);
                        //label
                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(PhysicalMainCategoryTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType(int LabtestTypeID, string LabTestCodeOrName)
        //    public List<LIONICLabTestTypeDTO> LoadLIONICLabTestType()
        {
            List<LIONICLabTestTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LIONICLAB_TEST_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<LIONICLabTestTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@LabtestTypeID", DbType.Int32, LabtestTypeID);
                db.AddInParameter(command, "@LabTestCodeOrName", DbType.String, LabTestCodeOrName);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(LIONICLabTestTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load FollowUp Method Type
        /// /// </summary>
        /// <returns></returns> 
        public List<FollowUpMethodTypeDTO> LoadFollowUpMethodTypes()
        {
            List<FollowUpMethodTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_METHOD_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<FollowUpMethodTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(FollowUpMethodTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public bool CoSignVisit(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_MARK_COSIGNED_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@ProviderId", DbType.Int32, ProviderId);
                db.AddInParameter(command, "@ProviderUserId", DbType.Int32, ProviderUserId);
                db.AddInParameter(command, "@ProviderTypeId", DbType.Int32, ProviderTypeId);

                db.AddInParameter(command, "@CoSignedOn", DbType.DateTime, OperationDate);
                db.AddInParameter(command, "@CreatedAt", DbType.DateTime, OperationDate);
                db.AddInParameter(command, "@CreatedBy", DbType.Int32, ProviderUserId);
                db.AddOutParameter(command, "@VisitCoSignIDOUT", DbType.Int32, 1);
                //@VisitCoSignIDOUT

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool MarkValidDataExists(int VisitId)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_MARK_DATA_EXIST_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //  throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.QUICK_INPUT_PREFIX, ErrorCodes.OPERATION_MARK_VALID_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }

        public bool CalculateVisitScore(int VisitId)
        {
            bool result = false;
            string sProc = VisitConstant.VISIT_CALCULATE_SCORE_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    //   throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG,
                         ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                    ErrorCodes.VISIT_PREFIX, ErrorCodes.OPERATION_CALCULATE_SCORE_PREFIX,
                                    ErrorCodes.RECORD_NOT_INTRESTED_PREFIX));
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            return result;
        }
        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// Method Load In House Lab Types By Clinic branch Id
        /// </summary>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public List<InHouseLabTestTypeDTO> LoadInHouseLabTestTypeByClinicBranchId(int ClinicBranchId)
        {
            List<InHouseLabTestTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.IN_HOUSE_LAB_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<InHouseLabTestTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InHouseLabTestTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 06 april 2011
        /// Method Load CPT Medical Types
        /// </summary>
        /// <returns></returns>
        public List<CPTMedicalTypeDTO> LoadCPTMedicalTypes(String CPTName)
        {
            List<CPTMedicalTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.CPT_MEDICAL_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<CPTMedicalTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name


                db.AddInParameter(command, "@CPTName", DbType.String, CPTName);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(CPTMedicalTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<InstructionAgeTypeDTO> LoadFollowUpInstructionAgeTypes()
        {
            List<InstructionAgeTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_INSTRUCTION_AGE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<InstructionAgeTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InstructionAgeTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<InstructionDurationTypeDTO> LoadFollowUpInstructionDurationTypes()
        {
            List<InstructionDurationTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_INSTRUCTION_DURATION_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<InstructionDurationTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InstructionDurationTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<ServiceTypeDTO> LoadFollowUpServiceTypes(int ClinicBranchID)
        {
            List<ServiceTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_SERVICE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ServiceTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchID);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ServiceTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<InstructionAgeValueTypeDTO> LoadFollowUpInstructionAgeValueTypes()
        {
            List<InstructionAgeValueTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_INSTRUCTION_AGE_VALUE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<InstructionAgeValueTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(InstructionAgeValueTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public bool ManageModuleDictation(List<ModuleDictationDTO> ModuleDictationList, int ManagedByUserID, int ClinicBranchID, int VisitId)
        {
            bool UpdateResult = false;
            string sProc = VisitConstant.MODULE_DICTATION_MANAGE_SP;
            DataTable dtModuleDictation = null;
            SqlConnection connection = null;
            bool isConnectionOpen = false;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                if (ModuleDictationList != null && ModuleDictationList.Count > 0)
                    dtModuleDictation = DTOToTableConverter.ModuleDictationDTOListConverter(ModuleDictationList, ManagedByUserID, ClinicBranchID, VisitId);

                Database DB = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(DB.ConnectionString);

                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //use SqlParameter to be able to define the TypeName of the parameter
                SqlParameter tvpModuleNotes = sqlCommand.Parameters.AddWithValue("@ModuleDictationTVP", dtModuleDictation);
                tvpModuleNotes.SqlDbType = SqlDbType.Structured;
                //should be the name of your TVP
                tvpModuleNotes.TypeName = ProjectConstant.MODULE_DICTATION_TVP;

                connection.Open();
                isConnectionOpen = true;

                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    UpdateResult = true;
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                throw; // Just Re-Throw Exception From DAO LEVEL
            }
            finally
            {
                if (isConnectionOpen)
                    connection.Close();
            }
            return UpdateResult;
        }


        public List<ModuleTypeDTO> LoadModuleTypes()
        {
            List<ModuleTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MODULE_TYPE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ModuleTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.ModuleTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypes(int ROSSubCategoryTypeID)
        {
            List<ROSSubLevelCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ROS_SUB_LEVEL_CATEGORY_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ROSSubLevelCategoryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                db.AddInParameter(command, "@ROSSubCategoryTypeID", DbType.Int32, ROSSubCategoryTypeID);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.ROSSubLevelCategoryTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<PhysicalCategoryTypeDTO> LoadPhysicalCategoryTypes()
        {
            List<PhysicalCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PHYSICAL_CATEGORY_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PhysicalCategoryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.PhysicalCategoryTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<PhysicalCategoryTypeDTO> LoadPhysicalCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly)
        {
            List<PhysicalCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PHYSICAL_CATEGORY_LOAD_BY_GENDER_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PhysicalCategoryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                db.AddInParameter(command, "@LoadUniversal", DbType.Boolean, LoadUniversal);
                db.AddInParameter(command, "@LoadMaleOnly", DbType.Boolean, LoadMaleOnly);
                db.AddInParameter(command, "@LoadFemaleOnly", DbType.Boolean, LoadFemaleOnly);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.PhysicalCategoryTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<ROSSubLevelCategoryTypeDTO> LoadROSSubLevelCategoryTypesByGender(bool LoadUniversal, bool LoadMaleOnly, bool LoadFemaleOnly, int ROSSubCategoryTypeID)
        {
            List<ROSSubLevelCategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ROS_SUB_LEVEL_CATEGORY_LOAD_BY_GENDER_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ROSSubLevelCategoryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                db.AddInParameter(command, "@LoadUniversal", DbType.Boolean, LoadUniversal);
                db.AddInParameter(command, "@LoadMaleOnly", DbType.Boolean, LoadMaleOnly);
                db.AddInParameter(command, "@LoadFemaleOnly", DbType.Boolean, LoadFemaleOnly);
                db.AddInParameter(command, "@ROSSubCategoryTypeID", DbType.Int32, ROSSubCategoryTypeID);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.ROSSubLevelCategoryTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        public List<PhysicalLevel3CategoryTypeDTO> LoadPhysical3LevelCategoryTypes(int PhysicalSubCategoryTypeID)
        {
            List<PhysicalLevel3CategoryTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PHYSICAL_LEVEL_THREE_CATEGORY_LOAD_BY_GENDER_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PhysicalLevel3CategoryTypeDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name               

                db.AddInParameter(command, "@PhysicalSubCategoryTypeID", DbType.Int32, PhysicalSubCategoryTypeID);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        list = DataReaderToDTOConverter.PhysicalLevel3CategoryTypeConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 8/4/2011
        /// </summary>
        /// <param name="hpiTypeId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>

        public List<FollowupDataControlTypeDTO> LoadFollowupDataControlType()
        {
            List<FollowupDataControlTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_DATACONTROL_TYPE;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<FollowupDataControlTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(FollowupDataControlTypeDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 8/12/2011
        /// This Method will load Patient Diagnosis By PatientId and clinic branch id for recommendation
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="ClinicBranchId"></param>
        /// <returns></returns>
        public List<ServiceTypeDTO> LoadFollowUpAutoCompleteByTypeIdAndClinicId(int followupDataControlTypeID, int clinicBranchId)
        {
            List<ServiceTypeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOW_UP_AUTO_COMPLETE_DATA_LOAD_CLINIC_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ServiceTypeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, clinicBranchId);

                if (followupDataControlTypeID != 0)
                {
                    db.AddInParameter(command, "@FollowupDataControlTypeID", DbType.Int32, followupDataControlTypeID);
                }

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        ServiceTypeDTO objServiceTypeDTO = new ServiceTypeDTO();

                        while (objIReader.Read())
                        {
                            if (objIReader["Value"] != DBNull.Value)
                            {
                                objServiceTypeDTO.FollowupService = objIReader["Value"].ToString();
                            }
                            if (objIReader["ClinicBranchID"] != DBNull.Value)
                            {
                                objServiceTypeDTO.ClinicBranchId = int.Parse(objIReader["ClinicBranchID"].ToString());
                            }
                            list.Add(objServiceTypeDTO);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }


        public List<QuivusModel.User.Patient.PatientLastSeenDTO> LoadPatientLastSeenByPatientId(int PatientId)
        {
            List<PatientLastSeenDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_LAST_SEEN_BY_PATIENT_ID;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<PatientLastSeenDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        PatientLastSeenDTO objServiceTypeDTO = new PatientLastSeenDTO();

                        while (objIReader.Read())
                        {

                            if (objIReader["LastSeen"] != DBNull.Value)
                            {
                                try
                                {
                                    objServiceTypeDTO.LastSeen = DateTime.Parse(objIReader["LastSeen"].ToString());
                                }
                                catch
                                {
                                    // By-Pass EXp :)
                                }

                            }
                            if (objIReader["LastSeenByUserID"] != DBNull.Value)
                            {
                                objServiceTypeDTO.LastSeenByUserID = int.Parse(objIReader["LastSeenByUserID"].ToString());
                            }
                            if (objIReader["LastSeenByUserTypeID"] != DBNull.Value)
                            {
                                objServiceTypeDTO.LastSeenByUserTypeID = int.Parse(objIReader["LastSeenByUserTypeID"].ToString());
                            }
                            list.Add(objServiceTypeDTO);
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list;
        }
     
        /// <summary>
        /// Baber waqas at 1/24/2012
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        public List<ICDCodeDTO> LoadICDCodeByName(string queryString, int clinicBranchID)
        {
            List<ICDCodeDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ICDCodeCustomSpLoadByName; //VisitConstant.ICD_CODE_BY_NAME_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ICDCodeDTO>();
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ICDCodeName", DbType.String, queryString);
                db.AddInParameter(command,"@ClinicBranchID",DbType.Int32,clinicBranchID);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                       // GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ICDCodeDTO));
                     list =    DataReaderToDTOConverter.ConvertToICDCodeDTO( objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return list; 
        }
    }
}
