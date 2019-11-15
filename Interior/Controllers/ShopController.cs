using AutoMapper;
using Interior.Enums;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFilesAttachmentService _filesAttachmentService;
        private readonly IContentAttachmentService _contentAttachmentService;


        public ShopController(IShopService shopService,
            IMapper mapper,
            IFileService fileService,
            IContentService contentService,
            IFilesAttachmentService filesAttachmentService,
            IContentAttachmentService contentAttachmentService)
        {
            _shopService = shopService;
            _mapper = mapper;
            _fileService = fileService;
            _filesAttachmentService = filesAttachmentService;
            _contentService = contentService;
            _contentAttachmentService = contentAttachmentService;

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetShops()
        {
            try
            {
                var (data, count) = await _shopService.GetAllShopsAsync();
                var newData = _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopShowViewModel>>(data);
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
        public async Task<IActionResult> GetShop(int id)
        {
            try
            {
                var model = await _shopService.GetShopById(id);
                if (model != null)
                {
                    var result = _mapper.Map<Shop, CreateRequestShopViewModel>(model);

                    if (model.FilesAttachment?.File != null)
                    {
                        var currentFile = _fileService.DownloadFile(Path.GetFileName(model.FilesAttachment.File.Path));
                        var fileViewModel = new FileViewModel { FileId = model.FilesAttachment.FileId, FileName = model.FilesAttachment.File.Name, ImageData = currentFile.FileContents, ImageMimeType = currentFile.ContentType,FileType=(byte)FileType.Image };
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
        [HttpPost("create-shop")]
        public async Task<IActionResult> CreateShop([FromForm]CreateResponseShopViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;
                   
                    Shop shop = new Shop { Id = 0 };
                    var currentShop = await _shopService.AddShopAsync(shop);
                    if (currentShop == ResultCode.Success)
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
                            FilesAttachment filesAttachment = new FilesAttachment { ShopId = shop.Id, FileId = (int)fileID };
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
                                    await _contentAttachmentService.AddContentAttachmentAsync(new ContentAttachment { ShopId = shop.Id, ContentId = content.Id });
                                }

                            }
                        }

                        return Ok(ResponseSuccess.Create("Success"));

                    }
                    return BadRequest(ResponseError.Create("Can't create shop"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));

            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
        [HttpPost("edit-shop")]
        public async Task<IActionResult> EditShop([FromForm]CreateResponseShopViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldShop = await _shopService.GetShopById(model.Id);
                    if (oldShop == null)
                        return BadRequest(ResponseError.Create("not found shop"));
                   
                    Shop shop = new Shop { Id = model.Id };
                    var currentShop = await _shopService.UpdateShopAsync(shop);
                    if (currentShop == ResultCode.Success)
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
                                file.FileType = (byte)FileType.Image;
                                currentFileStatusCode = await _fileService.AddFileAsync(file);
                            }


                            if (currentFileStatusCode != ResultCode.Error)
                                fileID = file.Id;
                            else
                                return BadRequest(ResponseError.Create("Can't create file"));

                        }

                        if (fileID != null)
                        {
                            FilesAttachment filesAttachment = new FilesAttachment { ShopId = shop.Id, FileId = (int)fileID };
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
                                await _contentAttachmentService.AddContentAttachmentAsync(new ContentAttachment { ShopId = shop.Id, ContentId = content.Id });
                            }
                        }
                        return Ok(ResponseSuccess.Create("Success"));

                    }


                    return BadRequest(ResponseError.Create("Can't create shop"));

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