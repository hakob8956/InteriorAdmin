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
    public class ContentAttachmentService:IContentAttachmentService
    {
        private readonly ApplicationContext _context;
        public ContentAttachmentService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResultCode> AddContentAttachmentAsync(ContentAttachment contentAttachment)
        {

            try
            {
                contentAttachment.Id = 0;
                _context.ContentAttachments.Add(contentAttachment);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> DeleteContentAttachmentAsync(int contentAttachmentId)
        {

            try
            {
                var model = await _context.ContentAttachments.SingleOrDefaultAsync(f => f.Id == contentAttachmentId);
                if (model != null)
                {
                    _context.ContentAttachments.Remove(model);
                    return ResultCode.Success;
                }
                return ResultCode.Error;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ContentAttachment> GetContentAttachmentByContentIdAsync(int contentId)
        {
            return await _context.ContentAttachments.Include(s => s.Content).SingleOrDefaultAsync(s => s.ContentId == contentId);
        }

        public async  Task<ResultCode> UpdateContentAttachmentAsync(ContentAttachment contentAttachment)
        {
            try
            {
                var model = await _context.ContentAttachments.SingleOrDefaultAsync(f => f.Id == contentAttachment.Id);
                if (model != null)
                {
                    _context.ContentAttachments.Update(contentAttachment);
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
