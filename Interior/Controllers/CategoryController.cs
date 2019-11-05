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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private IFileService _fileService;
        IHostingEnvironment _appEnvironment;
        private long _fileSize;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IFileService fileService, IHostingEnvironment appEnvironment, IOptions<AppSettings> settings, IContentService contentService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _fileSize = settings.Value.FileSize;
            _contentService = contentService;
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
                var result = new CreateCategoryViewModel { Id = model.Id, Contents = modelContents, FileName = model.File?.Name };
                return Ok(ResponseSuccess.Create(result));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
            

        }
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = 0;
                    int? fileID = null;
                    if (model.File != null)
                    {
                        if (!Directory.Exists("/Files"))
                            Directory.CreateDirectory("/Files");
                        var fileName = DateTime.Now.Ticks + Path.GetExtension(model.File.FileName);
                        string filePath = Path.Combine(_appEnvironment.WebRootPath, "Files", fileName);
                        if (model.File.Length <= _fileSize)
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await model.File.CopyToAsync(fileStream);
                            }
                            FileStorage file = new FileStorage { Name = model.FileName, Path = filePath };
                            var currentFile = await _fileService.AddFileAsync(file);
                            if (currentFile != null)
                                fileID = currentFile.Id;
                            else
                                return BadRequest(ResponseError.Create("Can't create file"));
                        }
                        else
                        {
                            return BadRequest(ResponseError.Create($"File big then {_fileSize} bytes "));
                        }
                    }
                    Category category = new Category { Id = 0, FileId = fileID };
                    var currentCategory = await _categoryService.AddCategoryAsync(category);
                    if (currentCategory != null)
                    {
                        var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(model.Contents);
                        foreach (var content in currentContents)
                        {
                            content.CategoryId = currentCategory.Id;
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