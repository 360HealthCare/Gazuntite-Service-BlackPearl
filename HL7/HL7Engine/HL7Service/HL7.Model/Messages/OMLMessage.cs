using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.FieldsGroup;
using HL7.Model.Messages.Inferface;

namespace HL7.Model.Messages
{
    /// <summary>
    /// Author Sahibzada Sami ud din
    /// Date 25 June 2012
    /// This class represent OML message which will go to order test to Lab
    /// </summary>
    public class OMLMessage : IMessage
    {
        public MessageHeader MsgHeader { get; set; }

        public PatientIdentification PatientInfo { get; set; }

        public NextOfKin NxtOfKin { get; set; }

        public List<CommonOrder> Order { get; set; }

        public PatientVisitInfo PatientVisit { get; set; }

        public Insurance PatientInsurance { get; set; }

        public GuarantorInfo Guarantor { get; set; }

        public List<ObservationRequest> ObservationReq { get; set; }

        public List<Specimen> SpecimenInfo { get; set; }

    }
}
