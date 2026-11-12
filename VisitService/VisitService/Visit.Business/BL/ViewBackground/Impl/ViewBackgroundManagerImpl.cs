using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Utility;
using Quivus.Utility.CustomException;
using Visit.DataAccess;
using Quivus.Utility.Factory;
using Visit.DataAccess.DAO;
using Quivus.Utility.Logger;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.Notes;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.ROS;
using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;


using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.MainQuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory;
using Quivus.Model.QuickInput.MainQuickInput.PMH;
using Quivus.Model.QuickInput.MainQuickInput.Physical;
using Quivus.Model.QuickInput.MainQuickInput.Assessment;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.Visit;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Dictation;
using QuivusModel.QuickInput.ComprehensiveHistory.Social;


namespace Visit.Business.BL.ViewBackground.Impl
{
    public class ViewBackgroundManagerImpl : IViewBackgroundManager
    {

        private ViewBackgroundDAO viewBackgroundDAO = null;
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Patient Medical History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryDTO> LoadPatientMedicalHistoryByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<MedicalHistoryDTO> MedicalHistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicalHistoryByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                MedicalHistoryList = viewBackgroundDAO.LoadPatientMedicalHistoryByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
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
            List<ImmunizationDTO> ImmunizationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadImmunizationsByVisitId PARAM :: visitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISTI_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ImmunizationList = viewBackgroundDAO.LoadImmunizationsByVisitId(visitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ImmunizationList;
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
            List<ModuleNoteDTO> ModuleNotesList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesByVisitId PARAM :: visitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISTI_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ModuleNotesList = viewBackgroundDAO.LoadModuleNotesByVisitId(visitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ModuleNotesList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Family History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<FamilyHistoryDTO> LoadPatientFamilyHistoryByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<FamilyHistoryDTO> FamilyHistoryList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicalHistoryByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                FamilyHistoryList = viewBackgroundDAO.LoadPatientFamilyHistoryByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return FamilyHistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Vital History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitalByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<VitalDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitalByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientVitalByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<LabDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabsByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientLabsByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 18 March 2011
        /// This Method Will be used to load Patient Previous 10 Vitals History 
        /// </summary>
        /// <returns></returns>
        public List<RecentVitalDTO> LoadPatientPreviousVitals(int PatientId)
        {
            List<RecentVitalDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientPreviousVitals PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientPreviousVitals(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }

        public List<ROSDTO> LoadPatientROSByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<ROSDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientROSByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientROSByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }

        public List<SocialHistoryAddictionDTO> LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId, bool IsTemplateVisit)
        {
            List<SocialHistoryAddictionDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientROSByVisit PARAM :: PatientSocialHistoryId " + PatientSocialHistoryId);

                if (PatientSocialHistoryId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadSocialHistoryAddictionBySocialHistoryID(PatientSocialHistoryId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by visit Id
        /// </summary>
        /// <returns></returns>  
        public List<HPI_CCDTO> LoadPatientHPIByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<HPI_CCDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientHPIByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientHPIByVisitId(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
        }


        public List<SocialHistoryDTO> LoadPatientSocialHistoryByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<SocialHistoryDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientROSByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadSocialHistoryByVisitId(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
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
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationByVisit PARAM :: VisitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                RecommendationList = viewBackgroundDAO.LoadPatientRecommendationByVisitId(visitId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return RecommendationList;
        }

        public List<PhysicalsDTO> LoadPatientPhysicalsByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<PhysicalsDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientPhysicalsByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientPhysicalsByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return List;
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
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByVisitId PARAM :: VisitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                diagnosisList = viewBackgroundDAO.LoadPatientDiagnosisByVisitId(visitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return diagnosisList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Recommendation History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<PatientRecommendationDTO> LoadPatientRecommendationByVisit(int VisitId, bool IsTemplateVisit)
        {
            List<PatientRecommendationDTO> TretmentList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationByVisit PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                TretmentList = viewBackgroundDAO.LoadPatientRecommendationByVisit(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return TretmentList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Recommendation History by visit Id
        /// </summary>
        /// <returns></returns>
        public QuickInputDTO LoadQuickInputByVisit(int VisitId,bool IsTemplateVisit)
        {
            QuickInputDTO quickIputObject = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling LoadQuickInputByVisit VisitId :: " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                VisitDTO visitObj = LoadVisitDataByVisitId(VisitId, IsTemplateVisit);

                if (visitObj != null)
                {
                    if (quickIputObject == null)
                    {
                        quickIputObject = new QuickInputDTO();
                    }
                    quickIputObject.Visit = visitObj;
                }
                quickIputObject = LoadQuickInputSection(VisitId, quickIputObject, IsTemplateVisit);
                //TODO Load all quick input

            }
            catch
            {
                throw;
            }
            return quickIputObject;
        }

        private QuickInputDTO LoadQuickInputSection(int VisitId, QuickInputDTO quickIputObject, bool IsTemplateVisit)
        {
            try
            {
                PMHDTO PMH = LoadPMHSection(VisitId, IsTemplateVisit);

                if (PMH != null)
                {
                    if (quickIputObject == null)
                    {
                        quickIputObject = new QuickInputDTO();
                    }
                    quickIputObject.PMH = PMH;
                }

                PhysicalDTO Physical = LoadPhysicalSection(VisitId, IsTemplateVisit);

                if (Physical != null)
                {
                    if (quickIputObject == null)
                    {
                        quickIputObject = new QuickInputDTO();
                    }
                    quickIputObject.Physical = Physical;
                }

                AssessmentDTO Assessment = LoadAssessmentSection(VisitId, IsTemplateVisit);

                if (Assessment != null)
                {
                    if (quickIputObject == null)
                    {
                        quickIputObject = new QuickInputDTO();
                    }
                    quickIputObject.Assessment = Assessment;
                }

                List<ModuleNoteDTO> ModuleNotesList = LoadModuleNotesByVisitId(VisitId, IsTemplateVisit);

                if (ModuleNotesList != null)
                    quickIputObject.ModuleNotes = ModuleNotesList;


                List<ModuleDictationDTO> ModuleDictationList = LoadModuleDictationByVisitId(VisitId, IsTemplateVisit);

               if (ModuleDictationList != null)
                   quickIputObject.ModuleDictations = ModuleDictationList;

                

            }
            catch
            {
                throw;
            }
            return quickIputObject;
        }

        private PMHDTO LoadPMHSection(int VisitId, bool IsTemplateVisit)
        {
            PMHDTO PMH = null;
            try
            {
                ComprehensiveHistoryDTO comprehensiveHistory = LoadComprehensiveHistory(VisitId, IsTemplateVisit);

                if (comprehensiveHistory != null)
                {
                    if (PMH == null)
                    {
                        PMH = new PMHDTO();
                    }
                    PMH.ComprehensiveHistory = comprehensiveHistory;
                }

                List<ImmunizationDTO> ImmunizationList = LoadImmunizationsByVisitId(VisitId,IsTemplateVisit);

                if (ImmunizationList != null && ImmunizationList.Count > 0)
                {
                    if (PMH == null)
                    {
                        PMH = new PMHDTO();
                    }
                    PMH.Immunization = ImmunizationList;
                }

            }
            catch
            {
                throw;
            }
            return PMH;
        }

        private ComprehensiveHistoryDTO LoadComprehensiveHistory(int VisitId,bool IsTemplateVisit)
        {
            ComprehensiveHistoryDTO comprehensiveHistory = null;

            try
            {
                List<MedicalHistoryDTO> MedicalHistoryList = LoadPatientMedicalHistoryByVisit(VisitId, IsTemplateVisit);

                if (MedicalHistoryList != null && MedicalHistoryList.Count > 0)
                {
                    if (comprehensiveHistory == null)
                        comprehensiveHistory = new ComprehensiveHistoryDTO();

                    comprehensiveHistory.MedicalHistory = MedicalHistoryList;
                }

                List<FamilyHistoryDTO> FamilHistoryList = LoadPatientFamilyHistoryByVisit(VisitId, IsTemplateVisit);

                if (FamilHistoryList != null && FamilHistoryList.Count > 0)
                {
                    if (comprehensiveHistory == null)
                        comprehensiveHistory = new ComprehensiveHistoryDTO();

                    comprehensiveHistory.FamilyHistory = FamilHistoryList;
                }

                List<SocialHistoryDTO> SocialHistoryList = LoadPatientSocialHistoryByVisitId(VisitId,IsTemplateVisit);

                if (SocialHistoryList != null && SocialHistoryList.Count > 0)
                {
                    if (comprehensiveHistory == null)
                        comprehensiveHistory = new ComprehensiveHistoryDTO();

                    comprehensiveHistory.SocialHistory = SocialHistoryList;
                }
            }
            catch
            {
                throw;
            }
            return comprehensiveHistory;
        }


        private PhysicalDTO LoadPhysicalSection(int VisitId, bool IsTemplateVisit)
        {
            PhysicalDTO Physical = null;
            try
            {
                List<VitalDTO> VitalList = LoadPatientVitalByVisit(VisitId, IsTemplateVisit);

                if (VitalList != null && VitalList.Count > 0)
                {
                    if (Physical == null)
                    {
                        Physical = new PhysicalDTO();
                    }
                    Physical.Vitals = VitalList[0]; // As in Vital There is only one record
                }

                List<HPI_CCDTO> HPIList = LoadPatientHPIByVisitId(VisitId, IsTemplateVisit);
                if (HPIList != null && HPIList.Count > 0)
                {
                    if (Physical == null)
                    {
                        Physical = new PhysicalDTO();
                    }
                    Physical.HPI_CC = HPIList;
                }

                List<ROSDTO> ROSList = LoadPatientROSByVisit(VisitId, IsTemplateVisit);
                if (ROSList != null && ROSList.Count > 0)
                {
                    if (Physical == null)
                    {
                        Physical = new PhysicalDTO();
                    }
                    Physical.ReviewOfSystem = ROSList;
                }

                List<PhysicalsDTO> PhysicalList = LoadPatientPhysicalsByVisit(VisitId, IsTemplateVisit);
                if (PhysicalList != null && PhysicalList.Count > 0)
                {
                    if (Physical == null)
                    {
                        Physical = new PhysicalDTO();
                    }
                    Physical.Physicals = PhysicalList;
                }

                List<LabDTO> LabList = LoadPatientLabsByVisit(VisitId, IsTemplateVisit);
                if (LabList != null && LabList.Count > 0)
                {
                    if (Physical == null)
                    {
                        Physical = new PhysicalDTO();
                    }
                    Physical.Labs = LabList; // As in Vital There is only one record
                }

                List<InHouseLabDTO> InHouseLabList = LoadPateintInHouseLabsByVisitId(VisitId, IsTemplateVisit);
                if (InHouseLabList != null && InHouseLabList.Count > 0)
                {
                    if (Physical == null)
                    {
                        Physical = new PhysicalDTO();
                    }
                    Physical.InHouseLabs = InHouseLabList;
                }


            }
            catch
            {
                throw;
            }
            return Physical;

        }

        private AssessmentDTO LoadAssessmentSection(int VisitId,bool IsTemplateVisit)
        {
            AssessmentDTO Assessment = null;
            try
            {
                List<PatientDiagnosisDTO> DiagnosisList = LoadPatientDiagnosisByVisitId(VisitId, IsTemplateVisit);

                if (DiagnosisList != null && DiagnosisList.Count > 0)
                {
                    if (Assessment == null)
                    {
                        Assessment = new AssessmentDTO();
                    }
                    Assessment.Diagnosis = DiagnosisList; // As in Vital There is only one record
                }

                List<PatientRecommendationDTO> RecommendationList = LoadPatientRecommendationByVisit(VisitId, IsTemplateVisit);

                if (RecommendationList != null && RecommendationList.Count > 0)
                {
                    if (Assessment == null)
                    {
                        Assessment = new AssessmentDTO();
                    }
                    Assessment.Recommendation = RecommendationList;
                }

                PatientFollowUpDTO FollowUpDTO = LoadFollowUpByVisitId(VisitId, IsTemplateVisit);

                if (FollowUpDTO != null)
                {
                    if (Assessment == null)
                    {
                        Assessment = new AssessmentDTO();
                    }
                    Assessment.PatientFollowUp = FollowUpDTO;
                }

                List<PatientFollowupInstructionDTO> FollowupInstructionDTOList = LoadFollowupInstructionDTOByVisitId(VisitId, IsTemplateVisit);

                if (FollowUpDTO != null)
                {
                    if (Assessment == null)
                    {
                        Assessment = new AssessmentDTO();
                    }
                    Assessment.PatientFollowupInstruction = FollowupInstructionDTOList;
                }


                List<ProcedureDTO> ProcedureDTOList = LoadProcedureDTOListByVisitId(VisitId, IsTemplateVisit);

                if (ProcedureDTOList != null)
                {
                    if (Assessment == null)
                    {
                        Assessment = new AssessmentDTO();
                    }
                    Assessment.Procedure = ProcedureDTOList;
                }

                //List<PatientRecommendationDTO> RecommendationDTOList = LoadPatientRecommendationByVisitId(VisitId);

                //if (RecommendationDTOList != null)
                //{
                //    if (Assessment == null)
                //    {
                //        Assessment = new AssessmentDTO();
                //    }
                //    Assessment.Recommendation = RecommendationDTOList;
                //}

            }
            catch
            {
                throw;
            }
            return Assessment;
        }

        public List<ProcedureDTO> LoadProcedureDTOListByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<ProcedureDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling [PatientProcedure_LoadByVisit] PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadProcedureDTOListByVisitId(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
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
            try
            {
                QuivusLogger.Info("Calling LoadFollowupByVisitId PARAM :: VisitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                followUp = viewBackgroundDAO.LoadFollowUpByVisitId(visitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return followUp;
        }

        public List<VisitDTO> LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId)
        {
            List<VisitDTO> visitList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVisitByClinicBranchId PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (ClinicBranchId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                visitList = viewBackgroundDAO.LoadPatientVisitByClinicBranchId(VisitId, ClinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return visitList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load visit stake holder by visit by Id
        /// </summary>
        /// <returns></returns>
        public List<VisistStakeHolderDTO> LoadVisitStakeHolderByVisitId(int VisitId, int UserTypeId)
        {
            List<VisistStakeHolderDTO> visitList = null;
            try
            {
                QuivusLogger.Info("Calling LoadVisitStakeHolderByVisitId PARAM :: VisitId " + VisitId + "UserTypeId" + UserTypeId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                visitList = viewBackgroundDAO.LoadVisitStakeHolderByVisitId(VisitId, UserTypeId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return visitList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient medication prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        public List<MedicationPrescribedDTO> LoadPatientMedicationPrescribedByPatient(int PatientId)
        {
            List<MedicationPrescribedDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicationPrescribedByPatient PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientMedicationPrescribedByPatient(PatientId);
            }
            catch
            {
                throw;
            }
            return List;
        }

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        public List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId)
        {
            List<AllergyDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAllergyByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientAllergyByPatientId(PatientId);
            }
            catch
            {
                throw;
            }
            return List;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitals(int PatientId, int ClinicBranchId)
        {
            List<VitalDTO> List = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitals PARAM :: PatientId " + PatientId + "ClinicBranchId" + ClinicBranchId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                List = viewBackgroundDAO.LoadPatientVitals(PatientId, ClinicBranchId);
            }
            catch
            {
                throw;
            }
            return List;
        }

        /// <summary>
        /// Author Ismail
        /// Date 05 April 2011
        /// This Method Will be used to load visit by visit by Id
        /// IsTemplateVisit Bool Added To Check Either it is template visit or simple
        /// In Case Of Template Visit Remove Primary Key/Date Information
        /// </summary>
        /// <returns></returns>
        public VisitDTO LoadVisitDataByVisitId(int VisitId, bool IsTemplateVisit)
        {
            VisitDTO visitObj = null;
            try
            {
                QuivusLogger.Info("Calling LoadVisitDataByVisitId PARAM :: VisitId " + VisitId + "IsTemplateVisit::" + IsTemplateVisit);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                visitObj = viewBackgroundDAO.LoadVisitDataByVisitId(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return visitObj;
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
            try
            {
                QuivusLogger.Info("Calling LoadPateintPrescriptionByVisitId PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                PrescriptionList = viewBackgroundDAO.LoadPateintPrescriptionByVisitId(VisitId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return PrescriptionList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientPrescriptionId(int PatientPrescriptionId)
        {
            List<PrescriptionMedicationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPrescriptionMedicationByPatientPrescriptionId PARAM :: PatientPrescriptionId " + PatientPrescriptionId);

                if (PatientPrescriptionId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_PRESCRIPTION_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPrescriptionMedicationByPatientPrescriptionId(PatientPrescriptionId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        public List<MedicationDTO> LoadPatientMedicationByPatientId(int PatientId)
        {
            List<MedicationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientMedicationByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientMedicationByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientDiagnosisDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientDiagnosisByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Sami
        /// Date 8/12/2011
        /// This Method Will be used to load patient medication by patient diagnosis Id for recommendation
        /// </summary>
        /// <returns></returns>
        public List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId)
        {
            List<PatientDiagnosisDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientDiagnosisByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientDiagnosisByPatientIdANDClinicBranchIDForRecommendation(PatientId, ClinicBranchId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionDTOByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<PatientFollowupInstructionDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupInstructionDTOByVisitId PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadFollowupInstructionDTOByVisitId(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load presription medication by patient  Id
        /// </summary>
        /// <returns></returns>
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientId(int PatienId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PrescriptionMedicationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPrescriptionMedicationByPatientId PARAM :: PatientPrescriptionId " + PatienId);

                if (PatienId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPrescriptionMedicationByPatientId(PatienId, ClinicBranchId,  stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Atta
        /// Date 14 April 2011
        /// This Method Will be used to load presription medication by patient  Id
        /// </summary>
        /// <returns></returns>
        public List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            List<FollowupLabsDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabTestsForFollowupByPatient PARAM :: PatientPrescriptionId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }                

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientLabTestsForFollowupByPatient(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by visit by Id
        /// </summary>
        /// <returns></returns>
        public List<InHouseLabDTO> LoadPateintInHouseLabsByVisitId(int VisitId, bool IsTemplateVisit)
        {
            List<InHouseLabDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPateintInHouseLabsByVisitId PARAM :: VisitId " + VisitId);

                if (VisitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISIT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPateintInHouseLabsByVisitId(VisitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        public List<InHouseLabDTO> LoadPateintInHouseLabsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<InHouseLabDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPateintInHouseLabsByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPateintInHouseLabsByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryDTO> LoadMedicalHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<MedicalHistoryDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadMedicalHistoryByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadMedicalHistoryByPatientId(PatientId,ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        public List<FamilyHistoryDTO> LoadFamilyHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<FamilyHistoryDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFamilyHistoryByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadFamilyHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient vital by patient Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadVitalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<VitalDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadVitalsByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadVitalsByPatientId(PatientId, ClinicBranchId,  stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        public List<HPI_CCDTO> LoadHPIByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<HPI_CCDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadHPIByPatientId(PatientId,ClinicBranchId,  stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        public List<ROSDTO> LoadROSByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<ROSDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadROSByPatientId(PatientId, ClinicBranchId,  stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// Author Ismail
        /// Date 28 April 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        public List<PhysicalsDTO> LoadPhysicalsByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PhysicalsDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPhysicalsByPatientId(PatientId, ClinicBranchId, stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisByPatientId(int PatientId)
        {
            List<PatientDiagnosisDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientResolvedDiagnosisByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientResolvedDiagnosisByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientDiagnosisDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientChronicDiagnosisByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientChronicDiagnosisByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        public List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisByPatientId(int PatientId)
        {
            List<PatientDiagnosisDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientAcuteDiagnosisByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientAcuteDiagnosisByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public List<ModuleDictationDTO> LoadModuleDictationByVisitId(int visitId, bool IsTemplateVisit)
        {
            List<ModuleDictationDTO> ModuleDictationList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleDictationByVisitId PARAM :: visitId " + visitId);

                if (visitId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.VISTI_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ModuleDictationList = viewBackgroundDAO.LoadModuleDictationByVisitId(visitId, IsTemplateVisit);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ModuleDictationList;
        }

          /// <summary>
        /// Author Ismail
        /// Date 5 may 2011
        /// This Method Will be used to load patient chronic diagnosis by patient Id
        /// </summary>
        /// <returns></returns>
        public List<ProcedureDTO> LoadPatientProcedureByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<ProcedureDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationsByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientProcedureByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


         /// <summary>
        /// Author Muhammad Ismail 
        /// Date 06 May 2011
        /// This Method Will be used to import visit By template
        /// </summary>
        /// <returns></returns>
        public QuickInputDTO ImportVisitByTemplate(int TemplateId, int ClinicBranchId)
        {
            QuickInputDTO QIObject = null;
            int VisitId = 0;

            // Decide On Template Id to Which Method to Call Either Accute,Chronic,Comprehensive
            if (viewBackgroundDAO == null)
            {
                viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
            }

       //     int TempId = (int)ProjectConstant.VISIT_TEMPLATE_TYPE.AccuteVisit;

            if (TemplateId.Equals((int)ProjectConstant.VISIT_TEMPLATE_TYPE.AccuteVisit))
            {
               VisitId= viewBackgroundDAO.LoadAccuteVisitTemplateByClinicBranchId(ClinicBranchId);
            }
            else if (TemplateId.Equals((int)ProjectConstant.VISIT_TEMPLATE_TYPE.ChronicDiseaseVisit))
            {

              VisitId= viewBackgroundDAO.LoadChronicDiseaseVisitTemplateByClinicBranchId(ClinicBranchId);
            }
            else if (TemplateId.Equals((int)ProjectConstant.VISIT_TEMPLATE_TYPE.ComprehensiveVisit))
            {
               VisitId= viewBackgroundDAO.LoadComprehensiveVisitTemplateByClinicBranchId(ClinicBranchId);
            }
            else
            {
                throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, ProjectConstant.TEMPLATE_ID_INVALID_ERROR_MSG);
            }

            if (VisitId > 0)
            {
                // Load Visit/QI Info By Visit Id & Referesh its VisitId,VisitDate,CreatedDate,ModifiedDate,CrestedAt,ModifiedAt
                // & Primary Key

                QIObject = LoadQuickInputByVisit(VisitId, true); // This True Indicates that it is Template Visit Load Call
            }
            else
            {
                throw new InvalidArgumentException(ProjectConstant.NO_VISIT_TEMPLATE_FOUND);
            }
           
            return QIObject;
        }

         /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient social history by patient Id
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryDTO> LoadPatientSocialHistoryByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<SocialHistoryDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientSocialHistoryByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientSocialHistoryByPatientId(PatientId, IsTemplateVisit, ClinicBranchID,  stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

         /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public List<SocialhistoryVBDTO> LoadSocialHistoryByPatientId(int PatientId, bool IsTemplateVisit, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<SocialhistoryVBDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadSocialHistoryByPatientId(PatientId, IsTemplateVisit, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }
        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        public List<PatientRecommendationDTO> LoadPatientRecommendationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientRecommendationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientRecommendationByPatientId(PatientId, false, ClinicBranchId,stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

          /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>

        public List<PatientFollowUpDTO> LoadPatientFollowUpByPatientId(int PatientId, int ClinicBranchId,List<int> stationIDs, bool isAllSelected)
        {
            List<PatientFollowUpDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowUpByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientFollowUpByPatientId(PatientId, false,ClinicBranchId,stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

         /// <summary>
       /// Author Muhammad Ismail 
       /// Date 10 May 2011
       /// This Method Will be used to load Patient Labs by patrint Id
       /// </summary>
       /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByPatientId(int PatientId,int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<LabDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabsByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientLabsByPatientId(PatientId, clinicBranchID, false, stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

          /// <summary>
       /// Author Muhammad Ismail 
       /// Date 20 May 2011
       /// This Method Will be used to load Patient module dictation by patrint Id
       /// </summary>
       /// <returns></returns>
        public List<ModuleDictationDTO> LoadModuleDictationByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ModuleDictationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleDictationByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadModuleDictationByPatientId(PatientId, false, clinicBranchID,stationIDs, isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

         /// <summary>
       /// Author Muhammad Ismail 
       /// Date 20 May 2011
       /// This Method Will be used to load Patient module notes by patrint Id
       /// </summary>
       /// <returns></returns>
        public List<ModuleNoteDTO> LoadModuleNotesByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<ModuleNoteDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadModuleNotesByPatientId(PatientId, false, clinicBranchID,  stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

         /// Author Ismail
       /// Date 22 may 2011
       /// </summary>
       /// <param name="InputList"></param>
       /// <returns></returns>

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientFollowupInstructionDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupInstructionByPatientId PARAM :: PatientId " + PatientId + "clinicBranchID::" + clinicBranchID);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                else if (clinicBranchID <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.CLINIC_BRANCH_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadFollowupInstructionByPatientId(PatientId, false, clinicBranchID,  stationIDs,  isAllSelected);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        #region WEB PORTAL

        //Alerady added
        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>              
        public List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId)
        {
            List<MedicalHistoryDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadMedicalHistoryAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }               
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadMedicalHistoryAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>               
        public List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId)
        {
            List<FamilyHistoryDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFamilyHistoryAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }               
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadFamilyHistoryAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>        
        public List<SocialhistoryVBDTO> LoadSocialHistoryAllByPatientId(int PatientId)
        {
            List<SocialhistoryVBDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadSocialHistoryAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadSocialHistoryAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        //Alerady added
        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //[FaultContract(typeof(QuivusFault))]
        //List<MedicationDTO> LoadPatientMedicationByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>       
        public List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int patientId)
        {
            List<ImmunizationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientImmunizationAllByPatientId PARAM :: PatientId " + patientId);

                if (patientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientImmunizationAllByPatientId(patientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>       
        public List<VitalDTO> LoadPatientVitalAll(int PatientId)
        {
            List<VitalDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientVitalAll PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientVitalAll(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId)
        {
            List<HPI_CCDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadHPIAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadHPIAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<ROSDTO> LoadROSAllByPatientId(int PatientId)
        {
            List<ROSDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadROSAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadROSAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId)
        {
            List<PhysicalsDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPhysicalsAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPhysicalsAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }




        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>        
        public List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId)
        {
            List<ModuleDictationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleDictationAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadModuleDictationAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>        
        public List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId)
        {
            List<ModuleNoteDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadModuleNotesAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadModuleNotesAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>                
        public List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId)
        {
            List<InHouseLabDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientInHouseLabsAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientInHouseLabsAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>                
        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            List<LabDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientLabsAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientLabsAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient procedure by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId)
        {
            List<ProcedureDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientProcedureAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientProcedureAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>        
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId)
        {
            List<PrescriptionMedicationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPrescriptionMedicationAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPrescriptionMedicationAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId)
        {
            List<PatientRecommendationDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientRecommendationAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientRecommendationAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>        
        public List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId)
        {
            List<PatientDiagnosisDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientChronicDiagnosisAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }
                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientChronicDiagnosisAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>

        public List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId)
        {
            List<PatientFollowUpDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadPatientFollowUpAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadPatientFollowUpAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId)
        {
            List<PatientFollowupInstructionDTO> ReturnList = null;
            try
            {
                QuivusLogger.Info("Calling LoadFollowupInstructionAllByPatientId PARAM :: PatientId " + PatientId);

                if (PatientId <= 0)
                {
                    throw new InvalidArgumentException(ProjectConstant.PATIENT_ID_INVALID_ERROR_MSG, (int)ProjectConstant.ERROR_CODE.InvalidArguments);
                }

                if (viewBackgroundDAO == null)
                {
                    viewBackgroundDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                }

                ReturnList = viewBackgroundDAO.LoadFollowupInstructionAllByPatientId(PatientId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ReturnList;
        }

        #endregion
    }
}
