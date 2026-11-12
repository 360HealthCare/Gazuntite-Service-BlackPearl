using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Extended Address
    /// and will map data to nhapi XAD data type.
    /// </summary>
    public class ExtendedAddress
    {
        public StreetAddress StreetAddress { get; set; }

        public string OtherDesignation { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string ZipOrPostalCode { get; set; }

        public Identity Country { get; set; }

        public Identity AddressType { get; set; }

        public string OtherGeographicDesignation { get; set; }

        public UserDefinedCodedValue CountyParishCode { get; set; }

        public UserDefinedCodedValue CensusTract { get; set; }

        public Identity AddressRepresentationCode { get; set; }

        public TimeStamp EffectiveDate { get; set; }

        public TimeStamp ExpirationDate { get; set; }

    }
}
