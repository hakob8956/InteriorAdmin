using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class CategoryShowViewModel
    {
        public int Id { get; set; }
        public int? ParentId  { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
    public class CreateRequestCategoryViewModel
    {
        public int Id { get; set; }
        public FileViewModel CurrentFile { get; set; }
        public int? ParentId { get; set; }
        public ICollection<ContentViewModel> Contents { get; set; }
    }
    public class CreateResponseCategoryViewModel
    {
        public int Id { get; set; }
        public string CurrentFile { get; set; }//Json
        public string Contents { get; set; }
        public int? ParentId { get; set; }

        public IFormFile File { get; set; }
    }
}
