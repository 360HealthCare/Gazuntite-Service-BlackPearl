using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quivus.Model.QuickInput.ComprehensiveHistory.Medical;
using Quivus.Model.TypeDTOs;
using Quivus.Utility.Logger;
using Visit.Business.Validators.QuickInput.PMH.ComprehensiveHistory;
using Quivus.Utility.CustomException;
using Visit.DataAccess.DAO;
using Quivus.Utility.Factory;
using Quivus.Model.QuickInput.MainQuickInput;
using Quivus.Model.QuickInput.ComprehensiveHistory.Family;
using Quivus.Model.QuickInput.Vitals;
using Visit.Business.Validators.QuickInput.Physical.Vital;
using Quivus.Model.QuickInput.Labs;
using Visit.Business.Validators.QuickInput.Physical.Labs;
using Visit.Business.Validators;
using Quivus.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Quivus.Model.QuickInput;
using Quivus.Model.QuickInput.ROS;
using Visit.Business.Validators.QuickInput.Physical.ROS;
using Quivus.Model.QuickInput.HPI;
using Visit.Business.Validators.QuickInput.Physical.HPI;
using Quivus.Model.QuickInput.ComprehensiveHistory.Social;
using Quivus.Model.QuickInput.Physical;
using Visit.Business.Validators.QuickInput.Physical.Physicals;
using Quivus.Model.QuickInput.Diagnosis;
using Visit.Business.Validators.QuickInput.Assessment;
using Quivus.Model.QuickInput.Recommendation;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.User.Patient;
using Visit.Business.Validators.QuickInput.PMH.Allergy;
using Visit.Business.Validators.QuickInput.PMH.ChronicMedication;
using Quivus.Model.QuickInput.Procedure;
using Quivus.Model.QuickInput.Medication;
using Visit.Business.Validators.QuickInput.PMH.Medication;
using Quivus.Model.QuickInput.FollowUp;
using Quivus.Model.QuickInput.Dictation;
using Quivus.Model.QuickInput.Notes;
using QuivusUtility.Utilities;
using QuivusUtility.ProjectConstants;
using Quivus.Model.QuickInput.Prescription;
using Visit.Business.Validators.QuickInput.Physical.Prescription;
using Quivus.Model.Visit;


namespace Visit.Business.BL.QuickInput.Impl
{
    public class QuickInputManagerImpl : IQuickInputManager
    {
        QuickInputDAO quickInputDAO = null;

        VisitDAO visitDAO = null;


        ViewBackgroundDAO vbDAO = null;
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
        public bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId)
        {
            bool Status = false;
            bool flag = true;
            try
            {
                QuivusLogger.Info("Calling ManageVisitWithData PARAM :: QuickInput " + QuickInput + " managedByUserId " + managedByUserId);

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Validation Start On Quick Input");

                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + QuickInput.VisitId.ToString() + " and PatientID: " + QuickInput.PatientId.ToString() + "UserTypeId::" + UserTypeId);

                if (VisitValidator.ValidateVisitComplete(QuickInput, managedByUserId, ref sb, flag))
                {
                    QuivusLogger.Info("Start Quick Input Transaction");
                    QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + QuickInput.VisitId.ToString() + " and PatientID: " + QuickInput.PatientId.ToString());

                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    int zeroVisitId = visitDAO.CheckZeroVisitExists(QuickInput.PatientId);

                    if (zeroVisitId == 0)
                    {
                        VisitDTO vDTO = new VisitDTO();

                        CreateZeroVisitDTO(QuickInput.PatientId,QuickInput.Visit.VisitDate,QuickInput.ClinicBranchId, managedByUserId, UserTypeId, vDTO);

                        visitDAO.CreateZeroVisit(vDTO, managedByUserId);
                    }
                   

                    Status = ManageQuickInputTransaction(QuickInput, managedByUserId, UserTypeId);

                    if (Status) //cOmmented Out New Check Introduce
                    //  if (Status && IsFinal) // As QI Save/Update Call Is Breakdown so If IsFinal is Set True Then Futhur Proceed
                    {

                       
                        /* Added By Ismail As Visit is saved (1 bit is set false) before saving quick input.
                         * After Saving QI (quick input) successfully, mark that Bit to True :)
                         */
                        visitDAO.MarkValidDataExists(QuickInput.VisitId);

                        /*
                         After Each Save/Update Call Calculate Score As it will update Visit Score in DB
                         */
                        visitDAO.CalculateVisitScore(QuickInput.VisitId);
                    }
                    QuivusLogger.Info("End Quick Input Transaction");
                }
                else
                {
                    QuivusLogger.Info("Validation Fail On Quick Input");
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return Status;
        }

        private static void CreateZeroVisitDTO(int patientID, DateTime visitDtate, int cbId, int managedByUserId, int UserTypeId, VisitDTO vDTO)
        {
            vDTO.PatientId = patientID;
            vDTO.ClinicBranch = new Quivus.Model.Clinic.ClinicBranchDTO();

            vDTO.ClinicBranch.ClinicBranchIdSeq = cbId;

            vDTO.VisitDate = visitDtate;
            vDTO.UserId = managedByUserId;
            vDTO.UserType = new Quivus.Model.TypeDTOs.UserTypeDTO();
            vDTO.UserType.UserTypeId = UserTypeId;

            vDTO.VisitStatus = "UnSigned";
            vDTO.RecGuid = new Guid();
            vDTO.OwnerId = patientID;
            vDTO.OwnerUserType = new Quivus.Model.TypeDTOs.UserTypeDTO();
            vDTO.OwnerUserType.UserTypeId = (int)ProjectConstant.USER_TYPE.Patient;
            vDTO.IsVirtualVisit = false;

            vDTO.CreatedBy = managedByUserId;
        }

