using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class CategoryAttachment
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? InteriorId { get; set; }
        public Interior Interior { get; set; }
        public int? RecommendationId { get; set; }
        public Recommendation Recommendation { get; set; }
        public class CategoryAttachmentMapping : IEntityTypeConfiguration<CategoryAttachment>
        {

            public void Configure(EntityTypeBuilder<CategoryAttachment> builder)
            {

                builder
                    .HasOne<Category>(s => s.Category)
                    .WithMany(s => s.CategoryAttachments)
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder
                    .HasOne<Interior>(s => s.Interior)
                    .WithMany(s => s.CategoryAttachments)
                    .HasForeignKey(s => s.InteriorId)
                    .OnDelete(DeleteBehavior.Cascade);
                builder
                    .HasOne<Recommendation>(s => s.Recommendation)
                    .WithMany(s => s.CategoryAttachments)
                    .HasForeignKey(s => s.RecommendationId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
