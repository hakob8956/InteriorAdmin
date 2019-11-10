using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class OptionContent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public string Link { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int InteriorId { get; set; }
        public Interior Interior { get; set; }
        public class OptionContentMapping : IEntityTypeConfiguration<OptionContent>
        {

            public void Configure(EntityTypeBuilder<OptionContent> builder)
            {
                builder.HasOne<Language>(s => s.Language)
                .WithMany(s => s.OptionContents)
                .HasForeignKey(s => s.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
            }
        }

    }
}
