﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Service.Entities.Category;

namespace TaskManagement.Database.Repository.Category
{
    public interface ICategoryRepository : IRepository<TaskCategoryMaster>
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategories(GetCategoryDto pageResult);
    }
}
