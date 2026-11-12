using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.CustomExceptions
{
    public class ParameterPopulatorException : Exception
    {
        public string ErrorCode { get; set; }
        public string CustomMessage { get; set; }
        public ParameterPopulatorException(string Message) 
            : base(Message)
        {
            
        }
        public ParameterPopulatorException(string Message, string Code)
            : base(Message)
        {
            ErrorCode = Code;
        }
        public ParameterPopulatorException(string Message, string code, string CustmMsg)
            : base(Message)
        {
            ErrorCode = code;
            CustomMessage = CustmMsg;
        }
    }
}
