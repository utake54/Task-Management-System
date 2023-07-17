﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Login.DTO;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;

namespace TaskManagement.Database.Repository.UserRepository
{
    public interface IUserRepository : IRepository<UserMaster>
    {
        Task<LoginDTO> GetUserDetails(LoginRequest request);
        Task<IEnumerable<UserDTO>> GetAllUsers(int companyId, int pageNumber, int pageSize);
    }
}
