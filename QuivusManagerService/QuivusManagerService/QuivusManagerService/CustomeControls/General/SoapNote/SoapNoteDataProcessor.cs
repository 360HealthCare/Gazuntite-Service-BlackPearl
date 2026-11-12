using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.Buisness.StaticDataBL;
using QuivusManager.Buisness.StaticDataBL.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.Buisness.Utility;
using QuivusManager.IntegratedServies.UserManagerService;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuivusManagerService.CustomeControls.General.SoapNote
{
    public class SoapNoteDataProcessor
    {
        /// <summary>
        /// TODO: the following region should be moved to constant classs of the project
        ///  item assigned to sami 
        ///  Code review Done by Baber Waqas on 1/20/2012
        /// </summary>
        #region Constant region need to be moved to constant files
        public static string YES_HTML = "Yes";
        public static string NO_HTML = "No";
        public static string All_NORMAL_HTML = ": All Are Normal";
        public static string NEW_LINE_HTML = "<br /><hr />";
        #endregion

        #region code convention adjusted by baber waqas 1/20/2012
        private static FamilyHistoryTypeResponse _familyHistoryList;
        private static SocialHistoryAddictionTypeResponse _socialHistoryList;
        private static SocialHistoryMediumTypeResponse _socialHistoryMediumTypeList;
        private static SocialHistoryUsageAmountTypeResponse _socialHistoryUsageAmountList;
        private static MedicalHistoryTypeResponse _medicalHistoryList;
        private static IStaticDataBL _staticData;
        private static List<RelationshipTypeDTO> _relationShipList;
        private static List<SocialMaritalStatusTypeDTO> _MartalStatusList;
        #endregion
        //private static DiseaseTypeResponse diseaseList;

        public static List<QuivusManager.IntegratedServies.VisitService.ModuleTypeDTO> ModuleTypeList { get; set; }

        //TODO:  Remove these lists from static data to save thread and correction of architecture 
        // Item assigned to sami sahibzad 
        // code review done by Baber Waqas on 1/20/2012
        #region unstable code  need to be fixed
        public static List<AllergyDTO> AllergyList { get; set; }
        public static List<MedicationDTO> MedicationList { get; set; }
        public static List<PrescriptionMedicationDTO> PreMed { get; set; }
        public static List<ModuleNoteDTO> ModuleList { get; set; }
        public static List<ROSDTO> RosDataList { get; set; }
        public static List<PhysicalsDTO> PhyDataList { get; set; }
        #endregion

        public static void getStaticData()
        {
            if (_staticData == null)
            {
                _staticData = new StaticDataBLImpl();
            }
            if (_familyHistoryList == null)
            {
                _familyHistoryList = _staticData.LoadFamilyHistoryTypes();
            }
            if (_socialHistoryList == null)
            {
                _socialHistoryList = _staticData.LoadSocialHistoryAddictionTypes();
            }
            if (_socialHistoryMediumTypeList == null)
            {
                _socialHistoryMediumTypeList = _staticData.LoadSocialHistoryMediumTypes();
            }
            if (_socialHistoryUsageAmountList == null)
            {
                _socialHistoryUsageAmountList = _staticData.LoadSocialHistoryUsageAmountTypes();
            }
            if (_medicalHistoryList == null)
            {
                _medicalHistoryList = _staticData.LoadMedicalHistoryTypes();
            }
            if (_relationShipList == null)
            {
                _relationShipList = _staticData.GetRegisterationStaticData().ResponseData.RelationshipTypes;
            }
            if (_MartalStatusList == null)
            {
                _MartalStatusList = _staticData.LoadMaritalStatusTypes().SocialHistoryMaritalList;
            }
        }

        public static ModuleNoteDTO getModuleNote(string name)
        {
            ModuleNoteDTO moduleNote = null;
            if (ModuleList != null)
            {
                foreach (QuivusManager.IntegratedServies.VisitService.ModuleTypeDTO moduleType in ModuleTypeList)
                {
                    if (moduleType.ModuleName == name)
                    {
                        foreach (ModuleNoteDTO note in ModuleList)
                        {
                            if (note.ModuleTypeId == moduleType.ModuleTypeId)
                            {
                                moduleNote = note;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return moduleNote;
        }

        public static List<List<SoapNoteCatagoryDTO>> ProcessCatagories(QuickInputDTO qiDto, int clinicBranchId)
        {
            var list = new List<List<SoapNoteCatagoryDTO>>();
            if (qiDto != null)
            {
                getStaticData();
                if (qiDto.PMH != null)
                    list.Add(ProcessHistory(qiDto.PMH, clinicBranchId,qiDto.Visit.VisitId));

                if (qiDto.Physical != null)
                {
                    list.Add(ProcessSubjective(qiDto.Physical));
                    list.Add(ProcessObjective(qiDto.Physical));
                }

                if (qiDto.Assessment != null)
                    list.Add(ProcessAssessment(qiDto.Assessment, qiDto.VisitId));

                if (qiDto.Physical != null && qiDto.Assessment != null)
                    list.Add(ProcessPlan(qiDto.Physical, qiDto.Assessment, null));

            }
            return list;
        }

        /*
         * 
         * History Section Processing
         * 
         * */
        public static List<SoapNoteCatagoryDTO> ProcessHistory(PMHDTO pmhDto, int clinicBranchId, int qiVisitId)
        {
            var history = new List<SoapNoteCatagoryDTO>();

            var moduleNote = getModuleNote(ModuleNameContants.COMPREHENSIVE_HISTORY_MODULE_NAME);
            if (pmhDto.ComprehensiveHistory != null && moduleNote != null)
                history.Add(ProcessComprehensiveHistory(moduleNote));
            // <code>missing null check 
            // code review done and adjusted by Baber Waqas 
            // Code review done on 1/20/2012</code>
            if (pmhDto.ComprehensiveHistory != null)
            {
                if (pmhDto.ComprehensiveHistory.MedicalHistory != null &&
                    pmhDto.ComprehensiveHistory.MedicalHistory.Count > 0)
                    history.Add(ProcessMedicalHistory(pmhDto.ComprehensiveHistory.MedicalHistory,qiVisitId));


                if (pmhDto.ComprehensiveHistory.FamilyHistory != null && pmhDto.ComprehensiveHistory.FamilyHistory.Count > 0)
                    history.Add(ProcessFamilyHistory(pmhDto.ComprehensiveHistory.FamilyHistory, clinicBranchId, qiVisitId));

                if (pmhDto.ComprehensiveHistory.SocialHistory != null && pmhDto.ComprehensiveHistory.SocialHistory.Count > 0)
                    history.Add(ProcessSocialHistory(pmhDto.ComprehensiveHistory.SocialHistory, qiVisitId));
            }

            moduleNote = getModuleNote(ModuleNameContants.ALLERGY_MODULE_NAME);
            if (AllergyList != null && AllergyList.Count > 0 || moduleNote != null)
                history.Add(ProcessAllergies());

            moduleNote = getModuleNote(ModuleNameContants.IMMUNIZATION_MODULE_NAME);
            if (pmhDto.Immunization != null && pmhDto.Immunization.Count > 0 || moduleNote != null)
                history.Add(ProcessImmunization(pmhDto.Immunization, qiVisitId));

            moduleNote = getModuleNote(ModuleNameContants.CHRONIC_MEDICATION_MODULE_NAME);
            if (MedicationList != null && MedicationList.Count > 0 || moduleNote != null)
                history.Add(ProcessChronicMedication());

            return history;
        }

        /*
         * 
         * Subjective Section Processing
         * 
         * */
        public static List<SoapNoteCatagoryDTO> ProcessSubjective(PhysicalDTO physical)
        {
            List<SoapNoteCatagoryDTO> subjective = new List<SoapNoteCatagoryDTO>();
            ModuleNoteDTO moduleNote;

            moduleNote = getModuleNote(ModuleNameContants.CHIEF_COMPLAINT_MODULE_NAME);
            if (physical.HPI_CC != null && physical.HPI_CC.Count > 0 || moduleNote != null)
                subjective.Add(ProcessHPI_CC(physical.HPI_CC));

            moduleNote = getModuleNote(ModuleNameContants.REVIEW_OF_SYSTEM_MODULE_NAME);
            if (physical.ReviewOfSystem != null && physical.ReviewOfSystem.Count > 0 || moduleNote != null)
                subjective.Add(ProcessROS(physical.ReviewOfSystem));
            return subjective;
        }

        /*
        * 
        * Objective Section Processing
        * 
        * */
        public static List<SoapNoteCatagoryDTO> ProcessObjective(PhysicalDTO physicalDto)
        {
            var objective = new List<SoapNoteCatagoryDTO>();
            var moduleNote = getModuleNote(ModuleNameContants.PATIENT_VITAL_MODULE_NAME);

            if ((physicalDto.Vitals != null && ContainsVitalsData(physicalDto.Vitals)) || moduleNote != null)
                objective.Add(ProcessVital(physicalDto.Vitals));

            moduleNote = getModuleNote(ModuleNameContants.PHYSICAL_MODULE_NAME);
            if ((physicalDto.Physicals != null && physicalDto.Physicals.Count > 0) || moduleNote != null)
                objective.Add(ProcessPhysicals(physicalDto.Physicals));

            moduleNote = getModuleNote(ModuleNameContants.LAB_IN_MODULE_NAME);
            if (physicalDto.InHouseLabs != null && physicalDto.InHouseLabs.Count > 0 || moduleNote != null)
                objective.Add(ProcessLabsIn(physicalDto.InHouseLabs));

            return objective;
        }

        /*
        * 
        * Assessment Section Processing
        * 
        * */
        public static List<SoapNoteCatagoryDTO> ProcessAssessment(AssessmentDTO assessmentDto, int qiVisitId)
        {
            var assessment = new List<SoapNoteCatagoryDTO>();
            if (assessmentDto.Diagnosis != null && assessmentDto.Diagnosis.Count > 0)
                assessment.Add(ProcessDiagnosis(assessmentDto.Diagnosis, qiVisitId));

            return assessment;
        }

        /*
        * 
        * Plan Section Processing
        * 
        * */
        public static List<SoapNoteCatagoryDTO> ProcessPlan(PhysicalDTO physicalDto, AssessmentDTO assessmentDto, List<PrescriptionMedicationDTO> PreMedList)
        {
            var assessment = new List<SoapNoteCatagoryDTO>();
            ModuleNoteDTO moduleNote;

            moduleNote = getModuleNote(ModuleNameContants.TREATMENT_MODULE_NAME);
            if (moduleNote != null)
                assessment.Add(ProcessTreatmentModuleLevelNote(moduleNote));
            moduleNote = getModuleNote(ModuleNameContants.PLAN_MODULE_NAME);

            if (moduleNote != null)
                assessment.Add(ProcessPlanModuleLevelNote(moduleNote));

            if (physicalDto.Labs != null && physicalDto.Labs.Count > 0)
                assessment.Add(ProcessLabsOut(physicalDto.Labs));

            if (assessmentDto.Procedure != null && assessmentDto.Procedure.Count > 0)
                assessment.Add(ProcessProcedure(assessmentDto.Procedure));

            moduleNote = getModuleNote(ModuleNameContants.FOLLOWUP_MODULE_NAME);
            if (assessmentDto.PatientFollowUp != null || moduleNote != null)
                //    assessment.Add(ProcessFollowUp(assessmentDto.PatientFollowUp));
                //if (assessmentDto.PatientFollowupInstruction != null && assessmentDto.PatientFollowupInstruction.Count > 0)
                assessment.Add(ProcessFollowInstrucation(assessmentDto.PatientFollowupInstruction, ProcessFollowUp(assessmentDto.PatientFollowUp)));

            if (assessmentDto.Recommendation != null && assessmentDto.Recommendation.Count > 0)
                assessment.Add(ProcessRecommendation(assessmentDto.Recommendation));

            if (PreMedList != null && PreMedList.Count > 0)
                assessment.Add(ProcessPresciption(PreMedList));

            return assessment;
        }

        private static SoapNoteCatagoryDTO ProcessAllergies()
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Allergies";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.ALLERGY_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";

            if (AllergyList != null)
            {
                int i;
                for (i = 0; i < AllergyList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    AllergyDTO allergy = AllergyList[i];
                    if (!allergy.AllergyName.Contains("NKDA"))
                    {
                        dto.Notes += allergy.Note;
                        if (allergy.VisitDate != null)
                            dto.Particulars += "<b>Visit Date </b>: " + allergy.VisitDate.ToShortDateString() + ", ";
                        if (!string.IsNullOrEmpty(allergy.OnSet))
                            dto.Particulars += "<b>OnSet</b>: " + allergy.OnSet + ", ";
                        if (!string.IsNullOrEmpty(allergy.AllergyName))
                            dto.Particulars += "<b>Allergy Name</b>: " + allergy.AllergyName + ", ";
                        if (!string.IsNullOrEmpty(allergy.Reaction))
                            dto.Particulars += "<b>Reaction</b>: " + allergy.Reaction + ", ";
                        if (allergy.IsActive == "true")
                            dto.Particulars += "<b>Status</b>: Active";
                        if (allergy.IsActive == "false")
                            dto.Particulars += "<b>Status</b>: InActive";
                    }
                    else if (allergy.AllergyName.Contains("NKDA"))
                    {
                        dto.Particulars += "<b>No Known Drug Allergies</b>";
                    }
                }
            }

            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessComprehensiveHistory(ModuleNoteDTO moduleNote)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Comprehensive History";
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";

            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessMedicalHistory(List<MedicalHistoryDTO> medicalList, int qiVisitId)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Medical History";
            if (medicalList != null)
            {
                int i;
                for (i = 0; i < medicalList.Count; i++)
                {
                    foreach (MedicalHistoryTypeDTO medicalType in _medicalHistoryList.MedicalHistoryList)
                    {
                        MedicalHistoryDTO med = medicalList[i];
                        if (med.VisitID != 0 && med.VisitID == qiVisitId)
                        {
                            if (med.MedicalHistoryTypeID == medicalType.MedicalHistoryTypeID)
                            {

                                dto.Particulars += "<br /><b>" + medicalType.MedicalHistoryTypeName + "</b>";
                                if (med.IsNone == "true")
                                {
                                    dto.Particulars += ":" + "None";
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(med.MedicalHistoryNote))
                                        dto.Notes += "<b>" + medicalType.MedicalHistoryTypeName + ": </b> " + med.MedicalHistoryNote + "<br />";
                                    dto.Particulars += ":" + YES_HTML;
                                }
                            }
                        }
                    }
                }
            }

            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessFamilyHistory(List<FamilyHistoryDTO> FamilyList, int clinicBranchId, int qiVisitid)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Family History";
            //dto.Particulars += "Family History: ";
            if (FamilyList != null)
            {
                int i;
                for (i = 0; i < FamilyList.Count; i++)
                {
                    FamilyHistoryDTO fam = FamilyList[i];
                    FamilyHistoryTypeDTO familyhistoryType = null;
                    DiseaseTypeResponse diseaseList = null;
                    if (fam.VisitID != 0 && fam.VisitID == qiVisitid)
                    {
                        foreach (FamilyHistoryTypeDTO familyStaticData in _familyHistoryList.FamilyHistoryList)
                        {
                            diseaseList = null;
                            familyhistoryType = familyStaticData;
                            if (familyStaticData.FamilyHistoryTypeID == fam.FamilyHistoryTypeID)
                            {
                                if (!string.IsNullOrEmpty(fam.IsYesSelected))
                                {
                                    if (fam.IsYesSelected == "false")
                                    {
                                        dto.Particulars += "<br /><b>" + familyhistoryType.FamilyHistoryTypeName + "</b>:" + NO_HTML;
                                    }
                                    if (fam.IsYesSelected == "true")
                                    {
                                        dto.Particulars += "<br /><b>" + familyhistoryType.FamilyHistoryTypeName + "</b>:" + " Yes";
                                    }
                                    if (!string.IsNullOrEmpty(fam.FamilyHistoryNote))
                                        dto.Notes += "<b>" + familyhistoryType.FamilyHistoryTypeName + "</b>: " + fam.FamilyHistoryNote + "<br />";

                                    if (diseaseList == null)
                                    {
                                        diseaseList = _staticData.LoadDiseaseTypes(clinicBranchId, familyStaticData.FamilyHistoryTypeID);
                                    }

                                    foreach (RelationshipTypeDTO relationShip in _relationShipList)
                                    {
                                        if (relationShip.RelationshipId == fam.ReletionshipTypeID)
                                        {

                                            if (!string.IsNullOrEmpty(relationShip.RelationshipName))
                                                dto.Particulars += "<b>, Relation</b>: " + relationShip.RelationshipName;

                                        }
                                    }
                                    if (_staticData == null)
                                    {
                                        _staticData = new StaticDataBLImpl();
                                    }

                                    if (diseaseList != null)
                                    {
                                        foreach (DiseaseTypeDTO disease in diseaseList.DiseaseList)
                                        {
                                            if (fam.IsYesSelected == "true" && !string.IsNullOrEmpty(fam.DiseaseName))
                                            {
                                                if (!string.IsNullOrEmpty(disease.Description))
                                                    dto.Particulars += ", <b>Disease</b>: " + disease.Description;
                                            }

                                            break;
                                        }
                                    }
                                }
                                break;//found type,
                            }
                        }
                    }
                    dto.Particulars += "</br>";
                }
            }

            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessSocialHistory(List<SocialHistoryDTO> SocialList, int qiVisitId)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Social History";
            //dto.Particulars += "Social : ";
            if (SocialList != null)
            {
                int i;
                for (i = 0; i < SocialList.Count; i++)
                {
                    SocialHistoryDTO soc = SocialList[i];
                    if (soc.VisitID != 0 && soc.VisitID == qiVisitId)
                    {
                        #region AddicationList Parser
                        foreach (SocialHistoryAddictionDTO socailHistory in soc.SocialHistoryAddictionList)
                        {
                            foreach (SocialHistoryAddictionTypeDTO socailHistoryType in _socialHistoryList.SocialHistoryAddictionList)
                            {
                                if (socailHistory.SocialHistoryAddictionTypeID == socailHistoryType.SocialHistoryAddictionTypeID)
                                {
                                    if (!string.IsNullOrEmpty(socailHistory.AddictionNotes))
                                        dto.Notes += "<b>" + socailHistoryType.AddictionName + ":</b>" + socailHistory.AddictionNotes + "<br />";
                                    if (socailHistory.IsYesSelected == "false" && !socailHistory.IsUnknown)
                                    {
                                        dto.Particulars += "<b>" + socailHistoryType.AddictionName + "</b>:" + NO_HTML;
                                    }
                                    if (socailHistory.IsUnknown)
                                    {
                                        dto.Particulars += "<b>" + socailHistoryType.AddictionName + "</b>: Unknown";
                                    }
                                    if (socailHistory.IsYesSelected == "true")
                                    {
                                        dto.Particulars += "<b>" + socailHistoryType.AddictionName + "</b>: Yes";
                                    }
                                    if (socailHistory.IsPresentSelected == "true")
                                        dto.Particulars += ", <b>Current</b>: Present";
                                    else if (socailHistory.IsPresentSelected == "false")
                                        dto.Particulars += ", <b>Current</b>: Past";
                                    if (socailHistory.IsEducated == "true")
                                        dto.Particulars += ", <b>Educated</b>: Yes";
                                    else if (socailHistory.IsEducated == "false")
                                        dto.Particulars += ", <b>Educated</b>: No";

                                    if (socailHistory.SocialHistoryAddictionTypeID == socailHistoryType.SocialHistoryAddictionTypeID)
                                    {
                                        foreach (SocialHistoryMediumTypeDTO MediumType in _socialHistoryMediumTypeList.ResponseList)
                                        {

                                            if (socailHistory.MediumTypeID == MediumType.SocialHistoryMediumID)
                                            {
                                                if (!string.IsNullOrEmpty(MediumType.MediumName))
                                                    dto.Particulars += ", <b>Type</b>: " + MediumType.MediumName;
                                            }
                                        }
                                    }
                                    foreach (SocialHistoryUsageAmountTypeDTO AmountType in _socialHistoryUsageAmountList.ResponseList)
                                    {
                                        if (AmountType.SocialHistoryUsageAmountID == socailHistory.UsageAmountID)
                                        {
                                            //if (socailHistory.IsYesSelected == "true")
                                            {
                                                if (!string.IsNullOrEmpty(AmountType.UsageAmountName))
                                                    dto.Particulars += ", <b>Amount</b>: " + AmountType.UsageAmountName;

                                            }
                                            //else
                                            //{
                                            //    //dto.Particulars += ", <b>EndDate: </b>" + socailHistory.EndDate + ",";
                                            //    //dto.Particulars += "<br>";
                                            //    //dto.Particulars += "<b>" + socailHistoryType.AddictionName + ":" + NO_HTML + "</b>";
                                            //}
                                            break;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(socailHistory.OnSet))
                                        dto.Particulars += ", <b>Onset: </b>: " + socailHistory.OnSet;
                                    if (socailHistory.IsPresentSelected == "false")
                                    {
                                        dto.Particulars += ", <b>End Date: </b>" + socailHistory.EndDate;
                                    }
                                    dto.Particulars += "</br></br>";
                                }
                            }
                        }
                        #endregion

                        if (soc.IsWorkStatusYesSelected == "true" || soc.IsWorkStatusYesSelected == "false")
                        {
                            if (soc.IsWorkStatusYesSelected == "false")
                                dto.Particulars += "<b>" + ModuleNameContants.WORK_STATUS_TYPE_NAME + "</b>:" + NO_HTML + "<br /><br />";
                            else if (soc.IsWorkStatusYesSelected == "true")
                            {
                                dto.Particulars += "<b>" + ModuleNameContants.WORK_STATUS_TYPE_NAME + "</b>:" + YES_HTML + "<br /><br />";
                                if (!string.IsNullOrEmpty(soc.WorkStatusNotes))
                                    dto.Notes += "<b>" + ModuleNameContants.WORK_STATUS_TYPE_NAME + ":</b>" + soc.WorkStatusNotes + "<br />";
                            }
                        }

                        if (soc.IsOtherYesSelected == "true" || soc.IsOtherYesSelected == "false")
                        {
                            if (soc.IsOtherYesSelected == "true")
                            {
                                dto.Particulars += "<b>" + ModuleNameContants.OTHER_TYPE_NAME + "</b>:" + YES_HTML + "<br /><br />";
                                if (!string.IsNullOrEmpty(soc.OtherNotes))
                                    dto.Notes += "<b>" + ModuleNameContants.OTHER_TYPE_NAME + ":</b>" + soc.OtherNotes + "<br />";
                            }
                            else
                            {
                                dto.Particulars += "<b>" + ModuleNameContants.OTHER_TYPE_NAME + "</b>:" + NO_HTML + "<br /><br />";
                            }
                        }

                        if (soc.MaritalStatusID > 0)
                        {
                            foreach (SocialMaritalStatusTypeDTO maritalStatus in _MartalStatusList)
                            {
                                if (soc.MaritalStatusID == maritalStatus.MaritalStatusID)
                                {
                                    dto.Particulars += "<b>" + ModuleNameContants.MARITAL_TYPE_NAME + "</b>:" + maritalStatus.MaritalStatusName + "<br />";
                                }
                            }
                        }
                    }
                }
            }
            return dto;
        }

        public static string getModuleNameById(int id)
        {
            string ModuleName = "";

            return ModuleName;
        }

        private static SoapNoteCatagoryDTO ProcessChronicMedication()
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Chronic Medication";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.CHRONIC_MEDICATION_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";
            //dto.Notes += "<b>Subject: </b>"++"<br><b>Note: </b>";
            if (MedicationList != null)
            {
                int i;
                for (i = 0; i < MedicationList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    MedicationDTO med = MedicationList[i];
                    //dto.Notes += "Notes: " + med.n + "<br />";
                    //dto.Particulars += med.DrugName + "," + med.VisitDate + "," + med.Onset + "," + med.MedicationStrength + "," + med.Dose + "," + med.Status + "<br />";
                    //if (med.VisitDate != null)
                    //    dto.Particulars += "<b>Visit Date </b>: " + med.VisitDate.ToShortDateString() + ", ";
                    if (med.CreatedAt != null)
                        dto.Particulars += "<b>OnSet</b>: " + med.CreatedAt.ToShortDateString() + ", ";
                    dto.Particulars += "<b>Med,Strength</b>: " + med.BrandName + "(" + med.GenericName + ")";
                    if (!string.IsNullOrEmpty(med.Strength))
                        dto.Particulars += med.Strength + ", ";
                    if (!string.IsNullOrEmpty(med.Dose))
                        dto.Particulars += "<b>Dose</b>: " + med.Dose + ", ";
                    if (med.IsActive == "true")
                        dto.Particulars += "<b>Status</b>: Active";
                    if (med.IsActive == "false")
                        dto.Particulars += "<b>Status</b>: InActive";
                }
            }

            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessImmunization(List<ImmunizationDTO> ImmunizationList, int qiVisitId)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Immunization";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.IMMUNIZATION_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";


            if (ImmunizationList != null)
            {
                int i;
                for (i = 0; i < ImmunizationList.Count; i++)
                {
                    ImmunizationDTO imm = ImmunizationList[i];
                    if (imm.VisitId != 0 && imm.VisitId == qiVisitId)
                    {
                        if (i > 0)
                        {
                            dto.Particulars += NEW_LINE_HTML;
                        }
                        if (!string.IsNullOrEmpty(imm.Note))
                            dto.Notes += "<b>" + imm.ImmunizationType.ImmunizationTypeName + "</b>: " + imm.Note + "<br />";
                        dto.Particulars += "<b>Immunization</b>: " + imm.ImmunizationType.ImmunizationTypeName + ", <b>Dose</b>: " + imm.Dose;
                        if (!string.IsNullOrEmpty(imm.LotNumber))
                            dto.Particulars += ", <b>Lot #</b> " + imm.LotNumber;
                        if (!string.IsNullOrEmpty(imm.DateAdmin))
                            dto.Particulars += ", <b>Date Admin</b>: " + imm.DateAdmin;
                        if (!string.IsNullOrEmpty(imm.NextDoseDue))
                            dto.Particulars += ", <b>Next Due</b>: " + imm.NextDoseDue;
                    }
                }
            }

            return dto;
        }


        /*
         * 
         * Physical Section Processing
         * 
         * */
        public static List<SoapNoteCatagoryDTO> ProcessPhysical(PhysicalDTO physicalDto)
        {
            List<SoapNoteCatagoryDTO> physical = new List<SoapNoteCatagoryDTO>();

            if (physicalDto.HPI_CC != null)
                physical.Add(ProcessHPI_CC(physicalDto.HPI_CC));
            if (physicalDto.ReviewOfSystem != null)
                physical.Add(ProcessROS(physicalDto.ReviewOfSystem));



            return physical;
        }



        private static SoapNoteCatagoryDTO ProcessVital(VitalDTO vitalDto)
        {

            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Vitals";

            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.PATIENT_VITAL_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Note: </b>" + moduleNote.Note + "<br>";
            if (vitalDto != null)
            {
                if (!string.IsNullOrEmpty(vitalDto.VitalNote))
                    dto.Notes += "Notes: " + vitalDto.VitalNote + "<br />";

                if (!string.IsNullOrEmpty(vitalDto.Height))
                {
                    if (vitalDto.Height.Contains('.'))
                    {
                        double height = float.Parse(vitalDto.Height);
                        height = Math.Round(height, 1);
                        vitalDto.Height = height.ToString();
                    }
                    dto.Particulars += "<b>Ht</b>: " + vitalDto.Height;
                }
                if (!string.IsNullOrEmpty(vitalDto.Weight))
                {
                    if (vitalDto.Weight.Contains('.'))
                    {
                        double weight = float.Parse(vitalDto.Weight);
                        weight = Math.Round(weight, 1);
                        vitalDto.Weight = weight.ToString();
                    }
                    dto.Particulars += ", <b>Wt</b>: " + vitalDto.Weight;
                }
                if (!string.IsNullOrEmpty(vitalDto.Temperature))
                {
                    if (vitalDto.Temperature.Contains('.'))
                    {
                        double temperature = float.Parse(vitalDto.Temperature);
                        temperature = Math.Round(temperature, 1);
                        vitalDto.Temperature = temperature.ToString();
                    }
                    dto.Particulars += ", <b>Temp</b>: " + vitalDto.Temperature;
                }
                if (!string.IsNullOrEmpty(vitalDto.Pulse))
                {
                    if (vitalDto.Pulse.Contains('.'))
                    {
                        double pulse = float.Parse(vitalDto.Pulse);
                        pulse = Math.Round(pulse, 1);
                        vitalDto.Pulse = pulse.ToString();
                    }
                    dto.Particulars += ", <b>P</b>: " + vitalDto.Pulse;
                }
                if (!string.IsNullOrEmpty(vitalDto.Respiration))
                {
                    if (vitalDto.Respiration.Contains('.'))
                    {
                        double respiration = float.Parse(vitalDto.Respiration);
                        respiration = Math.Round(respiration, 1);
                        vitalDto.Respiration = respiration.ToString();
                    }
                    dto.Particulars += ", <b>R</b>: " + vitalDto.Respiration;
                }
                if (!string.IsNullOrEmpty(vitalDto.Oxygen))
                {
                    if (vitalDto.Oxygen.Contains('.'))
                    {
                        double oxygen = float.Parse(vitalDto.Oxygen);
                        oxygen = Math.Round(oxygen, 1);
                        vitalDto.Oxygen = oxygen.ToString();
                    }
                    dto.Particulars += ", <b>PO2</b>: " + vitalDto.Oxygen;
                }
                if (!string.IsNullOrEmpty(vitalDto.BPSys) && !string.IsNullOrEmpty(vitalDto.BPDys))
                {
                    if (vitalDto.BPSys.Contains('.'))
                    {
                        double BP = float.Parse(vitalDto.BPSys);
                        BP = Math.Round(BP, 1);
                        vitalDto.BPSys = BP.ToString();
                    }
                    if (vitalDto.BPDys.Contains('.'))
                    {
                        double BP = float.Parse(vitalDto.BPDys);
                        BP = Math.Round(BP, 1);
                        vitalDto.BPDys = BP.ToString();
                    }
                    dto.Particulars += ", <b>BP</b>: " + vitalDto.BPSys;
                    dto.Particulars += " / " + vitalDto.BPDys + "<br />";
                }
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessHPI_CC(List<HPI_CCDTO> HPI_CCList)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "CC/HPI";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.CHIEF_COMPLAINT_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note: </b>" + moduleNote.Note + "<br>";

            if (HPI_CCList != null)
            {
                int i;
                for (i = 0; i < HPI_CCList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    HPI_CCDTO hpi_cc = HPI_CCList[i];
                    if (!string.IsNullOrEmpty(hpi_cc.Note) && !string.IsNullOrEmpty(hpi_cc.ChiefComplaint))
                        dto.Notes += hpi_cc.ChiefComplaint + ": " + hpi_cc.Note + "<br />";
                    if (!string.IsNullOrEmpty(hpi_cc.Note) && !string.IsNullOrEmpty(hpi_cc.HPIFields.Symptom))
                        dto.Notes += hpi_cc.HPIFields.Symptom + ": " + hpi_cc.Note + "<br />";
                    if (!string.IsNullOrEmpty(hpi_cc.ChiefComplaint) && hpi_cc.ChiefComplaint != "N/A")
                        dto.Particulars += "<b>CHIEF COMPLAINT</b>: " + hpi_cc.ChiefComplaint;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Symptom) && hpi_cc.HPIFields.Symptom != "N/A")
                        dto.Particulars += "<br /> <b>Symptom</b>: " + hpi_cc.HPIFields.Symptom;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Location) && hpi_cc.HPIFields.Location != "N/A")
                        dto.Particulars += "<br /> <b>Location</b>: " + hpi_cc.HPIFields.Location;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Severity) && hpi_cc.HPIFields.Severity != "N/A")
                        dto.Particulars += "<br /> <b>Severity</b>:" + hpi_cc.HPIFields.Severity;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Onset))
                        dto.Particulars += "<br /> <b>Onset</b>:" + hpi_cc.HPIFields.Onset;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Duration) && hpi_cc.HPIFields.Duration != "N/A")
                        dto.Particulars += "<br /> <b>Duration</b>:" + hpi_cc.HPIFields.Duration;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Progression) && hpi_cc.HPIFields.Progression != "N/A")
                        dto.Particulars += "<br /> <b>Progression</b>:" + hpi_cc.HPIFields.Progression;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.ModifyingFactors) && hpi_cc.HPIFields.ModifyingFactors != "N/A")
                        dto.Particulars += "<br /> <b>Modifying Factors</b>:" + hpi_cc.HPIFields.ModifyingFactors;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Quality) && hpi_cc.HPIFields.Quality != "N/A")
                        dto.Particulars += "<br /> <b>Quality</b>:" + hpi_cc.HPIFields.Quality;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Context) && hpi_cc.HPIFields.Context != "N/A")
                        dto.Particulars += "<br /> <b>Context</b>:" + hpi_cc.HPIFields.Context;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Exacerbating) && hpi_cc.HPIFields.Exacerbating != "N/A")
                        dto.Particulars += "<br /> <b>Exacerbating</b>:" + hpi_cc.HPIFields.Exacerbating;
                    if (!string.IsNullOrEmpty(hpi_cc.HPIFields.Radiation) && hpi_cc.HPIFields.Radiation != "N/A")
                        dto.Particulars += "<br /> <b>Radiation</b>:" + hpi_cc.HPIFields.Radiation;

                    //dto.Particulars += "<br/>";

                }
            }

            return dto;

        }


        /// <summary>
        /// prepare ROS soap not list, the logic of this method is to retrieve original static list from db and then check each item in patient physical list
        /// to show it is normal or abnormal
        /// 
        /// </summary>
        /// <author> Baber Waqas</author>
        /// <param name="physicalsList"></param>
        /// <returns></returns>
        private static SoapNoteCatagoryDTO ProcessROS(List<ROSDTO> rosList)
        {
            var dto = new SoapNoteCatagoryDTO();
            dto.Name = "Review Of System";
            var moduleNote = getModuleNote(ModuleNameContants.REVIEW_OF_SYSTEM_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";
            #region Process ROS List
            foreach (var rosItem in RosDataList)
            {
                bool isCategorySelected = false;
                bool isCategoryNormal = true;
                if (rosItem.children.Count > 0)
                {
                    bool isSubCategoryNormal = true;
                    bool isSubCategorySelected = false;
                    bool qualifyAllNormal = true;
                    String messageString = "";
                    foreach (var rosLevelTwo in rosItem.children)
                    {
                        if (rosLevelTwo.children != null && rosLevelTwo.children.Count > 0)
                        {
                            #region 3rd level processing for ros

                            String subMessageString = "";
                            bool is3rdCategoryNormal = true;
                            bool is3rdCategorySelected = false;
                            bool qualifyAllNormalSub = true;
                            foreach (var rosLevelThreeItem in rosLevelTwo.children)
                            {
                                ROSDTO rosMatcheditem = null;
                                // linq querry for extract matching items in both lists for sub category
                                var resultSub = from ros in rosList
                                                where
                                                    ros.ROSSubLevel3CategoryTypeID ==
                                                    rosLevelThreeItem.ROSSubLevel3CategoryTypeID
                                                select ros;
                                if (resultSub.Any())
                                    rosMatcheditem = (ROSDTO)resultSub.First();
                                if (rosMatcheditem != null)
                                {
                                    isSubCategorySelected = true;
                                    is3rdCategorySelected = true;

                                    if (rosMatcheditem.IsNormal.Equals("false") &&
                                        !string.IsNullOrEmpty(rosMatcheditem.Notes))
                                    {
                                        isSubCategoryNormal = false;
                                        is3rdCategoryNormal = false;
                                        subMessageString += ModuleNameContants.TagTabinHTML + "<b>" +
                                                            rosMatcheditem.ROSSubLevel3CategoryName + " is Abnormal" +
                                                            "</b>";
                                        dto.Notes += rosMatcheditem.SubCategoryName + "," +
                                                     rosMatcheditem.ROSSubLevel3CategoryName + " " +
                                                     rosMatcheditem.Notes + " </br>";
                                    }
                                    else if (rosMatcheditem.IsNormal.Equals("true"))
                                        subMessageString += ModuleNameContants.TagTabinHTML + "" +
                                                            rosMatcheditem.ROSSubLevel3CategoryName + " is Normal" +
                                                            "</br>";
                                    else
                                    {
                                        isSubCategorySelected = false;
                                        is3rdCategorySelected = false;
                                        qualifyAllNormalSub = false;
                                    }
                                }
                                else
                                    qualifyAllNormalSub = false;
                            }
                            if (isSubCategoryNormal == true && isSubCategorySelected == true &&
                                qualifyAllNormalSub == true)
                                dto.Particulars += rosLevelTwo.label + "</br>" + ModuleNameContants.TagTabinHTML +
                                                   " All are Normal" + "</br><hr />";
                            else if (is3rdCategoryNormal == true && is3rdCategorySelected == true &&
                                     qualifyAllNormalSub == true)
                                dto.Particulars += rosLevelTwo.label + "</br>" + ModuleNameContants.TagTabinHTML +
                                                   " All are Normal" + "</br><hr />";
                            else if (subMessageString.Trim().Length > 0)
                            {
                                dto.Particulars += rosLevelTwo.label + "</br>" + subMessageString + "<hr />";
                            }

                            #endregion
                        }
                        else
                        {
                            #region 2nd Level ROS Processing

                            // linq querry for extract matching items in both lists for sub category
                            var resultMain = from ros in rosList
                                             where ros.ROSSubCategoryTypeID == rosLevelTwo.ROSSubCategoryTypeID
                                             select ros;
                            ROSDTO rosMatchedItemForSubCat = null;
                            ;
                            if (resultMain.Any())
                                rosMatchedItemForSubCat = (ROSDTO)resultMain.First();
                            if (rosMatchedItemForSubCat != null)
                            {
                                isCategorySelected = true;
                                if (rosMatchedItemForSubCat.IsNormal.Equals("false") &&
                                    !string.IsNullOrEmpty(rosMatchedItemForSubCat.Notes))
                                {
                                    // if true print ros item is abnormal
                                    isCategoryNormal = false;
                                    messageString +=ModuleNameContants.TagTabinHTML + "<b>"+
                                                       rosMatchedItemForSubCat.SubCategoryName + " is Abnormal</b></br>";
                                    dto.Notes += rosMatchedItemForSubCat.SubCategoryName + "," +
                                                 rosMatchedItemForSubCat.ROSSubLevel3CategoryName + " " +
                                                 rosMatchedItemForSubCat.Notes + " </br>";

                                }
                                else if (rosMatchedItemForSubCat.IsNormal.Equals("true"))
                                    messageString += ModuleNameContants.TagTabinHTML +
                                                     rosMatchedItemForSubCat.SubCategoryName + " is Normal" + "</br>";
                                else
                                {
                                    isCategorySelected = false;
                                    qualifyAllNormal = false;
                                }
                            }
                            else
                                qualifyAllNormal = false;

                            #endregion
                        }
                    }
                    if (isCategoryNormal == true && isCategorySelected == true && qualifyAllNormal == true)
                        dto.Particulars += rosItem.MainCategoryName + "</br>" + ModuleNameContants.TagTabinHTML +
                                           " All are Normal" + "</br><hr />";
                    else if (messageString.Trim().Length>0)
                    {
                       // rosLevelTwo.label +"</br>" + subMessageString + "<hr />"; }
                        dto.Particulars += rosItem.MainCategoryName + "</br>" + messageString + "</br><hr />";
                    }

                }
            }
            #endregion
            return dto;
        }

        /// <summary>
        /// prepare physical soap not list, the logic of this method is to retrieve original static list from db and then check each item in patient physical list
        /// to show it is normal or abnormal
        /// 
        /// </summary>
        /// <author> Baber Waqas</author>
        /// <param name="physicalsList"></param>
        /// <returns></returns>
        private static SoapNoteCatagoryDTO ProcessPhysicals(List<PhysicalsDTO> physicalsList)
        {
            #region Processing physicals
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Physical Exam";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.PHYSICAL_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";

            #region Process Physical List
            foreach (var phyItem in PhyDataList)
            {
                bool isCategorySelected = false;
                bool isCategoryNormal = true;
                if (phyItem.children.Count > 0)
                {
                    bool isSubCategoryNormal = true;
                    bool isSubCategorySelected = false;
                    bool qualifyAllNormal = true;
                    String messageString = "";
                    foreach (var phyLevelTwo in phyItem.children)
                    {
                        if (phyLevelTwo.children != null && phyLevelTwo.children.Count > 0)
                        {
                            #region 3rd level processing for Physical
                            String subMessageString = "";
                            bool is3rdCategoryNormal = true;
                            bool is3rdCategorySelected = false;
                            bool qualifyAllNormalSub = true;
                            foreach (var phyLevelThreeItem in phyLevelTwo.children)
                            {
                                PhysicalsDTO matchedItem = null;
                                // linq querry for extract matching items in both lists for sub category
                                var resultSub = from phy in physicalsList where phy.PhysicalSubLevel3CategoryTypeID == phyLevelThreeItem.PhysicalSubLevel3CategoryTypeID select phy;
                                if (resultSub.Count() > 0)
                                    matchedItem = (PhysicalsDTO)resultSub.First();
                                if (matchedItem != null)
                                {
                                    isSubCategorySelected = true;
                                    is3rdCategorySelected = true;
                                    if (matchedItem.IsNormal.Equals("false") && matchedItem.Notes != null && matchedItem.Notes.Length > 0)
                                    {
                                        isSubCategoryNormal = false;
                                        is3rdCategoryNormal = false;
                                        //dto.Particulars += matchedItem.SubCategoryName + ":" + matchedItem.SubLevel3CategoryName + " is Abnormal" + "</br>";
                                        subMessageString = ModuleNameContants.TagTabinHTML + "<b>" +
                                                            matchedItem.SubLevel3CategoryName + " is Abnormal" +
                                                            "</b></br>";

                                        dto.Notes += matchedItem.SubCategoryName + "," + matchedItem.SubLevel3CategoryName + " " + matchedItem.Notes + " </br>";

                                    }
                                    else if (matchedItem.IsNormal.Equals("true"))
                                    {
                                        subMessageString += ModuleNameContants.TagTabinHTML + "" +
                                                            matchedItem.SubLevel3CategoryName + " is Normal" +
                                                            "</br>"; ;
                                        //subMessageString += matchedItem.SubCategoryName + ":" + matchedItem.SubLevel3CategoryName + " is Normal" + "</br>";
                                    }
                                    else
                                    {
                                        isSubCategorySelected = false;
                                        is3rdCategorySelected = false;
                                        qualifyAllNormalSub = false;
                                    }

                                }
                                else
                                    qualifyAllNormalSub = false;
                            }
                            if (isSubCategoryNormal == true && isSubCategorySelected == true && qualifyAllNormalSub == true)
                                //dto.Particulars += phyLevelTwo.label + " All are Normal" + "</br>";
                                dto.Particulars += phyLevelTwo.label + "</br>" + ModuleNameContants.TagTabinHTML +
                                                   " All are Normal" + "</br><hr />";
                            else if (is3rdCategoryNormal == true && is3rdCategorySelected == true && qualifyAllNormalSub == true)
                                //dto.Particulars += phyLevelTwo.label + " All are Normal" + "</br>";
                                dto.Particulars += phyLevelTwo.label + "</br>" + ModuleNameContants.TagTabinHTML +
                                                   " All are Normal" + "</br><hr />";
                            //else
                            //    dto.Particulars += subMessageString;
                            else if (subMessageString.Trim().Length > 0)
                            {
                                dto.Particulars += phyLevelTwo.label + "</br>" + subMessageString + "<hr />";
                            }
                            #endregion
                        }
                        else
                        {
                            #region 2nd Level ROS Processing
                            // linq querry for extract matching items in both lists for sub category
                            var resultMain = from phy in physicalsList where phy.PhysicalSubCategoryTypeID == phyLevelTwo.PhysicalSubCategoryTypeID select phy;
                            PhysicalsDTO matchedItemForSubCat = null; ;
                            if (resultMain.Any())
                                matchedItemForSubCat = (PhysicalsDTO)resultMain.First();
                            if (matchedItemForSubCat != null)
                            {
                                isCategorySelected = true;
                                if (matchedItemForSubCat.IsNormal.Equals("false"))
                                {
                                    isCategoryNormal = false;
                                    //dto.Particulars += matchedItemForSubCat.MainCategoryName + ":" + matchedItemForSubCat.SubCategoryName + " is Abnormal" + "</br>";
                                    messageString += ModuleNameContants.TagTabinHTML + "<b>" +
                                                       matchedItemForSubCat.SubCategoryName + " is Abnormal</b></br>";
                                    dto.Notes += matchedItemForSubCat.SubCategoryName + ":" + matchedItemForSubCat.SubLevel3CategoryName + " " + matchedItemForSubCat.Notes + " </br>";
                                }
                                else if (matchedItemForSubCat.IsNormal.Equals("true"))
                                    //messageString += matchedItemForSubCat.MainCategoryName + ":" + matchedItemForSubCat.SubCategoryName + " is Normal" + "</br>";
                                    messageString += ModuleNameContants.TagTabinHTML +
                                                     matchedItemForSubCat.SubCategoryName + " is Normal" + "</br>";
                                else
                                {
                                    isCategorySelected = false;
                                    qualifyAllNormal = false;
                                }
                            }
                            else
                                qualifyAllNormal = false;
                            #endregion
                        }
                    }
                    if (isCategoryNormal == true && isCategorySelected == true && qualifyAllNormal == true)
                        //dto.Particulars += phyItem.MainCategoryName + " All are Normal" + "</br>";
                        dto.Particulars += phyItem.MainCategoryName + "</br>" + ModuleNameContants.TagTabinHTML +
                                           " All are Normal" + "</br><hr />";
                    else if (messageString.Trim().Length>0)
                        //dto.Particulars += messageString;
                        dto.Particulars += phyItem.MainCategoryName + "</br>" + messageString + "</br><hr />";
                }
            }
            #endregion
            return dto;
            #endregion
        }

        private static SoapNoteCatagoryDTO ProcessLabsIn(List<InHouseLabDTO> labsList)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "In-house Lab";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.LAB_IN_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note: </b>" + moduleNote.Note + "<br>";

            if (labsList != null)
            {
                int i;
                for (i = 0; i < labsList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    InHouseLabDTO lab = labsList[i];
                    dto.Notes += lab.InHouseLabTestType.TestName + ": " + lab.Notes + "<br />";
                    dto.Particulars += "<b>Code</b>: " + lab.InHouseLabTestType.TestCode + " ,<b>Test</b>: " + lab.InHouseLabTestType.TestName + " ,<b>Result</b>: " + lab.TestResult;
                    if (string.IsNullOrEmpty(lab.TestResult))
                        dto.Particulars += " ,<b>Result</b> : " + lab.TestResult;

                }
            }

            return dto;

        }

        private static SoapNoteCatagoryDTO ProcessLabsOut(List<LabDTO> labsList)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Lab";
            if (labsList != null)
            {
                int i;
                for (i = 0; i < labsList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    LabDTO lab = labsList[i];
                    if (!string.IsNullOrEmpty(lab.LabNote))
                        dto.Notes += lab.LabType.LabName + lab.LabNote + "<br />";
                    dto.Particulars += "<b>Test</b>: " + lab.LabType.LabName;
                    if (!string.IsNullOrEmpty(lab.Description))
                    {
                        dto.Particulars += "<b>Description</b>: " + lab.Description;
                    }
                }
            }

            return dto;

        }


        private static SoapNoteCatagoryDTO ProcessDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int qiVisitId)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Diagnosis";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.DIAGNOSIS_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note: </b>" + moduleNote.Note + "<br>";

            if (diagnosisList != null)
            {
                int i;
                for (i = 0; i < diagnosisList.Count; i++)
                {
                    PatientDiagnosisDTO diag = diagnosisList[i];
                    if (diag.VisitID != 0 && diag.VisitID == qiVisitId)
                    {
                        if (i > 0)
                        {
                            dto.Particulars += NEW_LINE_HTML;
                        }

                        if (!string.IsNullOrEmpty(diag.Note))
                            dto.Notes += diag.ICDCode.IcdCodeDescription + ": " + diag.Note + "<br />";
                        dto.Particulars += "<b>OnSet</b>: " + diag.OnSet + ",<b>Diagnosis</b>: ( " + diag.ICDCode.IcdCodeName + " ) " + diag.ICDCode.IcdCodeDescription;
                        if (diag.IsChronic)
                        {
                            dto.Particulars += ",<b>Chronic</b>: Yes";
                        }
                        else if (!diag.IsChronic)
                        {
                            dto.Particulars += ",<b>Chronic</b>: No";
                        }
                    }
                }
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessProcedure(List<ProcedureDTO> procedureList)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Procedure";
            //ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants);
            //dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note<b>" + moduleNote.Note + "<br>";

            if (procedureList != null)
            {
                int i;
                for (i = 0; i < procedureList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    ProcedureDTO proc = procedureList[i];
                    if (!string.IsNullOrEmpty(proc.Notes))
                        dto.Notes += proc.CPTMedicalType.ProcedureDescription + proc.Notes + "<br />";
                    if (proc.CPTMedicalType != null)
                        dto.Particulars += "<b>CPT Code</b>: " + proc.CPTMedicalType.ProcedureCode + ",<b>Procedure</b>: " + proc.CPTMedicalType.ProcedureDescription;
                }
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessPresciption(List<PrescriptionMedicationDTO> preMedList)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Presciption";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.MEDICATION_PRESCRIBED_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note: </b>" + moduleNote.Note + "<br>";

            int i = 0;
            foreach (PrescriptionMedicationDTO preMed in preMedList)
            {
                if (i > 0)
                {
                    dto.Particulars += NEW_LINE_HTML;
                }
                dto.Particulars += preMed.GenericName + ": " + preMed.Dose;
                if (string.IsNullOrEmpty(preMed.Strength))
                {
                    dto.Particulars += "(" + preMed.Strength + ")";
                }
                dto.Particulars += preMed.Signature + "," + preMed.Action + "," + preMed.Refills;

                ++i;
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessFollowUp(PatientFollowUpDTO followUpDto)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Follow-Up";
            ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.FOLLOWUP_MODULE_NAME);
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note: </b>" + moduleNote.Note + "<br>";

            if (followUpDto != null)
            {
                //dto.Notes += "Notes: " + followUpDto.Notes;
                if (followUpDto.IsByDate)
                {
                    dto.Particulars += "By Date";
                }
                else
                {
                    dto.Particulars += "Upon Completion of";
                    foreach (MedicationDTO med in MedicationList)
                    {
                        if (followUpDto.DrugID == med.PatientMedicationID)
                        {
                            dto.Particulars += ",<b>Drug</b>: " + med.BrandName;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(followUpDto.FollowUpDate))
                    dto.Particulars += ",<b>Date</b>: " + followUpDto.FollowUpDate;
                if (!string.IsNullOrEmpty(followUpDto.FollowUpMethodType.MethodName))
                    dto.Particulars += ",<b>Method</b>: " + followUpDto.FollowUpMethodType.MethodName;
                dto.Particulars += "</br>";
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessFollowInstrucation(List<PatientFollowupInstructionDTO> followUpInstructionList, SoapNoteCatagoryDTO dto)
        {
            //SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            //dto.Name = "Patient Followup Instruction";
            //ModuleNoteDTO moduleNote = getModuleNote(ModuleNameContants.recco);
            //dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note<b>" + moduleNote.Note + "<br>";
            if (followUpInstructionList != null && followUpInstructionList.Count > 0)
            {
                int i = 0;
                foreach (PatientFollowupInstructionDTO followupInstrucation in followUpInstructionList)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    dto.Notes = followupInstrucation.Notes;
                    dto.Particulars += "Service: " + followupInstrucation.objServiceTypeDTO.FollowupServiceTypeName + ",";
                    if (followupInstrucation.IsAgeSelected)
                    {
                        dto.Particulars += "Age: ";
                    }
                    else
                    {
                        dto.Particulars += "Duration: ";
                    }
                    if (followupInstrucation.IsAgeSelected)
                    {
                        if (followupInstrucation.AgeTypeDTO != null)
                            dto.Particulars += followupInstrucation.AgeTypeDTO.AgeTypeName + " ";

                        if (followupInstrucation.AgeValueTypeDTO != null)
                            dto.Particulars += followupInstrucation.AgeValueTypeDTO.AgeValueTypeName;
                    }
                    else if (followupInstrucation.IsDurationSelected)
                    {
                        if (followupInstrucation.AfterDurationValue != 0)
                            dto.Particulars += "After " + followupInstrucation.AfterDurationValue + " ";

                        if (followupInstrucation.AfterInstructionDurationTypeDTO != null)
                            dto.Particulars += followupInstrucation.AfterInstructionDurationTypeDTO.DurationTypeName + " ";

                        if (followupInstrucation.ForDurationValue != 0)
                            dto.Particulars += "for " + followupInstrucation.ForDurationValue + " ";

                        if (followupInstrucation.ForInstructionDurationTypeDTO != null)
                            dto.Particulars += followupInstrucation.ForInstructionDurationTypeDTO.DurationTypeName;
                    }

                    ++i;
                }
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessRecommendation(List<PatientRecommendationDTO> recommendationList)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Recommendation";

            if (recommendationList != null)
            {
                int i;
                for (i = 0; i < recommendationList.Count; i++)
                {
                    if (i > 0)
                    {
                        dto.Particulars += NEW_LINE_HTML;
                    }
                    PatientRecommendationDTO rec = recommendationList[i];
                    if (!string.IsNullOrEmpty(rec.Note))
                        dto.Notes += rec.Plan.PlanName + rec.Note;

                    if (rec.Plan != null)
                        dto.Particulars += "<b>Plan</b>: " + rec.Plan.PlanName;

                    if (rec.Method != null)
                        dto.Particulars += ",<b>Method</b>: " + rec.Method.MethodName;

                    if (rec.Links != null)
                        dto.Particulars += ",<b>Links</b>: " + rec.Links;

                    if (rec.Duration != null)
                        dto.Particulars += ",<b>Duration</b>: " + rec.Duration.DurationName;

                    if (rec.Description != null)
                        dto.Particulars += ",<b>Description</b>: " + rec.Description;

                    if (rec.IsDiscussed == "true")
                    {
                        dto.Particulars += ",<b>Discussed</b>: Yes";
                    }
                    if (rec.IsDiscussed == "false")
                    {
                        dto.Particulars += ",<b>Discussed</b>: No";
                    }
                }
            }
            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessPlanModuleLevelNote(ModuleNoteDTO moduleNote)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Plan";
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";

            return dto;
        }

        private static SoapNoteCatagoryDTO ProcessTreatmentModuleLevelNote(ModuleNoteDTO moduleNote)
        {
            SoapNoteCatagoryDTO dto = new SoapNoteCatagoryDTO();
            dto.Name = "Treatment";
            if (moduleNote != null)
                dto.Notes += "<b>Subject: </b>" + moduleNote.NoteSubject + "<br><b>Note :</b>" + moduleNote.Note + "<br>";

            return dto;
        }

        private static ROSDTO DeepCopy(ROSDTO stateObject)
        {

            MemoryStream memStream = new MemoryStream();

            IFormatter formatter = new BinaryFormatter();

            formatter.Serialize(memStream, stateObject);

            memStream.Seek(0, 0);

            ROSDTO newStateObject = (ROSDTO)formatter.Deserialize(memStream);

            memStream.Close();

            return newStateObject;

        }

        /// <summary>
        /// vitals data checking  move this to utility package
        /// </summary>
        /// <author> Baber waqas</author>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static bool ContainsVitalsData(VitalDTO instance)
        {

            try
            {
                var result = instance.Height + instance.Weight + instance.Temperature + instance.BPDys + instance.BPSys +
                             instance.Pulse + instance.Respiration + instance.Oxygen + "";

                if (result.Length > 0)
                    return true;
            }
            catch
            {
                ;
            }

            return false;


        }
    }
}