using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Entities
{
    public class Role
    {
        //public const string Admin = "Admin";
        //public const string User = "User";
        [Key]
        public int Id { get; set; }
        public string Name  { get; set; }
        public ICollection<User> Users { get; set; }
        public DateTime CreatedDate { get; set; }
        public Role()
        {
            this.CreatedDate = DateTime.UtcNow;
        }
    }
}
