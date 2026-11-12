using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.CustomExceptions
{
    /// <summary>
    /// Author:Sahibzada Sami ud din
    /// Date:06/07/2011
    /// Description: This class will server InvalidArgumentExceptions
    /// </summary>
    public class InvalidArgumentException : Exception
    {
        public String ErrorCode { get; set; }
        public int Code { get; set; }

        public String ErrorDetail { get; set; }

        public InvalidArgumentException(string message, int code)
            : base(message)
        {
            Code = code;
        }


        public InvalidArgumentException(string message, String Detail, String code)
            : base(message)
        {
            ErrorCode = code;
        }

        public InvalidArgumentException(string message)
            : base(message)
        {

        }

        public InvalidArgumentException(string message, String Detail)
            : base(message)
        {

            this.ErrorDetail = Detail;
        }
    }
}
