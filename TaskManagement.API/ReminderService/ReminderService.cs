﻿using Hangfire;
using TaskManagement.Service.OverDueService;

namespace TaskManagement.API.Reminders
{
    public static class ReminderService
    {
        private static IOverdueService overdueService = null;
        private static OverdueJobs jobscheduler = new OverdueJobs(overdueService);
        public static void NightlyReminderService(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate("OverdueService", () => jobscheduler.GetOverdueTask(), Cron.HourInterval(1));
        }
    }
}
