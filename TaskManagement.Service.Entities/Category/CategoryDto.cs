using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.Entities.Category
{
    public class CategoryMasterDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
    }
    public class AddCategoryDto
    {
        public string Category { get; set; }
        public int CreadetBy { get; set; }
    }

    public class UpdateCategoryDto : GetByIdCategoryDto
    {
        public string Category { get; set; }
        public int ActionBy { get; set; }
    }
    public class GetByIdCategoryDto
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryDto : GetByIdCategoryDto
    {
        public int ActionBy { get; set; }
    }
    public class GetCategoryDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
