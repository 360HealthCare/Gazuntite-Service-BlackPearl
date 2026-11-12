using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Parser;
using NHapi.Model.V251;
using NHapi.Base.Model;
using NHapi.Model.V251.Message;
using NHapi.Model.V251.Segment;
using HL7.Parser.XMLEncoder.ORM;
using HL7.Model.FieldsGroup;
using HL7.Model.Messages;
using HL7.Business.HL7Encoder;
namespace TestClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            OML_O21 oml = new OML_O21();
            MessageHeader msh = new MessageHeader();
            msh.SendingApplication = new HL7.Model.Fields.HierarchicDesignator();
            msh.SendingApplication.NamespaceID = new HL7.Model.Fields.UserDefinedCodedValue();
            msh.SendingApplication.NamespaceID.Value = "namespace";
            msh.SendingApplication.UniversalID = "Universal ID";
            msh.SendingApplication.UniversalIDType = new HL7.Model.Fields.Identity();
            msh.SendingApplication.UniversalIDType.ID = "Universal ID Type";
            msh.SendingFacility = new HL7.Model.Fields.HierarchicDesignator();
            msh.SendingFacility.NamespaceID = new HL7.Model.Fields.UserDefinedCodedValue();
            msh.SendingFacility.NamespaceID.Value = "Universal ID";
            msh.SendingFacility.UniversalID = "Sending Facility Universal ID";
            msh.SendingFacility.UniversalIDType = new HL7.Model.Fields.Identity();
            msh.SendingFacility.UniversalIDType.ID = "Sending Facility Universal ID Type";
            ParseOML omlParser = new ParseOML();
            OMLMessage msg = new OMLMessage();
            msg.MsgHeader = msh;
            var response = omlParser.ParseOMLMessage(msg);
            Console.WriteLine(response);
            Console.ReadKey();

        }
    }
}
