using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Service.CategoryService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("GetCategory/{categoryId}")]
        public async Task<Dictionary<string, object>> GetCategory(int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }

        [HttpPost("GetAllCategories")]
        public async Task<Dictionary<string, object>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories.Message == "Success")
                return APIResponse("Success", categories.Data);
            return FailureResponse("Failed", categories.Message);
        }

        [HttpPost("AddCategory")]
        public async Task<Dictionary<string, object>> AddCategory(CategoryRequest request)
        {
            var category = await _categoryService.AddCategory(request, UserId);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }

        [HttpPost("UpdateCategory")]
        public async Task<Dictionary<string, object>> UpdateCategory(CategoryRequest request)
        {
            var category = await _categoryService.UpdateCategory(request);
            if (category.Message == "Success")
                return APIResponse("Success", category.Data);
            return FailureResponse("Failed", category.Message);
        }

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
