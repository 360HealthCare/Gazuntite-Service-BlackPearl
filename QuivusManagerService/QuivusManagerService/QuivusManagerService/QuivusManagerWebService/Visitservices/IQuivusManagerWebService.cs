using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using QuivusManager.IntegratedServies.VisitService;

namespace QuivusManagerWebService
{
    public partial interface IQuivusManagerWebService
    {

        #region WEB PORTAL

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient allergy prescribed by PatientId by Id
        /// </summary>
        /// <returns></returns>        
        [OperationContract]
        List<AllergyDTO> LoadPatientAllergyAllByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medical history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MedicalHistoryDTO> LoadMedicalHistoryAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient family history by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<FamilyHistoryDTO> LoadFamilyHistoryAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        [OperationContract]
        List<SocialhistoryVBDTO> LoadSocialHistoryAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient medication by patient prescription Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MedicationDTO> LoadPatientMedicationAllByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// get immunization list of patient        
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="providerUserId"></param>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        List<ImmunizationDTO> LoadPatientImmunizationAllByPatientId(int patientId);


        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient visit by clinic branch Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VitalDTO> LoadPatientVitalAll(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient hpi by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<HPI_CCDTO> LoadHPIAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient ROS by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ROSDTO> LoadROSAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load patient physicals by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PhysicalsDTO> LoadPhysicalsAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 14 Sep 2011
        /// This Method Will be used to load Patient module dictation by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ModuleDictationDTO> LoadModuleDictationAllByPatientId(int PatientId);

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 20 May 2011
        /// This Method Will be used to load Patient module notes by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ModuleNoteDTO> LoadModuleNotesAllByPatientId(int PatientId);

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient in house labs by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<InHouseLabDTO> LoadPatientInHouseLabsAllByPatientId(int PatientId);//  

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load Patient Labs by patrint Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<LabDTO> LoadPatientLabsAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient resolved diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientResolvedDiagnosisAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientChronicDiagnosisAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient chronic diagnosis by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientDiagnosisDTO> LoadPatientAcuteDiagnosisAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient procedure by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<ProcedureDTO> LoadPatientProcedureAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient medication by patient diagnosis Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PrescriptionMedicationDTO> LoadPrescriptionMedicationAllByPatientId(int PatientId);//

        /// <summary>
        /// Author Mansoor
        /// Date 22 Sep 2011
        /// This Method Will be used to load patient recommendation by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientRecommendationDTO> LoadPatientRecommendationAllByPatientId(int PatientId);//


        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowUpDTO> LoadPatientFollowUpAllByPatientId(int PatientId);


        /// <summary>
        /// Author Mansoor
        /// Date 26 Sep 2011
        /// This Method Will be used to load patient FollowUp by patient Id
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(QuivusFault))]
        List<PatientFollowupInstructionDTO> LoadFollowupInstructionAllByPatientId(int PatientId);

        #endregion
    }
}
