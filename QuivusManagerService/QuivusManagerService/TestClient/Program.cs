using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using TestClient.QuivusManagerService;

using TestClient.test;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.ServiceModel;


namespace TestClient
{
    class Program
    {

        //public static void LogObject(object sourceObject)
        //{
        //    try
        //    {
                

        //        String emptyString = string.Empty;

        //        if (sourceObject == null)
        //            return;

        //        System.Type sourceType = sourceObject.GetType();


        //        foreach (PropertyInfo currentProperty in sourceType.GetProperties())
        //        {
        //            //PropertyInfo targetObj = targetType.GetProperty(currentProperty.Name);

        //            if (currentProperty.PropertyType.IsClass && !currentProperty.PropertyType.IsInstanceOfType(emptyString))
        //            {
        //                object sourceSub = currentProperty.GetValue(sourceObject, null);
        //                if (!currentProperty.PropertyType.IsGenericType)
        //                    LogObject(sourceSub);
        //            }
        //            else
        //            {
        //                String vaue = "" + currentProperty.GetValue(sourceObject, null);
        //                Console.WriteLine("currentProperty :: " + currentProperty + ":: " + vaue);

        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //} // End Method 

        //static void checkcoSign()
        //{
        //}

        static void Main(string[] args)
        {
           
            test.QuivusManagerServiceClient client = new test.QuivusManagerServiceClient();
            int []ids=new int[2];
            ids[0]=1;
            var html = client.GenerateSoapNoteByVisitID(4300, null,7,ids,false);
            Console.Write(html);
            Console.Read();

        // //   QuivusManagerService.QuivusManagerServiceClient client = new QuivusManagerServiceClient();
        // //   var pingr = client.PingAllClientServices();

            //test.QuivusManagerServiceClient client = new test.QuivusManagerServiceClient();
            //var html = client.GenerateSoapNoteByVisitID(4300, null);
            //Console.Write(html);
            //Console.Read();

        // //   QuivusManagerServiceClient client1 = new QuivusManagerServiceClient(); 
        // //       var res=client1.AuthenticateUser("test@quivus.com", "quivus");

        // //   //QMServiceReference_local.QuivusManagerServiceClient client = new QuivusManagerServiceClient();

        // //   UserSessionDTO session = client.LogLoginHistory(597, 3, 7, true);
        // //   var syncCallRestul = client.SyncEPrescription(529,"",7,4442,session);
        // //   MedicalHistoryDTO deleteMedical = new MedicalHistoryDTO { PatientMedicalHistoryID = 162, ModifiedBy = 470, ModifiedAt = new DateTime() };

        // //// var dfgfd32sf=  client.DeleteMedicalHistory(deleteMedical,session);

        // //   var vvvv = client.LoadUserDefaultQuickLinksbyLocation(487, 7, session);

        // //   List<int> sid = new List<int>();

        // //   sid.Add(3);

        // //   client.LoadPatientAllDiagnosisByPatientId(517, 7, session, sid, true);

        // //   session.KickOutPreviousSession = true;

        // //   var dasfsdf = client.UpdateUserSession(session);

        // //   // client.UpdateUserSession(session);

        // //   List<int> stationId = new List<int>();
        // //   stationId.Add(1);

        // //   client.LoadPatientDiagnosisByPatientId(8199, 7, session, stationId, true);

        // //   var vvv = client.LoadPatientCompleteProfileForClinic(2325, 7, 601, session);

        // //   List<PatientEmergencyContactDTO> EmergencyContactList = new List<PatientEmergencyContactDTO>();

        // //   PatientEmergencyContactDTO contact = new PatientEmergencyContactDTO
        // //   {
        // //       FirstName = "firt123EMergency",
        // //       LastName = "LastNAme123Emergency",
        // //       RelationShipType = new RelationshipTypeDTO { RelationshipId = 1 },
        // //       StatusType = new StatusTypeDTO { StatusID = 1 },
        // //       EmergencyPhone = new PhoneDTO { PhoneNumber = "1112223333", Extension = "1234", NumberTypeID = 1, OwnerTypeID = 1 }
        // //   };

        // //   EmergencyContactList.Add(contact);

        // //   client.ManagePatientEmergencyContact(471, 472, EmergencyContactList, session);
        // //   //   var testingLoad = client.LoadPatientDocumentByClinicIdAndPatientID(7, 547, 1, session);

        // //   //    ar ftg = client.LoadPatientProfileSettings(7, 487, 8176);
        // //   //   var dsfds23 = client.LoadPatientProfileSettings(7, 487, 8084, session);

        // //   var v = client.LoadSearchField(session);

        // //   BaseUserDTO basDTO = new BaseUserDTO();
        // //   basDTO.FirstName = "ali";
        // //   basDTO.LastName = "noor";
        // //   basDTO.UserEmail = "waqaske.baber@quivus.com";
        // //   basDTO.TextPassword = "TestPWD";
        // //   basDTO.Gender = new GenderTypeDTO { GenderId = 1 };
        // //   basDTO.AccountStatus = new StatusTypeDTO { StatusID = 1 };
        // //   basDTO.ActiveUserType = new UserTypeDTO { UserTypeId = 1 };
        // //   basDTO.ClinicBranhId = 7;
        // //   basDTO.CreatedBy = 471;
        // //   basDTO.CreatedAt = DateTime.Now;

        // //   //    var fgp = client.RegisterBaseUser(basDTO, session);

        // //   SavePatient(749, client, session);

        // //   // OnlineReference.QuivusManagerServiceClient onlineClient = new OnlineReference.QuivusManagerServiceClient();

        // //   // var pingronline = onlineClient.PingAllClientServices();

        // //   // OnlineReference.UserSessionDTO onlinesession = onlineClient.LogLoginHistory(471, 3, 7, true);

        // //   //var onreslf= onlineClient.AuthenticateUser("test@quivus.com", "quivus");

        // //   //  OnlineReference.QuickInputResponse qpkObj = onlineClient.LoadQuickInputByVisit(4197, onlinesession);

        // //   //var frgt=  onlineClient.LoadPatientAllDiagnosisByPatientId(517, onlinesession);


        // //   //LogObject(qpkObj.ResponseData.Physical.Vitals);
        // //   //var dsfdsf=   client.DateTestServices(DateTime.Now);

        // //   var physicalsVisitList = client.LoadPatientPhysicalsByVisit(5245, session);


        // //   var physicalPatList = client.LoadPhysicalsByPatientId(8081, 7, session, stationId, true);
        // //   //     client.SyncEPrescription(8049, "", 7, 4952, session);
        // //   /*  var responseAll = client.LoadPatientAllergyByPatientId(625, session);
        // //   var dsfsdf = client.LoadPhysicalCategoryTypes(true,session);

        // //   client.LoadPatientVisitsByPatientIdAndClinicId(635, 7, true, session);
        // //   client.LoadPatientAllergyByPatientId(643, session);
        // //   var dfgdf = client.LoadPrescriptionMedicationByPatientId(620, session);
        // //  var dfgdf = client.LoadPrescriptionMedicationByPatientId(620, session);


        // //   var gfgdfg=client.LoadPrescriptionMedicationByPatientId(625, session);
        // // //  var fgfdgdf = client.LoadPatientMedicationPrescribedByPatient(625, session);
        // //   */

        // //   var responseMedd = client.LoadPatientMedicationByPatientId(625, session);
        // //   PatientEncountersResponse sdrt2 = client.LoadPatientVisitsByPatientIdAndClinicId(620, 7, true, session);

        // //   var response = client.SyncEPrescription(620, "", 7, 4629, session);










        // //   var dfd12s = client.LoadPhysicalCategoryType(session);

        // //   var dfd1s = client.LoadPhysicalCategoryTypesByGender(true, true, false, session);




        // //   var thfrgd = client.GetNotificationsForProvider("517", session);

        // //   PatientProfileSettingResponse bfdsf = client.LoadPatientProfileSettings(7, 572, 512, session);

        // //   // bfdsf.CompleteSettings.SectionThirdData.DiagnosisList[0].PatientHistoryMedicalID
        // //   //   var ftg = client.LoadPatientProfileSettings(7, 486, 517,session);

        // //   //     client.SyncExternalPatients(1, 5);

        // //   //     client.GetRegisterationStaticData(session);

        // //   var ssdsfs = client.LoadPatientCompleteProfileForClinic(413, 7, 586, session);

        // //   //UserManagerService.UserManagerServiceClient userMClient = new UserManagerService.UserManagerServiceClient();
        // //   //var basEDTO = userMClient.LoadBaseUserByUserId(493);


        // // //  userMClient.RegisterBaseUser(basEDTO);
        // //   //   var drr=client.LoadPatientProfileInfoForProvider(427, 7,471, session);

        // //   PatientProfileResponse cmpprofl = client.LoadPatientCompleteProfileForClinic(427, 7, 471, session);







        // //   var rsponEMail = client.GetUserEmailByUserId(594, session);


        // //   //  client.LoadPatientAllDiagnosisByPatientId(517, session);

        // //   var stks2 = client.GetRegisterationStaticData(session);

        // //   //   var familyhostory = client.LoadFamilyHistoryByPatientId(451,session);

        // //   //   ManagerPatientDiagnosis(client, session);



        // //   PatientEncountersResponse sdrt = client.LoadPatientVisitsByPatientIdAndClinicId(513, 7, true, session);






        // //   QuickInputResponse quickIN = client.LoadQuickInputByVisit(4285, session);
        // //   quickIN.ResponseData.PatientId = 413;
        // //   quickIN.ResponseData.VisitId = 4285;
        // //   quickIN.ResponseData.ActionDate = DateTime.Now;
        // //   quickIN.ResponseData.ClinicBranchId = 7;
        // //   // var refgty=  client.ManageQuickInputData(quickIN.ResponseData, 471, session);


        // //   SaveUpdateVisit(client, session);



        // ////   var clientPresc = new prescriptionServTest.EprescriptionAdapterServiceClient();

        // //   QuickInputDTO fgh = new QuickInputDTO();



        // //   //  var c = clientPresc.prepareLoginCallForEprescription("aaa");
        // //   //  LoginResponse pdsf = client.AuthenticateUser("john_tamjeed@quivus.com", "quivus");
        // //   QuickInputDTO quick = new QuickInputDTO();
        // //   //    quick.PMH.ComprehensiveHistory.MedicalHistory[0].
        // //   //   var xcml= client.GetResponseAsXML();

        // //   // var imm = client.LoadPatientImmunizationsByPatientId(451, 487, 3,session);

        // //   //   var plabs = client.LoadPatientLabsByPatientId(90,session);


        // //   var stks = client.GetRegisterationStaticData(session);



        // //   //   var mdd = client.LoadModuleDictationByPatientId(413, session);

        // //   //    var mddNN = client.LoadModuleNotesByPatientId(413, session);

        // //   var socialLoadByPatientId = client.LoadSocialHistoryByVisitId(3765, session);

        // //   PatientEncountersResponse ASD = client.LoadPatientVisitsByPatientIdAndClinicId(413, 7, true, session);



        // //   client.GetRegisterationStaticData(session);
        // //   //client.LoadFamilyHistoryByPatientId(413, session);

        // //   var asdas = client.LoadPatientVisitsByPatientIdAndClinicId(413, 7, true, session);

        // //   //   SaveQuickInputVisit(client, session);

        // //   var partda = client.LoadPatientProfileSettingsBySection(7, 470, 413, 3, session);



        // //   client.ManageQuickInputData(fgh, 80, session);

        // //   VisitDTO visit = new VisitDTO();

        // //   visit.UserId = 471;
        // //   //M visit.ClinicBranch = 7;
        // //   visit.EncounterTypeId = 1;
        // //   var createVist = client.CreateVisit(null, 471, session);


        // //   var qry = client.LoadAllModuleTypes(session);



        // //   PatientInHouseLabResponse dsfd = client.LoadPateintInHouseLabsByVisitId(2835, session);

        // //   var qpkObj34 = client.LoadQuickInputByVisit(2835, session);

        // //   //    var rosList = client.LoadROSCategoryTypes(false, session);

        // //   //   var loginresp = client.PrepareLoginCallForEprescription("doctorid", session);
        // //   //   var cpt = client.LoadCPTMedicalTypes("a", session);

        // //   //   PatientEncountersResponse esp = client.LoadPatientVisitsByPatientIdAndClinicId(80, 7, false,session);



        // //   PatientProfileResponse rsp = client.LoadPatientCompleteProfileForClinic(80, 7, 79, session);

        // //   EmailResponse ersp = client.GetUserEmailByUserId(452, session);

        // //   var resultfr = client.LoadSocialHistoryUsageAmountTypes(session);

        // //   var resultfr2 = client.LoadSocialHistoryMediumTypes(session);

        // //   var resultio = client.VerifyUser(1, "asdasdasdsad", session);

        // //   //    ROSMainCategoryTypeResponse rqsp=  client.LoadROSMainCategoryTypes(true,session);

        // //   LabDTO objjj = new LabDTO();


        // //   //ROSMainCategoryTypeDTO dto;



        // //   //   var fds = client.LoadHPIDataControlTypes(session);

        // //   //    var abc = client.LoadDiseaseTypes(7, 2, session);



        // //   //    client.ManagePatientVitals(labobj, session);



        // //   //client.ManagePatientVitals(VitalObject);
        // //   //   PatientLabResponse pkl=     client.ManagePatientLabs(labobj, session);

        // //   //  PatientLabResponse l3s = client.LoadPatientLabsByVisit(315, session);

        // //   ICDCodeResponse cdf = client.LoadICDCodeByName(session, "ex");
        // //   LabNatureTypeResponse drt = client.LoadLabNatureTypes(session);

        // //   LabTypeResponse drt2 = client.LoadLabTypes(session);
        // //   LabTestTypeResponse drt232 = client.LoadLabTestTypes(session);

        // //   LabTestPriorityTypeResponse drt4 = client.LoadLabTestPriorityTypes(session);
        // //   LabTestSpecimenTypeResponse drt3 = client.LoadLabTestSpecimenTypes(session);
        // //   LabNatureTypeResponse drt21 = client.LoadLabNatureTypes(session);


        // //   FamilyHistoryTypeResponse fg = client.LoadFamilyHistoryTypes(session);
        // //   DiseaseTypeResponse rserp = client.LoadDiseaseTypes(7, 1, session);
        // //   PatientProfileInfoResposne plo = client.LoadPatientProfileInfoForProvider(60, 7, 80, session);


        // //   AddressBookStaticDataResponse lo = client.GetAddressBookStaticData(session);

        // //   ProfileStaticDataResponse fr = client.LoadProfileStaticData(session);

        // //   MedicalHistoryTypeResponse trp = client.LoadMedicalHistoryTypes(session);

        // //   MedicalHistoryDTO mdo = new MedicalHistoryDTO { CreatedAt = DateTime.Now, CreatedBy = 3, MedicalHistoryTypeID = 1, VisitID = 1 };
        // //   // PatientMedicalHistoryResponse mkop = client.ManagePatientMedicalHistory(mdo, session);

        // //   //   AddressBookStaticDataResponse resp = client.GetAddressBookStaticData(session);


        // //   //    bool ping = client.PingService();
        // //   //   String result = cls.GetData(34);
        // //   //   QuivusManagerServiceClient client = new QuivusManagerServiceClient();

        // //   //   QuivusManagerServiceClient client  = new QuivusManagerServiceClient();

        // //   //   LoginResponse erp= client.AuthenticateUser("john12@quivus.com","quivus");

        // //   //  PatientProfileSettingResponse rfp=   client.LoadPatientProfileSettingsBySection(7, 82, 79, 1);

        // //   //rfp.CompleteSettings.SectionFirstData.t

        // //   //  LoginResponse erp1 = client.AuthenticateUser("john12@quivus.com", "quivus");

        // //   //PatientEncountersResponse resp = client.LoadPatientVisitsByPatientIdAndClinicId(102, 7, false);

        // //   //PatientProfileResponse rsp = client.LoadPatientCompleteProfileForClinic(80, 7, 79);
        // //   // //   LoginResponse resp =  client.AuthenticateUser("jphn","123");
        // //   //// client.SendEmail(80, 7, "Subjct", "muhammad.ismail@zigron.com", "sendEmail");

        // //   //PatientProfileSettingResponse reswp = client.LoadPatientProfileSettings(7, 80, 82);
        // //   //PatientProfileSettingDTO dto = new PatientProfileSettingDTO
        // //   //{
        // //   //    SectionNo = 3,
        // //   //    UserProfileCategoryID = 31,
        // //   //    ClinicBranchID = 7,
        // //   //    UserUserProfileCategoryID = 137,
        // //   //    UserID = 82,
        // //   //    CreatedBy = 82,
        // //   //    ModifiedBy = 82
        // //   //};
        // //   //client.ManagePatientProfileSettings(dto);
        // //   //client.LoadPatientProfileInfoForProvider(82, 7, 80);

        // //   //QuickLinkResponse qrsp = client.LoadUserDefaultQuickLinksbyLocation(82, 7);

        // //   //    SearchEngineResponse srsp = client.LoadUserSearchEnginebyLocation(82, 7);

        // //   // ImmunizationTypesResponse imrep=   client.LoadImmunizationTypes();


        // //   //  client.SetDefaultSearchEngine(82, 7, 1);
        // //   // ContactSearchResponse crsp= client.LoadUsersByClinicBranchID(7);
        // //   // EmailResponse ersp= client.GetUserEmailByUserId(82);

        // //   // SearchEngineResponse sresp= client.LoadAllSearchEngines();

        // //   //  RegistrationStaticDataResponse rrsp = client.GetRegisterationStaticData();

        // //   //  CityResponse cdrsp= client.GetCityListByState(44);
        // //   //    AllergyTypeResponse asp= client.SearchAllergyTypeByName("hyper");

        // //   //   ICDCodeResponse icrsp= client.SearchICDCodeByCode("520");

        // //   //   ProfileStaticDataResponse lrsp= client.LoadProfileStaticData();

        // //   //    ImageTypeResponse irsp= client.LoadImageTypes();

        // //   //  PatientEncountersResponse esp=   client.LoadPatientVisitsByPatientIdAndClinicId(80, 7, false);



        }


