using Interior.Enums;
using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IFileService
    {
        Task<ResultCode> AddFileAsync(FileStorage file);
        Task<ResultCode> DeleteFileAsync(int fileId);
        Task<ResultCode> UpdateFileAsync(FileStorage file);
        Task<FileStorage> GetFileById(int id);
        FileContentResult DownloadFile(string filename);
        Task<FileStorage> UploadFileAsync(IFormFile fileModel);
        string GetMimeType(string fileName);
    }
}
