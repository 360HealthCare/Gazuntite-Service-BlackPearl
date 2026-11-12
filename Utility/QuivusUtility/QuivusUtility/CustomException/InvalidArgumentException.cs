using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quivus.Utility.Logger;

namespace Quivus.Utility.CustomException
{
    public class InvalidArgumentException : Exception
    {
        //private string p;
      
        public String ErrorCode { get; set; }
        public int Code { get; set; }
       
        public String ErrorDetail { get; set; }

        public InvalidArgumentException(string message, int code)
            : base(message)
        {
            Code = code;
        }


        public InvalidArgumentException(string message,String Detail, String code)
            : base(message)
        {
            ErrorDetail = Detail;
            ErrorCode = code;
        }

        public InvalidArgumentException(string message)
            : base(message)
        {

        }

        public InvalidArgumentException(string message,  String Detail)
            : base(message)
        {
            QuivusLogger.Error("Invalid Args MSG$$$:::" + message);
            QuivusLogger.Error("Invalid Args Details$$$:::" + Detail);
            this.ErrorDetail = Detail;
        }

       
    }
}