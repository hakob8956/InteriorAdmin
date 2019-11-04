using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int? FileId { get; set; }
        public FileStorage File { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Interior> Interiors { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }

        public DateTime CreatedDate { get; set; }
        public Category()
        {
            this.CreatedDate = DateTime.UtcNow;
        }

    }
}
