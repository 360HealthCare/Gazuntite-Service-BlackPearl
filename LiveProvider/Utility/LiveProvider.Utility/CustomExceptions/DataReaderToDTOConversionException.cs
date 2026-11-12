using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.CustomExceptions
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 Juy 2011
    /// Custom exception class used for Data Reader to DTO conversion errors  
    /// </summary>
    public class DataReaderToDTOConversionException : Exception
    {

        public string ErrorCode { get; set; }
        public string ErrorDetail { get; set; }

        public DataReaderToDTOConversionException(string message,string ErrorDetail,  string ErrorCode)
            : base(message)
        {
            this.ErrorCode = ErrorCode;
        this.ErrorDetail = ErrorDetail;
        }

    }
}
