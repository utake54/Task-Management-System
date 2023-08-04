using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Category.Request
{
    public class CategoryRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter category.")]
        public string Category { get; set; }
    }
}
