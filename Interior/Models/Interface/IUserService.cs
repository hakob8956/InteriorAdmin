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
        Task<ResultCode> Authenticate(string username, string password);
        Task<ResultCode> CreateUserAsync(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
