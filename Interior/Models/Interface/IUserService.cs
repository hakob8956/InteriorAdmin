using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<ResultCode> CreateUserAsync(User user);
        Task<ResultCode> UpdateUserAsync(User user);
        Task<(IEnumerable<User>, int count)> GetAllUsersAsync(int? skip,int? take);
        Task<(IEnumerable<User>, int count)> GetAllUsersAsync(int? skip, int? take,string roleName);
        Task<User> GetByIdAsync(int id);
    }
}
