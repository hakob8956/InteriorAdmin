using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IFileService
    {
        Task<FileStorage> AddFileAsync(FileStorage file);
        Task<ResultCode> DeleteFileAsync(FileStorage file);
        Task<ResultCode> UpdateFileAsync(FileStorage file);
        Task<FileStorage> GetFileById(int id);
    }
}
