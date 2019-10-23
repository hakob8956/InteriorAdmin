using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IBrandService
    {
        Task<ResultCode> AddBrandAsync(Brand brand);
        Task<ResultCode> DeleteBrandAsync(Brand brand);
        Task<ResultCode> UpdateBrandAsync(Brand brand);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
