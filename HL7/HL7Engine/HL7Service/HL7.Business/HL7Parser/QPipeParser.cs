using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Parser;

namespace HL7.Business.HL7Parser
{
    /// <summary> An implementation of Parser that supports traditionally encoded (ie delimited with characters
    /// like |, ^, and ~) HL7 messages.  Unexpected segments and fields are parsed into generic elements
    /// that are added to the message.  
    /// </summary>
    /// <author>  Muhammad Ismail
    /// This is a custom wrapper class on PipeParser
    /// </author>
    public class QPipeParser : PipeParser
    {
    }
}
