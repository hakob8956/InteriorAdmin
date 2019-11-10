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
        public async Task<ResultCode> AddLanguageAsync(Language language)
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

        public async Task<ResultCode> DeleteLanguageAsync(Language language)
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

        public async Task<Language> GetLanguageByIdAsync(int id)
        {
            return await _context.Languages.Include(s => s.FilesAttachment).ThenInclude(s=>s.File).AsNoTracking().SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<(IEnumerable<Language>, int count)> GetLanguagesAsync()
        {
            var model = await _context.Languages.AsNoTracking().ToListAsync();
            return (model,model.Count);
        }

        public async Task<ResultCode> UpdateLanguageAsync(Language language)
        {
            try
            {
                //var currentLanguage = await _context.Languages.AsNoTracking().SingleOrDefaultAsync(n => n.Id == language.Id);
                //if (currentLanguage == null)
                //    return ResultCode.Error;
                _context.Languages.Update(language);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception e)
            {
                return ResultCode.Error;
            }
        }
    }
}
