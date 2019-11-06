using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IShopService
    {
        Task<ResultCode> AddShopAsync(Shop shop);
        Task<ResultCode> DeleteShopAsync(Shop shop);
        Task<ResultCode> UpdateShopAsync(Shop shop);
        Task<(IEnumerable<Shop>, int count)> GetAllShopsAsync();
        Task<Shop> GetShopById(int id);
    }
}
