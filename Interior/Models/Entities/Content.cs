using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string Text { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public int InteriorId { get; set; }
        public Interior Interior { get; set; }
        public int RecommendationId { get; set; }
        public Recommendation Recommendation { get; set; }

        public ICollection<Interior> InteriorsNames { get; set; }
        public ICollection<Interior> InteriorsDescriptions { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<Shop> Shops { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }



    }
}
