using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictation.DataAccess
{
    public static class DBConstants
    {
        public static string DICTATION_INSERT_SP = "[Dictation_Insert]";
        public static string DICTATION_UPDATE_SP = "[Dictation_Update]";
        public static string DICTATION_MARK_AS_DELETED_SP = "[Dictation_MarkAsDeleted]";
        public static string DICTATION_LOAD_SP = "[Dictation_LoadByDictationID]";
        public static string DICTATION_STREAM_BY_ID_SP = "[StreamDictation_LoadByDictationID]";

    }
}
