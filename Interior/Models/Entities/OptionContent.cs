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


    }
}
