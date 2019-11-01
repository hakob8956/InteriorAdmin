using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        public int FileId { get; set; }
        public File File { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public ICollection<Content> Contents { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Recommendation()
        {
            this.CreatedDate = DateTime.UtcNow;
        }

    }
}
