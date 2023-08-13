using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
