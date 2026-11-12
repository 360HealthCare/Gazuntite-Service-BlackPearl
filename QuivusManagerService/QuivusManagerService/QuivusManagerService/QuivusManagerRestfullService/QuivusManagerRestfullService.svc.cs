using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Visit;
using QuivusManager.IntegratedServies.VisitService;
using System.Diagnostics;
using Quivus.Utility.Logger;
using QuivusManager.Buisness.ApplicationSession;
using QuivusManager.Buisness.VisitBL.QuickInput.Impl;
using QuivusManager.Buisness.VisitBL.ViewBackgorund;
using QuivusManager.Buisness.VisitBL.QuickInput;
using Quivus.Utility;
using QuivusManager.ServiceResponse.ServiceCustomSession;
using Quivus.Utility.CustomException;
using QuivusManager.Buisness.Utility;
using System.Runtime.Serialization.Json;
using System.IO;
using QuivusManagerService.CustomeControls.General.SoapNote;
using QuivusManager.Buisness.VisitBL.ViewBackgorund.Impl;
using QuivusManager.Buisness.StaticDataBL.Impl;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Prescription;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Notes;
using QuivusManagerService.CustomeControls;
using QuivusManager.IntegratedServies.UserManagerService;
using QuivusManager.Buisness.PatientBL.Impl;
using QuivusManager.Buisness.UserBL.Impl;
using QuivusManager.Buisness.ClinicBL.Impl;
using QuivusManagerService.CustomeControls.General;
using System.Web.UI;
using QuivusManager.Buisness.UserBL;
using QuivusManager.Buisness.PatientBL;
using QuivusManager.Buisness.StaticDataBL;
using QuivusManager.Buisness.ClinicBL;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.PMH.Allergy;
using QuivusManager.ServiceResponse.ServiceCustomResponse.QuickInput.Assessment.Medication;
using ICDCodeDTO = QuivusManager.IntegratedServies.VisitService.ICDCodeDTO;
using QuivusManager.ServiceResponse.ServiceCustomResponse.Search;
using System.Web.UI.WebControls;

