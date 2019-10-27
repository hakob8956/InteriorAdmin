using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Content> Contents { get; set; }
        public DateTime CreatedDate { get; set; }
        public Language()
        {
            this.CreatedDate = DateTime.UtcNow;
        }


    }
}
