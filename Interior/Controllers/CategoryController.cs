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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly long _fileSize;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFilesAttachmentService _filesAttachmentService;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper,
            IFileService fileService, 
            IHostingEnvironment appEnvironment,
            IOptions<AppSettings> settings,
            IContentService contentService,
            IFilesAttachmentService filesAttachmentService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _fileSize = settings.Value.FileSize;
            _contentService = contentService;
            _filesAttachmentService = filesAttachmentService;

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var (data, count) = await _categoryService.GetAllCategoriesAsync();
                var newData = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryShowViewModel>>(data);
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
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var model = await _categoryService.GetCategoryById(id);
                List<ContentViewModel> modelContents = new List<ContentViewModel>();
                foreach (var item in model.Contents)
                {
                    modelContents.Add(new ContentViewModel { Text = item.Text, LanguageId = item.LanguageId, Id = item.Id });
                }
                var result = new CreateRequestBrandViewModel { Id = model.Id, Contents = modelContents };

                if (model.FilesAttachment.File != null)
                {
                    var currentFile = _fileService.DownloadFile(Path.GetFileName(model.FilesAttachment.File.Path));
                    var fileViewModel = new FileViewModel { FileId = model.FilesAttachment.FileId, FileName = model.FilesAttachment.File.Name, ImageData = currentFile.FileContents, ImageMimeType = currentFile.ContentType };
                    result.CurrentFile = fileViewModel;
                }
                return Ok(ResponseSuccess.Create(result));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
        }
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromForm]CreateResponseCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;
                    int? fileID = null;
                    if (model.File != null)
                    {

                        FileStorage file = await _fileService.UploadFileAsync(model.File);
                        var currentFile = await _fileService.AddFileAsync(file);
                        if (currentFile != ResultCode.Error)
                            fileID = file.Id;
                        else
                            return BadRequest(ResponseError.Create("Can't create file"));

                    }
                    Category category = new Category { Id = 0 };
                    var currentCategory = await _categoryService.AddCategoryAsync(category);
                    if (currentCategory == ResultCode.Success)
                    {
                        if (fileID != null)
                        {
                            FilesAttachment filesAttachment = new FilesAttachment { CategoryId = category.Id, FileId = (int)fileID, FileType = (byte)FileType.Image };
                            await _filesAttachmentService.AddFilesAttachemntAsync(filesAttachment);
                        }


                        if (model.Contents != null)
                        {
                            IEnumerable<ContentViewModel> contentModel = JsonConvert.DeserializeObject<IEnumerable<ContentViewModel>>(model.Contents);
                            var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(contentModel);
                            foreach (var content in currentContents)
                            {
                                content.CategoryId = category.Id;
                                if (String.IsNullOrEmpty(content.Text))
                                    await _contentService.DeleteTextToContentAsync(content.Id);
                                else if (content.Id > 0)
                                    await _contentService.EditTextToContentAsync(content);
                                else
                                    await _contentService.AddTextToContentAsync(content);

                            }
                        }

                        return Ok(ResponseSuccess.Create("Success"));

                    }
                    return BadRequest(ResponseError.Create("Can't create category"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));

            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
        [HttpPost("edit-category")]
        public async Task<IActionResult> EditCategory([FromForm]CreateResponseCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldCategory = await _categoryService.GetCategoryById(model.Id);
                    if (oldCategory == null)
                        return BadRequest(ResponseError.Create("not found category"));
                    int? fileID = null;
                    if (model.File != null)
                    {
                        FileViewModel fileView = JsonConvert.DeserializeObject<FileViewModel>(model.CurrentFile);

                        FileStorage file = await _fileService.UploadFileAsync(model.File);
                        file.Id = fileView.FileId;
                        ResultCode currentFileStatusCode = ResultCode.Error;
                        if (fileView.FileId > 0)
                            currentFileStatusCode = await _fileService.UpdateFileAsync(file);
                        else
                            currentFileStatusCode = await _fileService.AddFileAsync(file);


                        if (currentFileStatusCode != ResultCode.Error)
                            fileID = fileView.FileId;
                        else
                            return BadRequest(ResponseError.Create("Can't create file"));

                    }
                    Category category = new Category { Id = model.Id };
                    var currentCategory = await _categoryService.UpdateCategoryAsync(category);
                    if (currentCategory == ResultCode.Success)
                    {

                        if (fileID != null)
                        {
                            FilesAttachment filesAttachment = new FilesAttachment { CategoryId = category.Id, FileId = (int)fileID, FileType = (byte)FileType.Image };
                            var CurrentFilesAttachment = await _filesAttachmentService.GetFilesAttachmentAsync(filesAttachment.FileId);
                            if (CurrentFilesAttachment == null)
                                await _filesAttachmentService.AddFilesAttachemntAsync(filesAttachment);
                        }

                        IEnumerable<ContentViewModel> contentModel = JsonConvert.DeserializeObject<IEnumerable<ContentViewModel>>(model.Contents);
                        var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(contentModel);
                        foreach (var content in currentContents)
                        {
                            content.CategoryId = model.Id;
                            if (String.IsNullOrEmpty(content.Text))
                                await _contentService.DeleteTextToContentAsync(content.Id);
                            else if (content.Id > 0)
                                await _contentService.EditTextToContentAsync(content);
                            else
                                await _contentService.AddTextToContentAsync(content);
                        }
                        return Ok(ResponseSuccess.Create("Success"));

                    }


                    return BadRequest(ResponseError.Create("Can't create category"));

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