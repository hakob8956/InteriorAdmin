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
        private readonly IHostingEnvironment _appEnvironment;
        private readonly long _fileSize;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        public BrandController(IBrandService brandService, IMapper mapper, IFileService fileService, IHostingEnvironment appEnvironment, IOptions<AppSettings> settings, IContentService contentService)
        {
            _brandService = brandService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _fileSize = settings.Value.FileSize;
            _contentService = contentService;
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
                List<ContentViewModel> modelContents = new List<ContentViewModel>();
                foreach (var item in model.Contents)
                {
                    modelContents.Add(new ContentViewModel { Text = item.Text, LanguageId = item.LanguageId, Id = item.Id });
                }
                var result = new CreateBrandViewModel { Id = model.Id, Contents = modelContents, FileName = model.File?.Name };
                return Ok(ResponseSuccess.Create(result));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
        }
        [HttpPost("create-brand")]
        public async Task<IActionResult> CreateBrand([FromForm]CreateTakeBrandViewModel model)
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
                    Brand brand = new Brand { Id = 0, FileId = fileID };
                    var currentBrand = await _brandService.AddBrandAsync(brand);
                    if (currentBrand == ResultCode.Success)
                    {
                        IEnumerable<ContentViewModel> contentModel = JsonConvert.DeserializeObject<IEnumerable<ContentViewModel>>(model.Contents);
                        var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(contentModel);
                        foreach (var content in currentContents)
                        {
                            content.BrandId = brand.Id;
                            if (String.IsNullOrEmpty(content.Text))
                                await _contentService.DeleteTextToContentAsync(content.Id);
                            else if (content.Id > 0)
                                await _contentService.EditTextToContentAsync(content);
                            else
                                await _contentService.AddTextToContentAsync(content);

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
        public async Task<IActionResult> EditBrand([FromForm]CreateTakeBrandViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldBrand = await _brandService.GetBrandById(model.Id);
                    if (oldBrand == null)
                        return BadRequest(ResponseError.Create("not found brand"));
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
                    Brand brand = new Brand { Id = model.Id, FileId = fileID };
                    var currentBrand = await _brandService.UpdateBrandAsync(brand);
                    if (currentBrand == ResultCode.Success)
                    {
                        IEnumerable<ContentViewModel> contentModel = JsonConvert.DeserializeObject<IEnumerable<ContentViewModel>>(model.Contents);
                        var currentContents = _mapper.Map<IEnumerable<ContentViewModel>, IEnumerable<Content>>(contentModel);
                        foreach (var content in currentContents)
                        {
                            content.BrandId = model.Id;
                            if (String.IsNullOrEmpty(content.Text))
                                await _contentService.DeleteTextToContentAsync(content.Id);
                            else if (content.Id > 0)
                                await _contentService.EditTextToContentAsync(content);
                            else
                                await _contentService.AddTextToContentAsync(content);
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