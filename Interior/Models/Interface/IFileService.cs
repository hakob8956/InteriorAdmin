using Interior.Enums;
using Interior.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IFileService
    {
        Task<FileStorage> AddFileAsync(FileStorage file);
        Task<ResultCode> DeleteFileAsync(int fileId);
        Task<ResultCode> UpdateFileAsync(FileStorage file);
        Task<FileStorage> GetFileById(int id);
        FileContentResult DownloadFile(string filename);
        string GetMimeType(string fileName);
    }
}
