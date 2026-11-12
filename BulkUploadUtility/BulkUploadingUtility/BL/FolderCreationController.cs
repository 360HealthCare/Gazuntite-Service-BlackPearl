using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.UserManagerService;
using BulkUploadingUtility.DocumentManagerService;
using BulkUploadingUtility.Model;
using BulkUploadingUtility.Model.Response;
using System.IO;
using BulkUploadingUtility.Utility;

namespace BulkUploadingUtility.BL
{
    public class FolderCreationController
    {
        public static List<DocumentSectionTypeDTO> SectionList = null;

        public static event EventHandler LogControl;


        public List<ClinicBranchDTO> Login(string providerID, string password)
        {
            List<ClinicBranchDTO> response = null;
            List<UserClinicBranchDTO> userClinicBranchesList = null;
            UserManagerServiceClient userManagerClient = null;
            try
            {
                if (userManagerClient == null)
                {
                    userManagerClient = new UserManagerServiceClient();
                }
                Session.CurrentUser = userManagerClient.AuthenticateUser(providerID, password);
                if (Session.CurrentUser != null)
                {
                    userClinicBranchesList = Session.CurrentUser.UserClinicBranchesList;
                    foreach (UserClinicBranchDTO userClinicBranch in userClinicBranchesList)
                    {
                        ClinicBranchDTO clinicBranch = userClinicBranch.ClinicBranch;
                        if (response == null)
                        {
                            response = new List<ClinicBranchDTO>();
                            ClinicBranchDTO tempClinic = new ClinicBranchDTO();
                            tempClinic.ClinicBranchIdSeq = 0;
                            tempClinic.ClinicBranchName = "Please select practice";
                            response.Add(tempClinic);
                        }
                        response.Add(clinicBranch);
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public bool IsUserLogin()
        {
            bool response = false;
            try
            {
                if (Session.CurrentUser != null)
                {
                    if (Session.CurrentClinicBranchID > 0)
                    {
                        response = true;
                    }
                }
            }
            catch
            {
            }
            return response;
        }

        public List<PatSearch> SearchPatient(string qurey)
        {
            List<PatSearch> searchList = null;
            try
            {
                int clinicBranchID = Session.CurrentClinicBranchID;
                int providerID = Session.CurrentProviderID;
                int userTypeID = Session.CurrentUserTypeID;
                int pageNo = 1;
                UserManagerServiceClient userClient = new UserManagerServiceClient();
                PatientSearchResultDTO patientSearchResult = userClient.SearchPatientbyClinicIdAndSearchString(clinicBranchID, providerID, userTypeID, qurey, pageNo);
                searchList = ConvertToPatSearchList(patientSearchResult);
            }
            catch
            {
                throw;
            }
            return searchList;
        }

        public void SetCurrentPractice(int clinicBranchID)
        {
            List<UserClinicBranchDTO> userClinicBranchesList = null;
            try
            {
                userClinicBranchesList = Session.CurrentUser.UserClinicBranchesList;
                foreach (UserClinicBranchDTO userClinicBranch in userClinicBranchesList)
                {
                    ClinicBranchDTO clinicBranch = userClinicBranch.ClinicBranch;
                    UserTypeDTO userType = userClinicBranch.ClinicBranchUserRole.UserTypeRole;
                    UserClinicRoleDTO clinicBranchUserRole = userClinicBranch.ClinicBranchUserRole;
                    Session.StationList = userClinicBranch.StationList;
                    if (clinicBranch.ClinicBranchIdSeq == clinicBranchID)
                    {
                        Session.CurrentUserTypeID = userType.UserTypeId;
                        Session.CurrentClinicBranchName = clinicBranch.ClinicBranchName;
                        Session.CurrentClinicBranchID = clinicBranch.ClinicBranchIdSeq;
                        Session.CurrentProviderID = clinicBranchUserRole.UserTypeProfileID;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void Logout()
        {
            try
            {
                Session.CurrentUser = null;
                Session.CurrentClinicBranchName = string.Empty;
                Session.CurrentClinicBranchID = 0;
                Session.CurrentProviderID = 0;
                Session.CurrentUserTypeID = 0;
            }
            catch
            {
                throw;
            }
        }

        public List<PatSearch> ConvertToPatSearchList(PatientSearchResultDTO patientSearchResult)
        {
            List<PatSearch> searchList = null;
            List<PatientSearchDTO> searchResultList = null;
            try
            {
                searchResultList = patientSearchResult.PatientSearchList;
                foreach (PatientSearchDTO searchResult in searchResultList)
                {
                    PatSearch patSearch = new PatSearch();
                    patSearch.DOB = searchResult.DOB;
                    patSearch.FirstName = searchResult.FirstName;
                    patSearch.LastName = searchResult.LastName;
                    patSearch.GenderId = searchResult.GenderId;
                    patSearch.GenderName = searchResult.GenderName;
                    patSearch.IsAuthorized = searchResult.IsAuthorized;
                    patSearch.IsChatIdAvailable = searchResult.IsChatIdAvailable;
                    patSearch.IsEmailIdAvailable = searchResult.IsEmailIdAvailable;
                    patSearch.IsPhoneNoAvailable = searchResult.IsPhoneNoAvailable;
                    patSearch.IsSMSNoAvalable = searchResult.IsSMSNoAvalable;
                    patSearch.IsSnailIdAvailable = searchResult.IsSnailIdAvailable;
                    patSearch.MiddleName = searchResult.MiddleName;
                    patSearch.PatientId = searchResult.PatientId;
                    patSearch.QuivusID = searchResult.QuivusID;
                    patSearch.SSN = searchResult.SSN;
                    patSearch.UserId = searchResult.UserId;

                    if (searchList == null)
                    {
                        searchList = new List<PatSearch>();
                    }
                    searchList.Add(patSearch);
                }
            }
            catch
            {
                throw;
            }
            return searchList;
        }
        #region Folder Creation Methods

        public CreateFolderReponse CreateFolder(List<PatSearch> patSearchList, string drive)
        {
            CreateFolderReponse response = new CreateFolderReponse();
            //CreateFolderReponse stationFolderResponse = null;
            CreateFolderReponse patientFolderResponse = null;
            CreateFolderReponse SectionFolderResponse = null;
            string root = drive + UConfigurationManager.Root;
            string vender = UConfigurationManager.VendorName;
            try
            {
                //foreach (DriveInfo drveInfo in DriveInfo.GetDrives())
                //{
                //    if (drveInfo.Name.Contains('D'))
                //    {
                //        root = UConfigurationManager.Root;
                //        break;
                //    }
                //}

                //if (string.IsNullOrEmpty(root))
                //{
                //    root = UConfigurationManager.OptionalRoot;
                //}
                bool havePermission = true;
                if (!Directory.Exists(root))
                {
                    try
                    {
                        LogControl("Creating folder " + root, EventArgs.Empty);
                        Directory.CreateDirectory(root);
                    }
                    catch (UnauthorizedAccessException uae)
                    {
                        //response.Msg += "User do not have permission to create folder.\n";
                        LogControl("User do not have permission to create folder.", EventArgs.Empty);
                        havePermission = false;
                    }
                }

                if (havePermission)
                {
                    string directory = root + vender + Contants.DIR_SEPRATOR;
                    if (!Directory.Exists(directory))
                    {
                        try
                        {
                            LogControl("Creating folder " + directory, EventArgs.Empty);
                            Directory.CreateDirectory(directory);
                        }
                        catch (UnauthorizedAccessException uae)
                        {
                            //response.Msg += "User do not have permission to create folder.\n";
                            LogControl("User do not have permission to create folder.", EventArgs.Empty);
                            havePermission = false;
                        }
                    }

                    if (havePermission)
                    {
                        directory = directory + Session.CurrentClinicBranchName;
                        if (!Directory.Exists(directory))
                        {
                            try
                            {
                                Directory.CreateDirectory(directory);
                            }
                            catch (UnauthorizedAccessException uae)
                            {
                                //response.Msg += "User do not have permission to create folder.\n";
                                LogControl("User do not have permission to create folder.", EventArgs.Empty);
                                havePermission = false;
                            }
                        }

                        if (havePermission)
                        {
                            //stationFolderResponse = CreateStationFolder(Session.StationList, directory);
                            patientFolderResponse = CreatePatientFolder(Session.StationList, patSearchList, directory);
                            //if (patientFolderResponse.Msg == null || patientFolderResponse.Msg.Length == 0)
                            SectionFolderResponse = CreateSectionFolder(Session.StationList, patSearchList, directory);
                            response.TotalFolderCreated = /*stationFolderResponse.TotalFolderCreated +*/ patientFolderResponse.TotalFolderCreated + SectionFolderResponse.TotalFolderCreated;
                            response.TotalExistingFolder = /*stationFolderResponse.TotalExistingFolder +*/ patientFolderResponse.TotalExistingFolder + SectionFolderResponse.TotalExistingFolder;

                           /* response.Msg += patientFolderResponse.Msg + "\n";
                            if (SectionFolderResponse != null)
                                response.Msg += SectionFolderResponse.Msg;
                            * */
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public CreateFolderReponse CreateStationFolder(List<StationDTO> stationList, string directory)
        {
            CreateFolderReponse response = new CreateFolderReponse();
            try
            {
                foreach (StationDTO station in stationList)
                {
                    string stationDirectory = directory + Contants.DIR_SEPRATOR + station.StationName;
                    if (!Directory.Exists(stationDirectory))
                    {
                        Directory.CreateDirectory(stationDirectory);
                        response.TotalFolderCreated++;
                    }
                    else
                    {
                        response.TotalExistingFolder++;
                    }
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public CreateFolderReponse CreatePatientFolder(List<StationDTO> stationList, List<PatSearch> patSearchList, string directory)
        {
            CreateFolderReponse response = new CreateFolderReponse();
            try
            {
                //foreach (StationDTO station in stationList)
                {
                    LogControl("STARTING PATIENT FOLDER CREATION PROCESS...", EventArgs.Empty);
                    string patientDirectory = directory + Contants.DIR_SEPRATOR /*+ station.StationName + Contants.DIR_SEPRATOR*/;
                    foreach (PatSearch patsearch in patSearchList)
                    {
                        string patDOB = patsearch.DOB.ToShortDateString();
                        patDOB = patDOB.Replace('/', '-');
                        string patientFolderName = patsearch.LastName + "," + patsearch.FirstName + "," + patDOB + "," + patsearch.QuivusID;
                        patientDirectory = directory + Contants.DIR_SEPRATOR /*+ station.StationName + Contants.DIR_SEPRATOR */+ patientFolderName;
                        if (!Directory.Exists(patientDirectory))
                        {


                            int i = 1;
                            while (!Directory.Exists(patientDirectory))
                            {
                                if (i > 3)
                                    break;
                                try
                                {
                                    LogControl("Creating folder for patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                    Directory.CreateDirectory(patientDirectory);
                                    LogControl("Successfully created folder for patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                }
                                catch (UnauthorizedAccessException uae)
                                {
                                    //response.Msg += "User do not have permission to create folder.\n";
                                    LogControl("User do not have permission to create folder.", EventArgs.Empty);
                                    break;
                                }

                                i++;

                            }

                            if (Directory.Exists(patientDirectory))
                                response.TotalFolderCreated++;
                            else
                            {
                                //response.Msg += "Unable to create the Diectory for the patient " + patsearch.LastName + "," + patsearch.FirstName + "." + "\n";
                                LogControl("Unable to create the Diectory for the patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                break;
                            }

                        }
                        else
                        {
                            response.TotalExistingFolder++;
                        }
                    }
                    LogControl("PATIENT FOLDER CREATION PROCESS COMPLETED...", EventArgs.Empty);
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        public CreateFolderReponse CreateSectionFolder(List<StationDTO> stationList, List<PatSearch> patSearchList, string directory)
        {
            CreateFolderReponse response = new CreateFolderReponse();
            try
            {
                //foreach (StationDTO station in stationList)
                {
                    LogControl("SECTION FOLDER CREATION PROCESS STARTED...", EventArgs.Empty);
                    string patientDirectory = directory + Contants.DIR_SEPRATOR /*+ station.StationName + Contants.DIR_SEPRATOR*/;
                    foreach (PatSearch patsearch in patSearchList)
                    {
                        string patDOB = patsearch.DOB.ToShortDateString();
                        patDOB = patDOB.Replace('/', '-');
                        string patientFolderName = patsearch.LastName + "," + patsearch.FirstName + "," + patDOB + "," + patsearch.QuivusID;
                        bool breakPatientLoop = false;
                        patientDirectory = directory + Contants.DIR_SEPRATOR + /*station.StationName + Contants.DIR_SEPRATOR +*/ patientFolderName + Contants.DIR_SEPRATOR;
                        if (!Directory.Exists(patientDirectory + "Archive"))
                        {
                            Directory.CreateDirectory(patientDirectory + "Archive");
                        }
                        GetSections();
                        try
                        {
                            foreach (DocumentSectionTypeDTO section in SectionList)
                            {
                                string sectionDirectory = patientDirectory + section.SectionName;
                                if (!Directory.Exists(sectionDirectory))
                                {
                                    int i = 1;
                                    while (!Directory.Exists(sectionDirectory))
                                    {
                                        if (i > 3)
                                            break;
                                        try
                                        {
                                            //response.Msg += "Creating folder for section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName;
                                            LogControl("Creating folder for section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                            Directory.CreateDirectory(sectionDirectory);
                                            LogControl("Successfully created folder for section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                            //response.Msg += "Seccessfully created folder for section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName;
                                        }
                                        catch (UnauthorizedAccessException uae)
                                        {
                                            //response.Msg = "User do not have permission to create folder.\n";
                                            LogControl("User do not have permission to create folder", EventArgs.Empty);
                                            break;
                                            breakPatientLoop = true;
                                        }
                                        catch (Exception ex)
                                        {

                                        }


                                        i++;

                                    }

                                    if (Directory.Exists(sectionDirectory))
                                        response.TotalFolderCreated++;
                                    else
                                    {
                                        //response.Msg = "Unable to create the section diectory for the patient " + patsearch.LastName + "," + patsearch.FirstName + ".";
                                        LogControl("Unable to create the section diectory for the patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                        break;
                                        breakPatientLoop = true;

                                    }
                                }
                                else
                                {
                                    response.TotalExistingFolder++;
                                }
                                if (section.ChildSectionType != null)
                                {
                                    foreach (DocumentSectionTypeDTO subSection in section.ChildSectionType)
                                    {
                                        string subSectionDirectory = sectionDirectory + Contants.DIR_SEPRATOR + subSection.SectionName;
                                        if (!Directory.Exists(subSectionDirectory))
                                        {

                                            int i = 1;
                                            while (!Directory.Exists(subSectionDirectory))
                                            {
                                                if (i > 3)
                                                    break;
                                                try
                                                {
                                                    //response.Msg += "Creating folder for sub section " + subSection.SectionName + " of section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName;
                                                    LogControl("Creating folder for sub section " + subSection.SectionName + " of section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                                    Directory.CreateDirectory(subSectionDirectory);
                                                    LogControl("Successfully created folder for sub section " + subSection.SectionName + " of section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                                    //response.Msg += "Successfully created folder for sub section " + subSection.SectionName + " of section " + section.SectionName + " for patient " + patsearch.LastName + "," + patsearch.FirstName;
                                                }
                                                catch (UnauthorizedAccessException uae)
                                                {
                                                    //response.Msg = "User do not have permission to create folder.\n";
                                                    LogControl("User do not have permission to create folder", EventArgs.Empty);
                                                    break;
                                                    breakPatientLoop = true;
                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                                i++;

                                            }

                                            if (Directory.Exists(sectionDirectory))
                                                response.TotalFolderCreated++;
                                            else
                                            {
                                                //response.Msg = "Unable to create the Sub Section diectory for the patient " + patsearch.LastName + "," + patsearch.FirstName + ".";
                                                LogControl("Unable to create the Sub Section diectory for the patient " + patsearch.LastName + "," + patsearch.FirstName, EventArgs.Empty);
                                                break;
                                                breakPatientLoop = true;
                                            }



                                        }
                                        else
                                        {
                                            response.TotalExistingFolder++;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }

                        if (breakPatientLoop)
                            break;

                    }
                    LogControl("SECTION FOLDER CREATION PROCESS COMPLETED...", EventArgs.Empty);
                }
            }
            catch
            {
                throw;
            }
            return response;
        }

        #endregion
        public void GetSections()
        {
            DocumentManagerServiceClient DocManagerService = new DocumentManagerServiceClient();
            try
            {
                if (SectionList == null)
                {
                    SectionList = DocManagerService.LoadDocumentSectionTypes();
                }
            }
            catch
            {
                throw;
            }
        }

        public CreateFolderReponse CreateAllFolder(string drive)
        {
            int pageSize = 200;
            int pageNo = 1;
            int totalRecord = 0;
            int lastRecord = 0;
            int firstRecord = 0;
            CreateFolderReponse response = null;
            UserManagerServiceClient userManagerClient = new UserManagerServiceClient();
            try
            {
                do
                {
                    PatientSearchResultDTO searchResult = userManagerClient.LoadUsersByPracticeId(Session.CurrentClinicBranchID, pageNo, pageSize);
                    totalRecord = searchResult.TotalNoOfRecords;
                    firstRecord = searchResult.CurrentFirstRecordNo;
                    //lastRecord = searchResult.CurrentLastRecordNo;
                    lastRecord = firstRecord + searchResult.PatientSearchList.Count - 1;
                    response = CreateFolder(ConvertToPatSearchList(searchResult), drive);
                    pageNo++;

                }
                while (totalRecord > lastRecord);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<Document> SearchPatientFolder(string qurey)
        {
            List<Document> searchList = null;
            try
            {
                string root = InfoStore.ReadDriveInfo() + UConfigurationManager.Root;
                DirectoryInfo directory = new DirectoryInfo(root);
                foreach (DirectoryInfo fstLvlDirectory in directory.GetDirectories())
                {
                    string vendorDocuments = fstLvlDirectory.Name;
                    foreach (DirectoryInfo secLvlDirectory in fstLvlDirectory.GetDirectories())
                    {
                        string pratice = secLvlDirectory.Name;
                        foreach (DirectoryInfo thrdLvlDirectory in secLvlDirectory.GetDirectories(qurey, SearchOption.TopDirectoryOnly))
                        {
                            string patient = thrdLvlDirectory.Name;
                            //LogControl("Searching patient " + patient, EventArgs.Empty);
                            Document doc = new Document();
                            doc.VendorDocuments = vendorDocuments;
                            doc.Pratice = pratice;
                            doc.Patient = patient;
                            if (searchList == null)
                            {
                                searchList = new List<Document>();
                            }
                            searchList.Add(doc);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return searchList;
        }
    }
}
