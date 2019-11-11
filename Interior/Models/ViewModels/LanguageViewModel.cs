using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class LanguageShowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class LanguageEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }

        public string CurrentFile { get; set; }//Json
        public IFormFile File { get; set; }
    }
    public class LanguageRequestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public FileViewModel CurrentFile { get; set; }
    }
}
