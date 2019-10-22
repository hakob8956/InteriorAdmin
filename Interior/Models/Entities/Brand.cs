﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public Content Name { get; set; }

        public string ImageHref { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Interior> Interiors { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public DateTime CreatedDate { get; set; }
        public Brand()
        {
            this.CreatedDate = DateTime.UtcNow;
        }


    }
}
