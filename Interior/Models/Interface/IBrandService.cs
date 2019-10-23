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
        Task<ResultCode> AddBrand(Brand brand);
        Task<ResultCode> DeleteBrand(Brand brand);
        Task<ResultCode> UpdateBrand(Brand brand);
        Task<IEnumerable<Brand>> GetAllBrands();
    }
}
