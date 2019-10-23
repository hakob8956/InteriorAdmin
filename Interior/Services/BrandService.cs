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
    public class BrandService : IBrandService
    {
        private readonly ApplicationContext _context;
        public BrandService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddBrand(Brand brand)
        {
            try
            {
                brand.Id = 0;
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteBrand(Brand brand)
        {
            try
            {
                var currentBrand = await _context.Brands.SingleOrDefaultAsync(n => n.Id == brand.Id);
                if (currentBrand == null)
                    return ResultCode.Error;
                _context.Brands.Remove(currentBrand);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<ResultCode> UpdateBrand(Brand brand)
        {
            try
            {
                var currentBrand = await _context.Brands.SingleOrDefaultAsync(n => n.Id == brand.Id);
                if (currentBrand == null)
                    return ResultCode.Error;
                _context.Brands.Update(currentBrand);
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
