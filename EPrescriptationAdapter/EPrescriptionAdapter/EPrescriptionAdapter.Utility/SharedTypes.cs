using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EprescriptionService.Utility
{
    /// <summary>
    /// Holds the common enums and types for the project
    /// </summary>
    public class SharedTypes
    {


        /// <summary>
        ///  Shared errors between the server and client
        /// </summary>
        public enum ErrorTypeEnum
        {
            S,  /// Session
            O   /// Other
        }

        /// <summary>
        ///  Different error types
        /// </summary>
        public enum SeverityTypeEnum
        {
            E,  /// Error
            W,  /// Warning
            I   /// Information
        }
        public static SeverityTypeEnum GetSeverityTypeEnum(string enumVal)
        {
            switch (enumVal)
            {
                case "E":
                    return SeverityTypeEnum.E;
                case "W":
                    return SeverityTypeEnum.W;
                case "I":
                    return SeverityTypeEnum.I;
            }
            return SeverityTypeEnum.E;
        }


    }
}