        //private static void ManagerPatientDiagnosis(QuivusManagerService.QuivusManagerServiceClient client, UserSessionDTO session)
        //{
        //    List<PatientDiagnosisDTO> diagnosisList = new List<PatientDiagnosisDTO>();

        //    PatientDiagnosisDTO diagObj = new PatientDiagnosisDTO();

        //    diagObj.CreatedBy = 471;
        //    diagObj.CreatedAt = DateTime.Now;
        //    diagObj.ICDCode = new ICDCodeDTO { IcdCodeIdSeq = 1 };
        //    diagObj.VisitID = 4362;
        //    diagObj.PatientID = 517;

        //    diagnosisList.Add(diagObj);

        //    try
        //    {
        //        var diagnosisResult = client.ManagePatientDiagnosis(diagnosisList, session);

        //    }
        //    catch (FaultException<QuivusFault> fault)
        //    {

        //    }
        //    catch (Exception exp)
        //    {

        //    }
        //}

        //private static void SaveUpdateVisit(QuivusManagerService.QuivusManagerServiceClient client, UserSessionDTO session)
        //{
        //    VisitDTO visitDTO = new VisitDTO();

        //    visitDTO.VisitId = 4452;// 4280;
        //    visitDTO.UserId = 471;
        //    visitDTO.ClinicBranch = new ClinicBranchDTO { ClinicBranchIdSeq = 7 };
        //    visitDTO.PatientId = 413;
        //    visitDTO.VisitStatus = "UnSigned";
        //    visitDTO.VisitDate = DateTime.Now;
        //    visitDTO.UserType = new UserTypeDTO { UserTypeId = 3 };
        //    visitDTO.EncounterTypeId = 1;
        //    visitDTO.TemplateId = 2;
        //    //   visitDTO.OwnerId = 471;
        //    //  visitDTO.OwnerUserType = new UserTypeDTO { UserTypeId = 3 };

