using Interior.Enums;
using Interior.Models.EFContext;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationContext _context;
        public LanguageService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<ResultCode> AddLanguage(Language language)
        {
            try
            {
                language.Id = 0;
                _context.Languages.Add(language);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteLanguage(Language language)
        {
            try
            {
                var currentLanguage = await _context.Languages.SingleOrDefaultAsync(n => n.Id == language.Id);
                if (currentLanguage == null)
                    return ResultCode.Error;
                _context.Languages.Remove(currentLanguage);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<IEnumerable<Language>> GetAllLanguages()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<ResultCode> UpdateLanguage(Language language)
        {
            try
            {
                var currentLanguage = await _context.Languages.SingleOrDefaultAsync(n => n.Id == language.Id);
                if (currentLanguage == null)
                    return ResultCode.Error;
                _context.Languages.Update(currentLanguage);
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
