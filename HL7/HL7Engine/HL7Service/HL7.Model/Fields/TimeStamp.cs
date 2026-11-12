using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Time Stamp
    /// and will map data to nhapi TS data type.
    /// </summary>
    public class TimeStamp
    {
        public HL7DateTime Time { get; set; }
    }
}
