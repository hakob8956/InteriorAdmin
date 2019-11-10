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
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string Text { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        


        public int? RecommendationId { get; set; }
        public Recommendation Recommendation { get; set; }

        public class ContentMapping : IEntityTypeConfiguration<Content>
        {

            public void Configure(EntityTypeBuilder<Content> modelBuilder)
            {
                modelBuilder
                .HasOne<Language>(s => s.Language)
                .WithMany(s => s.Contents)
                .HasForeignKey(s => s.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder
                    .HasOne<Category>(s => s.Category)
                    .WithMany(s => s.Contents)
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder
                  .HasOne<Shop>(s => s.Shop)
                  .WithMany(s => s.Contents)
                  .HasForeignKey(s => s.ShopId)
                  .OnDelete(DeleteBehavior.Cascade);

                modelBuilder
                  .HasOne<Brand>(s => s.Brand)
                  .WithMany(s => s.Contents)
                  .HasForeignKey(s => s.BrandId)
                  .OnDelete(DeleteBehavior.Cascade);
                modelBuilder
                  .HasOne<Recommendation>(s => s.Recommendation)
                  .WithMany(s => s.Contents)
                  .HasForeignKey(s => s.RecommendationId)
                  .OnDelete(DeleteBehavior.Cascade);

                //modelBuilder
                //  .HasOne<Entities.Interior>(s => s.InteriorName)
                //  .WithMany(s => s.InteriorNames)
                //  .HasForeignKey(s => s.InteriorNameId)
                //  .OnDelete(DeleteBehavior.Restrict);
                //modelBuilder
                //.HasOne<Entities.Interior>(s => s.InteriorDescription)
                //.WithMany(s => s.InteriorDescriptions)
                //.HasForeignKey(s => s.InteriorDescriptionId)
                //.OnDelete(DeleteBehavior.Restrict);
            }
        }

    }
}
