using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.Fields;

namespace HL7.Model.FieldsGroup
{
    public class NotesAndComments
    {
        public string SetID { get; set; } //Sequence ID

        public Identity SourceOfComment { get; set; }

        public List<string> Comment { get; set; } //Formatted Text data type

        public AllFieldsCodedWithException CommentType { get; set; }
    }
}
