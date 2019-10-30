using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IRoleService
    {
        Task<ResultCode> AddRoleAsync(Role role);
        Task<ResultCode> DeleteRoleAsync(Role role);
        Task<ResultCode> UpdateRoleAsync(Role role);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByName(string name);
    }
}
