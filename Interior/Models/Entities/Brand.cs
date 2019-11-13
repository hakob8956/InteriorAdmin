using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }


        public FilesAttachment FilesAttachment { get; set; }
        public virtual ICollection<ContentAttachment> ContentsAttachment { get; set; }
        public virtual ICollection<Interior> Interiors { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
        public DateTime CreatedDate { get; set; }
        public Brand()
        {
            this.CreatedDate = DateTime.UtcNow;
            ContentsAttachment = new Collection<ContentAttachment>();
            Interiors = new Collection<Interior>();
            Recommendations = new Collection<Recommendation>();
        }

      

    }
}
