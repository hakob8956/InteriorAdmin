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
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    public class RecommendationController : Controller
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFilesAttachmentService _filesAttachmentService;
        private readonly IContentAttachmentService _contentAttachmentService;
        public RecommendationController(
            IRecommendationService recommendationService,
            IMapper mapper,
            IFileService fileService,
            IContentService contentService,
            IFilesAttachmentService filesAttachmentService,
            IContentAttachmentService contentAttachmentService)
        {
            _recommendationService = recommendationService;
            _mapper = mapper;
            _fileService = fileService;
            _contentService = contentService;
            _filesAttachmentService = filesAttachmentService;
            _contentAttachmentService = contentAttachmentService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetRecommendations()
        {
            try
            {
                var (data, count) = await _recommendationService.GetAllRecommendationsAsync();
                var newData = _mapper.Map<IEnumerable<Recommendation>, IEnumerable<RecommendationShowViewModel>>(data);
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
        public async Task<IActionResult> GetRecomendation(int id)
        {
            try
            {
                var model = await _recommendationService.GetRecommendationById(id);
                if (model != null)
                {


                    var result = _mapper.Map<Recommendation, CreateRequestRecommendationViewModel>(model);

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
    }
}
