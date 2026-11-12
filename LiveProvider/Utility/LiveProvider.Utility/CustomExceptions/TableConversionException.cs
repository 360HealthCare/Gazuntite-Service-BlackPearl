using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.CustomExceptions
{
    public class TableConversionException : Exception
    {
        public string Code { get; set; }

        public TableConversionException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}
