using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IContentAttachmentService
    {
        Task<ResultCode> AddContentAttachmentAsync(ContentAttachment contentAttachment);
        Task<ResultCode> DeleteContentAttachmentAsync(int contentAttachmentId);
        Task<ResultCode> UpdateContentAttachmentAsync(ContentAttachment contentAttachment);
        Task<ContentAttachment> GetContentAttachmentByContentIdAsync(int contentId);
    }
}
