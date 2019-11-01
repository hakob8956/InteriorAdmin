using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public ICollection<Language> Languages { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Shop> Shops { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<Interior> Interiors { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }

    }
}
