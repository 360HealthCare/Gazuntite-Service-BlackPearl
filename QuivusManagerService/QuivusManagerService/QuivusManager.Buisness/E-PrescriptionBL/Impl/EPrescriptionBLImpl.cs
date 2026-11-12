using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuivusManager.IntegratedServies.EprescriptionService;
using QuivusManager.IntegratedServies.ReferenceFactory;
using Quivus.Utility.Logger;
using QuivusManager.ServiceResponse.ServiceCustomResponse.E_PRescription;
using Quivus.Utility;
using QuivusManager.IntegratedServies.VisitService;
using QuivusManager.Buisness.VisitBL.QuickInput;
using QuivusManager.Buisness.VisitBL.QuickInput.Impl;
using QuivusManager.Buisness.PatientBL;
using QuivusManager.Buisness.PatientBL.Impl;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;

namespace QuivusManager.Buisness.E_PrescriptionBL.Impl
{
    public class EPrescriptionBLImpl : IEPrescriptionBL
    {
        EprescriptionAdapterServiceClient EprescriptionServiceClient = null;
        IQuickInputBL QuickInputBl = null;

        IPatientBL PatientBl = null;

        public ANSWebURLResponse GetANSUrl(string practiceName)
        {
            string result = string.Empty;
            ANSWebURLResponse objANSWebURLResponse = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));

                }

                QuivusLogger.Error("EprescriptionAdapterService EPrescriptionBLImpl :: Calling GetANSUrl On EprescriptionAdapterServiceClient");

                
                result = EprescriptionServiceClient.GetANSWebURL(practiceName);

                QuivusLogger.Info("Response Recieved From EprescriptionAdapterServiceClient Result::" + result);

                if (objANSWebURLResponse == null)
                {
                    objANSWebURLResponse = new ANSWebURLResponse();
                }
                if (!string.IsNullOrEmpty(result))
                {
                    objANSWebURLResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    objANSWebURLResponse.ResponseData = result;
                }
                else
                {
                    objANSWebURLResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return objANSWebURLResponse;
        }

        public string GetANSWebURL(string PracticeUserName)
        {
            throw new NotImplementedException();
        }

        public string SSOLoginToEprescription(string rcopia_user_external_id, string Action, string StartUpScreen, string skip_auth, string MAC)
        {
            throw new NotImplementedException();
        }

        public string UploadPatient(PatientInfoDTO patientInfoDTO)
        {
            String RcopiaId = String.Empty;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }


                RcopiaId = EprescriptionServiceClient.UploadPatient(patientInfoDTO, "", "");
            }
            catch
            {
                throw;
            }
            return RcopiaId;
        }

        //public bool UploadProblems(ProblemsInfoDTO ProblemInfoDTO)
        //{
        //    String UploadStatus = string.Empty;
        //    bool Result = false;
        //    try
        //    {
        //        if (EprescriptionServiceClient == null)
        //        {
        //            EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
        //        }


        //        UploadStatus = EprescriptionServiceClient.UploadProblems(ProblemInfoDTO, "", "");
        //        if (!String.IsNullOrEmpty(UploadStatus))
        //        {
        //            if (!UploadStatus.ToLower().StartsWith("error"))
        //            {
        //                Result = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return Result;
        //}

        public List<AllergyInfoDTO> DownloadAllergies(string patientUserID, string rcopiaID, string clinicBranchID)
        {
            List<AllergyInfoDTO> AllergiesList = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }
                AllergiesList = EprescriptionServiceClient.DownloadAllergies(patientUserID, rcopiaID, clinicBranchID);
            }
            catch
            {
                throw;
            }
            return AllergiesList;
        }

        public List<MedicationInfoDTO> DownloadMedications(string patientUserID, string rcopiaID, string clinicBranchID)
        {
            List<MedicationInfoDTO> Medications = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }
                Medications = EprescriptionServiceClient.DownloadMedications(patientUserID, rcopiaID, clinicBranchID);
            }
            catch
            {
                throw;
            }
            return Medications;
        }

        public List<ProblemsInfoDTO> DownloadProblems(string patientUserID, string rcopiaID, string ClinicBranchID)
        {
            List<ProblemsInfoDTO> Problems = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }
                Problems = EprescriptionServiceClient.DownloadProblems(patientUserID, rcopiaID, ClinicBranchID);
            }
            catch
            {
                throw;
            }
            return Problems;
        }

        public List<PrescriptionInfoDTO> DownloadPrescription(string patientUserID, string rcopiaID, string ClinicBranchID)
        {
            List<PrescriptionInfoDTO> Prescriptions = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }
                Prescriptions = EprescriptionServiceClient.DownloadPrescription(patientUserID, rcopiaID, ClinicBranchID);
            }
            catch
            {
                throw;
            }
            return Prescriptions;
        }

        public LoginCallResponse prepareLoginCallForEprescription(string rcopia_user_external_id, int PatientId)
        {
            string result = string.Empty;
            LoginCallResponse objANSWebURLResponse = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = new EprescriptionAdapterServiceClient();

                    //(EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));

                }


                string rcopiaId = string.Empty;
                // Load Patient Info By PAtient Id 
                if (PatientBl == null)
                {
                    PatientBl = new PatientBLImpl();
                }


                rcopiaId = PatientBl.LoadRcopiaIdByPatientId(PatientId);


                QuivusLogger.Error("EprescriptionAdapterService EPrescriptionBLImpl :: Calling prepareLoginCallForEprescription On EprescriptionAdapterServiceClient");

                if (!string.IsNullOrEmpty(rcopiaId))
                    result = EprescriptionServiceClient.prepareLoginCallForEprescription(rcopia_user_external_id, rcopiaId);

                else
                    result = EprescriptionServiceClient.prepareLoginCallForEprescription(rcopia_user_external_id, PatientId.ToString());

                QuivusLogger.Info("Response Recieved From EprescriptionAdapterServiceClient Result::" + result);

                if (objANSWebURLResponse == null)
                {
                    objANSWebURLResponse = new LoginCallResponse();
                }
                if (!string.IsNullOrEmpty(result))
                {
                    objANSWebURLResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    objANSWebURLResponse.ResponseData = result;
                }
                else
                {
                    objANSWebURLResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return objANSWebURLResponse;

        }

        /// <summary>
        /// Author Ismail
        /// Date 30 april 2011
        /// Method Will be used to load url for edit allergies
        /// </summary>
        /// <param name="rcopiaIDForUser"></param>
        /// <param name="rcopiaIDForPatient"></param>
        /// <param name="practiceName"></param>
        /// <param name="practiceUserName"></param>
        /// <returns></returns>
        public UrlResponse GetUrlForEditAllergies(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName, String practiceUserName)
        {
            string result = string.Empty;
            UrlResponse uRLResponse = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));

                }

                int UserId = 0;
                string rcopiaId = string.Empty;
                // Load Patient Info By PAtient Id 
                if (PatientBl == null)
                {
                    PatientBl = new PatientBLImpl();
                }

                try
                {
                    UserId = Int32.Parse(rcopiaIDForPatient);
                }
                catch
                {
                }
                if (UserId > 0)
                {
                    rcopiaId = PatientBl.LoadRcopiaIdByPatientId(UserId);
                }

                QuivusLogger.Error("EprescriptionAdapterService EPrescriptionBLImpl :: Calling GetUrlForEditAllergies On EprescriptionAdapterServiceClient");

                if (!string.IsNullOrEmpty(rcopiaId))
                    result = EprescriptionServiceClient.GetUrlForEditAllergies(rcopiaIDForUser, rcopiaId, practiceName, practiceUserName);

                else
                    result = EprescriptionServiceClient.GetUrlForEditAllergies(rcopiaIDForUser, rcopiaIDForPatient, practiceName, practiceUserName);

                QuivusLogger.Info("Response Recieved From EprescriptionAdapterServiceClient Result::" + result);

                if (uRLResponse == null)
                {
                    uRLResponse = new UrlResponse();
                }
                if (!string.IsNullOrEmpty(result))
                {
                    uRLResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    uRLResponse.AllergyUrl = result;
                }
                else
                {
                    uRLResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return uRLResponse;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// Method Used To Load prescription url
        /// </summary>
        /// <param name="rcopiaIDForUser"></param>
        /// <param name="rcopiaIDForPatient"></param>
        /// <param name="practiceName"></param>
        /// <param name="practiceUserName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public UrlResponse GetUrlForEditPrescription(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
        String practiceUserName)
        {
            string result = string.Empty;
            UrlResponse uRLResponse = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));

                }

                int UserId = 0;
                string rcopiaId = string.Empty;
                // Load Patient Info By PAtient Id 
                if (PatientBl == null)
                {
                    PatientBl = new PatientBLImpl();
                }

                try
                {
                    UserId = Int32.Parse(rcopiaIDForPatient);
                }
                catch
                {
                }
                if (UserId > 0)
                {
                    rcopiaId = PatientBl.LoadRcopiaIdByPatientId(UserId);
                }

                QuivusLogger.Error("EprescriptionAdapterService EPrescriptionBLImpl :: Calling GetUrlForEditPrescription On EprescriptionAdapterServiceClient");

                if (!String.IsNullOrEmpty(rcopiaId))
                    result = EprescriptionServiceClient.GetUrlForEditPrescription(rcopiaIDForUser, rcopiaId, practiceName, practiceUserName);

                else
                    result = EprescriptionServiceClient.GetUrlForEditPrescription(rcopiaIDForUser, rcopiaIDForPatient, practiceName, practiceUserName);

                QuivusLogger.Info("Response Recieved From GetUrlForEditPrescription Result::" + result);

                if (uRLResponse == null)
                {
                    uRLResponse = new UrlResponse();
                }
                if (!string.IsNullOrEmpty(result))
                {
                    uRLResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    uRLResponse.PrescriptionUrl = result;
                }
                else
                {
                    uRLResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return uRLResponse;
        }

        /// <summary>
        /// Author Ismail
        /// Date 30 April 2011
        /// Method will be used to load medication url from e-prescrption service :)
        /// </summary>
        /// <param name="rcopiaIDForUser"></param>
        /// <param name="rcopiaIDForPatient"></param>
        /// <param name="practiceName"></param>
        /// <param name="practiceUserName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public UrlResponse GetUrlForEditMedication(String rcopiaIDForUser, String rcopiaIDForPatient, String practiceName,
        String practiceUserName)
        {
            string result = string.Empty;
            UrlResponse uRLResponse = null;
            try
            {
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));

                }

                int UserId = 0;
                string rcopiaId = string.Empty;
                // Load Patient Info By PAtient Id 
                if (PatientBl == null)
                {
                    PatientBl = new PatientBLImpl();
                }

                try
                {
                    UserId = Int32.Parse(rcopiaIDForPatient);
                }
                catch
                {
                }
                if (UserId > 0)
                {
                    rcopiaId = PatientBl.LoadRcopiaIdByPatientId(UserId);
                }

                QuivusLogger.Error("EprescriptionAdapterService EPrescriptionBLImpl :: Calling GetUrlForEditMedication On EprescriptionAdapterServiceClient");

                if (!string.IsNullOrEmpty(rcopiaId))
                    result = EprescriptionServiceClient.GetUrlForEditMedication(rcopiaIDForUser, rcopiaId, practiceName, practiceUserName);
                else
                    result = EprescriptionServiceClient.GetUrlForEditMedication(rcopiaIDForUser, rcopiaIDForPatient, practiceName, practiceUserName);

                QuivusLogger.Info("Response Recieved From GetUrlForEditPrescription Result::" + result);

                if (uRLResponse == null)
                {
                    uRLResponse = new UrlResponse();
                }
                if (!string.IsNullOrEmpty(result))
                {
                    uRLResponse.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    uRLResponse.MedicationUrl = result;
                }
                else
                {
                    uRLResponse.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return uRLResponse;
        }

        public List<PatientInfoDTO> SynchronizePaitents(List<ExternalPatientInfoDTO> externalPatientList)
        {
            List<PatientInfoDTO> returnList = null;
            try
            {
                List<PatientInfoDTO> patientlst = null;
                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }

                if (externalPatientList != null)
                {

                    patientlst = new List<PatientInfoDTO>();

                    foreach (ExternalPatientInfoDTO obj in externalPatientList)
                    {
                        if (obj != null)
                        {
                            PatientInfoDTO patientInfo = new PatientInfoDTO();
                            patientInfo.FirstName = obj.FirstName;
                            patientInfo.LastName = obj.LastName;

                            patientInfo.DOB = obj.DateOfBirth.ToString();

                            patientInfo.Address = obj.Address;
                            patientInfo.City = obj.City;
                            patientInfo.State = obj.State;
                            patientInfo.Zip = obj.Zip;

                            patientInfo.ExternalID = obj.PatientId.ToString();

                            patientlst.Add(patientInfo);
                        }
                    }
                }

                if (patientlst != null)
                    returnList = EprescriptionServiceClient.SynchronizePaitents(patientlst, string.Empty, string.Empty);
            }
            catch
            {
                throw;
            }
            return returnList;
        }

        public QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse SyncEPrescription(int patientUserID, string rcopiaID, int clinicBranchID, int VisitId)
        {
            QuivusManager.ServiceResponse.ServiceCustomResponse.ServiceResponse response = null;

            try
            {
                // Load Patient Info By PAtient Id 
                if (PatientBl == null)
                {
                    PatientBl = new PatientBLImpl();
                }

                string rcopiaId = PatientBl.LoadRcopiaIdByPatientId(patientUserID);

                SyncAllergies(patientUserID, clinicBranchID, VisitId, rcopiaId);

                SyncMedications(patientUserID, clinicBranchID, VisitId, rcopiaId);

                SyncPrescription(patientUserID, rcopiaId, clinicBranchID, VisitId);

                SyncProblems(patientUserID, rcopiaId, clinicBranchID, VisitId);

                if (response == null)
                {
                    response = new ServiceResponse.ServiceCustomResponse.ServiceResponse();
                    response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                }
            }
            catch (Exception exp)
            {
                //  throw; // Just Log This Exception Not to break the flow of App By Ismail :)
                QuivusLogger.Error(exp);
            }
            return response;
        }

        private void SyncProblems(int patientUserID, string rcopiaID, int clinicBranchID, int VisitId)
        {
            try
            {
                List<ProblemsInfoDTO> Problems = DownloadProblems(patientUserID.ToString(), rcopiaID, clinicBranchID.ToString());

                //if (Problems == null)
                //    Problems = new List<ProblemsInfoDTO>();

                //Problems.Add(new ProblemsInfoDTO { RcopiaID = rcopiaID });
                if (Problems != null && Problems.Count > 0)
                {
                    List<PatientDiagnosisDTO> diagnosisList = ConvertPatientDiagnosisDTO(Problems, patientUserID, VisitId);

                    if (QuickInputBl == null)
                    {
                        QuickInputBl = new QuickInputBLImpl();
                    }

                    QuickInputBl.ManagePatientDiagnosisByRcopia(diagnosisList); // Call To Update problems & Diagnosis
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.StackTrace);
            }
        }

        private void SyncPrescription(int patientUserID, string rcopiaID, int clinicBranchID, int VisitId)
        {
            try
            {
                List<PrescriptionInfoDTO> Prescriptions = DownloadPrescription(patientUserID.ToString(), rcopiaID, clinicBranchID.ToString());

                //if (Prescriptions == null)
                //    Prescriptions = new List<PrescriptionInfoDTO>();

              //  Prescriptions.Add(new PrescriptionInfoDTO { PatientExternalID = patientUserID.ToString(), RcopiaID = rcopiaID,medicationInfoDTO = new MedicationInfoDTO() });
                if (Prescriptions != null && Prescriptions.Count > 0)
                {
                    List<PrescriptionMedicationDTO> PrescriptionMedicationlist = ConvertPrescriptionMedicationDTO(Prescriptions, patientUserID, VisitId);

                    if (QuickInputBl == null)
                    {
                        QuickInputBl = new QuickInputBLImpl();
                    }

                    QuickInputBl.ManagePatientPrescriptionMedication(PrescriptionMedicationlist); // Call To Update Medication Prescription
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.StackTrace);
            }
        }

        private void SyncMedications(int patientUserID, int clinicBranchID, int VisitId, string rcopiaId)
        {
            try
            {
                List<MedicationInfoDTO> Medications = DownloadMedications(string.Empty, rcopiaId, clinicBranchID.ToString());

                List<MedicationDTO> MedicationList = ConvertMedicationInfoTOMedicationDTO(Medications, patientUserID, VisitId);

                if (MedicationList != null && MedicationList.Count > 0)
                {
                    if (QuickInputBl == null)
                    {
                        QuickInputBl = new QuickInputBLImpl();
                    }

                PatientMedicationReponse mediResp=    QuickInputBl.ManagePatientMedicationList(MedicationList);

                    /* foreach (MedicationDTO obj in MedicationList)
                     {
                         QuickInputBl.ManagePatientMedication(obj);
                     }*/
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.StackTrace);
            }
        }

        private void SyncAllergies(int patientUserID, int clinicBranchID, int VisitId, string rcopiaId)
        {
            try
            {
                List<AllergyInfoDTO> AllergiesList = DownloadAllergies(string.Empty, rcopiaId, clinicBranchID.ToString()); // We Use Loaded rcopia id as it front send null & we send user empty as dr first takes either user id or rcopia id ....by user id it not working so this chapii :)

                List<AllergyDTO> allergyDTOList = ConvertAllergyInfoDTOToAllergyDTO(AllergiesList, patientUserID, VisitId);

                // Now Save this allergy listy 
                if (allergyDTOList != null && allergyDTOList.Count > 0)
                {
                    if (QuickInputBl == null)
                    {
                        QuickInputBl = new QuickInputBLImpl();
                    }

                    QuickInputBl.ManagePatientAllergyList(allergyDTOList);

                    /* foreach (AllergyDTO obj in allergyDTOList)
                     {
                         //if(String.IsNullOrEmpty(obj.Reaction))
                         //    obj.Reaction ="NA"; // Todo: validate it , it needs to be replaced with correct reactions
                         QuickInputBl.ManagePatientAllergy(obj);
                     }*/
                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.StackTrace);
            }
        }

        /// <summary>
        /// Author Ismail
        /// Date 21 July 2011
        /// </summary>
        /// <param name="Problems"></param>
        /// <param name="patientUserID"></param>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        private List<PatientDiagnosisDTO> ConvertPatientDiagnosisDTO(List<ProblemsInfoDTO> Problems, int patientUserID, int VisitId)
        {
            List<PatientDiagnosisDTO> DiagnosisList = new List<PatientDiagnosisDTO>();

            if (Problems != null)
            {


                foreach (ProblemsInfoDTO obj in Problems)
                {
                   
                    PatientDiagnosisDTO diagnosisObj = new PatientDiagnosisDTO();
                    diagnosisObj.ICDCodeName = obj.Code;
                    diagnosisObj.ICDCodeDescription = obj.Description;
                    diagnosisObj.RCOPIAID = obj.RcopiaID;

                    if (!String.IsNullOrEmpty(obj.IsDeleted))
                    {
                        if (obj.IsDeleted.ToLower().Equals(ProjectConstant.SELECTION_Y))
                        {
                            diagnosisObj.IsDeleted = true;
                        }
                        else
                        {
                            diagnosisObj.IsDeleted = false;
                        }
                    }
                    try
                    {
                        diagnosisObj.CreatedAt = DateTime.Parse(obj.LastModifiedBy);
                        diagnosisObj.ModifiedAt = DateTime.Parse(obj.LastModifiedBy);

                        
                    }
                    catch
                    { 
                    }
                    DiagnosisList.Add(diagnosisObj);
                    //  diagnosisObj.PatientDiagnosisID = obj.
                }
            }
            return DiagnosisList;
        }

        /// <summary>
        /// Author Ismail
        /// Date 21 July 2011
        /// </summary>
        /// <param name="Prescriptions"></param>
        /// <param name="patientUserID"></param>
        /// <param name="VisitId"></param>
        /// <returns></returns>
        private List<PrescriptionMedicationDTO> ConvertPrescriptionMedicationDTO(List<PrescriptionInfoDTO> Prescriptions, int patientUserID, int VisitId)
        {
            List<PrescriptionMedicationDTO> PrescriptionList = new List<PrescriptionMedicationDTO>();

            if (Prescriptions != null)
            {
                foreach (PrescriptionInfoDTO obj in Prescriptions)
                {
                    if (obj != null)
                    {
                        try
                        {
                            PrescriptionMedicationDTO prescriptionObj = new PrescriptionMedicationDTO();
                            prescriptionObj.VisitID = VisitId;
                            prescriptionObj.PatientID = patientUserID;
                            try
                            {
                                prescriptionObj.PatientPrescriptionID = Int32.Parse(obj.medicationInfoDTO.PatientMedicationID);
                            }
                            catch
                            {
                            }
                            if (obj.medicationInfoDTO != null)
                            {
                                prescriptionObj.RCOPIAID = obj.medicationInfoDTO.RCOPIAID;
                                prescriptionObj.NDCID = obj.medicationInfoDTO.NDCID;
                                prescriptionObj.FirstDataBankMedID = obj.medicationInfoDTO.FirstDataBankMedID;
                                prescriptionObj.BrandName = obj.medicationInfoDTO.BrandName;
                                prescriptionObj.GenericName = obj.medicationInfoDTO.GenericName;
                                prescriptionObj.Form = obj.medicationInfoDTO.Form;
                                prescriptionObj.Strength = obj.medicationInfoDTO.Strength;
                                prescriptionObj.Action = obj.medicationInfoDTO.Action;
                                prescriptionObj.Dose = obj.medicationInfoDTO.Dose;
                                prescriptionObj.DoseUnit = obj.medicationInfoDTO.DoseUnit;

                                prescriptionObj.Route = obj.medicationInfoDTO.Route;
                                prescriptionObj.DoseTiming = obj.medicationInfoDTO.DoseTiming;
                                prescriptionObj.DoseOther = obj.medicationInfoDTO.DoseOther;
                                prescriptionObj.Duration = obj.medicationInfoDTO.Duration;
                                prescriptionObj.Quantity = obj.medicationInfoDTO.Quantity;
                                prescriptionObj.QuantityUnit = obj.medicationInfoDTO.QuantityUnit;
                                prescriptionObj.Refills = obj.medicationInfoDTO.Refills;
                                prescriptionObj.IsSubstitutionPermitted = obj.medicationInfoDTO.IsSubstitutionPermitted;
                                prescriptionObj.OtherNotes = obj.medicationInfoDTO.OtherNotes;

                                prescriptionObj.PatientNotes = obj.medicationInfoDTO.PatientNotes;
                                prescriptionObj.Comments = obj.medicationInfoDTO.OtherNotes;
                                try
                                {
                                    prescriptionObj.CreatedAt = DateTime.Parse(obj.medicationInfoDTO.LastModifiedDate);
                                    prescriptionObj.ModifiedAt = DateTime.Parse(obj.medicationInfoDTO.LastModifiedDate);
                                    prescriptionObj.CreatedBy = Int32.Parse(obj.medicationInfoDTO.LastModifiedBy);
                                    prescriptionObj.ModifiedBy = Int32.Parse(obj.medicationInfoDTO.LastModifiedBy);

                                    if (!String.IsNullOrEmpty(obj.medicationInfoDTO.IsDeleted))
                                    {
                                        if (obj.medicationInfoDTO.IsDeleted.Equals(ProjectConstant.SELECTION_Y))
                                        {
                                            prescriptionObj.IsDeleted = true;
                                        }
                                        else
                                        {
                                            prescriptionObj.IsDeleted = false;
                                        }
                                    }
                                    
                                }
                                catch { }
                                prescriptionObj.Status = obj.medicationInfoDTO.Status;
                                prescriptionObj.NoteSubject = obj.medicationInfoDTO.PatientNotes;

                                PrescriptionList.Add(prescriptionObj);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return PrescriptionList;
        }

        private static List<MedicationDTO> ConvertMedicationInfoTOMedicationDTO(List<MedicationInfoDTO> Medications, int patientUserID, int VisitId)
        {
            List<MedicationDTO> MedicationList = new List<MedicationDTO>();

            if (Medications != null)
            {
                if (MedicationList == null)
                    MedicationList = new List<MedicationDTO>();

                foreach (MedicationInfoDTO obj in Medications)
                {
                    if (obj != null)
                    {
                        try
                        {
                            MedicationDTO medicationObj = new MedicationDTO();
                            //   obj.BrandName
                            medicationObj.Action = obj.Action;
                            medicationObj.BrandName = obj.BrandName;
                            medicationObj.Comments = obj.Comments;

                            medicationObj.Dose = obj.Dose;
                            medicationObj.DoseOther = obj.DoseOther;
                            medicationObj.DoseTiming = obj.DoseTiming;
                            medicationObj.DoseUnit = obj.DoseUnit;
                            medicationObj.Duration = obj.Duration;
                            medicationObj.FirstDataBankMedID = obj.FirstDataBankMedID;
                            medicationObj.Form = obj.Form;
                            medicationObj.GenericName = obj.GenericName;
                            medicationObj.IsSubstitutionPermitted = obj.IsSubstitutionPermitted;

                            medicationObj.NDCID = obj.NDCID;
                            medicationObj.OtherNotes = obj.OtherNotes;


                            medicationObj.PatientNotes = obj.PatientNotes;
                            medicationObj.Quantity = obj.Quantity;
                            medicationObj.QuantityUnit = obj.QuantityUnit;
                            medicationObj.RCOPIAID = obj.RCOPIAID;
                            medicationObj.Refills = obj.Refills;
                            medicationObj.Route = obj.Route;
                            medicationObj.Status = obj.Status;
                            medicationObj.Strength = obj.Strength;
                            medicationObj.VisitDate = obj.VisitDate;
                            //Load Visit Id &  Then Save It
                            //  medicationObj.VisitId = VisitId;


                            if (string.IsNullOrEmpty(obj.PatientExternalID))
                            {
                                try
                                {
                                    medicationObj.PatientId = Int32.Parse(obj.PatientExternalID); //Compulsory

                                }
                                catch
                                {
                                    medicationObj.PatientId = patientUserID;
                                }
                            }
                            else
                            {
                                medicationObj.PatientId = patientUserID;
                            }

                            //if (string.IsNullOrEmpty(obj.PatientMedicationID))
                            //{
                            //    try
                            //    {
                            //        medicationObj.PatientMedicationID = Int32.Parse(obj.PatientMedicationID); //Compulsory

                            //    }
                            //    catch
                            //    {
                            //        medicationObj.PatientMedicationID  = patientUserID;
                            //    }
                            //}
                            //else
                            //{
                            //    medicationObj.PatientMedicationID = patientUserID;
                            //}


                            if (string.IsNullOrEmpty(obj.LastModifiedDate))
                            {
                                try
                                {
                                    medicationObj.CreatedAt = DateTime.Parse(obj.LastModifiedDate); //Compulsory
                                    medicationObj.ModifiedAt = DateTime.Parse(obj.LastModifiedDate); //Compulsory
                                }
                                catch
                                {
                                    medicationObj.CreatedAt = DateTime.Now; //Compulsory
                                    medicationObj.ModifiedAt = DateTime.Now;
                                }
                            }
                            else
                            {
                                medicationObj.PatientMedicationID = patientUserID;
                            }

                            if (! String.IsNullOrEmpty(obj.IsDeleted))
                            {
                                if (obj.IsDeleted.Equals(ProjectConstant.SELECTION_Y))
                                {
                                    medicationObj.IsDeleted = true;
                                }
                                else
                                {
                                    medicationObj.IsDeleted = false;
                                }
                            }

                            MedicationList.Add(medicationObj);
                        }
                        catch
                        {
                            // By Pass :)
                        }
                    }
                }
            }
            return MedicationList;
        }

        private static List<AllergyDTO> ConvertAllergyInfoDTOToAllergyDTO(List<AllergyInfoDTO> AllergiesList, int patientUserID, int VisitId)
        {
            List<AllergyDTO> allergyDTOList = null;

            if (AllergiesList != null)
            {
                if (allergyDTOList == null)
                    allergyDTOList = new List<AllergyDTO>();

                foreach (AllergyInfoDTO obj in AllergiesList)
                {
                    if (obj != null)
                    {
                        AllergyDTO allerygObj = new AllergyDTO();

                        allerygObj.AllergyName = obj.AllergyName;

                        allerygObj.IsActive = ProjectConstant.SELECTION_TRUE;
                        //allerygObj.
                        if (string.IsNullOrEmpty(obj.Reaction))
                            allerygObj.Reaction = "N/A"; //Compulsory
                        else
                            allerygObj.Reaction = obj.Reaction; //Compulsory

                        allerygObj.Drug = obj.AllergyDrug;
                        allerygObj.VisitId = VisitId;
                        if (string.IsNullOrEmpty(obj.LastModifiedBy))
                        {
                            try
                            {
                                allerygObj.CreatedBy = Int32.Parse(obj.LastModifiedBy); //Compulsory
                                allerygObj.ModifiedBy = Int32.Parse(obj.LastModifiedBy); //Compulsory
                            }
                            catch
                            {
                                allerygObj.CreatedBy = patientUserID;
                                allerygObj.ModifiedBy = patientUserID;
                            }
                        }
                        else
                        {
                            allerygObj.CreatedBy = patientUserID;
                            allerygObj.ModifiedBy = patientUserID;
                        }

                        try
                        {
                            if (!String.IsNullOrEmpty(obj.RcopiaID))
                            {
                                allerygObj.RCOPIAID = Int32.Parse(obj.RcopiaID);
                            }


                            if (!String.IsNullOrEmpty(obj.OnsetDate))
                            {
                                try
                                {
                                    //allerygObj.OnSet = DateTime.Parse(obj.OnsetDate); //Compulsory
                                    allerygObj.OnSet = obj.OnsetDate; //Compulsory
                                }
                                catch
                                {
                                    allerygObj.OnSet = DateTime.Now.ToString(); //Compulsory
                                }
                            }
                            else
                            {
                                allerygObj.OnSet = DateTime.Now.ToString(); //Compulsory
                            }

                            if (!String.IsNullOrEmpty(obj.LastModifiedDate))
                            {
                                try
                                {
                                    allerygObj.CreatedAt = DateTime.Parse(obj.LastModifiedDate); //Compulsory
                                    allerygObj.ModifiedAt = DateTime.Parse(obj.LastModifiedDate); // optional
                                }
                                catch
                                {
                                    allerygObj.CreatedAt = DateTime.Now; //Compulsory
                                    allerygObj.ModifiedAt = DateTime.Now;
                                }
                            }
                            {
                                allerygObj.CreatedAt = DateTime.Now; //Compulsory
                                allerygObj.ModifiedAt = DateTime.Now;
                            }

                            allerygObj.IsDeleted = obj.IsDeleted;

                            allergyDTOList.Add(allerygObj);
                        }

                        catch
                        {
                            // ByPass EXP :)
                        }
                    }
                }
            }

            return allergyDTOList;
        }




        public NotificationResponse GetNotificationsForProvider(string ExternalId)
        {
            NotificationResponse Response = null;
            List<NotificationsInfoDTO> notificationLst = null;
            try
            {

                if (EprescriptionServiceClient == null)
                {
                    EprescriptionServiceClient = (EprescriptionAdapterServiceClient)ServiceReferenceFactory.GetServiceReference(typeof(EprescriptionAdapterServiceClient));
                }

                notificationLst = EprescriptionServiceClient.GetNotificationsForProvider(ExternalId);

                QuivusLogger.Info("Response Recieved From EprescriptionAdapterServiceClient notificationLst::" + notificationLst);

                if (Response == null)
                {
                    Response = new NotificationResponse();
                }
                if (notificationLst != null)
                {
                    Response.ResponseStatus = ProjectConstant.RESPONSE_SUCCESS_STATUS;
                    Response.ResponseData = notificationLst;
                }
                else
                {
                    Response.ResponseStatus = ProjectConstant.RESPONSE_FAILURE_STATUS;
                }

            }
            catch
            {
                throw;
            }
            return Response;

        }

        //public LoginCallResponse prepareLoginCallForEprescription(string rcopia_user_external_id)
        //{
        //   // throw new NotImplementedException();
        //    return null;
        //}

        //public ServiceResponse.ServiceCustomResponse.ServiceResponse SyncEPrescription(int patientUserID, string rcopiaID, int clinicBranchID)
        //{
        //   // throw new NotImplementedException(
        //    return null;
        //}
    }
}
