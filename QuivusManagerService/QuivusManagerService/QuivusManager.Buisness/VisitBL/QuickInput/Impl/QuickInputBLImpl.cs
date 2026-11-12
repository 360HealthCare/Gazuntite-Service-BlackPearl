using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Utility.Logger;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.ReferenceFactory;
using QuivusManager.Buisness.UserBL.Impl;
using QuivusManager.Buisness.UserBL;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Immunization;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Medical;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Family;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Vital;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Labs;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.HPI;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.ComprehensiveHistory.Social;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.Physicals;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Physical.ROS;

using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Recommendation;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.ChronicMedication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Procedure;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.FollowUp;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Diagnosis;
using QuivusUtility.Utilities;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.Buisness.AutoSaveBL;


namespace QuivusManager.Buisness.VisitBL.QuickInput.Impl
{
    public class QuickInputBLImpl : IQuickInputBL
    {
        VisitServiceClient clientVisit = null;
        IUserBL UserBL = null;

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// create visit
        /// </summary>
        /// <returns></returns> 
        public CreateVisitResponse CreateVisit(VisitDTO visitDTO, int managedByUserId, int managedByUserTypeId)
        {
            CreateVisitResponse response = null;
            int visitId = 0;
            bool isProvider = false;
            try
            {
                QuivusLogger.Info("Calling CreateVisit");

                //LabDTO oj = new LabDTO();
                //oj.LIONICLabTestType

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                if (UserBL == null)
                {
                    UserBL = new UserBLImpl();
                }

                QuivusLogger.Info("Calling IsProviderAtClinic Of User BL::");

              /*  isProvider = UserBL.IsProviderAtClinic(managedByUserId, visitDTO.ClinicBranch.ClinicBranchIdSeq);

                QuivusLogger.Info("Response Recieved  isProvider::" + isProvider);

                if (isProvider)
                {
                    visitDTO.OwnerId = managedByUserId; // the creator is also a Doctor
                    if (visitDTO.OwnerUserType != null)
                    {
                        visitDTO.OwnerUserType.UserTypeId = managedByUserTypeId;
                    }
                }
                else
                {
                    visitDTO.OwnerId = 0; // not set yet
                }
                * Commented By Ismail*/

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching CreateVisit Call TO VisitService");

                visitId = clientVisit.CreateVisit(visitDTO, managedByUserId);

                QuivusLogger.Info("Response Recieved  visitId::" + visitId);

                if (response == null)
                {
                    response = new CreateVisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                response.VisitId = visitId;
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 23 Feb 2011
        /// update visit
        /// </summary>
        /// <returns></returns>     
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SaveVisit(VisitDTO visitDTO, int managedByUserId,int managedByUserTypeId)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool isProvider = false;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling SaveVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                if (UserBL == null)
                {
                    UserBL = new UserBLImpl();
                }

                QuivusLogger.Info("Calling IsProviderAtClinic Of User BL::");
               /* isProvider = UserBL.IsProviderAtClinic(managedByUserId, visitDTO.ClinicBranch.ClinicBranchIdSeq);
                QuivusLogger.Info("Response Recieved  isProvider::" + isProvider);



                if (visitDTO.OwnerId == 0 && isProvider)
                {
                    visitDTO.OwnerId = managedByUserId; // the updater is a Doctor and not set before 
                    if (visitDTO.OwnerUserType != null)
                    {
                        visitDTO.OwnerUserType.UserTypeId = managedByUserTypeId;
                    }
                   // visitDTO.OwnerUserType.UserTypeId = managedByUserTypeId;
                }
                *Commented By Ismail */

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching SaveVisit Call TO VisitService");

                result = clientVisit.SaveVisit(visitDTO, managedByUserId);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();
                }

                if (result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 17 Feb 2011
        /// mark visit as signed
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>  
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SignVisit(int VisitId, int SignedByUserID, int SignedByUserTypeId, DateTime OperationDate)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling SignVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching SignVisit Call TO VisitService");

                result = clientVisit.SignVisit(VisitId, SignedByUserID, SignedByUserTypeId, OperationDate);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns> 
        public ImmunizationSaveResponse ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int cbId, int patientId)
        {
            ImmunizationSaveResponse response = null;
            bool result = true;
            try
            {
                QuivusLogger.Info("Calling ManagePatientImmunization");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientImmunization Call TO VisitService");

                result = clientVisit.ManagePatientImmunization(immunizationList, cbId, patientId);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new ImmunizationSaveResponse();
                }

                if (result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// manage module notes, insert/ update/ delete
        /// </summary>
        /// <param name="moduleNotes"></param>
        /// <returns></returns>      
        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling ManageModuleNotes");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageModuleNotes Call TO VisitService");

                result = clientVisit.ManageModuleNotes(moduleNotes);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new PatientImmunizationResponse();
                }

                if (result)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        public PatientMedicalHistoryResponse ManagePatientMedicalHistory(List<MedicalHistoryDTO> PateintMedicalHistoryList, int cbID, int patId)
        {
            PatientMedicalHistoryResponse response = null;
            int Result = 0;
            try
            {
                QuivusLogger.Info("Calling ManagePatientMedicalHistory");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientMedicalHistory Call TO VisitService");

                foreach (MedicalHistoryDTO obj in PateintMedicalHistoryList)
                    Result = clientVisit.ManagePatientMedicalHistory(obj,cbID,patId);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientMedicalHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result > 0)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 11 Mar 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        public ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId, int UserTypeId)
        {
            ManageVisitResponse response = null;
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManageQuickInputData");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService");

                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + quickInputDTO.VisitId.ToString() + " and PatientID: " + quickInputDTO.PatientId.ToString()
                    + "UserTypeId::" + UserTypeId);

                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                //    try
                //    {
                Status = clientVisit.ManageQuickInputData(quickInputDTO, managedByUserId, UserTypeId);
                //   ts.Complete();
                //}
                //catch
                //{
                //    ts.Dispose();
                //    Status = false;
                //    throw;
                //}
                // }
                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new ManageVisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (Status)
                {
                    response.ResultStatus = Status;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 12 july 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        public ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, int managedByUserId, int UserTypeId,bool IsFinal)
        {
            ManageVisitResponse response = null;
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManageQuickInputData");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService");

                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + quickInputDTO.VisitId.ToString() + " and PatientID: " + quickInputDTO.PatientId.ToString()
                    + "UserTypeId::" + UserTypeId + "IsFinal" + IsFinal);
               