        //    //   var rsult = client.CreateVisit(visitDTO, 470, session);
        //    var rsult = client.SaveVisit(visitDTO, 470, session);
        //}

        //private static void SaveQuickInputVisit(QuivusManagerService.QuivusManagerServiceClient client, UserSessionDTO session)
        //{
        //    int VisitID = 2835;

        //    MedicalHistoryDTO PateintMedicalHistoryObject = new MedicalHistoryDTO();

        //    //  PateintMedicalHistoryObject.PatientMedicalHistoryID = 4;
        //    PateintMedicalHistoryObject.CreatedBy = 80;
        //    PateintMedicalHistoryObject.CreatedAt = DateTime.Now;
        //    PateintMedicalHistoryObject.MedicalHistoryTypeID = 1;
        //    PateintMedicalHistoryObject.MedicalHistoryNote = "Notes$$$$$$$$$$";
        //    PateintMedicalHistoryObject.VisitID = VisitID;
        //    //    PateintMedicalHistoryObject.PatientMedicalHistoryDescription = "Descption-Updadddted";

        //    FamilyHistoryDTO familyObj = new FamilyHistoryDTO();

        //    //    familyObj.PatientFamilyHistoryID = 2;
        //    familyObj.CreatedAt = DateTime.Now;
        //    familyObj.CreatedBy = 80;
        //    familyObj.FamilyHistoryTypeID = 2;
        //    familyObj.VisitID = VisitID;
        //    familyObj.FamilyHistoryNote = "Notes$$$$$$$$$%%ewone";

