using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Service.CategoryService;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// This is Category controller to control over task categories
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Permissible("Admin")]

    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// This is for get details of selected category option.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>If category found as per given id return category in details</returns>
        [HttpPost("GetCategory/{categoryId}")]
        public async Task<Dictionary<string, object>> GetCategory(int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }

        /// <summary>
        /// This return all categories from table
        /// </summary>
        /// <returns>All categories</returns>
        [HttpPost("GetAllCategories")]
        public async Task<Dictionary<string, object>> GetAllCategories(PageResult pageResult)
        {
            var categories = await _categoryService.GetAllCategories(pageResult);
            if (categories.Message == "Success")
                return APIResponse("Success", categories.Data);
            return FailureResponse("Failed", categories.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddCategory")]
        public async Task<Dictionary<string, object>> AddCategory(CategoryRequest request)
        {
            var category = await _categoryService.AddCategory(request, UserId);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UpdateCategory")]
        public async Task<Dictionary<string, object>> UpdateCategory(CategoryRequest request)
        {
            var category = await _categoryService.UpdateCategory(request);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }
        /// <summary>
        /// z
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost("DeleteCategory/{categoryId}")]
        public async Task<Dictionary<string, object>> DeleteCategory(int categoryId)
        {
            var category = await _categoryService.DeleteCategory(categoryId);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }
    }
}
