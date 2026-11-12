using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 19 March 2012
    /// This class represent Parent Result Link
    /// and will map data to nhapi PRL data type.
    /// </summary>
    public class ParentResultLink
    {
        public AllFieldsCodedWithException ParentObservationIdentifier { get; set; }

        public string ParentObservationSubIdentifier { get; set; }

        public string ParentObservationValueDescriptor { get; set; } // This feild represent text data
    }
}
