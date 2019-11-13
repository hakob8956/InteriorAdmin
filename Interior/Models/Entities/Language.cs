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
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public virtual ICollection<Content> Content { get; set; }
        public virtual ICollection<OptionContent> OptionContents { get; set; }

        public DateTime CreatedDate { get; set; }
        public FilesAttachment FilesAttachment { get; set; }
        public Language()
        {
            this.CreatedDate = DateTime.UtcNow;
            Content = new Collection<Content>();
            OptionContents = new Collection<OptionContent>();
        }



    }
}
