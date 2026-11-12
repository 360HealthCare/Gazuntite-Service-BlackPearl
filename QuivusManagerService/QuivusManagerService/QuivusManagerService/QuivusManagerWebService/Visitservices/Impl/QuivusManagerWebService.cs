using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.VisitBL.ViewBackgorund;
using QuivusManager.Buisness.VisitBL.ViewBackgorund.Impl;
using System.ServiceModel;

namespace QuivusManagerWebService
{
    public partial class QuivusManagerWebService
    {
          IViewBackgroundBL viewBackImpl = null;


        #region WEB PORTAL
        
        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>                
        public List<AllergyDTO> LoadPatientAllergyAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<AllergyDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientAllergyAllByPatientId Called");
                QuivusLogger.Error("LoadPatientAllergyAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadPatientAllergyAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        public List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<MedicalHistoryDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadMedicalHistoryAllByPatientId Called");
                QuivusLogger.Error("LoadMedicalHistoryAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadMedicalHistoryAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        public List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<FamilyHistoryDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadFamilyHistoryAllByPatientId Called");
                QuivusLogger.Error("LoadFamilyHistoryAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadFamilyHistoryAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns> 
        public List<SocialhistoryVBDTO> LoadSocialHistoryAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<SocialhistoryVBDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadSocialHistoryAllByPatientId Called");
                QuivusLogger.Error("LoadSocialHistoryAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadSocialHistoryAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns> 
        public List<MedicationDTO> LoadPatientMedicationAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<MedicationDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientMedicationAllByPatientId Called");
                QuivusLogger.Error("LoadPatientMedicationAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadPatientMedicationAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns> 
        public List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<ImmunizationDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientImmunizationAllByPatientId Called");
                QuivusLogger.Error("LoadPatientImmunizationAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadPatientImmunizationAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        public List<VitalDTO> LoadPatientVitalAll(int PatientId)
        {
            int ClinicBranchId = 0;
            List<VitalDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientVitalAll Called");
                QuivusLogger.Error("LoadPatientVitalAll Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadPatientVitalAll(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        public List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId) 
        {
            int ClinicBranchId = 0;
            List<HPI_CCDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadHPIAllByPatientId Called");
                QuivusLogger.Error("LoadHPIAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadHPIAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        public List<ROSDTO> LoadROSAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<ROSDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadROSAllByPatientId Called");
                QuivusLogger.Error("LoadROSAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadROSAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>    
        public List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PhysicalsDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPhysicalsAllByPatientId Called");
                QuivusLogger.Error("LoadPhysicalsAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadPhysicalsAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }




        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>    
        public List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<ModuleDictationDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadModuleDictationAllByPatientId Called");
                QuivusLogger.Error("LoadModuleDictationAllByPatientId Param PatientId::" + PatientId);

               if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                response = viewBackImpl.LoadModuleDictationAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }  
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>
        public List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<ModuleNoteDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadModuleNotesAllByPatientId Called");
                QuivusLogger.Error("LoadModuleNotesAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadModuleNotesAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<InHouseLabDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientInHouseLabsAllByPatientId Called");
                QuivusLogger.Error("LoadPatientInHouseLabsAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientInHouseLabsAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
 

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>       
        public List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<LabDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientLabsAllByPatientId Called");
                QuivusLogger.Error("LoadPatientLabsAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientLabsAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PatientDiagnosisDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientResolvedDiagnosisAllByPatientId Called");
                QuivusLogger.Error("LoadPatientResolvedDiagnosisAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientResolvedDiagnosisAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PatientDiagnosisDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientChronicDiagnosisAllByPatientId Called");
                QuivusLogger.Error("LoadPatientChronicDiagnosisAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientChronicDiagnosisAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PatientDiagnosisDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientAcuteDiagnosisAllByPatientId Called");
                QuivusLogger.Error("LoadPatientAcuteDiagnosisAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientAcuteDiagnosisAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient procedure by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<ProcedureDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientProcedureAllByPatientId Called");
                QuivusLogger.Error("LoadPatientProcedureAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientProcedureAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>       
        public List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PrescriptionMedicationDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPrescriptionMedicationAllByPatientId Called");
                QuivusLogger.Error("LoadPrescriptionMedicationAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPrescriptionMedicationAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PatientRecommendationDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientRecommendationAllByPatientId Called");
                QuivusLogger.Error("LoadPatientRecommendationAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientRecommendationAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }



        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PatientFollowUpDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadPatientFollowUpAllByPatientId Called");
                QuivusLogger.Error("LoadPatientFollowUpAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadPatientFollowUpAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>       
        public List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId)
        {
            int ClinicBranchId = 0;
            List<PatientFollowupInstructionDTO> response = null;
            try
            {
                QuivusLogger.Error("In QuivusManagerService LoadFollowupInstructionAllByPatientId Called");
                QuivusLogger.Error("LoadFollowupInstructionAllByPatientId Param PatientId::" + PatientId);

                if (viewBackImpl == null)
                {
                    viewBackImpl = new ViewBackgroundBLImpl();
                }
                response = viewBackImpl.LoadFollowupInstructionAllByPatientId(PatientId);
            }
            //catch (InvalidSessionException exp)
            //{
            //    QuivusLogger.Error(exp);
            //    if (response == null)
            //        response = new ServiceResponse();

            //    ResponseErrorHandler.HandleException(response, exp, exp.Code);
            //}
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //if (response == null)
                //    response = new EncounterTypeResponse();

                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }


        #endregion
    }
}