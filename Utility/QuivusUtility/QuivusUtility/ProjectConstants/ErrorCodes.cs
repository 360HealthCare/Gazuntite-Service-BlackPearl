using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuivusUtility.ProjectConstants
{
    /// <summary>
    /// Author Ismail
    /// Date 08 June 2011
    /// </summary>
    public class ErrorCodes
    {
       
        public static string MODULE_NOTES_PREFIX = "MN";

        public static string ERROR_CODE_PREFIX = "EC";
        public static string SEPRATOR="-";
        public static string INVALID_AURGUMENTS_PREFIX="IARGS";
        public static string RECORD_NOT_INTRESTED_PREFIX="RNINS";
        public static string TABLE_CONVERSION_PREFIX="TBCNV";
        public static string GENERAL_ERROR_CODE_PREFIX="GEC";
        public static string LEVEL_ONE_PREFIX = "01";
        public static string LEVEL_TWO_PREFIX = "02";

        public static string OPERATION_LOAD_PREFIX = "OLD";
        public static string OPERATION_SAVE_PREFIX = "OSV";
        public static string OPERATION_UPDATE_PREFIX = "OUP";
        public static string OPERATION_SIGN_PREFIX = "OSGN";
        public static string OPERATION_MANAGE_PREFIX = "OMG";


        public static string IMMUNIZATION_PREFIX="IMM";

        public static string SECTION_QUICK_INPUT_PREFIX = "QI";
        public static string SECTION_VIEWBACKGROUND_PREFIX="VB";
       
       
        public static string VISIT_PREFIX="VST";
       
        public static string SOAP_TEMPLATE_PREFIX="SPTP";
        public static string ENCOUNTER_TYPES_PREFIX="ENTYP";

     
        public static string OPERATION_MARK_VALID_PREFIX = "OMRKVD";
        public static string OPERATION_CALCULATE_SCORE_PREFIX="OCALCSCR";

        public static string MEDICAL_HISTORY_TYPES_PREFIX="MDHTYP";
        public static string FAMILY_HISTORY_TYPES_PREFIX="FMHTYP";
       

        public static string DTO_CONVERSION_ERROR_PREFIX = "DTOCNV";
        public static string PARAMETER_POPULATOR_PREFIX="PRPO";
        public static string QUICK_INPUT_PREFIX="SECQI";

        public static string MEDICAL_HISTORY_PREFIX = "MDH";
        public static string FAMILY_HISTORY_PREFIX="FMH";
        public static string SOCIAL_HISTORY_PREFIX="SCH";
        public static string SOCIAL_HISTORY_ADDICTION_HISTORY_PREFIX="SHADD";
        public static string VITALS_PREFIX="VTL";
        public static string HPI_PREFIX="HPI";
        public static string LABS_PREFIX="LABS";
        public static string IN_HOUSE_LABS_PREFIX="INLBS";
        public static string ROS_PREFIX="ROS";
        public static string PHYSICALS_PREFIX="PHY";
        public static string DIAGNOSIS_PREFIX="DGN";
        public static string RECOMMENDATION_PREFIX="RECMD";
        public static string FOLLOW_UP_PREFIX="FOLUP";
        public static string FOLLOW_UP_INSTRUCTION_PREFIX="FLUPINS";
        public static string PROCEDURE_PREFIX="PROC";
        
        
       
       

        public static string ComposeErrorCode(String ERROR_CODE_PREFIX, String SECTION_PREFIX, String SUB_SECTION_PREFIX, String OPERATION_PREFIX, String ERROR_PREFIX)
        {
            return ERROR_CODE_PREFIX + SECTION_PREFIX + ErrorCodes.SEPRATOR +
                                    SUB_SECTION_PREFIX + ErrorCodes.SEPRATOR + OPERATION_PREFIX +
                                    ERROR_PREFIX;
        }
    }
}