        //    VitalDTO VitalObject = new VitalDTO();

        //    //    VitalObject.PatientVitalID = 1;
        //    VitalObject.CreatedAt = DateTime.Now;
        //    VitalObject.CreatedBy = 80;
        //    VitalObject.VitalNote = "updated";
        //    VitalObject.VisitID = VisitID;

        //    LabTestTypeResponse labtestList = client.LoadLabTestTypes(session);
        //    LabDTO labobj = new LabDTO();


        //    //    labobj.PatientLabID = 2;
        //    labobj.CreatedAt = DateTime.Now;
        //    labobj.Description = "inform";
        //    labobj.LabTestType = labtestList.ResponseList[0];
        //    labobj.VisitID = VisitID;
        //    labobj.Onset = DateTime.Now.ToString();
        //    labobj.CreatedBy = 80;
        //    labobj.ModifiedBy = 80;

        //    LIONICLabTestTypeResponse dfty = client.LoadLIONICLabTestType(1, "a", session);
        //    labobj.LIONICLabTestType = dfty.ResponseList[0];

        //    QuickInputDTO quickInputDTO = new QuickInputDTO();

        //    PMHDTO PMH = new PMHDTO();
        //    ComprehensiveHistoryDTO ComprehensiveHistory = new ComprehensiveHistoryDTO();

