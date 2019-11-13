using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Content
    {
        [Key]
        public int Id { get; set; }
        public ContentAttachment ContentAttachment { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public byte ContentType { get; set; }

        public class ContentMapping : IEntityTypeConfiguration<Content>
        {
            public void Configure(EntityTypeBuilder<Content> modelBuilder)
            {
                modelBuilder
                  .HasOne<Language>(s => s.Language)
                  .WithMany(s => s.Content)
                  .HasForeignKey(s => s.LanguageId)
                  .OnDelete(DeleteBehavior.Cascade);
            }
        }

    }
}
