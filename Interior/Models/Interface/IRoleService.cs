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
        Task<ResultCode> AddRole(Role role);
        Task<ResultCode> DeleteRole(Role role);
        Task<ResultCode> UpdateRole(Role role);
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
