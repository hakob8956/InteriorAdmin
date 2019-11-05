using Interior.Enums;
using Interior.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interior.Models.Interface
{
    public interface IContentService
    {
        Task<ResultCode> AddTextToContentAsync (Content content);



    }
}
