using System;
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
using Newtonsoft.Json;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IFileService _fileService;
        private readonly IFilesAttachmentService _filesAttachmentService;


        private readonly IMapper _mapper;
        public LanguageController(
            ILanguageService languageService, 
            IMapper mapper, 
            IFileService fileService, 
            IFilesAttachmentService filesAttachmentService)
        {
            _languageService = languageService;
            _mapper = mapper;
            _fileService = fileService;
            _filesAttachmentService = filesAttachmentService;
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
                    var result = new LanguageRequestViewModel { Id = currentLanguage.Id, Code = currentLanguage.Code, Name = currentLanguage.Name };
                    if (currentLanguage.FilesAttachment?.File != null)
                    {
                        var currentFile = _fileService.DownloadFile(Path.GetFileName(currentLanguage.FilesAttachment.File.Path));
                        var fileViewModel = new FileViewModel { FileId = currentLanguage.FilesAttachment.FileId, FileName = currentLanguage.FilesAttachment.File.Name, ImageData = currentFile.FileContents, ImageMimeType = currentFile.ContentType ,FileType=(byte)FileType.Image};
                        result.CurrentFile = fileViewModel;
                    }

                    return Ok(ResponseSuccess.Create(result));

                }
                return BadRequest(ResponseError.Create("Can't find language"));

            }
            catch (Exception e)
            {

                return BadRequest(ResponseError.Create("Unknown error"));
            }
        }
        //TODO FIX CONTENT ADD/DELETE/EDIT TAKE TRUE ID FROM FRONT-END
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
                            FileViewModel fileView = JsonConvert.DeserializeObject<FileViewModel>(model.CurrentFile);

                            FileStorage file = await _fileService.UploadFileAsync(model.File,FileType.Image);
                            file.Id = fileView.FileId;
                            ResultCode currentFileStatusCode = ResultCode.Error;

                            if (fileView.FileId > 0)
                                currentFileStatusCode = await _fileService.UpdateFileAsync(file);
                            else
                                currentFileStatusCode = await _fileService.AddFileAsync(file);
                            

                            if (currentFileStatusCode != ResultCode.Error)
                                fileID = file.Id;
                            else
                                return BadRequest(ResponseError.Create("Can't create file"));
                        }
                
                        Language language = new Language { Id = model.Id, Name = model.Name, Code = model.Code };
                        var resultCode = await _languageService.UpdateLanguageAsync(language);
                        if (resultCode != ResultCode.Error)
                        {
                            if (fileID != null)
                            {
                                FilesAttachment filesAttachment = new FilesAttachment { LanguageId = language.Id, FileId = (int)fileID };
                                var CurrentFilesAttachment = await _filesAttachmentService.GetFilesAttachmentAsync(filesAttachment.FileId);
                                if (CurrentFilesAttachment == null)
                                    await _filesAttachmentService.AddFilesAttachemntAsync(filesAttachment);
                            }
                            return Ok(ResponseSuccess.Create("Success"));
                        }
                        return BadRequest(ResponseError.Create("Can't create language"));
                    }
                    return NotFound(ResponseError.Create("Can't found language"));

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
                        FileViewModel fileView = JsonConvert.DeserializeObject<FileViewModel>(model.CurrentFile);

                        FileStorage file = await _fileService.UploadFileAsync(model.File,FileType.Image);
                        file.Id = fileView.FileId;
                        ResultCode currentFileStatusCode = ResultCode.Error;

                        if (fileView.FileId > 0)
                            currentFileStatusCode = await _fileService.UpdateFileAsync(file);
                        else
                             currentFileStatusCode = await _fileService.AddFileAsync(file);
                        

                        if (currentFileStatusCode != ResultCode.Error)
                            fileID = file.Id;
                        else
                            return BadRequest(ResponseError.Create("Can't create file"));
                    }
                    Language language = new Language { Name = model.Name, Code = model.Code };
                    var resultCode = await _languageService.AddLanguageAsync(language);
                    if (resultCode == ResultCode.Success)
                    {
                        if (fileID != null)
                        {
                            FilesAttachment filesAttachment = new FilesAttachment { LanguageId = language.Id, FileId = (int)fileID };
                            await _filesAttachmentService.AddFilesAttachemntAsync(filesAttachment);
                        }
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