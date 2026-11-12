using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.CustomExceptions
{
    /// <summary>
    /// Author:Sahibzada Sami ud din
    /// Date:05/07/2011
    /// </summary>
    public class RecordNotInsertException : Exception
    {
        public string Code { get; set; }

        public RecordNotInsertException(string message)
            : base(message)
        {

        }

        public RecordNotInsertException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}
