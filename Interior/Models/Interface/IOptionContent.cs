using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
   public  interface IOptionContent
    {
        Task<ResultCode> AddOptionContentsAsync (OptionContent content);
        Task<ResultCode> EditOptionContentsAsync(OptionContent content);
        Task<ResultCode> DeleteOptionContentsAsync(int id);
    }
}
