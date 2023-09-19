using AutoMapper;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Service.Entities.Category;

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

        public async Task<ResponseModel> AddAsync(AddCategoryDto requestDto)
        {
            var response = new ResponseModel();
            var category = _mapper.Map<AddCategoryDto, TaskCategoryMaster>(requestDto);
            category.CreatedDate = DateTime.UtcNow;
            category.CreatedBy = requestDto.CreadetBy;
            category.IsActive = true;
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> DeleteAsync(DeleteCategoryDto requestDto)
        {
            var response = new ResponseModel();

            var category = await _unitOfWork.CategoryRepository.GetById(requestDto.Id);
            if (category == null)
            {
                response.Failure("TM052");
                return response;
            }
            category.IsActive = false;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> GetAsync(GetCategoryDto pageResult)
        {
            var response = new ResponseModel();

            var categories = await _unitOfWork.CategoryRepository.GetAllCategories(pageResult);
            if (!categories.Any())
            {
                response.Failure("TM052");
                return response;
            }

            response.Ok(categories);
            return response;
        }

        public async Task<ResponseModel> GetByIdAsync(GetByIdCategoryDto requestDto)
        {
            var response = new ResponseModel();
            var category = await _unitOfWork.CategoryRepository.GetDefault(x => x.Id == requestDto.Id && x.IsActive == true);
            if (category == null)
            {
                response.Failure("TM052");
                return response;
            }
            var categoryDTO = _mapper.Map<TaskCategoryMaster, CategoryMasterDto>(category);
            response.Ok(categoryDTO);
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(UpdateCategoryDto requestDto)
        {
            var response = new ResponseModel();
            var category = await _unitOfWork.CategoryRepository.GetById(requestDto.Id);
            if (category == null)
            {
                response.Failure("TM052");
                return response;
            }

            category.Category = requestDto.Category;
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }
    }
}
