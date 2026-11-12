using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Street Address
    /// and will map data to nhapi SAD data type.
    /// </summary>
    public class StreetAddress
    {
        public string MailingAddress { get; set; }

        public string StreetName { get; set; }

        public string DwellingNumber { get; set; }
    }
}
