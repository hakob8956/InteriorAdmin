using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Interior
    {
        [Key]
        public int Id { get; set; }

        public string DeepLinkingUrl { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public bool Avaiable { get; set; }
        public bool IsVisible { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ContentAttachment> ContentAttachments { get; set; }
        public ICollection<FilesAttachment> FilesAttachments { get; set; }

        public virtual ICollection<Recommendation> Recommendations { get; set; }

        public virtual ICollection<OptionContent> OptionContents { get; set; }

        public DateTime CreatedDate { get; set; }
        public Interior()
        {
            this.CreatedDate = DateTime.UtcNow;
        }
        public class InteriorMapping : IEntityTypeConfiguration<Interior>
        {

            public void Configure(EntityTypeBuilder<Interior> builder)
            {
                builder
                  .HasOne<Brand>(s => s.Brand)
                  .WithMany(s => s.Interiors)
                  .HasForeignKey(s => s.BrandId)
                  .OnDelete(DeleteBehavior.Restrict);

                builder
                 .HasOne<Shop>(s => s.Shop)
                 .WithMany(s => s.Interiors)
                 .HasForeignKey(s => s.ShopId)
                 .OnDelete(DeleteBehavior.Restrict);

                builder
                 .HasOne<Category>(s => s.Category)
                 .WithMany(s => s.Interiors)
                 .HasForeignKey(s => s.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            }
        }



    }
}