namespace QuivusManagerService.QuivusManagerRestfullService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QuivusManagerRestfullService" in code, svc and config file together.
    /// <summary>
    /// Author Baber Waqas Restfull service for saving visit operations
    /// </summary>
    public class QuivusManagerRestfullService : IQuivusManagerRestfullService
    {

        private IViewBackgroundBL _viewBackImpl = null;
        private IQuickInputBL _quickInputImpl = null;
        IClinicBL clinicBL = null;
        IUserBL UserImpl = null;
        IPatientBL PatientBL = null;
        IStaticDataBL StaticDataBL = null;
        IViewBackgroundBL viewBackImpl = null;
        VisitDTO visit = null;

        /// <summary>
        /// For testing purpose 
        /// </summary>
        /// <returns></returns>
        public String TestQIConcept()
        {
            try
            {
                //Person p = new Person();
                //  p.Name = "adsfasdf";
                //  p.DOB = DateTime.Now;
                String jsonTestData = "{Name:fisrd}";//=@"{"PMH":{"ChronicDiagnosis":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"D2976E55-2DAD-F8B3-CD62-21ECF9E36E4B"},"sort":null},"ComprehensiveHistory":{"FamilyHistory":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"961F56C7-4E6B-D026-7761-21ECF9E74E2C"},"sort":null},"MedicalHistory":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"08D72020-B370-3DDA-C6C8-21ECF9E6B980"},"sort":null},"SocialHistory":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"43F63FE0-577F-3091-1D18-21ECF9E7CB48"},"sort":null},"IsDirty":false,"IsDeleted":false},"Immunization":{"filterFunction":null,"source":[{"ImmunizationType":{"ImmunizationTypeId":21,"ImmunizationTypeName":"Adenovirus vaccine, type 7"},"DateAdmin":"","UpdatedAt":/Date(1323419145876)/,"IsRecordEditable":false,"LotNumber":"","NextDoseDue":"","IsRefusal":false,"ManagedBy":42932,"Manufacturer":"","ExpiryDate":"12/09/2011","Note":null,"IsDeleted":false,"CreatedBy":42932,"IsDirty":false,"Rout":"","CreatedAt":/Date(1323419145876)/,"DictationID":"00000000-0000-0000-0000-000000000000","RefusalReason":null,"Site":"","PatientImmunizationId":0,"TestedBy":0,"ModifiedBy":0,"NoteSubject":null,"Dose":"234","Series":"","ModifiedAt":/Date(-2174749200000)/,"VisitId":4940,"VisitDate":/Date(1323370800000)/}],"length":1,"list":{"source":[{"ImmunizationType":{"ImmunizationTypeId":21,"ImmunizationTypeName":"Adenovirus vaccine, type 7"},"DateAdmin":"","UpdatedAt":/Date(1323419145876)/,"IsRecordEditable":false,"LotNumber":"","NextDoseDue":"","IsRefusal":false,"ManagedBy":42932,"Manufacturer":"","ExpiryDate":"12/09/2011","Note":null,"IsDeleted":false,"CreatedBy":42932,"IsDirty":false,"Rout":"","CreatedAt":/Date(1323419145876)/,"DictationID":"00000000-0000-0000-0000-000000000000","RefusalReason":null,"Site":"","PatientImmunizationId":0,"TestedBy":0,"ModifiedBy":0,"NoteSubject":null,"Dose":"234","Series":"","ModifiedAt":/Date(-2174749200000)/,"VisitId":4940,"VisitDate":/Date(1323370800000)/}],"length":1,"uid":"77B0685E-855B-50FC-5472-21ECF9E79CC2"},"sort":null},"IsDeleted":false,"IsDirty":false,"Allergy":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"A176DE71-C491-6BAD-42D3-21ECF9E24A96"},"sort":null},"ChiefComplaint":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"40DE6B80-A453-BAA1-204D-21ECF9E365D2"},"sort":null},"ChronicMedication":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"0DAD9DCD-46D3-1399-DC42-21ECF9E40839"},"sort":null}},"SignedDate":/Date(-2174749200000)/,"Visit":{"ClinicBranch":{"ClinicBranchIdSeq":0,"ClinicBranchName":null,"IsDeleted":false,"IsDirty":false},"UserId":0,"IsDressedForPhysical":null,"IsRecordEditable":false,"IsValidDataExists":false,"SignedDate":/Date(-2174749200000)/,"IsVirtualVisit":false,"OwnerId":0,"OwnerUserType":null,"RecGuid":"00000000-0000-0000-0000-000000000000","RecordStatus":null,"CreatedBy":42932,"IsDirty":false,"CreatedAt":/Date(1323419105759)/,"DictationID":"00000000-0000-0000-0000-000000000000","EncounterTypeId":1,"CoSignedDate":/Date(-2174749200000)/,"TemplateId":0,"UserType":{"UserTypeName":null,"UserTypeId":0},"ModifiedBy":42932,"IsDeleted":false,"ModifiedDate":/Date(-2174749200000)/,"SignedByUserType":{"UserTypeName":null,"UserTypeId":0},"CreatedStationID":0,"VisitStatus":null,"TemplateVisitId":0,"AppointmentId":0,"SignedByUserId":0,"VisitId":0,"PatientId":0,"ModifiedAt":/Date(-2174749200000)/,"VisitDate":/Date(1323419104831)/},"Assessment":{"PatientFollowUp":null,"Diagnosis":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"E1608AC0-F2BD-155E-6B57-21ECF9F1048C"},"sort":null},"Recommendation":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"6BB8AB28-11F2-65D9-5A82-21ECF9F297D6"},"sort":null},"Procedure":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"84A948AE-CD31-7654-BA6C-21ECF9F26C78"},"sort":null},"PatientFollowupInstruction":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"346F3073-5941-040C-FEA7-21ECF9F2B911"},"sort":null},"IsDeleted":false,"IsDirty":false},"IsDeleted":false,"SignedByUserId":0,"isReviewed":false,"ModuleDictations":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"A05F421B-1AB4-CD49-F06E-21ECF9F3DA44"},"sort":null},"ModuleNotes":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"11130469-C752-F05A-E274-21ECF9F3B886"},"sort":null},"IsDirty":false,"IsSigned":false,"Physical":{"HPI_CC":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"E4C26502-9A1F-4BC9-5747-21ECF9EEA489"},"sort":null},"Labs":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"E7444E83-1400-397A-57E3-21ECF9EF9D0D"},"sort":null},"Physicals":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"C365BBDE-9A9C-CE1F-8816-21ECF9EF6E64"},"sort":null},"ReviewOfSystem":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"B517FE6B-383E-EF01-50C5-21ECF9EEDAAC"},"sort":null},"InHouseLabs":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"AF06FA5F-6C7E-7691-3EC8-21ECF9F0157A"},"sort":null},"IsDeleted":false,"IsDirty":false,"Vitals":null},"VisitId":4940,"ActionDate":/Date(1323419148704)/,"PatientId":42911,"ClinicBranchId":7,"SignedByUserTypeId":0}"
                //DateTime time = new DateTime();
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Person person = new Person();
                person.DOB = DateTime.Now;
                person.Name = "My Name is Baber ";

                var jasonString = oSerializer.Serialize(person);


                QuickInputDTO quickObject = new QuickInputDTO();

                quickObject.ActionDate = DateTime.Now;
                quickObject.Assessment = new AssessmentDTO();
                quickObject.ClinicBranchId = 7;
                quickObject.ExtensionData = null;
                quickObject.IsDeleted = false;
                quickObject.ModuleDictations = new List<ModuleDictationDTO>();
                quickObject.ModuleNotes = new List<ModuleNoteDTO>();
                quickObject.PatientId = 44339;
                quickObject.Physical = new PhysicalDTO();
                quickObject.PMH = new PMHDTO();
                quickObject.SignedByUserId = 33;
                quickObject.SignedByUserTypeId = 3;
                quickObject.SignedDate = DateTime.Now;
                quickObject.Visit = new VisitDTO();
                quickObject.VisitId = 3232;
                // quickObject.PMH.Immunization;
                var immuniList = new List<ImmunizationDTO>();

                for (int count = 0; count <= 6; count++)
                {
                    var immunDto = new ImmunizationDTO();
                    immunDto.CreatedAt = DateTime.Now;
                    immunDto.CreatedBy = 597;
                    immunDto.DateAdmin = " empty";
                    immunDto.DictationID = new Guid();
                    immunDto.Dose = (count + 200).ToString();
                    immunDto.ExpiryDate = DateTime.Now.ToShortDateString();
                    immunDto.ImmunizationType = null;
                    immunDto.LotNumber = (400 + count).ToString();
                    immunDto.ManagedBy = 597;
                    immunDto.Manufacturer = "adsfad";
                    immunDto.ModifiedAt = DateTime.Now;
                    immunDto.NextDoseDue = " pata nahi";
                    immunDto.Note = " this should be successfull";
                    immunDto.Rout = "343";
                    immunDto.Series = "333";
                    immunDto.Site = "3421";
                    immuniList.Add(immunDto);

                }
                quickObject.PMH.Immunization = immuniList;

                jasonString = oSerializer.Serialize(quickObject);


                // time = Convert.ToDateTime(oSerializer.Deserialize(jsonDate, typeof(DateTime)));

                // MemoryStream memory = new MemoryStream();
                // DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));



                // ser.WriteObject(memory, p);

                // //StringReader sr = new StringReader();
                //var p2= ser.ReadObject(memory);

                // return time.ToString();
                return jasonString;
            }
            catch (Exception ex)
            {
                //ToDO: 

            }
            return "FunctionFailedEnd";
        }

        /// <summary>
        /// For testing date concept
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public String TestDateConcept(QuickInputDTO p)
        {
            long incomingLength = WebOperationContext.Current.IncomingRequest.ContentLength;

            try
            {
                //  String jsonTestData = "{Name:fisrd}";//=@"{"PMH":{"ChronicDiagnosis":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"D2976E55-2DAD-F8B3-CD62-21ECF9E36E4B"},"sort":null},"ComprehensiveHistory":{"FamilyHistory":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"961F56C7-4E6B-D026-7761-21ECF9E74E2C"},"sort":null},"MedicalHistory":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"08D72020-B370-3DDA-C6C8-21ECF9E6B980"},"sort":null},"SocialHistory":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"43F63FE0-577F-3091-1D18-21ECF9E7CB48"},"sort":null},"IsDirty":false,"IsDeleted":false},"Immunization":{"filterFunction":null,"source":[{"ImmunizationType":{"ImmunizationTypeId":21,"ImmunizationTypeName":"Adenovirus vaccine, type 7"},"DateAdmin":"","UpdatedAt":/Date(1323419145876)/,"IsRecordEditable":false,"LotNumber":"","NextDoseDue":"","IsRefusal":false,"ManagedBy":42932,"Manufacturer":"","ExpiryDate":"12/09/2011","Note":null,"IsDeleted":false,"CreatedBy":42932,"IsDirty":false,"Rout":"","CreatedAt":/Date(1323419145876)/,"DictationID":"00000000-0000-0000-0000-000000000000","RefusalReason":null,"Site":"","PatientImmunizationId":0,"TestedBy":0,"ModifiedBy":0,"NoteSubject":null,"Dose":"234","Series":"","ModifiedAt":/Date(-2174749200000)/,"VisitId":4940,"VisitDate":/Date(1323370800000)/}],"length":1,"list":{"source":[{"ImmunizationType":{"ImmunizationTypeId":21,"ImmunizationTypeName":"Adenovirus vaccine, type 7"},"DateAdmin":"","UpdatedAt":/Date(1323419145876)/,"IsRecordEditable":false,"LotNumber":"","NextDoseDue":"","IsRefusal":false,"ManagedBy":42932,"Manufacturer":"","ExpiryDate":"12/09/2011","Note":null,"IsDeleted":false,"CreatedBy":42932,"IsDirty":false,"Rout":"","CreatedAt":/Date(1323419145876)/,"DictationID":"00000000-0000-0000-0000-000000000000","RefusalReason":null,"Site":"","PatientImmunizationId":0,"TestedBy":0,"ModifiedBy":0,"NoteSubject":null,"Dose":"234","Series":"","ModifiedAt":/Date(-2174749200000)/,"VisitId":4940,"VisitDate":/Date(1323370800000)/}],"length":1,"uid":"77B0685E-855B-50FC-5472-21ECF9E79CC2"},"sort":null},"IsDeleted":false,"IsDirty":false,"Allergy":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"A176DE71-C491-6BAD-42D3-21ECF9E24A96"},"sort":null},"ChiefComplaint":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"40DE6B80-A453-BAA1-204D-21ECF9E365D2"},"sort":null},"ChronicMedication":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"0DAD9DCD-46D3-1399-DC42-21ECF9E40839"},"sort":null}},"SignedDate":/Date(-2174749200000)/,"Visit":{"ClinicBranch":{"ClinicBranchIdSeq":0,"ClinicBranchName":null,"IsDeleted":false,"IsDirty":false},"UserId":0,"IsDressedForPhysical":null,"IsRecordEditable":false,"IsValidDataExists":false,"SignedDate":/Date(-2174749200000)/,"IsVirtualVisit":false,"OwnerId":0,"OwnerUserType":null,"RecGuid":"00000000-0000-0000-0000-000000000000","RecordStatus":null,"CreatedBy":42932,"IsDirty":false,"CreatedAt":/Date(1323419105759)/,"DictationID":"00000000-0000-0000-0000-000000000000","EncounterTypeId":1,"CoSignedDate":/Date(-2174749200000)/,"TemplateId":0,"UserType":{"UserTypeName":null,"UserTypeId":0},"ModifiedBy":42932,"IsDeleted":false,"ModifiedDate":/Date(-2174749200000)/,"SignedByUserType":{"UserTypeName":null,"UserTypeId":0},"CreatedStationID":0,"VisitStatus":null,"TemplateVisitId":0,"AppointmentId":0,"SignedByUserId":0,"VisitId":0,"PatientId":0,"ModifiedAt":/Date(-2174749200000)/,"VisitDate":/Date(1323419104831)/},"Assessment":{"PatientFollowUp":null,"Diagnosis":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"E1608AC0-F2BD-155E-6B57-21ECF9F1048C"},"sort":null},"Recommendation":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"6BB8AB28-11F2-65D9-5A82-21ECF9F297D6"},"sort":null},"Procedure":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"84A948AE-CD31-7654-BA6C-21ECF9F26C78"},"sort":null},"PatientFollowupInstruction":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"346F3073-5941-040C-FEA7-21ECF9F2B911"},"sort":null},"IsDeleted":false,"IsDirty":false},"IsDeleted":false,"SignedByUserId":0,"isReviewed":false,"ModuleDictations":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"A05F421B-1AB4-CD49-F06E-21ECF9F3DA44"},"sort":null},"ModuleNotes":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"11130469-C752-F05A-E274-21ECF9F3B886"},"sort":null},"IsDirty":false,"IsSigned":false,"Physical":{"HPI_CC":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"E4C26502-9A1F-4BC9-5747-21ECF9EEA489"},"sort":null},"Labs":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"E7444E83-1400-397A-57E3-21ECF9EF9D0D"},"sort":null},"Physicals":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"C365BBDE-9A9C-CE1F-8816-21ECF9EF6E64"},"sort":null},"ReviewOfSystem":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"B517FE6B-383E-EF01-50C5-21ECF9EEDAAC"},"sort":null},"InHouseLabs":{"filterFunction":null,"source":[],"length":0,"list":{"source":[],"length":0,"uid":"AF06FA5F-6C7E-7691-3EC8-21ECF9F0157A"},"sort":null},"IsDeleted":false,"IsDirty":false,"Vitals":null},"VisitId":4940,"ActionDate":/Date(1323419148704)/,"PatientId":42911,"ClinicBranchId":7,"SignedByUserTypeId":0}"
                //DateTime time = new DateTime();
                var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                // QuickInputDTO q2i = (QuickInputDTO)oSerializer.Deserialize(jsonString, typeof(QuickInputDTO));


                //  QuickInputDTO a = (QuickInputDTO)p;
                //Person p = new Person();
                //  p.Name = "adsfasdf";
                //  p.DOB = DateTime.Now;

                // DateTime time = new DateTime();
                //  System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                // string sJSON = oSerializer.Serialize(p);

                // time = Convert.ToDateTime(oSerializer.Deserialize(jsonDate, typeof(DateTime)));

                // MemoryStream memory = new MemoryStream();
                // DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));



                // ser.WriteObject(memory, p);

                // //StringReader sr = new StringReader();
                //var p2= ser.ReadObject(memory);

                // return time.ToString();
                var q2i = p;
                return q2i.VisitId.ToString();
            }
            catch (Exception ex)
            {
                //ToDO: 

            }
            return "FunctionFailedEnd";
        }

        /// <summary>
        /// For saving visit 
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="managedByUserId"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public ManageVisitResponse ManageQuickInputData(QuickInputDTO quickInputDTO, String managedByUserId, String sessionID)
        {
            Stopwatch stopwatch = new Stopwatch();
            QuivusLogger.Info("PerfomanceTestManageQuickInput:Start time stamp is " + DateTime.Now);


            //System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //QuickInputDTO quickInputDTO = (QuickInputDTO)oSerializer.Deserialize(data, typeof(QuickInputDTO));

            // Begin timing
            stopwatch.Start();



            ManageVisitResponse response = null;
            int UserTypeId = 0;
            try
            {
                QuivusLogger.Info("In QuivusManagerServiceRestfull ManageQuickInputData Called");
                QuivusLogger.Info("ManageVisitWithData restfull PARAM quickInputDTO::" + quickInputDTO + "managedByUserId" + managedByUserId);

                int managedByUserIDForVisit = Convert.ToInt32(managedByUserId);
                Guid sessionIDGuid = Guid.Parse(sessionID);
                UserSessionDTO session = QApplicationSession.GetUserSession(sessionIDGuid);
                QuivusLogger.Info("Session found with session data" + session + "managedByUserId" + managedByUserId);

                #region patch for session id check
                if (session == null)
                {
                    if (_quickInputImpl == null)
                    {
                        _quickInputImpl = new QuickInputBLImpl();
                    }
                    //  UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (session != null)
                        UserTypeId = 3;

                    response = _quickInputImpl.ManageQuickInputData(quickInputDTO, managedByUserIDForVisit, UserTypeId);
                    #region Update Allergies from eprescription
                    if (quickInputDTO.PMH.Allergy != null)
                    {
                        var qResults = from allergy in quickInputDTO.PMH.Allergy
                                       where allergy.IsActive.Equals(ProjectConstant.STRING_FALSE.ToLower())
                                       select allergy;

                        _quickInputImpl.UpdateAllergiesToEprescription(qResults, quickInputDTO.Visit.PatientId);


                    }
                    #endregion
                }

                else if (ApplicationSessionHandler.IsValidUserSession(session))
                {
                    if (_quickInputImpl == null)
                    {
                        _quickInputImpl = new QuickInputBLImpl();
                    }
                    //  UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);
                    if (session != null)
                        UserTypeId = session.UserTypeID;

                    response = _quickInputImpl.ManageQuickInputData(quickInputDTO, managedByUserIDForVisit, UserTypeId);
                    #region Update Allergies from eprescription
                    if (quickInputDTO.PMH.Allergy != null)
                    {
                        var qResults = from allergy in quickInputDTO.PMH.Allergy
                                       where allergy.IsActive.Equals(ProjectConstant.STRING_FALSE.ToLower())
                                       select allergy;

                        _quickInputImpl.UpdateAllergiesToEprescription(qResults, quickInputDTO.Visit.PatientId);


                    }
                    #endregion
                }
                #endregion
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                if (response == null)
                    response = new ManageVisitResponse();

                ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<QuivusManager.IntegratedServies.VisitService.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);

                if (response == null)
                    response = new ManageVisitResponse();

                ResponseErrorHandler.HandleException(response, exp);
            }
            stopwatch.Stop();

            // Write result
            //Console.WriteLine("Time elapsed: {0}",
            //    stopwatch.Elapsed);
            QuivusLogger.Info("PerfomanceTestManageQuickInput:total time stamp restfull  is " + DateTime.Now);
            QuivusLogger.Info("PerfomanceTestManageQuickInput:total time elapsed with restfull is " + stopwatch.Elapsed);
            return response;
        }



        /// <summary>
        /// Author: Baber Waqas at 1/24/2012
        /// </summary>
        /// <param name="customICDCodeDTO"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
     public bool ManageCustomICDCode(ICDCodeDTO customICDCodeDTO,String userID, String sessionID)
     {   
         var userTypeId = 0;
         var clinicBranchID = 0;
            var response = false;
         try
         {
             QuivusLogger.Info("In QuivusManagerServiceRestfull ManageCustomICDCode Called");
             QuivusLogger.Info("ManageCustomICDCode restfull PARAM quickInputDTO::" + customICDCodeDTO + "managedByUserId" );

             var manageByUserID = Convert.ToInt32(userID);
             var sessionIDGuid = Guid.Parse(sessionID);
             var session = QApplicationSession.GetUserSession(sessionIDGuid);
             QuivusLogger.Info("Session found with session data" + session + "managedByUserId" + session);

             #region patch for session id check
             if (ApplicationSessionHandler.IsValidUserSession(session))
             {
                 if (_quickInputImpl == null)
                 {
                     _quickInputImpl = new QuickInputBLImpl();
                 }
                 //  UserSessionDTO session = QApplicationSession.GetUserSession(userSession.SessionID);
                 if (session != null)
                 {
                     userTypeId = session.UserTypeID;
                     clinicBranchID = session.ClinicBranchID;
                 }

                 response = _quickInputImpl.ManageCustomICDCode(customICDCodeDTO, manageByUserID,
                                                                     clinicBranchID);
             }

             #endregion
         }
         catch (Exception  exp)
         {
             QuivusLogger.Error(exp);
             throw;
         }
       
         return response;
     }

            
        /// <summary>
        /// For auto save functionality 
        /// author Baber Waqas
        /// </summary>
        /// <param name="quickInputDTO"></param>
        /// <param name="fileName"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public bool SaveTempQI(QuickInputDTO quickInputDTO, string fileName, String sessionID)
        {
            bool response = false;
            try
            {
                QuivusLogger.Info("Saving TempQI at " + DateTime.Now + "and file name is " + fileName + " with session ID " + sessionID);
                Guid sessionIDGuid = Guid.Parse(sessionID);
                UserSessionDTO session = QApplicationSession.GetUserSession(sessionIDGuid);
                QuivusLogger.Info("Session found with session data" + session);
                if (ApplicationSessionHandler.IsValidUserSession(session))
                {
                    if (_quickInputImpl == null)
                    {
                        _quickInputImpl = new QuickInputBLImpl();
                        response = _quickInputImpl.SaveTempQI(quickInputDTO, fileName);
                    }

                }
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp);
                // ResponseErrorHandler.HandleException(new PatientFollowupInstructionResponse(), exp);
            }
            return response;
        }

        public string GenerateSoapNoteByVisitID(QuickInputDTO quickInputDTO, string VisitID, string sessionID, string clinicbranchId, string stationId, string isAllSelected, string isMale, string isTemp)
        {
            string note = "";
            SoapNoteResponse html = new SoapNoteResponse();
            string address = string.Empty;
            PatientProfileDTO patientInfo = null;
            BaseUserDTO providerInfo = null;
            string logoFileName = string.Empty;
            try
            {
                QuivusLogger.Info("In QuivusManagerService GenerateSoapNoteByVisitID Called");
                QuivusLogger.Info("GenerateSoapNoteByVisitID PARAM VisitID::" + VisitID);
                int visitId = int.Parse(VisitID);
                Guid sessionIDGuid = Guid.Parse(sessionID);
                UserSessionDTO userSession = QApplicationSession.GetUserSession(sessionIDGuid);
                int clinicBranId = int.Parse(clinicbranchId);
                List<int> stationIds = new List<int>();
                stationIds.Add(int.Parse(stationId));
                bool allSelected = bool.Parse(isAllSelected);
                QuivusLogger.Info("Soap note call recive with sessionId " + sessionID);
                if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    QuivusLogger.Info("Vaild User Session");
                    Page page = new Page();
                    page.EnableEventValidation = false;

                    SoapNoteView soapNote = (SoapNoteView)page.LoadControl("CustomeControls/General/SoapNoteView.ascx");
                    SoapNotePatient patientDetails = (SoapNotePatient)soapNote.FindControl("patientDetails");
                    SoapNoteHeader myHeader = (SoapNoteHeader)soapNote.FindControl("header");

                    if (clinicBL == null)
                    {
                        clinicBL = new ClinicBL();
                    }
                    if (UserImpl == null)
                    {
                        UserImpl = new UserBLImpl();
                    }

                    List<List<SoapNoteCatagoryDTO>> list = getSoapNoteDataListByVisit(quickInputDTO, visitId, clinicBranId, stationIds, allSelected, "true", bool.Parse(isTemp));

                    if (visit != null)
                    {
                        if (PatientBL == null)
                        {
                            PatientBL = new PatientBLImpl();
                        }
                        if (!bool.Parse(isTemp))
                        {
                            patientInfo = PatientBL.LoadPatientCompleteProfileForClinic(visit.PatientId, clinicBranId, visit.OwnerId).PatientProfileResponseData;
                            providerInfo = UserImpl.LoadBaseUserByUserId(visit.OwnerId).ResponseData;
                        }
                        else
                        {
                            patientInfo = PatientBL.LoadPatientCompleteProfileForClinic(visit.PatientId, clinicBranId, userSession.UserID).PatientProfileResponseData;
                            providerInfo = UserImpl.LoadBaseUserByUserId(userSession.UserID).ResponseData;
                        }
                        if (patientInfo != null)
                        {
                            patientDetails.PatientName = patientInfo.UserDTO.FirstName + " " + patientInfo.UserDTO.LastName;
                            patientDetails.DOB = patientInfo.PatientDTO.DateOfBirth.ToShortDateString();
                            patientDetails.DateOfService = visit.VisitDate.ToShortDateString();
                        }
                        if (providerInfo != null)
                            patientDetails.ProviderName = providerInfo.FirstName + " " + providerInfo.LastName;
                    }
                    ClinicDTO clinic = clinicBL.LoadClinicBranchAndStationInfo(int.Parse(stationId));
                    //myHeader.ClinicName = clinic.ClinicName;
                    myHeader.ClinicAddress = clinic.ClinicBranchInfo.DefaultStation.Address + ", " + clinic.ClinicBranchInfo.ZIP + "," + clinic.ClinicBranchInfo.State;
                    myHeader.ClinicPhone = clinic.ClinicBranchInfo.DefaultStation.PhoneNumber;
                    myHeader.ClinicFax = clinic.ClinicBranchInfo.DefaultStation.FaxNumber;
                    soapNote.Controls.Add(myHeader);
                    soapNote.Controls.Add(patientDetails);
                    logoFileName = clinic.ClinicName + ".png";

                    //History Section
                    if (list.Count >= 1 && list[0].Count > 0)
                    {
                        SoapNoteGridView history = (SoapNoteGridView)soapNote.FindControl("history");
                        history.Title = "History:";
                        history.Data = list[0];

                        soapNote.Controls.Add(history);
                    }

                    //Subjective Section
                    if (list.Count >= 2 && list[1].Count > 0)
                    {
                        SoapNoteGridView subjective = (SoapNoteGridView)soapNote.FindControl("subjective");
                        subjective.Title = "Subjective:";
                        subjective.Data = list[1];

                        soapNote.Controls.Add(subjective);
                    }

                    //Objective Section
                    if (list.Count >= 3 && list[2].Count > 0)
                    {
                        SoapNoteGridView objective = (SoapNoteGridView)soapNote.FindControl("objective");
                        objective.Title = "Objective:";
                        objective.Data = list[2];

                        soapNote.Controls.Add(objective);
                    }

                    //Assessment Section
                    if (list.Count >= 4 && list[3].Count > 0)
                    {
                        SoapNoteGridView assessment = (SoapNoteGridView)soapNote.FindControl("assessment");
                        assessment.Title = "Assessment:";
                        assessment.Data = list[3];

                        soapNote.Controls.Add(assessment);
                    }

                    //Plan Section
                    if (list.Count >= 5 && list[4].Count > 0)
                    {
                        SoapNoteGridView plan = (SoapNoteGridView)soapNote.FindControl("plan");
                        plan.Title = "Plan:";
                        plan.Data = list[4];

                        soapNote.Controls.Add(plan);
                    }
                    SoapNoteSignedBy signedBy = (SoapNoteSignedBy)soapNote.FindControl("signedBy");
                    if (visit.SignedByUserId > 0)
                    {
                        BaseUserDTO signedByUser = UserImpl.LoadBaseUserByUserId(visit.SignedByUserId).ResponseData;
                        signedBy.ProviderName = signedByUser.FirstName + " " + signedByUser.LastName + " M.D - at " + visit.SignedDate.ToShortDateString() + " " + visit.SignedDate.ToShortTimeString();

                    }
                    else
                    {
                        signedBy.ProviderName = "Visit is not Signed";
                    }
                    soapNote.Controls.Add(signedBy);
                    SoapNoteFooter footer = (SoapNoteFooter)soapNote.FindControl("footer");
                    if (providerInfo != null)
                        footer.ProviderName = providerInfo.FirstName + " " + providerInfo.LastName + "- at " + DateTime.Now.ToShortDateString();
                    soapNote.Controls.Add(footer);
                    soapNote.BindData();
                    patientDetails.BindData();
                    page.Controls.Add(soapNote);
                    page.DataBind();
                    note = TempCtrlUtil.RenderControl(page);
                    note = System.Web.HttpUtility.HtmlDecode(note);

                    // Code Review and adjusted  by Waqas baber on 1/30/2012
                    address =
                        TempCtrlUtil.createHtmlPage(
                            userSession != null ? "soapNote_" + userSession.UserID + ".html" : "soapNote_00000.html",
                            note, logoFileName); 
                        
                    html.body = note;

                }
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp.Code + exp.Data.ToString() + "\n" + exp.StackTrace);
            }
            catch (Exception exp)
            {
                QuivusLogger.Error(exp.Message.ToString() + "\n" + exp.StackTrace);
            }
            finally
            {

            }
            //WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            //return new MemoryStream(Encoding.UTF8.GetBytes(note));
            //return note;
            return address;
        }

        public List<List<SoapNoteCatagoryDTO>> getSoapNoteDataListByVisit(QuickInputDTO quickInputDTO, int VisitID, int clinicBranchId, List<int> stationIds, bool isAllSelected, string isMale, bool isTemp)
        {
            List<List<SoapNoteCatagoryDTO>> list = new List<List<SoapNoteCatagoryDTO>>();
            try
            {
                QuivusLogger.Info("In QuivusManagerService GenerateSoapNoteByVisitID Called");
                QuivusLogger.Info("GenerateSoapNoteByVisitID PARAM VisitID::" + VisitID);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                {
                    if (viewBackImpl == null)
                    {
                        viewBackImpl = new ViewBackgroundBLImpl();
                    }
                    if (StaticDataBL == null)
                    {
                        StaticDataBL = new StaticDataBLImpl();
                    }
                    QuickInputResponse response = null;
                    if (!isTemp)
                        response = viewBackImpl.LoadQuickInputByVisit(VisitID);
                    AllergyResponse allergyResponse = null;
                    PatientMedicationReponse medicationResponse = null;
                    PatientPrescriptionMedicationResponse prescriptionResponse = null;

                    if (!isTemp)
                    {
                        allergyResponse = viewBackImpl.LoadPatientAllergyByPatientId(response.ResponseData.Visit.PatientId, clinicBranchId);
                        medicationResponse = viewBackImpl.LoadPatientMedicationByPatientId(response.ResponseData.Visit.PatientId);
                        prescriptionResponse = viewBackImpl.LoadPrescriptionMedicationByPatientId(response.ResponseData.Visit.PatientId, clinicBranchId, stationIds, isAllSelected);
                    }
                    else
                    {
                        allergyResponse = viewBackImpl.LoadPatientAllergyByPatientId(quickInputDTO.PatientId, clinicBranchId);
                        medicationResponse = viewBackImpl.LoadPatientMedicationByPatientId(quickInputDTO.PatientId);
                        prescriptionResponse = viewBackImpl.LoadPrescriptionMedicationByPatientId(quickInputDTO.PatientId, clinicBranchId, stationIds, isAllSelected);
                    }
                    VisitNotesResponse notes = viewBackImpl.LoadModuleNotesByVisitId(VisitID);
                    SoapNoteDataProcessor.ModuleTypeList = StaticDataBL.LoadModuleTypes().ModuleTypeData;
                    SoapNoteDataProcessor.RosDataList = StaticDataBL.LoadROSCategoryTypes(bool.Parse(isMale)).ResponseList;
                    SoapNoteDataProcessor.PhyDataList = StaticDataBL.LoadPhysicalCategoryTypes(bool.Parse(isMale)).ResponseList;
                    if (!isTemp)
                    {
                        visit = response.ResponseData.Visit;
                        SoapNoteDataProcessor.ModuleList = response.ResponseData.ModuleNotes;
                    }
                    else
                    {
                        visit = quickInputDTO.Visit;
                        visit.PatientId = quickInputDTO.PatientId;
                        SoapNoteDataProcessor.ModuleList = quickInputDTO.ModuleNotes;
                    }

                    //if (!isTemp)
                    //     list = list.Concat(SoapNoteDataProcessor.ProcessCatagories(response.ResponseData, clinicBranchId, preResponse.ResponseList)).ToList<SoapNoteCatagoryDTO>();
                    //else
                    //    list = list.Concat(SoapNoteDataProcessor.ProcessCatagories(quickInputDTO, clinicBranchId, preResponse.ResponseList)).ToList<SoapNoteCatagoryDTO>();

                    // <codereview>
                    // Code review and adjusted by Baber Waqas on 1/20/2012
                    //
                    //</codereview>
                    if (allergyResponse != null)
                        SoapNoteDataProcessor.AllergyList = allergyResponse.ResponseData;

                    if (medicationResponse != null)
                  
                        SoapNoteDataProcessor.MedicationList = medicationResponse.ResponseList;

                    if (prescriptionResponse != null)
                        SoapNoteDataProcessor.PreMed = prescriptionResponse.ResponseList;
                     //<codereview>
                   // Code review and adjusted by Baber Waqas 1/30/2012
                   // 
                   //</codereview>
                   list = SoapNoteDataProcessor.ProcessCatagories(!isTemp ? response.ResponseData : quickInputDTO, clinicBranchId);
               }
           }
           catch (InvalidSessionException exp)
           {
               QuivusLogger.Critical(exp.Code + exp.Data.ToString() + "\n" + exp.StackTrace);
           }


            return list;
        }



        /// <summary>
        /// Author:Zahid Mehmood
        /// Date: 13 Jan 2012
        /// Description: This function Returns Contact list for Communication Provider Search List
        /// </summary>
        /// <param name="clinicBranchId"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public List<ContactSearchDTO> LoadUsersByClinicBranchID(string clinicBranchId)
        {
            List<ContactSearchDTO> response = null;
            try
            {
                QuivusLogger.Info("In QuivusManagerRestFullService LoadUsersByClinicBranchID Called");
                QuivusLogger.Info("LoadUsersByClinicBranchID PARAM ClinicBranchId::" + clinicBranchId);

                //if (ApplicationSessionHandler.IsValidUserSession(userSession))
                //{
                //    if (UserImpl == null)
                //    {
                //        UserImpl = new UserBLImpl();
                //    }
                //    response = UserImpl.LoadUsersByClinicBranchID(int.Parse(ClinicBranchId));
                //}
                if (UserImpl == null)
                {
                    UserImpl = new UserBLImpl();
                }
                response = UserImpl.LoadProviderByClinicBranchID(int.Parse(clinicBranchId));
            }
            catch (InvalidSessionException exp)
            {
                QuivusLogger.Error(exp);
                //ResponseErrorHandler.HandleException(response, exp, exp.Code);
            }
            catch (FaultException<Quivus.Utility.Fault.QuivusFault> exp)
            {
                QuivusLogger.Error(exp);
                //ResponseErrorHandler.HandleException(response, exp);
            }
            return response;
        }
    }
}