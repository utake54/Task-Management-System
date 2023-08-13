using AutoMapper;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;

namespace TaskManagement.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddCategory(CategoryRequest request, int userId)
        {
            var response = new ResponseModel();
            var category = _mapper.Map<CategoryRequest, TaskCategoryMaster>(request);
            category.CreatedDate = DateTime.UtcNow;
            category.CreatedBy = userId;
            category.IsActive = true;
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> DeleteCategory(int categoryId)
        {
            var response = new ResponseModel();

            var category = await _unitOfWork.CategoryRepository.GetById(categoryId);
            if (category == null)
            {
                response.Failure("Category not found.");
                return response;
            }
            category.IsActive = false;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> GetAllCategories(PageResult pageResult)
        {
            var response = new ResponseModel();

            var categories = await _unitOfWork.CategoryRepository.GetAllCategories(pageResult);
            if (!categories.Any())
            {
                response.Failure("Categories not found.");
                return response;
            }

            response.Ok(categories);
            return response;
        }

        public async Task<ResponseModel> GetCategory(int categoryId)
        {
            var response = new ResponseModel();
            var category = await _unitOfWork.CategoryRepository.GetDefault(x => x.Id == categoryId && x.IsActive == true);
            if (category == null)
            {
                response.Failure("Category not found.");
                return response;
            }
            var categoryDTO = _mapper.Map<TaskCategoryMaster, CategoryDTO>(category);
            response.Ok(categoryDTO);
            return response;
        }

        public async Task<ResponseModel> UpdateCategory(CategoryRequest request)
        {
            var response = new ResponseModel();
            var category = await _unitOfWork.CategoryRepository.GetById(request.Id);
            if (category == null)
            {
                response.Failure("Category not found.");
                return response;
            }

            category.Category = request.Category;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }
    }
}
