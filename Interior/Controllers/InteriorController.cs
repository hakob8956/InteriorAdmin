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
using Newtonsoft.Json;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorController : ControllerBase
    {
        private readonly IInteriorService _interiorService;
        private readonly IFileService _fileService;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFilesAttachmentService _filesAttachmentService;
        private readonly IOptionContent _optionContent;
        private readonly IContentAttachmentService _contentAttachmentService;
        public InteriorController(IInteriorService interiorService,
            IMapper mapper,
            IFileService fileService,
            IHostingEnvironment appEnvironment,
            IContentService contentService,
            IFilesAttachmentService filesAttachmentService,
            IOptionContent optionContent,
            IContentAttachmentService contentAttachmentService)
        {
            _interiorService = interiorService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _contentService = contentService;
            _filesAttachmentService = filesAttachmentService;
            _optionContent = optionContent;
            _contentAttachmentService = contentAttachmentService;
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
                                var fileViewModel = new FileViewModel { FileId = item.FileId, FileName = item.File.Name, ImageData = currentFile.FileContents, ImageMimeType = currentFile.ContentType, FileType = item.File.FileType };
                                switch ((FileType)item.File.FileType)
                                {
                                    case FileType.Image:
                                        result.CurrentImageFile = fileViewModel;
                                        break;
                                    case FileType.AndroidBundle:
                                        result.CurrentAndroidFile = fileViewModel;
                                        break;
                                    case FileType.IosBundle:
                                        result.CurrentIosFile = fileViewModel;
                                        break;
                                    case FileType.Glb:
                                        result.CurrentGlbFile = fileViewModel;
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
                    var currentInterior = _mapper.Map<InteriorResponseModel, Interior.Models.Entities.Interior>(model);
                    var resultCode = await _interiorService.AddInteriorAsync(currentInterior);
                    if (resultCode == ResultCode.Success)
                    {
                        var files = await UploadFilesAsync(model.ImageFile, model.IosFile, model.AndroidFile, model.GlbFile);
                        foreach (var file in files)
                        {
                            if (file == null)
                                return BadRequest("Not uploade on of files");
                            await _fileService.AddFileAsync(file);
                        }
                        foreach (var file in files)
                        {
                            await _filesAttachmentService.AddFilesAttachemntAsync(new FilesAttachment { InteriorId = currentInterior.Id, FileId = file.Id });
                        }

                        IEnumerable<OptionContent> optionContents = JsonConvert.DeserializeObject<IEnumerable<OptionContent>>(model.OptionContents);
                        foreach (var item in optionContents)
                        {
                            item.InteriorId = currentInterior.Id;
                            if (String.IsNullOrEmpty(item.Name) && String.IsNullOrEmpty(item.Value))
                                await _optionContent.DeleteOptionContentsAsync(item.Id);
                            else if (item.Id > 0)
                                await _optionContent.EditOptionContentsAsync(item);
                            else
                                await _optionContent.AddOptionContentsAsync(item);
                        }
                        var contentNameStatusCode = await UploadContentsAsync(model.NameContent, ContentType.Name, currentInterior.Id);
                        var contentDescriptionStatusCode = await UploadContentsAsync(model.DescriptionContent, ContentType.Description, currentInterior.Id);
                        if (contentNameStatusCode == ResultCode.Error || contentDescriptionStatusCode == ResultCode.Error)
                            return BadRequest(ResponseError.Create("can't upload content"));

                        return Ok(ResponseSuccess.Create("Success"));
                    }
                    return BadRequest(ResponseError.Create("Can't create interior"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));


            }
            catch (Exception e)
            {
                return BadRequest("Unknown Error");
            }
        }
        [HttpPost("edit-interior")]
        public async Task<IActionResult> EditInterior([FromForm]InteriorResponseModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldInterior = await _interiorService.GetByIdAsync(model.Id);
                    if (oldInterior == null)
                        return BadRequest(ResponseError.Create("not found interior"));

                    var currentInterior = _mapper.Map<InteriorResponseModel, Interior.Models.Entities.Interior>(model);
                    var resultCode = await _interiorService.UpdateInteriorAsync(currentInterior);
                    if (resultCode == ResultCode.Success)
                    {
                        var files = await UploadFilesAsync(model.ImageFile, model.IosFile, model.AndroidFile, model.GlbFile);
                        var filesId = JsonConvert.DeserializeObject<IEnumerable<FileIdStorageViewModel>>(model.FileIdStorage);
                        foreach (var file in files)
                        {
                            if (file == null)
                                return BadRequest("Not uploade on of files");
                            ResultCode currentFileStatusCode = ResultCode.Error;
                            int? fileOldId = filesId.FirstOrDefault(s => s.FileType == file.FileType)?.FileId;
                            file.Id = fileOldId != null?(int)fileOldId:0;
                            if (file.Id != 0)
                                currentFileStatusCode = await _fileService.UpdateFileAsync(file);
                            else
                                currentFileStatusCode = await _fileService.AddFileAsync(file);
                            if (currentFileStatusCode == ResultCode.Error)
                                return BadRequest(ResponseError.Create("Can't create file"));

                        }
                        foreach (var file in files)
                        {
                            var fileAttach = new FilesAttachment { InteriorId = currentInterior.Id, FileId = file.Id };
                            var currentFilesAttachment = await _filesAttachmentService.GetFilesAttachmentAsync(fileAttach.FileId);
                            if (currentFilesAttachment == null)
                            {
                                var resultfilesCode = await _filesAttachmentService.AddFilesAttachemntAsync(fileAttach);
                                if (resultfilesCode == ResultCode.Error)
                                {
                                    await _fileService.DeleteFileAsync(fileAttach.FileId);
                                    return BadRequest("Cant create file");
                                };
                            }
                        }
                        IEnumerable<OptionContent> optionContents = JsonConvert.DeserializeObject<IEnumerable<OptionContent>>(model.OptionContents);
                        foreach (var item in optionContents)
                        {
                            item.InteriorId = currentInterior.Id;
                            if (String.IsNullOrEmpty(item.Name) && String.IsNullOrEmpty(item.Value))
                                await _optionContent.DeleteOptionContentsAsync(item.Id);
                            else if (item.Id > 0)
                                await _optionContent.EditOptionContentsAsync(item);
                            else
                                await _optionContent.AddOptionContentsAsync(item);
                        }
                        var contentNameStatusCode = await UploadContentsAsync(model.NameContent, ContentType.Name, currentInterior.Id);
                        var contentDescriptionStatusCode = await UploadContentsAsync(model.DescriptionContent, ContentType.Description, currentInterior.Id);
                        if (contentNameStatusCode == ResultCode.Error || contentDescriptionStatusCode == ResultCode.Error)
                            return BadRequest(ResponseError.Create("can't upload content"));

                        return Ok(ResponseSuccess.Create("Success"));
                    }
                    return BadRequest(ResponseError.Create("Can't update interior"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));


            }
            catch (Exception e)
            {
                return BadRequest("Unknown Error");
            }
        }
        private async Task<List<FileStorage>> UploadFilesAsync(IFormFile ImageFile, IFormFile IosFile, IFormFile AndroidFile, IFormFile GlbFile)
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
        private async Task<ResultCode> UploadContentsAsync(string content, ContentType contentType, int id)
        {
            var resultCode = ResultCode.Success;
            if (content != null)
            {
                IEnumerable<Content> contentModel = JsonConvert.DeserializeObject<IEnumerable<Content>>(content);
                foreach (var item in contentModel)
                {
                    if (String.IsNullOrEmpty(item.Text))
                        resultCode = await _contentService.DeleteTextToContentAsync(item.Id);
                    else if (item.Id > 0)
                        resultCode = await _contentService.EditTextToContentAsync(item);
                    else
                    {
                        item.ContentType = (byte)contentType;
                        resultCode = await _contentService.AddTextToContentAsync(item);
                        if(resultCode==ResultCode.Success)
                            resultCode = await _contentAttachmentService.AddContentAttachmentAsync(new ContentAttachment { InteriorId = id, ContentId = item.Id });
                    }
                }

            }
            return resultCode;
        }

    }
}