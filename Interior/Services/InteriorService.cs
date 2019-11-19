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
        public async Task<ResultCode> AddInteriorAsync(Models.Entities.Interior interior)
        {
            try
            {
                interior.Id = 0;
                _context.Interiors.Add(interior);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception e)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteInteriorAsync(Models.Entities.Interior interior)
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
        private IQueryable<Interior.Models.Entities.Interior> OrderTable(IQueryable<Interior.Models.Entities.Interior> data, string columnName, bool desc)
        {
            switch (columnName)
            {
                case "Id":
                    if (desc)
                        return data.OrderBy(x => x.Id);
                    else
                        return data.OrderByDescending(x => x.Id);
                case "Price":
                    if (desc)
                        return data.OrderBy(x => x.Price);
                    else
                        return data.OrderByDescending(x => x.Price);
                case "DeepLinkUrl":
                    if (desc)
                        return data.OrderBy(x => x.BuyUrl);
                    else
                        return data.OrderByDescending(x => x.BuyUrl);
                default:
                    return null;
            }
        }

        public async Task<(IEnumerable<Interior.Models.Entities.Interior>, int count)> GetAllInteriorsAsync(int? skip, int? take, bool? desc, string columnName)
        {
            try
            {
                var lenght = await _context.Interiors.CountAsync();
                IQueryable<Interior.Models.Entities.Interior> data = null;
                if (skip != null || take != null)
                    data = _context.Interiors.Include(s=>s.ContentAttachments).ThenInclude(s=>s.Content).Skip((int)skip).Take((int)take);
                else
                    data = _context.Interiors.Include(s=>s.ContentAttachments).ThenInclude(s=>s.Content);
                if (desc != null && columnName != null)
                    data = OrderTable(data, columnName, (bool)desc);

                return (await data.AsNoTracking().ToListAsync(), lenght);
            }
            catch (Exception)
            {
                return (null, 0);
            }
        }
        public async Task<Interior.Models.Entities.Interior> GetByIdAsync(int id)
        {
            return await _context.Interiors
                .Include(s=>s.ContentAttachments).ThenInclude(s=>s.Content)
                .Include(s=>s.FilesAttachments).ThenInclude(s=>s.File)
                .Include(s=>s.OptionContents)
                .AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ResultCode> UpdateInteriorAsync(Models.Entities.Interior interior)
        {
            try
            {
                var currentInterior = await _context.Interiors.AsNoTracking().SingleOrDefaultAsync(n => n.Id == interior.Id);
                if (currentInterior == null)
                    return ResultCode.Error;
                _context.Interiors.Update(interior);
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
