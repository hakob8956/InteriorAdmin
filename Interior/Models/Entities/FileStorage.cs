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
    public class FileStorage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public byte FileType { get; set; }

        public FilesAttachment FilesAttachment { get; set; }
        public DateTime CreatedDate { get; set; }
        public FileStorage()
        {
            this.CreatedDate = DateTime.UtcNow;


        }


    }
}
