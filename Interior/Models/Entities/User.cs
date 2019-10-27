using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Interior.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
      
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public User()
        {
            this.CreatedDate = DateTime.UtcNow;
        }

    }
}
