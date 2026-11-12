using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Parser;

namespace HL7.Business.HL7Parser
{
    /// <summary> Parses and encodes HL7 messages in XML form, according to HL7's normative XML encoding
    /// specification.  This is an abstract class that handles datatype and segment parsing/encoding,
    /// but not the parsing/encoding of entire messages.  To use the XML parser, you should create a
    /// subclass for a certain message structure.  This subclass must be able to identify the Segment
    /// objects that correspond to various Segment nodes in an XML document, and call the methods <code>
    /// parse(Segment segment, ElementNode segmentNode)</code> and <code>encode(Segment segment, ElementNode segmentNode)
    /// </code> as appropriate.  XMLParser uses the Xerces parser, which must be installed in your classpath.
    /// </summary>
    /// <author> Muhammad Ismail
    /// This is custom wrapper class on XMLParser
    /// </author>
    public abstract class QXMLParser /*: XMLParser*/
    {
    }
}