        //    List<MedicalHistoryDTO> MedicalHistory = new List<MedicalHistoryDTO>();
        //    List<FamilyHistoryDTO> FamilyHistory = new List<FamilyHistoryDTO>();


        //    PMH.ComprehensiveHistory = ComprehensiveHistory;

        //    ComprehensiveHistory.MedicalHistory = MedicalHistory;
        //    ComprehensiveHistory.FamilyHistory = FamilyHistory;

        //    MedicalHistory.Add(PateintMedicalHistoryObject);
        //    FamilyHistory.Add(familyObj);

        //    List<ImmunizationDTO> imList = new List<ImmunizationDTO>();

        //    ImmunizationTypesResponse imListType = client.LoadImmunizationTypes(session);

        //    ImmunizationDTO imobj = new ImmunizationDTO();

        //    imobj.ImmunizationType = imListType.ImmunzationTypeList[0];
        //    imobj.CreatedBy = 80;
        //    imobj.VisitId = VisitID;
        //    imobj.CreatedAt = DateTime.Now;
        //    imobj.ManagedBy = 80;
        //    imobj.VisitId = 9;

        //    imobj.Note = "only%%%%%%%Record";
        //    imList.Add(imobj);

        //    PMH.Immunization = imList;

        //    PhysicalDTO Physical = new PhysicalDTO();

