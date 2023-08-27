using AutoMapper;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.Category.Request;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryDTO, TaskCategoryMaster>().ReverseMap();
            CreateMap<CategoryDTO, TaskCategoryMaster>().ReverseMap();
            CreateMap<CategoryRequest, TaskCategoryMaster>().ReverseMap();
        }
    }
}
