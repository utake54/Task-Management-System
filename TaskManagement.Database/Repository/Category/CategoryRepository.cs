using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;

namespace TaskManagement.Database.Repository.Category
{
    public class CategoryRepository : Repository<TaskCategoryMaster>, ICategoryRepository
    {
        public CategoryRepository(MasterDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await (from c in Context.TaskCategoryMaster
                                    where c.IsActive == true
                                    select new CategoryDTO
                                    {
                                        Id = c.Id,
                                        Category = c.Category
                                    }).ToListAsync();

            return categories;
        }
    }
}
