using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
    }
    public class UserRegisterByAdminViewModel : UserBase
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
    public class UserUpdateByAdminViewModel : UserBase {
        [Required]
        public int RoleId { get; set; }
    }
    public class UserRegisterByUserViewModel:UserBase
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        public string Token { get; set; }
        public bool IsRemember { get; set; } = false;
    }
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }
    }
}
