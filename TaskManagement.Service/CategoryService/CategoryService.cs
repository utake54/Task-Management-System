﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.Category.Request;
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
            category.CreatedDate = DateTime.Now;
            category.CreatedBy = userId;
            category.IsActive = true;
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CategoryRepository.SaveChanges();
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
           
            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.CategoryRepository.SaveChanges();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> GetAllCategories()
        {
            var response = new ResponseModel();
            var categoriesDTO = new List<CategoryDTO>();
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            if (!categories.Any())
            {
                response.Failure("Categories not found.");
                return response;
            }

            foreach (var category in categories)
            {
                var categoryDTO = _mapper.Map<TaskCategoryMaster, CategoryDTO>(category);
                categoriesDTO.Add(categoryDTO);
            }
            response.Ok(categoriesDTO);
            return response;
        }

        public async Task<ResponseModel> GetCategory(int categoryId)
        {
            var response = new ResponseModel();
            var category = await _unitOfWork.CategoryRepository.GetById(categoryId);
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
            await _unitOfWork.CategoryRepository.SaveChanges();
            response.Ok();
            return response;
        }
    }
}