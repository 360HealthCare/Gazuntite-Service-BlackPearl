using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveProvider.Utility.Constant.Errors
{
    /// <summary>
    /// Author Muhammad Ismail
    /// Date 04 July 2011
    /// Class conatins the error code prefix
    /// </summary>
    public class ErrorCodes
    {
        public static string ERROR_CODE_PREFIX = "EC";
        public static string SEPRATOR = "-";
        public static string INVALID_AURGUMENTS_PREFIX = "IARGS";
        public static string RECORD_NOT_INTRESTED_PREFIX = "RNINS";
        public static string PARAMETER_NOT_POPULATE_PREFIX = "PARAMNTPOP";
        public static string TABLE_CONVERSION_PREFIX = "TBCNV";
        public static string GENERAL_ERROR_CODE_PREFIX = "GEC";
        public static string LEVEL_ONE_PREFIX = "01";
        public static string LEVEL_TWO_PREFIX = "02";

        public static string OPERATION_LOAD_PREFIX = "OLD";
        public static string OPERATION_SAVE_PREFIX = "OSV";
        public static string OPERATION_UPDATE_PREFIX = "OUP";
        public static string OPERATION_SIGN_PREFIX = "OSGN";
        public static string OPERATION_MANAGE_PREFIX = "OMG";
        public static string OPERATION_PARAMETER_POPULATOR_PREFIX = "OPARAMPOP";
        public static string OPERATION_VALIDATOR_PREFIX = "OVLDTR";
        public static string OPERATION_CALL_USER_MANAGER_SERVICE_PREFIX = "OCUMSP";

        public static string ComposeErrorCode(String ERROR_CODE_PREFIX, String SECTION_PREFIX, String SUB_SECTION_PREFIX, String OPERATION_PREFIX, String ERROR_PREFIX)
        {
            return ERROR_CODE_PREFIX + SECTION_PREFIX + ErrorCodes.SEPRATOR +
                                    SUB_SECTION_PREFIX + ErrorCodes.SEPRATOR + OPERATION_PREFIX +
                                    ERROR_PREFIX;
        }

        public static string SECTION_DOCUMENT_MANAGMENT_PREFIX = "DOCMNG";
        public static string SECTION_ELAB_MANAGEMENT_PREFIX = "ELABMNG";
        public static string SECTION_LIVE_PROVIDER_PREFIX = "LVPRV";
        public static string SECTION_DOCUMENT_SECTION_MANAGMENT_PREFIX = "DOCSECMNG";
        public static string DTO_CONVERSION_ERROR_PREFIX = "DTOCNV";
        public static string SUB_SECTION_UTILITY_LOG = "SSULG";


        public static string PARAMETER_POPULATOR_PREFIX = "PRPO";

        public static string STATION_IDS_CONVERTER = "STIDCON";
    }
}
