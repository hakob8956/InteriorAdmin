using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class ContentViewModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public byte Type { get; set; }
    }
    public class ContentShowViewModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Text { get; set; }
    }
}
