using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Interior.Models.Interface
{
    public interface IInteriorService
    {
        Task<ResultCode> AddInteriorAsync(Interior.Models.Entities.Interior interior);
        Task<ResultCode> DeleteInteriorAsync(Interior.Models.Entities.Interior interior);
        Task<ResultCode> UpdateInteriorAsync(Interior.Models.Entities.Interior interior);
        Task<(IEnumerable<Interior.Models.Entities.Interior>, int count)> GetAllInteriorsAsync(int? skip, int? take, bool? desc, string columnName);
        Task<Interior.Models.Entities.Interior> GetByIdAsync(int id);
    }
}
