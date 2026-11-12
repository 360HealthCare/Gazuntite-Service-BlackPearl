using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent Name With Date And Location
    /// and will map data to nhapi NDL data type.
    /// </summary>
    public class NameWithDateAndLocation
    {
        public CompositeIDNoAndNameSimplified Name { get; set; }
    }
}
