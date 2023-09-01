using AutoMapper;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.Category.Request;
using TaskManagement.Service.Entities.Category;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryDTO, TaskCategoryMaster>().ReverseMap();
            CreateMap<CategoryDTO, TaskCategoryMaster>().ReverseMap();
            CreateMap<CategoryRequest, TaskCategoryMaster>().ReverseMap();
            CreateMap<GetByIdCategoryRequest, GetByIdCategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequest, AddCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryRequest, UpdateCategoryDto>().ReverseMap();
            CreateMap<DeleteCategoryRequest, DeleteCategoryDto>().ReverseMap();
        }
    }
}
