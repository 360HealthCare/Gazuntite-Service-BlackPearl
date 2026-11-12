using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusUtility.CustomException
{
   public class ParameterPopulatorException: Exception
    {
        public string Code { get; set; }
        public String Detail { get; set; }

        public ParameterPopulatorException(string message,string Detail, string code)
            : base(message)
        {
            Code = code;
            this.Detail = Detail;
        }
    }
}
