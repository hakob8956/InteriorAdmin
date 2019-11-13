using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Enums;
using Interior.Helpers;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorController : ControllerBase
    {
        private readonly IInteriorService _interiorService;
        private readonly IFileService _fileService;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly long _fileSize;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFilesAttachmentService _filesAttachmentService;
        public InteriorController(IInteriorService interiorService,
            IMapper mapper, 
            IFileService fileService,
            IHostingEnvironment appEnvironment,
            IContentService contentService,
            IFilesAttachmentService filesAttachmentService)
        {
            _interiorService = interiorService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _contentService = contentService;
            _filesAttachmentService = filesAttachmentService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllInterior(int? skip, int? take, string dir, string field)
        {
            try
            {
                bool? desc = null;
                if (!string.IsNullOrEmpty(dir) && dir != "undefined")
                    desc = dir == "asc" ? false : true;

                (IEnumerable<Models.Entities.Interior>, int count) tuple = (null, 0);
                tuple = await _interiorService.GetAllInteriorsAsync(skip, take, desc, field);

                var newData = _mapper.Map<IEnumerable<Models.Entities.Interior>, IEnumerable<InteriorShowViewModel>>(tuple.Item1);
                var result = new
                {
                    data = newData,
                    lenght = tuple.count
                };
                return Ok(ResponseSuccess.Create(result));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
        }
        [HttpGet("get-byId/{id}")]
        public async Task<IActionResult> GetInterior(int id)
        {
            try
            {
                var model = await _interiorService.GetByIdAsync(id);
                if (model != null)
                {
                    var result = _mapper.Map<Models.Entities.Interior, InteriorRequestModel>(model);
                    if (model.FilesAttachments != null)
                    {
                        foreach (var item in model.FilesAttachments)
                        {
                            if (item.File != null)
                            {
                                var currentFile = _fileService.DownloadFile(Path.GetFileName(item.File.Path));
                                var fileViewModel = new FileViewModel { FileId = item.FileId, FileName = item.File.Name, ImageData = currentFile.FileContents, ImageMimeType = currentFile.ContentType };
                                switch ((FileType)item.File.FileType)
                                {
                                    case FileType.Image:
                                        result.ImageFile = fileViewModel;
                                        break;
                                    case FileType.AndroidBundle:
                                        result.AndroidFile = fileViewModel;
                                        break;
                                    case FileType.IosBundle:
                                        result.IosFile = fileViewModel;
                                        break;
                                    case FileType.Glb:
                                        result.GlbFile = fileViewModel;
                                        break;
                                }
                            }
                        }
                    }
                    return Ok(ResponseSuccess.Create(result));
                }
                return NotFound(ResponseError.Create("Not found"));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
        }
        [HttpPost("create-interior")]
        public async Task<IActionResult> CreateInterior([FromForm]InteriorResponseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;
                    var files = await UploadFilesAsync(model.ImageFile, model.IosFile, model.AndroidFile, model.GlbFile);
                    foreach (var file in files)
                    {
                        if (file == null)
                            return BadRequest("Not uploade on of files");
                        await _fileService.AddFileAsync(file);
                    }


                    var currentInterior = _mapper.Map<InteriorResponseModel, Interior.Models.Entities.Interior>(model);
                    var resultCode = await _interiorService.AddInteriorAsync(currentInterior);
                    if (resultCode == ResultCode.Success)
                    {
                        foreach(var file in files)
                        {
                            await _filesAttachmentService.AddFilesAttachemntAsync(new FilesAttachment { InteriorId = currentInterior.Id, FileId = file.Id });
                        }

                    }
                    currentInterior.OptionsContents.Select(s => s.InteriorId=currentInterior.I);
                }

            }
            catch (Exception)
            {
                return BadRequest("Unknown Error");
            }
        }

        private async Task<List<FileStorage>> UploadFilesAsync(IFormFile ImageFile,IFormFile IosFile,IFormFile AndroidFile,IFormFile GlbFile)
        {
            List<FileStorage> files = new List<FileStorage>();

            if (ImageFile != null)
                files.Add(await _fileService.UploadFileAsync(ImageFile, FileType.Image));
            if (IosFile != null)
                files.Add(await _fileService.UploadFileAsync(IosFile, FileType.IosBundle));
            if (AndroidFile != null)
                files.Add(await _fileService.UploadFileAsync(AndroidFile, FileType.AndroidBundle));
            if (GlbFile != null)
                files.Add(await _fileService.UploadFileAsync(GlbFile, FileType.Glb));
            return files;
        }

    }
}