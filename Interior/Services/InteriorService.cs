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
    public class InteriorService : IInteriorService
    {
        private readonly ApplicationContext _context;
        public InteriorService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<ResultCode> AddInterior(Models.Entities.Interior interior)
        {
            try
            {
                interior.Id = 0;
                _context.Interiors.Add(interior);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteInterior(Models.Entities.Interior interior)
        {
            try
            {
                var currentInterior = await _context.Interiors.SingleOrDefaultAsync(n => n.Id == interior.Id);
                if (currentInterior == null)
                    return ResultCode.Error;
                _context.Interiors.Remove(currentInterior);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<IEnumerable<Models.Entities.Interior>> GetAllInteriors()
        {
            return await _context.Interiors.ToListAsync();
        }

        public async Task<ResultCode> UpdateInterior(Models.Entities.Interior interior)
        {
            try
            {
                var currentInterior = await _context.Interiors.SingleOrDefaultAsync(n => n.Id == interior.Id);
                if (currentInterior == null)
                    return ResultCode.Error;
                _context.Interiors.Update(currentInterior);
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
