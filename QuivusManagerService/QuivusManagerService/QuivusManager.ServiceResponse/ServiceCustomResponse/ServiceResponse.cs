using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


using System.Text;

namespace QuivusManager.ServiceResponse.ServiceCustomResponse
{
    /// <summary>
    /// Mansoor
    /// Date 25 Feb 2011
    /// StringBuilder is changed to string
    /// </summary>

    [DataContract]
    public class ServiceResponse
    {
        //private StringBuilder _responseMessage;
        //private StringBuilder _errorDetails;

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public int ResponseCode { get; set; }

        [DataMember]
        public string ResponseMessage { get; set; }

         [DataMember]
        public string ResponseWarning { get; set; }
        
        //{
        //    get { return _responseMessage.ToString(); } 
        //    private set { } 
        //}

         [DataMember]
         public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorDetails  { get; set; }

      
        public StringBuilder DebuggingDetails { get; set; } // Will Used in fututre for debugginh purpose :)
        //{
        //    get { return _errorDetails.ToString(); }
        //    private set { }
        //}


        //public StringBuilder InnerResponseMessage 
        //{
        //    set { _responseMessage = value; }
        //    get { return _responseMessage; }
        //}
        //public StringBuilder InnerErrorDetails
        //{
        //    set { _errorDetails = value; }
        //    get { return _errorDetails; }
        //}
    }
}