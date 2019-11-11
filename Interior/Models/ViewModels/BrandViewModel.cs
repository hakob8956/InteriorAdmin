using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class BrandShowViewModel
    {
        public int Id { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
    public class CreateRequestBrandViewModel
    {
        public int Id { get; set; }
        public FileViewModel CurrentFile { get; set; }
        public ICollection<ContentViewModel> Contents { get; set; }
    }
    public class CreateResponseBrandViewModel
    {
        public int Id { get; set; }
        public string CurrentFile { get; set; }//Json
        public string Contents { get; set; }
        public IFormFile File { get; set; }
    }
}
