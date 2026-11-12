using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler;

namespace BulkUploadingUtility.Model
{
    public class UtilityTrigger : UtilityBase
    {
        public int Type { get; set; }

        public short Hour { get; set; }

        public short Min { get; set; }

        public DaysOfTheWeek WeekDay { get; set; }

        public int[] Day { get; set; }
    }
}
