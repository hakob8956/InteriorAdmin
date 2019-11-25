using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public FilesAttachment FilesAttachment { get; set; }
        public virtual ICollection<ContentAttachment> ContentsAttachment { get; set; }
        public virtual ICollection<CategoryAttachment> CategoryAttachments { get; set; }
        public virtual ICollection<Category> Parents { get; set; }
  
        public DateTime CreatedDate { get; set; }
        public Category()
        {
            this.CreatedDate = DateTime.UtcNow;
            ContentsAttachment = new Collection<ContentAttachment>();

        }
        public class CategoryMapping : IEntityTypeConfiguration<Category>
        {

            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasOne(s=>s.Parent)
                    .WithMany(s=>s.Parents)
                    .HasForeignKey(s => s.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);            
            }
        }


    }
}
