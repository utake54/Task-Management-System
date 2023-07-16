using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Login.DTO;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Database.Repository.UserRepository
{
    public class UserRepository : Repository<UserMaster>, IUserRepository
    {
        public UserRepository(MasterDbContext context) : base(context)
        {

        }

        public async Task<LoginDTO> GetUserDetails(LoginRequest request)
        {
            var user = await (from u in Context.UserMaster
                              join r in Context.RoleMaster
                              on u.RoleId equals r.RoleId
                              where u.EmailId == request.UserId && u.Password == request.Password && u.IsActive == true
                              select new LoginDTO
                              {
                                  FirstName = u.FirstName,
                                  Id = u.Id,
                                  RoleId = u.RoleId,
                                  CompanyId = u.CompanyId,
                                  Role = r.Role
                              }).FirstOrDefaultAsync();

            return user;
        }
    }
}
