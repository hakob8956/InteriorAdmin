using Interior.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class InteriorShowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeepLinkingUrl { get; set; }
        public decimal Price { get; set; }
        public bool Avaiable { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<Content> Contents { get; set; }
        public BrandShowViewModel Brand { get; set; }
    }
    public class InteriorRequestModel
    {
        public int Id { get; set; }
        public Content NameContent { get; set; }
        public Content DescriptionContent { get; set; }
        public FileViewModel ImageFile { get; set; }

        public FileViewModel IosFile { get; set; }
        public FileViewModel AndroidFile { get; set; }

        public FileViewModel GlbFile { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
        public string BuyUrl { get; set; }
        public int ShopId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<OptionContent> OptionContents { get; set; }
    }
    public class InteriorResponseModel
    {
        public int Id { get; set; }
        public string NameContent { get; set; } // Convert Json
        public string DescriptionContent { get; set; } //Convert Json
        public IFormFile ImageFile { get; set; }

        public IFormFile IosFile { get; set; }
        public IFormFile AndroidFile { get; set; }

        public IFormFile GlbFile { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
        public string BuyUrl { get; set; }
        public int ShopId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<OptionContent> OptionContents { get; set; } //convertJson
    }

}