        //    List<InHouseLabDTO> inhouseLabsList = new List<InHouseLabDTO>();
        //    InHouseLabDTO inHouseObj = new InHouseLabDTO();

        //    InHouseLabTestTypeResponse inhouseTypes = client.LoadInHouseLabTestTypeByClinicBranchId(7, session);

        //    inHouseObj.InHouseLabTestType = inhouseTypes.ResponseList[0];

        //    inHouseObj.Notes = "$$$$$$$$$$$$$$$$Notes%^&**^%$$$$$$@@#$%^^^";

        //    Image image = Image.FromFile("toto.png");

        //    MemoryStream ms = new MemoryStream();
        //    image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);


        //    inHouseObj.ResultAttachment = ms.ToArray();


        //    inhouseLabsList.Add(inHouseObj);
        //    Physical.InHouseLabs = inhouseLabsList;

        //    quickInputDTO.Physical = Physical;

        //    Physical.Vitals = VitalObject;

        //    List<LabDTO> labs = new List<LabDTO>();

        //    labs.Add(labobj);

        //    Physical.Labs = labs;

        //    //  quickInputDTO.PMH = PMH;

        //    List<PatientDiagnosisDTO> diagLst = new List<PatientDiagnosisDTO>();
        //    AssessmentDTO assesmentDTO = new AssessmentDTO();
        //    PatientDiagnosisDTO diag = new PatientDiagnosisDTO();

