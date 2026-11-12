using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.Logger;

using System.Data;
using Quivus.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Visit.DataAccess.DB_Util;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.ROS;

using Quivus.Model.QuickInput.ComprehensiveHistory.Social;

using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.Visit;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Dictation;
using Quivus.Utility.CustomException;
using QuivusModel.QuickInput.ComprehensiveHistory.Social;
using System.Data.SqlClient;


namespace Visit.DataAccess.DAO
{
    public class ViewBackgroundDAO : BaseDAO
    {
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Patient Medical History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryDTO> LoadPatientMedicalHistoryByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<MedicalHistoryDTO> MedicalHistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_MEDICAL_HISTORY_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                MedicalHistoryList = new List<MedicalHistoryDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                //  @
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        MedicalHistoryList = DataReaderToDTOConverter.PatientMedicalHistoryConverter(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return MedicalHistoryList;
        }




        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId, bool IsTemplateVisit)
        {
            List<ImmunizationDTO> immunizationList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.IMMUNIZATION_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                immunizationList = new List<ImmunizationDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        immunizationList = DataReaderToDTOConverter.ImmunizationConverter(objIReader, IsTemplateVisit);
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
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId, bool IsTemplateVisit)
        {
            List<ModuleNoteDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MODULE_NOTES_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ModuleNoteDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ModuleNoteDTO));
                    }
                }
                if (list != null)
                {
                    foreach (ModuleNoteDTO obj in list)
                    {
                        if (IsTemplateVisit)
                        {
                            obj.VisitID = 0;
                            obj.ModuleNoteId = 0;
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
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Family History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<FamilyHistoryDTO> LoadPatientFamilyHistoryByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<FamilyHistoryDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_FAMILY_HISTORY_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<FamilyHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientFamilyHistoryConverter(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Vital History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitalByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<VitalDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_VITAL_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<VitalDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(HistoryList, objIReader, typeof(VitalDTO));
                    }
                }
                if (HistoryList != null)
                {
                    foreach (VitalDTO obj in HistoryList)
                    {
                        if (IsTemplateVisit)
                        {
                            obj.PatientVitalID = 0;
                            obj.VisitID = 0;
                            obj.DictationID = Guid.Empty;
                            obj.CreatedAt = DateTime.MinValue;
                            obj.CreatedBy = 0;
                            obj.ModifiedAt = DateTime.MinValue;
                            obj.CreatedBy = 0;
                            obj.VisitDate = DateTime.MinValue;
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<LabDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_LABS_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<LabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientLabsConverter(objIReader, IsTemplateVisit);
                        //  GenericDataReaderToDTOConverter.ConvertToDTOList(HistoryList, objIReader, typeof(LabDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load Patient Previous 10 Vitals History 
        /// </summary>
        /// <returns></returns>
        public List<RecentVitalDTO> LoadPatientPreviousVitals(int PatientId)
        {
            List<RecentVitalDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_VITAL_PREVIOUS_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<RecentVitalDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(HistoryList, objIReader, typeof(RecentVitalDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        public List<ROSDTO> LoadPatientROSByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<ROSDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_ROS_PREVIOUS_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<ROSDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientROSConverter(objIReader, IsTemplateVisit);
                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(HistoryList, objIReader, typeof(ROSDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social history Addiction By social History Id
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryAddictionDTO> LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId, bool IsTemplateVisit)
        {
            List<SocialHistoryAddictionDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_SOCIAL_HISTORY_ADDICTION_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<SocialHistoryAddictionDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@PatientSocialHistoryID", DbType.Int32, PatientSocialHistoryId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientSocialHistoryAddictionConverter(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by visit Id
        /// </summary>
        /// <returns></returns>
        public List<HPI_CCDTO> LoadPatientHPIByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<HPI_CCDTO> hpiList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_HPI_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                hpiList = new List<HPI_CCDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        hpiList = DataReaderToDTOConverter.PatientHPIConverter(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return hpiList;
        }



        public List<SocialHistoryDTO> LoadSocialHistoryByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<SocialHistoryDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_SOCIAL_HISTORY_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<SocialHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientSocialHistoryConverter(objIReader, IsTemplateVisit);

                        if (HistoryList != null)
                        {
                            foreach (SocialHistoryDTO obj in HistoryList)
                            {
                                if (obj != null && obj.PatientSocialHistoryID > 0)
                                {
                                    List<SocialHistoryAddictionDTO> HistoryAddictionList = LoadSocialHistoryAddictionBySocialHistoryID(obj.PatientSocialHistoryID, IsTemplateVisit);
                                    if (HistoryAddictionList != null)
                                    {
                                        obj.SocialHistoryAddictionList = HistoryAddictionList;
                                    }

                                    if (IsTemplateVisit)
                                        obj.PatientSocialHistoryID = 0; // IF Template Visit Make it Zero As We have to load addiction that's why it is not set in PatientSocialHistoryConverter Method :)
                                }
                            }
                        }
                        //
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Recommendation by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns>  
        public List<PatientRecommendationDTO> LoadPatientRecommendationByVisitId(int visitId)
        {
            List<PatientRecommendationDTO> RecommendationList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_RECOMMENDATION_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                RecommendationList = new List<PatientRecommendationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        //M TODO      
                        RecommendationList = DataReaderToDTOConverter.PatientRecommendationConverter(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return RecommendationList;
        }


        public List<PhysicalsDTO> LoadPatientPhysicalsByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<PhysicalsDTO> HistoryList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_PHYSICAL_PREVIOUS_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<PhysicalsDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name
                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        HistoryList = DataReaderToDTOConverter.PatientPhysicalConverter(objIReader, IsTemplateVisit);
                        //   GenericDataReaderToDTOConverter.ConvertToDTOList(HistoryList, objIReader, typeof(ROSDTO));
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 March 2011
        /// This Method Will be used to load Patient Diagnosis in Assessment Tab by visit Id
        /// </summary>
        /// <returns></returns>
        /// <param name="VisitId"></param>
        /// <returns></returns> 
        public List<PatientDiagnosisDTO> LoadPatientDiagnosisByVisitId(int visitId, bool IsTemplateVisit)
        {
            List<PatientDiagnosisDTO> diagnosisList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                diagnosisList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        diagnosisList = DataReaderToDTOConverter.PatientDiagnosisConverter(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return diagnosisList;
        }

        public List<PatientRecommendationDTO> LoadPatientRecommendationByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<PatientRecommendationDTO> RecommendationList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_RECOMMENDATION_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                RecommendationList = new List<PatientRecommendationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        RecommendationList = DataReaderToDTOConverter.ConvertPatientRecommendationByVisit(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return RecommendationList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow up by visit Id
        /// </summary>
        /// <returns></returns> 
        public PatientFollowUpDTO LoadFollowUpByVisitId(int visitId, bool IsTemplateVisit)
        {
            PatientFollowUpDTO followUp = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_FOLLOWUP_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                followUp = new PatientFollowUpDTO();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        followUp = DataReaderToDTOConverter.ConvertToPatientFollowUp(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return followUp;
        }

        public List<VisitDTO> LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId)
        {
            List<VisitDTO> VisitList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_VISIT_LOAD_BY_CLINICBRANCH_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                VisitList = new List<VisitDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        VisitList = DataReaderToDTOConverter.ConvertToPatientVisitByClinicBranch(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitList;
        }

        public List<VisistStakeHolderDTO> LoadVisitStakeHolderByVisitId(int VisitId, int UserTypeId)
        {
            List<VisistStakeHolderDTO> VisitList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.VISIT_STAKE_HOLDER_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                VisitList = new List<VisistStakeHolderDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);
                db.AddInParameter(command, "@UserTypeID", DbType.Int32, UserTypeId);
                
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        VisitList = DataReaderToDTOConverter.ConvertVisitStakeHolderByVisit(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitList;
        }

        public List<MedicationPrescribedDTO> LoadPatientMedicationPrescribedByPatient(int PatientId)
        {
            List<MedicationPrescribedDTO> List = new List<MedicationPrescribedDTO>();

            IDataReader objIReader = null;
            string sProc = VisitConstant.MEDICATION_PRESCRIBED_LOAD_BY_PATIENT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<MedicationPrescribedDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                //TOD: remove this parameter and replace it with appropriate parameter 
                // changes by baber 
                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.ConvertPatientMedictionPrescribed(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId)
        {
            List<AllergyDTO> List = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.ALLERGY_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<AllergyDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.ConvertPatientAllergyDTO(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<VitalDTO> LoadPatientVitals(int PatientId, int ClinicBranchId)
        {
            List<VitalDTO> List = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LOAD_PATIENT_VITALS_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<VitalDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.ConvertPatientVitals(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public VisitDTO LoadZeroVisitDataByPatientId(int patientId, bool IsTemplateVisit)
        {
            VisitDTO VisitObj = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LOAD_ZEROVISIT_BY_PATIENT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientId", DbType.Int32, patientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    VisitObj = ConvertVisitData(VisitObj, objIReader, IsTemplateVisit);
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitObj;
        }


        public VisitDTO LoadVisitDataByVisitId(int VisitId, bool IsTemplateVisit)
        {
            VisitDTO VisitObj = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.VISIT_DATA_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    VisitObj = ConvertVisitData(VisitObj, objIReader, IsTemplateVisit);
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitObj;
        }



        private static VisitDTO ConvertVisitData(VisitDTO VisitObj, IDataReader objIReader, bool IsTemplateVisit)
        {
            if (objIReader != null)
            {
                if (objIReader.Read())
                {
                    VisitObj = new VisitDTO();

                    VisitObj.VisitId = GetKeyAsNullIfTemplate(objIReader, "VisitID", VisitObj.VisitId, IsTemplateVisit);

                    if (objIReader["VisitID"] != DBNull.Value)
                    {
                        VisitObj.VisitId = int.Parse(objIReader["VisitID"].ToString());
                    }
                    if (objIReader["PatientID"] != DBNull.Value)
                    {
                        VisitObj.PatientId = int.Parse(objIReader["PatientID"].ToString());
                    }
                    if (objIReader["ClinicBranchID"] != DBNull.Value)
                    {
                        if (VisitObj.ClinicBranch == null)
                            VisitObj.ClinicBranch = new Quivus.Model.Clinic.ClinicBranchDTO();

                        VisitObj.ClinicBranch.ClinicBranchIdSeq = int.Parse(objIReader["ClinicBranchID"].ToString());
                    }
                    if (objIReader["VisitDate"] != DBNull.Value)
                    {
                        VisitObj.VisitDate = DateTime.Parse(objIReader["VisitDate"].ToString());
                    }

                    if (objIReader["RecordStatus"] != DBNull.Value)
                    {
                        VisitObj.RecordStatus = objIReader["RecordStatus"].ToString();
                    }

                    if (objIReader["UserID"] != DBNull.Value)
                    {
                        VisitObj.UserId = int.Parse(objIReader["UserID"].ToString());
                    }

                    if (objIReader["UserTypeId"] != DBNull.Value)
                    {
                        if (VisitObj.UserType == null)
                            VisitObj.UserType = new Quivus.Model.TypeDTOs.UserTypeDTO();

                        VisitObj.UserType.UserTypeId = int.Parse(objIReader["UserTypeId"].ToString());
                    }
                    if (objIReader["VisitStatus"] != DBNull.Value)
                    {
                        VisitObj.VisitStatus = objIReader["VisitStatus"].ToString();
                    }

                    VisitObj.RecGuid = GetDictationGUIDAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_GUID, VisitObj.RecGuid, IsTemplateVisit);
                    //if (objIReader["GUID"] != DBNull.Value)
                    //{
                    //    VisitObj.RecGuid = Guid.Parse(objIReader["GUID"].ToString());
                    //}

                    VisitObj.DictationID = GetDictationGUIDAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_DICTATION_ID, VisitObj.DictationID, IsTemplateVisit);
                    //if (objIReader["DictationID"] != DBNull.Value)
                    //{
                    //    VisitObj.DictationID = Guid.Parse(objIReader["DictationID"].ToString());
                    //}
                    if (objIReader["OwnerUserID"] != DBNull.Value)
                    {
                        VisitObj.OwnerId = int.Parse(objIReader["OwnerUserID"].ToString());
                    }
                    if (objIReader["OwnerUserTypeID"] != DBNull.Value)
                    {
                        if (VisitObj.OwnerUserType == null)
                            VisitObj.OwnerUserType = new Quivus.Model.TypeDTOs.UserTypeDTO();

                        VisitObj.OwnerUserType.UserTypeId = int.Parse(objIReader["OwnerUserTypeID"].ToString());
                    }
                    if (objIReader["IsVirtualVisit"] != DBNull.Value)
                    {
                        VisitObj.IsVirtualVisit = bool.Parse(objIReader["IsVirtualVisit"].ToString());
                    }


                    VisitObj.SignedByUserId = GetKeyAsNullIfTemplate(objIReader, "SignedByUserID", VisitObj.SignedByUserId, IsTemplateVisit);

                    //if (objIReader["SignedByUserID"] != DBNull.Value)
                    //{
                    //    VisitObj.SignedByUserId = int.Parse(objIReader["SignedByUserID"].ToString());
                    //}
                    if (objIReader["SignedByUserTypeId"] != DBNull.Value)
                    {
                        if (VisitObj.SignedByUserType == null)
                            VisitObj.SignedByUserType = new Quivus.Model.TypeDTOs.UserTypeDTO();

                        VisitObj.SignedByUserType.UserTypeId = GetKeyAsNullIfTemplate(objIReader, "SignedByUserTypeId", VisitObj.SignedByUserType.UserTypeId, IsTemplateVisit);
                        //VisitObj.SignedByUserType.UserTypeId = int.Parse(objIReader["SignedByUserTypeId"].ToString());
                    }
                    if (objIReader["SignedDate"] != DBNull.Value)
                    {
                        VisitObj.SignedDate = DateTime.Parse(objIReader["SignedDate"].ToString());
                    }
                    if (objIReader["AppointmentID"] != DBNull.Value)
                    {
                        VisitObj.AppointmentId = int.Parse(objIReader["AppointmentID"].ToString());
                    }
                    if (objIReader["SoapTemplateID"] != DBNull.Value)
                    {
                        VisitObj.TemplateId = int.Parse(objIReader["SoapTemplateID"].ToString());
                    }
                    if (objIReader["EncounterTypeId"] != DBNull.Value)
                    {
                        VisitObj.EncounterTypeId = int.Parse(objIReader["EncounterTypeId"].ToString());
                    }

                    VisitObj.CreatedBy = GetKeyAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_CREATED_BY, VisitObj.CreatedBy, IsTemplateVisit);
                    //if (objIReader["CreatedBy"] != DBNull.Value)
                    //{
                    //    VisitObj.CreatedBy = int.Parse(objIReader["CreatedBy"].ToString());
                    //}

                    VisitObj.CreatedAt = GetDateAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_CREATED_AT, VisitObj.CreatedAt, IsTemplateVisit);
                    //if (objIReader["CreatedAt"] != DBNull.Value)
                    //{
                    //    VisitObj.CreatedAt = DateTime.Parse(objIReader["CreatedAt"].ToString());
                    //}

                    VisitObj.ModifiedBy = GetKeyAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_MODIFIED_BY, VisitObj.ModifiedBy, IsTemplateVisit);
                    //if (objIReader["ModifiedBy"] != DBNull.Value)
                    //{
                    //    VisitObj.ModifiedBy = int.Parse(objIReader["ModifiedBy"].ToString());
                    //}

                    VisitObj.ModifiedAt = GetDateAsNullIfTemplate(objIReader, ProjectConstant.PROPERTY_MODIFIED_AT, VisitObj.ModifiedAt, IsTemplateVisit);
                    //if (objIReader["ModifiedAt"] != DBNull.Value)
                    //{
                    //    VisitObj.ModifiedAt = DateTime.Parse(objIReader["ModifiedAt"].ToString());
                    //}
                    if (objIReader["IsValidDataExists"] != DBNull.Value)
                    {
                        VisitObj.IsValidDataExists = bool.Parse(objIReader["IsValidDataExists"].ToString());
                    }

                    if (objIReader["StationID"] != DBNull.Value)
                    {
                        VisitObj.CreatedStationID = int.Parse(objIReader["StationID"].ToString());
                    }
                }
            }
            return VisitObj;
        }



        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient presription by visit by Id
        /// </summary>
        /// <returns></returns>
        public List<PrescriptionDTO> LoadPateintPrescriptionByVisitId(int VisitId)
        {
            List<PrescriptionDTO> PrescriptionList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_PRESCRIPTION_LOAD_BY_VISIT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                PrescriptionList = new List<PrescriptionDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        PrescriptionList = DataReaderToDTOConverter.ConvertPateintprescriptionByVisit(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return PrescriptionList;
        }

        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId)
        {
            List<PrescriptionMedicationDTO> PrescriptionMedicationList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PRESCRIPTION_MEDICATION_LOADBY_PATIENTPRESCRIPTIONID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                PrescriptionMedicationList = new List<PrescriptionMedicationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientPrescriptionID", DbType.Int32, PatientPrescriptionId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        PrescriptionMedicationList = DataReaderToDTOConverter.ConvertPrescriptionMedication(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return PrescriptionMedicationList;
        }

        public List<MedicationDTO> LoadPatientMedicationByPatientId(int PatientId)
        {
            List<MedicationDTO> PrescriptionList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_MEDICATION_LOAD_BY_PRESCRIPTION_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                PrescriptionList = new List<MedicationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        PrescriptionList = DataReaderToDTOConverter.ConvertPateintMedicationByprescriptionId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return PrescriptionList;
        }

        public List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientDiagnosisDTO> DiagnosisList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiagnosisList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        DiagnosisList = DataReaderToDTOConverter.ConvertPateintDiagnosisByprescriptionId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiagnosisList;
        }

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionDTOByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<PatientFollowupInstructionDTO> DiagnosisList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_FOLLOWUP_INSTRUCTION_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiagnosisList = new List<PatientFollowupInstructionDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        DiagnosisList = DataReaderToDTOConverter.ConvertFollowupInstruction(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiagnosisList;
        }

        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PrescriptionMedicationDTO> PrescriptionMedicationList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PRESCRIPTION_MEDICATION_LOADBY_PATIENTID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                PrescriptionMedicationList = new List<PrescriptionMedicationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        PrescriptionMedicationList = DataReaderToDTOConverter.ConvertPrescriptionMedication(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return PrescriptionMedicationList;
        }

        public List<ProcedureDTO> LoadProcedureDTOListByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<ProcedureDTO> ProcedureDTOList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_PROCEDURE_LOAD_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ProcedureDTOList = new List<ProcedureDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        ProcedureDTOList = DataReaderToDTOConverter.ConvertPRocedureDtoByVisitId(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ProcedureDTOList;
        }

        public List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            List<FollowupLabsDTO> List = new List<FollowupLabsDTO>();

            IDataReader objIReader = null;
            string sProc = VisitConstant.FOLLOWUP_LABS_LOAD_BY_PATIENT_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<FollowupLabsDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name


                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                
                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.ConvertPatientFollowupLabs(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }


        public List<InHouseLabDTO> LoadPateintInHouseLabsByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<InHouseLabDTO> InHouseList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.LOAD_INHOUSE_LABS_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                InHouseList = new List<InHouseLabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, VisitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        InHouseList = DataReaderToDTOConverter.ConvertInHouseLabs(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return InHouseList;
        }

        public List<InHouseLabDTO> LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<InHouseLabDTO> InHouseList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_INHOUSE_LABS_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                InHouseList = new List<InHouseLabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        InHouseList = DataReaderToDTOConverter.ConvertInHouseLabsByPatientId(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return InHouseList;
        }

        public List<MedicalHistoryDTO> LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId,List<int> stationIDs, bool isAllSelected)
        {
            List<MedicalHistoryDTO> MedicalHistoryList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_MEDICAL_HISTORY_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                MedicalHistoryList = new List<MedicalHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        MedicalHistoryList = DataReaderToDTOConverter.ConvertPatientMedicalHistory(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return MedicalHistoryList;
        }

        public List<FamilyHistoryDTO> LoadFamilyHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<FamilyHistoryDTO> FamilyHistoryList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_FAMILY_HISTORY_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                FamilyHistoryList = new List<FamilyHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        FamilyHistoryList = DataReaderToDTOConverter.ConvertPatientFamilyHistory(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return FamilyHistoryList;
        }


        public List<VitalDTO> LoadVitalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<VitalDTO> VitalList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_VITAL_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                VitalList = new List<VitalDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        VitalList = DataReaderToDTOConverter.ConvertPatientViewBackgroundVitals(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VitalList;
        }

        public List<HPI_CCDTO> LoadHPIByPatientId(int PatientId, int ClinicBranchId,List<int> stationIDs, bool isAllSelected)
        {
            List<HPI_CCDTO> HPIList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_HPI_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HPIList = new List<HPI_CCDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        HPIList = DataReaderToDTOConverter.ConvertPatientHPI(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HPIList;
        }

        public List<ROSDTO> LoadROSByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<ROSDTO> ROSList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_ROS_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ROSList = new List<ROSDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        ROSList = DataReaderToDTOConverter.ConvertPatientROS(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ROSList;
        }


        public List<PhysicalsDTO> LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PhysicalsDTO> PhysicalsList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.LOAD_PHYSICAL_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                PhysicalsList = new List<PhysicalsDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        PhysicalsList = DataReaderToDTOConverter.ConvertPatientPhysicals(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return PhysicalsList;
        }

        public List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisByPatientId(int PatientId)
        {
            List<PatientDiagnosisDTO> DiagnosisList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_RESOLVED_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiagnosisList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        DiagnosisList = DataReaderToDTOConverter.ConvertPateintDiagnosisByPatientId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiagnosisList;
        }

        public List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinioBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientDiagnosisDTO> DiagnosisList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_CHRONIC_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiagnosisList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinioBranchID", ClinioBranchId);
                clinicBranchIDPara.SqlDbType = SqlDbType.Int;
                
                //Mansoor 9 Sep 2011
                //SqlParameter isAllSelectedPara = sqlCommand.Parameters.AddWithValue("@IsAllSelected", isAllSelected);
                //isAllSelectedPara.SqlDbType = SqlDbType.Bit;
                //if (stationIDs != null && stationIDs.Count > 0)
                //{
                //    dtStationIDs = DTOToTableConverter.StationIDsListConverter(stationIDs);
                //}
                //SqlParameter stationIdsTVPPara = sqlCommand.Parameters.AddWithValue("@StationIdsTVP", dtStationIDs);
                //stationIdsTVPPara.SqlDbType = SqlDbType.Structured;
                //stationIdsTVPPara.TypeName = ProjectConstant.STATION_IDS_TVP;
                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        DiagnosisList = DataReaderToDTOConverter.ConvertPateintChronicDiagnosisByPatientId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiagnosisList;
        }

        public List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisByPatientId(int PatientId)
        {
            List<PatientDiagnosisDTO> DiagnosisList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_ACUTE_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiagnosisList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        DiagnosisList = DataReaderToDTOConverter.ConvertPateintAcuteDiagnosisByPatientId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiagnosisList;
        }

        public List<ModuleDictationDTO> LoadModuleDictationByVisitId(int visitId, bool IsTemplateVisit)
        {
            List<ModuleDictationDTO> list = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.MODULE_DICTATION_LOAD_BY_VISIT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                list = new List<ModuleDictationDTO>();

                // Database db = DatabaseFactory.CreateDatabase(ProjectConstant.PREFERENCE_DB); // specify The DB Name
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@VisitID", DbType.Int32, visitId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        GenericDataReaderToDTOConverter.ConvertToDTOList(list, objIReader, typeof(ModuleDictationDTO));
                    }
                }
                if (list != null)
                {
                    foreach (ModuleDictationDTO obj in list)
                    {
                        if (obj != null)
                        {
                            if (IsTemplateVisit)
                            {
                                obj.DictationID = Guid.Empty;
                                obj.VisitID = 0;
                                obj.ModuleDictationId = 0;

                            }
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

        /// Author Ismail
        /// Date 5 may 2011
        /// This Method Will be used to load patient chronic diagnosis by patient Id
        /// </summary>
        /// <returns></returns>
        public List<ProcedureDTO> LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<ProcedureDTO> ProcedureList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_PROCEDURE_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ProcedureList = new List<ProcedureDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        ProcedureList = DataReaderToDTOConverter.ConvertPateintProcedureByPatientId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ProcedureList;
        }


        /// Author Ismail
        /// Date 06 may 2011
        /// This Method Will be used to load patient accute visit by clinicbranch Id
        /// </summary>
        /// <returns></returns>
        public int LoadAccuteVisitTemplateByClinicBranchId(int ClinicBranchId)
        {
            int VisitId = 0;
            string sProc = VisitConstant.ACCUTE_VISIT_LOAD_BY_CLINIC_BRANCH_ID_SP;
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);
                db.AddOutParameter(command, "@VisitID", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    string OutputParam = db.GetParameterValue(command, "@VisitID").ToString();
                    if (!string.IsNullOrEmpty(OutputParam))
                        VisitId = int.Parse(OutputParam);
                   // VisitId = int.Parse(db.GetParameterValue(command, "@VisitID").ToString());
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitId;
        }

        /// Author Ismail
        /// Date 06 may 2011
        /// This Method Will be used to load patient accute visit by clinicbranch Id
        /// </summary>
        /// <returns></returns>
        public int LoadChronicDiseaseVisitTemplateByClinicBranchId(int ClinicBranchId)
        {
            int VisitId = 0;
            string sProc = VisitConstant.CHRONIC_DISEASE_VISIT_LOAD_BY_CLINIC_BRANCH_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);
                db.AddOutParameter(command, "@VisitID", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                    string OutputParam = db.GetParameterValue(command, "@VisitID").ToString();
                    if (!string.IsNullOrEmpty(OutputParam))
                    VisitId = int.Parse(OutputParam);
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitId;
        }


        /// Author Ismail
        /// Date 06 may 2011
        /// This Method Will be used to load patient accute visit by clinicbranch Id
        /// </summary>
        /// <returns></returns>
        public int LoadComprehensiveVisitTemplateByClinicBranchId(int ClinicBranchId)
        {
            int VisitId = 0;
            string sProc = VisitConstant.COMPREHENSIVE_VISIT_LOAD_BY_CLINIC_BRANCH_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);
                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);
                db.AddOutParameter(command, "@VisitID", DbType.Int32, 1);

                if (db.ExecuteNonQuery(command) == 0)
                {
                    throw new RecordNotInsertException(ProjectConstant.RECORD_NOT_INSERTED_ERROR_MSG, ProjectConstant.ERROR_CODE.RecordNotInserted.ToString());
                }
                else
                {
                     string OutputParam = db.GetParameterValue(command, "@VisitID").ToString();
                    if (!string.IsNullOrEmpty(OutputParam))
                        VisitId = int.Parse(OutputParam);
                   // VisitId = int.Parse(db.GetParameterValue(command, "@VisitID").ToString());
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VisitId;
        }

        public List<SocialHistoryDTO> LoadPatientSocialHistoryByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<SocialHistoryDTO> HistoryList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_SOCIAL_HISTORY_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<SocialHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        HistoryList = DataReaderToDTOConverter.PatientSocialHistorByPatientIdyConverter(objIReader, IsTemplateVisit);

                        if (HistoryList != null)
                        {
                            foreach (SocialHistoryDTO obj in HistoryList)
                            {
                                if (obj != null && obj.PatientSocialHistoryID > 0)
                                {
                                    List<SocialHistoryAddictionDTO> HistoryAddictionList = LoadSocialHistoryAddictionBySocialHistoryID(obj.PatientSocialHistoryID, IsTemplateVisit);
                                    if (HistoryAddictionList != null)
                                    {
                                        obj.SocialHistoryAddictionList = HistoryAddictionList;
                                    }

                                    if (IsTemplateVisit)
                                        obj.PatientSocialHistoryID = 0; // IF Template Visit Make it Zero As We have to load addiction that's why it is not set in PatientSocialHistoryConverter Method :)
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        public List<PatientRecommendationDTO> LoadPatientRecommendationByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientRecommendationDTO> ProcedureList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_RECOMMENDATION_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ProcedureList = new List<PatientRecommendationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        ProcedureList = DataReaderToDTOConverter.ConvertPatientRecommendationByPatientId(objIReader, IsTemplateVisit);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ProcedureList;
        }

        public List<Quivus.Model.QuickInput.FollowUp.PatientFollowUpDTO> LoadPatientFollowUpByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientFollowUpDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_FOLLOW_UP_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PatientFollowUpDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        PatientFollowUpDTO obj = DataReaderToDTOConverter.ConvertToPatientFollowUpByPatientId(objIReader, IsTemplateVisit);

                        if (obj != null)
                        {
                            if (List == null)
                                List = new List<Quivus.Model.QuickInput.FollowUp.PatientFollowUpDTO>();

                            List.Add(obj);
                        }

                       
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, bool IsTemplateVisit, List<int> stationIDs, bool isAllSelected)
        {
            List<LabDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LABS_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<LabDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
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
                        List = DataReaderToDTOConverter.PatientLabsByPatientIdConverter(objIReader, IsTemplateVisit);
                                                
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<ModuleDictationDTO> LoadModuleDictationByPatientId(int PatientId, bool IsTemplateVisit, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ModuleDictationDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_MODULE_DICTATION_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ModuleDictationDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
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
                        List = DataReaderToDTOConverter.PatientModuleDictationConverter(objIReader, IsTemplateVisit);

                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<ModuleNoteDTO> LoadModuleNotesByPatientId(int PatientId, bool IsTemplateVisit, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ModuleNoteDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_MODULE_NOTES_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<ModuleNoteDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
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
                        List = DataReaderToDTOConverter.PatientModuleNotesConverter(objIReader, IsTemplateVisit);

                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionByPatientId(int PatientId, bool IsTemplateVisit, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientFollowupInstructionDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_FOLLOW_UP_INSTRUCTION_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PatientFollowupInstructionDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
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
                        List = DataReaderToDTOConverter.FollowupInstructionByPatientIdConverter(objIReader, IsTemplateVisit);

                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<SocialhistoryVBDTO> LoadSocialHistoryByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<SocialhistoryVBDTO> HistoryList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.SOCIAL_HISTORY_LOAD_BY_PATIENT_ID_SP;
            //TODO Both Are Same Diff ? B/W Patient Social Load By Patient Id & Social Load By Patient Id 
            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HistoryList = new List<SocialhistoryVBDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                SqlParameter clinicBranchIDPara = sqlCommand.Parameters.AddWithValue("@ClinicBranchID", ClinicBranchId);
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
                        HistoryList = DataReaderToDTOConverter.SocialHistorByPatientIdyConverter(objIReader, IsTemplateVisit);

                        //if (HistoryList != null)
                        //{
                        //    foreach (SocialHistoryDTO obj in HistoryList)
                        //    {
                        //        if (obj != null && obj.PatientSocialHistoryID > 0)
                        //        {
                        //            List<SocialHistoryAddictionDTO> HistoryAddictionList = LoadSocialHistoryAddictionBySocialHistoryID(obj.PatientSocialHistoryID, IsTemplateVisit);
                        //            if (HistoryAddictionList != null)
                        //            {
                        //                obj.SocialHistoryAddictionList = HistoryAddictionList;
                        //            }

                        //            if (IsTemplateVisit)
                        //                obj.PatientSocialHistoryID = 0; // IF Template Visit Make it Zero As We have to load addiction that's why it is not set in PatientSocialHistoryConverter Method :)
                        //        }
                        //    }
                        //}
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HistoryList;
        }

        public List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientIdANDClinicBranchIDForRecommendation(int PatientId, int ClinicBranchId)
        {
            List<PatientDiagnosisDTO> DiagnosisList = null;
            IDataReader objIReader = null;
            string sProc = VisitConstant.PATIENT_DIAGNOSIS_LOAD_BY_PATIENT_ID_CLINIC_BRANCH_ID_FOR_RECOMMENDATION_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DiagnosisList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                DbCommand command = db.GetStoredProcCommand(sProc); // Specify Store Procedure Name

                db.AddInParameter(command, "@PatientID", DbType.Int32, PatientId);
                db.AddInParameter(command, "@ClinicBranchID", DbType.Int32, ClinicBranchId);

                using (objIReader = db.ExecuteReader(command))
                {
                    if (objIReader != null)
                    {
                        DiagnosisList = DataReaderToDTOConverter.ConvertPateintDiagnosisByprescriptionId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DiagnosisList;
        }




        //Mansoor
        public List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId)
        {
            List<MedicalHistoryDTO> MedicalHistoryList = null;
            IDataReader objIReader = null;            
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_MEDICAL_HISTORY;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                MedicalHistoryList = new List<MedicalHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
                
                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        MedicalHistoryList = DataReaderToDTOConverter.ConvertPatientMedicalHistory(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return MedicalHistoryList;
        }

        //Mansoor
        public List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId)
        {
            List<FamilyHistoryDTO> FamilyHistoryList = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_FAMILY_HISTORY;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                FamilyHistoryList = new List<FamilyHistoryDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        FamilyHistoryList = DataReaderToDTOConverter.ConvertPatientFamilyHistory(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return FamilyHistoryList;
        }

        //Mansoor
        public List<SocialhistoryVBDTO> LoadSocialHistoryAllByPatientId(int PatientId)
        {
            List<SocialhistoryVBDTO> SocialhistoryList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_SOCIAL_HISTORY;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                SocialhistoryList = new List<SocialhistoryVBDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        SocialhistoryList = DataReaderToDTOConverter.SocialHistorByPatientIdyConverter(objIReader, false,PatientId);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return SocialhistoryList;
        }
        
        //Mansoor
        public List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int patientId)
        {
            List<ImmunizationDTO> ImmunizationList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_IMMUNIZATION;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ImmunizationList = new List<ImmunizationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", patientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        ImmunizationList = DataReaderToDTOConverter.ImmunizationPatientIdConverter(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ImmunizationList;
        }
                
        //Mansoor
        public List<VitalDTO> LoadPatientVitalAll(int PatientId)
        {
            List<VitalDTO> VitalList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_VITAL;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                VitalList = new List<VitalDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        VitalList = DataReaderToDTOConverter.ConvertPatientVitals(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return VitalList;
        }

        //Mansoor         
        public List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId)
        {
            List<HPI_CCDTO> HPIList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_HPI;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                HPIList = new List<HPI_CCDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        HPIList = DataReaderToDTOConverter.ConvertPatientHPI(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return HPIList;
        }
        
        //Mansoor         
        public List<ROSDTO> LoadROSAllByPatientId(int PatientId)
        {
            List<ROSDTO> ROSIList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_ROS;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ROSIList = new List<ROSDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        ROSIList = DataReaderToDTOConverter.ConvertPatientROS(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ROSIList;
        }

        //Mansoor         
        public List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId)
        {
            List<PhysicalsDTO> PhysicalsList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_PHYSICALS;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                PhysicalsList = new List<PhysicalsDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        PhysicalsList = DataReaderToDTOConverter.ConvertPatientPhysicals(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return PhysicalsList;
        }

        //Mansoor 
        public List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId)
        {
            List<ModuleDictationDTO> DictationList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_DICTATION;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                DictationList = new List<ModuleDictationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        DictationList = DataReaderToDTOConverter.PatientModuleDictationConverter(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return DictationList;
        }

        //Mansoor 
        public List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId)
        {
            List<ModuleNoteDTO> NotesList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_NOTES;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                NotesList = new List<ModuleNoteDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        NotesList = DataReaderToDTOConverter.PatientModuleNotesConverter(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return NotesList;
        }

        //Mansoor 
        public List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId)
        {
            List<InHouseLabDTO> LabList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_INHOUSE_LABS;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                LabList = new List<InHouseLabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        LabList = DataReaderToDTOConverter.ConvertInHouseLabsByPatientId(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return LabList;
        }

        //Mansoor 
        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            List<LabDTO> LabList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_LABS;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                LabList = new List<LabDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        LabList = DataReaderToDTOConverter.PatientLabsByPatientIdConverter(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return LabList;
        }

        //Mansoor 
        public List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId)
        {
            List<ProcedureDTO> ProcedureList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_PROCEDURE;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                ProcedureList = new List<ProcedureDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        ProcedureList = DataReaderToDTOConverter.ConvertPRocedureDto(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return ProcedureList;
        }

        //Mansoor 
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId)
        {
            List<PrescriptionMedicationDTO> medicationList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_PRESCRIPTION_MEDICATION;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                medicationList = new List<PrescriptionMedicationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        medicationList = DataReaderToDTOConverter.ConvertPrescriptionMedication(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return medicationList;
        }

        //Mansoor 
        public List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId)
        {
            List<PatientRecommendationDTO> recommendationList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_RECOMMENDATION;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                recommendationList = new List<PatientRecommendationDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        recommendationList = DataReaderToDTOConverter.ConvertPatientRecommendationByPatientId(objIReader, false);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return recommendationList;
        }

        //Mansoor 
        public  List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId)
        {
            List<PatientDiagnosisDTO> recommendationList = null;
            IDataReader objIReader = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_LOAD_ALL_CHRONIC_DIAGNOSIS;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                recommendationList = new List<PatientDiagnosisDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;

                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        recommendationList = DataReaderToDTOConverter.ConvertPateintChronicDiagnosisByPatientId(objIReader);
                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return recommendationList;
        }

        //Mansoor
        public List<Quivus.Model.QuickInput.FollowUp.PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId)
        {
            List<PatientFollowUpDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_FOLLOW_UP_ALL_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PatientFollowUpDTO>();

                Database db = DatabaseFactory.CreateDatabase(); // Load default DB i.e AddressBook
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;              
                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        PatientFollowUpDTO obj = DataReaderToDTOConverter.ConvertToPatientFollowUp(objIReader, false);

                        if (obj != null)
                        {
                            if (List == null)
                                List = new List<Quivus.Model.QuickInput.FollowUp.PatientFollowUpDTO>();

                            List.Add(obj);
                        }


                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId)
        {
            List<PatientFollowupInstructionDTO> List = null;
            IDataReader objIReader = null;
            DataTable dtStationIDs = null;
            SqlConnection connection = null;
            string sProc = VisitConstant.PATIENT_FOLLOW_UP_INSTRUCTION_ALL_LOAD_BY_PATIENT_ID_SP;

            try
            {
                QuivusLogger.Info("Calling SP:: " + sProc);

                List = new List<PatientFollowupInstructionDTO>();

                Database db = DatabaseFactory.CreateDatabase();
                connection = new SqlConnection(db.ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(sProc, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter patientIDPara = sqlCommand.Parameters.AddWithValue("@PatientID", PatientId);
                patientIDPara.SqlDbType = SqlDbType.Int;
               
                connection.Open();

                using (objIReader = sqlCommand.ExecuteReader())
                {
                    if (objIReader != null)
                    {
                        List = DataReaderToDTOConverter.FollowupInstructionByPatientIdConverter(objIReader, false);

                    }
                }
            }
            catch
            {
                throw;// Just Re-Throw Exception From DAO LEVEL
            }
            return List;
        }
    }
}
