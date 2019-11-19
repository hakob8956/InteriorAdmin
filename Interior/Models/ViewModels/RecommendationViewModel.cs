using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class RecommendationShowViewModel
    {
        public int Id { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
    public class CreateRequestRecommendationViewModel
    {
        public int Id { get; set; }
        public FileViewModel CurrentFile { get; set; }
        public ICollection<ContentViewModel> Contents { get; set; }
        public int ShopId { get; set; }
        public int InteriorId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
    public class CreateResponseRecommendationViewModel
    {
        public int Id { get; set; }
        public string CurrentFile { get; set; }//Json
        public string Contents { get; set; }
        public IFormFile File { get; set; }
        public int ShopId { get; set; }
        public int InteriorId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
