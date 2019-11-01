using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class UserShowViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
