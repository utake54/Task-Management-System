using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Service.CategoryService;
using TaskManagement.Service.Entities.Category;

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
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("GetCategory")]
        public async Task<Dictionary<string, object>> GetByIdAsync(GetByIdCategoryRequest request)
        {
            var requestDto = _mapper.Map<GetByIdCategoryDto>(request);
            var category = await _categoryService.GetByIdAsync(requestDto);
            if (category.Message == "Success")
                return APIResponse("TM002A", category.Data);
            return FailureResponse("TM002B", category.Message);
        }

        [HttpPost("GetAllCategories")]
        public async Task<Dictionary<string, object>> GetAsync(GetCategoryRequest request)
        {
            var requestDto = _mapper.Map<GetCategoryDto>(request);
            var categories = await _categoryService.GetAsync(requestDto);
            if (categories.Message == "Success")
                return APIResponse("TM002A", categories.Data);
            return FailureResponse("TM002B", categories.Message);
        }

        [HttpPost("AddCategory")]
        public async Task<Dictionary<string, object>> AddAsync(AddCategoryRequest request)
        {
            var requestDto = _mapper.Map<AddCategoryDto>(request);
            var category = await _categoryService.AddAsync(requestDto);
            if (category.Message == "Success")
                return APIResponse("TM002A", "TM020");
            return FailureResponse("TM002B", category.Message);
        }

        [HttpPost("UpdateCategory")]
        public async Task<Dictionary<string, object>> UpdateAsync(UpdateCategoryRequest request)
        {
            var requestDto = _mapper.Map<UpdateCategoryDto>(request);
            var category = await _categoryService.UpdateAsync(requestDto);
            if (category.Message == "Success")
                return APIResponse("TM021", category.Data);
            return FailureResponse("TM002B", category.Message);
        }

        [HttpPost("DeleteCategory")]
        public async Task<Dictionary<string, object>> DeleteAsync(DeleteCategoryRequest request)
        {
            var requestDto = _mapper.Map<DeleteCategoryDto>(request);
            var category = await _categoryService.DeleteAsync(requestDto);
            if (category.Message == "Success")
                return APIResponse("TM022", category.Data);
            return FailureResponse("TM002B", category.Message);
        }
    }
}
