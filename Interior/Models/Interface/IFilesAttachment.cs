﻿using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IFilesAttachment
    {
        Task<ResultCode> AddFilesAttachemntAsync(FilesAttachment fileAttachment);
        Task<ResultCode> DeleteFilesAttachmentAsync(int fileId);
        Task<ResultCode> UpdateFilesAttachmentAsync(FilesAttachment fileAttachment);

    }
}
