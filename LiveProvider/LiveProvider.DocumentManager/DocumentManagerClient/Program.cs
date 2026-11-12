using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using LiveProvider.Model;
using LiveProvider.Model.TypeDTOs.DocumentManager;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Drawing;
using System.Drawing.Imaging;
using DocumentManagerClient.UserManagerService;
using DocumentManagerClient.DocumentManagerService;


namespace DocumentManagerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //LPatientSearchResultDTO searchResult = null;

            //UserManagerServiceClient searchClient = new UserManagerServiceClient();
            //PatientSearchResultDTO userManagerSearchResult = searchClient.SearchPatientbyClinicIdAndSearchString(7, 61, 3, "01-02-1982", 1);
            //if (userManagerSearchResult != null)
            //{
            //    if (searchResult == null)
            //    {
            //        searchResult = new LPatientSearchResultDTO();
            //        searchResult.CurrentFirstRecordNo = userManagerSearchResult.CurrentFirstRecordNo;
            //        searchResult.CurrentLastRecordNo = userManagerSearchResult.CurrentLastRecordNo;
            //        searchResult.CurrentPageNo = userManagerSearchResult.CurrentPageNo;
            //        searchResult.TotalNoOfRecords = userManagerSearchResult.TotalNoOfRecords;
            //        List<LPatientSearchDTO> patientSerachList = new List<LPatientSearchDTO>();
            //        PatientSearchDTO[] usrpatientSerachMngmntList = userManagerSearchResult.PatientSearchList;
            //        foreach (PatientSearchDTO usrMngmntpatientSerachObj in usrpatientSerachMngmntList)
            //        {
            //            LPatientSearchDTO patientSearchObj = new LPatientSearchDTO();
            //            patientSearchObj.FirstName = usrMngmntpatientSerachObj.FirstName;
            //            patientSearchObj.LastName = usrMngmntpatientSerachObj.LastName;
            //            patientSearchObj.MiddleName = usrMngmntpatientSerachObj.MiddleName;

            //            //Below Check is added by Sami for correction of bug
            //            if (string.IsNullOrEmpty(patientSearchObj.MiddleName))
            //            {
            //                patientSearchObj.MiddleName = string.Empty;
            //            }

            //            patientSearchObj.QuivusID = usrMngmntpatientSerachObj.QuivusID;
            //            patientSearchObj.UserId = usrMngmntpatientSerachObj.UserId;
            //            patientSearchObj.PatientId = usrMngmntpatientSerachObj.PatientId;
            //            patientSearchObj.GenderId = usrMngmntpatientSerachObj.GenderId;
            //            patientSearchObj.GenderName = usrMngmntpatientSerachObj.GenderName;
            //            patientSearchObj.SSN = usrMngmntpatientSerachObj.SSN;
            //            patientSearchObj.DOB = usrMngmntpatientSerachObj.DOB.ToShortDateString();
            //            patientSerachList.Add(patientSearchObj);
            //        }
            //        searchResult.PatientSearchList = patientSerachList;
                //}
                DocumentManagerServiceClient client = new DocumentManagerServiceClient();
                var res = client.SearchPatientDocumentByClinicId(7,"08/19/2011",3,1,416);
            }
        }
            //DocumentManagerService.DocumentManagerServiceClient client = new DocumentManagerService.DocumentManagerServiceClient();

            //var resultnew = client.LoadPatientDocumentByClinicIdAndPatientID(7, 518, 1);
                
           // testUpload1();
            //testUpload();
           // WcfTesting();

           // DocumentManagerService directTest = new DocumentManagerService();

          //  directTest.LoadDocumentTypes();

        //    DocumentManagerClient.UserManagerService.UserManagerServiceClient client = new UserManagerService.UserManagerServiceClient();
        //    var response = client.SearchPatientbyClinicIdAndSearchString(7, 487, 3, "07-31-2011", 1);

        //    string mainUrl = "http://192.168.0.63:82/LiveProvider/DocumentManager/Service/RestFulService/DocumentManagementRestfulService.svc";

        //    WebClient Proxy = new WebClient();
        //    DataContractSerializer serializer = new DataContractSerializer(typeof(List<DocumentTypeDTO>));

        //    try
        //    {
              
        //        byte[] qdata = Proxy.DownloadData(mainUrl + "LoadDocumentTypes");
        //        Stream qstream = new MemoryStream(qdata);

        //        List<DocumentTypeDTO> result = serializer.ReadObject(qstream) as List<DocumentTypeDTO>;
        //    }
        //    catch (Exception exp)
        //    { 
            
        //    }

        //    //string response = Proxy.DownloadString(mainUrl+"Ping");


        //    Proxy.Headers["Content-type"] = "text/xml";
        //    MemoryStream ms = new MemoryStream();

        //    LiveProviderBaseDTO testobj = new LiveProviderBaseDTO { IsDeleted=false,IsDirty=true};
        //    serializer.WriteObject(ms, testobj);
        //    try
        //    {
        //        byte[] data = Proxy.UploadData(mainUrl+"UpdateCustomCall", "Post", ms.ToArray());
        //        Stream stream = new MemoryStream(data);
        //        var qusr = serializer.ReadObject(stream) as LiveProviderBaseDTO;

        //    }
        //    catch (Exception exp)
        //    {

        //    }

        //}