        /// <summary>
        /// Author Ismail
        /// Date 12 july 2011
        /// create/ save visit and save the relative infor like immunization and chronic medication
        /// used only in case of operation performed in Quick Input Container
        /// This is overloaded method it save PMH,Physical & Assessment One By One In Last call front end make IsFinal True to
        /// Indicate that this is last call & on last call we will update visit status & calculate visit score
        /// </summary>
        /// <param name="visitDTO"></param>
        /// <param name="immunizationList"></param>
        /// <param name="chronicMedicationList"></param>
        /// <returns></returns>
        public bool ManageQuickInputData(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId, bool IsFinal)
        {
            bool Status = false;
            bool flag = true;
            try
            {
                QuivusLogger.Info("Calling ManageVisitWithData PARAM :: QuickInput " + QuickInput + " managedByUserId " + managedByUserId);

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Validation Start On Quick Input");

                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + QuickInput.VisitId.ToString() + " and PatientID: " + QuickInput.PatientId.ToString() + "UserTypeId::" + UserTypeId);

                if (VisitValidator.ValidateVisitComplete(QuickInput, managedByUserId, ref sb, flag))
                {
                    QuivusLogger.Info("Start Quick Input Transaction");
                    QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + QuickInput.VisitId.ToString() + " and PatientID: " + QuickInput.PatientId.ToString());
                    Status = ManageQuickInputTransaction(QuickInput, managedByUserId, UserTypeId);

                    if (Status && IsFinal) // As QI Save/Update Call Is Breakdown so If IsFinal is Set True Then Futhur Proceed
                    {

                        if (visitDAO == null)
                        {
                            visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                        }
                        /* Added By Ismail As Visit is saved (1 bit is set false) before saving quick input.
                         * After Saving QI (quick input) successfully, mark that Bit to True :)
                         */
                        visitDAO.MarkValidDataExists(QuickInput.VisitId);

                        /*
                         After Each Save/Update Call Calculate Score As it will update Visit Score in DB
                         */
                        visitDAO.CalculateVisitScore(QuickInput.VisitId);
                    }
                    QuivusLogger.Info("End Quick Input Transaction");
                }
                else
                {
                    QuivusLogger.Info("Validation Fail On Quick Input");
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return Status;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 15 March 2011
        /// This Method Will manage i.e Insert/Update Quick Input Sections 
        /// </summary>
        /// <param name="QuickInput"></param>
        /// <param name="managedByUserId"></param>
        private bool ManageQuickInputTransaction(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId)
        {
            bool Status = false;
            //  Database db = DatabaseFactory.CreateDatabase(); // Load default DB 
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();
            try
            {
                if (QuickInput != null)
                {
                    # region PMH Saving :)

                    ManageQuickInputPMHSection(QuickInput, managedByUserId, UserTypeId);     // Manage PMH Section :)

                    #endregion End PMH Saving

                    # region Physical Saving

                    ManageQuickInputPhysical(QuickInput, managedByUserId);      // Manage Physical Section :)

                    #endregion End Physical Section

                    #region ASSESSMENT Saving
                    ManageQuickInputAssessment(QuickInput, managedByUserId);

                    #endregion


                    #region Module notes and Dictation

                    if (QuickInput.ModuleNotes != null && QuickInput.ModuleNotes.Count > 0)
                        ManageModuleNotes(QuickInput.ModuleNotes, managedByUserId, QuickInput.VisitId, QuickInput.ActionDate);
                    //   ManageModuleNotes(QuickInput.ModuleNotes);

                    if (QuickInput.ModuleDictations != null && QuickInput.ModuleDictations.Count > 0)
                        ManageModuleDictation(QuickInput.ModuleDictations, managedByUserId, QuickInput.ClinicBranchId, QuickInput.VisitId);

                    #endregion


                    Status = true;
                    // transaction.Commit();    // commit the transaction
                }
            }
            catch
            {
                //  transaction.Rollback();  // rollback the transaction
                throw;
            }
            finally
            {
                //if (connection.State == ConnectionState.Open)
                //    connection.Close();
            }
            //}
            return Status;
        }

        private void ManageQuickInputAssessment(QuickInputDTO QuickInput, int managedByUserId)
        {
            try
            {
                if (QuickInput.Assessment != null)
                {
                    QuivusLogger.Info("Start Assessment Saving Section");

                    if (QuickInput.Assessment.Diagnosis != null && QuickInput.Assessment.Diagnosis.Count > 0)
                    {
                        ManagePatientDiagnosis(QuickInput.Assessment.Diagnosis, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ActionDate);
                    }

                    if (QuickInput.Assessment.Recommendation != null)
                    {
                        ManagePatientRecommendation(QuickInput.Assessment.Recommendation, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ClinicBranchId, QuickInput.ActionDate); // For time beign it is skipped IT-4
                    }

                    if (QuickInput.Assessment.PatientFollowUp != null)
                    {
                        ManagePatientFollowUp(QuickInput.Assessment.PatientFollowUp, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ActionDate);
                    }

                    if (QuickInput.Assessment.PatientFollowupInstruction != null)
                    {
                        ManagePatientFollowupInstruction(QuickInput.Assessment.PatientFollowupInstruction, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ClinicBranchId, QuickInput.ActionDate);
                    }

                    if (QuickInput.Assessment.Procedure != null && QuickInput.Assessment.Procedure.Count > 0)
                    {
                        QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + QuickInput.VisitId.ToString() + " and PatientID: " + QuickInput.PatientId.ToString());
                        ManagePatientProcedure(QuickInput.Assessment.Procedure, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ClinicBranchId, QuickInput.ActionDate);
                    }


                    QuivusLogger.Info("End Assessment Saving Section");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 15 Marh 2011
        /// This MEthod will manage i.e Insert/Update Physical Sections
        /// </summary>
        /// <param name="QuickInput"></param>
        private void ManageQuickInputPhysical(QuickInputDTO QuickInput, int managedByUserId)
        {
            try
            {
                if (QuickInput.Physical != null)
                {
                    QuivusLogger.Info("In Physical Saving Section");

                    if (QuickInput.Physical.Vitals != null)
                    {
                        QuivusLogger.Info("Start Saving Patient Vitals");
                        ManagePatientVitals(QuickInput.Physical.Vitals, managedByUserId, QuickInput.VisitId, QuickInput.ActionDate);

                        //QuivusLogger.Info("Start Saving Patient Vitals");
                    }

                    //Auhtor Mansoor
                    //Date 21 Mar 2011
                    if (QuickInput.Physical.HPI_CC != null)
                    {
                        QuivusLogger.Info("Start Saving Patient HPI");

                        if (QuickInput.Physical.HPI_CC != null)
                            ManagePatientHPI(QuickInput.Physical.HPI_CC, managedByUserId, QuickInput.PatientId, QuickInput.VisitId, QuickInput.ClinicBranchId, QuickInput.ActionDate);

                        //QuivusLogger.Info("End Saving Patient Vitals");
                    }

                    if (QuickInput.Physical.Labs != null)
                    {
                        QuivusLogger.Info("Start Saving Patient Labs ");

                        if (QuickInput.Physical.Labs != null && QuickInput.Physical.Labs.Count > 0)
                            QuickInput.Physical.Labs.ForEach(obj => ManagePatientLabs(obj, managedByUserId, QuickInput.VisitId, QuickInput.ActionDate));

                        QuivusLogger.Info("End Saving Patient Labs ");
                    }

                    if (QuickInput.Physical.InHouseLabs != null)
                    {
                        QuivusLogger.Info("Start Saving Patient InHouseLabs ");

                        if (QuickInput.Physical.InHouseLabs != null && QuickInput.Physical.InHouseLabs.Count > 0)
                            ManagePatientInHouseLabs(QuickInput.Physical.InHouseLabs, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ActionDate);

                        QuivusLogger.Info("End Saving Patient InHouseLabs ");
                    }



                    if (QuickInput.Physical.ReviewOfSystem != null)
                    {
                        QuivusLogger.Info("Start Saving Patient ROS ");

                        if (QuickInput.Physical.ReviewOfSystem != null && QuickInput.Physical.ReviewOfSystem.Count > 0)
                            ManagePatientROS(QuickInput.Physical.ReviewOfSystem, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ActionDate);

                        QuivusLogger.Info("End Saving Patient ROS ");
                    }

                    if (QuickInput.Physical.Physicals != null)
                    {
                        QuivusLogger.Info("Start Saving Patient Physicals ");

                        if (QuickInput.Physical.Physicals != null && QuickInput.Physical.Physicals.Count > 0)
                            ManagePatientPhysical(QuickInput.Physical.Physicals, managedByUserId, QuickInput.VisitId, QuickInput.PatientId, QuickInput.ActionDate);

                        QuivusLogger.Info("End Saving Patient Physicals ");
                    }

                    QuivusLogger.Info("End Physical Saving Section");
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Date 15 Marh 2011
        /// This MEthod will manage i.e Insert/Update PMH Sections
        /// </summary>
        /// <param name="QuickInput"></param>
        private void ManageQuickInputPMHSection(QuickInputDTO QuickInput, int managedByUserId, int UserTypeId)
        {
            try
            {
                if (QuickInput.PMH != null)
                {
                    QuivusLogger.Info("In PMH Saving Section");
                    # region Comprehensive History Saving :)
                    if (QuickInput.PMH.ComprehensiveHistory != null)
                    {
                        if (QuickInput.PMH.ComprehensiveHistory.MedicalHistory != null && QuickInput.PMH.ComprehensiveHistory.MedicalHistory.Count > 0)
                        {
                            QuickInput.PMH.ComprehensiveHistory.MedicalHistory.ForEach(obj => ManagePatientMedicalHistory(obj, managedByUserId, QuickInput.VisitId, QuickInput.ActionDate));
                        }
                        if (QuickInput.PMH.ComprehensiveHistory.FamilyHistory != null && QuickInput.PMH.ComprehensiveHistory.FamilyHistory.Count > 0)
                        {
                            QuickInput.PMH.ComprehensiveHistory.FamilyHistory.ForEach(obj => ManagePatientFamilyHistory(obj, managedByUserId, QuickInput.VisitId, QuickInput.ActionDate, UserTypeId, QuickInput.ClinicBranchId));
                        }
                        if (QuickInput.PMH.ComprehensiveHistory.SocialHistory != null && QuickInput.PMH.ComprehensiveHistory.SocialHistory.Count > 0)
                        {
                            QuickInput.PMH.ComprehensiveHistory.SocialHistory.ForEach(obj => ManagePatientSocialHistory(obj, managedByUserId, QuickInput.PatientId, QuickInput.VisitId, QuickInput.ActionDate));
                        }

                    }
                    # endregion End Comprehensive History Saving :)

                    # region Immunization History Saving :)
                    if (QuickInput.PMH.Immunization != null && QuickInput.PMH.Immunization.Count > 0)
                    {
                        ManagePatientImmunization(QuickInput.PMH.Immunization, managedByUserId, QuickInput.VisitId, QuickInput.ActionDate);
                    }
                    # endregion End Immunization History Saving :)
                    QuivusLogger.Info("End PMH Saving Section");
                }
            }
            catch
            {
                throw;
            }

        }




        # region PMH Section

        /// <summary>
        /// Author Ismail
        /// Date 26 March 2011
        /// save patient allergies
        /// </summary>
        /// <param name="PatientAllergyDTO"></param>
        /// <returns></returns>
        public bool ManagePatientAllergy(AllergyDTO AllergyObj)
        {
            bool result = false;
            bool IsUpdate = false;
            try
            {
                if (AllergyObj.PatientAllergyId > 0)
                {
                    IsUpdate = true;
                }
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientAllergy AllergyList :: " + AllergyObj);

                if (AllergyValidator.ValidateAllergy(AllergyObj, ref sb, IsUpdate))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    int returnId = quickInputDAO.ManagePatientAllergy(AllergyObj, IsUpdate);

                    if (returnId > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date 20 July 2011
        /// Manage Patient Allergies List
        /// </summary>
        /// <param name="Allergylist"></param>
        /// <returns></returns>
        public bool ManagePatientAllergies(List<AllergyDTO> Allergylist)
        {
            bool result = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientAllergy AllergyList :: " + Allergylist);

                if (AllergyValidator.ValidateAllergy(Allergylist, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientAllergies(Allergylist);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date 20 July 2011
        /// This Method Will be used to manage PrescriptionMedication
        /// </summary>
        /// <param name="Allergylist"></param>
        /// <returns></returns>
        public bool ManagePatientPrescriptionMedication(List<PrescriptionMedicationDTO> PrescriptionMedicationlist)
        {
            bool result = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientPrescriptionMedication PrescriptionMedicationlist :: " + PrescriptionMedicationlist);

                if (PrescriptionValidator.ValidatePrescription(PrescriptionMedicationlist, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientPrescriptionMedication(PrescriptionMedicationlist);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, int cbID, int patID)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientMedicalHistory PARAM :: PateintMedicalHistoryObject " + PateintMedicalHistoryObject);

                if (PateintMedicalHistoryObject != null && PateintMedicalHistoryObject.PatientMedicalHistoryID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (ComprehensiveValidator.ValidateVBMedicalHistoryDTO(PateintMedicalHistoryObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    if (PateintMedicalHistoryObject.VisitID == 0)
                    {
                        int zeroVisitId = visitDAO.CheckZeroVisitExists(patID);
                        if (zeroVisitId > 0)
                        {
                            PateintMedicalHistoryObject.VisitID = zeroVisitId;
                        }
                        else
                        {
                            VisitDTO vDTO = new VisitDTO();
                            CreateZeroVisitDTO(patID, DateTime.Now, cbID, patID, (int)ProjectConstant.USER_TYPE.Patient, vDTO);

                            zeroVisitId = visitDAO.CreateZeroVisit(vDTO, patID);

                            PateintMedicalHistoryObject.VisitID = zeroVisitId;
                        }

                        
                    }        
                    Result = quickInputDAO.ManagePatientMedicalHistory(PateintMedicalHistoryObject, IsUpdate);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                    ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                    ErrorCodes.INVALID_AURGUMENTS_PREFIX)
                                    );
                    //   throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, int managedUsedId, int VisitId, DateTime ModificationDate)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientMedicalHistory PARAM :: PateintMedicalHistoryObject " + PateintMedicalHistoryObject);

                if (PateintMedicalHistoryObject != null && PateintMedicalHistoryObject.PatientMedicalHistoryID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (ComprehensiveValidator.ValidateMedicalHistoryDTO(PateintMedicalHistoryObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }
                    if (vbDAO == null)
                    {
                        vbDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                    }
                    if (PateintMedicalHistoryObject.VisitID == 0)
                    {
                        VisitDTO visitDTO = vbDAO.LoadVisitDataByVisitId(VisitId, false);
                        if (visitDTO != null)
                        {
                            int zeroVisitId = visitDAO.CheckZeroVisitExists(visitDTO.PatientId);
                            if (zeroVisitId > 0)
                            {
                                PateintMedicalHistoryObject.VisitID = zeroVisitId;
                            }

                        }
                    }

                    Result = quickInputDAO.ManagePatientMedicalHistory(PateintMedicalHistoryObject, IsUpdate, managedUsedId, VisitId, ModificationDate);
                }
                else
                {
                    //   throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.MEDICAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                   );
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 11 March 2011
        /// This Method Will be used to Insert/Update Patient Medical History
        /// </summary>
        /// <returns></returns>
        /*    public int ManagePatientMedicalHistory(MedicalHistoryDTO PateintMedicalHistoryObject, Database db, DbTransaction transaction)
            {
                int Result = 0;
                bool IsUpdate = false;
                try
                {
                    QuivusLogger.Info("Calling ManagePatientMedicalHistory PARAM :: PateintMedicalHistoryObject " + PateintMedicalHistoryObject);

                    if (PateintMedicalHistoryObject != null && PateintMedicalHistoryObject.PatientMedicalHistoryID > 0)
                    {
                        IsUpdate = true;
                    }

                    StringBuilder sb = new StringBuilder();

                    if (ComprehensiveValidator.ValidateMedicalHistoryDTO(PateintMedicalHistoryObject, ref sb, IsUpdate))
                    {

                        if (quickInputDAO == null)
                        {
                            quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                        }

                        Result = quickInputDAO.ManagePatientMedicalHistory(PateintMedicalHistoryObject, IsUpdate, db, transaction);
                    }
                    else
                    {
                        throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                    }
                }
                catch
                {
                    throw;
                }
                return Result;
            }
            */

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, int UserTypeId, int ClinicBranchId, int patId)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientFamilyHistory PARAM :: PateintFamilyHistoryObject " + PateintFamilyHistoryObject);

                if (PateintFamilyHistoryObject != null && PateintFamilyHistoryObject.PatientFamilyHistoryID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (ComprehensiveValidator.ValidateVBFamilyHistoryDTO(PateintFamilyHistoryObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    if (PateintFamilyHistoryObject.VisitID == 0)
                    {


                        int zeroVisitId = visitDAO.CheckZeroVisitExists(patId);
                            if (zeroVisitId > 0)
                            {
                                PateintFamilyHistoryObject.VisitID = zeroVisitId;
                            }
                            else
                            {
                                VisitDTO vDTO = new VisitDTO();
                                CreateZeroVisitDTO(patId, DateTime.Now, ClinicBranchId, patId, (int)ProjectConstant.USER_TYPE.Patient, vDTO);

                                zeroVisitId = visitDAO.CreateZeroVisit(vDTO, patId);

                                PateintFamilyHistoryObject.VisitID = zeroVisitId;
                            }
                        
                    }        

                    Result = quickInputDAO.ManagePatientFamilyHistory(PateintFamilyHistoryObject, IsUpdate, UserTypeId, ClinicBranchId);
                }
                else
                {
                    //  throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                  ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }

            return Result;
        }


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, int managedUsedId, int VisitId, DateTime ModificationDate, int UserTypeId, int ClinicBranchId)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientFamilyHistory PARAM :: PateintFamilyHistoryObject " + PateintFamilyHistoryObject);

                if (PateintFamilyHistoryObject != null && PateintFamilyHistoryObject.PatientFamilyHistoryID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (ComprehensiveValidator.ValidateFamilyHistoryDTO(PateintFamilyHistoryObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }
                    if (vbDAO == null)
                    {
                        vbDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                    }

                    if (PateintFamilyHistoryObject.VisitID == 0)
                    {
                        VisitDTO visitDTO = vbDAO.LoadVisitDataByVisitId(VisitId, false);
                        if (visitDTO != null)
                        {
                            int zeroVisitId = visitDAO.CheckZeroVisitExists(visitDTO.PatientId);
                            if (zeroVisitId > 0)
                            {
                                PateintFamilyHistoryObject.VisitID = zeroVisitId;
                            }

                        }
                    }
                    Result = quickInputDAO.ManagePatientFamilyHistory(PateintFamilyHistoryObject, IsUpdate, managedUsedId, VisitId, ModificationDate, UserTypeId, ClinicBranchId);
                }
                else
                {
                    //  throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.FAMILY_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }

            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Family History
        /// </summary>
        /// <returns></returns>
        public int ManagePatientFamilyHistory(FamilyHistoryDTO PateintFamilyHistoryObject, Database db, DbTransaction transaction)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientFamilyHistory PARAM :: PateintFamilyHistoryObject " + PateintFamilyHistoryObject);

                if (PateintFamilyHistoryObject != null && PateintFamilyHistoryObject.PatientFamilyHistoryID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (ComprehensiveValidator.ValidateFamilyHistoryDTO(PateintFamilyHistoryObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientFamilyHistory(PateintFamilyHistoryObject, IsUpdate, db, transaction);
                }
                else
                {
                    throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                }
            }
            catch
            {
                throw;
            }

            return Result;
        }


        public int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, int cbID, int patId)
        {
            int result = 0;
            bool IsUpdate = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageSocialHistoryAddiction SocialHistoryAddiction :: " + SocialHistoryAddiction);

                if (SocialHistoryAddiction != null && SocialHistoryAddiction.SocialHistoryAddictionID > 0)
                {
                    IsUpdate = true;
                }
                if (SocialAddictionValidator.ValidateSocialAddiction(SocialHistoryAddiction, ref sb, IsUpdate))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (vbDAO == null)
                    {
                        vbDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                    }

                    if (SocialHistoryAddiction.VisitId == 0)
                    {

                        int zeroVisitId = visitDAO.CheckZeroVisitExists(patId);
                        if (zeroVisitId > 0)
                        {
                            SocialHistoryAddiction.VisitId = zeroVisitId;
                        }
                        else
                        {
                            VisitDTO vDTO = new VisitDTO();
                            CreateZeroVisitDTO(patId, DateTime.Now, cbID, patId, (int)ProjectConstant.USER_TYPE.Patient, vDTO);

                            zeroVisitId = visitDAO.CreateZeroVisit(vDTO, patId);

                            SocialHistoryAddiction.VisitId = zeroVisitId;
                        }

                        
                    }   
                    result = quickInputDAO.ManageSocialHistoryAddiction(SocialHistoryAddiction, IsUpdate);
                }
                else
                {
                    // throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                ErrorCodes.SOCIAL_HISTORY_ADDICTION_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public int ManageSocialHistoryAddiction(SocialHistoryAddictionDTO SocialHistoryAddiction, int managedUsedId, int VisitId, DateTime ActionDate)
        {
            int result = 0;
            bool IsUpdate = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageSocialHistoryAddiction SocialHistoryAddiction :: " + SocialHistoryAddiction);

                if (SocialHistoryAddiction != null && SocialHistoryAddiction.SocialHistoryAddictionID > 0)
                {
                    IsUpdate = true;
                }
                if (SocialAddictionValidator.ValidateSocialAddiction(SocialHistoryAddiction, ref sb, IsUpdate))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (vbDAO == null)
                    {
                        vbDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                    }
                    if (SocialHistoryAddiction.VisitId == 0)
                    {
                        VisitDTO visitDTO = vbDAO.LoadVisitDataByVisitId(VisitId, false);
                        if (visitDTO != null)
                        {
                            int zeroVisitId = visitDAO.CheckZeroVisitExists(visitDTO.PatientId);
                            if (zeroVisitId > 0)
                            {
                                SocialHistoryAddiction.VisitId = zeroVisitId;
                            }

                        }
                    }
                    result = quickInputDAO.ManageSocialHistoryAddiction(SocialHistoryAddiction, IsUpdate, managedUsedId, VisitId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.SOCIAL_HISTORY_ADDICTION_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool DeleteSocialHistoryAddiction(SocialHistoryAddictionDTO DeletedObject)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling DeleteSocialHistoryAddiction DeletedObject :: " + DeletedObject);


                if (SocialAddictionValidator.ValidateDeletedSocialAddiction(DeletedObject, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.DeleteSocialHistoryAddiction(DeletedObject);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        public bool DeleteSocialHistory(SocialHistoryDTO DeletedObject)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling DeleteSocialHistoryAddiction DeletedObject :: " + DeletedObject);


                if (SocialValidator.ValidateDeletedSocial(DeletedObject, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.DeleteSocialHistory(DeletedObject);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public int ManagePatientSocialHistory(SocialHistoryDTO SocialHistoryObject, int cbID, int patId)
        {
            int result = 0;
            bool IsUpdate = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageSocialHistoryAddiction SocialHistoryObject :: " + SocialHistoryObject);

                if (SocialHistoryObject != null && SocialHistoryObject.PatientSocialHistoryID > 0)
                {
                    IsUpdate = true;
                }
                if (SocialValidator.ValidateVBSocial(SocialHistoryObject, ref sb, IsUpdate))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    if (SocialHistoryObject.VisitID == 0)
                    {

                        int zeroVisitId = visitDAO.CheckZeroVisitExists(patId);
                        if (zeroVisitId > 0)
                        {
                            SocialHistoryObject.VisitID = zeroVisitId;
                        }
                        else
                        {
                            VisitDTO vDTO = new VisitDTO();
                            CreateZeroVisitDTO(patId, DateTime.Now, cbID, patId, (int)ProjectConstant.USER_TYPE.Patient, vDTO);

                            zeroVisitId = visitDAO.CreateZeroVisit(vDTO, patId);

                            SocialHistoryObject.VisitID = zeroVisitId;
                        }

                        
                    }        


                    result = quickInputDAO.ManageSocialHistory(SocialHistoryObject, IsUpdate);

                    if (result > 0)
                    {
                        if (SocialHistoryObject.SocialHistoryAddictionList != null && SocialHistoryObject.SocialHistoryAddictionList.Count > 0)
                        {
                            foreach (SocialHistoryAddictionDTO obj in SocialHistoryObject.SocialHistoryAddictionList)
                            {
                                obj.PatientSocialHistoryID = result;
                                ManageSocialHistoryAddiction(obj,cbID,patId);
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public int ManagePatientSocialHistory(SocialHistoryDTO SocialHistoryObject, int managedUsedId, int PatientId, int VisitId, DateTime ActionDate)
        {
            int result = 0;
            bool IsUpdate = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageSocialHistoryAddiction SocialHistoryObject :: " + SocialHistoryObject);

                if (SocialHistoryObject != null && SocialHistoryObject.PatientSocialHistoryID > 0)
                {
                    IsUpdate = true;
                }
                if (SocialValidator.ValidateSocial(SocialHistoryObject, ref sb, IsUpdate))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (vbDAO == null)
                    {
                        vbDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                    }
                    if (SocialHistoryObject.VisitID == 0)
                    {
                        VisitDTO visitDTO = vbDAO.LoadVisitDataByVisitId(VisitId, false);
                        if (visitDTO != null)
                        {
                            int zeroVisitId = visitDAO.CheckZeroVisitExists(visitDTO.PatientId);
                            if (zeroVisitId > 0)
                            {
                                SocialHistoryObject.VisitID = zeroVisitId;
                            }

                        }
                    }
                    result = quickInputDAO.ManageSocialHistory(SocialHistoryObject, IsUpdate, managedUsedId, PatientId, VisitId, ActionDate);

                    if (result > 0)
                    {
                        if (SocialHistoryObject.SocialHistoryAddictionList != null && SocialHistoryObject.SocialHistoryAddictionList.Count > 0)
                        {
                            foreach (SocialHistoryAddictionDTO obj in SocialHistoryObject.SocialHistoryAddictionList)
                            {
                                obj.PatientSocialHistoryID = result;
                                ManageSocialHistoryAddiction(obj, managedUsedId, VisitId, ActionDate);
                            }
                        }
                    }
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                    ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                ErrorCodes.SOCIAL_HISTORY_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool ManagePatientChronicMedication(MedicationPrescribedDTO ChronicMedicationObj)
        {
            bool result = false;
            bool IsUpdate = false;
            try
            {
                if (ChronicMedicationObj.PatientMedicationPrescribedID > 0)
                {
                    IsUpdate = true;
                }
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientChronicMedication ChronicMedicationObj :: " + ChronicMedicationObj);

                if (ChronicMedicationValidator.ValidateMedication(ChronicMedicationObj, ref sb, IsUpdate))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    int returnId = quickInputDAO.ManagePatientChronicMedication(ChronicMedicationObj, IsUpdate);

                    if (returnId > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int cbId, int patId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientImmunization immunizationList :: " + immunizationList);

                if (VisitValidator.ValidateVBImmunization(immunizationList, ref sb))
                {
                    // throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString()); // For Testing Transaction

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    
                    int zeroVisitId = visitDAO.CheckZeroVisitExists(patId);
                    if (zeroVisitId == 0)
                    {
                        VisitDTO vDTO = new VisitDTO();
                        CreateZeroVisitDTO(patId, DateTime.Now, cbId, patId, (int)ProjectConstant.USER_TYPE.Patient, vDTO);

                        zeroVisitId = visitDAO.CreateZeroVisit(vDTO, patId);                        
                    }

                    foreach (ImmunizationDTO obj in immunizationList)
                    {
                        if (obj != null)
                        {
                            if (obj.VisitId == 0)
                            {

                                obj.VisitId = zeroVisitId;                           

                               
                            }

                        }
                    }



                    result = quickInputDAO.ManagePatientImmunization(immunizationList);
                }
                else
                {
                    //   throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                   ErrorCodes.IMMUNIZATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                   );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            bool result = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientImmunization immunizationList :: " + immunizationList + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateImmunization(immunizationList, managedByUserId, ref sb))
                {
                    // throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString()); // For Testing Transaction

                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }


                    if (vbDAO == null)
                    {
                        vbDAO = (ViewBackgroundDAO)DAOFactory.GetDataAccessObject(typeof(ViewBackgroundDAO));
                    }

                    foreach (ImmunizationDTO obj in immunizationList)
                    {
                        if (obj != null)
                        {
                            if (obj.VisitId == 0)
                            {
                                VisitDTO visitDTO = vbDAO.LoadVisitDataByVisitId(VisitId, false);
                                if (visitDTO != null)
                                {
                                    int zeroVisitId = visitDAO.CheckZeroVisitExists(visitDTO.PatientId);
                                    if (zeroVisitId > 0)
                                    {
                                        obj.VisitId = zeroVisitId;
                                    }
                                    
                                }
                            }

                        }
                    }

                    result = quickInputDAO.ManagePatientImmunization(immunizationList, managedByUserId, VisitId, ActionDate);
                }
                else
                {
                    //   throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.IMMUNIZATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor
        /// Date 18 Feb 2011
        /// save/update patient immunization with visit
        /// </summary>
        /// <param name="immunizationList"></param>
        /// <param name="managedByUserId"></param>
        /// <returns></returns>     
        public bool ManagePatientImmunization(List<ImmunizationDTO> immunizationList, int managedByUserId, Database db, DbTransaction transaction)
        {
            bool result = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientImmunization immunizationList :: " + immunizationList + " managedByUserId " + managedByUserId);

                if (VisitValidator.ValidateImmunization(immunizationList, managedByUserId, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientImmunization(immunizationList, managedByUserId, db, transaction);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }




        # endregion PMH Section


        #region Physical Section


        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        public int ManagePatientVitals(VitalDTO VitalObject)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientVitals PARAM :: VitalObject " + VitalObject);
                //   ObjectLogger.LogObject(VitalObject);

                if (VitalObject != null && VitalObject.PatientVitalID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (VitalValidator.ValidateVBVitalDTO(VitalObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientVitals(VitalObject, IsUpdate);
                }
                else
                {
                    //  throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                     ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                 ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                 ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                 );
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        public int ManagePatientVitals(VitalDTO VitalObject, int managedUsedId, int VisitId, DateTime ActionDate)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientVitals PARAM :: VitalObject " + VitalObject);

                if (VitalObject != null && VitalObject.PatientVitalID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (VitalValidator.ValidateVitalDTO(VitalObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientVitals(VitalObject, IsUpdate, managedUsedId, VisitId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.VITALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 13 March 2011
        /// This Method Will be used to Insert/Update Patient Vitals
        /// </summary>
        /// <returns></returns>
        public int ManagePatientVitals(VitalDTO VitalObject, Database db, DbTransaction transaction)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientVitals PARAM :: VitalObject " + VitalObject);

                if (VitalObject != null && VitalObject.PatientVitalID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (VitalValidator.ValidateVitalDTO(VitalObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientVitals(VitalObject, IsUpdate, db, transaction);
                }
                else
                {
                    throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientHPI(List<HPI_CCDTO> hpiList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientHPI HPIList :: " + hpiList);

                if (HPIValidator.ValidateVBHPI(hpiList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientHPI(hpiList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 21 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient HPI data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientHPI(List<HPI_CCDTO> hpiList, int managedUsedId, int PatientId, int VisitId, int ClinicBranchId, DateTime ActionDate)
        {
            bool result = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientHPI HPIList :: " + hpiList);

                if (HPIValidator.ValidateHPI(hpiList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientHPI(hpiList, managedUsedId, PatientId, VisitId, ClinicBranchId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.HPI_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                   );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date June 07 2011
        /// </summary>
        /// <param name="PhysicalList"></param>
        /// <returns></returns>
        public bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList)
        {
            bool result = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientPhysical PhysicalList :: " + PhysicalList);

                if (PhysicalValidator.ValidateVBPhysical(PhysicalList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientPhysical(PhysicalList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date june 07 2011
        /// </summary>
        /// <param name="PhysicalList"></param>
        /// <param name="managedByUserId"></param>
        /// <param name="VisitId"></param>
        /// <param name="PatientId"></param>
        /// <param name="ActionDate"></param>
        /// <returns></returns>
        public bool ManagePatientPhysical(List<PhysicalsDTO> PhysicalList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientPhysical PhysicalList :: " + PhysicalList);

                if (PhysicalValidator.ValidatePhysical(PhysicalList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientPhysical(PhysicalList, managedByUserId, VisitId, PatientId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.PHYSICALS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// date 07 june 2011
        /// </summary>
        /// <param name="ROSList"></param>
        /// <returns></returns>
        public bool ManagePatientROS(List<ROSDTO> ROSList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientROS ROSList :: " + ROSList);

                if (ROSValidator.ValidateVBROS(ROSList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientROS(ROSList);
                }
                else
                {
                    //    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                     ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                 ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                 ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                 );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 June 2011
        /// </summary>
        /// <param name="ROSList"></param>
        /// <param name="managedByUserId"></param>
        /// <param name="VisitId"></param>
        /// <param name="PatientId"></param>
        /// <param name="ActionDate"></param>
        /// <returns></returns>
        public bool ManagePatientROS(List<ROSDTO> ROSList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool result = false;

            try
            {

                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientROS ROSList :: " + ROSList);

                if (ROSValidator.ValidateROS(ROSList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientROS(ROSList, managedByUserId, VisitId, PatientId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.ROS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );

                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Date 15 March 2011
        /// This Method Will be used to Insert/Update Patient Labs
        /// </summary>
        /// <param name="LabObject"></param>
        /// <returns></returns>
        public int ManagePatientLabs(LabDTO LabObject)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientLabs 2 PARAM :: LabObject " + LabObject);

                if (LabObject != null && LabObject.PatientLabID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (LabValidator.ValidatVBLabDTO(LabObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientLabs(LabObject, IsUpdate);
                }
                else
                {
                    //   throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw

                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                                        ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_VIEWBACKGROUND_PREFIX,
                                                    ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                                    ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_ONE_PREFIX)
                                                    );
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }

        /// <summary>
        /// Author Muhammad Ismail
        /// Date 15 March 2011
        /// This Method Will be used to Insert/Update Patient Labs
        /// </summary>
        /// <param name="LabObject"></param>
        /// <returns></returns>
        public int ManagePatientLabs(LabDTO LabObject, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientLabs PARAM :: LabObject " + LabObject);

                if (LabObject != null && LabObject.PatientLabID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (LabValidator.ValidatLabDTO(LabObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientLabs(LabObject, IsUpdate, managedByUserId, VisitId, ActionDate);
                }
                else
                {
                    //   throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw

                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                                          ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                                      ErrorCodes.LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                                      ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                                      );
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }


        /// <summary>
        /// Author Muhammad Ismail
        /// Date 15 March 2011
        /// This Method Will be used to Insert/Update Patient Labs
        /// </summary>
        /// <param name="LabObject"></param>
        /// <returns></returns>
        public int ManagePatientLabs(LabDTO LabObject, Database db, DbTransaction transaction)
        {
            int Result = 0;
            bool IsUpdate = false;
            try
            {
                QuivusLogger.Info("Calling ManagePatientLabs PARAM :: LabObject " + LabObject);

                if (LabObject != null && LabObject.PatientLabID > 0)
                {
                    IsUpdate = true;
                }

                StringBuilder sb = new StringBuilder();

                if (LabValidator.ValidatLabDTO(LabObject, ref sb, IsUpdate))
                {

                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    Result = quickInputDAO.ManagePatientLabs(LabObject, IsUpdate, db, transaction);
                }
                else
                {
                    throw new InvalidArgumentException(sb.ToString()); // Validation Gets Failed Get Error MSG From String BUILDER & throw
                }
            }
            catch
            {
                throw;
            }
            return Result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool ManagePatientInHouseLabs(List<InHouseLabDTO> InputList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientProcedure InputList :: " + InputList);

                if (InHouseLabValidator.ValidateVBInHouseLabs(InputList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientInHouseLabs(InputList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool ManagePatientInHouseLabs(List<InHouseLabDTO> InputList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientProcedure InputList :: " + InputList);

                if (InHouseLabValidator.ValidateInHouseLabs(InputList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientInHouseLabs(InputList, managedByUserId, VisitId, PatientId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.IN_HOUSE_LABS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient in house labs results
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool ManagePatientInHouseLabResults(int PatientInHouseLabTestID, String TestResult, byte[] ResultAttachment, int ModifiedBy, DateTime ModifiedAt)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientInHouseLabResults PatientInHouseLabTestID :: " + PatientInHouseLabTestID
                    + "TestResult" + TestResult + "ResultAttachment" + ResultAttachment + "ModifiedBy" + ModifiedBy
                    + "ModifiedAt" + ModifiedAt);


                if (quickInputDAO == null)
                {
                    quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                }

                result = quickInputDAO.ManagePatientInHouseLabResults(PatientInHouseLabTestID, TestResult, ResultAttachment, ModifiedBy, ModifiedAt);

            }
            catch
            {
                throw;
            }
            return result;
        }


        #endregion End Physical Section

        # region Assessment
        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int cbID, int patID)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientDiagnosis diagnosisList :: " + diagnosisList);

                if (DiagnosisValidator.ValidateVBDiagnosis(diagnosisList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }
                    
                    int zeroVisitId = visitDAO.CheckZeroVisitExists(patID);
                    
                    if (zeroVisitId == 0)
                    {
                        VisitDTO vDTO = new VisitDTO();
                        CreateZeroVisitDTO(patID, DateTime.Now, cbID, patID, (int)ProjectConstant.USER_TYPE.Patient, vDTO);

                        zeroVisitId = visitDAO.CreateZeroVisit(vDTO, patID);
                    }

                    foreach (PatientDiagnosisDTO obj in diagnosisList)
                    {
                        if (obj != null)
                        {
                            if (obj.VisitID == 0)
                            {
                                obj.VisitID = zeroVisitId;                              
                                
                            }

                        }
                    }

                    result = quickInputDAO.ManagePatientDiagnosis(diagnosisList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 20 July 2011
        /// </summary>
        /// <param name="diagnosisList"></param>
        /// <returns></returns>
        public bool ManagePatientDiagnosisByRcopia(List<PatientDiagnosisDTO> diagnosisList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientDiagnosisByRcopia diagnosisList :: " + diagnosisList);

                if (DiagnosisValidator.ValidateSyncDiagnosis(diagnosisList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientDiagnosisByRcopia(diagnosisList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Mansoor 
        /// Date 22 Mar 2011
        /// This Method Will be used to Insert/Update/Delete Patient Diagnosis data
        /// </summary>
        /// <returns></returns>
        public bool ManagePatientDiagnosis(List<PatientDiagnosisDTO> diagnosisList, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientDiagnosis diagnosisList :: " + diagnosisList);

                if (DiagnosisValidator.ValidateDiagnosis(diagnosisList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    foreach (PatientDiagnosisDTO obj in diagnosisList)
                    {
                        if (obj != null)
                        {
                            if (obj.VisitID == 0)
                            {
                                VisitDTO visitDTO = vbDAO.LoadVisitDataByVisitId(VisitId, false);
                                if (visitDTO != null)
                                {
                                    int zeroVisitId = visitDAO.CheckZeroVisitExists(visitDTO.PatientId);
                                    if (zeroVisitId > 0)
                                    {
                                        obj.VisitID = zeroVisitId;
                                    }

                                }
                            }

                        }
                    }

                    result = quickInputDAO.ManagePatientDiagnosis(diagnosisList, managedByUserId, VisitId, PatientId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.DIAGNOSIS_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                   );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientDiagnosis RecommendationList :: " + RecommendationList);

                if (RecommendationValidator.ValidateVBRecommendation(RecommendationList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientRecommendation(RecommendationList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool ManagePatientRecommendation(List<PatientRecommendationDTO> RecommendationList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientDiagnosis RecommendationList :: " + RecommendationList);

                if (RecommendationValidator.ValidateRecommendation(RecommendationList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientRecommendation(RecommendationList, managedByUserId, VisitId, PatientId, ClinicBranchId, ActionDate);
                    QuivusLogger.Info("Saved ManagePatientDiagnosis RecommendationList :: " + RecommendationList);
                }
                else
                {
                    //   throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.RECOMMENDATION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                   );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }



        public bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp)
        {
            bool result = false;

            bool isUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling boolManagePatientFollowUp PatientFollowUp :: " + patientFollowUp);

                if (FollowUpValidator.ValidateVBFollowUp(patientFollowUp, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (patientFollowUp != null && patientFollowUp.PatientFollowUpID > 0)
                    {
                        isUpdate = true;
                    }

                    result = quickInputDAO.ManagePatientFollowUp(patientFollowUp, isUpdate);
                    {
                        result = true;
                    }
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool ManagePatientFollowUp(PatientFollowUpDTO patientFollowUp, int managedByUserId, int VisitId, int PatientId, DateTime ActionDate)
        {
            bool result = false;

            bool isUpdate = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling boolManagePatientFollowUp PatientFollowUp :: " + patientFollowUp);

                if (FollowUpValidator.ValidateFollowUp(patientFollowUp, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    if (patientFollowUp != null && patientFollowUp.PatientFollowUpID > 0)
                    {
                        isUpdate = true;
                    }

                    result = quickInputDAO.ManagePatientFollowUp(patientFollowUp, isUpdate, managedByUserId, VisitId, PatientId, ActionDate);
                    {
                        result = true;
                    }
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                       ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                   ErrorCodes.FOLLOW_UP_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                   ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                   );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        public bool ManagePatientProcedure(List<ProcedureDTO> InputList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientProcedure InputList :: " + InputList);

                if (ProcedureValidator.ValidateVBProcedure(InputList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientProcedure(InputList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 06 April 2011
        /// save/update patient procedure
        /// </summary>
        /// <param name="ProcedureList"></param>
        /// <returns></returns>
        public bool ManagePatientProcedure(List<ProcedureDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchId, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientProcedure InputList :: " + InputList);
                QuivusLogger.Info("Dispatching ManageQuickInputData Call TO VisitService with VisitID: " + VisitId.ToString() + " and PatientID: " + PatientId.ToString());
                if (ProcedureValidator.ValidateProcedure(InputList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientProcedure(InputList, managedByUserId, VisitId, PatientId, ClinicBranchId, ActionDate);
                }
                else
                {
                    //  throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.PROCEDURE_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public int ManagePatientMedication(MedicationDTO InputObj)
        {
            int result = 0;
            bool IsUpdate = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientProcedure InputObj :: " + InputObj);

                if (InputObj != null && InputObj.PatientMedicationID > 0)
                    IsUpdate = true;

                if (MedicationValidator.ValidateMedication(InputObj, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }


                    result = quickInputDAO.ManagePatientMedication(InputObj, IsUpdate);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public int ManagePatientMedication(MedicationDTO InputObj, int managedByUserId, int PatientId, DateTime ActionDate)
        {
            int result = 0;
            bool IsUpdate = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientProcedure InputObj :: " + InputObj);

                if (InputObj != null && InputObj.PatientMedicationID > 0)
                    IsUpdate = true;

                if (MedicationValidator.ValidateMedication(InputObj, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }


                    result = quickInputDAO.ManagePatientMedication(InputObj, IsUpdate, managedByUserId, PatientId, ActionDate);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Ismail
        /// Date 07 April 2011
        /// Delete patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool DeletePatientMedication(int PatientMedicationID, DateTime ModifiedAt)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientInHouseLabResults PatientMedicationID :: " + PatientMedicationID
                    + "ModifiedAt" + ModifiedAt);


                if (quickInputDAO == null)
                {
                    quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                }

                result = quickInputDAO.DeletePatientMedication(PatientMedicationID, ModifiedAt);

            }
            catch
            {
                throw;
            }
            return result;
        }

        ////


        public bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList, int managedByUserId, int VisitId, int PatientId, int ClinicBranchID, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientFollowupInstruction InputList :: " + InputList);

                if (FollowupInstructionValidator.ValidateFollowupInstruction(InputList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientFollowupInstruction(InputList, managedByUserId, VisitId, PatientId, ClinicBranchID, ActionDate);
                }
                else
                {
                    // throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString(),
                      ErrorCodes.ComposeErrorCode(ErrorCodes.ERROR_CODE_PREFIX, ErrorCodes.SECTION_QUICK_INPUT_PREFIX,
                                  ErrorCodes.FOLLOW_UP_INSTRUCTION_PREFIX, ErrorCodes.OPERATION_MANAGE_PREFIX,
                                  ErrorCodes.INVALID_AURGUMENTS_PREFIX + ErrorCodes.LEVEL_TWO_PREFIX)
                                  );
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool ManagePatientFollowupInstruction(List<PatientFollowupInstructionDTO> InputList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                QuivusLogger.Info("Calling ManagePatientFollowupInstruction InputList :: " + InputList);

                if (FollowupInstructionValidator.ValidateVBFollowupInstruction(InputList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientFollowupInstruction(InputList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        # endregion Assessment Section



        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        //public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes)
        //{
        //    bool result = false;

        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        QuivusLogger.Info("Calling ManageModuleNotes moduleNotes :: " + moduleNotes);

        //        if (VisitValidator.ValidateModuleNotes(moduleNotes, ref sb))
        //        {
        //            if (visitDAO == null)
        //            {
        //                visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
        //            }

        //            result = visitDAO.ManageModuleNotes(moduleNotes);
        //        }
        //        else
        //        {
        //            throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return result;
        //}

        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module notes of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public bool ManageModuleNotes(List<ModuleNoteDTO> moduleNotes, int managedByUserId, int VisitId, DateTime ActionDate)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageModuleNotes moduleNotes :: " + moduleNotes);

                if (VisitValidator.ValidateModuleNotes(moduleNotes, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.ManageModuleNotes(moduleNotes, managedByUserId, VisitId, ActionDate);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Mansoor
        /// Date 22 Feb 2011
        /// get list of module dictation of a visit
        /// </summary>      
        /// <param name="visitId"></param>
        /// <returns></returns> 
        public bool ManageModuleDictation(List<ModuleDictationDTO> ModuleDictationDTOList, int ManagedByUserID, int ClinicBranchID, int VisitId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageModuleDictation moduleDictations :: " + ModuleDictationDTOList);

                if (VisitValidator.ValidateModuleDictation(ModuleDictationDTOList, ref sb))
                {
                    if (visitDAO == null)
                    {
                        visitDAO = (VisitDAO)DAOFactory.GetDataAccessObject(typeof(VisitDAO));
                    }

                    result = visitDAO.ManageModuleDictation(ModuleDictationDTOList, ManagedByUserID, ClinicBranchID, VisitId);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Author Ismail
        /// Date 21 July 2011
        /// save/update patient medications
        /// </summary>
        /// <param name="InputList"></param>
        /// <returns></returns>
        public bool ManagePatientMedication(List<MedicationDTO> MedicationList)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManagePatientMedication MedicationList :: " + MedicationList);

                if (MedicationValidator.ValidateMedication(MedicationList, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManagePatientMedication(MedicationList);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Muhammad Ismail 
        /// Date 07 October 2011
        /// This Method Will be used to Delete Patient Family History
        /// </summary>
        /// <returns></returns>
        public bool DeleteFamilyHistory(FamilyHistoryDTO DeletedObject)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling DeleteFamilyHistory DeletedObject :: " + DeletedObject);


                if (ComprehensiveValidator.ValidateDeleteFamilyHistoryDTO(DeletedObject, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.DeleteFamilyHistory(DeletedObject);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
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
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling DeleteMedicalHistory DeletedObject :: " + DeletedObject);


                if (ComprehensiveValidator.ValidateDeleteMedicalHistoryDTO(DeletedObject, ref sb))
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.DeleteMedicalHistory(DeletedObject);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Author Baber Waqas 
        /// </summary>
        /// <param name="icdCodeDTO"></param>
        /// <param name="managedByUserIDForVisit"></param>
        /// <param name="clinicBranchID"></param>
        /// <returns></returns>
        public bool ManageCustomICDCode(ICDCodeDTO icdCodeDTO, int managedByUserIDForVisit, int clinicBranchID)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();

                QuivusLogger.Info("Calling ManageCustomICDCode ICDCodeDTO :: " );

                if (icdCodeDTO.IcdCodeIdSeq >0)
                {
                    if (quickInputDAO == null)
                    {
                        quickInputDAO = (QuickInputDAO)DAOFactory.GetDataAccessObject(typeof(QuickInputDAO));
                    }

                    result = quickInputDAO.ManageCustomICDCode(icdCodeDTO, managedByUserIDForVisit, clinicBranchID);
                }
                else
                {
                    throw new InvalidArgumentException(ProjectConstant.INVALID_ARGUMENT_EXCEPTION_MSG, sb.ToString());
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}