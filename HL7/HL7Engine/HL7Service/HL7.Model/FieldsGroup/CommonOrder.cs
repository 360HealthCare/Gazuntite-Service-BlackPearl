using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    public class CommonOrder
    {
        public Identity OrderControl { get; set; }

        public EntityIdentifier PlacerOrderNumber { get; set; }

        public EntityIdentifier FillerOrderNumber { get; set; }

        public EntityIdentifier PlacerGroupNumber { get; set; }

        public Identity OrderStatus { get; set; }

        public Identity ResponseFlag { get; set; }

        public EntityIdentifierPair Parent { get; set; }

        public TimeStamp TransactionDateTime { get; set; }

        public ExtCompIDAndNme EnteredBy { get; set; }

        public ExtCompIDAndNme VerifiedBy { get; set; }

        public ExtCompIDAndNme OrderingProvider { get; set; }

        public PersonLocation EntererSLocation { get; set; }

        public ExtendedTelecoNo CallBackPhoneNo { get; set; }

        public TimeStamp OrderEffectiveDateTime { get; set; }

        public AllFieldsCodedWithException OrderControlCodeReason { get; set; }

        public AllFieldsCodedWithException EnteringOrganization { get; set; }

        public AllFieldsCodedWithException EnteringDevice { get; set; }

        public ExtCompIDAndNme ActionBy { get; set; }

        public OrgExtCompNameAndIdentity OrderingFacilityName { get; set; }

        public ExtendedAddress OrderingFacilityAddress { get; set; }

        public ExtendedTelecoNo OrderingFacilityPhoneNumber { get; set; }

        public ExtendedAddress OrderingProviderAddress { get; set; }

        public AllFieldsCodedWithException OrderStatusModifier { get; set; }

        public AllFieldsCodedWithException FillerSExpectedAvailabilityDateTime { get; set; }

        public AllFieldsCodedWithException ConfidentialityCode { get; set; }

        public AllFieldsCodedWithException OrderType { get; set; }

        public CodedWithNoExceptions EntererAuthorizationMode { get; set; }

        public AllFieldsCodedWithException ParentUniversalServiceIdentifier { get; set; }
    }
}
