using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interior.Enums;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.EFContext;

using Microsoft.EntityFrameworkCore;


namespace Interior.Services
{
    public class ShopService : IShopService
    {
        private readonly ApplicationContext _context;
        public ShopService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<ResultCode> AddShopAsync(Shop shop)
        {
            try
            {
                shop.Id = 0;
                _context.Shops.Add(shop);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteShopAsync(Shop shop)
        {
            try
            {
                var currentShop = await _context.Shops.SingleOrDefaultAsync(n => n.Id == shop.Id);
                if (currentShop == null)
                    return ResultCode.Error;
                _context.Shops.Remove(currentShop);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }


        public async Task<(IEnumerable<Shop>, int count)> GetAllShopsAsync()
        {
            try
            {
                var model = await _context.Shops.Include(s => s.Contents).AsNoTracking().ToListAsync();
                return (model, model.Count);

            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<Shop> GetShopById(int id)
        {
            return await _context.Shops.Include(s => s.Contents).Include(s => s.File).AsNoTracking().SingleOrDefaultAsync(i => id == i.Id);
        }

        public async Task<ResultCode> UpdateShopAsync(Shop shop)
        {
            try
            {
                var currentShop = await _context.Shops.AsNoTracking().SingleOrDefaultAsync(n => n.Id == shop.Id);
                if (currentShop == null)
                    return ResultCode.Error;
                _context.Shops.Update(shop);
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
