using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent Financial Class
    /// and will map data to nhapi FC data type.
    /// </summary>
    public class FinancialClass
    {
        public UserDefinedCodedValue FinancialClassCode { get; set; }

        public TimeStamp EffectiveDate { get; set; }
    }
}
