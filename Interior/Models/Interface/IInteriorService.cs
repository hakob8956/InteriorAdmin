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
        Task<ResultCode> AddInterior(Interior.Models.Entities.Interior interior);
        Task<ResultCode> DeleteInterior(Interior.Models.Entities.Interior interior);
        Task<ResultCode> UpdateInterior(Interior.Models.Entities.Interior interior);
        Task<IEnumerable<Interior.Models.Entities.Interior>> GetAllInteriors();
    }
}
