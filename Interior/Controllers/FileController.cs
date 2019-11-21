using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interior.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IFileService _fileService;
        public FileController(IHostingEnvironment appEnvironment, IFileService fileService)
        {
            _appEnvironment = appEnvironment;
            _fileService = fileService;
        }

        [HttpGet("get-byId/{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            try
            {
                var currentFile = await _fileService.GetFileById(id);
                if (currentFile != null)
                {
                    var type = _fileService.GetMimeType(currentFile.Name);
                    return PhysicalFile(currentFile.Path, type, currentFile.Name);
                }
                return BadRequest("Can't find file");
            }          
             catch (Exception)
            {
                return BadRequest("Unknown Error");
            }
        }
    }
}