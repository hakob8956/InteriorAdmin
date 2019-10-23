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
        Task<ResultCode> AddLanguage(Language language);
        Task<ResultCode> DeleteLanguage(Language language);
        Task<ResultCode> UpdateLanguage(Language language);
        Task<IEnumerable<Language>> GetAllLanguages();
       
    }
}
