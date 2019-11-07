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
    public class CreateInteriorViewModel
    {
        public int Id { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
    //public class CreateBrandViewModel
    //{
    //    public int Id { get; set; }
    //    public string FileName { get; set; }
    //    public ICollection<ContentViewModel> Contents { get; set; }
    //    public IFormFile File { get; set; }
    //}
    public class CreateSendInteriorViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public IEnumerable<Content> NameContents { get; set; }
        public IEnumerable<Content> DescriptionContents { get; set; }
        public FileViewModel ImageFile { get; set; }

        public FileViewModel IosFile { get; set; }
        public FileViewModel AndroidFile { get; set; }

        public FileViewModel GlbFile { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public bool IsAvailable { get; set; }
        public string BuyUrl { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
    public class CreateTakeInteriorViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string NameContents { get; set; }
        public string DescriptionContents { get; set; }
        public IFormFile ImageFile { get; set; }

        public IFormFile IosFile { get; set; }
        public IFormFile AndroidFile { get; set; }

        public IFormFile GlbFile { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public bool IsAvailable { get; set; }
        public string BuyUrl { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
