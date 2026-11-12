using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 20 March 2012
    /// This class represent Message Header
    /// and will map data to nhapi MSH data type.
    /// </summary>
    public class MessageHeader
    {
        public string FieldSeparator { get; set; }

        public string EncodingCharacters { get; set; }

        public HierarchicDesignator SendingApplication { get; set; }

        public HierarchicDesignator SendingFacility { get; set; }

        public HierarchicDesignator ReceivingApplication { get; set; }

        public HierarchicDesignator ReceivingFacility { get; set; }

        public TimeStamp MessageDateTime { get; set; }

        public MessageType MessageType { get; set; }

        public string MessageControlID { get; set; }

        public ProcessingType ProcessingID { get; set; }

        public Version VersionID { get; set; }

        public Numeric SequenceNumber { get; set; }

        public string ContinuationPointer { get; set; }

        public Identity AcceptAcknowledgmentType { get; set; }

        public Identity ApplicationAcknowledgmentType { get; set; }

        public Identity CountryCode { get; set; }

        public Identity CharacterSet { get; set; }

        public AllFieldsCodedWithException PrincipalLanguageOfMessage { get; set; }

        public Identity AlternateCharacterSetHandlingScheme { get; set; }

        public EntityIdentifier MessageProfileIdentifier { get; set; }
    }
}
