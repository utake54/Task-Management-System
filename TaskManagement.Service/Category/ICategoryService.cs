using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Service.Entities.Category;

namespace TaskManagement.Service.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseModel> AddAsync(AddCategoryDto requestDto);
        Task<ResponseModel> UpdateAsync(UpdateCategoryDto requestDto);
        Task<ResponseModel> DeleteAsync(DeleteCategoryDto requestDto);
        Task<ResponseModel> GetByIdAsync(GetByIdCategoryDto requestDto);
        Task<ResponseModel> GetAsync(GetCategoryDto requestDto);
    }
}
