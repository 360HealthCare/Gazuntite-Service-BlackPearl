using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


using System.Text;

namespace QuivusBilling.ServiceResponse
{
    /// <summary>
    /// Author: Shahid Munir
    /// Date: 01/04/2012   
    /// </summary>

    [DataContract]
    public class ServiceResponse
    {        

        [DataMember]
        public bool ResponseStatus { get; set; }

        [DataMember]
        public int ResponseCode { get; set; }

        [DataMember]
        public string ResponseMessage { get; set; }

         [DataMember]
        public string ResponseWarning { get; set; }      
       

         [DataMember]
         public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorDetails  { get; set; }

      
        //public StringBuilder DebuggingDetails { get; set; } // Will Used in fututre for debugginh purpose :)
       
    }
}