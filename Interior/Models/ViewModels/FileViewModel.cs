using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.ViewModels
{
    public class FileViewModel
    {
        public int FileId { get; set; } = 0;
        public byte FileType { get; set; }

        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
    public class FileIdStorageViewModel
    {
        public int FileId { get; set; }
        public byte FileType { get; set; }
    }
}

