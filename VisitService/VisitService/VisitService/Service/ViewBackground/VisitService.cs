using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quivus.Model.QuickInput.Notes;

using Quivus.Utility.Logger;
using Quivus.Utility.CustomException;
using QuivusUtility.Utilities;
using Quivus.Model.PatientBackground;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Visit.Business.BL.Encounter.Impl;
using Visit.Business.BL.ViewBackground;
using Visit.Business.BL.ViewBackground.Impl;
using Quivus.Model.TypeDTOs;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Quivus.Model.QuickInput.Labs;
using Quivus.Model.QuickInput.ROS;

using Quivus.Model.QuickInput.ComprehensiveHistory.Social;

using Quivus.Model.QuickInput.HPI;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.Physical;
using Quivus.Model.QuickInput.Diagnosis;
using Quivus.Model.QuickInput.MainQuickInput;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.Visit;
using Quivus.Model.Visit;
using Quivus.Model.QuickInput.Prescription;
using Quivus.Model.QuickInput.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Dictation;
using QuivusModel.QuickInput.ComprehensiveHistory.Social;


namespace VisitService.Service
{
    public partial class VisitService 
    {
        //M to remove error
        IViewBackgroundManager ViewBackgroundImpl = null;

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>     
        public List<ModuleNoteDTO> LoadModuleNotesByVisitId(int visitId)
        {
            List<ModuleNoteDTO> ModuleNotesList = null;
            try
            {  
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ModuleNotesList = ViewBackgroundImpl.LoadModuleNotesByVisitId(visitId,false);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ModuleNotesList;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// load patient encounters for patient background
        /// </summary>
        /// <param name="patinetId"></param>
        /// <param name="clinicBranchId"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>      
        public PatientEncounterResult LoadPatientVisitsByPatientIdAndClinicId(int patientId, int clinicBranchId, bool showAll, int UserId, int UserTypeId)
        {
            PatientEncounterResult response = null;
            try
            {
                if (VisitImpl == null)
                {
                    VisitImpl = new VisitManagerImpl();
                }

                response = VisitImpl.LoadPatientVisitsByPatientIdAndClinicId(patientId, clinicBranchId, showAll, UserId,UserTypeId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 21 Feb 2011
        /// get immunization list of visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns>     
        public List<ImmunizationDTO> LoadImmunizationsByVisitId(int visitId)
        {
            List<ImmunizationDTO> ImmunizationList = null;
            try
            {
                //M to remove error
                //if (ViewBackgroundImpl == null)
                //{
                //    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                //}

                //ImmunizationList = ViewBackgroundImpl.LoadImmunizationsByVisitId(visitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ImmunizationList;
        
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to load Patient Medical History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryDTO> LoadPatientMedicalHistoryByVisit(int VisitId)
        {
            List<MedicalHistoryDTO> MedicalHistoryList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                MedicalHistoryList = ViewBackgroundImpl.LoadPatientMedicalHistoryByVisit(VisitId,false); //True Indicates tO Tempate Visit Default is False

                QuivusLogger.Info("LoadPatientMedicalHistoryByVisit Return List ::" + MedicalHistoryList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return MedicalHistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Family History by visit Id
        /// </summary>
        /// <returns></returns>

        public List<FamilyHistoryDTO> LoadPatientFamilyHistoryByVisit(int VisitId)
        {
            List<FamilyHistoryDTO> FamilyHistoryList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                FamilyHistoryList = ViewBackgroundImpl.LoadPatientFamilyHistoryByVisit(VisitId,false);

                QuivusLogger.Info("LoadPatientFamilyHistoryByVisit Return List ::" + FamilyHistoryList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return FamilyHistoryList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to load Patient Vital History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitalByVisit(int VisitId)
        {
            List<VitalDTO> List = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadPatientVitalByVisit(VisitId,false);

                QuivusLogger.Info("LoadPatientVitalByVisit Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return List;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<LabDTO> LoadPatientLabsByVisit(int VisitId)
        {
            List<LabDTO> List = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadPatientLabsByVisit(VisitId,false);

                QuivusLogger.Info("LoadPatientVitalByVisit Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadPatientPreviousVitals(PatientId);

                QuivusLogger.Info("LoadPatientPreviousVitals Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return List;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient Labs History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<ROSDTO> LoadPatientROSByVisit(int VisitId)
        {
            List<ROSDTO> List = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadPatientROSByVisit(VisitId,false);

                QuivusLogger.Info("LoadPatientROSByVisit Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return List;
        }


        /// <summary>
        /// Author Ismail 
        /// Date 21 Mar 2011
        /// This Method Will be used to load Social history Addiction By social History Id
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryAddictionDTO> LoadSocialHistoryAddictionBySocialHistoryID(int PatientSocialHistoryId)
        {
            List<SocialHistoryAddictionDTO> List = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadSocialHistoryAddictionBySocialHistoryID(PatientSocialHistoryId,false);

                QuivusLogger.Info("LoadSocialHistoryAddictionBySocialHistoryID Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return List;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 21 March 2011
        /// This Method Will be used to load Patient HPI by visit Id
        /// </summary>
        /// <returns></returns>        
        public List<HPI_CCDTO> LoadPatientHPIByVisitId(int VisitId)
        {
            List<HPI_CCDTO> List = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadPatientHPIByVisitId(VisitId,false);

                QuivusLogger.Info("LoadPatientHPIByVisit Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return List;
        }


        /// <summary>
        /// Author Ismail 
        /// Date 22 Mar 2011
        /// This Method Will be used to load Social history by visit Id
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryDTO> LoadSocialHistoryByVisitId(int VisitId)
        {
            List<SocialHistoryDTO> List = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                List = ViewBackgroundImpl.LoadPatientSocialHistoryByVisitId(VisitId,false);

                QuivusLogger.Info("LoadSocialHistoryAddictionBySocialHistoryID Return List ::" + List);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<PatientRecommendationDTO> LoadPatientRecommendationByVisitId(int VisitId)
        {
            List<PatientRecommendationDTO> RecommendationList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                RecommendationList = ViewBackgroundImpl.LoadPatientRecommendationByVisitId(VisitId);

                QuivusLogger.Info("LoadPatientRecommendationByVisitId Return List ::" + RecommendationList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return RecommendationList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 16 March 2011
        /// This Method Will be used to load Patient physical History by visit Id
        /// </summary>
        /// <returns></returns>

        public List<PhysicalsDTO> LoadPatientPhysicalsByVisit(int VisitId)
        {
            List<PhysicalsDTO> HistoryList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                HistoryList = ViewBackgroundImpl.LoadPatientPhysicalsByVisit(VisitId,false);

                QuivusLogger.Info("LoadPatientROSByVisit Return HistoryList ::" + HistoryList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
        public List<PatientDiagnosisDTO> LoadPatientDiagnosisByVisitId(int visitId)
        {
            List<PatientDiagnosisDTO> diagnosisList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                diagnosisList = ViewBackgroundImpl.LoadPatientDiagnosisByVisitId(visitId,false);

                QuivusLogger.Info("LoadPatientDiagnosisByVisitId Return HistoryList ::" + diagnosisList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return diagnosisList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Recommendation History by visit Id
        /// </summary>
        /// <returns></returns>
        public List<PatientRecommendationDTO> LoadPatientRecommendationByVisit(int VisitId)
        {
            List<PatientRecommendationDTO> RecommendationList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                RecommendationList = ViewBackgroundImpl.LoadPatientRecommendationByVisit(VisitId,false);

                QuivusLogger.Info("LoadPatientRecommendationByVisit Return RecommendationList ::" + RecommendationList);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return RecommendationList;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 24 March 2011
        /// This Method Will be used to load Patient Recommendation History by visit Id
        /// </summary>
        /// <returns></returns>
        public QuickInputDTO LoadQuickInputByVisit(int VisitId)
        {
            QuickInputDTO quickInput = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                quickInput = ViewBackgroundImpl.LoadQuickInputByVisit(VisitId,false); /* false parameter added by Ismail it is used to check either visit is loaded as templete visit or not */
            //    quickInput = ViewBackgroundImpl.LoadQuickInputByVisit(VisitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return quickInput;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient Follow up by visit Id
        /// </summary>
        /// <returns></returns>        
        public PatientFollowUpDTO LoadFollowUpByVisitId(int visitId)
        {
            PatientFollowUpDTO followUp = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                followUp = ViewBackgroundImpl.LoadFollowUpByVisitId(visitId,false);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return followUp;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 25 March 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public List<VisitDTO> LoadPatientVisitByClinicBranchId(int VisitId, int ClinicBranchId)
        {
            List<VisitDTO> visitList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                visitList = ViewBackgroundImpl.LoadPatientVisitByClinicBranchId(VisitId, ClinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp,exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                visitList = ViewBackgroundImpl.LoadVisitStakeHolderByVisitId(VisitId, UserTypeId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return visitList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient medication prescribed by PatientId by Id
        /// Not in Use Right Now
        /// </summary>
        /// <returns></returns>
        public List<MedicationPrescribedDTO> LoadPatientMedicationPrescribedByPatient(int PatientId)
        {
            List<MedicationPrescribedDTO> DTOList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                DTOList = ViewBackgroundImpl.LoadPatientMedicationPrescribedByPatient(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return DTOList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 29 March 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>
        public List<AllergyDTO> LoadPatientAllergyByPatientId(int PatientId)
        {
            List<AllergyDTO> DTOList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                DTOList = ViewBackgroundImpl.LoadPatientAllergyByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return DTOList;
        }

        /// <summary>
        /// Author Mansoor
        ///Date 1 April 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitals(int PatientId, int ClinicBranchId)
        {
            List<VitalDTO> DTOList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                DTOList = ViewBackgroundImpl.LoadPatientVitals(PatientId, ClinicBranchId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return DTOList;
        }


        /// <summary>
        /// Author Ismail
        /// Date 05 April 2011
        /// This Method Will be used to load visit by visit by Id
        /// </summary>
        /// <returns></returns>
        public VisitDTO LoadVisitDataByVisitId(int VisitId)
        {
            VisitDTO visitObj = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                visitObj = ViewBackgroundImpl.LoadVisitDataByVisitId(VisitId, false); // Mark IsTemplateVisit To True In Case Of Template Visit Only... Default is False
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
            List<PrescriptionDTO> DTOList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                DTOList = ViewBackgroundImpl.LoadPateintPrescriptionByVisitId(VisitId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return DTOList;
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPrescriptionMedicationByPatientPrescriptionId(PatientPrescriptionId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientMedicationByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientDiagnosisByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Atta
        /// Date 06 April 2011
        /// </summary>
        /// <returns></returns>
        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionDTOByVisitId(int VisitId)
        {
            List<PatientFollowupInstructionDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadFollowupInstructionDTOByVisitId(VisitId,false);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// This Method Will be used to load presription medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PrescriptionMedicationDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPrescriptionMedicationByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }


        /// <summary>
        /// Author Atta
        /// Date 19 April 2011
        /// </summary>
        /// <returns></returns>
        public List<FollowupLabsDTO> LoadPatientLabTestsForFollowupByPatient(int PatientId)
        {
            List<FollowupLabsDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientLabTestsForFollowupByPatient(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 27 April 2011
        /// This Method Will be used to load patient in house labs by visit by Id
        /// </summary>
        /// <returns></returns>
        public List<InHouseLabDTO> LoadPateintInHouseLabsByVisitId(int VisitId)
        {
            List<InHouseLabDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPateintInHouseLabsByVisitId(VisitId,false);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPateintInHouseLabsByPatientId(PatientId, ClinicBranchId,stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadMedicalHistoryByPatientId(PatientId,ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadFamilyHistoryByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadVitalsByPatientId(PatientId, ClinicBranchId,  stationIDs,  isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadHPIByPatientId(PatientId,ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadROSByPatientId(PatientId, ClinicBranchId,  stationIDs,  isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPhysicalsByPatientId(PatientId, ClinicBranchId,  stationIDs,  isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientResolvedDiagnosisByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientChronicDiagnosisByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientAcuteDiagnosisByPatientId(PatientId);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientProcedureByPatientId(PatientId, ClinicBranchId, stationIDs,  isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient social history by patient Id
        /// </summary>
        /// <returns></returns>
        public List<SocialHistoryDTO> LoadPatientSocialHistoryByPatientId(int PatientId, int ClinicBranchID, List<int> stationIDs, bool isAllSelected)
        {
            List<SocialHistoryDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientSocialHistoryByPatientId(PatientId, false, ClinicBranchID, stationIDs,  isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 22 may 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public List<SocialhistoryVBDTO> LoadSocialHistoryByPatientId(int PatientId, int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<SocialhistoryVBDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadSocialHistoryByPatientId(PatientId, false, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
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
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientRecommendationByPatientId(PatientId, ClinicBranchId, stationIDs,  isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 10 may 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>

       public List<PatientFollowUpDTO> LoadPatientFollowUpByPatientId(int PatientId,int ClinicBranchId, List<int> stationIDs, bool isAllSelected)
        {
            List<PatientFollowUpDTO> ReturnList = null;
            try
            {
                if (ViewBackgroundImpl == null)
                {
                    ViewBackgroundImpl = new ViewBackgroundManagerImpl();
                }

                ReturnList = ViewBackgroundImpl.LoadPatientFollowUpByPatientId(PatientId, ClinicBranchId, stationIDs, isAllSelected);
            }
            catch (InvalidArgumentException exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp, exp.ErrorDetail);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message);
                QuivusLogger.Error(exp.StackTrace);
                HandleFault.HandleFautException(exp);
            }
            return ReturnList;
        }

       /// <summary>
       /// Author Muhammad Ismail 
       /// Date 10 May 2011
       /// This Method Will be used to load Patient Labs by patrint Id
       /// </summary>
       /// <returns></returns>
       public List<LabDTO> LoadPatientLabsByPatientId(int PatientId, int clinicBranchID, List<int> stationIDs, bool isAllSelected)
       {
           List<LabDTO> ReturnList = null;
           try
           {
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientLabsByPatientId(PatientId, clinicBranchID, stationIDs, isAllSelected);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadModuleDictationByPatientId(PatientId, clinicBranchID,  stationIDs,  isAllSelected);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadModuleNotesByPatientId(PatientId, clinicBranchID, stationIDs,  isAllSelected);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }

       /// <summary>
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadFollowupInstructionByPatientId(PatientId, clinicBranchID,  stationIDs,  isAllSelected);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }


       /// <summary>
       /// Author Sami
       /// Date 8/12/2011
       /// This Method Will be used to load patient medication by patient diagnosis Id for Recommendation
       /// </summary>
       /// <returns></returns>
       public List<PatientDiagnosisDTO> LoadPatientDiagnosisByPatientIdANDForRecommendation(int PatientId, int ClinicBranchId)
       {
           List<PatientDiagnosisDTO> ReturnList = null;
           try
           {
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientDiagnosisByPatientIdANDForRecommendation(PatientId, ClinicBranchId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }



       #region WEB PORTAL

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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadMedicalHistoryAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadFamilyHistoryAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadSocialHistoryAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientImmunizationAllByPatientId(patientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientVitalAll(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadHPIAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadROSAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPhysicalsAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadModuleDictationAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadModuleNotesAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientInHouseLabsAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientLabsAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientProcedureAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPrescriptionMedicationAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
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
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientRecommendationAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }

       /// <summary>
       /// Author Mansoor
       /// Date 22 Sep 2011
       /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
       /// </summary>
       /// <returns></returns>       
       public  List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId)
       {
           List<PatientDiagnosisDTO> ReturnList = null;
           try
           {
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientChronicDiagnosisAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }

      
        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
       /// This Method Will be used to load patient FollowUp by patient Id
       /// </summary>
       /// <returns></returns>

       public List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId)
       {
           List<PatientFollowUpDTO> ReturnList = null;
           try
           {
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadPatientFollowUpAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }


       /// <summary>
       /// Author Mansoor
       /// Date 26 Sep 2011
       /// This Method Will be used to load patient FollowUp by patient Id
       /// </summary>
       /// <returns></returns>

       public List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId)
       {
           List<PatientFollowupInstructionDTO> ReturnList = null;
           try
           {
               if (ViewBackgroundImpl == null)
               {
                   ViewBackgroundImpl = new ViewBackgroundManagerImpl();
               }

               ReturnList = ViewBackgroundImpl.LoadFollowupInstructionAllByPatientId(PatientId);
           }
           catch (InvalidArgumentException exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp, exp.ErrorDetail);
           }
           catch (Exception exp)
           {
               QuivusLogger.Error(exp.Message);
               QuivusLogger.Error(exp.StackTrace);
               HandleFault.HandleFautException(exp);
           }
           return ReturnList;
       }


       #endregion

    }
}