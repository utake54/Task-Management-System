using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.CommonModel;
using TaskManagement.Model.Model.Company;

namespace TaskManagement.Model.Model.Task
{
    [Table("TaskMaster")]
    public class TaskMaster : StandardFields
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyMaster CompanyMaster { get; set; }


        [ForeignKey("CategoryId")]
        public TaskCategoryMaster TaskCategoryMaster { get; set; }



    }
}
