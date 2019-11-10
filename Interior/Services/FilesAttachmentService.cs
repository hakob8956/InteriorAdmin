using Interior.Enums;
using Interior.Models.EFContext;
using Interior.Models.Entities;
using Interior.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Services
{
    public class FilesAttachmentService : IFilesAttachment
    {
        private readonly ApplicationContext _context;
        public FilesAttachmentService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<ResultCode> AddFilesAttachemntAsync(FilesAttachment fileAttachment)
        {
            throw new NotImplementedException();
        }

        public Task<ResultCode> DeleteFilesAttachmentAsync(int fileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultCode> UpdateFilesAttachmentAsync(FilesAttachment fileAttachment)
        {
            throw new NotImplementedException();
        }
    }
}
