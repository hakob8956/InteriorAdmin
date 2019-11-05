﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public class LanguageController : ControllerBase
    {
        private ILanguageService _languageService;
        private IFileService _fileService;
        IHostingEnvironment _appEnvironment;
        private long _fileSize;

        private IMapper _mapper;
        public LanguageController(ILanguageService languageService, IMapper mapper, IFileService fileService, IHostingEnvironment appEnvironment, IOptions<AppSettings> settings)
        {
            _languageService = languageService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _fileSize = settings.Value.FileSize;

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
        [HttpGet("get-byId/{id}")]
        public async Task<IActionResult> GetLanguage(int id)
        {
            try
            {
                var currentLanguage = await _languageService.GetLanguageByIdAsync(id);
                if (currentLanguage != null)
                {
                    FileContentResult currentFile = _fileService.DownloadFile(currentLanguage.File?.Name);
                    LanguageGetViewModel result = new LanguageGetViewModel()
                    {
                        Code = currentLanguage.Code,
                        Id = currentLanguage.Id,
                        Name = currentLanguage.Name,
                        FileName = currentLanguage?.File?.Name,
                        ImageData = currentFile?.FileContents,
                        ImageMimeType = currentFile?.ContentType
                    };
                    return Ok(ResponseSuccess.Create(result));

                }
                return BadRequest(ResponseError.Create("Can't find language"));

            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
        [HttpPost("edit-language")]
        public async Task<IActionResult> EditLanguage([FromForm]LanguageEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentLanguage = await _languageService.GetLanguageByIdAsync(model.Id);
                    if (currentLanguage != null)
                    {
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
                                if (currentLanguage.File != null)//delete old file
                                    await _fileService.DeleteFileAsync(currentLanguage.File.Id);

                                if (currentFile != null)
                                {
                                    fileID = currentFile.Id;
                                }
                                else
                                    return BadRequest(ResponseError.Create("Can't create file"));
                            }
                            else
                            {
                                return BadRequest(ResponseError.Create($"File big then {_fileSize} bytes "));
                            }
                        }
                        else if (currentLanguage.File != null && model.FileName == currentLanguage?.File?.Name)
                        {
                            fileID = currentLanguage.FileId;
                        }
                        Language language = new Language { Id = model.Id, Name = model.Name, FileId = fileID, Code = model.Code };
                        var resultCode = await _languageService.UpdateLanguageAsync(language);
                        if (resultCode != ResultCode.Error)
                        {
                            return Ok(ResponseSuccess.Create("Success"));
                        }
                        return BadRequest(ResponseError.Create("Can't create language"));
                    }
                    return BadRequest(ResponseError.Create("Can't found language"));

                }
                return BadRequest(ResponseError.Create("Invalid form"));
            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
        [HttpPost("create-language")]
        public async Task<IActionResult> CreateLanguages([FromForm]LanguageEditViewModel model)
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
                        if (/*model.File.Length <= _fileSize*/true)
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
                    Language language = new Language { Name = model.Name, FileId = fileID, Code = model.Code };
                    var resultCode = await _languageService.AddLanguageAsync(language);
                    if (resultCode != ResultCode.Error)
                    {
                        return Ok(ResponseSuccess.Create("Success"));
                    }
                    return BadRequest(ResponseError.Create("Can't create language"));

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