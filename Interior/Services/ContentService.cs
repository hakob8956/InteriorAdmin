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
    public class ContentService : IContentService
    {
        private readonly ApplicationContext _context;
        public ContentService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddTextToContentAsync(Content content)
        {
            try
            {
                content.Id = 0;
                _context.Contents.Add(content);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
        public async Task<ResultCode> EditTextToContentAsync(Content content)
        {
            try
            {
                _context.Contents.Update(content);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
        public async Task<ResultCode> DeleteTextToContentAsync(int id)
        {
            try
            {
                var currentModel = await _context.Contents.SingleOrDefaultAsync(s => s.Id == id);
                if (currentModel == null)
                    return ResultCode.Error;
                _context.Contents.Remove(currentModel);
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
