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
    public class FilesAttachmentService : IFilesAttachmentService
    {
        private readonly ApplicationContext _context;
        public FilesAttachmentService(ApplicationContext context)
        {
            _context = context;
        }

        public async  Task<ResultCode> AddFilesAttachemntAsync(FilesAttachment fileAttachment)
        {
            try
            {
                fileAttachment.Id = 0;
                _context.FilesAttachments.Add(fileAttachment);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception e)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteFilesAttachmentAsync(int fileId)
        {
            try
            {
                var model = await _context.FilesAttachments.SingleOrDefaultAsync(f => f.Id == fileId);
                if (model!=null)
                {
                     _context.FilesAttachments.Remove(model);
                    return ResultCode.Success;
                }
                return ResultCode.Error;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<FilesAttachment> GetFilesAttachmentAsync(int fileId)
        {
            return await _context.FilesAttachments.Include(s => s.File).SingleOrDefaultAsync(s => s.FileId == fileId);
        }

        public async Task<ResultCode> UpdateFilesAttachmentAsync(FilesAttachment fileAttachment)
        {
            try
            {
                var model = await _context.FilesAttachments.SingleOrDefaultAsync(f => f.Id == fileAttachment.Id);
                if (model != null)
                {
                    _context.FilesAttachments.Update(fileAttachment);
                    return ResultCode.Success;
                }
                return ResultCode.Error;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
    }
}
