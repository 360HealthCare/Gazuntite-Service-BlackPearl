using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    /// <summary>
    /// Author Sahibzada Sami ud din
    /// Date 18 july 2012
    /// This class represents TimingQuantity segment and maps to TQ1 of OML message of HL7 
    /// </summary>
    public class TimingQuantity
    {
        public string SetID { get; set; }

        public TimeStamp StartDateTime { get; set; }

        public AllFieldsCodedWithException Priority { get; set; }

    }
}
