using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface ILanguageService
    {
        Task<ResultCode> AddLanguageAsync(Language language);
        Task<ResultCode> DeleteLanguageAsync(Language language);
        Task<ResultCode> UpdateLanguageAsync(Language language);
        Task<(IEnumerable<Language>, int count)> GetLanguagesAsync();
       
    }
}
