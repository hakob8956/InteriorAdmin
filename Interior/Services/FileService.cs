using Interior.Enums;
using Interior.Models.EFContext;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Services
{
    public class FileService:IFileService
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileService(ApplicationContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;

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

        public async Task<ResultCode> DeleteFileAsync(int fileId)
        {
            try
            {
                var currentFile = await _context.Files.SingleOrDefaultAsync(n => n.Id == fileId);
                if (currentFile == null)
                    return ResultCode.Error;
                _context.Files.Remove(currentFile);
                await _context.SaveChangesAsync();
                File.Delete(currentFile.Path);//Delete File
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
                var currentFile = await _context.Files.AsNoTracking().SingleOrDefaultAsync(n => n.Id == file.Id);
                if (currentFile == null)
                    return ResultCode.Error;
                _context.Files.Update(file);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
        private string GetMimeType(string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        public FileContentResult DownloadFile(string filename)
        {
            try
            {
                var filepath = Path.Combine(this._hostingEnvironment.WebRootPath, "Files", filename);

                var mimeType = this.GetMimeType(filename);

                byte[] fileBytes;

                if (File.Exists(filepath))
                {
                    fileBytes = File.ReadAllBytes(filepath);
                }
                else
                {
                    return null;
                }

                return new FileContentResult(fileBytes, mimeType)
                {
                    FileDownloadName = filename
                };
            }
            
            
            catch (Exception)
            {

                return null;
            }
        }
    }
}
