using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler;
using BulkUploadingUtility.Utility;
using BulkUploadingUtility.Model;

namespace BulkUploadingUtility.BL
{
    public class SchedularController
    {
        public bool AddTask(UtilityTask utltyTask)
        {
            bool response = false;
            try
            {
                ScheduledTasks UtilitySchular = new ScheduledTasks();
                string[] taskList = UtilitySchular.GetTaskNames();
                string taskNme = UConfigurationManager.TaskName + ".job";
                if (taskList != null)
                {
                    foreach (string taskName in taskList)
                    {
                        if (taskName == taskNme)
                        {
                            UtilitySchular.DeleteTask(taskNme);
                        }
                    }
                }
                string appName = UConfigurationManager.ApplicationExeNameAndPathUrl;
                appName = appName.Substring(8);
                Task task = UtilitySchular.CreateTask(UConfigurationManager.TaskName);
                task.ApplicationName = appName;
                task.SetAccountInformation(utltyTask.UserName, utltyTask.Password);
                task.Save();
                task.Close();
                UtilitySchular.Dispose();
                response = true;
            }
            catch
            {
                
            }
            return response;
        }

        public bool AddTrigger(UtilityTrigger utltyTrigger)
        {
            bool response = false;
            try
            {
                ScheduledTasks UtilitySchular = new ScheduledTasks();
                Task task = UtilitySchular.OpenTask(UConfigurationManager.TaskName);
                if (task.Triggers.Count > 0)
                {
                    task.Triggers.Clear();
                }
                if (utltyTrigger.Type == 1)
                {
                    DailyTrigger dailyTrigger = new DailyTrigger(utltyTrigger.Hour, utltyTrigger.Min);
                    task.Triggers.Add(dailyTrigger);
                }
                else if (utltyTrigger.Type == 2)
                {
                    WeeklyTrigger weekTigger = new WeeklyTrigger(utltyTrigger.Hour, utltyTrigger.Min, utltyTrigger.WeekDay);
                    task.Triggers.Add(weekTigger);
                }
                else if (utltyTrigger.Type == 3)
                {
                    MonthlyTrigger montlyTigger = new MonthlyTrigger(utltyTrigger.Hour, utltyTrigger.Min, utltyTrigger.Day);
                    task.Triggers.Add(montlyTigger);
                }
                task.SetAccountInformation(utltyTrigger.UserName,utltyTrigger.Password);
                task.Save();
                task.Close();
                UtilitySchular.Dispose();
                response = true;
            }
            catch
            {
                throw;
            }
            return response;
        }
        public bool IsTaskCreated()
        {
            bool response = false;
            try
            {
                ScheduledTasks UtilitySchular = new ScheduledTasks();
                Task task = UtilitySchular.CreateTask(UConfigurationManager.TaskName);
            }
            catch (ArgumentException)
            {
                response = true;
            }
            return response;
        }
    }
}
