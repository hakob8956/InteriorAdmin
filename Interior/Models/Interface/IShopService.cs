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
        Task<ResultCode> AddShop(Shop shop);
        Task<ResultCode> DeleteShop(Shop shop);
        Task<ResultCode> UpdateShop(Shop shop);
        Task<IEnumerable<Shop>> GetAllShops();
    }
}
