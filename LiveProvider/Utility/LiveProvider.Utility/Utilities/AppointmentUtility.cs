using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveProvider.Utility.Constant.ProjectConstant;

namespace LiveProvider.Utility.Utilities
{
    public class AppointmentUtility
    {
        public static DateTime GetEndDateByViewType(int viewTypeId)
        {
            int daysToAdd = 0;

            if (viewTypeId == (int)SchedulerConstant.ViewTypes.Today)
            {
                daysToAdd = 0;
            }
            else  if (viewTypeId == (int)SchedulerConstant.ViewTypes.WorkingWeek)
            {
                daysToAdd = 5;
            }
            else  if (viewTypeId == (int)SchedulerConstant.ViewTypes.Week)
            {
                daysToAdd = 7;
            }
            else  if (viewTypeId == (int)SchedulerConstant.ViewTypes.Month)
            {
                daysToAdd = 30;
            }

            return DateTime.Now.AddDays(30);

        }
    }
}
