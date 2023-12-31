﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.Task.DTO;

namespace TaskManagement.Database.Repository.Task
{
    public class TaskRepository : Repository<TaskMaster>, ITaskRepository
    {
        public TaskRepository(MasterDbContext context) : base(context)
        {
        }

        public async Task<AllTaskDTO> GetAllTask(int companyId, SearchModel searchModel)
        {
            var sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@CompanyId",companyId),
                new SqlParameter("@Search",searchModel.Search),
                new SqlParameter("@PageNumber",searchModel.PageNumber),
                new SqlParameter("@PageSize",searchModel.PageSize),
                new SqlParameter("@Status",searchModel.Status),
                new SqlParameter("@OrderBy",searchModel.OrderBy),
                new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            var response = new AllTaskDTO();
            response.Tasks = await SQLHelper.GetDataAsync<TaskExportDTO>("USP_GetAllTask_WithPage", sqlParameters);
            response.TotalRecords = (int)sqlParameters.Single(p => p.ParameterName == "@TotalRecords").Value;

            return response;
        }

        public async Task<IEnumerable<MyTaskDTO>> GetMyTask(int userId)
        {
            var task = await (from tm in Context.TaskMaster
                              join at in Context.AssignedTask
                              on tm.Id equals at.TaskId
                              join c in Context.TaskCategoryMaster
                              on tm.CategoryId equals c.Id
                              join um in Context.UserMaster on at.AssignedBy equals um.Id
                              where at.UserId == userId && tm.IsActive == true

                              select new MyTaskDTO
                              {
                                  Id = tm.Id,
                                  Task = tm.Title,
                                  Category = c.Category,
                                  Description = tm.Description,
                                  Priority = tm.Priority,
                                  AssignedDate = at.AssignedDate.ToString("dd-MM-yyyy"),
                                  DueDate = tm.DueDate.ToString("dd-MM-yyyy"),
                                  CompletedDate = at.EndDate.ToString("dd-MM-yyyy"),
                                  AssignedBy = um.FirstName
                              }).ToListAsync();
            return task;
        }

        public async Task<List<TaskExportDTO>> GetTaskDetails(int companyId)
        {
            var sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@CompanyId",companyId)
            };

            var data = await SQLHelper.GetDataAsync<TaskExportDTO>("USP_GetTaskExport", sqlParameters);

            return (List<TaskExportDTO>)data;
        }

        public async Task<List<TaskMasterDto>> TaskByCategory(int categoryId)
        {
            var data = await (from t in Context.TaskMaster
                              join c in Context.TaskCategoryMaster
                              on t.CategoryId equals c.Id
                              where c.Id == categoryId && t.IsActive == true
                              select new TaskMasterDto
                              {
                                  Id = t.Id,
                                  Task = t.Title,
                                  Description = t.Description,
                                  Priority = t.Priority,
                                  AssignedDate = t.CreatedDate.ToShortDateString(),
                                  DueDate = t.DueDate.ToShortDateString()
                              }).ToListAsync();

            return data;
        }
    }
}
