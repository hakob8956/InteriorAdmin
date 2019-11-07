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
    public class OptionContentService : IOptionContent
    {
        private readonly ApplicationContext _context;
        public OptionContentService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddOptionContentsAsync(OptionContent content)
        {
            try
            {
                content.Id = 0;
                _context.OptionContents.Add(content);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
        public async Task<ResultCode> EditOptionContentsAsync(OptionContent content)
        {
            try
            {
                _context.OptionContents.Update(content);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
        public async Task<ResultCode> DeleteOptionContentsAsync(int id)
        {
            try
            {
                var currentModel = await _context.OptionContents.SingleOrDefaultAsync(s => s.Id == id);
                if (currentModel == null)
                    return ResultCode.Error;
                _context.OptionContents.Remove(currentModel);
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
