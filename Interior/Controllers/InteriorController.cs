using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Enums;
using Interior.Helpers;
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
    public class InteriorController : ControllerBase
    {
        private readonly IInteriorService _interiorService;
        private readonly IFileService _fileService;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly long _fileSize;
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        public InteriorController(IInteriorService interiorService, IMapper mapper, IFileService fileService, IHostingEnvironment appEnvironment, IOptions<AppSettings> settings, IContentService contentService)
        {
            _interiorService = interiorService;
            _mapper = mapper;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
            _fileSize = settings.Value.FileSize;
            _contentService = contentService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllInterior(int? skip, int? take, string dir, string field)
        {
            try
            {
                bool? desc = null;
                if (!string.IsNullOrEmpty(dir) && dir != "undefined")
                    desc = dir == "asc" ? false : true;

                (IEnumerable<Interior.Models.Entities.Interior>, int count) tuple = (null, 0);
                tuple = await _interiorService.GetAllInteriorsAsync(skip, take, desc, field);

                var newData = _mapper.Map<IEnumerable<Interior.Models.Entities.Interior>, IEnumerable<InteriorShowViewModel>>(tuple.Item1);
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
               // var result = _mapper.Map<Interior.Models.Entities.Interior,CreateTakeInteriorViewModel>(model);
                return Ok(ResponseSuccess.Create(model));
            }
            catch (Exception)
            {
                return BadRequest(ResponseError.Create("Error"));
            }
        }

    }
}