                Status = clientVisit.ManageQuickInput(quickInputDTO, managedByUserId, UserTypeId, IsFinal);
                
                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new ManageVisitResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }

                if (Status)
                {
                    response.ResultStatus = Status;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        public PatientFamilyHistoryResponse ManagePatientFamilyHistory(List<FamilyHistoryDTO> PateintFamilyHistoryList,int UserTypeId,int ClinicBranchId, int ptId)
        {
            PatientFamilyHistoryResponse response = null;
            int Result = 0;
            try
            {
                QuivusLogger.Info("Calling ManagePatientFamilyHistory");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientFamilyHistory Call TO VisitService");

                foreach (FamilyHistoryDTO obj in PateintFamilyHistoryList)
                    Result = clientVisit.ManagePatientFamilyHistory(obj,UserTypeId,ClinicBranchId, ptId);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientFamilyHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result > 0)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        public PatientVitalResponse ManagePatientVitals(List<VitalDTO> VitalList)
        {
            PatientVitalResponse response = null;
            int Result = 0;
            try
            {
                QuivusLogger.Info("Calling ManagePatientVitals");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientVitals Call TO VisitService");

                foreach (VitalDTO VitalObject in VitalList)
                    Result = clientVisit.ManagePatientVitals(VitalObject);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientVitalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result > 0)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Labs
        /// </summary>
        /// <returns></returns>
        public PatientLabResponse ManagePatientLabs(List<LabDTO> LabList)
        {
            PatientLabResponse response = null;
            int Result = 0;
            try
            {
                QuivusLogger.Info("Calling ManagePatientLabs");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientLabs Call TO VisitService");

                foreach (LabDTO LabObject in LabList)
                    Result = clientVisit.ManagePatientLabs(LabObject);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result > 0)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
                else 
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    response.ResponseWarning = ProjectConstant.INVALID_UPDATE_WITHOUT_IS_DIRTY;
                // 
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>

        public PatientHPIResponse ManagePatientHPI(List<HPI_CCDTO> hpiList)
        {
            PatientHPIResponse response = null;
            bool Result = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientHPI");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientHPI Call TO VisitService");

                    Result = clientVisit.ManagePatientHPI(hpiList);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientHPIResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public PatientFollowUpResponse ManagePatientFollowUp(List<PatientFollowUpDTO> patientFollowUpList)
        {
            PatientFollowUpResponse response = null;
            bool Result = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientHPI");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientHPI Call TO VisitService");

                foreach (PatientFollowUpDTO obj in patientFollowUpList)
                    clientVisit.ManagePatientFollowUp(obj);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientFollowUpResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

         /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public PatientDiagnosisResponse ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int cbID, int patId)
        {
            PatientDiagnosisResponse response = null;
            bool Result = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientDiagnosis");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientDiagnosis Call TO VisitService");

//                foreach (PatientDiagnosisDTO obj in diagnosisList)
                    clientVisit.ManagePatientDiagnosis(diagnosisList, cbID, patId);

                QuivusLogger.Info("Response Recieved  Result::" + Result);

                if (response == null)
                {
                    response = new PatientDiagnosisResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Result)
                {
                    response.ResponseData = Result;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
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
        public HPIDetailAutoCompleteResponse LoadHPIDetailAutoCompleteByTypeIdAndClinicId(int hpiTypeId, int clinicBranchId, string searchCriteria)
        {
            HPIDetailAutoCompleteResponse response = null;
            List<string> list;
            try
            {
                QuivusLogger.Info("Calling LoadHPIDetailAutoCompleteByTypeIdAndClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadHPIDetailAutoCompleteByTypeIdAndClinicId Call TO VisitService");

                list = clientVisit.LoadHPIDetailAutoCompleteByTypeIdAndClinicId(hpiTypeId, clinicBranchId, searchCriteria);

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new HPIDetailAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (list != null)
                {
                    response.AutoCompleteList = list;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }



        public SocialHistoryAddictionResponse ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, int cbId, int PatId)
        {
            SocialHistoryAddictionResponse response = null;
            int status = 0;
            try
            {
                QuivusLogger.Info("Calling ManageSocialHistoryAddiction");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManageSocialHistoryAddiction Call TO VisitService");

                status = clientVisit.ManageSocialHistoryAddiction(SocialHistoryAddiction,cbId,PatId);

                QuivusLogger.Info("Response Recieved  status::" + status);

                if (response == null)
                {
                    response = new SocialHistoryAddictionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (status > 0)
                {
                    response.SocialHistoryAddictionList = null;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public SocialHistoryAddictionResponse DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject)
        {
            SocialHistoryAddictionResponse response = null;
            bool status = false;
            try
            {
                QuivusLogger.Info("Calling DeleteSocialHistoryAddiction");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching DeleteSocialHistoryAddiction Call TO VisitService");

                status = clientVisit.DeleteSocialHistoryAddiction(DeletedObject);

                QuivusLogger.Info("Response Recieved  status::" + status);

                if (response == null)
                {
                    response = new SocialHistoryAddictionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (status)
                {
                    response.SocialHistoryAddictionList = null;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public SocialHistoryResponse ManagePatientSocialHistory(List<SocialHistoryDTO> SocialHistoryList, int cbID, int patId)
        {
            SocialHistoryResponse response = null;
            int status = 0;
            try
            {
                QuivusLogger.Info("Calling ManagePatientSocialHistory");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientSocialHistory Call TO VisitService");

                foreach (SocialHistoryDTO SocialHistoryObject in SocialHistoryList)
                    status = clientVisit.ManagePatientSocialHistory(SocialHistoryObject, cbID, patId);

                QuivusLogger.Info("Response Recieved  status::" + status);

                if (response == null)
                {
                    response = new SocialHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (status > 0)
                {
                    response.SocialHistoryList = null;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public SocialHistoryResponse DeleteSocialHistory(SocialHistoryDTO DeletedObject)
        {
            SocialHistoryResponse response = null;
            bool status = false;
            try
            {
                QuivusLogger.Info("Calling DeleteSocialHistory");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching DeleteSocialHistory Call TO VisitService");

                status = clientVisit.DeleteSocialHistory(DeletedObject);

                QuivusLogger.Info("Response Recieved  status::" + status);

                if (response == null)
                {
                    response = new SocialHistoryResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (status)
                {
                    response.SocialHistoryList = null;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public PatientPhysicalResponse ManagePatientPhysical(List<PhysicalsDTO> PhysicalList)
        {
            PatientPhysicalResponse response = null;
            bool status = false;
            try
            {
                QuivusLogger.Info("Calling PhysicalList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientPhysical Call TO VisitService");

                status = clientVisit.ManagePatientPhysical(PhysicalList);

                QuivusLogger.Info("Response Recieved  status::" + status);

                if (response == null)
                {
                    response = new PatientPhysicalResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public PatientROSResponse ManagePatientROS(List<ROSDTO> ROSList)
        {

            PatientROSResponse response = null;
            bool status = false;
            try
            {
                QuivusLogger.Info("Calling PhysicalList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientROS Call TO VisitService");

                status = clientVisit.ManagePatientROS(ROSList);

                QuivusLogger.Info("Response Recieved  status::" + status);

                if (response == null)
                {
                    response = new PatientROSResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Mar 2011
        /// get Recommendation auto complete suggestion box, by type id and clinic branch
        /// </summary>        
        /// <param name="searchCriteria"></param>
        /// <param name="clinicBranchId"></param>
        /// <returns></returns> 
        public LinksAutoCompleteResponse LoadRecommendationLinkAutoCompleteByClinicId(int clinicBranchId, string searchCriteria)
        {
            LinksAutoCompleteResponse response = null;
            List<string> list;
            try
            {
                QuivusLogger.Info("Calling LoadRecommendationLinksAutoCompleteByClinicId");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching LoadRecommendationLinksAutoCompleteByClinicId Call TO VisitService");

                list = clientVisit.LoadRecommendationLinksAutoCompleteByClinicId(clinicBranchId, searchCriteria);

                QuivusLogger.Info("Response Recieved  list::" + list);

                if (response == null)
                {
                    response = new LinksAutoCompleteResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (list != null)
                {
                    response.LinksList = list;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        public PatientRecommendationResponse ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList)
        {
            PatientRecommendationResponse response = null;
            //   List<PatientRecommendationDTO> TreatList = null;
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling PhysicalList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientRecommendation Call TO VisitService");

                Status = clientVisit.ManagePatientRecommendation(RecommendationList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientRecommendationResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    // response.RecommendationList = TreatList;
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ServiceResponse.ServiceCustomResponse.ServiceResponse CoSignVisit(int VisitId, int ProviderId, int ProviderUserId, int ProviderTypeId, DateTime OperationDate)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;
            bool result = false;
            try
            {
                QuivusLogger.Info("Calling SignVisit");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching CoSignVisit Call TO VisitService");

                result = clientVisit.CoSignVisit(VisitId, ProviderId, ProviderUserId, ProviderTypeId, OperationDate);

                QuivusLogger.Info("Response Recieved  result::" + result);

                if (response == null)
                {
                    response = new ServiceResponse.ServiceCustomResponse.ServiceResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public AllergyResponse ManagePatientAllergy(AllergyDTO AllergyObj)
        {
            AllergyResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling PhysicalList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientAllergy Call TO VisitService");

                Status = clientVisit.ManagePatientAllergy(AllergyObj);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new AllergyResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public AllergyResponse ManagePatientAllergyList(List<AllergyDTO> AllergyList)
        {
            AllergyResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientAllergyList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientAllergy Call TO VisitService");

                Status = clientVisit.ManagePatientAllergies(AllergyList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new AllergyResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient chrnoc medication
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public MedicationPrescribedResponse ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj)
        {
            MedicationPrescribedResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling PhysicalList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientAllergy Call TO VisitService");

                Status = clientVisit.ManagePatientChronicMedication(ChronicMedicationObj);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new MedicationPrescribedResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        public PatientProcedureResponse ManagePatientProcedure(List<ProcedureDTO> InputList)
        {
            PatientProcedureResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientProcedure");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientProcedure Call TO VisitService");

                Status = clientVisit.ManagePatientProcedure(InputList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientProcedureResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }



        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientInHouseLabResponse ManagePatientInHouseLabs(List<InHouseLabDTO> InputList)
        {
            PatientInHouseLabResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientInHouseLabs");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientInHouseLabs Call TO VisitService");

                Status = clientVisit.ManagePatientInHouseLabs(InputList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientInHouseLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs results
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>

        public PatientInHouseLabResponse ManagePatientInHouseLabResults(int PatientInHouseLabTestID, String TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt)
        {
            PatientInHouseLabResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientInHouseLabResults");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientInHouseLabResults Call TO VisitService");

                Status = clientVisit.ManagePatientInHouseLabResults(PatientInHouseLabTestID, TestResult, ResultAttachment, ModifiedBy, ModifiedAt);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientInHouseLabResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>

        public PatientMedicationReponse ManagePatientMedication(MedicationDTO InputObj)
        {
            PatientMedicationReponse response = null;

            int Status = 0;
            try
            {
                QuivusLogger.Info("Calling ManagePatientMedication");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientMedication Call TO VisitService");

                Status = clientVisit.ManagePatientMedication(InputObj);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientMedicationReponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status > 0)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Ismail
        /// Date 21 july 2011
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientMedicationReponse ManagePatientMedicationList(List<MedicationDTO> InputList)
        {
            PatientMedicationReponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientMedicationList");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientMedicationList Call TO VisitService");

                Status = clientVisit.ManagePatientMedicationList(InputList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientMedicationReponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// Delete patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public PatientMedicationReponse DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt)
        {
            PatientMedicationReponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling DeletePatientMedication");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching DeletePatientMedication Call TO VisitService");

                Status = clientVisit.DeletePatientMedication(PatientMedicationID, ModifiedAt);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientMedicationReponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

           /// <summary>
        /// Author Ismail 
        /// Date 22 May 2011
        /// This Method Will be used to Insert/Update/Delete Patient follow up instructions
        /// </summary>
        /// <returns></returns>
        public PatientFollowupInstructionResponse ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList)
        {
            PatientFollowupInstructionResponse response = null;

            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling PatientFollowupInstructionResponse");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching PatientFollowupInstructionResponse Call TO VisitService");

                Status = clientVisit.ManagePatientFollowupInstruction(InputList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

                if (response == null)
                {
                    response = new PatientFollowupInstructionResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

                if (Status)
                {
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return response;
        }


        public bool SaveTempQI(QuickInputDTO quickInputDTO, string fileName)
        {
            bool response = false;
            try
            {
                AutoSaveManager autoSaveMng = new AutoSaveManager();
                response = autoSaveMng.SaveTempQI(quickInputDTO, fileName);
            }
            catch
            {
                throw;
            }
            return response;
        }


        public bool DeleteTempQI(string fileName)
        {
            bool response = false;
            try
            {
                QuickInputTempUtility tempUtilityObj = new QuickInputTempUtility();
                response = tempUtilityObj.DeleteTempQI(fileName);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public ServiceResponse.ServiceCustomResponse.QuickInput.QuickInputResponse LoadTempQI(string fileName)
        {
            QuickInputResponse tempQIResponse = null;
            try
            {
                AutoSaveManager autoSaveMng = new AutoSaveManager();
                tempQIResponse = autoSaveMng.LoadTempQI(fileName);
                if(tempQIResponse==null)
                {
                   tempQIResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                   tempQIResponse.ResponseMessage=ProjectConstant.LOAD_AUTOSAVE_MSG;
                }
                else if(tempQIResponse!=null)
                {
                    tempQIResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch
            {
                throw;
            }
            return tempQIResponse;
        }

        public bool IsTempQIExists(string fileName)
        {
            bool response = false;
            try
            {
                QuickInputTempUtility tempUtilityObj = new QuickInputTempUtility();
                response = tempUtilityObj.IsTempQIExists(fileName);
            }
            catch
            {
                throw;
            }
            return response;
        }


        public bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist)
        {
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientPrescriptionMedication");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientPrescriptionMedication Call TO VisitService");

                Status = clientVisit.ManagePatientPrescriptionMedication(PrescriptionMedicationlist);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

            }
            catch
            {
                throw;
            }
            return Status;
        }

        public bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientDiagnosisByRcopia");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching ManagePatientDiagnosisByRcopia Call TO VisitService");

                Status = clientVisit.ManagePatientDiagnosisByRcopia(diagnosisList);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

            }
            catch
            {
                throw;
            }
            return Status;
        }


         /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient Family History
        /// </summary>
        /// <returns></returns>

        public bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject)
        {
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling DeleteFamilyHistory");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching DeleteFamilyHistory Call TO VisitService");

                Status = clientVisit.DeleteFamilyHistory(DeletedObject);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

            }
            catch
            {
                throw;
            }
            return Status;
        }

         /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient medical History
        /// PatientMedicalHistory_Delete
        /// </summary>
        /// <returns></returns>
        public bool DeleteMedicalHistory(MedicalHistoryDTO DeletedObject)
        {
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling DeleteMedicalHistory");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching DeleteMedicalHistory Call TO VisitService");

                Status = clientVisit.DeleteMedicalHistory(DeletedObject);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

            }
            catch
            {
                throw;
            }
            return Status;
        }

        /// <summary>
        /// Author Waqas Baber 
        /// Date 4 nov  2011
        /// This Method Will be used to Delete and update allergies to dr first
        /// 
        /// </summary>
        /// <returns></returns>
        public void UpdateAllergiesToEprescription(IEnumerable<AllergyDTO> qResults, int patientID)
        {
            // TODO:implement update call to eprescription
            return;
        }

        /// <summary>
        /// Author Baber Waqas
        /// Date 24 jan 2012
        /// This Method Will be used to add  custom ICD Code and description
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ManageCustomICDCode(ICDCodeDTO customICDCodeDTO, int managedByUserIDForVisit, int clinicBranchID)
        {
            bool Status = false;
            try
            {
                QuivusLogger.Info("Calling Manage Custom ICD CODE");

                if (clientVisit == null)
                {
                    clientVisit = (VisitServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(VisitServiceClient));


                    // adding some line to sure svn commit
                }

                QuivusLogger.Info("Calling service :: VisitService");

                QuivusLogger.Info("Dispatching manage icd code  Call TO VisitService");

                Status = clientVisit.ManageCustomICDCode(customICDCodeDTO, managedByUserIDForVisit,clinicBranchID);

                QuivusLogger.Info("Response Recieved  Status::" + Status);

            }
            catch
            {
                throw;
            }
            return Status;
        }





    }
}
