using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;

namespace TaskManagement.Service.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseModel> AddCategory(CategoryRequest request, int userId);
        Task<ResponseModel> UpdateCategory(CategoryRequest request);
        Task<ResponseModel> DeleteCategory(int categoryId);
        Task<ResponseModel> GetCategory(int categoryId);
        Task<ResponseModel> GetAllCategories(PageResult pageResult);
    }
}
