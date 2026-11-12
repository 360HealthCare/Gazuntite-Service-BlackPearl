using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace QuivusDictationService
{
    
    [ServiceContract]
    public interface IQuivusDictationService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "UploadFile/{fileName}", Method = "POST")]
        void UploadFile(Stream fileContents, string fileName);
        

        [OperationContract]
        [WebInvoke(UriTemplate = "UploadDictationFile/{FileName}/{RecordTimeStamp}/{SessionID}/{CreatedAt}/{CreatedBy}")]
        void UploadDictationFile(Stream FileContents, string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy);


        [OperationContract]
        [WebGet(UriTemplate = "GetAudioStream/{DictationFileID}")]
        Stream GetAudioStream(string DictationFileID);


        [OperationContract(Name = "Test")]
        [WebGet(UriTemplate = "test/{FileName}/{RecordTimeStamp}/{SessionID}/{CreatedAt}/{CreatedBy}")]
        string Test(string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy);

        [OperationContract]
        [WebGet(UriTemplate = "GetDictationFile/{DictationFileID}/{SessionID}")]
        Stream GetDictationFile(string DictationFileID, string SessionID);

        [OperationContract]
        bool PingService();
    }


   
}
