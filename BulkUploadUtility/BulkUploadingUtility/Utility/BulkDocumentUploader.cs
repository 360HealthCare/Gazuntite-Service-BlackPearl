using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulkUploadingUtility.Model;
using System.Net;
using System.IO;
using System.Configuration;
using BulkUploadingUtility.DocumentManagerService;
using BulkUploadingUtility.UserManagerService;
using BulkUploadingUtility.BL;
using BulkUploadingUtility.Model.Response;

namespace BulkUploadingUtility.Utility
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 07/20/2011
    /// This class will perform operation for uploading
    /// </summary>

    public class BulkDocumentUploader
    {
        /// <summary>
        /// Author Sahibzada Sami ud din
        /// 7/20/2011
        /// This methos will create directory at FTP server.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        /// 
       // public static event EventHandler LogControl;
        public static event EventHandler FileUpLoaded;
        public static event EventHandler LogControl;
        public List<DocumentSectionTypeDTO> secList = null;
        int fileCount = 0;

        public bool MakeDirectory(Document doc)
        {
            bool response = false;
            try
            {
                ServiceOperator.ManageLog("Calling Make Directory", "", false);
                response = CreateDirectory(DirectoryOperation.makeDirectoryUrlFstLvl(doc));
                response = CreateDirectory(DirectoryOperation.makeDirectoryUrlSecLvl(doc));
                // response = CreateDirectory(DirectoryOperation.makeDirectoryUrlThrdLvl(doc));
                response = CreateDirectory(DirectoryOperation.makeDirectoryUrlFourthLvl(doc));
                response = CreateDirectory(DirectoryOperation.makeDirectoryUrl(doc));
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud din
        /// Date 07/20/2011
        /// This methos will create request object.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>

        public FtpWebRequest CreateRequestObj(string directory)
        {
            ServiceOperator.ManageLog("Calling Create Request Obj", "", false);
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directory);
                request.Credentials = new NetworkCredential(UConfigurationManager.FTPUserName, UConfigurationManager.FTPPassword);
                request.KeepAlive = false;
                request.UsePassive = true;
                request.Timeout = 1000000000;
                request.ReadWriteTimeout = 1000000000;
                return request;
            }
            catch
            {
                throw;
            }
        }

        private bool CreateDirectory(string directory)
        {
            ServiceOperator.ManageLog("Calling CreateDirectory", "", false);
            bool response = false;
            FtpWebResponse ftpResponse = null;
            FtpWebRequest request = null;
            int count = 0;
            while (count < 3)
            {
                try
                {
                    request = CreateRequestObj(directory);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    ftpResponse = (FtpWebResponse)request.GetResponse();
                    response = true;
                    ftpResponse.Close();
                    break;
                }
                catch (WebException ex)
                {
                    FtpWebResponse responseCode = (FtpWebResponse)ex.Response;
                    if (responseCode.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {

                        response = true;
                        break;
                        //ftpResponse.Close();
                    }
                    else
                    {
                        ftpResponse.Close();
                        response = false;
                        //count++;
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// 07/20/2011
        /// This Method will get files from local directory.
        /// </summary>
        /// <returns></returns>

        public DocumentsUploadResponse ManageFiles(bool isAllUpload, string selectedPath, Document selectedDocument)
        {
            
            ServiceOperator.ManageLog("Calling GetFiles", "", false);

            DocumentsUploadResponse response = null;
            try
            {
                response = new DocumentsUploadResponse();

                #region All Files Upload

                string root = string.Empty;

                if (isAllUpload)
                {
                    root = InfoStore.ReadDriveInfo() + UConfigurationManager.Root;
                    DirectoryInfo directory = new DirectoryInfo(root);
                    #region Scaning Vendor, Practice and patient folders
                    foreach (DirectoryInfo fstLvlDirectory in directory.GetDirectories())
                    {
                        string vendorDocuments = fstLvlDirectory.Name;
                        foreach (DirectoryInfo secLvlDirectory in fstLvlDirectory.GetDirectories())
                        {
                            string pratice = secLvlDirectory.Name;
                            foreach (DirectoryInfo thrdLvlDirectory in secLvlDirectory.GetDirectories())
                            {
                                //foreach (DirectoryInfo fourthLvlDirectory in thrdLvlDirectory.GetDirectories())
                                {
                                    string patient = thrdLvlDirectory.Name;
                                    LogControl("Scanning patient " + patient + " for files", EventArgs.Empty);
                                    ++response.TotalPatientScaned;
                                    bool isNewPatient = true;
                                    #region Scaning section and Sub section
                                    foreach (DirectoryInfo fifthLvlDirectory in thrdLvlDirectory.GetDirectories())
                                    {

                                        if (!IsSection(fifthLvlDirectory.Name))
                                        {
                                            LogControl("Skipping folder " + fifthLvlDirectory.Name + " for patient " + patient, EventArgs.Empty);
                                            continue;
                                        }
                                        string section = fifthLvlDirectory.Name;
                                        Document doc = new Document();
                                        doc.VendorDocuments = vendorDocuments;
                                        doc.Pratice = pratice;
                                        doc.Patient = patient;
                                        doc.Section = section;
                                        LogControl("Scanning section " + section + " for patient" + patient + " for files", EventArgs.Empty);
                                        foreach (FileInfo file in fifthLvlDirectory.GetFiles())
                                        {
                                            string msg = "Scanning file " + file.Name + " in section " + section + " for patient" + patient;
                                            UploadedFileInfo uploadedFile = new UploadedFileInfo();
                                            LogControl(msg, EventArgs.Empty);
                                            UploadedFileInfo fileInfo = new UploadedFileInfo();
                                            fileInfo.FileName = file.Name;
                                            fileInfo.VirtualFileName = Guid.NewGuid();
                                            FileStream fs = file.OpenRead();
                                            byte[] contents = new byte[Contants.DOCUMENT_MANAGEMENT_DOC_SIZE];
                                            BinaryReader br = new BinaryReader(fs);
                                            contents = br.ReadBytes((int)fs.Length);
                                            br.Close();
                                            fs.Close();
                                            fileInfo.FileContent = contents;
                                            if (doc.FileList == null)
                                            {
                                                doc.FileList = new List<UploadedFileInfo>();
                                            }
                                            uploadedFile = SaveDocumentInfo(doc, fileInfo);
                                            UploadFile(doc, uploadedFile);
                                            if (isNewPatient)
                                            {
                                                ++response.TotalPatientUploaded;
                                                isNewPatient = false;
                                            }
                                            ++response.TotalFilesUploaded;
                                        }
                                        foreach (DirectoryInfo sixthLvlDirectory in fifthLvlDirectory.GetDirectories())
                                        {
                                            string subSection = sixthLvlDirectory.Name;
                                            Document document = new Document();
                                            document.VendorDocuments = vendorDocuments;
                                            document.Pratice = pratice;
                                            document.Patient = patient;
                                            document.SubSection = subSection;
                                            document.Section = section;

                                            foreach (FileInfo file in sixthLvlDirectory.GetFiles())
                                            {
                                                UploadedFileInfo uploadedFile = new UploadedFileInfo();
                                                string msg = "Scanning file " + file.Name + " in sub-section " + subSection + " of section " + section + " for patient" + patient;
                                                LogControl(msg, EventArgs.Empty);
                                                UploadedFileInfo fileInfo = new UploadedFileInfo();
                                                fileInfo.FileName = file.Name;
                                                fileInfo.VirtualFileName = Guid.NewGuid();
                                                FileStream fs = file.OpenRead();
                                                byte[] contents = new byte[Contants.DOCUMENT_MANAGEMENT_DOC_SIZE];
                                                BinaryReader br = new BinaryReader(fs);
                                                contents = br.ReadBytes((int)fs.Length);
                                                br.Close();
                                                fs.Close();
                                                fileInfo.FileContent = contents;
                                                if (document.FileList == null)
                                                {
                                                    document.FileList = new List<UploadedFileInfo>();
                                                }
                                                uploadedFile = SaveDocumentInfo(document, fileInfo);
                                                UploadFile(document, uploadedFile);
                                                //uploadedFile = SaveDocumentInfo(doc, fileInfo);
                                                //UploadFile(doc, uploadedFile);
                                                if (isNewPatient)
                                                {
                                                    ++response.TotalPatientUploaded;
                                                    isNewPatient = false;
                                                }
                                                ++response.TotalFilesUploaded;
                                            }

                                        }
                                    }
                                    #endregion

                                    LogControl("Scanning for patient " + patient + " is complete", EventArgs.Empty);
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                #region Selected File Upload
                else
                {
                    response = new DocumentsUploadResponse();
                    response.TotalPatientScaned++;
                    bool isNewPatient = true;
                    DirectoryInfo fourthLvlDirectory = new DirectoryInfo(selectedPath);
                    #region Section and Sub Section Scaning
                    foreach (DirectoryInfo fifthLvlDirectory in fourthLvlDirectory.GetDirectories())
                    {
                        if (!IsSection(fifthLvlDirectory.Name))
                        {
                            LogControl("Skipping folder " + fifthLvlDirectory.Name + " for patient " + selectedDocument.Patient, EventArgs.Empty);
                            continue;
                        }

                        string section = fifthLvlDirectory.Name;

                        LogControl("Scanning section " + section + " for patient " + selectedDocument.Patient + " for files", EventArgs.Empty);
                        Document doc = new Document();
                        doc.VendorDocuments = selectedDocument.VendorDocuments;
                        doc.Pratice = selectedDocument.Pratice;
                        //doc.Station = selectedDocument.Station;
                        doc.Patient = selectedDocument.Patient;
                        doc.Section = section;
                        foreach (FileInfo file in fifthLvlDirectory.GetFiles())
                        {
                            string msg = "Scanning file " + file.Name + " in section " + section + " for patient " + selectedDocument.Patient;
                            UploadedFileInfo fileInfo = new UploadedFileInfo();
                            UploadedFileInfo uploadedFile = new UploadedFileInfo();
                            fileInfo.FileName = file.Name;
                            fileInfo.VirtualFileName = Guid.NewGuid();
                            FileStream fs = file.OpenRead();
                            byte[] contents = new byte[Contants.DOCUMENT_MANAGEMENT_DOC_SIZE];
                            BinaryReader br = new BinaryReader(fs);
                            contents = br.ReadBytes((int)fs.Length);
                            br.Close();
                            fs.Close();
                            fileInfo.FileContent = contents;
                            if (doc.FileList == null)
                            {
                                doc.FileList = new List<UploadedFileInfo>();
                            }
                            //doc.FileList.Add(fileInfo);
                            uploadedFile = SaveDocumentInfo(doc, fileInfo);
                            UploadFile(doc, uploadedFile);
                            if (isNewPatient)
                            {
                                ++response.TotalPatientUploaded;
                                isNewPatient = false;
                            }
                            ++response.TotalFilesUploaded;
                        }
                        //if (documentList == null)
                        //{
                        //    documentList = new List<Document>();
                        //}
                        //List<UploadedFileInfo> docLst = SaveDocumentInfo(doc);
                        //doc.FileList = docLst;
                        //documentList.Add(doc);
                        foreach (DirectoryInfo sixthLvlDirectory in fifthLvlDirectory.GetDirectories())
                        {
                            string subSection = sixthLvlDirectory.Name;
                            Document document = new Document();
                            document.VendorDocuments = selectedDocument.VendorDocuments;
                            document.Pratice = selectedDocument.Pratice;
                            document.Station = selectedDocument.Station;
                            document.Patient = selectedDocument.Patient;
                            document.SubSection = subSection;
                            document.Section = section;

                            foreach (FileInfo file in sixthLvlDirectory.GetFiles())
                            {
                                string msg = "Scanning file " + file.Name + " in sub-section " + subSection + " of section " + section + " for patient " + selectedDocument.Patient;
                                UploadedFileInfo fileInfo = new UploadedFileInfo();
                                UploadedFileInfo uploadedFile = new UploadedFileInfo();
                                fileInfo.FileName = file.Name;
                                fileInfo.VirtualFileName = Guid.NewGuid();
                                FileStream fs = file.OpenRead();
                                byte[] contents = new byte[Contants.DOCUMENT_MANAGEMENT_DOC_SIZE];
                                BinaryReader br = new BinaryReader(fs);
                                contents = br.ReadBytes((int)fs.Length);
                                br.Close();
                                fs.Close();
                                fileInfo.FileContent = contents;
                                if (document.FileList == null)
                                {
                                    document.FileList = new List<UploadedFileInfo>();
                                }
                                //document.FileList.Add(fileInfo);
                                uploadedFile = SaveDocumentInfo(document, fileInfo);
                                UploadFile(document, uploadedFile);
                                if (isNewPatient)
                                {
                                    ++response.TotalPatientUploaded;
                                    isNewPatient = false;
                                }
                                ++response.TotalFilesUploaded;
                            }
                            //if (documentList == null)
                            //{
                            //    documentList = new List<Document>();
                            //}
                            //docLst = SaveDocumentInfo(document);
                            //document.FileList = docLst;
                            //documentList.Add(document);
                        }
                    } 
                    #endregion
                }

                #endregion

                LogControl("FILE UPLOADING PROCESS COMPLETED.....", EventArgs.Empty);
                LogControl("TOTAL FILES UPLOADED = " + fileCount, EventArgs.Empty);
                ServiceOperator.ManageLog("Total uploaded files :" + fileCount, "", false);
            }
            catch(Exception exp)
            {
                response.IsError = true;
                LogControl(exp.Message, EventArgs.Empty);
                response.ErrorMessage = exp.Message;
            }
            return response;
        }

        private void UploadFile(Document document, UploadedFileInfo file)
        {
            string directory = DirectoryOperation.MakeFullUrl(document, file);
            LogControl("uploading file "+file.FileName, EventArgs.Empty);
            MakeDirectory(document);
            if (UploadFile(directory, file))
            {
                string directoryAddress = DirectoryOperation.MakeDirectoryAddress(document, file);
                ManageDocument(file.DocmentInfo);
                if (!Directory.Exists(DirectoryOperation.MakeArchive(document, file)))
                {
                    LogControl("Creating Archive folder ", EventArgs.Empty);
                    Directory.CreateDirectory(DirectoryOperation.MakeArchive(document, file));
                    LogControl("Successfully created Archive folder ", EventArgs.Empty);
                }
                LogControl("Archiving file " + file.FileName, EventArgs.Empty);
                File.Move(directoryAddress, DirectoryOperation.MakeArchiveWithFile(document, file));
                LogControl("Archiving completed ", EventArgs.Empty);
                fileCount++;
                string msg = file.FileName + " file is uploaded sucessfully";
                LogControl(msg, EventArgs.Empty);
                //FileUpLoaded(totalFiles, new EventArgs());
            }
            else
            {
                string msg = file.FileName + " is fail to upload";
                LogControl(msg, new EventArgs());
            }
        }


        /// <summary>
        /// Author Sahibzada Sami ud Din
        /// Date 07/20/2011
        /// This method will uploadFile to Ftp Server.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        /// <returns></returns>

        public bool UploadFile(string directory, UploadedFileInfo file)
        {
            int count = 0;
            ServiceOperator.ManageLog("Calling UploadFile", "", false);
            bool response = false;
            FtpWebResponse ftpResponse = null;
            while (count < 3)
            {
                try
                {
                    ServiceOperator.ManageLog("Calling UploadFile First attempt", "", false);
                    FtpWebRequest request = CreateRequestObj(directory);
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    request.ContentLength = file.FileContent.Length;

                    Stream reqStream = request.GetRequestStream();
                    reqStream.Write(file.FileContent, 0, file.FileContent.Length);
                    reqStream.Close();
                    ftpResponse = (FtpWebResponse)request.GetResponse();
                    ftpResponse.Close();
                    response = true;
                    break;
                }
                catch
                {
                    if (count >= 3)
                    {
                        response = false;
                        //ftpResponse.Close();
                        throw;
                    }
                    else
                    {
                        //ftpResponse.Close();
                        count++;
                    }
                }
            }
            return response;
        }

        public List<UploadedFileInfo> SaveDocumentInfo(Document document)
        {
            List<UploadedFileInfo> patDocLst = null;
            UploadedFileInfo patientDocObj = null;
            List<DocumentSectionTypeDTO> sectionList = null;
            PatientUserInfo userInfo = null;
            int clinicBranchId = 0;
            try
            {
                string emptyName = string.Empty;
                string quviusId = string.Empty;
                string DOB = string.Empty;
                UserManagerServiceClient userClient = new UserManagerServiceClient();
                DocumentManagerServiceClient docClient = new DocumentManagerServiceClient();
                sectionList = new List<DocumentSectionTypeDTO>();
                if (document.FileList != null && document.FileList.Count > 0)
                {
                    foreach (UploadedFileInfo fleInfo in document.FileList)
                    {
                        string[] patientInfo = document.Patient.Split(',');
                        fleInfo.DocmentInfo = new PatientDocumentDTO();
                        fleInfo.DocmentInfo.PatientLastName = patientInfo[0];
                        fleInfo.DocmentInfo.PatientFirstName = patientInfo[1];
                        DOB = patientInfo[2];
                        quviusId = patientInfo[3];
                        fleInfo.DocmentInfo.PatientMiddleName = emptyName;
                        fleInfo.DocmentInfo.PatientFullName = fleInfo.DocmentInfo.PatientFirstName + "|" + fleInfo.DocmentInfo.PatientMiddleName + "|" + fleInfo.DocmentInfo.PatientLastName;
                        sectionList = docClient.LoadDocumentSectionTypes();
                        if (sectionList.Count > 0)
                        {
                            foreach (DocumentSectionTypeDTO sectionDTO in sectionList)
                            {
                                if (string.IsNullOrEmpty(document.SubSection))
                                {
                                    if (sectionDTO.SectionName == document.Section)
                                    {
                                        fleInfo.DocmentInfo.SectionId = sectionDTO.SectionId;
                                        fleInfo.DocmentInfo.SectionName = sectionDTO.SectionName;
                                        break;
                                    }
                                }
                                if (sectionDTO.ChildSectionType != null)
                                {
                                    foreach (DocumentSectionTypeDTO subSectio in sectionDTO.ChildSectionType)
                                    {
                                        if (subSectio.SectionName == document.SubSection && sectionDTO.SectionName == document.Section)
                                        {
                                            fleInfo.DocmentInfo.SectionId = subSectio.SectionId;
                                            fleInfo.DocmentInfo.SectionName = subSectio.SectionName;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        userInfo = userClient.LoadPatientUserInfoByQuivusId(quviusId);
                        fleInfo.DocmentInfo.PatientID = userInfo.PatientID;
                        document.PatientID = userInfo.PatientID.ToString();
                        fleInfo.DocmentInfo.DocumentName = fleInfo.FileName;
                        fleInfo.DocmentInfo.DocumentTypeId = 1;
                        fleInfo.DocmentInfo.UploadedByUserId = 42927;
                        fleInfo.DocmentInfo.UploadedByUserTypeId = 3;
                        fleInfo.DocmentInfo.PracticeName = document.Pratice;
                        fleInfo.DocmentInfo.StationName = document.Station;
                        fleInfo.DocmentInfo.LastSeenByUserId = 1;
                        fleInfo.DocmentInfo.CreatedBy = 42927;
                        fleInfo.DocmentInfo.UploadedDate = DateTime.Now.ToShortDateString();
                        clinicBranchId = userClient.LoadClinicBranchIdByClinicBranchName(document.Pratice);
                        fleInfo.DocmentInfo.UploadedByClinicBranchId = clinicBranchId;
                        fleInfo.DocmentInfo.DocumentVirtualName = fleInfo.VirtualFileName;
                        //docClient.ManagePatientDocuments(patientDocObj);
                        if (patDocLst == null)
                        {
                            patDocLst = new List<UploadedFileInfo>();
                        }
                        patDocLst.Add(fleInfo);
                    }
                }
            }
            catch
            {
                throw;
            }
            return patDocLst;
        }

        public UploadedFileInfo SaveDocumentInfo(Document document  ,UploadedFileInfo file)
        {
            int clinicBranchId = 0;
            UserManagerServiceClient userClient = new UserManagerServiceClient();
            DocumentManagerServiceClient docClient = new DocumentManagerServiceClient();
            string emptyName = string.Empty;
            string quviusId = string.Empty;
            string DOB = string.Empty;
            List<DocumentSectionTypeDTO> sectionList = null;
            PatientUserInfo userInfo = null;
            UploadedFileInfo fleInfo = file;
            string[] patientInfo = document.Patient.Split(',');
            fleInfo.DocmentInfo = new PatientDocumentDTO();
            fleInfo.DocmentInfo.PatientLastName = patientInfo[0];
            fleInfo.DocmentInfo.PatientFirstName = patientInfo[1];
            DOB = patientInfo[2];
            quviusId = patientInfo[3];
            fleInfo.DocmentInfo.PatientMiddleName = emptyName;
            fleInfo.DocmentInfo.PatientFullName = fleInfo.DocmentInfo.PatientFirstName + "|" + fleInfo.DocmentInfo.PatientMiddleName + "|" + fleInfo.DocmentInfo.PatientLastName;
            sectionList = docClient.LoadDocumentSectionTypes();
            if (sectionList.Count > 0)
            {
                foreach (DocumentSectionTypeDTO sectionDTO in sectionList)
                {
                    if (string.IsNullOrEmpty(document.SubSection))
                    {
                        if (sectionDTO.SectionName == document.Section)
                        {
                            fleInfo.DocmentInfo.SectionId = sectionDTO.SectionId;
                            fleInfo.DocmentInfo.SectionName = sectionDTO.SectionName;
                            break;
                        }
                    }
                    if (sectionDTO.ChildSectionType != null)
                    {
                        foreach (DocumentSectionTypeDTO subSectio in sectionDTO.ChildSectionType)
                        {
                            if (subSectio.SectionName == document.SubSection && sectionDTO.SectionName == document.Section)
                            {
                                fleInfo.DocmentInfo.SectionId = subSectio.SectionId;
                                fleInfo.DocmentInfo.SectionName = subSectio.SectionName;
                                break;
                            }
                        }
                    }
                }
            }
            userInfo = userClient.LoadPatientUserInfoByQuivusId(quviusId);
            fleInfo.DocmentInfo.PatientID = userInfo.PatientID;
            document.PatientID = userInfo.PatientID.ToString();
            fleInfo.DocmentInfo.DocumentName = fleInfo.FileName;
            fleInfo.DocmentInfo.DocumentTypeId = 1;
            fleInfo.DocmentInfo.UploadedByUserId = 42927;
            fleInfo.DocmentInfo.UploadedByUserTypeId = 3;
            fleInfo.DocmentInfo.PracticeName = document.Pratice;
            fleInfo.DocmentInfo.StationName = document.Station;
            fleInfo.DocmentInfo.LastSeenByUserId = 1;
            fleInfo.DocmentInfo.CreatedBy = 42927;
            fleInfo.DocmentInfo.UploadedDate = DateTime.Now.ToShortDateString();
            clinicBranchId = userClient.LoadClinicBranchIdByClinicBranchName(document.Pratice);
            fleInfo.DocmentInfo.UploadedByClinicBranchId = clinicBranchId;
            fleInfo.DocmentInfo.DocumentVirtualName = fleInfo.VirtualFileName;
            return fleInfo;
        }

        public bool ManageDocument(PatientDocumentDTO patientDocObj)
        {
            bool response = false;
            try
            {
                DocumentManagerServiceClient docClient = new DocumentManagerServiceClient();
                int docID = docClient.ManagePatientDocuments(patientDocObj);
                if (docID > 0)
                {
                    response = true;
                }
                response = true;
            }
            catch
            {
                throw;
            }
            return response;
        }

        private bool IsSection(string fileName)
        {
            bool response = false;
            DocumentManagerServiceClient docClient = new DocumentManagerServiceClient();
            if (secList == null)
            {
                secList = docClient.LoadDocumentSectionTypes();
            }
            foreach (DocumentSectionTypeDTO sec in secList)
            {
                if (sec.ParentId == 0 && sec.SectionName == fileName)
                {
                    response = true;
                    break;
                }
            }
            return response;
        }

    }
}
