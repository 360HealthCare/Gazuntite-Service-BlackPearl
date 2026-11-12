using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HL7.Model.FieldsGroup;

namespace HL7.Business.Validator
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 21 March 2012
    /// This class represent Message Header Validator
    /// and will validate message header.
    /// </summary>
    public class MessageHeaderValidator
    {
        public void ValidateOrderMSH(MessageHeader msh)
        {
            if (msh == null)
            {

            }
            else
            {
                if (msh.MessageDateTime==null)
                {

                }
            }
        }
    }
}
