using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class ShopShowViewModel
    {
        public int Id { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
    public class CreateRequestShopViewModel
    {
        public int Id { get; set; }
        public FileViewModel CurrentFile { get; set; }
        public ICollection<ContentViewModel> Contents { get; set; }
    }
    public class CreateResponseShopViewModel
    {
        public int Id { get; set; }
        public string CurrentFile { get; set; }//Json
        public string Contents { get; set; }//Json
        public IFormFile File { get; set; }
    }
}
