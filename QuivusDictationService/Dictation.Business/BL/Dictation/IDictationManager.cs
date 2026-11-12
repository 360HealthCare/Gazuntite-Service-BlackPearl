using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictation.Business.BL
{
    public interface IDictationManager
    {
        void SaveDictationFile(string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy, byte[] FileContents);

        void UpdateDictationFile(string FileName, string RecordTimeStamp, string SessionID, string CreatedAt, string CreatedBy, byte[] FileContents);
        
        void MarkDictationAsDeleted(Guid DictationID, Guid SessionID, DateTime CreatedAt, int CreatedBy);

        byte[] StreamDictationByID(string DictationID, string SessionID);
    }
}
