using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 20 March 2012
    /// This class represent Message Type
    /// and will map data to nhapi MSG data type.
    /// </summary>
    public class MessageType
    {
        public Identity MessageCode { get; set; }

        public Identity TriggerEvent { get; set; }

        public Identity MessageStructure { get; set; }
    }
}
