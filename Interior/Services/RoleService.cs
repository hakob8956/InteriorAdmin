using Interior.Enums;
using Interior.Models.EFContext;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationContext _context;
        public RoleService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddRoleAsync(Role role)
        {
            try
            {
                var currentRole = await _context.Roles.SingleOrDefaultAsync(n => n.Name == role.Name);
                if (currentRole != null)
                    return ResultCode.Error;
                _context.Roles.Add(currentRole);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;

            }
        }
        public async Task<ResultCode> DeleteRoleAsync(Role role)
        {
            try
            {
                var currentRole = await _context.Roles.SingleOrDefaultAsync(n => n.Id == role.Id);
                if (currentRole == null)
                    return ResultCode.Error;
                _context.Roles.Remove(currentRole);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _context.Roles.SingleOrDefaultAsync(n => n.Name == name);
        }

        public async Task<ResultCode> UpdateRoleAsync(Role role)
        {
            try
            {
                var currentRole = await _context.Roles.SingleOrDefaultAsync(n => n.Id == role.Id);
                if (currentRole == null)
                    return ResultCode.Error;
                _context.Roles.Update(currentRole);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
    }
}
