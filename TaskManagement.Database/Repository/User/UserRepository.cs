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
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Service.Entities.Login;

namespace TaskManagement.Database.Repository.UserRepository
{
    public class UserRepository : Repository<UserMaster>, IUserRepository
    {
        public UserRepository(MasterDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers(int companyId, int pageNumber = 1, int pageSize = 3)
        {
            var skip = ((pageNumber * pageSize) - pageSize);

            var allUsers = await (from u in Context.UserMaster
                                  where u.CompanyId == companyId
                                  select new UserDTO
                                  {
                                      Id = u.Id,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      EmailId = u.EmailId,
                                      MobileNo = u.MobileNo,
                                      DateOfBirth = u.DateOfBirth.ToString()
                                  })
                                  .Skip(skip)
                                  .Take(pageSize)
                                  .OrderBy(u => u.Id)
                                  .ToListAsync();
            return allUsers;
        }

        public async Task<LoginDTO> GetUserDetails(LoginDto request)
        {
            var user = await (from u in Context.UserMaster
                              join r in Context.RoleMaster
                              on u.RoleId equals r.RoleId
                              where u.EmailId == request.EmailOrMobile && u.Password == request.Password && u.IsActive == true
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
