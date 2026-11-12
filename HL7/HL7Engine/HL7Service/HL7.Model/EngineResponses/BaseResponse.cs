using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.EngineResponses
{
    public class BaseResponse
    {
        public string ErrorMessage { get; set; }

        public bool IsError { get; set; }

        public string OtherMessage { get; set; }
    }
}
