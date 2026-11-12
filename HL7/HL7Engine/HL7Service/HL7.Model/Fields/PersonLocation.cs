using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Patient Location
    /// and will map data to nhapi PL data type.
    /// </summary>
    public class PersonLocation
    {
        public UserDefinedCodedValue PointOfCare { get; set; }

        public UserDefinedCodedValue Room { get; set; }

        public UserDefinedCodedValue Bed { get; set; }

        public HierarchicDesignator Facility { get; set; }

        public UserDefinedCodedValue LocationStatus { get; set; }

        public AllFieldsCodedWithException PersonLocationType { get; set; }

        public UserDefinedCodedValue Building { get; set; }

        public UserDefinedCodedValue Floor { get; set; }

        public string LocationDescription { get; set; }

        public EntityIdentifier ComprehensiveLocationIdentifier { get; set; }

        public HierarchicDesignator AssigningAuthorityforLocation { get; set; }
    }
}
