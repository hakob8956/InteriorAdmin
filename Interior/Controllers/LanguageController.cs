using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interior.Enums;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Interior.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private ILanguageService _languageService;
        private IMapper _mapper;
        public LanguageController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetLanguages()
        {
            try
            {
                var (data,count) = await _languageService.GetLanguagesAsync();
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

    }
}