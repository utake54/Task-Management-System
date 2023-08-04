using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.CommonModel;

namespace TaskManagement.Model.Model.Task.Request
{
    public class TaskRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Please enter valid task title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter valid task description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select priority of task.")]
        public string Priority { get; set; }

        [Required(ErrorMessage ="Please select due date.")]
        public DateTime DueDate { get; set; }
    }
}
