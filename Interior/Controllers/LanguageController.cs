using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Enums;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private ILanguageService _languageService;
        private IFileService _fileService;
        IHostingEnvironment _appEnvironment;

        private IMapper _mapper;
        public LanguageController(ILanguageService languageService, IMapper mapper, IFileService fileService, IHostingEnvironment appEnvironment)
        {
            _languageService = languageService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetLanguages()
        {
            try
            {
                var (data, count) = await _languageService.GetLanguagesAsync();
                var newData = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageShowViewModel>>(data);
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
        [HttpPost("create-language")]
        public async Task<IActionResult> AddLanguages([FromForm]LanguageViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!Directory.Exists("/Files"))
                        Directory.CreateDirectory("/Files");
                    var fileName =  DateTime.Now.Ticks + Path.GetExtension(model.File.FileName);
                    string filePath = Path.Combine(_appEnvironment.WebRootPath, "Files", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }
                    FileStorage file = new FileStorage { Name = model.File.Name, Path = Path.GetDirectoryName(filePath) };
                    var currentFile = await _fileService.AddFileAsync(file);
                    if (currentFile != null)
                    {
                        Language language = new Language { Name = model.Name, FileId = currentFile.Id, Code = model.Code };
                        var resultCode = await _languageService.AddLanguageAsync(language);
                        if (resultCode != ResultCode.Error)
                        {
                            return Ok(ResponseSuccess.Create("Success"));
                        }
                        return BadRequest(ResponseError.Create("Can't create language"));
                    }
                    return BadRequest(ResponseError.Create("Can't create file"));

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