        //    //   diag.PatientDiagnosisID = 41;
        //    //    diag.IsDeleted = true;
        //    diag.Note = "diagzxczxczxcnote";
        //    diag.NoteSubject = "diagsubsubject";
        //    diag.IsChronic = true;
        //    diag.ICDCode = new ICDCodeDTO { IcdCodeIdSeq = 5 };
        //    diagLst.Add(diag);

        //    assesmentDTO.Diagnosis = diagLst;

        //    quickInputDTO.Assessment = assesmentDTO;

        //    quickInputDTO.VisitId = VisitID;
        //    quickInputDTO.PatientId = 79;
        //    quickInputDTO.ActionDate = DateTime.Now;
        //    quickInputDTO.ClinicBranchId = 7;


        //    ManageVisitResponse visit = client.ManageQuickInputData(quickInputDTO, 80, session);
        }

        //static void SavePatient(int userId, QuivusManagerServiceClient client, UserSessionDTO session)
        //{
        //    try
        //    {

        //        PatientDTO patientDTO = new PatientDTO();

        //        patientDTO.MaritalStatusType = new MaritalStatusTypeDTO();
        //        patientDTO.MaritalStatusType.MaritalStatusId = 1;

        //        patientDTO.DateOfBirth = DateTime.Now;
        //        patientDTO.QuivusId = "quivusId";
        //        patientDTO.AllowEmailCommunication = false;
        //        patientDTO.SpouseName = "spouse";
        //        patientDTO.MotherMaidenName = "mother2";
        //        patientDTO.FatherName = "father2";
        //        patientDTO.MedicareNumber = "medicare";
        //        patientDTO.IVR_PINCODE = 1234;

        //        patientDTO.StatusType = new StatusTypeDTO();
        //        patientDTO.StatusType.StatusID = 1;
        //        patientDTO.IsReportingExempt = false;

        //        patientDTO.RaceType = new RaceTypeDTO();
        //        patientDTO.RaceType.RaceId = 1;

        //        patientDTO.EthinicityType = new EthinicityTypeDTO();
        //        patientDTO.EthinicityType.EthinicityId = 1;

        //        patientDTO.OccupationType = new OccupationTypeDTO();
        //        patientDTO.OccupationType.OccupationId = 1;

        //        patientDTO.AlcoholStatus = "";
        //        patientDTO.DrugStatus = "";
        //        patientDTO.EmployerName = "zigron";
        //        patientDTO.IsProfileComplete = true;

        //        patientDTO.SmokingType = new SmokingTypeDTO();
        //        patientDTO.SmokingType.SmokingTypeId = 1;
        //        patientDTO.CreatedBy = 1;

        //        List<PatientLanguageDTO> langList = new List<PatientLanguageDTO>();

        //        PatientLanguageDTO objLang = new PatientLanguageDTO();

        //        objLang.LanguageID = 113;
        //        objLang.IsPreffered = false;

        //        langList.Add(objLang);

        //        client.SavePatientBasicProfileAndLangauge(userId, 472, patientDTO, langList, session);



        //        //client.AuthenticateUser("john@quivus.com", "quivus");
        //        //client.LogLoginHistory(3, 2, 1, true);
        //        //client.SendEmail(3, 1, "test", "some1shamsi@gmail.com", "test message");
        //        //client.LoadUserDefaultQuickLinksbyLocation(3, 1);
        //        //client.LoadUsersByClinicBranchID(1);
        //        //client.LoadUserDefaultQuickLinksbyLocation(3, 1);

        //        //client.GetRegisterationStaticData();

        //        // client.GetAddressBookStaticData();
        //        // client.GetCityListByState(1);

        //    }
        //    catch (Exception ex)
        //    {
        //        // ShowFaultContract(ex);
        //        // ShowExceptionDetails(ex);
        //    }

        //    Console.ReadLine();
        //}


//    }
}
