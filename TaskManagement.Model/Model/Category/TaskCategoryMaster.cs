using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Company;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Model.Model.Category
{
    [Table("TaskCategoryMaster")]
    public class TaskCategoryMaster
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyMaster CompanyMaster { get; set; }

        [ForeignKey("CreatedBy")]
        public UserMaster UserMaster { get; set; }

        public ICollection<TaskMaster> TaskMasters { get; set; }
        public ICollection<AssignTask> AssignTasks { get; set; }
    }
}
