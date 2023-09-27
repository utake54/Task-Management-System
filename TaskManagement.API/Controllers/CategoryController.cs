using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Request;
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
            return APIResponse(category.Data, category.Message);
        }

        [HttpPost("GetAllCategories")]
        public async Task<Dictionary<string, object>> GetAsync(GetCategoryRequest request)
        {
            var requestDto = _mapper.Map<GetCategoryDto>(request);
            var categories = await _categoryService.GetAsync(requestDto);
            return APIResponse(categories.Data, categories.Message);
        }

        [HttpPost("AddCategory")]
        public async Task<Dictionary<string, object>> AddAsync(AddCategoryRequest request)
        {
            var requestDto = _mapper.Map<AddCategoryDto>(request);
            requestDto.CreadetBy = UserId;
            requestDto.CompanyId = CompanyId;
            var category = await _categoryService.AddAsync(requestDto);
            return APIResponse(category.Result, category.Message, "TM049");
        }

        [HttpPost("UpdateCategory")]
        public async Task<Dictionary<string, object>> UpdateAsync(UpdateCategoryRequest request)
        {
            var requestDto = _mapper.Map<UpdateCategoryDto>(request);
            var category = await _categoryService.UpdateAsync(requestDto);
            return APIResponse(category.Result, category.Message, "TM050");
        }

        [HttpPost("DeleteCategory")]
        public async Task<Dictionary<string, object>> DeleteAsync(DeleteCategoryRequest request)
        {
            var requestDto = _mapper.Map<DeleteCategoryDto>(request);
            var category = await _categoryService.DeleteAsync(requestDto);
            return APIResponse(category.Result, category.Message, "TM051");
        }
    }
}
