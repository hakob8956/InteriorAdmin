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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFilesAttachmentService _filesAttachmentService;
        private readonly IContentAttachmentService _contentAttachmentService;
        public BrandController(
            IBrandService brandService,
            IMapper mapper,
            IFileService fileService,
            IContentService contentService,
            IFilesAttachmentService filesAttachmentService,
            IContentAttachmentService contentAttachmentService)
        {
            _brandService = brandService;
            _mapper = mapper;
            _fileService = fileService;
            _contentService = contentService;
            _filesAttachmentService = filesAttachmentService;
            _contentAttachmentService = contentAttachmentService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                var (data, count) = await _brandService.GetAllBrandsAsync();
                var newData = _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandShowViewModel>>(data);
                var result = new
                {
                    data = newData,
                    lenght = count
                };
                return Ok(ResponseSuccess.Create(result));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
        }
        [HttpGet("get-byId/{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            try
            {
                var model = await _brandService.GetBrandById(id);
                if (model != null)
                {


                    var result = _mapper.Map<Brand, CreateRequestBrandViewModel>(model);

                    if (model.FilesAttachment?.File != null)
                    {
                        var currentFile = _fileService.DownloadFile(Path.GetFileName(model.FilesAttachment.File.Path));
                        var fileViewModel = new FileViewModel { FileId = model.FilesAttachment.FileId, FileName = model.FilesAttachment.File.Name, ImageData = currentFile.FileContents, ImageMimeType = currentFile.ContentType, FileType = (byte)FileType.Image };
                        result.CurrentFile = fileViewModel;
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
        [HttpPost("create-brand")]
        public async Task<IActionResult> CreateBrand([FromForm]CreateResponseBrandViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;

                    Brand brand = new Brand { Id = 0 };
                    var currentBrand = await _brandService.AddBrandAsync(brand);
                    if (currentBrand == ResultCode.Success)
                    {
                        int? fileID = null;
                        if (model.File != null)
                        {

                            FileStorage file = await _fileService.UploadFileAsync(model.File, FileType.Image);
                            var currentFile = await _fileService.AddFileAsync(file);
                            if (currentFile != ResultCode.Error)
                                fileID = file.Id;
                            else
                                return BadRequest(ResponseError.Create("Can't create file"));

                        }
                        if (fileID != null)
                        {
                            FilesAttachment filesAttachment = new FilesAttachment { BrandId = brand.Id, FileId = (int)fileID };
                            var resultfilesCode = await _filesAttachmentService.AddFilesAttachemntAsync(filesAttachment);
                            if (resultfilesCode != ResultCode.Success)
                            {
                                await _fileService.DeleteFileAsync(filesAttachment.FileId);
                                return BadRequest("Cant create file");
                            };
                        }


                        if (model.Contents != null)
                        {
                            IEnumerable<ContentViewModel> contentModel = JsonConvert.DeserializeObject<IEnumerable<ContentViewModel>>(model.Contents);
                            var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(contentModel);

                            foreach (var content in currentContents)
                            {
                                if (String.IsNullOrEmpty(content.Text))
                                    await _contentService.DeleteTextToContentAsync(content.Id);
                                else if (content.Id > 0)
                                    await _contentService.EditTextToContentAsync(content);
                                else
                                {
                                    content.ContentType = (byte)ContentType.Name;
                                    await _contentService.AddTextToContentAsync(content);
                                    await _contentAttachmentService.AddContentAttachmentAsync(new ContentAttachment { BrandId = brand.Id, ContentId = content.Id });
                                }
                            }

                        }

                        return Ok(ResponseSuccess.Create("Success"));

                    }
                    return BadRequest(ResponseError.Create("Can't create brand"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));

            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
        [HttpPost("edit-brand")]
        public async Task<IActionResult> EditBrand([FromForm]CreateResponseBrandViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldBrand = await _brandService.GetBrandById(model.Id);
                    if (oldBrand == null)
                        return BadRequest(ResponseError.Create("not found brand"));
                    Brand brand = new Brand { Id = model.Id };
                    var currentBrand = await _brandService.UpdateBrandAsync(brand);
                    if (currentBrand == ResultCode.Success)
                    {
                        int? fileID = null;
                        if (model.File != null)
                        {
                            FileViewModel fileView = JsonConvert.DeserializeObject<FileViewModel>(model.CurrentFile);

                            FileStorage file = await _fileService.UploadFileAsync(model.File, FileType.Image);
                            file.Id = fileView.FileId;
                            ResultCode currentFileStatusCode = ResultCode.Error;
                            if (fileView.FileId > 0)
                                currentFileStatusCode = await _fileService.UpdateFileAsync(file);
                            else
                            {
                                currentFileStatusCode = await _fileService.AddFileAsync(file);
                            }


                            if (currentFileStatusCode != ResultCode.Error)
                                fileID = file.Id;
                            else
                                return BadRequest(ResponseError.Create("Can't create file"));

                        }
                        if (fileID != null)
                        {
                            FilesAttachment filesAttachment = new FilesAttachment { BrandId = brand.Id, FileId = (int)fileID };
                            var CurrentFilesAttachment = await _filesAttachmentService.GetFilesAttachmentAsync(filesAttachment.FileId);
                            if (CurrentFilesAttachment == null)
                            {
                                var resultfilesCode = await _filesAttachmentService.AddFilesAttachemntAsync(filesAttachment);
                                if (resultfilesCode != ResultCode.Success)
                                {
                                    await _fileService.DeleteFileAsync(filesAttachment.FileId);
                                    return BadRequest("Cant create file");
                                };
                            }
                        }

                        IEnumerable<ContentViewModel> contentModel = JsonConvert.DeserializeObject<IEnumerable<ContentViewModel>>(model.Contents);
                        var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(contentModel);
                        foreach (var content in currentContents)
                        {
                            if (String.IsNullOrEmpty(content.Text))
                                await _contentService.DeleteTextToContentAsync(content.Id);
                            else if (content.Id > 0)
                                await _contentService.EditTextToContentAsync(content);
                            else
                            {
                                content.ContentType = (byte)ContentType.Name;
                                await _contentService.AddTextToContentAsync(content);
                                await _contentAttachmentService.AddContentAttachmentAsync(new ContentAttachment { BrandId = brand.Id, ContentId = content.Id });
                            }


                        }
                        return Ok(ResponseSuccess.Create("Success"));

                    }


                    return BadRequest(ResponseError.Create("Can't create brand"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));

            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
    }
}