using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Family Name
    /// and will map data to nhapi FN data type.
    /// </summary>
    public class FamilyName
    {
        public string SurName { get; set; }

        public string OwnSurNamePrefix { get; set; }

        public string OwnSurName { get; set; }

        public string PartnerSurNamePrefix { get; set; }

        public string PartnerSurName { get; set; }
    }
}
