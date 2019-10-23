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
        public async Task<ResultCode> AddShop(Shop shop)
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

        public async Task<ResultCode> DeleteShop(Shop shop)
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

        public async Task<IEnumerable<Shop>> GetAllShops()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<ResultCode> UpdateShop(Shop shop)
        {
            try
            {
                var currentShop = await _context.Shops.SingleOrDefaultAsync(n => n.Id == shop.Id);
                if (currentShop == null)
                    return ResultCode.Error;
                _context.Shops.Update(currentShop);
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
