using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 20 March 2012
    /// This class represent Processing Type
    /// and will map data to nhapi PT data type.
    /// </summary>
    public class ProcessingType
    {
        public Identity ProcessingID { get; set; }

        public Identity ProcessingMode { get; set; }
    }
}
