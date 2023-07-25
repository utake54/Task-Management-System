using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.OverDueService
{
    public class OverdueJobs
    {
        private readonly IOverdueService _overdueService;
        public OverdueJobs(IOverdueService overdueService)
        {
            _overdueService = overdueService;
        }
        public async Task GetOverdueTask() => await _overdueService.TaskOverdue();
    }
}
