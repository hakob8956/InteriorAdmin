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
    public class FileService:IFileService
    {
        private readonly ApplicationContext _context;
        public FileService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FileStorage> AddFileAsync(FileStorage file)
        {
            try
            {
                file.Id = 0;
                _context.Files.Add(file);
                await _context.SaveChangesAsync();
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResultCode> DeleteFileAsync(FileStorage file)
        {
            try
            {
                var currentFile = await _context.Files.SingleOrDefaultAsync(n => n.Id == file.Id);
                if (currentFile == null)
                    return ResultCode.Error;
                _context.Files.Remove(currentFile);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }

        public async Task<FileStorage> GetFileById(int id)
        {
            return await _context.Files.SingleOrDefaultAsync(i => i.Id == id);
        }



        public async Task<ResultCode> UpdateFileAsync(FileStorage file)
        {
            try
            {
                var currentFile = await _context.Files.SingleOrDefaultAsync(n => n.Id == file.Id);
                if (currentFile == null)
                    return ResultCode.Error;
                _context.Files.Update(currentFile);
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
