using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class CreateCategoryViewModel
    {
        public int Id { get; set; }
        public string  FileName { get; set; }
        public ICollection<ContentViewModel> Contents { get; set; }
        public IFormFile File { get; set; }
    }
}
