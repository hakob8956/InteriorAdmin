using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Interior
    {
        [Key]
        public int Id { get; set; }

        public string DeepLinkingUrl { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string IosBundleHref { get; set; }
        public string AndroidBundleHref { get; set; }
        public string GlbHref { get; set; }
        public bool Avaiable { get; set; }
        public bool IsVisible { get; set; }


        public int FileId { get; set; }
        public FileStorage File { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Content> Contents { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }

        public DateTime CreatedDate { get; set; }
        public Interior()
        {
            this.CreatedDate = DateTime.UtcNow;
        }




    }
}