//        private static void WcfTesting()
//        {
//            string mainUrl = "http://192.168.0.63:82/LiveProvider/DocumentManager/Service/RestFulService/DocumentManagementRestfulService.svc";

//            WebClient Proxy = new WebClient();
//            DataContractSerializer serializer = new DataContractSerializer(typeof(List<DocumentTypeDTO>));


//            try
//            {

//                byte[] qdata = Proxy.DownloadData(mainUrl + "LoadPatientDocumentByClinicId/7/1");
//                Stream qstream = new MemoryStream(qdata);

//                List<DocumentTypeDTO> result = serializer.ReadObject(qstream) as List<DocumentTypeDTO>;
//            }
//            catch (Exception exp)
//            {

//            }
//        }

//        private static void testUpload()
//        {
//            try
//            {
//                PatientDocumentDTO dto = new PatientDocumentDTO();
//                dto.UploadedByUserId = 8080;
//                dto.IsDeleted = false;
//                dto.DocumentTypeId = 12;
//                dto.SectionId = 1;
//                dto.PatientID = 509;
//                dto.UploadedByUserTypeId = 1;
//                dto.PatientFirstName = "abc";
//                dto.DocumentName = "abc.docx";
//                dto.CreatedBy = 123;
//                dto.PracticeName = "Practicename";
//                dto.StationName = "station";
//                dto.PatientFullName = "ggg|dfg|dfg";
//                dto.DocumentTypeId = 1;
//                dto.UploadedDate = DateTime.Now.ToString();
//                dto.LastSeenByUserId = 123;
//                string mainUrl = "http://quivus.com/QuivusAir/LiveProvider/DocumentManager/Service/RestFulService/DocumentManagementRestfulService.svc/ManagePatientDocuments";

//                WebClient Proxy1 = new WebClient();

//             Proxy1.Headers["Content-type"] = "application/json";

//             MemoryStream ms = new MemoryStream();
    
//            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PatientDocumentDTO));

//            serializerToUplaod.WriteObject(ms, dto);

//            var data = Proxy1.UploadData(mainUrl, "POST", ms.ToArray());





//Console.ReadKey(true);


//            }
//            catch
//            { }
//        }


//        private static void testUpload1()
//        {
//             PatientDocumentDTO dto = new PatientDocumentDTO();
//             dto.DocumentId = 1;
//             dto.UploadedByUserId = 8080;
//             dto.IsDeleted = false;
//             dto.DocumentTypeId = 2;
//             dto.SectionId = 1;
//             dto.PatientID = 451;
//            dto.UploadedByUserTypeId=1;
//            dto.PatientFirstName = "abc";
//            dto.DocumentName = "abc.docx";
//            dto.CreatedBy=123;
//            dto.PracticeName = "Practicename";
//            dto.StationName = "station";
//            dto.PatientFullName = "iioo|null|dfgdfh";
//            dto.UploadedDate = DateTime.Now.ToString();
//            dto.LastSeenDate = DateTime.Now.ToString();
//            dto.LastSeenByUserId = 123;
//            dto.CommentNote = "thid id";
//            DocumentManagerService.DocumentManagerServiceClient client = new DocumentManagerService.DocumentManagerServiceClient();
//        client.ManagePatientDocuments(dto);
//        }
//    }
}
