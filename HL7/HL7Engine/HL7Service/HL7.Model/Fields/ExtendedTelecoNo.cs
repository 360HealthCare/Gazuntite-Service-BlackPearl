using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Extended Telecommunication Number
    /// and will map data to nhapi XTN data type.
    /// </summary>
    public class ExtendedTelecoNo
    {
        public Identity TelecoUseCode { get; set; }

        public Identity TelecoEquipmentType { get; set; }

        public string EmailAddress { get; set; }

        public Numeric CountryCode { get; set; }

        public Numeric AreaCode { get; set; }

        public Numeric LocalNumber { get; set; }

        public Numeric Extension { get; set; }

        public string AnyText { get; set; }
    }
}
