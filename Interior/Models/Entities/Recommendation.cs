using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        public FilesAttachment FilesAttachment { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public virtual ICollection<ContentAttachment> ContentsAttachment { get; set; }

        public DateTime CreatedDate { get; set; }
        public Recommendation()
        {
            this.CreatedDate = DateTime.UtcNow;
        }
        public class RecommendationMapping : IEntityTypeConfiguration<Recommendation>
        {

            public void Configure(EntityTypeBuilder<Recommendation> builder)
            {

                    builder
                   .HasOne<Category>(s => s.Category)
                   .WithMany(s => s.Recommendations)
                   .HasForeignKey(s => s.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

                builder
                    .HasOne<Brand>(s => s.Brand)
                    .WithMany(s => s.Recommendations)
                    .HasForeignKey(s => s.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder
                    .HasOne<Shop>(s => s.Shop)
                    .WithMany(s => s.Recommendations)
                    .HasForeignKey(s => s.ShopId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }

    }
}
