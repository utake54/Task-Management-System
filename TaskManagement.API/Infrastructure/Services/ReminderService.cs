using Hangfire;
using TaskManagement.Service.OverDueService;

namespace TaskManagement.API.Infrastructure.Services
{
    public static class ReminderService
    {
        private static readonly IOverdueService _overdueService = null;
        private static readonly OverdueJobs _jobscheduler = new OverdueJobs(_overdueService);

        public static IServiceCollection AddRecurringJobManager(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
            NightlyReminderService(recurringJobManager);
            return services;
        }
        public static void NightlyReminderService(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate("OverdueService", () => _jobscheduler.GetOverdueTask(), Cron.Daily());
        }
    }
}
