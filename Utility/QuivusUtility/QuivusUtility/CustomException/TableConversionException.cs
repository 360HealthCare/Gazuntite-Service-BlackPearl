
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quivus.Utility.CustomException
{
    public class TableConversionException : Exception
    {
        public string Code { get; set; }

        public TableConversionException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
