using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class ContentAttachment
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }

        public string Text { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        public int? RecommendationId { get; set; }
        public Recommendation Recommendation { get; set; }
        public int? InteriorId { get; set; }
        public Entities.Interior Interior { get; set; }

        public class ContentAttachmentMapping : IEntityTypeConfiguration<ContentAttachment>
        {
            public void Configure(EntityTypeBuilder<ContentAttachment> modelBuilder)
            {

                modelBuilder
                    .HasOne<Content>(s => s.Content)
                    .WithOne(s => s.ContentAttachment)
                    .HasForeignKey<ContentAttachment>(s => s.ContentId)
                    .OnDelete(DeleteBehavior.Cascade);
         
                modelBuilder
                    .HasOne<Category>(s => s.Category)
                    .WithMany(s => s.ContentsAttachment)
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder
                  .HasOne<Shop>(s => s.Shop)
                  .WithMany(s => s.ContentsAttachment)
                  .HasForeignKey(s => s.ShopId)
                  .OnDelete(DeleteBehavior.Cascade);

                modelBuilder
                  .HasOne<Brand>(s => s.Brand)
                  .WithMany(s => s.ContentsAttachment)
                  .HasForeignKey(s => s.BrandId)
                  .OnDelete(DeleteBehavior.Cascade);
                modelBuilder
                  .HasOne<Recommendation>(s => s.Recommendation)
                  .WithMany(s => s.ContentsAttachment)
                  .HasForeignKey(s => s.RecommendationId)
                  .OnDelete(DeleteBehavior.Cascade);

                modelBuilder
                   .HasOne<Entities.Interior>(s => s.Interior)
                   .WithMany(s => s.ContentAttachments)
                   .HasForeignKey(s => s.InteriorId)
                   .OnDelete(DeleteBehavior.Cascade);

            }
        }
    }
}

