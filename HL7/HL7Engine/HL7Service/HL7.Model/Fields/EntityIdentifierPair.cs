using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.Model.Fields
{
    /// <summary>
    /// Author Sahibzada Sami ud Din
    /// Date 14 March 2012
    /// This class represent Entity Identifier Pair
    /// and will map data to nhapi EIP data type.
    /// </summary>
    public class EntityIdentifierPair
    {
        public EntityIdentifier PlacerAssignedIdentifier { get; set; }

        public EntityIdentifier FillerAssignedIdentifier { get; set; }
    }
}
