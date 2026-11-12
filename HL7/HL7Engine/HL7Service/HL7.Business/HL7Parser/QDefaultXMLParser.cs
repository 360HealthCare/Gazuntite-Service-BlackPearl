using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Parser;

namespace HL7.Business.HL7Parser
{
    /// <summary> <p>A default XMLParser.  This class assigns segment elements (in an XML-encoded message) 
    /// to Segment objects (in a Message object) using the name of a segment and the names 
    /// of any groups in which the segment is nested.  The names of group classes must correspond
    /// to the names of group elements (they must be identical except that a dot in the element 
    /// name, following the message name, is replaced with an underscore, in order to consitute a 
    /// valid class name). </p>
    /// <p>At the time of writing, the group names in the XML spec are changing.  Many of the group 
    /// names have been automatically generated based on the group contents.  However, these automatic 
    /// names are gradually being replaced with manually assigned names.  This process is expected to 
    /// be complete by November 2002.  As a result, mismatches are likely.  Messages could be  HL
    /// transformed prior to parsing (using XSLT) as a work-around.  Alternatively the group class names 
    /// could be changed to reflect updates in the XML spec.  Ultimately, HAPI group classes will be 
    /// changed to correspond with the official group names, once these are all assigned.  </p>
    /// </summary>
    /// <author> Muhammad Ismail
    /// This is custom wrpper class build on DefaultXMLParser
    /// </author>
    public class QDefaultXMLParser : DefaultXMLParser
    {
    }
}
 