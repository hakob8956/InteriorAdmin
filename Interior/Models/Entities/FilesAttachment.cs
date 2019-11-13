using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class FilesAttachment
    {
        [Key]
        public int Id { get; set; }
        public int FileId { get; set; }
        public FileStorage File { get; set; }
        [Required]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int? InteriorId { get; set; }
        public Entities.Interior Interior { get; set; }

        public int? LanguageId { get; set; }
        public Language Language { get; set; }
        public int? RecommendationId { get; set; }
        public Recommendation Recommendation { get; set; }
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }


        public class FilesAttachmentMapping : IEntityTypeConfiguration<FilesAttachment>
        {

            public void Configure(EntityTypeBuilder<FilesAttachment> builder)
            {
                builder.HasOne<FileStorage>(s => s.File)
                    .WithOne(s => s.FilesAttachment)
                    .HasForeignKey<FilesAttachment>(s => s.FileId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne<Brand>(s => s.Brand)
                    .WithOne(s => s.FilesAttachment)
                    .HasForeignKey<FilesAttachment>(s => s.BrandId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne<Category>(s => s.Category)
                  .WithOne(s => s.FilesAttachment)
                  .HasForeignKey<FilesAttachment>(s => s.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne<Entities.Interior>(s => s.Interior)
                  .WithMany(s => s.FilesAttachments)
                  .HasForeignKey(s => s.InteriorId)
                  .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne<Language>(s => s.Language)
                 .WithOne(s => s.FilesAttachment)
                 .HasForeignKey<FilesAttachment>(s => s.LanguageId)
                 .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne<Recommendation>(s => s.Recommendation)
               .WithOne(s => s.FilesAttachment)
               .HasForeignKey<FilesAttachment>(s => s.RecommendationId)
               .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne<Shop>(s => s.Shop)
               .WithOne(s => s.FilesAttachment)
               .HasForeignKey<FilesAttachment>(s => s.ShopId)
               .OnDelete(DeleteBehavior.Cascade);
            }
        }



    }
}
